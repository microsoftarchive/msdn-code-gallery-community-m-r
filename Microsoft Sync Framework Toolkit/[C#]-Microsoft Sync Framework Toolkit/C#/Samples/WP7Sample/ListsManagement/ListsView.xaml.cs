// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using DefaultScope;
using ListsManagement.ViewModels;
using Microsoft.Phone.Controls;

namespace ListsManagement
{
    public partial class ListsView : PhoneApplicationPage
    {
        public ListsView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ListsView_Loaded);
        }

        void ListsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = SyncContextInstance.Context;
            this.ListBoxOne.SelectedIndex = -1;
        }

        private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOne.SelectedIndex >= 0)
            {
                List selectedList = ((ListBox)sender).SelectedItem as List;
                NavigationService.Navigate(new Uri("/ListDetailsView.xaml", UriKind.Relative));
                FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
                root.DataContext = selectedList;
            }
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddNewListBtn_Click(object sender, EventArgs e)
        {
            List newList = new List();
            newList.ID = Guid.NewGuid();
            newList.UserID = SettingsViewModel.Instance.UserId;
            newList.Name = "New List";
            newList.Description = "Enter Description.";
            newList.CreatedDate = DateTime.Now;

            NavigationService.Navigate(new Uri("/AddListView.xaml", UriKind.Relative));
            (Application.Current.RootVisual as FrameworkElement).DataContext = newList;
        }
    }
}