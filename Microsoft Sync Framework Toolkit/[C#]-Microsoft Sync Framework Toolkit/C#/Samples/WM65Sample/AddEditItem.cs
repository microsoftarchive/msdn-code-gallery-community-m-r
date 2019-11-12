// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using DefaultScope;
using Microsoft.Samples.Synchronization.ClientServices;

namespace SmartDeviceProject1
{
    public partial class AddEditItem : Form
    {
        public AddEditItem()
        {
            InitializeComponent();
        }

        public bool IsNewItem { get; set; }

        public Item Item { get; set; }

        public Guid ListId { get; set; }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEditItem_Load(object sender, EventArgs e)
        {
            LoadPriorityAndStatus();

            if (!IsNewItem)
            {
                if (null == Item)
                {
                    MessageBox.Show("Item property cannot be null when editing an Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    this.Close();
                    return;
                }

                textBoxName.Text = Item.Name;
                textBoxDescription.Text = Item.Description;

                if (null != Item.Priority)
                {
                    comboBoxPriority.SelectedIndex = Item.Priority.Value - 1;
                }

                if (null != Item.Status)
                {
                    comboBoxStatus.SelectedIndex = Item.Status.Value - 1;
                }
            }
        }

        private void LoadPriorityAndStatus()
        {
            var handler = new SqlCeStorageHandler();
            comboBoxPriority.DataSource = handler.GetAllPriority();
            comboBoxPriority.DisplayMember = "Name";

            comboBoxStatus.DataSource = handler.GetAllStatus();
            comboBoxStatus.DisplayMember = "Name";
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.textBoxName.Text))
            {
                MessageBox.Show("Item name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);

                return;
            }

            try
            {
                if (IsNewItem)
                {
                    var item = new Item()
                                   {
                                       Name = textBoxName.Text.Trim(),
                                       Description = textBoxDescription.Text.Trim(),
                                       UserID = new Guid(Settings.ClientId),
                                       ID = Guid.NewGuid(),
                                       ListID = this.ListId,
                                       Priority = ((Priority) comboBoxPriority.SelectedItem).ID,
                                       Status = ((Status) comboBoxStatus.SelectedItem).ID,
                                       ServiceMetadata = new OfflineEntityMetadata() { IsTombstone = false }
                                   };

                    var storageHandler = new SqlCeStorageHandler();
                    storageHandler.InsertItem(item, true);
                    MessageBox.Show("Item created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None,
                                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Item.Name = textBoxName.Text.Trim();
                    Item.Description = textBoxDescription.Text.Trim();
                    Item.Priority = ((Priority) comboBoxPriority.SelectedItem).ID;
                    Item.Status = ((Status) comboBoxStatus.SelectedItem).ID;

                    var storageHandler = new SqlCeStorageHandler();
                    storageHandler.UpdateItem(Item, true);

                    MessageBox.Show("Item updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None,
                                    MessageBoxDefaultButton.Button1);
                }

                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
            }
        }
    }
}