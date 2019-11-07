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
using MyCompany.Visitors.Client.ViewModels;

namespace MyCompany.Visitors.Client.Droid.Fragments
{
	public class MainFragment : Fragment
	{
		private VMMainPage model;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		    RetainInstance = true;
		}

		public VMMainPage Model
		{
			get { return model; }
			set
			{
				model = value;
				model.SubscribeToProperty("NextVisit", () =>
				{
					if (nextView == null)
						return;
					
					this.Activity.RunOnUiThread(() =>
						nextView.Visit = model.NextVisit);
				} );
			model.SubscribeToProperty("OtherVisits", () =>
			{
				if (otherViewFragment == null)
					return;
				this.Activity.RunOnUiThread(() =>
					otherViewFragment.Visits = model.OtherVisits);
			});
			model.SubscribeToProperty("TodayVisits", () =>
			{
				if (todaysFragment == null)
					return;
				this.Activity.RunOnUiThread(() =>
					todaysFragment.Visits = model.TodayVisits);
			});
			}
		}

		private VisitViewFragment nextView;
		private OtherVisitFragment otherViewFragment;
		TodaysVisitsFragment todaysFragment;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.Main, null);
			var fragTrans = FragmentManager.BeginTransaction().Replace(Resource.Id.nextVisit, nextView = new VisitViewFragment{Visit = Model.NextVisit, TopLabel = "Next Visit"});
			fragTrans.Replace(Resource.Id.otherVisits, otherViewFragment = new OtherVisitFragment() { Visits = Model.OtherVisits});
			fragTrans.Replace(Resource.Id.todaysVisits,todaysFragment =  new TodaysVisitsFragment(){Visits = Model.TodayVisits}).Commit();
			return view;
		}
	}
}