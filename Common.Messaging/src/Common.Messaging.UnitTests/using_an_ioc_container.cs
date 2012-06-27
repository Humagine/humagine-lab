namespace Common.Messaging.UnitTests
{
	using Castle.MicroKernel;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using Machine.Specifications;

	[Subject(typeof (WindsorContainer))]
	public class using_an_ioc_container
	{
		protected static WindsorContainer windsorContainer;

		private Establish context =
			() =>
				{
					windsorContainer = new WindsorContainer();
					windsorContainer.Register(Component.For<IKernel>().Instance(windsorContainer.Kernel));
				};

		private It will_have_a_kernel_registered =
			() => windsorContainer.Resolve<IKernel>().ShouldNotBeNull();
	}
}