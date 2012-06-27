namespace Common.Messaging.Dispatching
{
	using System.Linq;
	using Core;
	using Core.Dispatching;

	public class CommonBus : ICommonBus
	{
		private readonly IResolveHandlers resolveHandlers;

		public CommonBus(IResolveHandlers resolveHandlers)
		{
			this.resolveHandlers = resolveHandlers;
		}

		public void Publish(params IPublishedMessage<object>[] messages)
		{
			foreach (var publishedMessage in messages)
			{
				var publishedMessageType = publishedMessage.GetType();
				var interfaceType = publishedMessageType.GetInterface("IPublishedMessage`1");
				var messageType = interfaceType.GetGenericArguments().Single();
				var handlers = resolveHandlers.GetHandlers(messageType);

				foreach (var projectionHandler in handlers.ProjectionHandlers)
				{
					var methodInfo = projectionHandler.GetType().GetMethod("Handle");
					methodInfo.Invoke(projectionHandler, new object[] {publishedMessage});
				}

				foreach (var actionHandler in handlers.ActionHandlers)
				{
					var methodInfo = actionHandler.GetType().GetMethod("Handle");
					methodInfo.Invoke(actionHandler, new object[] { publishedMessage });
				}
			}
		}
	}
}