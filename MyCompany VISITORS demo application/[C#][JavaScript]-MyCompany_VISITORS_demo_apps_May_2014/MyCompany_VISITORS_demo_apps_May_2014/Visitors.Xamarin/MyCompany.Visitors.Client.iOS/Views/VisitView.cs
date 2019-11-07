using System;
using System.Drawing;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.iOS.Views
{
	class VisitView : UIControl
	{
		protected const float imageWidth = 150f;
		protected const float topHeight = 44f;
		protected const float edgePadding = 20f;
		protected const float smallPadding = edgePadding/2;
		const string font1 = "HelveticaNeue";
		const string font2 = "HelveticaNeue-Light";
		static readonly Lazy<UIImage> darkBgImage = new Lazy<UIImage>(() => UIImage.FromBundle("bar-left-back"));
		static readonly Lazy<UIImage> lightBgImage = new Lazy<UIImage>(() => UIImage.FromBundle("bar-right-back"));
		protected UIView bottomLine;
		protected UILabel company;
		protected UILabel employee;
		protected UIImageView image;
		bool isDarkThemed;
		protected UILabel name;
		protected UILabel reason;
		protected UILabel reasonLabel;
		protected BadgeView timeBadge;
		protected UILabel timeDescription;
		protected BadgeView topBadge;
		protected UILabel visitLabel;
		VMVisit vmVisit;

		public VisitView()
		{
			image = new UIImageView(new RectangleF(0, 0, imageWidth, imageWidth))
			{
				ContentMode = UIViewContentMode.ScaleAspectFill,
				Image = DefaultImage,
				BackgroundColor = UIColor.Clear,
				Layer = { MasksToBounds = true, CornerRadius = imageWidth/2}
			};
			AddSubview(image);

			timeDescription = new UILabel {Text = "Next Visit".ToUpper(), Font = UIFont.FromName(font1, 13)};
			timeDescription.SizeToFit();
			AddSubview(timeDescription);

			topBadge = new BadgeView {Text = "10", Font = UIFont.FromName(font1, 8)};
			AddSubview(topBadge);

			timeBadge = new BadgeView {Text = "10", Font = topBadge.Font};
			AddSubview(timeBadge);

			name = new UILabel {Text = "Name", Font = UIFont.FromName(font2, 30), AdjustsFontSizeToFitWidth = true};
			name.SizeToFit();
			AddSubview(name);

			company = new UILabel {Text = "Company", Font = UIFont.FromName(font1, 12)};
			company.SizeToFit();
			AddSubview(company);

			visitLabel = new UILabel {Text = "Visit To:".ToUpper(), Font = UIFont.FromName(font1, 8)};
			visitLabel.SizeToFit();
			AddSubview(visitLabel);

			reasonLabel = new UILabel {Text = "Reason:".ToUpper(), Font = UIFont.FromName(font1, 8)};
			reasonLabel.SizeToFit();
			AddSubview(reasonLabel);

			employee = new UILabel {Text = "Employee", Font = UIFont.FromName(font1, 12)};
			employee.SizeToFit();
			AddSubview(employee);

			reason = new UILabel {Text = "Reason", Font = UIFont.FromName(font1, 12)};
			reason.SizeToFit();
			AddSubview(reason);

			bottomLine = new UIView(new RectangleF(0, 0, 1, 1));
			AddSubview(bottomLine);
			clearData();
		}

		public UIImage DefaultImage
		{
			get { return isDarkThemed ? Theme.UserImageDefaultDark.Value : Theme.UserImageDefaultLight.Value; }
		}

		public VMVisit Visit
		{
			get { return vmVisit; }
			set
			{
				vmVisit = value;
				if (value == null)
				{
					clearData();
					return;
				}
				updateVisit();
				vmVisit.SubscribeToProperty("VisitorPhoto", () => BeginInvokeOnMainThread(updateImage));
				vmVisit.SubscribeToProperty("Visitor", () => BeginInvokeOnMainThread(updateVisit));
				Transition(this, .3, UIViewAnimationOptions.TransitionCrossDissolve, updateVisit, null);
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			RectangleF frame = timeDescription.Frame;
			frame.X = edgePadding;
			frame.Y = (topHeight - frame.Height)/2;
			timeDescription.Frame = frame;

			float x = frame.Right + smallPadding;
			frame = topBadge.Frame;
			frame.Y = (topHeight - frame.Height)/2;
			frame.X = x;
			topBadge.Frame = frame;

			float bottomHeight = Bounds.Height - topHeight;

			frame = image.Frame;
			frame.X = edgePadding;
			frame.Y = ((bottomHeight - frame.Height)/2) + topHeight;
			image.Frame = frame;

			x = frame.Right + edgePadding;
			float bottomWidth = Bounds.Width - (x + edgePadding);
			bottomLine.Frame = new RectangleF(x, topHeight + (bottomHeight/2), bottomWidth, .5f);

			frame = company.Frame;
			frame.Y = bottomLine.Frame.Top - smallPadding - frame.Height;
			frame.X = x;
			frame.Width = bottomWidth;
			company.Frame = frame;

			frame.Height = name.Frame.Height;
			frame.Y -= frame.Height;
			name.Frame = frame;

			float y = frame.Top;
			frame = timeBadge.Frame;
			frame.Y = y - frame.Height;
			frame.X = x;
			timeBadge.Frame = frame;

			frame = visitLabel.Frame;
			frame.Y = bottomLine.Frame.Bottom + smallPadding;
			frame.X = x;
			visitLabel.Frame = frame;

			RectangleF bounds = employee.Frame;
			bounds.X = frame.Right + 5;
			bounds.Y = frame.Bottom - bounds.Height;
			employee.Frame = bounds;

			frame = reasonLabel.Frame;
			frame.Y = visitLabel.Frame.Bottom + smallPadding;
			frame.X = x;
			reasonLabel.Frame = frame;

			bounds = reason.Frame;
			bounds.X = frame.Right + 5;
			bounds.Y = frame.Bottom - bounds.Height;
			reason.Frame = bounds;
		}

		public void SetDarkTheme()
		{
			isDarkThemed = true;
			BackgroundColor = UIColor.FromPatternImage(darkBgImage.Value);

			name.TextColor =
				company.TextColor =
					employee.TextColor =
						reason.TextColor = reasonLabel.TextColor = visitLabel.TextColor = timeDescription.TextColor = UIColor.White;

			reasonLabel.Alpha = visitLabel.Alpha = .5f;

			topBadge.TextColor = UIColor.FromRGB(196, 48, 81);
			topBadge.BackgroundColor = UIColor.White;

			bottomLine.BackgroundColor = UIColor.White;
			bottomLine.Alpha = .5f;
			image.Image = DefaultImage;
		}

		public void SetLightTheme()
		{
			isDarkThemed = false;
			BackgroundColor = UIColor.FromPatternImage(lightBgImage.Value);

			name.TextColor =
				company.TextColor = employee.TextColor = reason.TextColor = timeDescription.TextColor = UIColor.Black;

			timeBadge.BackgroundColor = UIColor.Gray;
			timeBadge.TextColor = UIColor.White;

			topBadge.TextColor = UIColor.White;

			reasonLabel.TextColor = visitLabel.TextColor = topBadge.BackgroundColor = UIColor.FromRGB(196, 48, 81);

			bottomLine.BackgroundColor = UIColor.Gray;
			bottomLine.Alpha = .5f;

			image.Image = DefaultImage;
		}


		protected virtual void updateVisit()
		{
			name.Text = Visit.VisitorName;
			name.SizeToFit();
			company.Text = Visit.CompanyName;
			company.SizeToFit();
			employee.Text = Visit.EmployeeName;
			employee.SizeToFit();
			reason.Text = Visit.Comment;
			reason.SizeToFit();
			timeBadge.Text = string.Format("{0} {1}", Visit.VisitFormattedDate, Visit.VisitFormattedTime);
			image.Image = Visit.VisitorPhoto.ToImage() ?? DefaultImage;
		}

		protected virtual void clearData()
		{
			topBadge.Text = "";
			name.Text = "";
			company.Text = "";
			employee.Text = "";
			reason.Text = "";
			timeBadge.Text = "";
			image.Image = DefaultImage;
		}

		void updateImage()
		{
			Transition(image, .3, UIViewAnimationOptions.TransitionFlipFromLeft,
				() => image.Image = Visit.VisitorPhoto.ToImage() ?? DefaultImage, null);
		}
	}

	class NextVisitView : VisitView
	{
		public NextVisitView()
		{
			SetDarkTheme();
		}

		protected override void updateVisit()
		{
			base.updateVisit();
			topBadge.Text = string.Format("{0} {1}", Visit.VisitFormattedDate, Visit.VisitFormattedTime);
		}
	}

	class OtherVisitView : VisitView
	{
		public OtherVisitView()
		{
			timeDescription.Text = "Other Visits".ToUpper();
			timeDescription.SizeToFit();
			SetLightTheme();
		}

		public int VisitCount
		{
			set { topBadge.Text = value.ToString(); }
		}
	}

	class TallVisitView : VisitView
	{
		const float groupPadding = 10f;
		const float padding = 5f;

		public TallVisitView()
		{
			name.LineBreakMode = UILineBreakMode.WordWrap;
			name.Lines = 2;
			SetLightTheme();
			BackgroundColor = UIColor.Clear;
			image.Layer.CornerRadius = 45;
			timeDescription.RemoveFromSuperview();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			bottomLine.Frame = RectangleF.Empty;

			image.Frame = new RectangleF(edgePadding, edgePadding, 90, 90);

			RectangleF frame = Bounds;
			frame.X = edgePadding;
			frame.Width -= edgePadding;


			float width = frame.Width;
			frame.Y = image.Frame.Bottom + edgePadding;
			frame.Height = timeBadge.Frame.Height;
			frame.Width = timeBadge.Frame.Width;
			timeBadge.Frame = frame;
			frame.Width = width;

			frame.Y = frame.Bottom;
			frame.Height = name.Frame.Height;
			name.Frame = frame;


			frame.Y = frame.Bottom;
			frame.Height = company.Frame.Height;
			company.Frame = frame;

			frame.Y = frame.Bottom + edgePadding;
			frame.Height = visitLabel.Frame.Height;
			frame.Width = visitLabel.Frame.Width;
			visitLabel.Frame = frame;

			RectangleF bounds = employee.Frame;
			bounds.X = frame.Right + 5;
			bounds.Y = frame.Bottom - bounds.Height;
			employee.Frame = bounds;


			frame.Y = frame.Bottom + smallPadding;
			frame.Height = reasonLabel.Frame.Height;
			frame.Width = reasonLabel.Frame.Width;
			reasonLabel.Frame = frame;

			bounds = reason.Frame;
			bounds.X = frame.Right + 5;
			bounds.Y = frame.Bottom - bounds.Height;
			reason.Frame = bounds;
		}
	}
}