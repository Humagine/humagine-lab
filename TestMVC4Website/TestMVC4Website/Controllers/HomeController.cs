namespace TestMVC4Website.Controllers
{
	using System;
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
				var imageBytes = client.DownloadData("https://www.humagine.com/img/logo-humagine-big.png");

				using (var image = Image.FromStream(new MemoryStream(imageBytes)))
				{
					using (var crop = imageCropper.Crop(image, imageData.X, imageData.Y, imageData.Width, imageData.Height))
					{
						var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "patate.png");
						crop.Save(path, ImageFormat.Png);
						return File(path, "image/png");
					}
				}
			}
		}
	}

	public class ImageData
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }
		public float Ratio { get; set; }
	}
}