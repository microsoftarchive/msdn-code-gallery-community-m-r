// MapFragment customization:
//   https://developers.google.com/maps/documentation/android/map#mapfragment


using Android.App;
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MyShuttle.Client.Core.ViewModels;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Content.PM;
using MyShuttle.Client.Droid.Converters;
using MyShuttle.Client.Core.Converters;
using Cirrious.CrossCore;
using Chance.MvvmCross.Plugins.UserInteraction;

namespace MyShuttle.Client.Droid.Views
{
    [Activity(Label = "Detail", ScreenOrientation = ScreenOrientation.Portrait)]
    public class VehicleDetailView : MvxActivity
    {
        private const float ZoomLevel = 13.25f;

        private LatLng mapCenter;
        
        private MapFragment mapFragment;

        public LatLng MapCenter
        {
            get 
            { 
                return this.mapCenter; 
            }

            set
            {
                this.mapCenter = value;

                if (this.mapCenter == null || this.mapFragment == null)
                {
                    return;
                }

                var center = CameraUpdateFactory.NewLatLngZoom(value, ZoomLevel);

                this.mapFragment.Map.MoveCamera(center);
            }
        }

        protected VehicleDetailViewModel VehicleDetailViewModel
        {
            get
            {
                return base.ViewModel as VehicleDetailViewModel;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.VehicleDetailView);

            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);

            this.SetUpBindings();
        }

        private void SetUpBindings()
        {
            this.SetUpRequestButton();

            mapFragment = this.FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);

            if (mapFragment.Map == null)
            {
                return;
            }

            var actualCurrentPositionMarker = this.AddNewMarkerToMap(mapFragment,
                new LatLng(0, 0),
                BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));

            var actualVehicleAvailableMarker = this.AddNewMarkerToMap(mapFragment,
                new LatLng(0, 0),
                BitmapDescriptorFactory.FromResource(Resource.Drawable.carAvailable));

            var actualVehicleOccupiedMarker = this.AddNewMarkerToMap(mapFragment,
                new LatLng(0, 0),
                BitmapDescriptorFactory.FromResource(Resource.Drawable.carOccupied));

            var set = this.CreateBindingSet<VehicleDetailView, VehicleDetailViewModel>();

            set.Bind(this)
                .For(view => view.MapCenter)
                .To(vm => vm.CurrentVehicle)
                .WithConversion(new VehicleToLatLngConverter());

            set.Bind(actualCurrentPositionMarker)
                .For(marker => marker.Position)
                .To(vm => vm.CurrentLocation)
                .WithConversion(new LocationToLatLngConverter());

            set.Bind(actualVehicleAvailableMarker)
                .For(marker => marker.Position)
                .To(vm => vm.CurrentVehicle)
                .WithConversion(new VehicleToLatLngConverter());

            set.Bind(actualVehicleAvailableMarker)
                .For(marker => marker.Visible)
                .To(vm => vm.CurrentVehicle.VehicleStatus)
                .WithConversion(new VehicleStatusToBoolConverter(), true);

            set.Bind(actualVehicleOccupiedMarker)
                .For(marker => marker.Position)
                .To(vm => vm.CurrentVehicle)
                .WithConversion(new VehicleToLatLngConverter());

            set.Bind(actualVehicleOccupiedMarker)
                .For(marker => marker.Visible)
                .To(vm => vm.CurrentVehicle.VehicleStatus)
                .WithConversion(new VehicleStatusToBoolConverter());

            set.Apply();
        }

        private void SetUpRequestButton()
        {
            var requestButton = this.FindViewById<Button>(Resource.Id.requestButton);

            if (requestButton == null)
            {
                return;
            }

            requestButton.Click += async (sender, args) =>
                {
                    var userInteractionService = Mvx.Resolve<IUserInteraction>();

                    if (userInteractionService == null)
                    {
                        return;
                    }

                    await userInteractionService.AlertAsync("Vehicle requested. Thanks!");
                };
        }

        private Marker AddNewMarkerToMap(MapFragment mapFragment, LatLng position, BitmapDescriptor icon)
        {
            var marker = new MarkerOptions();

            marker.SetPosition(position);
            marker.InvokeIcon(icon);

            var actualMarker = mapFragment.Map.AddMarker(marker);
            
            return actualMarker;
        }
    }
}