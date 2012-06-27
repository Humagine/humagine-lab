namespace Common.Messaging.Core.Handling.Action
{
	public interface IActionHandler<in T>
		where T : class
	{
		void Handle(IPublishedMessage<T> message);
	}
}