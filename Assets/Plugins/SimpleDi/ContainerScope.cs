using System.Collections.Generic;
using SimpleDi.Installers;
using UnityEngine;

namespace SimpleDi
{
	public abstract class ContainerScope : MonoBehaviour
	{
		[SerializeField] private List<ScriptableInstaller> _scriptableInstallers;
		[SerializeField] private List<MonoInstaller> _monoInstallers;
		private DiContainer _containerBuilder;
		private DiResolver _resolver;

		private void Awake()
		{
			_containerBuilder = new DiContainer();
			_containerBuilder.RegisterInstance(new DiResolver(_containerBuilder)).As<IObjectResolver>();
			Configure(_containerBuilder);
			InstallTo(_containerBuilder);
			Build();
		}

		protected abstract void Configure(IContainerBuilder builder);

		private void Build() =>
			_containerBuilder.Build();

		private void InstallTo(IContainerBuilder containerBuilder)
		{
			foreach (IInstaller scriptableInstaller in _scriptableInstallers)
				scriptableInstaller.Install(containerBuilder);

			foreach (IInstaller monoInstaller in _monoInstallers)
				monoInstaller.Install(containerBuilder);
		}
	}

	public interface IObjectResolver
	{
		T GetService<T>();
	}
}