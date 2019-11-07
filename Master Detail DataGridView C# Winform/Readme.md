# Master Detail DataGridView C# Winform
## Requires
- Visual Studio 2010
## License
- MIT
## Technologies
- C#
- Windows Forms
- DataGridView
- WinForms
## Topics
- C#
- Windows Forms
- DataGridView
- WinForms
## Updated
- 08/06/2015
## Description

<h1>Introduction</h1>
<p><em><img id="140926" src="140926-2.jpg" alt="" width="588" height="278"></em></p>
<p><em><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In my previous
 article I explained how to<span class="Apple-converted-space">&nbsp;</span></span><a href="https://code.msdn.microsoft.com/DataGridView-Helper-Class-e713de9d" target="_blank" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#0000ff; text-transform:none; text-indent:0px; letter-spacing:normal; text-decoration:none; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Create
 a DatagGridView helper class using C#</a><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff"><span class="Apple-converted-space">&nbsp;</span></span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">I have extended the DatagGridView
 Helper class to create a Master/Detail DatagGridView. My intent is to create a simple and easy program for users. Users can download the code and can customize it depending on their requirements.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Why
 to use a Nested or Hierarchical DataGridView<br style="outline:0px">
</strong><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In real projects like
 Order Management, Production Management and and so on we need to display the data in the hierarchical result.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">For example let's use
 an Order Management project for a restaurant. Let's consider four people going to a restaurant to have their lunch. The waiter from the restaurant will provide a menu card to select the item to place an order. Now in a table the total of 4 people are sitting
 in a restaurant. In restaurant management, usually all the tables have a unique Table Id or name. All 4 people will select their item from the menu and place the order to serve their food. In restaurant management for each order we will create a unique id
 in an Order Master table and all the item details related to the order in the Order Detail table. Let's see an example structure of the order.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Why
 to use a<span class="Apple-converted-space">&nbsp;</span></strong><strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Master
 and Detail Table<br style="outline:0px">
</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">&nbsp;</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">To avoid the duplicate
 data we can use the master Detail table relation to store our data. For example for every order there will be one waiter and one Table so if we didn't use the Master Detail table relation then the output will be like this below.</span></em></p>
<p><em><img id="140927" src="140927-t1.jpg" alt="" width="599" height="178"></em></p>
<p><em><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Here we can see
 that Order No, Table ID, Waiter Name and Order Date have been repeated. To avoid this duplicate data we will create a Master and Detail relation tables. See the following table for Master and Details.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Order
 Master Table</strong><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Here we can see that the
 duplicate data has been stored in a separate table as an Order Master Table.</span></em></p>
<p><em><img id="140928" src="140928-t2.jpg" alt="" width="609" height="45"></em></p>
<p><em><strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Order
 Detail Table<br style="outline:0px">
</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Here we can see
 all the item details of the order in a separate table. But in the detail table we have used the Order Number for a relation to the Master table. Using the relation we can combine both tables and produce the output.</span></em></p>
<p><em><img id="140929" src="140929-t3.jpg" alt="" width="614" height="134"></em></p>
<p><em><strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Normal
 grid result</strong><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">The result can be shown
 without using the Hierarchal grid output. But we must display the duplicate results as in the following.</span></em></p>
<p><em><img id="140930" src="140930-t4.jpg" alt="" width="613" height="126"></em></p>
<p><em><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">We can also merge
 the same data and show the result as in the following table. But the output is not much better and is not easy to view and understand.</span></em></p>
<p><em><img id="140931" src="140931-t5.jpg" alt="" width="617" height="104"></em></p>
<p><em><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Let's see now the
 hierarchical output of the same result.</span></em></p>
<p><em>&nbsp;</em>&nbsp;</p>
<p><em><img id="140932" src="140932-t6.jpg" alt="" width="614" height="179"></em></p>
<p><em><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Now this final
 result looks much better than all the previous. It will be easy to view the master and detail of all records.&nbsp;Here is my sample output of the hierarchical DataGridView.</span></em></p>
