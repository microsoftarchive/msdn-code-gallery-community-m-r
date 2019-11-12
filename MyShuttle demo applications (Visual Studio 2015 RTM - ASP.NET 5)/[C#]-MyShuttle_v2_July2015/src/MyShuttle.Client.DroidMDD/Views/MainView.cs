using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels;

namespace MyShuttle.Client.Droid.Views
{
    [Activity(Label = "View for FirstViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainView : MvxTabActivity
    {
        protected MainViewModel MainViewModel
        {
            get
            {
                return base.ViewModel as MainViewModel;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.RequestWindowFeature(WindowFeatures.NoTitle);
            
            this.SetContentView(Resource.Layout.MainView);

            this.AddNewTab("Distance", this.MainViewModel.VehiclesByDistanceViewModel);
            this.AddNewTab("Price", this.MainViewModel.VehiclesByPriceViewModel);
        }

        private void AddNewTab(string title, IMvxViewModel viewModel)
        {
            var tabSpec = TabHost.NewTabSpec(title);

            tabSpec.SetIndicator(title);
            tabSpec.SetContent(this.CreateIntentFor(viewModel));

            this.TabHost.AddTab(tabSpec);
        }
    }
}