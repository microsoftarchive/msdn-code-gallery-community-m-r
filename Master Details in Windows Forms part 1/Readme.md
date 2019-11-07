# Master Details in Windows Forms part 1
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Data Binding
- SQL Server
- Windows Forms
- Data Access
- SQL Server management Studio
- data relationships
## Topics
- Controls
- C#
- Data Binding
- SQL Server
- Class Library
- User Interface
- Windows Forms
- Data Access
- Development
- Patterns and Practices
- DataSet
- DataGridView
- How to
- Windows Forms Controls
- DataTable
- Databases
- application development
- Master/Detail
- data and storage
## Updated
- 08/30/2016
## Description

<h1>Description</h1>
<p><span style="font-size:small">The goal of this series is to show how to create a master-detail view of data which resides in a SQL-Server database using several tables from Microsoft NorthWind database. This is a series because I&rsquo;ve seen many attempts
 failing from developers asking how to rectify botched attempts on Microsoft social forums in several of the forums. This, part 1 will show a working example that works but in the end can be greatly improved upon which I will do in part 2 and part 3 of the
 series. In part 2 a class project will be introduced that will be responsible for all data operations which allows other applications/solutions that use the same database to have code to start with. Part 3 will show how to add detail records (orders table)
 along with adding detail records (the details to the parent table orders). The last part will show a fully functional version using Entity Framework which will have more tables added into the mix e.g. shippers, employees etc.</span></p>
<p><span style="font-size:small">For VB.NET developers see <a href="https://code.msdn.microsoft.com/Basics-of-manuall-creating-aa1a5c3d">
the following code sample</a> for doing master-detail.</span></p>
<p><span style="font-size:small">C# developers, to work with Entity Framework in a forms solution for working with one table see
<a href="https://code.msdn.microsoft.com/Entity-Framework-in-764fa5ba">my code sample</a>. Doing Entity Framwork may not appeal to everyone in the windows desktop developer's community which is why I didn't start with Entity Framework but I highly suggest considering
 working with Entity Framework, it's very powerful. At the current time Entity Framework 6 is best while the next version is very promising, Entity Framework 7 or Core.</span></p>
<p><br>
<span style="font-size:small">What is needed, Visual Studio 2015, SQL-Server Management Studio and of course SQL-Server which can be SQL-Server Express edition</span><br>
<br>
<span style="font-size:small">The first step should start with a good database design, this means you have thoroughly thought-out what tables will be needed, fields within the tables and proper relationships between all tables. Make sure to consider (via business
 rules) which columns in the tables are null-able. It will be wise to setup database diagram for tables in the database too as when you get to using Entity Framework they will be seen as navigation attributes which in turn takes care of joins between your related
 tables.</span><br>
<br>
<span style="font-size:small">It&rsquo;s very rare to have an application/solution that has a single table and there are indeed times when one table is needed yet this is indeed rare.</span><br>
<br>
<span style="font-size:small">Once a database design has been completed and tested by adding test data running queries to ensure you can return needed data you should create a copy of the database either by generating scripts to recreate the database or do
 a backup which you can then restore when a fresh copy is needed.</span><br>
<br>
<span style="font-size:small">If possible, have two databases which the same tables. One is for test, one for production and when developing only work with the test version.</span><br>
<br>
<span style="font-size:small">If using an attached database rather than a server based database, make a backup of the original as in some cases Visual Studio may upgrade your database and now SQL-Server Management Studio cannot read the database.&nbsp;</span><br>
<br>
<span style="font-size:small">Since you have a well-designed database this makes it easy to know your relationships to create relationships in code and this is very important.</span><br>
<br>
<span style="font-size:small">Before moving on to step two, as mentioned at the start this version is not the best way to write code even though it works as in this part all data operations are done within a single form. When inspecting the Visual Studio solution
 note there is a class project named DataOpertions which has stubbed out code to do the same thing as in the form for the project FirstAttemptMasterDetails. There is also another class project named LanguageExtensions which will be used in part two.</span><br>
