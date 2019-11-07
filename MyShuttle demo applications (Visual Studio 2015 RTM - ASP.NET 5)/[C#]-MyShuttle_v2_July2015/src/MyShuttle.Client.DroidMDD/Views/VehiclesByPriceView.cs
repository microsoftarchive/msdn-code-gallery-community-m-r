using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;

namespace MyShuttle.Client.Droid.Views
{
    [Activity(Label = "VehiclesByPriceView")]
    public class VehiclesByPriceView : VehiclesByCriteriaBaseView
    {
        public VehiclesByPriceView()
            : base(new string[] { "distanceText", "milesText" })
        { }
    }
}