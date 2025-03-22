using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleDi
{
	public class TestScope : ContainerScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<MessageProvider>(Lifetime.Transient).As<IMessageProvider>();
			builder.Register<MessagePublisher>(Lifetime.Transient).AsSelf();
		}
	}

	public class MessagePublisher
	{
		private readonly IMessageProvider _messageProvider;

		public MessagePublisher(IMessageProvider messageProvider) =>
			_messageProvider = messageProvider;

		public void PrintHello() =>
			Debug.Log(_messageProvider.GetMessage());
	}

	public class PublishInvokator
	{
		private readonly MessagePublisher _messagePublisher;

		public PublishInvokator(MessagePublisher messagePublisher) =>
			_messagePublisher = messagePublisher;

		public void Publish() =>
			_messagePublisher.PrintHello();
	}

	public interface IMessageProvider
	{
		string GetMessage();
	}

	public class MessageProvider : IMessageProvider
	{
		private readonly int _randomNumber;

		public MessageProvider() =>
			_randomNumber = Random.Range(0, 1000);

		public string GetMessage() =>
			$"Random Number + {_randomNumber}";
	}
}