<br>
<span style="font-size:small">Step two, create a Visual Studio solution, add a new windows form project. Next, in this case for data I detached NorthWind database from my SQL-Server.&nbsp;</span></p>
<p><span style="font-size:small">Note, this version of NorthWind has been modified to have proper auto-incrementing primary keys where the original version lacked proper primary keys. I also updated date fields in the order table to have current dates.</span></p>
<p><span style="font-size:small">Place the attached database into the bin\debug folder of the project. From here create a connection string that points to the database. In the connection string done in this code I use the following which will always point to
 the same folder as the executable.</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;NORTHWND.MDF&quot;);</pre>
<div class="preview">
<pre class="csharp">var&nbsp;fileName&nbsp;=&nbsp;System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,&nbsp;<span class="cs__string">&quot;NORTHWND.MDF&quot;</span>);</pre>
</div>
</div>
</div>
<p><span style="font-size:small">From here using <a href="https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnection%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396">
SqlConnection</a> to access customers and order tables via a <a href="https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqldataadapter(v=vs.110).aspx">
SqlDataAdapter</a> query both customers and orders tables using the fields we want rather then using SELECT * which is bad practice, best to ask for the fields you want.</span></p>
<p><span style="font-size:small">The SqlDataAdapter populates a DataTable into a <a href="https://msdn.microsoft.com/en-us/library/system.data.dataset(v=vs.110).aspx">
DataSet</a> for both customer and order tables.</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var northDataSet = new DataSet();

var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;NORTHWND.MDF&quot;);

var connectionString = $@&quot;Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=false;Connect Timeout=30&quot;;

var da = new SqlDataAdapter();
var ds = new DataSet();

using (SqlConnection cn = new SqlConnection() { ConnectionString = connectionString })
{
    cn.Open();


    da = new SqlDataAdapter(@&quot;
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
            Customers&quot;,cn);</pre>
<div class="preview">
<pre class="js"><span class="js__statement">var</span>&nbsp;northDataSet&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DataSet();&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;fileName&nbsp;=&nbsp;System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,&nbsp;<span class="js__string">&quot;NORTHWND.MDF&quot;</span>);&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;connectionString&nbsp;=&nbsp;$@<span class="js__string">&quot;Data&nbsp;Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated&nbsp;Security=false;Connect&nbsp;Timeout=30&quot;</span>;&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;da&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlDataAdapter();&nbsp;
<span class="js__statement">var</span>&nbsp;ds&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DataSet();&nbsp;
&nbsp;
using&nbsp;(SqlConnection&nbsp;cn&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlConnection()&nbsp;<span class="js__brace">{</span>&nbsp;ConnectionString&nbsp;=&nbsp;connectionString&nbsp;<span class="js__brace">}</span>)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cn.Open();&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;da&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlDataAdapter(@&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SELECT&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CustomerIdentifier,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CompanyName,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ContactName,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ContactTitle,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address,City,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Region,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PostalCode,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Country,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Phone,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fax&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FROM&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Customers&quot;,cn);</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;In the code above I have loaded the customer data. Note that the SQL is formatted in a manner making it easy to read and work with. I find even though the query is simple it's good practice to
 write out the SQL this way as when the query gets very complex it's much easier to work with.</span></div>
