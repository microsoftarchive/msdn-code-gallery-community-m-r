using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util.Jar;
using MyCompany.Visitors.Client.Droid.Views;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.Droid.Fragments
{
	public class TodaysVisitsFragment : Fragment
	{
		ImageAdapter source;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            RetainInstance = true;
		    // Create your fragment here
		}

		Gallery gallery;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.TodaysVisitsView, null);
			gallery = view.FindViewById<Gallery>(Resource.Id.gallery);
			gallery.Adapter = source = new ImageAdapter(this);
			gallery.SetSpacing(30);
			return view;
		}


		ObservableCollection<VMVisit> visits = new ObservableCollection<VMVisit>();
		public ObservableCollection<VMVisit> Visits
		{
			get { return visits; }
			set
			{
				try
				{
					if (visits != null)
						visits.CollectionChanged -= VisitsOnCollectionChanged;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				visits = value;
				visits.CollectionChanged += VisitsOnCollectionChanged;
				if (gallery == null)
					return;
				gallery.Adapter = source = new ImageAdapter(this);
				gallery.SetMinimumWidth(Resources.DisplayMetrics.WidthPixels);
			}
		}


		void VisitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add || args.Action == NotifyCollectionChangedAction.Remove)
				Activity.RunOnUiThread(() =>
				{
					gallery.Adapter = source = new ImageAdapter(this);
				});
		}

		public class ImageAdapter : BaseAdapter
		{
			TodaysVisitsFragment frag;

			public ImageAdapter(TodaysVisitsFragment fragment)
			{
				frag = fragment;
			}

			public override int Count
			{
				get
				{
					return frag.Visits.Count;
				}
			}

			public override Java.Lang.Object GetItem(int position)
			{
				return null;
			}

			public override long GetItemId(int position)
			{
				return 0;
			}

			// create a new ImageView for each item referenced by the Adapter
			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				var view = new TallVisitView(frag.Activity,null);
				view.Visit = frag.Visits[position];

				view.LayoutParameters = new Gallery.LayoutParams(frag.Resources.DisplayMetrics.WidthPixels/3 - 20, 1000);
				return view;
			}

		}


	}
}