<p><em><img id="140933" src="140933-3.jpg" alt="" width="612" height="179"></em></p>
<p><em></em>&nbsp;</p>
<p><em><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In the same manner
 as Order Management in restaurant projects we also have Bill Master and Detail, Account Master and Detail, Item Master and Detail&nbsp;and Inventory Master and Detail. In production projects we will have Production Order Master and Oder Detail, Finished Good
 Receipt Master and Detail, Finished Goods Issue Master and Detail and so on. In the same manner in our all our actual projects we will use the Master and detail relation to display our data.</span></em></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">As I explained in this
 article, I have used and extended my DataGridView helper class to create a Nested DataGridView. You can view my DataGridView helper class details from my article.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In my DGVhelper class
 I have added the following functionality to create the nested grid.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
</p>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px">ImageCoulmn&nbsp; </li><li style="outline:0px">DGVMasterGridClickEvents&nbsp; </li><li style="outline:0px">DGVDetailGridClickEvents&nbsp; </li></ul>
<p><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">The user can use all
 the events, like CellClick, CellContentClick and and so on for both the Master and Detail grid.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">I have created two separate
 list classes to populate the master and detail results. In the form load I called the method to add the details to each list class.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">I have created both a
 Master and Detail DataGridView programmatically (dynamically) using my ShanuDGVHelper Class.</span>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Master
 Grid Setting:</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff"><span class="Apple-converted-space">&nbsp;</span>In
 Form Load I have called this method to create a master DataGridView at runtime. In my code I add the comments before each line to explain its use.</span></em></p>
<h1><span>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// to generate Master Datagridview with your coding
        public void MasterGrid_Initialize()
        {

            //First generate the grid Layout Design
            Helper.ShanuDGVHelper.Layouts(Master_shanuDGV, Color.LightSteelBlue, Color.AliceBlue, Color.WhiteSmoke, false, Color.SteelBlue, false, false, false);

            //Set Height,width and add panel to your selected control
            Helper.ShanuDGVHelper.Generategrid(Master_shanuDGV, pnlShanuGrid, 1000, 600, 10, 10);

            // Color Image Column creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.ImageColumn, &quot;img&quot;, &quot;&quot;, &quot;&quot;, true, 26, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Order_No&quot;, &quot;Order NO&quot;, &quot;Order NO&quot;, true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Table_ID&quot;, &quot;Table ID&quot;, &quot;Table ID&quot;, true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Description&quot;, &quot;Description&quot;, &quot;Description&quot;, true, 320, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Order_DATE&quot;, &quot;Order DATE&quot;, &quot;Order DATE&quot;, true, 140, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Waiter_ID&quot;, &quot;Waiter_ID&quot;, &quot;Waiter_ID&quot;, true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            //Convert the List to DataTable
            DataTable detailTableList = ListtoDataTable(DataClass.OrderDetailBindClass.objDetailDGVBind);
            // Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.
            objshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV, Detail_shanuDGV, Master_shanuDGV.Columns[&quot;img&quot;].Index, ShanuEventTypes.cellContentClick, ShanuControlTypes.ImageColumn, detailTableList, &quot;Order_No&quot;);
            
            // Bind data to DGV.
            Master_shanuDGV.DataSource = DataClass.OrderMasterBindClass.objMasterDGVBind;
        }
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;to&nbsp;generate&nbsp;Master&nbsp;Datagridview&nbsp;with&nbsp;your&nbsp;coding</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MasterGrid_Initialize()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//First&nbsp;generate&nbsp;the&nbsp;grid&nbsp;Layout&nbsp;Design</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Layouts(Master_shanuDGV,&nbsp;Color.LightSteelBlue,&nbsp;Color.AliceBlue,&nbsp;Color.WhiteSmoke,&nbsp;<span class="cs__keyword">false</span>,&nbsp;Color.SteelBlue,&nbsp;<span class="cs__keyword">false</span>,&nbsp;<span class="cs__keyword">false</span>,&nbsp;<span class="cs__keyword">false</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;Height,width&nbsp;and&nbsp;add&nbsp;panel&nbsp;to&nbsp;your&nbsp;selected&nbsp;control</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Generategrid(Master_shanuDGV,&nbsp;pnlShanuGrid,&nbsp;<span class="cs__number">1000</span>,&nbsp;<span class="cs__number">600</span>,&nbsp;<span class="cs__number">10</span>,&nbsp;<span class="cs__number">10</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Color&nbsp;Image&nbsp;Column&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV,&nbsp;ShanuControlTypes.ImageColumn,&nbsp;<span class="cs__string">&quot;img&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">26</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;DataGridViewContentAlignment.MiddleRight,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Order_No&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;NO&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;NO&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">90</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Table_ID&quot;</span>,&nbsp;<span class="cs__string">&quot;Table&nbsp;ID&quot;</span>,&nbsp;<span class="cs__string">&quot;Table&nbsp;ID&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">80</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Description&quot;</span>,&nbsp;<span class="cs__string">&quot;Description&quot;</span>,&nbsp;<span class="cs__string">&quot;Description&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">320</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Order_DATE&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;DATE&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;DATE&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">140</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Waiter_ID&quot;</span>,&nbsp;<span class="cs__string">&quot;Waiter_ID&quot;</span>,&nbsp;<span class="cs__string">&quot;Waiter_ID&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">120</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Convert&nbsp;the&nbsp;List&nbsp;to&nbsp;DataTable</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataTable&nbsp;detailTableList&nbsp;=&nbsp;ListtoDataTable(DataClass.OrderDetailBindClass.objDetailDGVBind);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Image&nbsp;Colum&nbsp;Click&nbsp;Event&nbsp;-&nbsp;In&nbsp;&nbsp;this&nbsp;method&nbsp;we&nbsp;create&nbsp;an&nbsp;event&nbsp;for&nbsp;cell&nbsp;click&nbsp;and&nbsp;we&nbsp;will&nbsp;display&nbsp;the&nbsp;Detail&nbsp;grid&nbsp;with&nbsp;result.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;objshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV,&nbsp;Detail_shanuDGV,&nbsp;Master_shanuDGV.Columns[<span class="cs__string">&quot;img&quot;</span>].Index,&nbsp;ShanuEventTypes.cellContentClick,&nbsp;ShanuControlTypes.ImageColumn,&nbsp;detailTableList,&nbsp;<span class="cs__string">&quot;Order_No&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Bind&nbsp;data&nbsp;to&nbsp;DGV.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Master_shanuDGV.DataSource&nbsp;=&nbsp;DataClass.OrderMasterBindClass.objMasterDGVBind;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Cell
 Click Event:</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff"><span class="Apple-converted-space">&nbsp;</span>I
 have called this the preceding method to create a Cell click event for the master DataGridView.</span></div>
