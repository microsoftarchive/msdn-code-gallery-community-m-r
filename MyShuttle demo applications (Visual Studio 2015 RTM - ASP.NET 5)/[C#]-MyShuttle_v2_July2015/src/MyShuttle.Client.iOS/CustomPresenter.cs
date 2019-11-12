using System;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.Touch.Views;
using MyShuttle.Client.Core.ViewModels;

namespace MyShuttle.Client.iOS
{
    internal class CustomPresenter : MvxTouchViewPresenter
    {
        internal CustomPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        { }

        public override void Show(IMvxTouchView view)
        {
            if (view is MainView)
            {
                this.AddSettingsButtonToNavBar(view);
            }
            else if (view is SettingsView)
            {
                var viewController = view as SettingsView;

                viewController.NavigationItem.HidesBackButton = true;
            }

            base.Show(view);
        }

        private void AddSettingsButtonToNavBar(IMvxTouchView view)
        {
            var viewController = view as MainView;

            EventHandler navigateToSettings = (sender, args) =>
            {
                (viewController.ViewModel as MainViewModel).NavigateToSettingsCommand.Execute(null);
            };

            var settingsButton = new UIBarButtonItem(
                UIImage.FromFile("settings.png"),
                UIBarButtonItemStyle.Plain,
                navigateToSettings);

            viewController.NavigationItem.SetRightBarButtonItem(settingsButton, true);
        }
    }
}