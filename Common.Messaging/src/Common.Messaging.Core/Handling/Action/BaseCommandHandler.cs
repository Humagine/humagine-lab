namespace Common.Messaging.Core.Handling.Action
{
	using CommonDomain;
	using CommonDomain.Persistence;

	public abstract class BaseCommandHandler<TCommand, TAggregate> : IActionHandler<TCommand>
		where TCommand : class, ICommand
		where TAggregate : class, IAggregate
	{
		private readonly IRepository repository;

		protected BaseCommandHandler(IRepository repository)
		{
			this.repository = repository;
		}

		public virtual void Handle(IPublishedMessage<TCommand> message)
		{
			var command = message.Payload;
			var aggregate = repository.GetById<TAggregate>(command.AggregateId);
			HandleCommand(aggregate, command);
		}

		protected abstract void HandleCommand(TAggregate aggregate, TCommand command);
	}
}