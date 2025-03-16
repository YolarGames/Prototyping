using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleDi
{
	public class TestDi : MonoBehaviour
	{
		[Inject]
		private void Awake()
		{
			var container = new DiContainer();
			var resolver = new DiResolver(container);

			container.Register<MessageProvider>(Lifetime.Transient).As<IMessageProvider>();
			container.Register<MessagePublisher>(Lifetime.Transient).AsSelf();
			container.Register<PublishInvokator>(Lifetime.Transient).AsSelf();
			container.Build();

			var hello1 = resolver.GetService<PublishInvokator>();
			var hello2 = resolver.GetService<PublishInvokator>();
			var hello3 = resolver.GetService<PublishInvokator>();

			hello1.Publish();
			hello2.Publish();
			hello3.Publish();
		}

		private int[] CreateArray() =>
			new[] { 1, 2, 3 };
	}

	public class MessagePublisher
	{
		private readonly IMessageProvider _messageProvider;

		public MessagePublisher(IMessageProvider messageProvider)
		{
			_messageProvider = messageProvider;
		}

		public void PrintHello()
		{
			Debug.Log(_messageProvider.GetMessage());
		}
	}

	public class PublishInvokator
	{
		private readonly MessagePublisher _messagePublisher;

		public PublishInvokator(MessagePublisher messagePublisher)
		{
			_messagePublisher = messagePublisher;
		}

		public void Publish()
		{
			_messagePublisher.PrintHello();
		}
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