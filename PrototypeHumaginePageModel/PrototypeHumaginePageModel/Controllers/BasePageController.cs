namespace PrototypeHumaginePageModel.Controllers
{
	using System.Web.Mvc;
	using PrototypeHumaginePageModel.Models;

	public abstract class BasePageController<T> : Controller where T : PageViewModel, new()
	{
		protected T viewModel;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			viewModel = new T();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);

			viewModel.IsAuthenticated = filterContext.HttpContext.Session["authenticated"] == null ? false : true;
		}
	}
}