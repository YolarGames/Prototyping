using System;

namespace SimpleDi.Builders
{
	public sealed class InstanceDependencyBuilder : DependencyBuilder
	{
		private readonly object _instance;

		public InstanceDependencyBuilder(object instance, Type dependencyType, Lifetime lifetime) : base(
			dependencyType, lifetime) =>
			_instance = instance;

		public override Dependency Build()
		{
			Dependency dependency = base.Build();
			dependency.Implementation = _instance;
			return dependency;
		}
	}
}