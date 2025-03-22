namespace SimpleDi.Installers
{
	public class TestInstaller : MonoInstaller
	{
		public override void Install(IContainerBuilder builder)
		{
			builder.Register<PublishInvokator>(Lifetime.Transient).AsSelf();
		}

		[Inject]
		private void Construct(IObjectResolver resolver)
		{
			var hello1 = resolver.GetService<PublishInvokator>();
			var hello2 = resolver.GetService<PublishInvokator>();
			var hello3 = resolver.GetService<PublishInvokator>();

			hello1.Publish();
			hello2.Publish();
			hello3.Publish();
		}
	}
}