<div class="endscriptcode"><span style="font-size:small"><br>
</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">Side note, all queries used were created in SQL-Server Management Studio. What is cool is you select a database, create a new query then using the query designer to assist in creating the query.</span></div>
<div class="endscriptcode"><span style="font-size:small"><br>
</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><img id="158976" src="158976-111.jpg" alt="" width="438" height="409"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">Back to code, the last part was to load the customer data, the same is done to obtain order data and we place the data into the same DataSet.<br>
&nbsp;</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">Once all data has been loaded into the DataSet there is a need to hide columns when the data is displayed in DataGridView controls.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:x-small">
<div class="scriptcode" style="font-size:x-small">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">ds.Tables[&quot;Customers&quot;].Columns[&quot;CustomerIdentifier&quot;].ColumnMapping = MappingType.Hidden;
ds.Tables[&quot;Customers&quot;].Columns[&quot;Region&quot;].ColumnMapping = MappingType.Hidden;
ds.Tables[&quot;Customers&quot;].Columns[&quot;Phone&quot;].ColumnMapping = MappingType.Hidden;
ds.Tables[&quot;Customers&quot;].Columns[&quot;Fax&quot;].ColumnMapping = MappingType.Hidden;</pre>
<div class="preview">
<pre class="js">ds.Tables[<span class="js__string">&quot;Customers&quot;</span>].Columns[<span class="js__string">&quot;CustomerIdentifier&quot;</span>].ColumnMapping&nbsp;=&nbsp;MappingType.Hidden;&nbsp;
ds.Tables[<span class="js__string">&quot;Customers&quot;</span>].Columns[<span class="js__string">&quot;Region&quot;</span>].ColumnMapping&nbsp;=&nbsp;MappingType.Hidden;&nbsp;
ds.Tables[<span class="js__string">&quot;Customers&quot;</span>].Columns[<span class="js__string">&quot;Phone&quot;</span>].ColumnMapping&nbsp;=&nbsp;MappingType.Hidden;&nbsp;
ds.Tables[<span class="js__string">&quot;Customers&quot;</span>].Columns[<span class="js__string">&quot;Fax&quot;</span>].ColumnMapping&nbsp;=&nbsp;MappingType.Hidden;</pre>
</div>
</div>
</div>
<div class="endscriptcode" style="font-size:x-small"><span style="font-size:small">Who wants to see the primary key? so we hide it along with a few other columns. The same is done for the order table.</span></div>
<div class="endscriptcode" style="font-size:x-small"><span style="font-size:small"><br>
</span></div>
<div class="endscriptcode" style="font-size:x-small"></div>
<div class="endscriptcode" style="font-size:x-small"><span style="font-size:small">The next part shows how the tables become related via DataSet.<a href="https://msdn.microsoft.com/en-us/library/ay82azad%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396">Relations</a>.
 In this case the relations are simple but if needed you can get many-to-many with say three or more tables as
<a href="https://msdn.microsoft.com/en-us/library/aa289510(v=vs.71).aspx">shown here</a>&nbsp;(in the link look at it as conceptual as it's done in typed data containers which is a alternate to how I am doing the relations here).</span></div>
<div class="endscriptcode" style="font-size:x-small"></div>
<div class="endscriptcode" style="font-size:x-small"><span style="font-size:small">Relating customers to orders</span></div>
<div class="endscriptcode">
<div class="scriptcode" style="font-size:small">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">ds.Relations.Add(
    &quot;CustomersOrders&quot;, 
    ds.Tables[&quot;Customers&quot;].Columns[&quot;CustomerIdentifier&quot;], 
    ds.Tables[&quot;Orders&quot;].Columns[&quot;CustomerIdentifier&quot;]);</pre>
