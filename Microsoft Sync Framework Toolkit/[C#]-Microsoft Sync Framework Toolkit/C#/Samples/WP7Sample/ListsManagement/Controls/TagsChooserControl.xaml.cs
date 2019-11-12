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
using DefaultScope;
using ListsManagement.ViewModels;
using System.ComponentModel;

namespace ListsManagement.Controls
{
    public partial class TagsChooserControl : UserControl, INotifyPropertyChanged
    {
        public IEnumerable<Tag> SelectedTags
        {
            get
            {
                if (CurrentItem != null)
                {
                    // Get the item for which tags are to be reported
                    return from tags in SyncContextInstance.Context.TagCollection
                           join t2i in
                               SyncContextInstance.Context.TagItemMappingCollection.Where((e) => e.ItemID == CurrentItem.ID)
                           on tags.ID equals t2i.TagID
                           select tags;
                }
                else
                {
                    return null;
                }
            }
        }

        public Item CurrentItem
        {
            get;
            set;
        }

        public IEnumerable<Tag> NonSelectedTags
        {
            get
            {
                if (CurrentItem != null)
                {
                    // Get the item for which tags are to be reported
                    return SyncContextInstance.Context.TagCollection.Where(
                        (e) => SyncContextInstance.Context.TagItemMappingCollection.SingleOrDefault
                            (e1 => e1.TagID == e.ID && e1.ItemID == CurrentItem.ID) == null);
                }
                else
                {
                    return null;
                }
            }
        }

        public TagsChooserControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(TagsChooserControl_Loaded);
        }

        void TagsChooserControl_Loaded(object sender, RoutedEventArgs e1)
        {
            this.DataContext = this;
            this.SelectedTagsList.ItemsSource = SelectedTags;
            this.UnAddedTagsList.ItemsSource = NonSelectedTags;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void addNewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (addNewBtn.Content.ToString() == "+")
            {
                // Flip the view.
                this.SelectedTagsList.Visibility = System.Windows.Visibility.Collapsed;
                this.UnAddedTagsList.Visibility = System.Windows.Visibility.Visible;
                addNewBtn.Content = "X";
                NotifyPropertyChanged("NonSelectedTags");
                this.UnAddedTagsList.ItemsSource = NonSelectedTags;
                this.textLbl.Text = "Select Tags";
            }
            else
            {
                // Flip the view.
                this.SelectedTagsList.Visibility = System.Windows.Visibility.Visible;
                this.UnAddedTagsList.Visibility = System.Windows.Visibility.Collapsed;
                addNewBtn.Content = "+";
                NotifyPropertyChanged("SelectedTags");
                this.SelectedTagsList.ItemsSource = SelectedTags;
                this.textLbl.Text = "Tags Selected";
            }
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = (sender as Button).Tag as Tag;
            SyncContextInstance.Context.AddTagItemMapping(new TagItemMapping()
            {
                TagID = tag.ID,
                ItemID = CurrentItem.ID,
                UserID = SettingsViewModel.Instance.UserId,
            });
            SyncContextInstance.Context.SaveChanges();
            this.UnAddedTagsList.ItemsSource = NonSelectedTags;
            NotifyPropertyChanged("NonSelectedTags");
        }

        private void deselect_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = (sender as Button).Tag as Tag;
            SyncContextInstance.Context.DeleteTagItemMapping(
                SyncContextInstance.Context.TagItemMappingCollection.Where
                (e1 => e1.ItemID == CurrentItem.ID && e1.TagID == tag.ID).First());
            SyncContextInstance.Context.SaveChanges();
            this.SelectedTagsList.ItemsSource = SelectedTags;
            NotifyPropertyChanged("SelectedTags");
        }
    }
}
