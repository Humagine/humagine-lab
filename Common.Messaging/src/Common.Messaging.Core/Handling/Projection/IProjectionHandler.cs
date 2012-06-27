namespace Common.Messaging.Core.Handling.Projection
{
	public interface IProjectionHandler<in T>
		where T : class
	{
		void Handle(IPublishedMessage<T> message);
	}
}