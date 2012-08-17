namespace PrototypeHumaginePageModel.Controllers.Api
{
	using System.Web.Mvc;

	public class LogoutController : Controller
	{
		public ActionResult Index()
		{
			ControllerContext.HttpContext.Session.Clear();

			var url = ControllerContext.HttpContext.Request.UrlReferrer.ToString();
			return Redirect(url);
		}
	}
}