</span>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.

   bjshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV, Detail_shanuDGV, Master_shanuDGV.Columns[&quot;img&quot;].Index, ShanuEventTypes.cellContentClick, ShanuControlTypes.ImageColumn, detailTableList, &quot;Order_No&quot;);
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Image&nbsp;Colum&nbsp;Click&nbsp;Event&nbsp;-&nbsp;In&nbsp;&nbsp;this&nbsp;method&nbsp;we&nbsp;create&nbsp;an&nbsp;event&nbsp;for&nbsp;cell&nbsp;click&nbsp;and&nbsp;we&nbsp;will&nbsp;display&nbsp;the&nbsp;Detail&nbsp;grid&nbsp;with&nbsp;result.</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;bjshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV,&nbsp;Detail_shanuDGV,&nbsp;Master_shanuDGV.Columns[<span class="cs__string">&quot;img&quot;</span>].Index,&nbsp;ShanuEventTypes.cellContentClick,&nbsp;ShanuControlTypes.ImageColumn,&nbsp;detailTableList,&nbsp;<span class="cs__string">&quot;Order_No&quot;</span>);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">This
 event will be used for the master grid image click event. In this event I will get the Order Number and filter the result from DataTabledetail. Display the final Dataview result to the detail DataGridView.</span>&nbsp;</div>
