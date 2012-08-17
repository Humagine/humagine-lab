namespace PrototypeHumaginePageModel.Models
{
	public class PageModel
	{
		public PageModel()
		{
			Title = "";
		}

		public string Title { get; set; }
		public bool IsAuthenticated { get; set; }
	}
}