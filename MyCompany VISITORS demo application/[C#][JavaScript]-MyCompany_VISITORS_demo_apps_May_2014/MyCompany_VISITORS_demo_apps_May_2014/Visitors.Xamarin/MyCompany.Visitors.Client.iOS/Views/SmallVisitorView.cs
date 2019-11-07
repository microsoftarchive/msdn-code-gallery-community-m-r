using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS.Views;


namespace MyCompany.Visitors.Client
{
	class SmallVisitorView : UIView
	{
		public UIImageView image;
		UILabel name;
		VMVisitor visitor;

		public VMVisitor Visitor
		{
			get { return visitor; }
			set
			{
				visitor = value;
				name.Text = visitor.FullName;
				image.Image = visitor.VisitorPhoto.ToImage() ?? Theme.UserImageDefaultLight.Value;
				visitor.SubscribeToProperty("VisitorPhoto", updateImage);
			}
		}
		void updateImage()
		{
			Transition(image, .3, UIViewAnimationOptions.TransitionFlipFromLeft,
				() => image.Image = visitor.VisitorPhoto.ToImage() ?? Theme.UserImageDefaultLight.Value, null);
		}

		public SmallVisitorView()
		{
			image = new UIImageView
			{
				Layer = { MasksToBounds = true},
				ContentMode =  UIViewContentMode.ScaleAspectFill,
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