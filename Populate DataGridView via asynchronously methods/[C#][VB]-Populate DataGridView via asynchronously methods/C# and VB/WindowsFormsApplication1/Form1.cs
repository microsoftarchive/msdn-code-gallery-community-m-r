using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class CustomersForm : Form
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            token = tokenSource.Token;
        }

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token;
        private bool CurrentlyRunning = false;

        private DataTable dt = new DataTable { TableName = "MyTable" };

        public delegate void UpdateDataTableDelegateProgress(int CurrentPosition, int Total, Person[] person);

        public CustomersForm()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }

            InitializeComponent();
            this.FormClosing += CustomersForm_FormClosing;
        }

        private void CustomersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentlyRunning == true)
            {
                if (!token.IsCancellationRequested)
                {
                    tokenSource.Cancel();
                }
            }
        }

        public void UpDateDataTable(int CurrentPosition, int Total, Person[] person)
        {
            Person p;

            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateDataTableDelegateProgress(UpDateDataTable), CurrentPosition, Total, person);
            }
            else
            {
                if (dt.Columns.Count > 0)
                {
                    p = (Person)person.FirstOrDefault();
                    dt.Rows.Add(new object[] { p.Identifier, p.FirstName, p.LastName });
                    ProgressBar1.Value = CurrentPosition;
                    if (CurrentPosition == Total)
                    {
                        MessageBox.Show("Finished loading data");
                    }
                }
            }
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            dt.Columns.Add(new DataColumn { ColumnName = "Identifier", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn { ColumnName = "FirstName", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn { ColumnName = "LastName", DataType = typeof(string) });
        }

        private async void cmdLoad_Click(object sender, EventArgs e)
        {
            ProgressBar1.Visible = true;

            CurrentlyRunning = true;
            CustomersGrid.DataSource = dt;
            DataAccess da = new DataAccess(this);

            ProgressBar1.Maximum = da.RowCount();

            foreach (object[] item in da.LoadCustomers(token))
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await Task.Factory.StartNew(() => System.Threading.Thread.Sleep(1), token);

            }

            if (token.IsCancellationRequested)
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;

                if (chkClearRows.Checked)
                {
                    dt.Rows.Clear();
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (Dialogs.Question("Cancel loading"))
            {
                CurrentlyRunning = false;
                tokenSource.Cancel();
                ProgressBar1.Visible = false;
            }
        }
    }
}