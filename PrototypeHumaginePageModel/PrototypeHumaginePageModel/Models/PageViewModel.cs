namespace PrototypeHumaginePageModel.Models
{
	public class PageViewModel : ViewModel
	{
		public PageViewModel()
		{
			Title = "";
		}

		public string Title { get; set; }

		public bool IsAuthenticated { get; set; }
	}
}