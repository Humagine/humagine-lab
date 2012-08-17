namespace PrototypeHumaginePageModel.Models
{
	using System;
	using System.Collections.Generic;

	public class RegistrationRepository
	{
		private static IList<Guid> registrationIds = new List<Guid>(); 

		public static void Register(Guid registrationId)
		{
			if (registrationIds.Contains(registrationId))
			{
				return;
			}

			registrationIds.Add(registrationId);
		}

		public static bool IsRegistered(Guid registrationId)
		{
			return registrationIds.Contains(registrationId);
		}
	}
}