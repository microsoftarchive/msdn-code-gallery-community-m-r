using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.UIKit;


namespace MyCompany.Visitors.Client
{
	public static class Theme
	{
		public static readonly Lazy<UIImage> UserImageDefaultDark = new Lazy<UIImage>(() => UIImage.FromBundle("avatar-a"));
		public static readonly Lazy<UIImage> UserImageDefaultLight = new Lazy<UIImage>(() => UIImage.FromBundle("avatar-b"));

		public static readonly UIColor AccentColor = UIColor.FromRGB(196, 48, 81);
	}
}