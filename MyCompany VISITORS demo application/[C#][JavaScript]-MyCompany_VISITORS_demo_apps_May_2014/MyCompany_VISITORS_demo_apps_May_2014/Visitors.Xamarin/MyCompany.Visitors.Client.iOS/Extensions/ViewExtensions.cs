using MonoTouch.Foundation;

namespace MonoTouch.UIKit
{
	public static class ViewExtensions
	{
		public static UIView AddParallax(this UIView view, float min, float max)
		{
			return view;
			view.AddMotionEffect(new UIInterpolatingMotionEffect("center.x",
				UIInterpolatingMotionEffectType.TiltAlongHorizontalAxis)
			{
				MinimumRelativeValue = new NSNumber(min),
				MaximumRelativeValue = new NSNumber(max)
			});
			view.AddMotionEffect(new UIInterpolatingMotionEffect("center.y",
				UIInterpolatingMotionEffectType.TiltAlongVerticalAxis)
			{
				MinimumRelativeValue = new NSNumber(min),
				MaximumRelativeValue = new NSNumber(max)
			});
			return view;
		}
	}
}