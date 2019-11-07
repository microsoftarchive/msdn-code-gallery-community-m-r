
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

    public class VehiclesByCriteriaBaseView : MvxActivity
    {
        private BindableProgress _bindableProgress;
        
        private string[] controlsTagsToHide;

        public VehiclesByCriteriaBaseView()
        { }

        public VehiclesByCriteriaBaseView(string[] controlsTagsToHide)
        {
            this.controlsTagsToHide = controlsTagsToHide;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.VehiclesByCriteriaBaseView);

            this._bindableProgress = new BindableProgress(this);

            this.SetUpBindings();
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (this.controlsTagsToHide == null || this.controlsTagsToHide.Length == 0)
            {
                return;
            }

            var listView = this.FindViewById<ListView>(Resource.Id.listView);
            
            if (listView != null)
            {
                listView.ChildViewAdded += this.HideSpecificControls;
            }
        }

        private void HideSpecificControls(object sender, ViewGroup.ChildViewAddedEventArgs e)
        {
            var childView = e.Child;

            foreach (var tag in this.controlsTagsToHide)
            {
                var control = childView.FindViewWithTag(tag);

                if (control != null)
                {
                    control.Visibility = ViewStates.Invisible;
                }
            }
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<VehiclesByCriteriaBaseView, VehiclesViewModelBase>();

            set.Bind(_bindableProgress)
                .For(progress => progress.Visible)
                .To(vm => vm.IsLoadingFilteredVehicles);

            set.Apply();
        }
    }
}