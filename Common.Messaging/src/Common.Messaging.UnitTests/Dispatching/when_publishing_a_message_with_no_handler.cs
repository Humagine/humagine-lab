namespace Common.Messaging.UnitTests.Dispatching
{
	using System;
	using Core;
	using Machine.Specifications;
	using Messaging.Dispatching;

	[Subject(typeof (CommonBus))]
	public class when_publishing_a_message_with_no_handler : using_a_common_bus_as_a_bus
	{
		private static readonly DateTime timestamp = new DateTime(2012, 1, 2);
		private const string data = "Some sort of data";

		private static Exception exception;
		private static IPublishedMessage<TestData> messagePublished;

		private Establish context =
			() => messagePublished = new PublishedMessage<TestData>
			                         	{
			                         		MessageId = Guid.NewGuid(),
			                         		UserId = Guid.NewGuid(),
			                         		Timestamp = timestamp,
			                         		Payload = new TestData {Data = data}
			                         	};

		private Because of =
			() => exception = Catch.Exception(() => bus.Publish(messagePublished));

		private It will_not_have_caused_an_exception =
			() => exception.ShouldBeNull();

		private class TestData
		{
			public string Data { get; set; }
		}
	}
}