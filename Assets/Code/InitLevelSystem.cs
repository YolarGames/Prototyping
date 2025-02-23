using EasyCS;
using EasyCS.Systems;
using UnityEngine;

public class InitLevelSystem : IInitSystem
{
	public void Init()
	{
		Debug.Log("InitLevelSystem");
	}

	public void CreateFilter(EcsWorld world)
	{
		Debug.Log("CreateFilter for InitLevelSystem");
	}
}