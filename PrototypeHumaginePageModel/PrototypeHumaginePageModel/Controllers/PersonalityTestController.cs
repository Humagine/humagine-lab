namespace PrototypeHumaginePageModel.Controllers
{
	using System.Web.Mvc;
	using PrototypeHumaginePageModel.Models;

	public class PersonalityTestController : BasePageController<PageViewModel>
	{
		public ActionResult Index()
		{
			viewModel = new PageViewModel();
			return View(viewModel);
		}
    }
}
