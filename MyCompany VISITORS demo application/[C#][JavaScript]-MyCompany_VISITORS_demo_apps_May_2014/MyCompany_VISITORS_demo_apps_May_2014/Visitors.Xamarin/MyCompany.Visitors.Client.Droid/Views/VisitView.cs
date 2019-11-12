using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MyCompany.Visitors.Client.Droid.Extensions;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.Droid.Views
{
	public class VisitView : LinearLayout
	{
		FadeImageView image;
		TextView topLabel;
		TextView name;
		TextView position;
		TextView visitTo;
		TextView reason;
		Context context;
		public VisitView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			Initialize(context);
		}

		public VisitView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize(context);
		}

		protected void Initialize(Context context)
		{
			this.context = context;
			var inflater = LayoutInflater.FromContext(context);
			InflateView(inflater);
			SetupViews();
		}

		protected virtual void InflateView(LayoutInflater inflater)
		{
			inflater.Inflate(Resource.Layout.VisitView, this, true);
		}

		protected void SetupViews()
		{

			image = this.FindViewById<FadeImageView>(Resource.Id.imageView);
			topLabel = this.FindViewById<TextView>(Resource.Id.topLabel);
			name = this.FindViewById<TextView>(Resource.Id.Name);
			position = this.FindViewById<TextView>(Resource.Id.position);
			visitTo = this.FindViewById<TextView>(Resource.Id.visiting);
			reason = this.FindViewById<TextView>(Resource.Id.reason);
			SetupVisit();

		}

		VMVisit visit;
		public VMVisit Visit
		{
			get { return visit; }
			set
			{
				visit = value;
				SetupVisit();
			}
		}

		public void SetupVisit()
		{
			if (Visit == null || image == null)
				return;
			name.Text = Visit.VisitorName;
			position.Text = Visit.CompanyName;
			visitTo.Text = Visit.EmployeeName;
			reason.Text = Visit.Comment;
			image.SetImageBitmap(Visit.VisitorPhoto.ToImage());
			Visit.SubscribeToProperty("VisitorPhoto", () =>
				(this.context as Activity).RunOnUiThread(() => image.SetImageBitmap(Visit.VisitorPhoto.ToImage())));
		}

	}
}