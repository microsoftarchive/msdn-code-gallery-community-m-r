using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;

namespace MyShuttle.Client.iOS.Views
{
	partial class VehiclesByPriceView : VehiclesByCriteriaBaseView
	{
		public VehiclesByPriceView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            this.tableViewSource = new SearchTableViewSource(this.TableView);
            this.TableView.Source = this.tableViewSource;
            this.TableView.ReloadData();

            base.ViewDidLoad();
        }
        
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.TableView.DeselectRow(this.TableView.IndexPathForSelectedRow, true);
        }
	}
}
