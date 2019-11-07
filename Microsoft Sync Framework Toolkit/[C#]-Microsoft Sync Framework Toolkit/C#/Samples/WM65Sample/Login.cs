// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SmartDeviceProject1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxUserName.Text.Trim()))
                {
                    MessageBox.Show("UserName cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);

                    return;
                }

                var webRequest = (HttpWebRequest)WebRequest.Create(String.Format(Settings.LoginUrl, textBoxUserName.Text.Trim()));

                var response = (HttpWebResponse)webRequest.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Login failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1);
                    return;
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the GUID
                    using (var reader = new StreamReader(responseStream))
                    {
                        var clientId = reader.ReadToEnd();
                        DataStoreHelper.ListDbFileName = clientId + ".sdf";
                        Settings.ClientId = clientId;
                    }
                }

                if (String.IsNullOrEmpty(Settings.ClientId) || String.IsNullOrEmpty(DataStoreHelper.ListDbFileName))
                {
                    MessageBox.Show("Login failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1);
                    return;
                }



                bool newUser = false;
                if (!File.Exists(DataStoreHelper.ListDbFileName))
                {
                    newUser = true;
                }

                DataStoreHelper.InitializeDataStore();

                var storageHandler = new SqlCeStorageHandler();
                byte[] anchor = storageHandler.GetAnchor();

                if (null == anchor || 0 == anchor.Length)
                {
                    newUser = true;
                }

                if (newUser)
                {
                    // Do an initial sync and open the list view
                    DoInitialSync();
                }
                else
                {
                    var lists = new Lists();
                    lists.ShowDialog();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Unhandled Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void DoInitialSync()
        {
            panel1.Visible = true;

            try
            {
                LabelStatus.Text = "Sync in progress...";
                LabelStatus.Refresh();

                Utility.Sync();

                LabelStatus.Text = "Sync completed!";
                LabelSyncTime.Text = DateTime.Now.ToString();

                var lists = new Lists();
                lists.ShowDialog();

                panel1.Visible = false;
            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Error in sync";
                MessageBox.Show(ex.Message);
            }
        }
    }
}