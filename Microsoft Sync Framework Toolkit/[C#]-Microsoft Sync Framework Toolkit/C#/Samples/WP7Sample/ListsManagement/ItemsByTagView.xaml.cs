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
    public partial class ItemsByTagView : PhoneApplicationPage
    {
        Tag selectedTag = null;
        public ItemsByTagView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ItemsByTagView_Loaded);
        }

        void ItemsByTagView_Loaded(object sender, RoutedEventArgs e1)
        {
            ListBoxOne.SelectedIndex = -1;

            if (selectedTag == null)
            {
                this.selectedTag = this.DataContext as Tag;
            }
            else
            {
                this.DataContext = selectedTag;
            }

            ListBoxOne.ItemsSource = from items in SyncContextInstance.Context.ItemCollection.Where((e) => e.Status < 2)
                                     join t2i in SyncContextInstance.Context.TagItemMappingCollection.Where((e) =>e.TagID.Equals(selectedTag.ID))
                                     on items.ID equals t2i.ItemID 
                                     select items;
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOne.SelectedIndex == -1)
            {
                return;
            }
            NavigationService.Navigate(new Uri("/ItemDetailView.xaml", UriKind.Relative));
            FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
            root.DataContext = this.ListBoxOne.SelectedItem as Item;
            root.Tag = "DontSetDC";
        }


    }
}