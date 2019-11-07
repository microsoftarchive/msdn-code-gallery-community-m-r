using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace MyCompany.Visitors.Client.iOS.Views
{
	class BadgeView : UILabel
	{
		const float height = 15f;
		int badgeNumber;

		SizeF numberSize;

		public BadgeView()
		{
			Font = UIFont.BoldSystemFontOfSize(12);
			BackgroundColor = UIColor.White;
			TextColor = UIColor.Gray;
			UserInteractionEnabled = false;
			Layer.CornerRadius = height/2;
			Layer.MasksToBounds = true;
			TextAlignment = UITextAlignment.Center;
		}

		public int BadgeNumber
		{
			get { return badgeNumber; }
			set { Text = (badgeNumber = value).ToString(); }
		}

		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				CalculateSize();
				Alpha = (string.IsNullOrEmpty(value) || value == "0") ? 0 : 1;
				SetNeedsDisplay();
			}
		}

		void CalculateSize()
		{
			numberSize = StringSize(Text, Font);
			numberSize.Width += 10;
			Frame = new RectangleF(Frame.Location, new SizeF(Math.Max(numberSize.Width, height), height));
		}
	}
}