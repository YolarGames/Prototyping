using System;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

namespace AssetContract
{
	public static class AssetExtensions
	{
		public static int GetFileSizeKb(this Object obj)
		{
			string path = AssetDatabase.GetAssetPath(obj);

			return File.Exists(path)
				? (int)(new FileInfo(path).Length >> 10)
				: 0;
		}

		public static bool IsExtensionMatches(this Object obj, string extension) =>
			AssetDatabase.GetAssetPath(obj).EndsWith(extension, StringComparison.OrdinalIgnoreCase);
	}
}