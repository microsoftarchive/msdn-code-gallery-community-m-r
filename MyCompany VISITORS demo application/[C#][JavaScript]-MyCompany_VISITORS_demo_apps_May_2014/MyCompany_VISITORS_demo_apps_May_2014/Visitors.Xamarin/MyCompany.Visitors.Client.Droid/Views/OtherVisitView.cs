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
using Android.Widget;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.Droid.Views
{
	public class OtherVisitView : ViewAnimator
	{
		Context context;
		public OtherVisitView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			this.context = context;
			Initialize();
		}

		ObservableCollection<VMVisit> visits;

		public ObservableCollection<VMVisit> Visits
		{
			get { return visits; }
			set
			{
				visits = value;
				startFlipping();
				this.ShowNext();
			}
		}

		Timer timer;
		void startFlipping()
		{
			if (visits.Count == 1)
				this.ShowNext();

			else if (visits.Count == 0 || timer != null)
				return;
			timer = new Timer(5);
			timer.Elapsed += (sender,args) => ShowNext();
			timer.Start();
		}
		private void Initialize()
		{

		}

		public override int ChildCount
		{
			get
			{
				return visits.Count;
			}
		}

		public override View GetChildAt(int index)
		{
			var inflater = LayoutInflater.FromContext(context);
			var view = new VisitView(context, null);
			view.Visit = Visits[index];
			return view;
		}
	}
}