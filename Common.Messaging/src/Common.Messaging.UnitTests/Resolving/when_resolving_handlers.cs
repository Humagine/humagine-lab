namespace Common.Messaging.UnitTests.Resolving
{
	using System.Linq;
	using Castle.MicroKernel.Registration;
	using Core;
	using Core.Dispatching;
	using Core.Handling;
	using Core.Handling.Action;
	using Core.Handling.Projection;
	using Machine.Specifications;
	using Messaging.Dispatching;

	[Subject(typeof (WindsorContainerHandlerResolver))]
	public class when_resolving_handlers : using_an_ioc_container
	{
		private static Handlers handlers;

		private Establish context =
			() =>
				{
					windsorContainer.Register(Component.For<IActionHandler<TestData>>().ImplementedBy<TestActionHandler1>());
					windsorContainer.Register(Component.For<IActionHandler<TestData>>().ImplementedBy<TestActionHandler2>());
					windsorContainer.Register(Component.For<IProjectionHandler<TestData>>().ImplementedBy<TestProjectionHandler1>());
					windsorContainer.Register(Component.For<IProjectionHandler<TestData>>().ImplementedBy<TestProjectionHandler2>());

					windsorContainer.Register(Component.For<IResolveHandlers>().ImplementedBy<WindsorContainerHandlerResolver>());
				};

		private Because of =
			() => handlers = windsorContainer.Resolve<IResolveHandlers>().GetHandlers(typeof (TestData));

		private It will_return_a_handlers_object =
			() => handlers.ShouldNotBeNull();

		private It will_have_2_action_handlers =
			() => handlers.ActionHandlers.Count().ShouldEqual(2);

		private It will_have_one_action_handler_of_the_first_type =
			() => handlers.ActionHandlers.ShouldContain(h => h is TestActionHandler1);

		private It will_have_one_action_handler_of_the_second_type =
			() => handlers.ActionHandlers.ShouldContain(h => h is TestActionHandler2);

		private It will_have_2_projection_handlers =
			() => handlers.ProjectionHandlers.Count().ShouldEqual(2);

		private It will_have_one_projection_handler_of_the_first_type =
			() => handlers.ProjectionHandlers.ShouldContain(h => h is TestProjectionHandler1);

		private It will_have_one_projection_handler_of_the_second_type =
			() => handlers.ProjectionHandlers.ShouldContain(h => h is TestProjectionHandler2);


		private class TestData
		{
		}

		private class TestActionHandler1 : IActionHandler<TestData>
		{
			public void Handle(IPublishedMessage<TestData> message)
			{
			}
		}

		private class TestActionHandler2 : IActionHandler<TestData>
		{
			public void Handle(IPublishedMessage<TestData> message)
			{
			}
		}

		private class TestProjectionHandler1 : IProjectionHandler<TestData>
		{
			public void Handle(IPublishedMessage<TestData> message)
			{
			}
		}

		private class TestProjectionHandler2 : IProjectionHandler<TestData>
		{
			public void Handle(IPublishedMessage<TestData> message)
			{
			}
		}
	}
}