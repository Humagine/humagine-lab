namespace TestMVC4Website.Services
{
	using System.Drawing;
	using System.Drawing.Imaging;

	public interface IImageCropper
	{
		Image Crop(Image image, float x, float y, float width, float height);
	}

	public class ImageCropper : IImageCropper
	{
		public Image Crop(Image image, float x, float y, float width, float height)
		{
			using (var bitmap = new Bitmap(image))
			{
				using(var copy = Copy(bitmap))
				{
					using(var clone = Crop(x, y, width, height, copy, bitmap.PixelFormat))
					{
						return Resize(clone);
					}
				}
			}
		}

		private static Bitmap Copy(Bitmap srcBitmap)
		{
			var bmp = new Bitmap(500, 500);
			bmp.MakeTransparent();

			using(var g = Graphics.FromImage(bmp))
			{
				g.DrawImage(srcBitmap, (500 - srcBitmap.Width) / 2, (500 - srcBitmap.Height) / 2);
			}

			return bmp;
		}

		private static Bitmap Crop(float x, float y, float width, float height, Bitmap copy, PixelFormat pixelFormat)
		{
			var clone = copy.Clone(new RectangleF(x, y, width, height), pixelFormat);
			return clone;
		}

		private static Image Resize(Image img)
		{
			return new Bitmap(img, new Size(150, 100));
		}
	}
}