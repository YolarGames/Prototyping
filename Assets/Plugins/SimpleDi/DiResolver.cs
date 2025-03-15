using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SimpleDi
{
	public class DiResolver
	{
		private readonly DiContainer _container;
		private readonly Dictionary<Type, Dependency> _resolvedSingletons = new();
		private readonly Dictionary<Type, object[]> _resolvedScoped = new();

		public DiResolver(DiContainer container) =>
			_container = container;

		public T GetService<T>()
		{
			return (T)GetService(_container.GetDependency<T>().ImplementationType);
		}

		private object GetService(Type type)
		{
			Dependency dependency = _container.GetDependency(type);
			Type implementationType = dependency.ImplementationType;

			if (dependency.Lifetime == Lifetime.Singleton
			    && _resolvedSingletons.TryGetValue(implementationType, out Dependency value))
				return value.Implementation;

			ConstructorInfo[] constructors = implementationType.GetConstructors();

			if (CanFastResolve(constructors))
			{
				dependency.Implementation = Activator.CreateInstance(implementationType);

				if (dependency.Lifetime == Lifetime.Singleton)
					_resolvedSingletons.Add(implementationType, dependency);

				return dependency.Implementation;
			}

			Debug.Assert(constructors.Length == 1, $"Type {implementationType.Name} has more than one constructor\n");

			ConstructorInfo constructor = constructors[0];
			ParameterInfo[] parameters = constructor.GetParameters();
			var resolvedParameters = new object[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
				resolvedParameters[i] = GetService(parameters[i].ParameterType);


			dependency.Implementation = Activator.CreateInstance(implementationType, resolvedParameters);

			if (dependency.Lifetime == Lifetime.Singleton)
				_resolvedSingletons.Add(implementationType, dependency);

			return dependency.Implementation;
		}

		private static bool CanFastResolve(ConstructorInfo[] constructors) =>
			constructors.Length == 0 || HasOneParameterlessConstructor(constructors);

		private static bool HasOneParameterlessConstructor(ConstructorInfo[] constructors) =>
			constructors.Length == 1 && constructors[0].GetParameters().Length == 0;
	}
}