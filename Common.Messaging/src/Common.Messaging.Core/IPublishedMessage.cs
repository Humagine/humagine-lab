namespace Common.Messaging.Core
{
	using System;

	public interface IPublishedMessage<out T> where T : class
	{
		Guid MessageId { get; }
		Guid UserId { get; }
		DateTime Timestamp { get; }
		T Payload { get; }
	}
}