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
using ListsManagement.ViewModels;
using System.ComponentModel;
using System.Threading;

namespace ListsManagement
{
    public partial class SettingsView : PhoneApplicationPage
    {
        static string[] tickers = new string[] 
        {
            "| Computing",
            "/ Computing",
            "-- Computing",
            "| Computing",
            "\\ Computing",
            "-- Computing",
        };

        static int index = 0;
        Timer timer = null;

        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = SettingsViewModel.Instance;
            this.Loaded += new RoutedEventHandler(SettingsView_Loaded);
        }

        void SettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            AsyncReadCacheSize();
        }

        private void AsyncReadCacheSize()
        {
            timer = new Timer(delegate(object o)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    this.cacheSizeLbl.Text = tickers[index];
                    index++;
                    index %= tickers.Length;
                });
            }, null, 0, 500);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync(SettingsViewModel.Instance);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SettingsViewModel instance = e.Argument as SettingsViewModel;
            // Compute the cache file size
            string cacheSize = instance.CacheDataSize;
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer = null;
            Dispatcher.BeginInvoke(() => this.cacheSizeLbl.Text = cacheSize);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            SettingsViewModel.SaveSettingsFile();
            NavigationService.GoBack();
        }

        private void ToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            enableAutoSync.Content = SettingsViewModel.Instance.AutoSyncEnabled ? "On" : "Off";
            enableSyncLog.Content = SettingsViewModel.Instance.SyncLogEnabled ? "On" : "Off";
            syncInterval.IsReadOnly = !SettingsViewModel.Instance.AutoSyncEnabled;
        }

        private void enableAutoSync_Click(object sender, RoutedEventArgs e)
        {
            enableAutoSync.Content = SettingsViewModel.Instance.AutoSyncEnabled ? "On" : "Off";
            enableSyncLog.Content = SettingsViewModel.Instance.SyncLogEnabled ? "On" : "Off";
            syncInterval.IsReadOnly = !SettingsViewModel.Instance.AutoSyncEnabled;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SyncContextInstance.Context.ClearCache();
            AsyncReadCacheSize();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Delete the cache
                SyncContextInstance.Context.ClearCache();
            }
            finally
            {
                SettingsViewModel.Instance.UserId = Guid.Empty;
                SettingsViewModel.Instance.UserName = null;
                SyncContextInstance.ClearContext();
                SettingsViewModel.SaveSettingsFile();
                NavigationService.Navigate(new Uri("/Bootstrapview.xaml", UriKind.Relative));
            }
        }
    }
}