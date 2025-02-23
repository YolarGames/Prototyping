using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStateService
{
	/// <summary>
	///     Represents an observer that tracks changes to specific properties in a game state.
	/// </summary>
	/// <typeparam name="TState"></typeparam>
	public sealed class GameStateObserver<TState> : IDisposable where TState : IObservableGameState
	{
		private readonly Action<TState> _onChanged;
		private readonly HashSet<string> _trackedProperties;
		private TState _gameState;

		/// <summary>
		///     Initializes a new instance of the <see cref="GameStateObserver{TState}" /> class.
		///     Sets up an observer for the specified game state, tracking changes to the specified properties.
		/// </summary>
		/// <param name="gameState">The game state to observe.</param>
		/// <param name="onChanged">The action to perform when changes are detected in the observed properties.</param>
		/// <param name="properties">The properties of the game state to observe for changes.</param>
		public GameStateObserver(TState gameState, Action<TState> onChanged, params string[] properties)
		{
			_gameState = gameState;
			_gameState.OnChanged += CompareProperties;
			_onChanged = onChanged;
			_trackedProperties = new HashSet<string>(properties);
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///     Removes the observer from the game state and clears the list of tracked properties.
		/// </summary>
		public void Dispose()
		{
			Debug.Assert(!_gameState.IsInTransaction, "Observer is being disposed while a transaction is active.");

			_trackedProperties.Clear();
			_gameState.OnChanged -= CompareProperties;
		}

		/// <summary>
		///     Compares the properties that have changed in the game state against the list of properties being tracked by this
		///     observer.
		///     If a change is detected in any of the tracked properties, the specified action is invoked.
		/// </summary>
		/// <param name="changedProperties">The set of properties that have changed in the game state.</param>
		private void CompareProperties(HashSet<string> changedProperties)
		{
			foreach (string property in changedProperties)
				if (_trackedProperties.Contains(property))
				{
					_onChanged?.Invoke(_gameState);
					return;
				}
		}
	}
}