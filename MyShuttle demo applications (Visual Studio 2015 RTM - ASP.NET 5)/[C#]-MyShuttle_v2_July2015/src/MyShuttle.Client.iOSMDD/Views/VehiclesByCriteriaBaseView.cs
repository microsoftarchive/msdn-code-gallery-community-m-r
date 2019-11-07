using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MyShuttle.Client.iOS.Views
{
    class VehiclesByCriteriaBaseView : MvxViewController
    {
        protected BindableProgress bindableProgress;

        protected SearchTableViewSource tableViewSource;

        protected VehiclesViewModelBase VehiclesViewModelBase
        {
            get
            {
                return this.ViewModel as VehiclesViewModelBase;
            }
        }

		public VehiclesByCriteriaBaseView(IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.bindableProgress = new BindableProgress(View);

            this.SetUpBindings();
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<VehiclesByCriteriaBaseView, VehiclesViewModelBase>();

            set.Bind(this.tableViewSource)
                .To(vm => vm.FilteredVehicles);
            set.Bind(this.tableViewSource)
                .For(source => source.SelectionChangedCommand)
                .To(vm => vm.NavigateToVehicleDetailsAlternativeCommand);

            set.Bind(this.bindableProgress)
                .For(progress => progress.Visible)
                .To(vm => vm.IsLoadingFilteredVehicles);

            set.Apply();
        }
    }
}