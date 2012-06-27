namespace Common.Messaging
{
	using System;
	using Core;

	public sealed class PublishedMessage<T> : IPublishedMessage<T>
		where T : class
	{
		public Guid MessageId { get; set; }
		public Guid UserId { get; set; }
		public DateTime Timestamp { get; set; }

		public T Payload { get; set; }
	}
}