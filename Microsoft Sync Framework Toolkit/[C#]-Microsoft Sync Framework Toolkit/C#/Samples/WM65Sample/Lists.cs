// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows.Forms;
using DefaultScope;

namespace SmartDeviceProject1
{
    public partial class Lists : Form
    {
        public Lists()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                LabelStatus.Text = "Sync in progress...";
                LabelStatus.Refresh();

                Utility.Sync();

                LabelStatus.Text = "Sync completed!";
                LabelSyncTime.Text = DateTime.Now.ToString();

                RefreshLists();
            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Error in sync";
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshLists()
        {
            var storageHandler = new SqlCeStorageHandler();
            ListBoxLists.DataSource = storageHandler.GetAllLists();
            ListBoxLists.DisplayMember = "Name";
        }

        private void MenuItemNewList_Click(object sender, EventArgs e)
        {
            var newListForm = new AddEditList {Text = "Add List", IsNewItem = true};
            newListForm.ShowDialog();

            RefreshLists();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            var newListForm = 
                new AddEditList {Text = "Edit List", IsNewItem = false, ListItem = ListBoxLists.SelectedItem as List};

            newListForm.ShowDialog();

            RefreshLists();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            var listItem = ListBoxLists.SelectedItem as List;

            if (null != listItem)
            {
                var storageHandler = new SqlCeStorageHandler();
                storageHandler.TombstoneList(listItem);
            }

            RefreshLists();
        }

        private void Lists_Load(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            var items = new Items {ListId = ((List) ListBoxLists.SelectedItem).ID};
            items.ShowDialog();

            RefreshLists();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            SqlCeStorageHandler.CloseConnection();
            if (File.Exists(DataStoreHelper.ListDbFileName))
            {
                File.Delete(DataStoreHelper.ListDbFileName);
            }

            Close();
        }
    }
}