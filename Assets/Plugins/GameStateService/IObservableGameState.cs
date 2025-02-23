using System;
using System.Collections.Generic;

namespace GameStateService
{
	public interface IObservableGameState
	{
		public bool IsInTransaction { get; }
		Action<HashSet<string>> OnChanged { get; set; }
	}
}