using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleDi
{
	public class DiContainer
	{
		private readonly Dictionary<Type, Dependency> _dependencies = new();

		public Dependency GetDependency<T>() =>
			GetDependency(typeof(T));

		public Dependency GetDependency(Type type)
		{
			if (_dependencies.TryGetValue(type, out Dependency dependency))
				return dependency;

			Debug.LogError($"Container has no dependencies of type {type.Name} registered\n");
			return null;
		}

		public void Register<T>(Lifetime lifetime)
		{
			_dependencies.TryAdd(typeof(T), new Dependency(typeof(T), lifetime));
		}
	}
}