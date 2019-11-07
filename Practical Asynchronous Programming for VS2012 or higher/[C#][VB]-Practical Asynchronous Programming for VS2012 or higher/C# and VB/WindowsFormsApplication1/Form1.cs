using DataOperations;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Contains code to return simple data from a ms-access table
    /// using Task-based Asynchronous Pattern in a class project
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// At form level for cancel capability
        /// </summary>
        private CancellationTokenSource cts;
       
        public Form1()
        {
            InitializeComponent();
            this.Shown += Form1_Shown;
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            ActiveControl = cmdGetData;
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            ResetData();
            Work w = new Work();
            w.Caller = this;
            w.Label = label1;

            dataGridView1.DataSource = w.dtPeople;

            cts = new CancellationTokenSource();
            Task.Run(() => w.GetDataAsync(cts.Token));

        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
        private void ResetData()
        {
            label1.Text = "0";
            label1.Refresh();

            if (cts != null)
            {
                cts.Cancel();
            }

            if (dataGridView1.DataSource != null)
            {
                var dt = (DataTable)dataGridView1.DataSource;
                dt.Rows.Clear();
                dataGridView1.Refresh();

                Thread.Sleep(1000);
            }
        }

    }
}