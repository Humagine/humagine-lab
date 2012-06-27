namespace Common.Messaging.UnitTests.Dispatching
{
	using System;
	using System.Collections.Generic;
	using Castle.MicroKernel.Registration;
	using CommonDomain;
	using CommonDomain.Core;
	using CommonDomain.Persistence;
	using Core;
	using Core.Handling.Action;
	using Machine.Specifications;
	using Messaging.Dispatching;

	[Subject(typeof (CommonBus))]
	public class when_publishing_a_message_for_a_command_handler : using_a_common_bus_as_a_bus
	{
		private static readonly DateTime timestamp = new DateTime(2012, 1, 2);
		private static Guid aggregateId = Guid.NewGuid();
		private const string data = "Some sort of data";
		private const int version = 127843;

		private static IPublishedMessage<TestData> messagePublished;
		private static TestData commandReceived;
		private static TestAggregate aggregatePassedToCommandHandler;

		private Establish context =
			() =>
				{
					windsorContainer.Register(Component.For<BaseCommandHandler<TestData, TestAggregate>>().ImplementedBy<TestHandler>());
					windsorContainer.Register(Component.For<IRepository>().ImplementedBy<TestRepository>());
					messagePublished = new PublishedMessage<TestData>
					                   	{
					                   		MessageId = Guid.NewGuid(),
					                   		UserId = Guid.NewGuid(),
					                   		Timestamp = timestamp,
					                   		Payload = new TestData {Data = data, AggregateId = aggregateId}
					                   	};
				};

		private Because of =
			() => bus.Publish(messagePublished);

		private It will_have_a_received_a_command =
			() => commandReceived.ShouldNotBeNull();

		private It will_have_received_a_command_with_the_same_aggregate_id =
			() => commandReceived.AggregateId.ShouldEqual(aggregateId);

		private It will_have_received_a_command_with_the_correct_data =
			() => commandReceived.Data.ShouldEqual(data);

		private It will_have_passed_an_aggregate_to_the_command_handler =
			() => aggregatePassedToCommandHandler.ShouldNotBeNull();

		private It will_have_passed_an_aggregate_to_the_command_handler_with_the_correct_aggregate_id =
			() => aggregatePassedToCommandHandler.Id.ShouldEqual(aggregateId);

		private It will_have_passed_an_aggregate_to_the_command_handler_with_the_correct_version =
			() => aggregatePassedToCommandHandler.Version.ShouldEqual(version);


		private class TestData : ICommand
		{
			public Guid AggregateId { get; set; }
			public string Data { get; set; }
		}

		private class TestAggregate : AggregateBase
		{
			public TestAggregate(Guid id, int version)
				: base(null)
			{
				Id = id;
				Version = version;
			}
		}

		private class TestRepository : IRepository
		{
			public TAggregate GetById<TAggregate>(Guid id) 
				where TAggregate : class, IAggregate
			{
				return (TAggregate)Activator.CreateInstance(typeof(TAggregate), id, version);
			}

			public TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
			{
				throw new NotImplementedException();
			}

			public void Save(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
			{
				throw new NotImplementedException();
			}
		}

		private class TestHandler : BaseCommandHandler<TestData, TestAggregate>
		{
			public TestHandler(IRepository repository) 
				: base(repository)
			{
			}

			protected override void HandleCommand(TestAggregate aggregate, TestData command)
			{
				commandReceived = command;
				aggregatePassedToCommandHandler = aggregate;
			}
		}
	}
}