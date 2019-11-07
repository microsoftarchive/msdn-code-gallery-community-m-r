using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace MyCompany.Visitors.Client.iOS.ViewControllers
{
	class PopupViewController : UIViewController
	{
		public Action Dismiss { get; set; }


		public void Show(UIViewController parentView)
		{

			parentView.View.AddSubview(View);
			parentView.AddChildViewController(this);
			view.Show();
		}

		public async Task Hide()
		{
			await view.Hide();
			View.RemoveFromSuperview();
		}

		public UIView ContentView
		{
			get { return view.ContentView; }
			set
			{
				view.ContentView = value;
			}
		}

		PopupView view;
		public override void LoadView()
		{
			View = view = new PopupView();

		}

		class PopupView : UIView
		{
			public PopupView()
			{
				BackgroundColor = UIColor.Clear;
				BlurryView = new BrightlyBlurredUIView{TintColor = UIColor.FromRGBA(51,51,51,153)};
				this.AddSubview(BlurryView);
			}

			UIView contentView;
			public UIView ContentView
			{
				get { return contentView; }
				set
				{
					if (contentView != null)
						contentView.RemoveFromSuperview();
					contentView = value;
					AddSubview(contentView);
					ContentView.Center = BlurryView.Center;
				}
			}
			public UIView BlurryView { get; set; }
			public override void LayoutSubviews()
			{
				base.LayoutSubviews();
				BlurryView.Frame = Bounds;
				if (ContentView != null)
					ContentView.Center = BlurryView.Center;
			}

			public async Task Show()
			{
				BlurryView.Frame = Bounds;
				BlurryView.Alpha = 0;
				if (ContentView != null)
				{
					ContentView.Center = BlurryView.Center;
					var frame = ContentView.Frame;
					frame.Y = Bounds.Bottom;
					ContentView.Frame = frame;
				}
				await UIView.AnimateAsync(.3, () =>
				{
					BlurryView.Alpha = 1f;
					if (ContentView != null)
						ContentView.Center = BlurryView.Center;
				});
				contentView.LayoutSubviews();
			}

			public async Task Hide()
			{
				await UIView.AnimateAsync(.3, () =>
				{
					BlurryView.Alpha = 0;
					if (ContentView != null)
					{
						ContentView.Center = BlurryView.Center;
						var frame = ContentView.Frame;
						frame.Y = Bounds.Bottom;
						ContentView.Frame = frame;
					}
				});
			}
		}
	}
}