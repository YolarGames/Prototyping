using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SimpleDi
{
	public sealed class DiResolver : IObjectResolver
	{
		private readonly Dictionary<Type, Dependency[]> _resolvedScoped = new();
		private readonly Dictionary<Type, Dependency> _resolvedSingletons = new();
		private readonly IContainerBuilder _container;

		public DiResolver(IContainerBuilder container) =>
			_container = container;

		public T GetService<T>() =>
			(T)GetService(_container.GetDependency<T>().DependencyType);

		private object GetService(Type type)
		{
			Dependency dependency = _container.GetDependency(type);
			Type dependencyType = dependency.DependencyType;

			if (_resolvedSingletons.ContainsKey(dependencyType))
				return dependency.Implementation;

			if (dependency.Implementation != null)
			{
				_resolvedSingletons.Add(dependency.DependencyType, dependency);
				return dependency.Implementation;
			}

			ConstructorInfo[] constructors = dependencyType.GetConstructors();

			if (CanFastResolve(constructors))
				return CreateInstance(dependency);

			AssertHasOneConstructor(dependencyType, constructors);

			return CreateInstance(dependency, ResolveParameters(constructors));
		}

		private static void AssertHasOneConstructor(Type type, ConstructorInfo[] constructors) =>
			Debug.Assert(constructors.Length == 1, $"Type {type.Name} has more than one constructor\n");

		private object[] ResolveParameters(ConstructorInfo[] constructors)
		{
			ConstructorInfo constructor = constructors[0];
			ParameterInfo[] parameters = constructor.GetParameters();
			var resolvedParameters = new object[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
				resolvedParameters[i] = GetService(parameters[i].ParameterType);
			return resolvedParameters;
		}

		private object CreateInstance(Dependency dependency, params object[] parameters)
		{
			dependency.Implementation = Activator.CreateInstance(dependency.DependencyType, parameters);

			if (dependency.Lifetime == Lifetime.Singleton)
				_resolvedSingletons.Add(dependency.DependencyType, dependency);

			return dependency.Implementation;
		}

		private static bool CanFastResolve(ConstructorInfo[] constructors) =>
			constructors.Length == 0 || HasOneParameterlessConstructor(constructors);

		private static bool HasOneParameterlessConstructor(ConstructorInfo[] constructors) =>
			constructors.Length == 1 && constructors[0].GetParameters().Length == 0;
	}
}