using EasyCS;
using UnityEngine;

namespace Examples
{
	public class EcsTest : MonoBehaviour
	{
		private EcsWorld _world;
		private int _entityId;

		private void Awake()
		{
			_world.RegisterSystem<MoveSystem>();
			_world.RegisterSystem<InitLevelSystem>();
			_world.RegisterSystem<CleanupSystem>();
		}

		private void Start() =>
			_world.SystemsRunner.RunInitSystems();

		private void Update() =>
			_world.SystemsRunner.RunTickSystems(Time.deltaTime);

		private void OnDisable() =>
			_world.SystemsRunner.RunCleanupSystems();
	}
}