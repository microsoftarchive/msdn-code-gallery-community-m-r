using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace MyCompany.Visitors.Client.Droid.Views
{
	public class FadeImageView : ImageView
	{
		Animation fadeInAnimation;
		Animation fadeOutAnimation;
		public FadeImageView(Context ctx)
			: base(ctx)
		{
			Initialize();
		}
		public FadeImageView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			Initialize();
		}
		public FadeImageView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize();
		}
		void Initialize()
		{
			this.SetScaleType (ImageView.ScaleType.FitCenter);
			fadeInAnimation = new AlphaAnimation(0, 1)
			{
				Duration = 500
			};
			fadeOutAnimation = new AlphaAnimation(1, 0)
			{
				Duration = 100
			};
		}
		void DoAnimation(bool really, Action changePic)
		{
			if (!really)
				changePic();
			else
			{
				EventHandler<Animation.AnimationEndEventArgs> callback = (s, e) =>
				{
					changePic();
					StartAnimation(fadeInAnimation);
				};
				fadeOutAnimation.AnimationEnd += callback;
				StartAnimation(fadeOutAnimation);
			}
		}
		public void SetImageBitmap(Bitmap bm, bool animate)
		{
			DoAnimation(animate, () => SetImageBitmap(bm));
		}
		public void SetImageDrawable(Drawable drawable, bool animate)
		{
			DoAnimation(animate, () => SetImageDrawable(drawable));
		}
		public void SetImageResource(int resId, bool animate)
		{
			DoAnimation(animate, () => SetImageResource(resId));
		}
		public void SetImageURI(Android.Net.Uri uri, bool animate)
		{
			DoAnimation(animate, () => SetImageURI(uri));
		}
	}
}