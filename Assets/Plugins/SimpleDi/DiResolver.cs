using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SimpleDi
{
	public class DiResolver
	{
		private readonly DiContainer _container;
		private readonly Dictionary<Type, object> _resolved = new();

		public DiResolver(DiContainer container) =>
			_container = container;

		public T GetService<T>()
		{
			return (T)GetService(_container.GetDependency<T>());
		}

		private object GetService(Type type)
		{
			Type dependency = _container.GetDependency(type);

			if (_resolved.TryGetValue(dependency, out object value))
				return value;

			ConstructorInfo[] constructors = dependency.GetConstructors();

			if (CanFastResolve(constructors))
			{
				_resolved.Add(dependency, Activator.CreateInstance(dependency));
				return _resolved[dependency];
			}

			Debug.Assert(constructors.Length == 1, $"Type {dependency.Name} has more than one constructor\n");

			ConstructorInfo constructor = constructors[0];
			ParameterInfo[] parameters = constructor.GetParameters();
			var resolvedParameters = new object[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
				resolvedParameters[i] = GetService(parameters[i].ParameterType);

			_resolved.Add(dependency, Activator.CreateInstance(type, resolvedParameters));
			return _resolved[dependency];
		}

		private static bool CanFastResolve(ConstructorInfo[] constructors) =>
			constructors.Length == 0 && HasOneParameterlessConstructor(constructors);

		private static bool HasOneParameterlessConstructor(ConstructorInfo[] constructors) =>
			constructors.Length == 1 && constructors[0].GetParameters().Length == 0;
	}
}