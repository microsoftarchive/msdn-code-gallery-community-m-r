using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;
using MyShuttle.Client.iOS.Views.Annotations;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MyShuttle.Client.iOS
{
	partial class VehiclesInMapView : MvxViewController
	{
        private AnnotationManager annotationManager;
        
        private SearchTableViewSource tableViewSource;

        protected VehiclesInMapViewModel VehiclesInMapViewModel
        {
            get
            {
                return this.ViewModel as VehiclesInMapViewModel;
            }
        }

		public VehiclesInMapView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.annotationManager = new AnnotationManager(this.Map);
            this.Map.Delegate = new MapDelegate(/* customize layout */ true);

            this.Map.AddGestureRecognizer(new UITapGestureRecognizer(_ =>
                {
                    this.VehiclesInMapViewModel.SelectedVehicle = null;
                }));

            this.tableViewSource = new SearchTableViewSource(this.SelectedVehicleTable);
            this.SelectedVehicleTable.Source = this.tableViewSource;
            this.SelectedVehicleTable.ReloadData();

            this.SetUpBindings();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.SelectedVehicleTable.DeselectRow(this.SelectedVehicleTable.IndexPathForSelectedRow, true);
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<VehiclesInMapView, VehiclesInMapViewModel>();

            set.Bind(this.Map)
                .For(map => map.Region)
                .To(vm => vm.CurrentLocation)
                .WithConversion("LocationToRegion");
            set.Bind(this.annotationManager)
                .For(manager => manager.ShowDetailsCommand)
                .To(vm => vm.SwitchSelectedVehicleCommand);
            set.Bind(this.annotationManager)
                .For(manager => manager.ItemsSource)
                .To(vm => vm.FilteredVehicles);
            set.Bind(this.annotationManager)
                .For(manager => manager.CurrentLocation)
                .To(vm => vm.CurrentLocation);

            set.Bind(this.SelectedVehicleTable)
                .For(table => table.Hidden)
                .To(vm => vm.SelectedVehicle)
                .WithConversion("NullToBoolWithOptionalInverse");
            set.Bind(this.tableViewSource)
                .To(vm => vm.SelectedVehicles);
            set.Bind(this.tableViewSource)
                .For(source => source.SelectionChangedCommand)
                .To(vm => vm.NavigateToVehicleDetailsAlternativeCommand);

            set.Apply();
        }
	}
}
