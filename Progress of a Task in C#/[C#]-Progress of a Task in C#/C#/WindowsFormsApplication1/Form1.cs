using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var progressIndicator = new Progress<MyTaskProgressReport>(ReportProgress);
            await MyMethodAsync(1000, progressIndicator);
        }

        private void ReportProgress(MyTaskProgressReport progress)
        {
            label1.Text = progress.CurrentProgressMessage;
            textBox1.Text = string.Format("{0} out of {1}", progress.CurrentProgressAmount, progress.TotalProgressAmount);
        }

        async Task MyMethodAsync(int sleepTime, IProgress<MyTaskProgressReport> progress)
        {
            int totalAmount = 10000;

            for (int i = 0; i <= totalAmount;)
            {
                await Task.Delay(sleepTime);
                progress.Report(new MyTaskProgressReport { CurrentProgressAmount = i, TotalProgressAmount = totalAmount, CurrentProgressMessage = string.Format("On {0} Message", i) });
                i = i + sleepTime;
            }
        }
    }

    public class MyTaskProgressReport
    {
        //current progress
        public int CurrentProgressAmount { get; set; }
        //total progress
        public int TotalProgressAmount { get; set; }
        //some message to pass to the UI of current progress
        public string CurrentProgressMessage { get; set; }
    }
}
