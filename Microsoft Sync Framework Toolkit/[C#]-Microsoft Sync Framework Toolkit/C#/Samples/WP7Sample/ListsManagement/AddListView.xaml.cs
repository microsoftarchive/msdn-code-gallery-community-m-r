// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using Microsoft.Phone.Controls;
using ListsManagement.ViewModels;
using DefaultScope;

namespace ListsManagement
{
    public partial class AddListView : PhoneApplicationPage
    {
        public AddListView()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            if ((this.DataContext  as List).EntityState == Microsoft.Synchronization.ClientServices.IsolatedStorage.OfflineEntityState.Detached)
            {
                SyncContextInstance.Context.AddList(this.DataContext as List);
                SyncContextInstance.Context.SaveChanges();
            } 
            NavigationService.GoBack();
        }
    }
}