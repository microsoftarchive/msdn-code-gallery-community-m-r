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
    public partial class ArchivedItemsView : PhoneApplicationPage
    {
        public List<Item> ArchivedItems
        {
            get
            {
                // In the sample DB the value for completed is 4. So return anything that is completed or abandoned
                return SyncContextInstance.Context.ItemCollection.Where((e) => e.Status >= 4).OrderByDescending((e) => e.EndDate).ToList();
            }
        }
        public ArchivedItemsView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ArchivedItemsView_Loaded);
        }

        void ArchivedItemsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }
    }
}