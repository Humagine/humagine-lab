namespace TestMVC4Website.Services
{
	using System.Drawing;

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
				var clone = bitmap.Clone(new RectangleF(x, y, width, height), bitmap.PixelFormat);
				return clone;
			}
		}
	}
}