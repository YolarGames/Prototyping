namespace SimpleDi
{
	public enum Lifetime
	{
		Singleton = 0,
		Transient = 1,
		Scoped = 2,
	}

	public static class LifetimeExtensions
	{
		public static bool IsSingleton(this Lifetime lifetime)
		{
			return lifetime == Lifetime.Singleton;
		}

		public static bool IsTransient(this Lifetime lifetime)
		{
			return lifetime == Lifetime.Scoped;
		}

		public static bool IsScoped(this Lifetime lifetime)
		{
			return lifetime == Lifetime.Scoped;
		}
	}
}