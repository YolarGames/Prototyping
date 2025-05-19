using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetContract
{
	public sealed class TextureContractAttribute : AssetContractAttributeBase
	{
		private int MaxFileSizeKb { get; }
		private string Extension { get; }
		private Vector2Int Resolution { get; }

		public TextureContractAttribute(
			int width, int height,
			int maxFileSizeKb = 0,
			string extension = null)
		{
			Resolution = new Vector2Int(width, height);
			MaxFileSizeKb = maxFileSizeKb;
			Extension = extension;
		}

		public override bool IsValid(Object asset, out string error)
		{
			error = null;

			if (!asset)
				return false;

			Texture2D texture = asset switch
			{
				Sprite sprite => sprite.texture,
				Texture2D texture2D => texture2D,
				_ => null,
			};

			if (!texture)
				return false;

			if (!IsCorrectResolution(texture))
				error += $"Resolution must be {Resolution.x}x{Resolution.y}. ";

			if (!IsCorrectFileSize(texture))
				error += $"File size must be <= {MaxFileSizeKb} KB. ";

			if (!IsRightFormat(texture))
				error += $"Texture must be in {Extension} format.";

			return error == null;
		}

		public override bool IsSupportedFieldType(Type fieldType, out string error)
		{
			error = $"Asset type {fieldType.Name} is not supported target of attribute {GetType().Name}.";
			return fieldType == typeof(Texture2D)
			       || fieldType == typeof(Sprite);
		}

		private bool IsCorrectResolution(Texture2D texture) =>
			Resolution is { x: <= 0, y: <= 0 }
			|| (texture.width == Resolution.x
			    && texture.height == Resolution.y);

		private bool IsCorrectFileSize(Texture2D texture) =>
			MaxFileSizeKb == 0 || texture.GetFileSizeKb() <= MaxFileSizeKb;

		private bool IsRightFormat(Texture2D texture) =>
			Extension == null || texture.IsExtensionMatches(Extension);
	}
}