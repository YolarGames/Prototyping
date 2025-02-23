using EasyCS;
using EasyCS.Systems;
using UnityEngine;

namespace Examples
{
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
}