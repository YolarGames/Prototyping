using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetContract
{
	public class AssetContractBuildValidator : IPreprocessBuildWithReport
	{
		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport _)
		{
			var violations = new List<string>();

			MonoBehaviour[] allBehaviours =
				Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

			foreach (MonoBehaviour behaviour in allBehaviours)
				ValidateObjectContracts(behaviour, violations);

			ScriptableObject[] allScriptables = LoadAllAssets<ScriptableObject>();
			foreach (ScriptableObject scriptable in allScriptables)
				ValidateObjectContracts(scriptable, violations);

			if (violations.Count > 0)
			{
				string msg = "Asset Contract Violations:\n" + string.Join("\n", violations);
				throw new BuildFailedException(msg);
			}
		}

		private static void ValidateObjectContracts(Object target, List<string> violations)
		{
			FieldInfo[] fields = target
				.GetType()
				.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			string objectProjectPath = GetTargetObjectProjectPath(target);

			foreach (FieldInfo field in fields)
			{
				object[] attrs = field.GetCustomAttributes(typeof(AssetContractAttributeBase), true);
				if (attrs.Length == 0)
					continue;

				var asset = field.GetValue(target) as Object;
				if (!asset)
					violations.Add($"{objectProjectPath}.{field.Name}: Maybe you forgot to assign this field?");

				var contract = (AssetContractAttributeBase)attrs[0];
				if (!contract.IsSupportedFieldType(field.FieldType, out string error))
					violations.Add($"{objectProjectPath}.{field.Name}: {error}");

				if (!contract.IsValid(asset, out error))
					violations.Add($"{objectProjectPath}.{field.Name}: {error}");
			}
		}

		private static string GetTargetObjectProjectPath(Object obj)
		{
			if (obj is not MonoBehaviour monoBehaviour)
				return obj.name;

			if (PrefabUtility.IsPartOfPrefabAsset(monoBehaviour))
				return monoBehaviour.gameObject.name;

			if (monoBehaviour.gameObject.scene.IsValid())
				return $"{monoBehaviour.gameObject.scene.name}.{monoBehaviour.gameObject.name}";

			return obj.name;
		}

		private static T[] LoadAllAssets<T>() where T : Object
		{
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
			List<T> assets = new();

			foreach (string guid in guids)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				var asset = AssetDatabase.LoadAssetAtPath<T>(path);
				if (asset)
					assets.Add(asset);
			}

			return assets.ToArray();
		}
	}
}