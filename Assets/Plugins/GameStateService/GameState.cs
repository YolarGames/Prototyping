using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStateService
{
	public sealed class GameState : ICommitableGameState, IObservableGameState
	{
		private readonly HashSet<string> _changedProperties = new();

		// Create properties for the game state here
		public GameStateProperty<int> Score { get; private set; }

		public GameState()
		{
			// Initialize the game state properties here
			Score = new GameStateProperty<int>(this, nameof(Score), 0);
		}

		/// <summary>
		///     Indicates whether a transaction is currently active, controlling the ability to modify the game state.
		/// </summary>
		public bool IsInTransaction { get; private set; } = false;

		/// <summary>
		///     Begins a transaction, allowing changes to the game state.
		/// </summary>
		public void BeginTransaction() =>
			IsInTransaction = true;

		/// <summary>
		///     Ends the current transaction, disallowing further changes to the game state.
		/// </summary>
		public void EndTransaction() =>
			IsInTransaction = false;

		/// <summary>
		///     Commits all changes made during the current transaction to the game state.
		///     Notifies observers about the changes.
		/// </summary>
		public void CommitChanges()
		{
			OnChanged?.Invoke(_changedProperties);
			_changedProperties.Clear();
		}

		/// <summary>
		///     Event triggered when properties of the game state change.
		/// </summary>
		public Action<HashSet<string>> OnChanged { get; set; } = _ => { };

		/// <summary>
		///     Marks a property as changed, to trigger notifications if observed.
		/// </summary>
		/// <param name="propertyName">The name of the property that has changed.</param>
		private void SetPropertyChanged(string propertyName) =>
			_changedProperties.Add(propertyName);

		/// <summary>
		///     Checks if a value change is allowed, based on the current transaction state.
		///     Asserts that changes are made within a transaction.
		/// </summary>
		/// <typeparam name="T">The type of the value being changed.</typeparam>
		/// <param name="v1">The original value.</param>
		/// <param name="v2">The new value.</param>
		/// <returns>True if the change is allowed; otherwise, false.</returns>
		private bool CanProcessValueChange<T>(T v1, T v2)
		{
			if (IsInTransaction && !IsValueEquals())
				return true;

			Debug.Assert(IsInTransaction, "Cannot change value outside of transaction");
			return false;

			bool IsValueEquals() =>
				EqualityComparer<T>.Default.Equals(v1, v2);
		}

		public class GameStateProperty<T> where T : struct
		{
			private readonly GameState _gameState;
			private readonly string _propertyName;
			public T Value { get; private set; }

			public GameStateProperty(GameState gameState, string propertyName, T value)
			{
				_gameState = gameState;
				_propertyName = propertyName;
				Value = value;
			}

			public void SetValue(T value)
			{
				if (!_gameState.CanProcessValueChange(Value, value))
					return;

				Value = value;
				_gameState.SetPropertyChanged(_propertyName);
			}
		}
	}
}