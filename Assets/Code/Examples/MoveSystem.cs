using EasyCS;
using EasyCS.Systems;
using UnityEngine;

namespace Examples
{
	public class MoveSystem : ITickSystem
	{
		public void Tick(in float deltaTime)
		{
			Debug.Log($"MoveSystem: {deltaTime}");
		}

		public void CreateFilter(EcsWorld world)
		{
			Debug.Log("CreateFilter for MoveSystem");
		}
	}
}