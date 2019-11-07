
using Android.App;

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