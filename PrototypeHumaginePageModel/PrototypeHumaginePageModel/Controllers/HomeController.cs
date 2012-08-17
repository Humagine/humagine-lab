namespace PrototypeHumaginePageModel.Controllers
{
	using System.Web.Mvc;
	using PrototypeHumaginePageModel.Models;

	public class HomeController : BasePageController<PageModel>
    {
        public ActionResult Index()
        {
			viewModel = new PageModel();
            return View(viewModel);
        }
    }
}