<div class="preview">
<pre class="js">ds.Relations.Add(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">&quot;CustomersOrders&quot;</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ds.Tables[<span class="js__string">&quot;Customers&quot;</span>].Columns[<span class="js__string">&quot;CustomerIdentifier&quot;</span>],&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ds.Tables[<span class="js__string">&quot;Orders&quot;</span>].Columns[<span class="js__string">&quot;CustomerIdentifier&quot;</span>]);</pre>
</div>
</div>
</div>
<div class="endscriptcode" style="font-size:small">The first argument is the name for the relationship. When you need to access the relationship use the name, in this case CustomerOrders while the alternate is by index which is fine but tells us nothing about
 which relationship we have and when there are many relationships this assist in using the correct one.</div>
<div class="endscriptcode" style="font-size:small">.</div>
<div class="endscriptcode" style="font-size:small"></div>
<div class="endscriptcode" style="font-size:small"></div>
<div class="endscriptcode" style="font-size:small">Next I create a column expression for the customers table that totals frieght for all orders for the selected customer. Sum returns the sum for freight by pointing to the Order table via Child(CustomerOrders
 &quot;our relationship&quot;).Freight &quot;the field to sum&quot;. Later on I format the data (which is type money) in the DataGridView for customer data.</div>
<div class="endscriptcode">
<div class="scriptcode" style="font-size:small">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var FreightExpression = &quot;Sum(Child(CustomersOrders).Freight) &quot;;
ds.Tables[&quot;Customers&quot;].Columns.Add(new DataColumn()
{
    ColumnName = &quot;Freight&quot;,
    DataType = typeof(Decimal),
    Expression = FreightExpression
});</pre>
<div class="preview">
<pre class="csharp">var&nbsp;FreightExpression&nbsp;=&nbsp;<span class="cs__string">&quot;Sum(Child(CustomersOrders).Freight)&nbsp;&quot;</span>;&nbsp;
ds.Tables[<span class="cs__string">&quot;Customers&quot;</span>].Columns.Add(<span class="cs__keyword">new</span>&nbsp;DataColumn()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ColumnName&nbsp;=&nbsp;<span class="cs__string">&quot;Freight&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;DataType&nbsp;=&nbsp;<span class="cs__keyword">typeof</span>(Decimal),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Expression&nbsp;=&nbsp;FreightExpression&nbsp;
});</pre>
</div>
</div>
</div>
<div class="endscriptcode" style="font-size:small">&nbsp;Next two BindingSource components are wired up to properly display the related customer order data.</div>
<div class="endscriptcode">
<div class="scriptcode" style="font-size:small">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">bsCustomers.DataSource = ds;
bsCustomers.DataMember = ds.Tables[&quot;Customers&quot;].TableName;
bsOrders.DataSource = bsCustomers;
bsOrders.DataMember = ds.Relations[&quot;CustomersOrders&quot;].RelationName;</pre>
<div class="preview">
<pre class="js">bsCustomers.DataSource&nbsp;=&nbsp;ds;&nbsp;
bsCustomers.DataMember&nbsp;=&nbsp;ds.Tables[<span class="js__string">&quot;Customers&quot;</span>].TableName;&nbsp;
bsOrders.DataSource&nbsp;=&nbsp;bsCustomers;&nbsp;
bsOrders.DataMember&nbsp;=&nbsp;ds.Relations[<span class="js__string">&quot;CustomersOrders&quot;</span>].RelationName;</pre>
</div>
</div>
</div>
<div class="endscriptcode" style="font-size:small">&nbsp;<span style="font-size:small">The above is the magic for several reasons</span></div>
<div class="endscriptcode">
<ul>
<li><span style="font-size:small">Provides methods to traverse data via a BindingNavigator.</span>
</li><li><span style="font-size:small">Provides search capabilities to the data.</span>
</li><li><span style="font-size:small">Allows us to subscribe to several events for working with underly data.</span>
</li><li><span style="font-size:small">Provided methods to remove, add and edit data. Part 2 I will use language extension methods to assist with these operations.</span>
</li><li><span style="font-size:small">And of course managing the relations which could be done at the DataSet level but the above bullet points makes sense to move to BindingSource components.</span>
</li></ul>
</div>
<div class="endscriptcode"><span style="font-size:small">The next few lines of code configure the DataGridView controls to show data and format a few columns.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">CustomersDataGridView.DataSource = bsCustomers;
CustomersDataGridView.Columns[&quot;Freight&quot;].DefaultCellStyle.Format = &quot;C2&quot;;
CustomersDataGridView.Columns[&quot;Freight&quot;].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

OrdersDataGridView.DataSource = bsOrders;
OrdersDataGridView.Columns[&quot;Freight&quot;].DisplayIndex = 10;
OrdersDataGridView.Columns[&quot;Freight&quot;].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

