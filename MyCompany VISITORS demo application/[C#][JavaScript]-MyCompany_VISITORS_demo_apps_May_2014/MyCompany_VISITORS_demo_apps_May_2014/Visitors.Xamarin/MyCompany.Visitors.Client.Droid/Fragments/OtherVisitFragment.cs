using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using MyCompany.Visitors.Client.Droid.Views;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.Droid.Fragments
{
	public class OtherVisitFragment : VisitViewFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            RetainInstance = true;
		    // Create your fragment here
		}

		ObservableCollection<VMVisit> visits = new ObservableCollection<VMVisit>();

		public ObservableCollection<VMVisit> Visits
		{
			get { return visits; }
			set
			{
				visits = value;
				startFlipping();
			}
		}

		Timer timer;
		void startFlipping()
		{
			if (visits.Count == 1)
				this.ShowNext();

			else if (visits.Count == 0 || timer != null)
				return;
			timer = new Timer(5000);
			timer.Elapsed += (sender, args) => Activity.RunOnUiThread(ShowNext);
			timer.Start();
		}

		public override void SetupVisit()
		{
			base.SetupVisit();
			topValue.Text = visits.Count.ToString();
			topLabel.Text = "Other Visits";
		}

		public void ShowNext()
		{
			int currentIndex = Visit == null ? -1 : visits.IndexOf(Visit);
			currentIndex++;
			if (currentIndex >= visits.Count)
				currentIndex = 0;
            if (visits.Count > currentIndex)
			    Visit = visits[currentIndex];
		}
	}
}