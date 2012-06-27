namespace Common.Messaging.Core.Handling
{
	using System.Collections.Generic;

	public class Handlers
	{
		public IEnumerable<object> ActionHandlers { get; set; }
		public IEnumerable<object> ProjectionHandlers { get; set; }
	}
}