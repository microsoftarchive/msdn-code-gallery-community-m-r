
using Android.App;

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