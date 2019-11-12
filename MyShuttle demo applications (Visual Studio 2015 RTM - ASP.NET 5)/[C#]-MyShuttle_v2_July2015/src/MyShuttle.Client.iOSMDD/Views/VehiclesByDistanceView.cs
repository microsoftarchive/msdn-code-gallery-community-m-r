using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MBProgressHUD;

namespace MyShuttle.Client.iOS.Views
{
    partial class VehiclesByDistanceView : VehiclesByCriteriaBaseView
	{
        public VehiclesByDistanceView (IntPtr handle) 
            : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            this.CustomizeNavigationBar();

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

        private void CustomizeNavigationBar()
        {
            var navigationController = this.ParentViewController.NavigationController;

            navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            navigationController.NavigationBar.Translucent = false;
            // TODO: Share color palettes across PCL
            navigationController.NavigationBar.BarTintColor = new UIColor(47 / 255f, 60 / 255f, 76 / 255f, 1);
            navigationController.NavigationBar.TintColor = UIColor.White;
            navigationController.NavigationBar.SetBackgroundImage(
                UIImage.FromFile("bgnav.png"),
                UIBarMetrics.Default);

            var logoImage = UIImage.FromFile("logo.png");
            var logoView = new UIImageView(logoImage);

            navigationController.NavigationBar.TopItem.TitleView = logoView;
        }
	}
}
