using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client;


namespace MyCompany.Visitors.Client
{
	class SmallEmployeeView : UIView
	{
		public UIImageView image;
		UILabel name;
		VMEmployee visitor;

		public VMEmployee Employee
		{
			get { return visitor; }
			set
			{
				visitor = value;
				name.Text = visitor.FullName;
				image.Image = visitor.EmployeePhoto.ToImage() ?? Theme.UserImageDefaultLight.Value;
				visitor.SubscribeToProperty("EmployeePhoto", updateImage);
			}
		}
		void updateImage()
		{
			Transition(image, .3, UIViewAnimationOptions.TransitionFlipFromLeft,
				() => image.Image = visitor.EmployeePhoto.ToImage() ?? Theme.UserImageDefaultLight.Value, null);
		}

		public SmallEmployeeView()
		{
			image = new UIImageView
			{
				Layer = { MasksToBounds = true}
			};
			AddSubview(image);

			name = new UILabel{TextAlignment = UITextAlignment.Center, AdjustsFontSizeToFitWidth = true};
			AddSubview(name);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			var width = Bounds.Width;
			image.Frame = new RectangleF(0, 0, width, width);
			image.Layer.CornerRadius = width / 2;

			var bounds = Bounds;
			bounds.Y = image.Frame.Bottom;
			bounds.Height -= bounds.Y;
			name.Frame = bounds;
		}
	}
}