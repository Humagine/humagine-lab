namespace PrototypeHumaginePageModel.Controllers.Api
{
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using PrototypeHumaginePageModel.Models;

	public class WebApiCommandController : ApiController
    {
		public HttpResponseMessage Post(RegisterCommand registrationCommand)
		{
			var referrer = ControllerContext.Request.Headers.Referrer;

			var responseMessage = new HttpResponseMessage(HttpStatusCode.Redirect);
			responseMessage.Headers.Location = referrer;
			responseMessage.Content = new StringContent("Content after command sent: " + registrationCommand.RegistrationId);

			RegistrationRepository.Register(registrationCommand.RegistrationId);

			return responseMessage;
        }
    }
}
