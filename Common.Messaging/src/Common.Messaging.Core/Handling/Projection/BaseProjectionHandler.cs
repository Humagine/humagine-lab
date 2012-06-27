namespace Common.Messaging.Core.Handling.Projection
{
	public abstract class BaseProjectionHandler<T> : IProjectionHandler<T>
		where T : class
	{
		public virtual void Handle(IPublishedMessage<T> message)
		{
			Handle(message.Payload);
		}

		protected abstract void Handle(T payload);
	}
}