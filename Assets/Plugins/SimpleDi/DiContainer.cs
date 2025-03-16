using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleDi
{
	public class DiContainer : IContainerBuilder
	{
		private readonly Dictionary<Type, Dependency> _dependencies = new();
		private readonly Dictionary<Type, DependencyBuilder> _builders = new();

		public Dependency GetDependency<T>() =>
			GetDependency(typeof(T));

		public Dependency GetDependency(Type type)
		{
			if (_dependencies.TryGetValue(type, out Dependency dependency))
				return dependency;

			Debug.LogError($"Container has no registered type {type.Name}\n");
			return default;
		}

		public DependencyBuilder Register<T>(Lifetime lifetime) =>
			Register(typeof(T), lifetime);

		public DependencyBuilder Register(Type type, Lifetime lifetime)
		{
			Debug.Assert(!_builders.ContainsKey(type), $"Dependency {type.Name} already registered\n");

			_builders.TryAdd(type, new DependencyBuilder(type, lifetime));
			return _builders[type];
		}

		public void Build()
		{
			IEnumerable<Dependency> dependencies = _builders.Values.Select(builder => builder.Build());

			foreach (Dependency dependency in dependencies)
			foreach (Type assignableType in dependency.AssignableTypes)
				_dependencies.Add(assignableType, dependency);
		}
	}

	public interface IContainerBuilder
	{
		Dependency GetDependency<T>();

		Dependency GetDependency(Type type);

		DependencyBuilder Register<T>(Lifetime lifetime);

		DependencyBuilder Register(Type type, Lifetime lifetime);
	}
}