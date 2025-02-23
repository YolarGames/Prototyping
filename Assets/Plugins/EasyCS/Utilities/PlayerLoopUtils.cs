// using System;
// using UnityEngine.LowLevel;
//
// namespace EasyCS.Utilities
// {
// 	public static class PlayerLoopUtils
// 	{
// 		public static void RemoveSubSystem<T>(ref PlayerLoopSystem currentLoop, Type systemToRemove)
// 			where T : struct
// 		{
// 			if (currentLoop.type == typeof(T))
// 			{
// 				RemoveSystem(ref currentLoop, systemToRemove);
// 				return;
// 			}
//
// 			for (var i = 0; i < currentLoop.subSystemList.Length; i++)
// 				RemoveSubSystem<T>(ref currentLoop.subSystemList[i], systemToRemove);
// 		}
//
// 		public static bool InsertSystemToLoop<T>(ref PlayerLoopSystem currentLoop, in PlayerLoopSystem ecsLoop)
// 			where T : struct
// 		{
// 			if (currentLoop.type != typeof(T))
// 				return HandleSubsystemLoop<T>(ref currentLoop, in ecsLoop);
//
// 			InsertSubsystem(ref currentLoop, ecsLoop);
// 			return true;
// 		}
//
// 		private static void RemoveSystem(ref PlayerLoopSystem currentLoop, Type systemToRemove)
// 		{
// 			var newLoop = new PlayerLoopSystem[currentLoop.subSystemList.Length - 1];
// 			for (var i = 0; i < currentLoop.subSystemList.Length; i++)
// 				if (currentLoop.subSystemList[i].type != systemToRemove)
// 					newLoop[i] = currentLoop.subSystemList[i];
//
// 			currentLoop.subSystemList = newLoop;
// 		}
//
// 		private static bool HandleSubsystemLoop<T>(ref PlayerLoopSystem currentLoop, in PlayerLoopSystem ecsLoop)
// 			where T : struct
// 		{
// 			if (currentLoop.subSystemList == null)
// 				return false;
//
// 			for (var j = 0; j < currentLoop.subSystemList.Length; j++)
// 			{
// 				if (!InsertSystemToLoop<T>(ref currentLoop.subSystemList[j], in ecsLoop))
// 					continue;
//
// 				return true;
// 			}
//
// 			return false;
// 		}
//
// 		private static void InsertSubsystem(ref PlayerLoopSystem currentLoop, in PlayerLoopSystem ecsLoop)
// 		{
// 			var newLoop = new PlayerLoopSystem[currentLoop.subSystemList.Length + 1];
// 			currentLoop.subSystemList.CopyTo(newLoop, 1);
// 			newLoop[0] = ecsLoop;
// 			currentLoop.subSystemList = newLoop;
// 		}
// 	}
// }