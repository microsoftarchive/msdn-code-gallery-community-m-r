// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Windows;
using DefaultScope;
using ListsManagement.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ListsManagement.Converters;
using System.Globalization;

namespace ListsManagement
{
    public partial class ItemDetailView : PhoneApplicationPage
    {
        List listObj;
        bool addMode = false;
        bool isInView = false;

        public ItemDetailView()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(ItemDetailView_SizeChanged);
            this.Loaded += new RoutedEventHandler(ItemDetailView_Loaded);
        }

        void ItemDetailView_Loaded(object sender, RoutedEventArgs e1)
        {
            this.listObj = SyncContextInstance.Context.ListCollection.Where((e) => e.ID.Equals((this.DataContext as Item).ListID)).FirstOrDefault();
            addMode = (this.DataContext as Item).EntityState == Microsoft.Synchronization.ClientServices.IsolatedStorage.OfflineEntityState.Detached;

            // If add mode then only the done button will be visible
            if (addMode)
            {
                FlipToEditMode();
                (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = false;
                (this.ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = false;
                (this.ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = true;
                (this.ApplicationBar.Buttons[3] as ApplicationBarIconButton).IsEnabled = false;
            }
        }

        void ItemDetailView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.popupPanel.IsOpen)
            {
                tagsChooser.Width = LayoutRoot.ActualWidth;
                tagsChooser.Height = LayoutRoot.ActualHeight;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e1)
        {
            base.OnNavigatedTo(e1);

            this.isInView = true;
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.isInView = false;
            base.OnNavigatingFrom(e);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            FlipToEditMode();

            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = false;
            (this.ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = true;
            (this.ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = true;
            (this.ApplicationBar.Buttons[3] as ApplicationBarIconButton).IsEnabled = false;
        }

        private void FlipToEditMode()
        {
            // Collapse the labels and enable the TextBoxes for editable fields
            this.itemNameTxt.Visibility = System.Windows.Visibility.Visible;
            this.itemNameLbl.Visibility = System.Windows.Visibility.Collapsed;

            this.itemDescTxt.IsReadOnly = false;

            this.dueByLbl.Visibility = System.Windows.Visibility.Collapsed;
            this.dueByTxt.Visibility = System.Windows.Visibility.Visible;

            this.tagsLbl.Visibility = System.Windows.Visibility.Collapsed;
            this.tagsTxt.Visibility = System.Windows.Visibility.Visible;

            this.statusLbl.Visibility = System.Windows.Visibility.Collapsed;
            this.statusTxt.Visibility = System.Windows.Visibility.Visible;

            this.priorityLbl.Visibility = System.Windows.Visibility.Collapsed;
            this.priorityTxt.Visibility = System.Windows.Visibility.Visible;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            Item item = this.DataContext as Item;
            SyncContextInstance.Context.DeleteItem(item);

            // Do a cascaded delete
            TagItemMapping[] tags = SyncContextInstance.Context.TagItemMappingCollection.Where(e1 => e1.ItemID == item.ID).ToArray();
            foreach (TagItemMapping t2i in tags)
            {
                SyncContextInstance.Context.DeleteTagItemMapping(t2i);
            }
            
            SyncContextInstance.Context.SaveChanges();
            GoBack();
        }

        private void GoBack()
        {
            NavigationService.GoBack();
            if (this.Tag == null || this.Tag.ToString() != "DontSetDC")
            {
                (Application.Current.RootVisual as FrameworkElement).DataContext = this.listObj;
            }
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                FlipToReadMode();
                GoBack();
            }
            else
            {
                SyncContextInstance.Context.AddItem(this.DataContext as Item);
                SyncContextInstance.Context.SaveChanges();
                GoBack();
            }
        }

        private void FlipToReadMode()
        {
            // Enable the labels and collapse the TextBoxes for editable fields
            this.itemNameTxt.Visibility = System.Windows.Visibility.Collapsed;
            this.itemNameLbl.Visibility = System.Windows.Visibility.Visible;

            this.itemDescTxt.IsReadOnly = true;

            this.dueByLbl.Visibility = System.Windows.Visibility.Visible;
            this.dueByTxt.Visibility = System.Windows.Visibility.Collapsed;

            this.tagsLbl.Visibility = System.Windows.Visibility.Visible;
            this.tagsTxt.Visibility = System.Windows.Visibility.Collapsed;

            this.statusLbl.Visibility = System.Windows.Visibility.Visible;
            this.statusTxt.Visibility = System.Windows.Visibility.Collapsed;

            this.priorityLbl.Visibility = System.Windows.Visibility.Visible;
            this.priorityTxt.Visibility = System.Windows.Visibility.Collapsed;

            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = true;
            (this.ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = false;
            (this.ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = false;
            (this.ApplicationBar.Buttons[3] as ApplicationBarIconButton).IsEnabled = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if ((this.DataContext as Item).HasChanges && (this.DataContext as Item).GetOriginal() != null)
            {
                    // Revert changes.
                    (this.DataContext as Item).RejectChanges();
            }
            FlipToReadMode();
        }

        private void priorityTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.priorityTxt.Text))
            {
                return;
            }

            this.priorityTxt.Text = new EnumConverter().ConvertBack(
                this.priorityTxt.Text,
                typeof(uint),
                "Priority",
                CultureInfo.CurrentUICulture).ToString();
        }

        private void statusTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.statusTxt.Text))
            {
                return;
            }

            this.statusTxt.Text = new EnumConverter().ConvertBack(
               this.statusTxt.Text,
               typeof(uint),
               "Status",
               CultureInfo.CurrentUICulture).ToString();
        }

        private void tagsTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            // Return if this page is not in view.
            if (!this.isInView)
            {
                return;
            }

            this.tagsChooser.Width = LayoutRoot.ActualWidth;
            this.tagsChooser.Height = LayoutRoot.ActualHeight;
            this.tagsChooser.CurrentItem = this.DataContext as Item;
            this.popupPanel.IsOpen = true;

            // Hide the app bar for now
            this.ApplicationBar.IsVisible = false;
        }

        private void popupDone_Click(object sender, RoutedEventArgs e)
        {
            this.popupPanel.IsOpen = false;
            this.ApplicationBar.IsVisible = true;
            this.tagsTxt.Text = (string)new TagsConverter().Convert((this.DataContext as Item).ID, typeof(string), null, CultureInfo.CurrentUICulture);
        }
    }
}