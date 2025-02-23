using System.Collections.Generic;

namespace EasyCS.Systems
{
	public class SystemsRunner
	{
		private readonly EcsWorld _world;
		private readonly HashSet<ICleanupSystem> _cleanupSystems = new();
		private readonly HashSet<IInitSystem> _initSystems = new();
		private readonly HashSet<ITickSystem> _tickSystems = new();

		public SystemsRunner(EcsWorld world) =>
			_world = world;

		public void Register<T>() where T : class, ISystem, new()
		{
			var system = new T();

			if (system is IInitSystem initSystem)
				_initSystems.Add(initSystem);

			if (system is ITickSystem tickSystem)
				_tickSystems.Add(tickSystem);

			if (system is ICleanupSystem cleanupSystem)
				_cleanupSystems.Add(cleanupSystem);

			system.CreateFilter(_world);
		}

		public void RunInitSystems()
		{
			foreach (IInitSystem system in _initSystems)
				system.Init();
		}

		public void RunTickSystems(float deltaTime)
		{
			foreach (ITickSystem system in _tickSystems)
				system.Tick(deltaTime);
		}

		public void RunCleanupSystems()
		{
			foreach (ICleanupSystem system in _cleanupSystems)
				system.Cleanup();
		}

		public void Clear()
		{
			_initSystems.Clear();
			_tickSystems.Clear();
			_cleanupSystems.Clear();
		}
	}
}