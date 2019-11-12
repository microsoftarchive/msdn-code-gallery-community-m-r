using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Business_Layer;

namespace FileSharing_FTP_Server
{
    public partial class Server : Form
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Server()
        {
            InitializeComponent();

            ServerIPValue.Text = MachineInfo.GetJustIP();            
        }

        /// <summary>
        /// It Establishs the remote object through the network
        /// </summary>
        private void EstablishRemote()
        {
            SoapServerFormatterSinkProvider soap = new SoapServerFormatterSinkProvider();
            BinaryServerFormatterSinkProvider binary = new BinaryServerFormatterSinkProvider();
            soap.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            binary.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            soap.Next = binary;

            Hashtable table = new Hashtable();
            table.Add("port", ServerPortValue.Text);

            TcpChannel channel = new TcpChannel(table, null, soap);

            FTPServer.Logger = Logger;

            ChannelServices.RegisterChannel(channel,false);
            RemotingConfiguration.ApplicationName = "FTPServerAPP";
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FTPServer),"ftpserver.svr", WellKnownObjectMode.Singleton);

            Logger.Text += Environment.NewLine+ "***** TCP Channel has been published... *****";
            
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
        /// It handles the StartServer button's click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartServer_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ServerPortValue.Text))
            {
                MessageBox.Show("Please enter server port.", "Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EstablishRemote();
            ServerPortValue.ReadOnly  = true;
            StartServer.Enabled = false;
            ServerStatusMessage.Text = "Server has been started...";
        }

        /// <summary>
        /// It handles the formclosing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes )
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }


          
        }

    }
}
