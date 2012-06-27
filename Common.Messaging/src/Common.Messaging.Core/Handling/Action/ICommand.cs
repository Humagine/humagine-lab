namespace Common.Messaging.Core.Handling.Action
{
	using System;

	public interface ICommand
	{
		Guid AggregateId { get; }
	}
}