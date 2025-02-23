using System;
using System.Reflection;

namespace GameStateService
{
	/// <summary>
	///     Represents a transaction for modifying the game state. Ensures modifications are made atomically.
	/// </summary>
	/// <typeparam name="TState">The type of the game state being modified.</typeparam>
	public sealed class GameStateTransaction<TState> : IDisposable where TState : ICommitableGameState
	{
		private readonly Action<TState>[] _onComplete;
		private readonly bool _isNestedTransaction;
		private readonly PropertyInfo[] _properties;
		private readonly TState _snapshot;
		private bool _isAborted = false;

		public TState State { get; }

		/// <summary>
		///     Initializes a new instance of the <see cref="GameStateTransaction{TState}" /> class.
		///     Begins a new transaction or joins an existing one if already in progress.
		/// </summary>
		/// <param name="state">The current state of the game to be modified.</param>
		/// <param name="onComplete">Optional callbacks to be invoked upon successful completion of the transaction.</param>
		public GameStateTransaction(TState state, params Action<TState>[] onComplete)
		{
			State = state;
			_properties = typeof(TState).GetProperties();
			_onComplete = onComplete;
			_snapshot = CreateSnapshot(state);

			if (state.IsInTransaction)
				_isNestedTransaction = true;
			else
				State.BeginTransaction();
		}

		/// <summary>
		///     Finalizes the transaction. Commits changes if not aborted; otherwise, reverts to the snapshot.
		/// </summary>
		public void Dispose()
		{
			if (_isAborted)
				RestoreSnapshot();
			else
				Commit();
		}

		/// <summary>
		///     Aborts the transaction, signaling that changes should be discarded.
		/// </summary>
		public void AbortTransaction() =>
			_isAborted = true;

		/// <summary>
		///     Commits the transaction, applying all changes made to the game state.
		///     Invokes any onComplete actions if the transaction is not nested.
		/// </summary>
		private void Commit()
		{
			if (_isNestedTransaction)
				return;

			State.EndTransaction();
			State.CommitChanges();

			foreach (Action<TState> action in _onComplete)
				action?.Invoke(State);
		}

		/// <summary>
		///     Creates a snapshot of the current game state. This snapshot can be used to revert changes if the transaction is
		///     aborted.
		/// </summary>
		/// <param name="state">The game state to snapshot.</param>
		/// <returns>A snapshot of the provided game state.</returns>
		private TState CreateSnapshot(TState state)
		{
			var snapshot = Activator.CreateInstance<TState>();

			snapshot.BeginTransaction();

			foreach (PropertyInfo property in _properties)
				property.SetValue(snapshot, property.GetValue(state));

			snapshot.EndTransaction();

			return snapshot;
		}

		/// <summary>
		///     Restores the game state to its condition at the start of the transaction, using the snapshot.
		/// </summary>
		private void RestoreSnapshot()
		{
			_snapshot.BeginTransaction();

			foreach (PropertyInfo property in _properties)
				property.SetValue(State, property.GetValue(_snapshot));
		}
	}
}