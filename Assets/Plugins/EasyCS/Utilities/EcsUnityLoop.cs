// using EasyCS.Systems;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.LowLevel;
// using UnityEngine.PlayerLoop;
//
// namespace EasyCS.Utilities
// {
// 	public static class EcsUnityLoop
// 	{
// 		private static PlayerLoopSystem s_ecsTickSystem;
//
// 		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
// 		private static void Initialize()
// 		{
// 			PlayerLoopSystem currentLoop = PlayerLoop.GetCurrentPlayerLoop();
//
// 			s_ecsTickSystem = CreateTickSystem();
//
// 			if (!PlayerLoopUtils.InsertSystemToLoop<Update>(ref currentLoop, in s_ecsTickSystem))
// 				Debug.Log("Error inserting ECS loop into Unity loop");
//
// 			PlayerLoop.SetPlayerLoop(currentLoop);
// 			Debug.Log("ECS loop inserted into Unity loop");
//
// #if UNITY_EDITOR
// 			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
// 			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
// #endif
// 		}
//
// 		private static void OnPlayModeStateChanged(PlayModeStateChange state)
// 		{
// 			if (state != PlayModeStateChange.ExitingPlayMode)
// 				return;
//
// 			PlayerLoopSystem currentLoop = PlayerLoop.GetCurrentPlayerLoop();
// 			PlayerLoopUtils.RemoveSubSystem<Update>(ref currentLoop, typeof(SystemsRunner));
// 			PlayerLoop.SetPlayerLoop(currentLoop);
// 			SystemsRunner.Clear();
// 		}
//
// 		private static PlayerLoopSystem CreateTickSystem() =>
// 			new()
// 			{
// 				type = typeof(SystemsRunner),
// 				updateDelegate = SystemsRunner.RunTickSystems,
// 				subSystemList = null,
// 			};
// 	}
// }