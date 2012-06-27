namespace Common.Messaging.Core.Dispatching
{
	using System;
	using Handling;

	public interface IResolveHandlers
	{
		Handlers GetHandlers(Type messageType);
	}
}