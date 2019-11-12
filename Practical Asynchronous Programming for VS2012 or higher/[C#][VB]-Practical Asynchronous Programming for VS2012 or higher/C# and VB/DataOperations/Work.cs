using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataOperations
{
    public class Work
    {
        private OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
        private CancellationTokenSource cts = new CancellationTokenSource();

        public int RecordCount { get; set; }

        public CancellationToken Token { get; set; }

        public DataTable dtPeople { get; set; }

        /// <summary>
        /// Form who called us
        /// </summary>
        public Form Caller { get; set; }

        /// <summary>
        /// Label to show how many rows are processed
        /// </summary>
        public Label Label { get; set; }

        public Work()
        {
            dtPeople = new DataTable { TableName = "PeopleTable" };
            dtPeople.Columns.Add(new DataColumn { ColumnName = "Identifier", DataType = typeof(Int32) });
            dtPeople.Columns.Add(new DataColumn { ColumnName = "FirstName", DataType = typeof(string) });
            dtPeople.Columns.Add(new DataColumn { ColumnName = "LastName", DataType = typeof(string) });

            Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            Builder.DataSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PeopleDatabase.accdb");

            GetRecordCount();
        }

        public async Task GetDataAsync(CancellationToken ct)
        {
            int CurrentRecord = 0;

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

                            int PrimaryKey = await reader.GetFieldValueAsync<int>(0);

                            Object[] ItemArray =
                            {
                                PrimaryKey,
                                await reader.GetFieldValueAsync<string>(1),
                                await reader.GetFieldValueAsync<string>(2)
                            };

                            Caller.Invoke(new EventHandler(delegate(object sender, EventArgs e)
                                {
                                    dtPeople.Rows.Add(ItemArray);

                                    if (Label != null)
                                    {
                                        if (Label.InvokeRequired == false)
                                        {
                                            Label.Text = string.Format("{0} of {1}", CurrentRecord, this.RecordCount);
                                        }
                                    }
                                }), new object[2] { ItemArray, null }
                            );

                            CurrentRecord += 1;
                        }
                    }
                }
            }
        }

        private void GetRecordCount()
        {
            using (OleDbConnection cn = new OleDbConnection(Builder.ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT Count(Identifier) FROM People;", cn))
                {
                    cn.Open();
                    RecordCount = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}