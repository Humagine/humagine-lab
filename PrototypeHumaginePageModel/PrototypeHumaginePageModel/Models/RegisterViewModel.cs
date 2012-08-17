namespace PrototypeHumaginePageModel.Models
{
	using System;

	public class RegisterViewModel : PageViewModel
	{
		public RegisterCommand Command { get; set; }
		public Guid RegistrationId { get; set; }
	}
}