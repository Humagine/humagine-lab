namespace Common.Messaging.UnitTests.Dispatching
{
	using System;
	using Castle.MicroKernel.Registration;
	using Core;
	using Core.Handling.Action;
	using Machine.Specifications;
	using Messaging.Dispatching;

	[Subject(typeof (CommonBus))]
	public class when_publishing_a_message_for_an_action_handler : using_a_common_bus_as_a_bus
	{
		private static readonly DateTime timestamp = new DateTime(2012, 1, 2);
		private const string data = "Some sort of data";

		private static IPublishedMessage<TestData> messagePublished;
		private static IPublishedMessage<TestData> messageReceived;

		private Establish context =
			() =>
				{
					windsorContainer.Register(Component.For<IActionHandler<TestData>>().ImplementedBy<TestHandler>());
					messagePublished = new PublishedMessage<TestData>
					                   	{
					                   		MessageId = Guid.NewGuid(),
					                   		UserId = Guid.NewGuid(),
					                   		Timestamp = timestamp,
					                   		Payload = new TestData {Data = data}
					                   	};
				};

		private Because of =
			() => bus.Publish(messagePublished);

		private It will_have_a_received_message =
			() => messageReceived.ShouldNotBeNull();

		private It will_have_the_same_message_id =
			() => messageReceived.MessageId.ShouldEqual(messagePublished.MessageId);

		private It will_have_the_same_user_id =
			() => messageReceived.UserId.ShouldEqual(messagePublished.UserId);

		private It will_have_the_same_timestamp =
			() => messageReceived.Timestamp.ShouldEqual(messagePublished.Timestamp);

		private It will_have_a_payload =
			() => messageReceived.Payload.ShouldNotBeNull();

		private It will_have_the_same_payload =
			() => messageReceived.Payload.ShouldBeTheSameAs(messagePublished.Payload);

		private class TestData
		{
			public string Data { get; set; }
		}

		private class TestHandler : IActionHandler<TestData>
		{
			public void Handle(IPublishedMessage<TestData> message)
			{
				messageReceived = message;
			}
		}
	}
}