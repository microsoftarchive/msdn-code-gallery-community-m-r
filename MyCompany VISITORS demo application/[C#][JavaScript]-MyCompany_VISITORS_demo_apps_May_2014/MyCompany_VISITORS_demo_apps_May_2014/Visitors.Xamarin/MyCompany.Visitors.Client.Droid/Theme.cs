using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyCompany.Visitors.Client.Droid
{
	public static class Images
	{
		public static Resources Resources;
		public static Lazy<Bitmap> DefaultImage = new Lazy<Bitmap>(()=> BitmapFactory.DecodeResource(Resources, Resource.Drawable.no_photo));
	}
}