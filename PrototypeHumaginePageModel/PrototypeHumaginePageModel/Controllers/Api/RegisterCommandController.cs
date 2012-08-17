namespace PrototypeHumaginePageModel.Controllers.Api
{
	using System;
	using System.Web.Mvc;
	using PrototypeHumaginePageModel.Models;

	public class RegisterCommandController : Controller
    {
        [HttpPost]
        public ActionResult PostCommand(Guid registrationId)
        {
			RegistrationRepository.Register(registrationId);

			var url = ControllerContext.HttpContext.Request.UrlReferrer.ToString();
			return Redirect(url);
        }
    }
}
