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
using MyCompany.Visitors.Client.Droid.Views;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.Droid.Fragments
{
	public class VisitViewFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            RetainInstance = true;
		    // Create your fragment here
		}
		protected FadeImageView image;
		protected TextView topLabel;
		protected TextView topValue;
		protected TextView name;
		protected TextView position;
		protected TextView visitTo;
		protected TextView reason;
		private VMVisit visit;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.VisitView, null);
			image = view.FindViewById<FadeImageView>(Resource.Id.imageView);
			topLabel = view.FindViewById<TextView>(Resource.Id.topLabel);
			topValue = view.FindViewById<TextView>(Resource.Id.topValue);
			topLabel.Text = TopLabel;
			name = view.FindViewById<TextView>(Resource.Id.Name);
			position = view.FindViewById<TextView>(Resource.Id.position);
			visitTo = view.FindViewById<TextView>(Resource.Id.visiting);
			reason = view.FindViewById<TextView>(Resource.Id.reason);
			SetupVisit();
			return view;
		}

		public string TopLabel { get; set; }

		public VMVisit Visit
		{
			get { return visit; }
			set
			{
				visit = value;
				SetupVisit();
			}
		}

		public virtual void SetupVisit()
		{
			if (Visit == null || image == null)
				return;
			name.Text = Visit.VisitorName;
			position.Text = Visit.CompanyName;
			visitTo.Text = Visit.EmployeeName;
			reason.Text = Visit.Comment;
			image.SetImageBitmap(Visit.VisitorPhoto.ToImage() );
			topValue.Text = Visit.VisitFormattedDate;
			Visit.SubscribeToProperty("VisitorPhoto", () =>
				this.Activity.RunOnUiThread(() => image.SetImageBitmap(Visit.VisitorPhoto.ToImage())));
		}

		public void updateImage()
		{
		}
	}
}