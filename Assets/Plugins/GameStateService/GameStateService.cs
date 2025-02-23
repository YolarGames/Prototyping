using System;

namespace GameStateService
{
	public sealed class GameStateService : IGameStateService
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="GameStateService" /> class.
		///     Optionally accepts an existing game state to manage.
		/// </summary>
		/// <param name="state">The initial game state to manage, or null to create a new state.</param>
		public GameStateService(GameState state = null) =>
			State = state ?? new GameState();

		public GameState State { get; }

		/// <summary>
		///     Starts a new transaction for modifying the game state.
		///     GameState can be modified only within a transaction.
		/// </summary>
		/// <returns>A new transaction for the current game state.</returns>
		public GameStateTransaction<GameState> StartTransaction() =>
			new(State);

		/// <summary>
		///     Creates a new observer for the game state.
		///     This observer will be notified when specified properties of the game state change.
		/// </summary>
		/// <param name="onChanged">The action to perform when the observed properties change.</param>
		/// <param name="properties">The properties to observe for changes.</param>
		/// <returns>A new game state observer.</returns>
		public GameStateObserver<GameState> CreateObserver(Action<GameState> onChanged, params string[] properties) =>
			new(State, onChanged, properties);
	}

	public interface IGameStateService
	{
		GameState State { get; }

		GameStateTransaction<GameState> StartTransaction();

		GameStateObserver<GameState> CreateObserver(Action<GameState> onChanged, params string[] properties);
	}
}