using System;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels;

namespace MyShuttle.Client.iOS
{
    partial class MainView : MvxTabBarViewController
	{
        private int tabsCreatedSoFarCount = 0;

        protected MainViewModel MainViewModel
        {
            get
            {
                return base.ViewModel as MainViewModel;
            }
        }

        public MainView(IntPtr handle)
            : base(handle)
        {
            this.ViewDidLoad();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (this.ViewModel == null)
            {
                return;
            }

            this.SetUpTabBar();
        }

        private void SetUpTabBar()
        {
            var viewControllers = new UIViewController[]
            {
                CreateTabFor("Distance", "distance", this.MainViewModel.VehiclesByDistanceViewModel),
                CreateTabFor("Price", "price", this.MainViewModel.VehiclesByPriceViewModel),
                CreateTabFor("Map", "map", this.MainViewModel.VehiclesInMapViewModel),
                CreateTabFor("My Rides", "myrides", this.MainViewModel.MyRidesViewModel)
            };

            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
            SelectedViewController = ViewControllers[0];

            // TODO: Share color palettes across PCL
            this.TabBar.TintColor = new UIColor(70 / 255f, 209 / 255f, 182 / 255f, 1);
        }

        private UIViewController CreateTabFor(string title, string imageName, IMvxViewModel viewModel)
        {
            var controller = new UINavigationController();
            controller.NavigationBarHidden = true;

            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

            SetTitleAndTabBarItem(screen, title, imageName);
            controller.PushViewController(screen, false);

            return controller;
        }

        private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
        {
            screen.Title = title;
            screen.TabBarItem = new UITabBarItem(
                title,
                UIImage.FromBundle(imageName + ".png").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal),
                tabsCreatedSoFarCount);
            screen.TabBarItem.SelectedImage = UIImage.FromBundle(imageName + "active.png")
                .ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            tabsCreatedSoFarCount++;
        }
	}
}
