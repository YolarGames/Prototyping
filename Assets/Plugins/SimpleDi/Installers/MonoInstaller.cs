using UnityEngine;

namespace SimpleDi.Installers
{
	public abstract class MonoInstaller : MonoBehaviour, IInstaller
	{
		public abstract void Install(IContainerBuilder builder);
	}
}