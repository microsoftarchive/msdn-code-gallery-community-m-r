using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business_Layer;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace FileSharing_FTP_Client
{
    public partial class Client : Form
    {

        /// <summary>
        /// It holds the server object
        /// </summary>
        private IFTPServer Server = null;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Client()
        {
            InitializeComponent();
        }

        /// <summary>
        /// It gets the available connection
        /// </summary>
        private bool GetConnection()
        {
            bool connected = true;

            SoapServerFormatterSinkProvider soap = new SoapServerFormatterSinkProvider();
            BinaryServerFormatterSinkProvider binary = new BinaryServerFormatterSinkProvider();
            soap.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            binary.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            soap.Next = binary;

            Hashtable table = new Hashtable();
            table.Add("port", "0");

            TcpChannel channel = new TcpChannel(table, null, soap);
            ChannelServices.RegisterChannel(channel, false);

            try
            {
                Server = (IFTPServer)Activator.GetObject(typeof(IFTPServer), string.Format("tcp://{0}:{1}/FTPServerAPP/ftpserver.svr", ServerIPValue.Text, ServerPortValue.Text));
            }
            catch(Exception ex)
            {
                connected = false;
                EventLogger.Logger(ex, "Client - GetConnection");
            }

            if (Server == null)
            {
                connected = false;
                ChannelServices.UnregisterChannel(channel);
                MessageBox.Show("Cannot Connect to the Server", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return connected ;
            }

            try
            {
                PostedData handler = new PostedData();
                handler.RefreshList += new EventHandler(handler_RefreshList);

                Server.PostedData += new PostedDataHandler(handler.Server_PostData);
                Server.Update += new UpdateHandler(handler.Server_Update);

                Server.Connect(MachineInfo.GetJustIP());

            }
            catch (Exception ex)
            {
                connected = false;
                ChannelServices.UnregisterChannel(channel);
                MessageBox.Show(ex.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return connected;

        }

        /// <summary>
        /// It handles the refreshlist event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void handler_RefreshList(object sender, EventArgs e)
        {
            MessageBox.Show("Please Refresh your list.", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// It handles the ServerPortValue textbox's Keypress event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerPortValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        /// <summary>
        /// It handles the ServerIPValue textbox's Keypress event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerIPValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        /// <summary>
        /// It handles the ShareFolder button's click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShareFolder_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "All|*.*";
                open.Multiselect = true;
                open.Title = "It selects the share folder for the FTP server.";
                if (open.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    List<UploadData> upload = new List<UploadData>();
                    foreach (string file in open.FileNames)
                    {
                        if ((new System.IO.FileInfo(file)).Length > 100000000)
                        {
                            MessageBox.Show("The file '" + file + "' size is more than 100MB, Please select a smaller file.", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                        UploadData data = new UploadData();
                        data.Filename = file.Split('\\')[file.Split('\\').Length - 1];
                        data.File = System.IO.File.ReadAllBytes(file);
                        upload.Add(data);
                    }

                    Server.Upload(MachineInfo.GetJustIP(),upload);
                }
            }
            catch (RemotingException re)
            {
                MessageBox.Show(re.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Refresh_Click(null, null);
            }
        }

        /// <summary>
        /// It handles the StartClient button's click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClient_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ServerPortValue.Text))
            {
                MessageBox.Show("Please enter server port.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ServerIPValue.Text))
            {
                MessageBox.Show("Please enter server IP address.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IPAddress ipAddress;
            if (!IPAddress.TryParse(ServerIPValue.Text, out ipAddress))
            {
                MessageBox.Show("Please enter correct server IP address.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!GetConnection())
                return;

            StartClient.Enabled = false;
            ShareFolder.Enabled = true;
            Refresh.Enabled = true;

        }


        /// <summary>
        /// It handles the formclosing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                else
                {
                    if(Server != null)
                        Server.Disconnect(MachineInfo.GetJustIP());
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = false;
            }

        }


        /// <summary>
        /// It handles the Refresh click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        /// <summary>
        /// It refreshs the list
        /// </summary>
        private void RefreshList()
        {
            if (ServerFileListView.InvokeRequired)
            {
                MethodInvoker invoker = new MethodInvoker(RefreshList);
                ServerFileListView.Invoke(invoker);
            }
            else
            {
                try
                {
                    ServerFileListView.Items.Clear();
                    ServerFileListView.SuspendLayout();
                    List<FileInfo> files = new List<FileInfo>();
                    Server.GetFiles(out files);

                    foreach (FileInfo file in files)
                    {
                        ListViewItem item = new ListViewItem((ServerFileListView.Items.Count + 1).ToString());
                        item.SubItems.Add("FTP Server");
                        item.SubItems.Add(file.Filename.Split('\\')[1]);
                        item.SubItems.Add(file.Size.ToString());
                        ServerFileListView.Items.Add(item);
                    }
                    ServerFileListView.ResumeLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// It handles the double click of ServerFile List view control event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerFileListView_DockChanged(object sender, EventArgs e)
        {
            if (ServerFileListView.SelectedItems.Count < 1)
                return;

            byte[] file;

            Server.Download(MachineInfo.GetJustIP(), ServerFileListView.SelectedItems[0].SubItems[2].Text, out file);

            SaveFileDialog save = new SaveFileDialog();
            save.Title = "It saves the downloaded file.";
            save.SupportMultiDottedExtensions = false;
            save.Filter = "All|*.*";
            save.FileName = ServerFileListView.SelectedItems[0].SubItems[2].Text;
            if (save.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                System.IO.File.WriteAllBytes(save.FileName, file);
                MessageBox.Show(ServerFileListView.SelectedItems[0].SubItems[2].Text +" has been downloaded.", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            save.Dispose();

        }

    }
}
