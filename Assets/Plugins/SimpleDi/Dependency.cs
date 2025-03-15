using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleDi
{
	public class Dependency
	{
		// private readonly HashSet<Type> _assignables = new();
		// public IReadOnlyCollection<Type> Assignables => _assignables;
		private readonly Lifetime _lifetime;
		public Type ImplementationType { get; }
		public Lifetime Lifetime { get; }
		public object Implementation;
		
		public Dependency(Type implementationType, Lifetime lifetime)
		{
			Lifetime = lifetime;
			ImplementationType = implementationType;
			// assignables
			// 	.ToList()
			// 	.ForEach(assignable => _assignables.Add(assignable));
		}

		// public void As<T1>()
		// {
		// 	AssertIsAssignable<T1>();
		//
		// 	_assignables.Add(typeof(T1));
		// 	_container.RegisterBuilder(this);
		// }
		//
		// public void As<T1, T2>()
		// {
		// 	AssertIsAssignable<T1>();
		// 	AssertIsAssignable<T2>();
		//
		// 	_assignables.Add(typeof(T1));
		// 	_assignables.Add(typeof(T2));
		// }
		//
		// public void As<T1, T2, T3>()
		// {
		// 	AssertIsAssignable<T1>();
		// 	AssertIsAssignable<T2>();
		// 	AssertIsAssignable<T3>();
		//
		// 	_assignables.Add(typeof(T1));
		// 	_assignables.Add(typeof(T2));
		// 	_assignables.Add(typeof(T3));
		// }
		//
		// public void As<T1, T2, T3, T4>()
		// {
		// 	AssertIsAssignable<T1>();
		// 	AssertIsAssignable<T2>();
		// 	AssertIsAssignable<T3>();
		// 	AssertIsAssignable<T4>();
		//
		// 	_assignables.Add(typeof(T1));
		// 	_assignables.Add(typeof(T2));
		// 	_assignables.Add(typeof(T3));
		// 	_assignables.Add(typeof(T4));
		// }
		//
		// public void As<T1, T2, T3, T4, T5>()
		// {
		// 	AssertIsAssignable<T1>();
		// 	AssertIsAssignable<T2>();
		// 	AssertIsAssignable<T3>();
		// 	AssertIsAssignable<T4>();
		// 	AssertIsAssignable<T5>();
		//
		// 	_assignables.Add(typeof(T1));
		// 	_assignables.Add(typeof(T2));
		// 	_assignables.Add(typeof(T3));
		// 	_assignables.Add(typeof(T4));
		// 	_assignables.Add(typeof(T5));
		// }
		//
		// public void AsImplementedInterfaces()
		// {
		// 	Type[] interfaces = ImplementationType.GetInterfaces();
		//
		// 	foreach (Type type in interfaces)
		// 		As(type);
		// }
		//
		// public void AsSelf() =>
		// 	_assignables.Add(ImplementationType);
		//
		// private void As(Type type)
		// {
		// 	_assignables.Add(type);
		// }
		//
		// private void AssertIsAssignable<T1>()
		// {
		// 	Debug.Assert(typeof(T1).IsAssignableFrom(ImplementationType),
		// 		$"Type {typeof(T1).Name} is not assignable from {ImplementationType.Name}");
		// }
	}
}