using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.IO;

namespace MessageQueuingTool
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox importFileTxt;
        private System.Windows.Forms.TextBox msgTXT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.IO.StreamReader sr = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 10;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }



        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        #endregion


        private void button1_Click(object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(msgLabelTXT.Text))
            {
                errorTXT.Text = "Specify a label";
                return;
            }

            try
            {
                string Id = SendMessage(msgTXT.Text);
                errorTXT.Text = "Message sent successfully: " + Id;
            }
            catch (Exception ex)
            {
                errorTXT.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                msgTXT.Text = "";
                importFileTxt.Text = openFileDialog1.FileName;
                msgLabelTXT.Text = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\")+1);

                msgTXT.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.GetEncoding(comboBox2.Text));
                errorTXT.Text = "";

                //sr = new System.IO.StreamReader(openFileDialog1.FileName);
                //msgTXT.Text = sr.ReadToEnd();
            }
        }

        public string SendMessage(string message)
        {
            MessageQueue queueManager = null;
            System.Messaging.Message messageToSend = new System.Messaging.Message();

            try
            {
                queueManager = new MessageQueue(queueTXT.Text);

                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(messageToSend.BodyStream, System.Text.Encoding.Unicode);
                streamWriter.Write(message);
                streamWriter.Flush();

                MessageQueueTransactionType mqTT = (MessageQueueTransactionType)Enum.Parse(typeof(MessageQueueTransactionType), comboBox1.Text);
                queueManager.Send(messageToSend, msgLabelTXT.Text, mqTT);
            }
            catch (MessageQueueException ex)
            {
                throw;
            }
            finally
            {
                if (queueManager != null) queueManager.Close();
            }
            return messageToSend.Id;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(importFileTxt.Text))
            {
                msgTXT.Text = File.ReadAllText(importFileTxt.Text, Encoding.GetEncoding(comboBox2.Text));
                errorTXT.Text = "";
            }
        }
    }
}
