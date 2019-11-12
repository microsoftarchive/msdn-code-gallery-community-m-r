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
	public class TallVisitView : VisitView
	{
		public TallVisitView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			Initialize(context);
		}

		public TallVisitView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize(context);
		}
		protected override void InflateView(LayoutInflater inflater)
		{

			inflater.Inflate(Resource.Layout.TallVisitView, this, true);
		}
	}
}