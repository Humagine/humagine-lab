namespace Common.Messaging.Dispatching
{
	using System;
	using System.Collections.Generic;
	using Castle.MicroKernel;
	using Core.Dispatching;
	using Core.Handling;
	using Core.Handling.Action;
	using Core.Handling.Projection;

	public class WindsorContainerHandlerResolver : IResolveHandlers
	{
		private readonly IKernel kernel;

		public WindsorContainerHandlerResolver(IKernel kernel)
		{
			this.kernel = kernel;
		}

		public Handlers GetHandlers(Type messageType)
		{
			if (messageType == null)
			{
				throw new ArgumentNullException("messageType");
			}

			return new Handlers
			       	{
			       		ActionHandlers = GetActionHandlers(messageType),
			       		ProjectionHandlers = GetProjectionHandlers(messageType),
			       	};
		}

		private IEnumerable<object> GetActionHandlers(Type messageType)
		{
			return GetHandlers(typeof (IActionHandler<>), messageType);
		}

		private IEnumerable<object> GetProjectionHandlers(Type messageType)
		{
			return GetHandlers(typeof (IProjectionHandler<>), messageType);
		}

		private IEnumerable<object> GetHandlers(Type handlerGenericType, Type messageType)
		{
			for (var currentType = messageType;
			     currentType != null && typeof (object) != currentType;
			     currentType = currentType.BaseType)
			{
				var handlers = kernel.ResolveAll(handlerGenericType.MakeGenericType(currentType));
				foreach (var handler in handlers)
				{
					yield return handler;
				}
			}
		}
	}
}