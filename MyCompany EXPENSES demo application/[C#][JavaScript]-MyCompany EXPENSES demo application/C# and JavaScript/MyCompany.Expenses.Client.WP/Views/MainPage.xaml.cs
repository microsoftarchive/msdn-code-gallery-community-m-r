namespace MyCompany.Expenses.Client.WP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MyCompany.Expenses.Client.WP.Resources;
    using MyCompany.Expenses.Client.WP.Settings;
    using GalaSoft.MvvmLight.Messaging;
    using MyCompany.Expenses.Client.WP.Messages;

    /// <summary>
    /// Main Page
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        ApplicationBarMenuItem settingsMenuItem;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Messages.TeamManagerMessage>(this, (msg) =>
            {
                // Force the calculation of the panorama items
                // because with MVVM Bindging is not working correctly
                // http://blogs.codes-sources.com/kookiz/archive/2012/07/01/wp7-dynamically-toggle-panoramaitem-visibility.aspx

                this.TeamHistoryPanoramaItem.Visibility = Visibility.Visible;
                this.TeamPendingPanoramaItem.Visibility = Visibility.Visible;
                this.PanoramaControl.DefaultItem = this.PanoramaControl.DefaultItem;
            });
        }

        /// <summary>
        /// On navigated to
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Messenger.Default.Send(new ReloadMainViewMessage());
            GenerateAppBar();
        }

        /// <summary>
        /// Regenerates appbar icons and menu items.
        /// </summary>
        private void GenerateAppBar()
        {
            this.ApplicationBar.MenuItems.Clear();

            this.settingsMenuItem = new ApplicationBarMenuItem();
            this.settingsMenuItem.Text = AppResources.SettingsMenuItemText;
            this.settingsMenuItem.Click += NavTo_Settings;

            this.ApplicationBar.MenuItems.Add(this.settingsMenuItem);
        }

        /// <summary>
        /// settings navigation event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavTo_Settings(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Views/SettingsPage.xaml", UriKind.Relative));
        }
    }
}