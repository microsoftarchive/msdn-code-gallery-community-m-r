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
using System.Windows.Data;
using System.ComponentModel;
using ListsManagement.ViewModels;
using DefaultScope;

namespace ListsManagement
{
    public partial class TagsView : PhoneApplicationPage
    {
        public TagsView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(TagsView_Loaded);
        }

        void TagsView_Loaded(object sender, RoutedEventArgs e)
        {
            // Add a sort to the TagCollection view
            this.ListBoxOne.ItemsSource = SyncContextInstance.Context.TagCollection.OrderBy((e1) => e1.Name);

            this.ListBoxOne.SelectedIndex = -1;
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOne.SelectedIndex == -1)
            {
                return;
            }
            NavigationService.Navigate(new Uri("/ItemsByTagView.xaml", UriKind.Relative));
            (Application.Current.RootVisual as FrameworkElement).DataContext =
                ListBoxOne.SelectedItem as Tag;
        }
    }
}