using System;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;

namespace MonoTouch.UIKit
{
	public class BrightlyBlurredUIView: UIView
	{

		public BrightlyBlurredUIView()
		{
			this.Layer.MasksToBounds = true;
			this.BackgroundColor = UIColor.Clear;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var bounds = Bounds;
		}

		public override UIColor TintColor {
			get {
				return base.TintColor;
			}
			set {
				base.TintColor = BackgroundColor = value;
			}
		}
	
	}
}