</div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// Image Colukmn Click evnet
        #region Image Colukmn Click Event
        public void DGVMasterGridClickEvents(DataGridView ShanuMasterDGV, DataGridView ShanuDetailDGV, int columnIndexs, ShanuEventTypes eventtype, ShanuControlTypes types,DataTable DetailTable,String FilterColumn)
        {
            MasterDGVs = ShanuMasterDGV;
            DetailDGVs = ShanuDetailDGV;
            gridColumnIndex = columnIndexs;
            DetailgridDT = DetailTable;
            FilterColumnName = FilterColumn;
           
            MasterDGVs.CellContentClick &#43;= new DataGridViewCellEventHandler(masterDGVs_CellContentClick_Event);


        }
        private void masterDGVs_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
        {
           
            DataGridViewImageColumn cols = (DataGridViewImageColumn)MasterDGVs.Columns[0];
         
           // cols.Image = Image.FromFile(ImageName);
            MasterDGVs.Rows[e.RowIndex].Cells[0].Value = Image.FromFile(&quot;expand.png&quot;);

             if (e.ColumnIndex == gridColumnIndex)
             {
                
            
                 if (ImageName == &quot;expand.png&quot;)
                 {
                     DetailDGVs.Visible = true;
                     ImageName = &quot;toggle.png&quot;;
                     // cols.Image = Image.FromFile(ImageName);
                     MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);


                     String Filterexpression = MasterDGVs.Rows[e.RowIndex].Cells[FilterColumnName].Value.ToString();

                     MasterDGVs.Controls.Add(DetailDGVs);

                     Rectangle dgvRectangle = MasterDGVs.GetCellDisplayRectangle(1, e.RowIndex, true);
                     DetailDGVs.Size = new Size(MasterDGVs.Width - 200, 200);
                     DetailDGVs.Location = new Point(dgvRectangle.X, dgvRectangle.Y &#43; 20);


                     DataView detailView = new DataView(DetailgridDT);
                     detailView.RowFilter = FilterColumnName &#43; &quot; = '&quot; &#43; Filterexpression &#43; &quot;'&quot;;
                     if (detailView.Count &lt;= 0)
                     {
                         MessageBox.Show(&quot;No Details Found&quot;);
                     }
                     DetailDGVs.DataSource = detailView;
                 }
                 else
                 {
                     ImageName = &quot;expand.png&quot;;
                     //  cols.Image = Image.FromFile(ImageName);
                     MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);
                     DetailDGVs.Visible = false;
                 }                 
             }
             else
             {
                 DetailDGVs.Visible = false;                
             }
        }
        #endregion
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Image&nbsp;Colukmn&nbsp;Click&nbsp;evnet</span><span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Image&nbsp;Colukmn&nbsp;Click&nbsp;Event</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;DGVMasterGridClickEvents(DataGridView&nbsp;ShanuMasterDGV,&nbsp;DataGridView&nbsp;ShanuDetailDGV,&nbsp;<span class="cs__keyword">int</span>&nbsp;columnIndexs,&nbsp;ShanuEventTypes&nbsp;eventtype,&nbsp;ShanuControlTypes&nbsp;types,DataTable&nbsp;DetailTable,String&nbsp;FilterColumn)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MasterDGVs&nbsp;=&nbsp;ShanuMasterDGV;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs&nbsp;=&nbsp;ShanuDetailDGV;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gridColumnIndex&nbsp;=&nbsp;columnIndexs;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailgridDT&nbsp;=&nbsp;DetailTable;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FilterColumnName&nbsp;=&nbsp;FilterColumn;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MasterDGVs.CellContentClick&nbsp;&#43;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DataGridViewCellEventHandler(masterDGVs_CellContentClick_Event);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;masterDGVs_CellContentClick_Event(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;DataGridViewCellEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataGridViewImageColumn&nbsp;cols&nbsp;=&nbsp;(DataGridViewImageColumn)MasterDGVs.Columns[<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;cols.Image&nbsp;=&nbsp;Image.FromFile(ImageName);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MasterDGVs.Rows[e.RowIndex].Cells[<span class="cs__number">0</span>].Value&nbsp;=&nbsp;Image.FromFile(<span class="cs__string">&quot;expand.png&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(e.ColumnIndex&nbsp;==&nbsp;gridColumnIndex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ImageName&nbsp;==&nbsp;<span class="cs__string">&quot;expand.png&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.Visible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageName&nbsp;=&nbsp;<span class="cs__string">&quot;toggle.png&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;cols.Image&nbsp;=&nbsp;Image.FromFile(ImageName);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value&nbsp;=&nbsp;Image.FromFile(ImageName);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;String&nbsp;Filterexpression&nbsp;=&nbsp;MasterDGVs.Rows[e.RowIndex].Cells[FilterColumnName].Value.ToString();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MasterDGVs.Controls.Add(DetailDGVs);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rectangle&nbsp;dgvRectangle&nbsp;=&nbsp;MasterDGVs.GetCellDisplayRectangle(<span class="cs__number">1</span>,&nbsp;e.RowIndex,&nbsp;<span class="cs__keyword">true</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.Size&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Size(MasterDGVs.Width&nbsp;-&nbsp;<span class="cs__number">200</span>,&nbsp;<span class="cs__number">200</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.Location&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Point(dgvRectangle.X,&nbsp;dgvRectangle.Y&nbsp;&#43;&nbsp;<span class="cs__number">20</span>);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataView&nbsp;detailView&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DataView(DetailgridDT);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;detailView.RowFilter&nbsp;=&nbsp;FilterColumnName&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;=&nbsp;'&quot;</span>&nbsp;&#43;&nbsp;Filterexpression&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(detailView.Count&nbsp;&lt;=&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="cs__string">&quot;No&nbsp;Details&nbsp;Found&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.DataSource&nbsp;=&nbsp;detailView;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageName&nbsp;=&nbsp;<span class="cs__string">&quot;expand.png&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;cols.Image&nbsp;=&nbsp;Image.FromFile(ImageName);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value&nbsp;=&nbsp;Image.FromFile(ImageName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.Visible&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.Visible&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In
 the cell click event if the image column is clicked then I will change the image to Expand and Collapse depending on the selected image name. If the image is selected to Expand then I will make the detail DataGridView visible.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In the cell click event
 I will get something for the current selected Order Number. This order number will be used in &ldquo;DataView&rdquo; to filter only the selected order result. The final result will be bound to the detail DataGridView.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Detail
 Grid Setting:</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff"><span class="Apple-converted-space">&nbsp;</span>In
 the Form load I have called this method to create a detail DataGridView at runtime.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In my code I added comments
 before each line to explain its use.</span></div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// to generate Detail Datagridview with your coding
        public void DetailGrid_Initialize()
        {
            //First generate the grid Layout Design
            Helper.ShanuDGVHelper.Layouts(Detail_shanuDGV, Color.Peru, Color.Wheat, Color.Tan, false, Color.Sienna, false, false, false);
            //Set Height,width and add panel to your selected control
           Helper.ShanuDGVHelper.Generategrid(Detail_shanuDGV, pnlShanuGrid, 800, 200, 10, 10);
            // Color Dialog Column creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Order_Detail_No&quot;, &quot;Detail No&quot;, &quot;Order Detail No&quot;, true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Order_No&quot;, &quot;Order NO&quot;, &quot;Order NO&quot;, true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);
            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Item_Name&quot;, &quot;Item_Name&quot;, &quot;Item_Name&quot;, true,160, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);
            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Notes&quot;, &quot;Notes&quot;, &quot;Notes&quot;, true, 260, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);
            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, &quot;Price&quot;, &quot;Price&quot;, &quot;Price&quot;, true, 70, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);
            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, &quot;QTY&quot;, &quot;QTY&quot;, &quot;QTY&quot;, true, 40, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, &quot;&quot;, &quot;&quot;, Color.Black);
            objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);
            

        }
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;to&nbsp;generate&nbsp;Detail&nbsp;Datagridview&nbsp;with&nbsp;your&nbsp;coding</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;DetailGrid_Initialize()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//First&nbsp;generate&nbsp;the&nbsp;grid&nbsp;Layout&nbsp;Design</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Layouts(Detail_shanuDGV,&nbsp;Color.Peru,&nbsp;Color.Wheat,&nbsp;Color.Tan,&nbsp;<span class="cs__keyword">false</span>,&nbsp;Color.Sienna,&nbsp;<span class="cs__keyword">false</span>,&nbsp;<span class="cs__keyword">false</span>,&nbsp;<span class="cs__keyword">false</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;Height,width&nbsp;and&nbsp;add&nbsp;panel&nbsp;to&nbsp;your&nbsp;selected&nbsp;control</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Generategrid(Detail_shanuDGV,&nbsp;pnlShanuGrid,&nbsp;<span class="cs__number">800</span>,&nbsp;<span class="cs__number">200</span>,&nbsp;<span class="cs__number">10</span>,&nbsp;<span class="cs__number">10</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Color&nbsp;Dialog&nbsp;Column&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Order_Detail_No&quot;</span>,&nbsp;<span class="cs__string">&quot;Detail&nbsp;No&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;Detail&nbsp;No&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">90</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;DataGridViewContentAlignment.MiddleRight,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Order_No&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;NO&quot;</span>,&nbsp;<span class="cs__string">&quot;Order&nbsp;NO&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">80</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Item_Name&quot;</span>,&nbsp;<span class="cs__string">&quot;Item_Name&quot;</span>,&nbsp;<span class="cs__string">&quot;Item_Name&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,<span class="cs__number">160</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Notes&quot;</span>,&nbsp;<span class="cs__string">&quot;Notes&quot;</span>,&nbsp;<span class="cs__string">&quot;Notes&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">260</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleLeft,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;Price&quot;</span>,&nbsp;<span class="cs__string">&quot;Price&quot;</span>,&nbsp;<span class="cs__string">&quot;Price&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">70</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleRight,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;BoundColumn&nbsp;creation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV,&nbsp;ShanuControlTypes.BoundColumn,&nbsp;<span class="cs__string">&quot;QTY&quot;</span>,&nbsp;<span class="cs__string">&quot;QTY&quot;</span>,&nbsp;<span class="cs__string">&quot;QTY&quot;</span>,&nbsp;<span class="cs__keyword">true</span>,&nbsp;<span class="cs__number">40</span>,&nbsp;DataGridViewTriState.True,&nbsp;DataGridViewContentAlignment.MiddleRight,&nbsp;DataGridViewContentAlignment.MiddleCenter,&nbsp;Color.Transparent,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Color.Black);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Detail
 Grid Cell Click Event:</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff"><span class="Apple-converted-space">&nbsp;</span>I
 have called this the preceding method to create a Cell click event for the detail DataGridView.</span></div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);</pre>
