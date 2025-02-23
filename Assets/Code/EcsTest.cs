using System;
using EasyCS;
using UnityEngine;

public class EcsTest : MonoBehaviour
{
	// private readonly int _entityCount = 10000;
	private EcsWorld _world;
	private int _entityId;

	private void Awake()
	{
		_world.RegisterSystem<MoveSystem>();
		_world.RegisterSystem<InitLevelSystem>();
		_world.RegisterSystem<CleanupSystem>();

		Result<float, InvalidOperationException> result = GetResult(true);
		result.Match(
			value => Debug.Log($"Value: {value}"),
			error => Debug.LogError(error.Message)
		);
	}

	private Result<float, InvalidOperationException> GetResult(bool isError)
	{
		return isError ? 0.1f : new InvalidOperationException();
	}
}