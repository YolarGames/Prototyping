using System;
using System.Collections.Generic;

namespace SimpleDi
{
	public struct Dependency
	{
		public readonly IReadOnlyCollection<Type> AssignableTypes;
		public readonly Lifetime Lifetime;
		public readonly Type DependencyType;
		public object Implementation;

		public Dependency(Type dependencyType, Lifetime lifetime, IReadOnlyCollection<Type> assignableTypes)
		{
			DependencyType = dependencyType;
			Lifetime = lifetime;
			Implementation = null;
			AssignableTypes = assignableTypes;
		}
	}
}