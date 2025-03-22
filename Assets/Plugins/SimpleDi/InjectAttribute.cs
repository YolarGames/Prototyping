using System;

namespace SimpleDi
{
	[AttributeUsage(AttributeTargets.Method)]
	public class InjectAttribute : Attribute { }
}