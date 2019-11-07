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
using ListsSample.Model;

namespace ListsSample
{
    /// <summary>
    /// This class displays a window which allows the user to modify a new or existing list
    /// item
    /// </summary>
    public partial class ModifyItemChildWindow : ChildWindow
    {
        private ModelItem item;

        /// <summary>
        /// Constructor which takes the parent list for a new item
        /// </summary>
        /// <param name="list">Parent list for an item</param>
        public ModifyItemChildWindow(ModelList list)
        {
            InitializeComponent();

            item = ContextModel.Instance.GetNewItem(list);
            NewItem = true;

            this.Loaded += new RoutedEventHandler(ItemModifyChildWindow_Loaded);
        }

        /// <summary>
        /// Constructor for modifying an existing item
        /// </summary>
        /// <param name="item"></param>
        public ModifyItemChildWindow(DefaultScope.Item item)
        {
            InitializeComponent();

            this.item = ContextModel.Instance.GetItem(item);

            this.Loaded += new RoutedEventHandler(ItemModifyChildWindow_Loaded);
        }

        public ModelItem Item
        {
            get
            {
                return item;
            }
        }

        public bool NewItem
        {
            get;
            set;
        }

        /// <summary>
        /// Event handler for when the window is loaded.  DataBinds the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemModifyChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = item;
        }

        /// <summary>
        /// Closes the dialog box with a successful result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Closes the dialog box with a failed result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Sets the priority to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearPriorityButtonClick(object sender, RoutedEventArgs e)
        {
            item.Priority = null;
        }

        /// <summary>
        /// Sets the status to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearStatusButtonClick(object sender, RoutedEventArgs e)
        {
            item.Status = null;
        }

        /// <summary>
        /// Button click to handler for removing a tag from an item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveTagButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;

            DefaultScope.Tag tag = (DefaultScope.Tag)control.Tag;

            item.RemoveTag(tag);
        }

        /// <summary>
        /// Button click handler for adding a tag to an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTagButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;

            DefaultScope.Tag tag = (DefaultScope.Tag)control.Tag;

            item.AddTag(tag);
        }

        /// <summary>
        /// Sets the starts date to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearStartDateButtonClick(object sender, RoutedEventArgs e)
        {
            item.StartDate = null;
        }

        /// <summary>
        /// Sets the end date to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearEndDateButtonClick(object sender, RoutedEventArgs e)
        {
            item.EndDate = null;
        }
    }
}

