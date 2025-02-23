using EasyCS;
using EasyCS.Systems;
using UnityEngine;

public class CleanupSystem : ICleanupSystem
{
	public void Cleanup()
	{
		Debug.Log("CleanupSystem");
	}

	public void CreateFilter(EcsWorld world)
	{
		Debug.Log("CreateFilter for CleanupSystem");
	}
}