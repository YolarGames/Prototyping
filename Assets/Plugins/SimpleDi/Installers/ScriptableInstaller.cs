using UnityEngine;

namespace SimpleDi.Installers
{
	public abstract class ScriptableInstaller : ScriptableObject, IInstaller
	{
		public abstract void Install(IContainerBuilder builder);
	}
}