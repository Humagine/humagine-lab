namespace Common.Messaging.UnitTests.Dispatching
{
	using System;
	using Castle.MicroKernel.Registration;
	using Core;
	using Core.Handling.Action;
	using Machine.Specifications;
	using Messaging.Dispatching;

	[Subject(typeof (CommonBus))]
	public class when_publishing_a_message_for_a_polymorphic_message_type : using_a_common_bus_as_a_bus
	{
		private static readonly DateTime timestamp = new DateTime(2012, 1, 2);
		private const string stringData = "Some sort of data";
		private const long longData = 15792654865;

		private static IPublishedMessage<ExtendedTestData> messagePublished;
		private static IPublishedMessage<ExtendedTestData> messageReceivedForExtendedTestData;
		private static IPublishedMessage<TestData> messageReceivedForTestData;
		private static IPublishedMessage<object> messageReceivedForObject;

		private Establish context =
			() =>
				{
					windsorContainer.Register(Component.For<IActionHandler<TestData>>().ImplementedBy<TestHandler>());
					windsorContainer.Register(Component.For<IActionHandler<ExtendedTestData>>().ImplementedBy<ExtendedTestHandler>());
					windsorContainer.Register(Component.For<IActionHandler<object>>().ImplementedBy<GlobalHandler>());

					messagePublished = new PublishedMessage<ExtendedTestData>
					                   	{
					                   		MessageId = Guid.NewGuid(),
					                   		UserId = Guid.NewGuid(),
					                   		Timestamp = timestamp,
					                   		Payload = new ExtendedTestData {StringData = stringData, LongData = longData}
					                   	};
				};

		private Because of =
			() => bus.Publish(messagePublished);

		private It will_have_a_received_message_for_object =
			() => messageReceivedForObject.ShouldNotBeNull();

		private It will_have_a_received_message_for_test_data =
			() => messageReceivedForTestData.ShouldNotBeNull();

		private It will_have_a_received_message_for_extended_test_data =
			() => messageReceivedForExtendedTestData.ShouldNotBeNull();

		private It will_have_the_same_message_id =
			() =>
				{
					messageReceivedForObject.MessageId.ShouldEqual(messagePublished.MessageId);
					messageReceivedForTestData.MessageId.ShouldEqual(messagePublished.MessageId);
					messageReceivedForExtendedTestData.MessageId.ShouldEqual(messagePublished.MessageId);
				};

		private It will_have_the_same_user_id =
			() =>
				{
					messageReceivedForObject.UserId.ShouldEqual(messagePublished.UserId);
					messageReceivedForTestData.UserId.ShouldEqual(messagePublished.UserId);
					messageReceivedForExtendedTestData.UserId.ShouldEqual(messagePublished.UserId);
				};

		private It will_have_the_same_timestamp =
			() =>
				{
					messageReceivedForObject.Timestamp.ShouldEqual(messagePublished.Timestamp);
					messageReceivedForTestData.Timestamp.ShouldEqual(messagePublished.Timestamp);
					messageReceivedForExtendedTestData.Timestamp.ShouldEqual(messagePublished.Timestamp);
				};

		private It will_have_a_payload =
			() =>
				{
					messageReceivedForObject.Payload.ShouldNotBeNull();
					messageReceivedForTestData.Payload.ShouldNotBeNull();
					messageReceivedForExtendedTestData.Payload.ShouldNotBeNull();
				};

		private It will_have_the_same_payload =
			() =>
				{
					messageReceivedForObject.Payload.ShouldBeTheSameAs(messagePublished.Payload);
					messageReceivedForTestData.Payload.ShouldBeTheSameAs(messagePublished.Payload);
					messageReceivedForExtendedTestData.Payload.ShouldBeTheSameAs(messagePublished.Payload);
				};

		private class TestData
		{
			public string StringData { get; set; }
		}

		private class ExtendedTestData : TestData
		{
			public long LongData { get; set; }
		}

		private class GlobalHandler : IActionHandler<object>
		{
			public void Handle(IPublishedMessage<object> message)
			{
				messageReceivedForObject = message;
			}
		}

		private class TestHandler : IActionHandler<TestData>
		{
			public void Handle(IPublishedMessage<TestData> message)
			{
				messageReceivedForTestData = message;
			}
		}

		private class ExtendedTestHandler : IActionHandler<ExtendedTestData>
		{
			public void Handle(IPublishedMessage<ExtendedTestData> message)
			{
				messageReceivedForExtendedTestData = message;
			}
		}
	}
}