<div class="preview">
<pre class="js">objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">This
 event will be used for the detail grid Cell Click event. In the cell click of the Detail grid I will get each cell text and display it in a Messagebox.</span>&nbsp;</div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public void DGVDetailGridClickEvents(DataGridView ShanuDetailDGV)
        {          
            DetailDGVs = ShanuDetailDGV;
            DetailDGVs.CellContentClick &#43;= new DataGridViewCellEventHandler(detailDGVs_CellContentClick_Event);
        }
          private void detailDGVs_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
          {
              MessageBox.Show(&quot;Detail grid Clicked : You clicked on &quot; &#43; DetailDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
          }
 </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;DGVDetailGridClickEvents(DataGridView&nbsp;ShanuDetailDGV)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs&nbsp;=&nbsp;ShanuDetailDGV;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DetailDGVs.CellContentClick&nbsp;&#43;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DataGridViewCellEventHandler(detailDGVs_CellContentClick_Event);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;detailDGVs_CellContentClick_Event(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;DataGridViewCellEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="cs__string">&quot;Detail&nbsp;grid&nbsp;Clicked&nbsp;:&nbsp;You&nbsp;clicked&nbsp;on&nbsp;&quot;</span>&nbsp;&#43;&nbsp;DetailDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<img id="140934" src="140934-4.jpg" alt="" width="610" height="243"></div>
</h1>
<h1>&nbsp;<span>Source Code Files</span></h1>
<ul>
<li>ShanuNestedDataGridView.zip<em><em></em></em> </li></ul>
<h1>More Information</h1>
<p><em></em></p>