bindingNavigator1.BindingSource = bsCustomers;</pre>
<div class="preview">
<pre class="js">CustomersDataGridView.DataSource&nbsp;=&nbsp;bsCustomers;&nbsp;
CustomersDataGridView.Columns[<span class="js__string">&quot;Freight&quot;</span>].DefaultCellStyle.Format&nbsp;=&nbsp;<span class="js__string">&quot;C2&quot;</span>;&nbsp;
CustomersDataGridView.Columns[<span class="js__string">&quot;Freight&quot;</span>].DefaultCellStyle.Alignment&nbsp;=&nbsp;DataGridViewContentAlignment.BottomRight;&nbsp;
&nbsp;
OrdersDataGridView.DataSource&nbsp;=&nbsp;bsOrders;&nbsp;
OrdersDataGridView.Columns[<span class="js__string">&quot;Freight&quot;</span>].DisplayIndex&nbsp;=&nbsp;<span class="js__num">10</span>;&nbsp;
OrdersDataGridView.Columns[<span class="js__string">&quot;Freight&quot;</span>].DefaultCellStyle.Alignment&nbsp;=&nbsp;DataGridViewContentAlignment.BottomRight;&nbsp;
&nbsp;
bindingNavigator1.BindingSource&nbsp;=&nbsp;bsCustomers;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">When using the code above&nbsp;each DataGridView will have columns named the same as the fields in the DataTable columns. An alternate is to create in the IDE, columns for each field which needs to
 be displayed, set properties for indication which column matches up to a field in the underlying data.</span></div>
