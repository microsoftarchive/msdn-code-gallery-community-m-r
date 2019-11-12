using System;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MyShuttle.Client.iOS.Views.Annotations;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;

namespace MyShuttle.Client.iOS
{
    partial class VehicleDetailView : MvxViewController
	{
        private AnnotationManager annotationManager;
        
        private BindableProgress bindableProgress;

        protected VehicleDetailViewModel VehicleDetailViewModel
        {
            get
            {
                return this.ViewModel as VehicleDetailViewModel;
            }
        }

		public VehicleDetailView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.bindableProgress = new BindableProgress(View);

            this.CustomizeNavigationBar();

            this.annotationManager = new AnnotationManager(this.Map);
            this.Map.Delegate = new MapDelegate();

            this.SetUpDefaultCoordinateRegion();
            this.SetUpBindings();
        }

        private void CustomizeNavigationBar()
        {
            this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(
                UIImage.FromFile("back.png"),
                UIBarButtonItemStyle.Plain,
                (sender, args) => this.NavigationController.PopViewControllerAnimated(true));
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<VehicleDetailView, VehicleDetailViewModel>();

            set.Bind(this.bindableProgress)
                .For(progress => progress.Visible)
                .To(vm => vm.IsLoadingVehicle);

            set.Bind(this.VehiclePicture)
                .To(vm => vm.CurrentVehicle.Picture)
                .WithConversion("InMemoryImage");
            set.Bind(this.Maker)
                .To(vm => vm.CurrentVehicle.Make);
            set.Bind(this.Model)
                .To(vm => vm.CurrentVehicle.Model);
            set.Bind(this.DriverName)
                .To(vm => vm.CurrentVehicle.Driver.Name);
            set.Bind(this.Status)
                .To(vm => vm.CurrentVehicle.VehicleStatus);
            set.Bind(this.Status)
                .For(label => label.TextColor)
                .To(vm => vm.CurrentVehicle.VehicleStatus)
                .WithConversion("VehicleStatusToUIColor");

            set.Bind(this.RatingStar1)
                .For(image => image.Hidden)
                .To(vm => vm.CurrentVehicle.RatingAvg)
                .WithConversion("RatingToBool", 1)
                .WithFallback(true);
            set.Bind(this.RatingStar2)
                .For(image => image.Hidden)
                .To(vm => vm.CurrentVehicle.RatingAvg)
                .WithConversion("RatingToBool", 2)
                .WithFallback(true);
            set.Bind(this.RatingStar3)
                .For(image => image.Hidden)
                .To(vm => vm.CurrentVehicle.RatingAvg)
                .WithConversion("RatingToBool", 3)
                .WithFallback(true);
            set.Bind(this.RatingStar4)
                .For(image => image.Hidden)
                .To(vm => vm.CurrentVehicle.RatingAvg)
                .WithConversion("RatingToBool", 4)
                .WithFallback(true);
            set.Bind(this.RatingStar5)
                .For(image => image.Hidden)
                .To(vm => vm.CurrentVehicle.RatingAvg)
                .WithConversion("RatingToBool", 5)
                .WithFallback(true);

            set.Bind(this.Type)
                .To(vm => vm.CurrentVehicle.Type);
            set.Bind(this.Seats)
                .To(vm => vm.CurrentVehicle.Seats);
            set.Bind(this.LicensePlate)
                .To(vm => vm.CurrentVehicle.LicensePlate);
            set.Bind(this.Rate)
                .For(label => label.Text)
                .To(vm => vm.CurrentVehicle.Rate)
                .WithConversion("PriceToString", false);
            
            set.Bind(this.Map)
                .For(map => map.Region)
                .To(vm => vm.CurrentVehicle)
                .WithConversion("VehicleToRegion");

            set.Bind(this.annotationManager)
                .For(manager => manager.SingleVehicle)
                .To(vm => vm.CurrentVehicle);
            set.Bind(this.annotationManager)
                .For(manager => manager.CurrentLocation)
                .To(vm => vm.CurrentLocation);

            set.Bind(this.CallButton)
                .To(vm => vm.CallVehicleCommand);
            set.Bind(this.CallButton)
                .For(button => button.Enabled)
                .To(vm => vm.CurrentVehicle.Driver.Phone)
                .WithConversion("NullToBoolWithOptionalInverse", true)
                .WithFallback(false);

            set.Bind(this.RequestButton)
                .To(vm => vm.RequestVehicleCommand);

            set.Apply();
        }

        private void SetUpDefaultCoordinateRegion()
        {
            var location = new CLLocationCoordinate2D(MapSettings.DefaultLatitudeValue, MapSettings.DefaultLongitudeValue);
            this.Map.Region = new MKCoordinateRegion
            {
                Center = location,
                Span = new MKCoordinateSpan(MapSettings.LatitudeLongitudeDelta, MapSettings.LatitudeLongitudeDelta)
            };
        }
    }
}
