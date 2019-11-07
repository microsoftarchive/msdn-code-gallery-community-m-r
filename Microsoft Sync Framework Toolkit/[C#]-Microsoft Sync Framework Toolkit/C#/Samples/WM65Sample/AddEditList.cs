// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using DefaultScope;
using Microsoft.Samples.Synchronization.ClientServices;

namespace SmartDeviceProject1
{
    public partial class AddEditList : Form
    {
        public bool IsNewItem { get; set; }
        public List ListItem { get; set; } 

        public AddEditList()
        {
            InitializeComponent();
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void MenuItemSave_Click(object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(this.textBoxName.Text))
            {
                MessageBox.Show("List name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);

                return;
            }

            try
            {

                if (IsNewItem)
                {
                    var listItem = new List
                                       {
                                           ID = Guid.NewGuid(),
                                           Name = textBoxName.Text,
                                           Description = textBoxDescription.Text,
                                           CreatedDate = DateTime.Now,
                                           UserID = new Guid(Settings.ClientId),
                                           ServiceMetadata = new OfflineEntityMetadata() { IsTombstone = false }
                                       };

                    var storageHandler = new SqlCeStorageHandler();
                    storageHandler.InsertList(listItem, true);
                    MessageBox.Show("List created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None,
                                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    ListItem.Name = textBoxName.Text.Trim();
                    ListItem.Description = textBoxDescription.Text.Trim();

                    var storageHandler = new SqlCeStorageHandler();
                    storageHandler.UpdateList(ListItem, true);

                    MessageBox.Show("List updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None,
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

        private void AddEditList_Load(object sender, EventArgs e)
        {
            if (!IsNewItem)
            {
                if (null == ListItem)
                {
                    MessageBox.Show("ListItem property cannot be null when editing a list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    this.Close();
                    return;
                }

                textBoxName.Text = ListItem.Name;
                textBoxDescription.Text = ListItem.Description;
            }
        }
    }
}