using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private int RowCount = 0;

        // DataSource for the DataGridView
        private DataTable dtPeople = new DataTable { TableName = "PeopleTable" };

        // No pain creating connection string (see form constructor)
        private OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();

        private CancellationTokenSource cts = new CancellationTokenSource();

        public delegate void ShowStatusDelegate(int value);

        public void ShowStatus(int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ShowStatusDelegate(ShowStatus), value);
            }
            else
            {
                int CurrentPercent = (100 * value) / RowCount;
                this.lblProgress.Text = string.Format("{0} percent done", CurrentPercent);
                progressBar1.Value = value;
            }
        }

        public Form1()
        {
            InitializeComponent();

            dtPeople.Columns.Add(new DataColumn { ColumnName = "Identifier", DataType = typeof(Int32) });
            dtPeople.Columns.Add(new DataColumn { ColumnName = "FirstName", DataType = typeof(string) });
            dtPeople.Columns.Add(new DataColumn { ColumnName = "LastName", DataType = typeof(string) });

            Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            Builder.DataSource = Path.Combine(Application.StartupPath, "PeopleDatabase.accdb");

            GetRecordCount();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = RowCount;

            cmdCancel.Enabled = false;
        }

        /// <summary>
        /// For demonstration only, here we clear the DataGridView and
        /// it's DataSource so you can see we are indeed starting over.
        /// </summary>
        private void ResetData()
        {
            if (dtPeople.Rows.Count > 0)
            {
                dtPeople.Rows.Clear();
                dataGridView1.Refresh();
                Thread.Sleep(1000);
            }
        }

        private void cmdRun_Click(object sender, EventArgs e)
        {
            cmdRun.Enabled = false;
            cmdCancel.Enabled = true;
            ResetData();

            cts = new CancellationTokenSource();
            dataGridView1.DataSource = dtPeople;
            Task.Run(() => GetPeopleData(cts.Token));
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            cmdRun.Enabled = true;
            cmdCancel.Enabled = false;
        }

        private void GetRecordCount()
        {
            using (OleDbConnection cn = new OleDbConnection(Builder.ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT Count(Identifier) FROM People;", cn))
                {
                    cn.Open();
                    RowCount = (int)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Here we use async methods to cycle through the data.
        /// Note although we are using the OleDb class the DataReader
        /// must be casted to a DbDataReader to do the ExecuteReaderAsync
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task GetPeopleData(CancellationToken ct)
        {
            using (OleDbConnection cn = new OleDbConnection(Builder.ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT Identifier, FirstName, LastName FROM People;", cn))
                {
                    await cn.OpenAsync();
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (ct.IsCancellationRequested)
                            {
                                break;
                            }

                            /*
                             * Only reason for the next line rather than placing the data
                             * directly into the Object[] is for passing the key to our
                             * UI update delegate
                             */
                            var PrimaryKey = await reader.GetFieldValueAsync<int>(0);

                            this.ShowStatus(PrimaryKey);

                            Object[] ItemArray =
                            {
                                PrimaryKey,
                                await reader.GetFieldValueAsync<string>(1),
                                await reader.GetFieldValueAsync<string>(2)
                            };

                            /*
                             * This code keeps our UI responsive
                             */
                            Invoke(new EventHandler(delegate(object sender, EventArgs e)
                            {
                                dtPeople.Rows.Add(ItemArray);
                            }), new object[2] { ItemArray, null });

                            /*
                             * If you had elected to do a small number of records the
                             * read operation would happen to fast to see if the UI is
                             * responsive or not so this provides a opportunity to do so.
                             */
                            if (chkUseShortDelay.Checked == true)
                            {
                                Thread.Sleep(200);
                            }
                        }
                    }

                    /*
                     * We could have placed this in a try/finally
                     */
                    cmdRun.Enabled = true;
                }
            }
        }
    }
}