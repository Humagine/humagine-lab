namespace PrototypeHumaginePageModel.Controllers
{
	using System;
	using System.Web.Mvc;
	using PrototypeHumaginePageModel.Models;

	public class RegisterController : BasePageController<PageViewModel>
    {
        public ActionResult Index()
        {
			return RedirectToAction("Form", new { registrationId = Guid.NewGuid() });
        }

		public ActionResult Form(Guid registrationId)
		{
			if (ControllerContext.HttpContext.Session["authenticated"] != null)
			{
				viewModel = new PageViewModel
				            	{
				            		Title = "Already registered"
				            	};

				return View("AlreadyRegistered", viewModel);
			}

			if (RegistrationRepository.IsRegistered(registrationId))
			{
				viewModel = new PageViewModel
				            	{
				            		Title = "Waiting activation"
				            	};

				return View("WaitingActivation", viewModel);
			}

			var registerPageViewModel = new RegisterViewModel
			{
				Title = "Register",
				Command = new RegisterCommand(),
				RegistrationId = registrationId,
			};

			viewModel = registerPageViewModel;

			return View("RegisterForm", registerPageViewModel);
		}
    }
}
