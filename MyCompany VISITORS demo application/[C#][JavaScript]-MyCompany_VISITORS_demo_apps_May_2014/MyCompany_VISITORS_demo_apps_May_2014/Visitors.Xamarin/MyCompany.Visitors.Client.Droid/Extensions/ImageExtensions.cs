using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyCompany.Visitors.Client.Droid.Extensions
{
	static class ImageExtensions
	{
		public static Bitmap ToImage(this VisitorPicture picture)
		{
			if (picture == null || picture.Content == null || picture.Content.Length == 0)
				return Images.DefaultImage.Value;
			Bitmap bitmap = BitmapFactory.DecodeByteArray(picture.Content, 0, picture.Content.Length);
			return bitmap;
		}
	}
}