// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using ListsManagement.ViewModels;
using DefaultScope;
using System.Threading;

namespace ListsManagement
{
    public partial class MainPage : PhoneApplicationPage
    {
        object _selectedItem;
        bool _cacheLoaded;
        Timer timer = null;
        static int index = 0;
        static string[] tickers = new string[]
        {
            "|",
            "/",
            "--",
            "|",
            "\\",
            "--",
        };

        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            Loaded += new RoutedEventHandler(MainPage_Loaded);

            PageTransitionList.Completed += new EventHandler(PageTransitionList_Completed);

            // Set the data context of the listbox control to the sample data
            DataContext = new MainViewModel();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ListBoxOne.SelectedIndex = -1;

            // Reset page transition
            ResetPageTransitionList.Begin();

            if (!this._cacheLoaded)
            {
                StartTimer();
                this.popupTitle.Text = "Loading Data...Please wait.";
                this.popupPanel.IsOpen = true;
                // Load the cache
                SyncContextInstance.Context.LoadCompleted += Context_LoadCompleted;

                SyncContextInstance.Context.LoadAsync();
            }
            else
            {
                this.popupPanel.IsOpen = false;
            }

            this.ListBoxOne.SelectedIndex = -1;
        }

        void Context_LoadCompleted(object sender, Microsoft.Synchronization.ClientServices.IsolatedStorage.LoadCompletedEventArgs e)
        {
            try
            {
                if (e.Exception != null)
                {
                    StopTimer();
                    Dispatcher.BeginInvoke(() =>
                    {
                        this.loadErrorTextBlk.Text = e.Exception.Message;
                        this.loadErrorTextBlk.Visibility = System.Windows.Visibility.Visible;
                    });
                }
                else
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        this.popupTitle.Text = "Synchronizing..Please wait.";
                        SyncContextInstance.Context.CacheController.RefreshCompleted += CacheController_RefreshCompleted;
                        SyncContextInstance.Context.CacheController.RefreshAsync();
                    });
                    this._cacheLoaded = true;
                }
            }
            finally
            {
                SyncContextInstance.Context.LoadCompleted -= Context_LoadCompleted;
            }
        }

        void CacheController_RefreshCompleted(object sender, Microsoft.Synchronization.ClientServices.RefreshCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    this.loadErrorTextBlk.Text = e.Error.Message;
                    this.loadErrorTextBlk.Visibility = System.Windows.Visibility.Visible;
                });
            }
            else
            {
                SyncContextInstance.Context.CacheController.RefreshCompleted -= CacheController_RefreshCompleted;
                SyncContextInstance.AddStats(e.Statistics, e.Error);
                Dispatcher.BeginInvoke(() => this.popupPanel.IsOpen = false);                                
            }
            StopTimer();
        }

        private void PageTransitionList_Completed(object sender, EventArgs e)
        {
            // Set datacontext of details page to selected listbox item
            NavigationService.Navigate(new Uri("/" + ((ItemViewModel)_selectedItem).DetailPage, UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsView.xaml", UriKind.Relative));
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex == -1)
            {
                // Do nothing when SelectedIndex is -1
                return;
            }

            // Capture selected item data
            _selectedItem = (sender as ListBox).SelectedItem;

            // Start page transition animation
            PageTransitionList.Begin();
        }

        private void SaveChangesBtn_Click(object sender, EventArgs e)
        {
            SyncContextInstance.Context.SaveChanges();
        }

        private void SyncNowBtn_Click(object sender, EventArgs e)
        {
            if (!SyncContextInstance.Context.CacheController.IsBusy)
            {
                StartTimer();
                this.loadErrorTextBlk.Visibility = System.Windows.Visibility.Collapsed;
                this.popupTitle.Text = "Synchronizing..Please wait.";
                this.popupPanel.IsOpen = true;
                SyncContextInstance.Context.CacheController.RefreshCompleted += new EventHandler<Microsoft.Synchronization.ClientServices.RefreshCompletedEventArgs>(CacheController_RefreshCompleted);
                SyncContextInstance.Context.CacheController.RefreshAsync();
            }
        }

        private void viewSyncLogBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SyncLogView.xaml", UriKind.Relative));
        }

        private void StartTimer()
        {
            timer = new Timer(delegate(object o)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    this.popupProgress.Text = tickers[index];
                    index++;
                    index %= tickers.Length;
                });
            }, null, 0, 500);
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            timer = null;
        }
    }
}