using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyCS
{
	public class EcsFilter<T1, T2>
		where T1 : struct, IComponent
		where T2 : struct, IComponent
	{
		private readonly EcsWorld _world;
		private readonly HashSet<Type> _includedComponents = new();
		private readonly HashSet<Type> _excludedComponents = new();

		public object this[int i]
		{
			get { throw new NotImplementedException(); }
		}

		public int EntityCount { get; }

		public EcsFilter(EcsWorld world)
		{
			_world = world;
		}

		public EcsFilter<T1, T2> With<TComponent>() where TComponent : struct, IComponent
		{
			Type type = typeof(TComponent);

			if (CheckForDuplicates(of: type, inside: _excludedComponents))
				_includedComponents.Add(type);

			return this;
		}

		public EcsFilter<T1, T2> Without<TComponent>() where TComponent : struct, IComponent
		{
			Type type = typeof(TComponent);

			if (CheckForDuplicates(of: type, inside: _includedComponents))
				_excludedComponents.Add(type);

			return this;
		}

		private static bool CheckForDuplicates(Type of, HashSet<Type> inside)
		{
			Debug.Assert(inside.Contains(of));
			return inside.Contains(of);
		}
	}
}