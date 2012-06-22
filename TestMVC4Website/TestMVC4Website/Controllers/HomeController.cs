namespace TestMVC4Website.Controllers
{
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Net;
	using System.Web.Mvc;
	using Services;

	public class HomeController : Controller
	{
		private readonly IImageCropper imageCropper = new ImageCropper();

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CropImage(ImageData imageData)
		{
			using (var client = new WebClient())
			{
				//var imageBytes = client.DownloadData("https://www.humagine.com/img/logo-humagine-big.png");
				var imageBytes = client.DownloadData("http://freeimagesarchive.com/data/media/8/5_images.jpg");

				using (var image = Image.FromStream(new MemoryStream(imageBytes)))
				{
					using (var crop = imageCropper.Crop(image, imageData.X, imageData.Y, imageData.Width, imageData.Height))
					{
						crop.Save("patate.png", ImageFormat.Png);
					}
				}
			}

			return RedirectToAction("Index");
		}
	}

	public class ImageData
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }
	}
}