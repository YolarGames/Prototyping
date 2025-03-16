using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleDi
{
	public class DependencyBuilder
	{
		private readonly HashSet<Type> _assignableTypes = new();
		private readonly Lifetime _lifetime;
		private readonly Type _dependencyType;

		public DependencyBuilder(Type dependencyType, Lifetime lifetime)
		{
			_dependencyType = dependencyType;
			_lifetime = lifetime;
		}

		public DependencyBuilder As<T>() =>
			As(typeof(T));

		public DependencyBuilder As<T1, T2>() =>
			As(typeof(T1), typeof(T2));

		public DependencyBuilder As<T1, T2, T3>() =>
			As(typeof(T1), typeof(T2), typeof(T3));

		public DependencyBuilder As(Type type)
		{
			AssertIsAssignable(type);

			_assignableTypes.Add(type);

			return this;
		}

		public DependencyBuilder As(Type type1, Type type2)
		{
			AssertIsAssignable(type1);
			AssertIsAssignable(type2);

			_assignableTypes.Add(type1);
			_assignableTypes.Add(type2);

			return this;
		}

		public DependencyBuilder As(Type type1, Type type2, Type type3)
		{
			AssertIsAssignable(type1);
			AssertIsAssignable(type2);
			AssertIsAssignable(type3);

			_assignableTypes.Add(type1);
			_assignableTypes.Add(type2);
			_assignableTypes.Add(type3);

			return this;
		}

		public DependencyBuilder AsImplementedInterfaces()
		{
			Type[] interfaces = _dependencyType.GetInterfaces();

			foreach (Type type in interfaces)
				As(type);

			return this;
		}

		public DependencyBuilder AsSelf()
		{
			_assignableTypes.Add(_dependencyType);
			return this;
		}

		public Dependency Build()
		{
			Debug.Assert(_assignableTypes.Count != 0,
				$"The assignable types aren't registered for {_dependencyType.Name}. " +
				"Please use \"As\" methods when registering your types\n");

			return new Dependency(_dependencyType, _lifetime, _assignableTypes);
		}

		private void AssertIsAssignable(Type type) =>
			Debug.Assert(type.IsAssignableFrom(_dependencyType),
				$"Type {type.Name} is not assignable from {_dependencyType.Name}\n");
	}
}