using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetContract
{
	public sealed class AudioContractAttribute : AssetContractAttribute
	{
		private int MaxFileSizeKb { get; }
		private string Extension { get; }

		public AudioContractAttribute(string extension = null, int maxFileSizeKb = 0, string fileFormat = null)
		{
			Extension = extension;
			MaxFileSizeKb = maxFileSizeKb;
		}

		public override bool IsValid(Object asset, out string error)
		{
			error = null;

			if (!IsValidExtension(asset))
				error += $"Texture must be in {Extension} format. ";

			if (!IsValidFileSize(asset))
				error += $"File size must be <= {MaxFileSizeKb} KB.";

			return error == null;
		}

		public override bool IsSupportedFieldType(Type fieldType, out string error)
		{
			error = $"Asset type {fieldType.Name} is not supported target of attribute {GetType().Name}.";
			return fieldType == typeof(AudioClip);
		}

		private bool IsValidFileSize(Object asset) =>
			MaxFileSizeKb == 0 || asset.GetFileSizeKb() <= MaxFileSizeKb;

		private bool IsValidExtension(Object asset) =>
			Extension == null || asset.IsExtensionMatches(Extension);
	}
}