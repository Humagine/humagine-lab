namespace Common.Messaging.EventStore
{
	using System.Linq;
	using Core;
	using Core.Dispatching;
	using global::EventStore;
	using global::EventStore.Dispatcher;

	public class BusDispatcher : IDispatchCommits
	{
		private readonly ICommonBus bus;

		public BusDispatcher(ICommonBus bus)
		{
			this.bus = bus;
		}

		public void Dispose()
		{
		}

		public void Dispatch(Commit commit)
		{
			var messages = commit.Events.Select(e => (IPublishedMessage<object>) e.Body).ToArray();
			bus.Publish(messages);
		}
	}
}