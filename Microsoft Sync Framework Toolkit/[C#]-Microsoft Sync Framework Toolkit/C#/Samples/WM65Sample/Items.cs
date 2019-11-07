// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using DefaultScope;

namespace SmartDeviceProject1
{
    public partial class Items : Form
    {
        public Guid ListId { get; set; }

        public Items()
        {
            InitializeComponent();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            var storageHandler = new SqlCeStorageHandler();
            ListBoxItems.DataSource = storageHandler.GetAllItems(ListId);
            ListBoxItems.DisplayMember = "Name";
        }

        private void menuItemNewItem_Click(object sender, EventArgs e)
        {
            var newItemForm = new AddEditItem {Text = "Add Item", IsNewItem = true, ListId = this.ListId};
            newItemForm.ShowDialog();

            RefreshItems();
        }

        private void menuItemEdit_Click(object sender, EventArgs e)
        {
            if (null == ListBoxItems.SelectedItem)
            {
                return;
            }

            var newItemForm = new AddEditItem
                                  {
                                      Text = "Edit Item",
                                      IsNewItem = false,
                                      Item = ListBoxItems.SelectedItem as Item,
                                      ListId = this.ListId
                                  };

            newItemForm.ShowDialog();

            RefreshItems();
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            var item = ListBoxItems.SelectedItem as Item;

            if (null != item)
            {
                var storageHandler = new SqlCeStorageHandler();
                storageHandler.TombstoneItem(item);
            }

            RefreshItems();
        }
    }
}