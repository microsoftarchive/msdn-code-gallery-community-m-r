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
using DefaultScope;
using ListsManagement.ViewModels;

namespace ListsManagement
{
    public partial class NearingCompletionView : PhoneApplicationPage
    {
        public NearingCompletionView()
        {
            InitializeComponent();
            this.Loaded += new System.Windows.RoutedEventHandler(NearingCompletionView_Loaded);
        }

        void NearingCompletionView_Loaded(object sender, System.Windows.RoutedEventArgs e1)
        {
            // In the sample database provide the value for completed is 4. So return only
            // non completed and non abandoned items.
            this.ListBoxOne.ItemsSource = SyncContextInstance.Context.ItemCollection.Where(e => e.Status < 4).OrderBy((e) => e.EndDate);
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOne.SelectedIndex >= 0)
            {
                NavigationService.Navigate(new Uri("/ItemDetailView.xaml", UriKind.Relative));
                FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
                root.DataContext = this.ListBoxOne.SelectedItem as Item;
                root.Tag = "DontSetDC";
            }
        }
    }
}