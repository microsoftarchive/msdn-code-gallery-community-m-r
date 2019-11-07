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
using Microsoft.Synchronization.ClientServices;

namespace ListsManagement
{
    public partial class SyncLogView : PhoneApplicationPage
    {
        public SyncLogView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SyncLogView_Loaded);
        }

        void SyncLogView_Loaded(object sender, RoutedEventArgs e)
        {
            DisabledErrorLbl.Visibility = (SettingsViewModel.Instance.SyncLogEnabled)
                ? System.Windows.Visibility.Collapsed
                : System.Windows.Visibility.Visible;
            ListBoxOne.ItemsSource = SyncContextInstance.Stats;
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOne.SelectedIndex >= 0)
            {
                NavigationService.Navigate(new Uri("/CacheRefreshStatisticsView.xaml", UriKind.Relative));
                FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
                root.DataContext = ((ListBox)sender).SelectedItem as CacheRefreshStatistics;
            }
        }
    }
}