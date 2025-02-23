using System.Collections.Generic;
using EasyCS.Systems;
using EasyCS.Utilities;

namespace EasyCS
{
	public class EcsWorld
	{
		private const int EmptyEntity = -1;
		private readonly EntityIdProvider _entityIdProvider;
		private readonly EntityList<int> _entities;
		public SystemsRunner SystemsRunner { get; }

		public EcsWorld(int entityCount = 1000)
		{
			_entityIdProvider = new EntityIdProvider();
			_entities = new EntityList<int>(entityCount);
			SystemsRunner = new SystemsRunner(this);
			_entities.PopulateWith(EmptyEntity);
		}

		public int CreateEntity()
		{
			int newEntity = _entityIdProvider.Get();

			if (newEntity >= _entities.Count)
				_entities.Resize();

			_entities[newEntity] = newEntity;
			return newEntity;
		}

		public void DestroyEntity(in int id)
		{
			_entities[id] = EmptyEntity;
			_entityIdProvider.Free(id);
		}

		public void RegisterSystem<T>() where T : class, ISystem, new() =>
			SystemsRunner.Register<T>();

		private class EntityIdProvider
		{
			private readonly Stack<int> _freeIds = new();
			private int _nextId;

			public int Get()
			{
				return _freeIds.Count > 0
					? _freeIds.Pop()
					: _nextId++;
			}

			public void Free(in int id) =>
				_freeIds.Push(id);
		}
	}
}