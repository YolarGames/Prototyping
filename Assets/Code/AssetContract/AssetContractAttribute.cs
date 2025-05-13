using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetContract
{
	[AttributeUsage(AttributeTargets.Field)]
	public abstract class AssetContractAttribute : PropertyAttribute
	{
		public abstract bool IsValid(Object asset, out string error);
		public abstract bool IsSupportedFieldType(Type fieldType);
	}
}