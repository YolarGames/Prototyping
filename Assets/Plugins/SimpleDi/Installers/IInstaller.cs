namespace SimpleDi.Installers
{
	public interface IInstaller
	{
		void Install(IContainerBuilder builder);
	}
}