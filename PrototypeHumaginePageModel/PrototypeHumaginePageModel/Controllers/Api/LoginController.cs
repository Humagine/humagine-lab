namespace PrototypeHumaginePageModel.Controllers.Api
{
	using System.Web.Mvc;

	public class LoginController : Controller
    {
        public ActionResult Index()
        {
			ControllerContext.HttpContext.Session.Add("authenticated", true);

			var url = ControllerContext.HttpContext.Request.UrlReferrer.ToString();
			return Redirect(url);
        }
    }
}
