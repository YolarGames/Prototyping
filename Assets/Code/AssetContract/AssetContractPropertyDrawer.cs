using UnityEditor;
using UnityEngine;

namespace AssetContract
{
	[CustomPropertyDrawer(typeof(AssetContractAttribute), true)]
	public class AssetContractPropertyDrawer : PropertyDrawer
	{
		private bool _hasError;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Object asset = property.objectReferenceValue;
			var contract = (AssetContractAttribute)attribute;


			if (!asset)
			{
				EditorGUI.ObjectField(position, property, label);
				return;
			}

			_hasError = !contract.IsValid(asset, out string error);

			if (!_hasError)
			{
				EditorGUI.ObjectField(position, property, label);
				return;
			}

			DrawFailedPropertyField(position, property, label);
			DrawError(position, error);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (_hasError)
				return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight * 2;
			return base.GetPropertyHeight(property, label);
		}

		private static void DrawFailedPropertyField(Rect position, SerializedProperty property, GUIContent label)
		{
			Color originalColor = GUI.color;
			GUI.color = Color.red;
			Rect fieldRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			EditorGUI.PropertyField(fieldRect, property, label, true);
			GUI.color = originalColor;
		}

		private static void DrawError(Rect position, string error)
		{
			var helpPosition = new Rect(
				position.x,
				position.y + EditorGUIUtility.singleLineHeight,
				position.width,
				EditorGUIUtility.singleLineHeight * 2);

			EditorGUI.HelpBox(helpPosition, error, MessageType.Error);
		}
	}
}