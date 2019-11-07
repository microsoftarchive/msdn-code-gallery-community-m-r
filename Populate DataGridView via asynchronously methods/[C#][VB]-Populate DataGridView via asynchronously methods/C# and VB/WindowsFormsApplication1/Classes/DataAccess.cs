using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Threading;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Code to read data from a backend ms-access database table,
    /// push data to the calling form and send the primary key as
    /// the current position to the Progressbar where an alternate
    /// would be to not use the primary key but instead have a int
    /// that gets incremented on each read operation since there may
    /// be cases (as with a where condition) that the key is no good
    /// used this way.
    /// </summary>
    /// <remarks>
    /// This code was modeled from the vb.net code yet there are
    /// significant syntax differences that a few things could not be
    /// ported say via a converter e.g. invoking the delegate as apposed
    /// to how it's done in vb.net
    /// </remarks>
    public class DataAccess
    {
        /// <summary>
        /// Connection to the backend database
        /// </summary>
        private OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder { DataSource = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.accdb"), Provider = "Microsoft.ACE.OLEDB.12.0" };
        /// <summary>
        /// Form which called this class
        /// </summary>
        private CustomersForm callingForm;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="f">Calling form</param>
        public DataAccess(CustomersForm f)
        {
            callingForm = f;
        }
        /// <summary>
        /// Primary key value (see class summary above)
        /// </summary>
        public int CurrentIdentifier = 0;
        /// <summary>
        /// Container for returned data back to calling form via a delegate
        /// </summary>
        public Person[] ItemArray;
        /// <summary>
        /// Return a count of rows in People tabe
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// In this demo we are returning all rows but that is usually
        /// not the case, we generally have a where condition so all rows
        /// will not be returned. For this case perhaps set up the select query
        /// below so that this function passes in a parameter for the a where 
        /// condition and use string.format to place the condition into the
        /// where clause.
        /// </remarks>
        public int RowCount()
        {
            int Count = 0;
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ConnectionString })
            {
                using (OleDbCommand cmd = new OleDbCommand { CommandText = "SELECT COUNT(Identifier) As RowCount FROM People;", Connection = cn })
                {
                    cn.Open();
                    Count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return Count;
        }
        /// <summary>
        /// Iterator to pass data back to calling form via a delegate
        /// </summary>
        /// <param name="ct">CancellationToken</param>
        /// <returns></returns>
        public IEnumerable<object[]> LoadCustomers(CancellationToken ct)
        {
            int RecordCount = this.RowCount();
            Person personArray = new Person();

            // prepare our delegate
            CustomersForm.UpdateDataTableDelegateProgress updateDelegate =
                delegate(int CurrentPosition, int Total, Person[] person)
                {
                    callingForm.UpDateDataTable(CurrentPosition, Total, person);
                };

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ConnectionString })
            {
                using (OleDbCommand cmd = new OleDbCommand { CommandText = "SELECT Identifier, FirstName, LastName FROM People;", Connection = cn })
                {
                    cn.Open();

                    OleDbDataReader Reader = cmd.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            personArray.Identifier = Reader.GetFieldValue<int>(0);
                            CurrentIdentifier = personArray.Identifier;
                            personArray.FirstName = Reader.GetFieldValue<string>(1);
                            personArray.LastName = Reader.GetFieldValue<string>(2);

                            ItemArray = new Person[] { personArray };

                            // return data to calling form
                            yield return ItemArray;


                            // invoke calling form delegate
                            updateDelegate(this.CurrentIdentifier, this.RowCount(), ItemArray);
                        }
                    }
                }
            }
        }
    }
}