using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// The code below represents a common method for novice developers using SqlClient managed data provider
    /// obtain backend data. 
    /// 
    /// There are several issues
    /// * data operations are done directly in the form were best practices are to place this code into a class or classes
    /// * lack of exception handling and assertions
    /// * duplication of common code
    /// 
    /// In part 2 of this series I will use a class project that will do all the database operations, add exception handling
    /// and assertion along with eliminate code duplication.
    /// 
    /// Part 3 will build on part 2 to show editing and delete operations
    /// 
    /// Part 4 will build on part 3 to provide adding new orders and order details.
    /// 
    /// Part 5 will refactor part 4 and use Entity Framework
    /// </summary>
    public partial class Form1 : Form
    {
        BindingSource bsCustomers = new BindingSource();
        BindingSource bsOrders = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup
        /// * read data from database
        /// * setup relationships between customers and orders
        /// * configure bindingsource for displaying data in the two DataGridView controls
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            var northDataSet = new DataSet();

            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NORTHWND.MDF");

            var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=false;Connect Timeout=30";

            var da = new SqlDataAdapter();
            var ds = new DataSet();

            using (SqlConnection cn = new SqlConnection() { ConnectionString = connectionString })
            {
                cn.Open();


                da = new SqlDataAdapter(@"
                    SELECT 
                        CustomerIdentifier, 
                        CompanyName, 
                        ContactName,
                        ContactTitle,
                        Address,City,
                        Region,
                        PostalCode,
                        Country,
                        Phone,
                        Fax 
                    FROM 
                        Customers",cn);

                da.Fill(ds, "Customers");
                ds.Tables["Customers"].Columns["CustomerIdentifier"].ColumnMapping = MappingType.Hidden;
                ds.Tables["Customers"].Columns["Region"].ColumnMapping = MappingType.Hidden;
                ds.Tables["Customers"].Columns["Phone"].ColumnMapping = MappingType.Hidden;
                ds.Tables["Customers"].Columns["Fax"].ColumnMapping = MappingType.Hidden;

                da = new SqlDataAdapter(@"
                    SELECT 
                        [OrderID],
                        [CustomerIdentifier],
                        [EmployeeID],
                        [OrderDate],
                        [RequiredDate],
                        [ShippedDate],
                        [ShipVia],
                        [Freight],
                        [ShipName],
                        [ShipAddress],
                        [ShipCity],
                        [ShipRegion],
                        [ShipPostalCode],
                        [ShipCountry] 
                    FROM [Orders]", cn);


                da.Fill(ds, "Orders");

                // uncomment to hide order id in detail DataGridView
                //ds.Tables["Orders"].Columns["OrderID"].ColumnMapping = MappingType.Hidden;
                ds.Tables["Orders"].Columns["CustomerIdentifier"].ColumnMapping = MappingType.Hidden;


                // not getting into this relationship
                ds.Tables["Orders"].Columns["EmployeeID"].ColumnMapping = MappingType.Hidden;
                // not getting into this relationship
                ds.Tables["Orders"].Columns["ShipVia"].ColumnMapping = MappingType.Hidden;


                /*
                 * Setup relationship between customers and orders
                 */
                ds.Relations.Add(
                    "CustomersOrders", 
                    ds.Tables["Customers"].Columns["CustomerIdentifier"], 
                    ds.Tables["Orders"].Columns["CustomerIdentifier"]);

                var FreightExpression = "Sum(Child(CustomersOrders).Freight) ";
                ds.Tables["Customers"].Columns.Add(new DataColumn()
                {
                    ColumnName = "Freight",
                    DataType = typeof(Decimal),
                    Expression = FreightExpression
                });

                /*
                 * configure master and detail BindingSource components to allow
                 * traversing of detail rows when traversing customer rows.
                 */
                bsCustomers.DataSource = ds;
                bsCustomers.DataMember = ds.Tables["Customers"].TableName;
                bsOrders.DataSource = bsCustomers;
                bsOrders.DataMember = ds.Relations["CustomersOrders"].RelationName;

                CustomersDataGridView.DataSource = bsCustomers;
                CustomersDataGridView.Columns["Freight"].DefaultCellStyle.Format = "C2";
                CustomersDataGridView.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                OrdersDataGridView.DataSource = bsOrders;
                OrdersDataGridView.Columns["Freight"].DisplayIndex = 10;
                OrdersDataGridView.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                bindingNavigator1.BindingSource = bsCustomers;
            }
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Stub for adding new customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Adding not done in this example");
        }
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"This is where you would remove '{((DataRowView)bsCustomers.Current).Row.Field<string>("CompanyName")}'");
        }
        /// <summary>
        /// Stub for allowing edit of current master row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomersDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                MessageBox.Show($"Edit marker for: '{((DataRowView)bsCustomers.Current).Row.Field<string>("CompanyName")}'");
                e.Handled = true;
            }
        }
        /// <summary>
        /// Simple example of obtaining details for current order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                DisplayCurrentOrderDetails();
            }
        }
        private void displayCurrentOrderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayCurrentOrderDetails();
        }
        /// <summary>
        /// This code is responsible for showing current order details in a child form
        /// </summary>
        private void DisplayCurrentOrderDetails()
        {
            var orderDetailsTable = new DataTable();

            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NORTHWND.MDF");
            var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=True;Connect Timeout=30";

            using (SqlConnection cn = new SqlConnection() { ConnectionString = connectionString })
            {
                var commandText = @"
                    SELECT
	                    [Order Details].OrderID, 
	                    Products.ProductName, 
	                    Categories.CategoryName, 
	                    [Order Details].UnitPrice, 
	                    [Order Details].Quantity, 
	                    [Order Details].Discount
                    FROM
	                    [Order Details] INNER JOIN
	                    Products ON [Order Details].ProductID = Products.ProductID INNER JOIN
	                    Categories ON Products.CategoryID = Categories.CategoryID
                    WHERE        
	                    ([Order Details].OrderID = @OrderID)                    
                    ";

                using (SqlCommand cmd = new SqlCommand() { Connection = cn, CommandText = commandText })
                {
                    cmd.Parameters.AddWithValue("@OrderID", ((DataRowView)bsOrders.Current).Row.Field<int>("OrderId"));
                    cn.Open();
                    orderDetailsTable.Load(cmd.ExecuteReader());
                    orderDetailsTable.Columns["OrderID"].ColumnMapping = MappingType.Hidden;
                    orderDetailsForm f = new orderDetailsForm(orderDetailsTable);

                    f.Text = $"Company: {((DataRowView)bsCustomers.Current).Row.Field<string>("CompanyName")} Order: {((DataRowView)bsOrders.Current).Row.Field<int>("OrderId")}";
                    try
                    {
                        f.ShowDialog();
                    }
                    finally
                    {
                        f.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Simple about dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 f = new AboutBox1();
            try
            {
                f.ShowDialog();
            }
            finally
            {
                f.Dispose();
            }
        }
    }
}
