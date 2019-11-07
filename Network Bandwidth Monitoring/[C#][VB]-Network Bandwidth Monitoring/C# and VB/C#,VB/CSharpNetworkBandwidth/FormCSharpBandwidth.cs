using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace CSharpNetworkBandwidth
{
    public partial class FormCSharpBandwidth : Form
    {
        NetworkTraffic trafficMonitor = null;
        Timer bandwidthCalcTimer = new Timer();
        float lastAmountOfBytesReceived;

        public FormCSharpBandwidth()
        {
            InitializeComponent();

            trafficMonitor = new NetworkTraffic(Process.GetCurrentProcess().Id);
            bandwidthCalcTimer.Interval = 1000;
            bandwidthCalcTimer.Tick += new EventHandler(bandwidthCalcTimer_Tick);
            bandwidthCalcTimer.Enabled = true;
        }

        void bandwidthCalcTimer_Tick(object sender, EventArgs e)
        {
            float currentAmountOfBytesReceived = trafficMonitor.GetBytesReceived();
            totalBandwidthConsumptionLabel.Text = string.Format("Total Bandwidth Consumption: {0} kb", currentAmountOfBytesReceived / 1024);
            currentBandwidthConsumptionLabel.Text = string.Format("Current Bandwidth Consumption: {0} kb/sec", (currentAmountOfBytesReceived - lastAmountOfBytesReceived) / 1024);
            lastAmountOfBytesReceived = currentAmountOfBytesReceived;
        }

        private void refreshBrowserButton_Click(object sender, EventArgs e)
        {
            WebRequest webRequest = WebRequest.Create("http://www.andrealveslima.com.br");
            webRequest.Method = "GET";
            testWebBrowser.DocumentStream = webRequest.GetResponse().GetResponseStream();
        }

        private void downloadSampleFileButton_Click(object sender, EventArgs e)
        {
            string url = @"http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=92ced922-d505-457a-8c9c-84036160639f&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f2%2f9%2f6%2f296AAFA4-669A-46FE-9509-93753F7B0F46%2fVS-KB-Brochure-CSharp-Letter-HiRez.pdf";
            WebClient client = new WebClient();

            client.DownloadFileAsync(new Uri(url), Path.GetTempFileName());
        }
    }
}
