
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Droid.Views;
using MyShuttle.Client.Core.ViewModels;

namespace MyShuttle.Client.Droid.Views
{
    [Activity(Label = "VehiclesByDistanceView")]
    public class VehiclesByDistanceView : VehiclesByCriteriaBaseView
    {
        public VehiclesByDistanceView()
            : base(new string[] { "priceText", "dollarText" })
        { }
    }
}