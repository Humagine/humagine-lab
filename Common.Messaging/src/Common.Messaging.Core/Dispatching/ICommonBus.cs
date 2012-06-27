namespace Common.Messaging.Core.Dispatching
{
	public interface ICommonBus
	{
		void Publish(params IPublishedMessage<object>[] messages);
	}
}