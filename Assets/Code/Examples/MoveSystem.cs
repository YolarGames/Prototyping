using EasyCS;
using EasyCS.Systems;
using UnityEngine;

namespace Examples
{
	public class MoveSystem : ITickSystem
	{
		private const string UpdateLogMessage = "MoveSystem.Tick()";
		private EcsFilter<Position, Velocity> _filter;

		public void Tick(float deltaTime)
		{
			float delta = Time.deltaTime;
			for (var i = 0; i < _filter.EntityCount; i++) { }
		}

		public void CreateFilter(EcsWorld world) =>
			_filter = new EcsFilter<Position, Velocity>(world);
	}

	public struct Position : IComponent
	{
		public Vector2 Value;
	}

	public struct Velocity : IComponent
	{
		public float Value;
	}
}