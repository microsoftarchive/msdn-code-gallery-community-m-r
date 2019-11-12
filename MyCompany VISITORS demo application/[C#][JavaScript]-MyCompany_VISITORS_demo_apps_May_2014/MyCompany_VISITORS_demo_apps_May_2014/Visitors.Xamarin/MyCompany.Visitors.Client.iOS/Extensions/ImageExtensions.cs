using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using MonoTouch.Foundation;
using MyCompany.Visitors.Client;

namespace MonoTouch.UIKit
{
	static class ImageExtensions
	{
		static CIContext context;


		public static UIImage ResizeImage(this UIImage theImage, float width, float height, bool keepRatio = true)
		{
			if (keepRatio)
			{
				var ratio = theImage.Size.Height / theImage.Size.Width;
				if (height > width)
					width = height * ratio;
				else
					height = width * ratio;
			}

			UIGraphics.BeginImageContext(new SizeF(width, height));
			var c = UIGraphics.GetCurrentContext();

			theImage.Draw(new RectangleF(0, 0, width, height));
			var converted = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return converted;
		}

		public static UIImage ToImage(this VisitorPicture picture)
		{
			return picture == null || picture.Content == null ? null : picture.Content.ToImage();
		}
		public static UIImage ToImage(this EmployeePicture picture)
		{
			return picture == null || picture.Content == null ? null : picture.Content.ToImage();
		}


		public static UIImage ToImage(this byte[] bytes)
		{
			if (bytes == null)
				return null;

			using (var data = NSData.FromArray(bytes))
			{
				return UIImage.LoadFromData(data);
			}
		}

		public static Task<UIImage> BlurAsync(this UIImage image, float radius)
		{
			return Task.Factory.StartNew(() => image.Blur(radius));
		}

		public static UIImage Blur(this UIImage image, float radius)
		{
			try
			{
				var imageToBlur = CIImage.FromCGImage(image.CGImage);

				var transform = new CIAffineClamp();
				transform.Transform = CGAffineTransform.MakeIdentity();
				transform.Image = imageToBlur;


				var gaussianBlurFilter = new CIGaussianBlur();

				gaussianBlurFilter.Image = transform.OutputImage;
				gaussianBlurFilter.Radius = radius;
				if (context == null)
					context = CIContext.FromOptions(null);

				var resultImage = gaussianBlurFilter.OutputImage;

				var finalImage = UIImage.FromImage(
					context.CreateCGImage(resultImage, new RectangleF(PointF.Empty, image.Size)), 1, UIImageOrientation.Up);
				return finalImage;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return image;
			}
		}
	}
}