<div class="endscriptcode"><span style="font-size:small">.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">Using the methods I've done here makes it easier to use in other forms and projects where the only dependency is a blank DataGridView.</span>&nbsp;</div>
<div class="endscriptcode">.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">From here we need to write code to allow the user to see order details, here is the code which will display a single order's records. Note we are back into bad practice e.g. using a local connection
 string and coding this in the form. Can you see a difference in the connection strings used in this form? This was done on purpose e.g. some developer figured out a different setting, does it in one connection string but forgets to do so in the other and this
 is where using one connection string pervents this from happening. Can you also determine other reasons why this code needs refactoring even when it works? We will go over this in part 2.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void DisplayCurrentOrderDetails()
{
    var orderDetailsTable = new DataTable();

    var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;NORTHWND.MDF&quot;);
    var connectionString = $@&quot;Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=True;Connect Timeout=30&quot;;

    using (SqlConnection cn = new SqlConnection() { ConnectionString = connectionString })
    {
        var commandText = @&quot;
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
            &quot;;

        using (SqlCommand cmd = new SqlCommand() { Connection = cn, CommandText = commandText })
        {
            cmd.Parameters.AddWithValue(&quot;@OrderID&quot;, ((DataRowView)bsOrders.Current).Row.Field&lt;int&gt;(&quot;OrderId&quot;));
            cn.Open();
            orderDetailsTable.Load(cmd.ExecuteReader());
            orderDetailsTable.Columns[&quot;OrderID&quot;].ColumnMapping = MappingType.Hidden;
            orderDetailsForm f = new orderDetailsForm(orderDetailsTable);

            f.Text = $&quot;Company: {((DataRowView)bsCustomers.Current).Row.Field&lt;string&gt;(&quot;CompanyName&quot;)} Order: {((DataRowView)bsOrders.Current).Row.Field&lt;int&gt;(&quot;OrderId&quot;)}&quot;;
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
}</pre>
<div class="preview">
<pre class="js">private&nbsp;<span class="js__operator">void</span>&nbsp;DisplayCurrentOrderDetails()&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;orderDetailsTable&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DataTable();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;fileName&nbsp;=&nbsp;System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,&nbsp;<span class="js__string">&quot;NORTHWND.MDF&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;connectionString&nbsp;=&nbsp;$@<span class="js__string">&quot;Data&nbsp;Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated&nbsp;Security=True;Connect&nbsp;Timeout=30&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;using&nbsp;(SqlConnection&nbsp;cn&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlConnection()&nbsp;<span class="js__brace">{</span>&nbsp;ConnectionString&nbsp;=&nbsp;connectionString&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;commandText&nbsp;=&nbsp;@&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SELECT&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Order&nbsp;Details].OrderID,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Products.ProductName,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Categories.CategoryName,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Order&nbsp;Details].UnitPrice,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Order&nbsp;Details].Quantity,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Order&nbsp;Details].Discount&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FROM&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Order&nbsp;Details]&nbsp;INNER&nbsp;JOIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Products&nbsp;ON&nbsp;[Order&nbsp;Details].ProductID&nbsp;=&nbsp;Products.ProductID&nbsp;INNER&nbsp;JOIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Categories&nbsp;ON&nbsp;Products.CategoryID&nbsp;=&nbsp;Categories.CategoryID&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WHERE&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([Order&nbsp;Details].OrderID&nbsp;=&nbsp;@OrderID)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&quot;;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;using&nbsp;(SqlCommand&nbsp;cmd&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlCommand()&nbsp;<span class="js__brace">{</span>&nbsp;Connection&nbsp;=&nbsp;cn,&nbsp;CommandText&nbsp;=&nbsp;commandText&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cmd.Parameters.AddWithValue(<span class="js__string">&quot;@OrderID&quot;</span>,&nbsp;((DataRowView)bsOrders.Current).Row.Field&lt;int&gt;(<span class="js__string">&quot;OrderId&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cn.Open();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderDetailsTable.Load(cmd.ExecuteReader());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderDetailsTable.Columns[<span class="js__string">&quot;OrderID&quot;</span>].ColumnMapping&nbsp;=&nbsp;MappingType.Hidden;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderDetailsForm&nbsp;f&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;orderDetailsForm(orderDetailsTable);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.Text&nbsp;=&nbsp;$<span class="js__string">&quot;Company:&nbsp;{((DataRowView)bsCustomers.Current).Row.Field&lt;string&gt;(&quot;</span>CompanyName<span class="js__string">&quot;)}&nbsp;Order:&nbsp;{((DataRowView)bsOrders.Current).Row.Field&lt;int&gt;(&quot;</span>OrderId<span class="js__string">&quot;)}&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.ShowDialog();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">finally</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.Dispose();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">Note the above method is used multiple times and was a refactor.&nbsp;</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">Next, a BindingNavigator is used to provide some cool functionality like traversing rows, adding an removing data. We will override the add and remove buttons and assume control rather than using default methods that are builtin.
 The BindingNavigator has a Items property. Each item has a value as shown below, by setting say the AddNewItem to none then nothing happems when depressing Add icon button in the BindingNavigator. In design mode double click the add icon button to create a
 click event or do it via the property window for the item.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">Add override (part 2 for working code)</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
{
    MessageBox.Show(&quot;Adding not done in this example&quot;);
}</pre>
<div class="preview">
<pre class="js">private&nbsp;<span class="js__operator">void</span>&nbsp;bindingNavigatorAddNewItem_Click(object&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="js__string">&quot;Adding&nbsp;not&nbsp;done&nbsp;in&nbsp;this&nbsp;example&quot;</span>);&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">Delete/remove override, note casting for get company name, we will optimize this and do confirmation and a real removal in part 2.</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
{
    MessageBox.Show($&quot;This is where you would remove '{((DataRowView)bsCustomers.Current).Row.Field&lt;string&gt;(&quot;CompanyName&quot;)}'&quot;);
}</pre>
<div class="preview">
<pre class="js">private&nbsp;<span class="js__operator">void</span>&nbsp;bindingNavigatorDeleteItem_Click(object&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show($<span class="js__string">&quot;This&nbsp;is&nbsp;where&nbsp;you&nbsp;would&nbsp;remove&nbsp;'{((DataRowView)bsCustomers.Current).Row.Field&lt;string&gt;(&quot;</span>CompanyName<span class="js__string">&quot;)}'&quot;</span>);&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
&nbsp;</div>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><img id="158978" src="158978-22222.jpg" alt="" width="455" height="174"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">So that is it for part 1. Part 2 will introduce the items mentions earlier in this code sample. At this point you have a working model that will continue to evolve in the next two parts of this series.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><img id="158979" src="158979-screenshot.jpg" alt="" width="539" height="301">&nbsp;</div>
</span></div>
</div>
</div>
</div>
</div>
</span></div>
<p><span style="font-size:small">Solution (note the xsd and MDF will not be in the next version)</span></p>
<p><img id="158980" src="158980-solution.jpg" alt="" width="302" height="409"></p>
