namespace GameStateService
{
	public interface ICommitableGameState
	{
		bool IsInTransaction { get; }
		void BeginTransaction();
		void EndTransaction();
		void CommitChanges();
	}
}