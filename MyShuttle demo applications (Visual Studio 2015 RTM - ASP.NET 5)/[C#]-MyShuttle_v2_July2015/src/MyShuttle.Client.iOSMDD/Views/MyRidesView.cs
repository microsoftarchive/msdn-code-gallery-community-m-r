using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace MyShuttle.Client.iOS
{
	partial class MyRidesView : MvxViewController
	{
        private MyRidesTableViewSource tableViewSource;
        
        private BindableProgress bindableProgress;

        protected MyRidesViewModel MyRidesViewModel
        {
            get
            {
                return base.ViewModel as MyRidesViewModel;
            }
        }

		public MyRidesView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.bindableProgress = new BindableProgress(View);

            this.tableViewSource = new MyRidesTableViewSource(this.TableView);

            this.SetUpBindings();

            this.TableView.Source = this.tableViewSource;
            this.TableView.ReloadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.TableView.DeselectRow(this.TableView.IndexPathForSelectedRow, true);
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<MyRidesView, MyRidesViewModel>();
            
            set.Bind(this.tableViewSource)
                .To(vm => vm.MyLastRides);
            set.Bind(this.tableViewSource)
                .For(source => source.SelectionChangedCommand)
                .To(vm => vm.NavigateToRideDetailsAlternativeCommand);

            set.Bind(this.bindableProgress)
                .For(progress => progress.Visible)
                .To(vm => vm.IsLoadingMyLastRides);
            
            set.Apply();
        }
	}
}
