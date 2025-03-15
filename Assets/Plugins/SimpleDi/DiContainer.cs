using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleDi
{
	public class DiContainer
	{
		private readonly Dictionary<Type, Type> _dependencies = new();

		public Type GetDependency<T>() =>
			GetDependency(typeof(T));

		public Type GetDependency(Type type)
		{
			if (_dependencies.TryGetValue(type, out Type dependency))
				return dependency;

			Debug.LogError($"Container has no dependencies of type {type.Name} registered\n");
			return null;
		}

		public void Register<T>(Lifetime lifetime = Lifetime.Singleton)
		{
			Type type = typeof(T);
			_dependencies.TryAdd(type, type);
		}
	}

	public class DependencyData<T>
	{
		private readonly HashSet<Type> _assignables = new();
		private Lifetime _lifetime;
		private T _dependency;

		public DependencyData<T> As<T1>()
		{
			AssertIsAssignable<T1>();

			_assignables.Add(typeof(T1));

			return this;
		}

		public DependencyData<T> As<T1, T2>()
		{
			AssertIsAssignable<T1>();
			AssertIsAssignable<T2>();

			_assignables.Add(typeof(T1));
			_assignables.Add(typeof(T2));

			return this;
		}

		public DependencyData<T> As<T1, T2, T3>()
		{
			AssertIsAssignable<T1>();
			AssertIsAssignable<T2>();
			AssertIsAssignable<T3>();

			_assignables.Add(typeof(T1));
			_assignables.Add(typeof(T2));
			_assignables.Add(typeof(T3));

			return this;
		}

		public DependencyData<T> Lifetime(Lifetime lifetime)
		{
			_lifetime = lifetime;
			return this;
		}

		private static void AssertIsAssignable<T1>()
		{
			Debug.Assert(typeof(T1).IsAssignableFrom(typeof(T)),
				$"Type {typeof(T1).Name} is not assignable from {typeof(T).Name}");
		}
	}
}