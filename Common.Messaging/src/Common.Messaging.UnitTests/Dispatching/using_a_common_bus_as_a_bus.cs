namespace Common.Messaging.UnitTests.Dispatching
{
	using Castle.MicroKernel.Registration;
	using Core.Dispatching;
	using Machine.Specifications;
	using Messaging.Dispatching;

	[Subject(typeof (CommonBus))]
	public class using_a_common_bus_as_a_bus : using_an_ioc_container
	{
		protected static ICommonBus bus;

		private Establish context =
			() =>
				{
					windsorContainer.Register(Component.For<ICommonBus>().ImplementedBy<CommonBus>());
					windsorContainer.Register(Component.For<IResolveHandlers>().ImplementedBy<WindsorContainerHandlerResolver>());
					bus = windsorContainer.Resolve<ICommonBus>();
				};

		private It will_not_be_null =
			() => bus.ShouldNotBeNull();

		private It will_be_a_common_bus =
			() => bus.ShouldBeOfType<CommonBus>();
	}
}