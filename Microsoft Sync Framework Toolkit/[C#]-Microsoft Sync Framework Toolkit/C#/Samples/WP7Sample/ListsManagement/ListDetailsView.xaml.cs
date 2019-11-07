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
using System.Collections.Specialized;
using System.ComponentModel;

namespace ListsManagement
{
    public partial class ListDetailsView : PhoneApplicationPage, INotifyPropertyChanged
    {
        List currentList = null;
        public IEnumerable<Item> Items
        {
            get
            {
                Guid listId = (this.DataContext as List).ID;
                // In the sample database provide the value for completed is 4. So return only
                // non completed and non abandoned items.
                return SyncContextInstance.Context.ItemCollection.Where((e) => e.ListID.Equals(listId) && e.Status < 4);
            }
        }

        public ListDetailsView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ListDetailsView_Loaded);
        }

        void ListDetailsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListBoxOne.DataContext = this;
            this.ListBoxOne.SelectedIndex = -1;
            if (this.currentList == null)
            {
                this.currentList = this.DataContext as List;
            }
            else
            {
                this.DataContext = currentList;
                this.RaisePropertyChanged("Items");
            }
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOne.SelectedIndex >= 0)
            {
                NavigationService.Navigate(new Uri("/ItemDetailView.xaml", UriKind.Relative));
                FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
                root.DataContext = this.ListBoxOne.SelectedItem as Item;
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Item newItem = new Item();
            newItem.ID = Guid.NewGuid();
            newItem.Name = "New Item";
            newItem.Description = "Enter description.";
            newItem.StartDate = DateTime.Now;
            newItem.EndDate = DateTime.Now.AddDays(1);
            newItem.Status = 1;
            newItem.Priority = 1;
            newItem.ListID = (this.DataContext as List).ID;
            newItem.UserID = (this.DataContext as List).UserID;

            NavigationService.Navigate(new Uri("/ItemDetailView.xaml?mode=Add", UriKind.Relative));
            FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
            root.DataContext = newItem;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            List list = this.DataContext as List;
            SyncContextInstance.Context.DeleteList(list);

            // Do a cascade delete
            Item[] associatedItems = SyncContextInstance.Context.ItemCollection.Where(e1 => e1.ListID == list.ID).ToArray();
            foreach (Item item in associatedItems)
            {
                SyncContextInstance.Context.DeleteItem(item);
                TagItemMapping[] tags = SyncContextInstance.Context.TagItemMappingCollection.Where(e1 => e1.ItemID == item.ID).ToArray();
                foreach (TagItemMapping t2i in tags)
                {
                    SyncContextInstance.Context.DeleteTagItemMapping(t2i);
                }
            }
            SyncContextInstance.Context.SaveChanges();
            NavigationService.GoBack();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddListView.xaml", UriKind.Relative));
            (Application.Current.RootVisual as FrameworkElement).Tag = "EditMode";
            (Application.Current.RootVisual as FrameworkElement).DataContext = this.DataContext as List;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}