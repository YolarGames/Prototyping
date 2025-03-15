using UnityEngine;

namespace SimpleDi
{
	public class TestDi : MonoBehaviour
	{
		private void Awake()
		{
			var container = new DiContainer();
			var resolver = new DiResolver(container);

			container.Register<MessagePublisher>();
			container.Register<PublishInvokator>();
			container.Register<MessageService>();

			var hello1 = resolver.GetService<PublishInvokator>();
			var hello2 = resolver.GetService<PublishInvokator>();
			var hello3 = resolver.GetService<PublishInvokator>();
			hello1.Publish();
			hello2.Publish();
			hello3.Publish();
		}
	}

	public class MessagePublisher
	{
		private readonly MessageService _messageService;

		public MessagePublisher(MessageService messageService)
		{
			_messageService = messageService;
		}

		public void PrintHello()
		{
			Debug.Log(_messageService.GetMessage());
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

	public class MessageService
	{
		private readonly int _randomNumber;

		public MessageService() =>
			_randomNumber = Random.Range(0, 1000);

		public string GetMessage() =>
			$"Random Number + {_randomNumber}";
	}
}