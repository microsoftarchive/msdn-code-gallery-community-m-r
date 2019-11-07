# MVC Dynamic Line Chart Using WEB API, AngularJS, and JQuery
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- ASP.NET MVC
- jQuery
- ASP.NET Web API
- AngularJS
- Line Chart
## Topics
- ASP.NET MVC
- Web API
- AngularJS
- Line Chart
## Updated
- 04/14/2016
## Description

<h1>Introduction</h1>
<p><img id="149963" src="149963-l_1.png" alt="" width="500" height="382"></p>
<p><span>In our previous article we have seen in detail about&nbsp;</span><a href="https://code.msdn.microsoft.com/MVC-Dynamic-Bar-Chart-743e47ce/edit?newSession=True" target="_blank"><span>how to draw Bar Chart in MVC web Application</span></a><span>. In this
 article we will see how to draw Line Chart for MVC application using HTML5 Canvas, JQuery, WEB API, &nbsp;and&nbsp;</span><span>AngularJS</span><span>.</span><br>
<br>
<span>In this series we will see one-by-one in detail starting from:</span></p>
<ol>
<li><a title="MVC Dynamic Bar Chart using WEB API, AngularJS and JQuery" href="https://code.msdn.microsoft.com/MVC-Dynamic-Bar-Chart-743e47ce/edit?newSession=True">MVC Dynamic&nbsp;<strong>Bar Chart</strong>&nbsp;using WEB API, AngularJS and JQuery</a>
</li><li><a href="https://code.msdn.microsoft.com/MVC-Dynamic-Line-Chart-af5e907f">MVC Dynamic Line Chart using WEB API, AngularJS and JQuery
</a></li><li>MVC Dynamic&nbsp;<strong>Pie Chart</strong>&nbsp;using WEB API, AngularJS and JQuery
</li><li>MVC Dynamic&nbsp;<strong>Line</strong>&nbsp;&amp;&nbsp;<strong>Bar&nbsp;</strong>Chart using WEB API, AngularJS and JQuery
</li><li>MVC Dynamic&nbsp;<strong>Donut</strong><strong>&nbsp;Chart</strong>&nbsp;using WEB API, AngularJS and JQuery
</li><li>MVC Dynamic&nbsp;<strong>Bubble Chart</strong>&nbsp;using WEB API, AngularJS and JQuery&nbsp;
</li></ol>
<p><strong>Our Chart Features</strong></p>
<p><img id="149968" src="149968-l_01.png" alt="" width="592" height="72"></p>
<ol>
<li><strong>Chart Source Data:</strong>&nbsp;Using WEB API and AngularJS we will be loading chart data from database to a Combobox. In our JQuery we will be plotting chart details from the Combobox.
</li><li><strong>Chart Number of Category:</strong>&nbsp;Chart Items will be dynamically loaded from database. Here we will plot all the Items in Combobox. It&rsquo;s good to plot less than 12 Items per chart.
</li><li><strong>&nbsp;&nbsp;</strong><strong>Chart Title Text:&nbsp;</strong>User can add their own Chart Title and dynamically change the titles if required. Here in our example we will draw the Title Textbox text at the Bottom of the Chart. (User can redesign
 and customize as per your requirements if needed).<strong>&nbsp;</strong> </li><li><strong>Chart Water Mark Text:</strong>&nbsp;In some cases we need to add our Company name as Water Mark to our Chart. Here in our example we will draw the Water mark Textbox text at the center of the Chart. (User can redesign and customize as per your
 requirements if needed).<strong>&nbsp;</strong> </li><li><strong>Chart Company LOGO:</strong>&nbsp;User can add their own Company logo to the Chart.(Here for sample we have added my own image as a Logo at the Top left corner.( User can redesign and customize as per your requirements if needed.).<strong>&nbsp;</strong>
</li><li><strong>Chart Alert Image Display:</strong>&nbsp;If the &ldquo;Alert On&rdquo; radio button is checked we will display the Alert Image. If the &ldquo;Alert Off&rdquo; radio button is clicked then the Alert Image will not be displayed. In JQuery we have
 declared alertCheckValue = 90; and we check the plot data with this aleartcheckValue and if the plot value is greater than this check value then we will display the alert image in the legend.<strong>&nbsp;</strong>
</li></ol>
<p>What is the use of Alert Image?</p>
<p><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>Let&rsquo;s consider a real time projects. For example we need to display the chart for a Manufacturing factory
 with production result as Good and Bad. For example if production result for each quality value is above 90 we need to display the Alert green Image and if the quality value is below 90 then we need to display the Red Image with label bars.</p>
<p>This Alert Image will be easy to identify each quality result with good or bad. (Here for a sample we have used for quality check and display green and red image, but users can customize as per your requirement and add your own image and logics.).</p>
<p><strong>7.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong><strong>Save Chart as Image:</strong>&nbsp;user can save the chart as Image.</p>
<p><strong>8.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong><strong>Chart Theme :</strong></p>
<p>Here we have created 2 themes, Blue and Green for our Chart. We can see both theme output here. User can also add any numbers of theme as they required.</p>
<p><strong>Blue Theme</strong></p>
<p><img id="149966" src="149966-l_2.png" alt="" width="504" height="383"></p>
<p><strong>Green Theme</strong></p>
<p><img id="149967" src="149967-l_3.png" alt="" width="505" height="387"></p>
<p>In this Article we have 2 parts</p>
<ul>
<li>&nbsp;Chart Item and Value Insert/Update to database, Select Chart Item and Value to Combobox from database using WEB API, AngularJS
</li><li>&nbsp;Using JQuery Draw our own Chart to HTML5 Canvas tag in our MVC page. </li></ul>
<h1>Building the Sample</h1>
<p><strong>Prerequisites</strong></p>
<p><strong>Visual Studio 2015:</strong>&nbsp;You can download it from&nbsp;<a href="https://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs.aspx" target="_blank">here</a>.</p>
<p><span>Description</span></p>
<h1><strong>Code Part</strong></h1>
<p>In Code part we can see 3 Steps</p>
<p><strong>Step 1:</strong>&nbsp;Explains about how to create a sample Database, Table, Stored procedure to select, Insert and Update Chart data to SQL Server.</p>
<p><strong>Step 2:</strong>&nbsp;Explains about how to create a WEB API to get the data and bind the result to MVC page using AngularJS.</p>
<p>&nbsp;<strong>Step 3:</strong>&nbsp;Explains about how to draw our own Chart to our MVC Web application using JQuery.</p>
<h1><strong>Step 1: Script to create Table and Stored Procedure</strong></h1>
<p>We will create a ItemMaster&nbsp;table under the Database &lsquo;ItemsDB.&nbsp;The following is the script to create a database, table and sample insert query. Run this script in your SQL Server. I have used SQL Server 2014. &nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">SQL</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js">USE&nbsp;MASTER&nbsp;&nbsp;&nbsp;
GO&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
--&nbsp;<span class="js__num">1</span>)&nbsp;Check&nbsp;<span class="js__statement">for</span>&nbsp;the&nbsp;Database&nbsp;Exists&nbsp;.If&nbsp;the&nbsp;database&nbsp;is&nbsp;exist&nbsp;then&nbsp;drop&nbsp;and&nbsp;create&nbsp;<span class="js__operator">new</span>&nbsp;DB&nbsp;&nbsp;&nbsp;
IF&nbsp;EXISTS&nbsp;(SELECT&nbsp;[name]&nbsp;FROM&nbsp;sys.databases&nbsp;WHERE&nbsp;[name]&nbsp;=&nbsp;<span class="js__string">'ItemsDB'</span>&nbsp;)&nbsp;&nbsp;&nbsp;
DROP&nbsp;DATABASE&nbsp;ItemsDB&nbsp;&nbsp;&nbsp;
GO&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
CREATE&nbsp;DATABASE&nbsp;ItemsDB&nbsp;&nbsp;&nbsp;
GO&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
USE&nbsp;ItemsDB&nbsp;&nbsp;&nbsp;
GO&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
--&nbsp;<span class="js__num">1</span>)&nbsp;<span class="js__sl_comment">////////////&nbsp;Item&nbsp;Masters&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;
IF&nbsp;EXISTS&nbsp;(&nbsp;SELECT&nbsp;[name]&nbsp;FROM&nbsp;sys.tables&nbsp;WHERE&nbsp;[name]&nbsp;=&nbsp;<span class="js__string">'ItemMaster'</span>&nbsp;)&nbsp;&nbsp;&nbsp;
DROP&nbsp;TABLE&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;
GO&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
CREATE&nbsp;TABLE&nbsp;[dbo].[ItemMaster](&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[ItemID]&nbsp;INT&nbsp;IDENTITY&nbsp;PRIMARY&nbsp;KEY,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[ItemName]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[SaleCount]&nbsp;&nbsp;[varchar](<span class="js__num">10</span>)&nbsp;NOT&nbsp;NULL&nbsp;&nbsp;
)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
--&nbsp;insert&nbsp;sample&nbsp;data&nbsp;to&nbsp;Item&nbsp;Master&nbsp;table&nbsp;&nbsp;&nbsp;
INSERT&nbsp;INTO&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;([ItemName],[SaleCount])&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VALUES&nbsp;(<span class="js__string">'Item1'</span>,<span class="js__string">'100'</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
INSERT&nbsp;INTO&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;([ItemName],[SaleCount])&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VALUES&nbsp;(<span class="js__string">'Item2'</span>,<span class="js__string">'82'</span>)&nbsp;&nbsp;&nbsp;
&nbsp;
INSERT&nbsp;INTO&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;([ItemName],[SaleCount])&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VALUES&nbsp;(<span class="js__string">'Item3'</span>,<span class="js__string">'98'</span>)&nbsp;&nbsp;&nbsp;
&nbsp;
INSERT&nbsp;INTO&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;([ItemName],[SaleCount])&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VALUES&nbsp;(<span class="js__string">'Item4'</span>,<span class="js__string">'34'</span>)&nbsp;&nbsp;&nbsp;
&nbsp;
INSERT&nbsp;INTO&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;([ItemName],[SaleCount])&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VALUES&nbsp;(<span class="js__string">'Item5'</span>,<span class="js__string">'68'</span>)&nbsp;&nbsp;&nbsp;
&nbsp;
select&nbsp;*&nbsp;from&nbsp;ItemMaster&nbsp;&nbsp;
&nbsp;
&nbsp;
--&nbsp;<span class="js__num">1</span>)To&nbsp;Select&nbsp;Item&nbsp;Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Create&nbsp;date&nbsp;:&nbsp;&nbsp;<span class="js__num">2016</span><span class="js__num">-03</span><span class="js__num">-15</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Description&nbsp;:To&nbsp;Select&nbsp;Item&nbsp;Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;<span class="js__num">2016</span><span class="js__num">-03</span><span class="js__num">-15</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
--&nbsp;To&nbsp;Select&nbsp;Item&nbsp;Details&nbsp;
--&nbsp;EXEC&nbsp;USP_Item_Select&nbsp;<span class="js__string">''</span>&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
CREATE&nbsp;PROCEDURE&nbsp;[dbo].[USP_Item_Select]&nbsp;&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@ItemName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VARCHAR(<span class="js__num">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="js__string">''</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
AS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
BEGIN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SELECT&nbsp;&nbsp;ItemName,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SaleCount&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FROM&nbsp;ItemMaster&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WHERE&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemName&nbsp;like&nbsp;&nbsp;@ItemName&nbsp;&#43;<span class="js__string">'%'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Order&nbsp;BY&nbsp;ItemName&nbsp;
END&nbsp;
GO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;
&nbsp;
--&nbsp;<span class="js__num">2</span>)&nbsp;To&nbsp;Insert/Update&nbsp;Item&nbsp;Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Create&nbsp;date&nbsp;:&nbsp;&nbsp;<span class="js__num">2016</span><span class="js__num">-03</span><span class="js__num">-15</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Description&nbsp;:To&nbsp;Insert/Update&nbsp;Item&nbsp;Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;<span class="js__num">2016</span><span class="js__num">-03</span><span class="js__num">-15</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
--&nbsp;To&nbsp;Insert/Update&nbsp;Item&nbsp;Details&nbsp;
--&nbsp;EXEC&nbsp;USP_Item_Edit&nbsp;<span class="js__string">''</span>&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
CREATE&nbsp;PROCEDURE&nbsp;[dbo].[USP_Item_Edit]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@ItemName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VARCHAR(<span class="js__num">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="js__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@SaleCount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VARCHAR(<span class="js__num">10</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="js__string">''</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
AS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
BEGIN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IF&nbsp;NOT&nbsp;EXISTS&nbsp;(SELECT&nbsp;*&nbsp;FROM&nbsp;ItemMaster&nbsp;WHERE&nbsp;ItemName=@ItemName)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;INSERT&nbsp;INTO&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;([ItemName],[SaleCount])&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VALUES&nbsp;(@ItemName,@SaleCount)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select&nbsp;<span class="js__string">'Inserted'</span>&nbsp;as&nbsp;results&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ELSE&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Update&nbsp;ItemMaster&nbsp;SET&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SaleCount=@SaleCount&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WHERE&nbsp;ItemName=@ItemName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select&nbsp;<span class="js__string">'Updated'</span>&nbsp;as&nbsp;results&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select&nbsp;<span class="js__string">'Error'</span>&nbsp;as&nbsp;results&nbsp;
END&nbsp;
&nbsp;
&nbsp;
--&nbsp;<span class="js__num">3</span>)To&nbsp;Max&nbsp;and&nbsp;Min&nbsp;Value&nbsp;
&nbsp;
--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Create&nbsp;date&nbsp;:&nbsp;&nbsp;<span class="js__num">2016</span><span class="js__num">-03</span><span class="js__num">-15</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Description&nbsp;:To&nbsp;Max&nbsp;and&nbsp;Min&nbsp;Value&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;ItemMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;<span class="js__num">2016</span><span class="js__num">-03</span><span class="js__num">-15</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
--&nbsp;To&nbsp;Max&nbsp;and&nbsp;Min&nbsp;Value&nbsp;
--&nbsp;EXEC&nbsp;USP_ItemMaxMin_Select&nbsp;<span class="js__string">''</span>&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
CREATE&nbsp;PROCEDURE&nbsp;[dbo].[USP_ItemMaxMin_Select]&nbsp;&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@ItemName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VARCHAR(<span class="js__num">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="js__string">''</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
AS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
BEGIN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SELECT&nbsp;&nbsp;&nbsp;MIN(convert(int,SaleCount))&nbsp;as&nbsp;MinValue,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MAX(convert(int,SaleCount))&nbsp;as&nbsp;MaxValue&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FROM&nbsp;ItemMaster&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WHERE&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemName&nbsp;like&nbsp;&nbsp;@ItemName&nbsp;&#43;<span class="js__string">'%'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
END&nbsp;
GO&nbsp;
</pre>
</div>
</div>
</div>
<p><strong>Step 2:&nbsp;</strong><strong>Create your MVC Web Application in Visual Studio 2015</strong></p>
<p>After installing our Visual Studio 2015 click Start, then Programs and select&nbsp;<strong>Visual Studio 2015</strong>&nbsp;- Click&nbsp;<strong>Visual Studio 2015</strong>. Click New, then Project, select Web and then select&nbsp;<strong>ASP.NET Web Application</strong>.
 Enter your project name and click OK.</p>
<p><img id="149793" src="149793-2.png" alt="" width="407" height="233"></p>
<p>Select MVC, WEB API and click OK.</p>
<p><img id="149794" src="149794-3.png" alt="" width="431" height="332"></p>
<p>Now we have created our MVC Application as a next step we add our SQL server database as Entity Data Model to our application.</p>
<p><strong>Add Database using ADO.NET Entity Data Model</strong></p>
<p>Right click our project and click Add -&gt; New Item.</p>
<p>Select Data-&gt;Select ADO.NET Entity Data Model&gt; Give the name for our EF and click Add</p>
<p><img id="149795" src="149795-4.png" alt="" width="550" height="300"></p>
<p>Select EF Designer from database and click next.</p>
<p><img id="149796" src="149796-5.png" alt="" width="559" height="306"></p>
<p>Here click New Connection and provide your SQL-Server Server Name and connect to your database.<strong>&nbsp;</strong></p>
<p>Here we can see we have given our SQL server name, Id and PWD and after it connected we have selected the data base as ItemsDB as we have created the Database using my SQL Script.</p>
<p><img id="149797" src="149797-6.png" alt="" width="363" height="451"></p>
<p>Click next and select our Tables and SP need to be used and click finish.</p>
<p><img id="149798" src="149798-8.png" alt="" width="508" height="324"></p>
<p>Once Entity has been created next step we add WEB API to our controller and write function to select/Insert/Update and Delete.</p>
<p><strong>Steps to add our WEB API Controller.</strong></p>
<p>Right Click Controllers folder-&gt; Click Add-&gt; Click Controller.</p>
<p>As we are going to create our WEB API Controller. Select Controller and Add Empty WEB API 2 Controller. Give your Name to Web API controller and click ok.&nbsp;</p>
<p>&nbsp;<img id="149799" src="149799-9.png" alt="" width="524" height="270"></p>
<p>As we all know Web API is a simple and easy to build HTTP Services for Browsers and Mobiles</p>
<p>Web API has four methods as&nbsp;<strong>Get/Post/Put and Delete&nbsp;</strong>where</p>
<p><strong>Get</strong>&nbsp;is to request for the data. (Select)</p>
<p><strong>Post&nbsp;</strong>is to create a data. (Insert)</p>
<p><strong>Put&nbsp;</strong>is to update the data.</p>
<p><strong>Delete</strong>&nbsp;is to delete data.</p>
<p>In our example we will use both&nbsp;<strong>Get</strong>&nbsp;and&nbsp;<strong>Post</strong>&nbsp;as we need to get all image name and descriptions from database and to insert new Image Name and Image Description to database.&nbsp;</p>
<p><strong>Get Method</strong></p>
<p>In our example I have used only Get method as I am using only Stored Procedure. We need to create object for our Entity and write our Get Method to perform Select/Insert/Update and Delete operations.</p>
<p><strong>Select Operation</strong></p>
<p>We use get Method to get all the details of itemMaster table using entity object and we return the result as IEnumerable .We use this method in our AngularJS and bind the result in ComboBox and insert the new chart Item to Database using the Insert Method.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span><span class="cs__keyword">class</span>&nbsp;ItemAPIController&nbsp;:&nbsp;ApiController&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemsDBEntities&nbsp;objapi&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ItemsDBEntities();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;To&nbsp;get&nbsp;all&nbsp;Item&nbsp;chart&nbsp;detaiuls</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;USP_Item_Select_Result&gt;&nbsp;getItemDetails(<span class="cs__keyword">string</span>&nbsp;ItemName)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ItemName&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemName&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;objapi.USP_Item_Select(ItemName).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;To&nbsp;get&nbsp;maximum&nbsp;and&nbsp;Minimum&nbsp;value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;USP_ItemMaxMin_Select_Result&gt;&nbsp;getItemMaxMinDetails(<span class="cs__keyword">string</span>&nbsp;ItemNM)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ItemNM&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemNM&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;objapi.USP_ItemMaxMin_Select(ItemNM).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;To&nbsp;Insert/Update&nbsp;Item&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;insertItem(<span class="cs__keyword">string</span>&nbsp;itemName,&nbsp;<span class="cs__keyword">string</span>&nbsp;SaleCount)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;objapi.USP_Item_Edit(itemName,SaleCount).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<p>Now we have created our Web API Controller Class. Next step we need to create our AngularJs Module and Controller. Let&rsquo;s see how to create our AngularJS Controller. In Visual Studio 2015 it&rsquo;s much easy to add our AngularJs Controller. Let&rsquo;s
 see step by Step on how to create and write our AngularJs Controller.&nbsp;</p>
<h2><strong>Creating AngularJS Controller</strong></h2>
<p>First create a folder inside the Script Folder and we give the folder name as &ldquo;MyAngular&rdquo;</p>
<p>First create a folder inside the Script Folder and I given the folder name as &ldquo;MyAngular&rdquo;</p>
<p>Now add your Angular Controller inside the folder.</p>
<p>Right Click the MyAngular Folder and click Add and New Item &gt; Select Web&nbsp; &gt; Select AngularJs Controller and give name to Controller.We have given my AngularJs Controller as &ldquo;Controller.js&rdquo;</p>
<p><img id="149800" src="149800-10.png" alt="" width="602" height="366"></p>
<p>If the Angular JS package is missing then add the package to your project.</p>
<p>Right Click your MVC project and Click-&gt; Manage NuGet Packages. Search for AngularJs and click Install.</p>
<p><img id="149801" src="149801-11.png" alt="" width="333" height="273"></p>
<p><strong>Modules.js:</strong>&nbsp;Here we will add the reference to the AngularJS JavaScript and create an Angular Module named &ldquo;<strong>AngularJs_Module</strong>&rdquo;.&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__statement">var</span>&nbsp;app;&nbsp;
(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;app&nbsp;=&nbsp;angular.module(<span class="js__string">&quot;AngularJs_Module&quot;</span>,&nbsp;[<span class="js__string">'ngAnimate'</span>]);&nbsp;
<span class="js__brace">}</span>)();&nbsp;
</pre>
</div>
</div>
</div>
<p><strong>Controllers:</strong>&nbsp;In AngularJS Controller I have done all the business logic and returned the data from Web API to our MVC HTML page.</p>
<p><strong>1. Variable declarations<br>
</strong><br>
Firstly, we declared all the local variables need to be used.&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js">app.controller(<span class="js__string">&quot;AngularJs_Controller&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;($scope,&nbsp;$timeout,&nbsp;$rootScope,&nbsp;$window,&nbsp;$http,&nbsp;FileUploadService)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.date&nbsp;=&nbsp;<span class="js__operator">new</span><span class="js__object">Date</span>();&nbsp;
<span class="js__sl_comment">//&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__statement">var</span>&nbsp;app;&nbsp;
(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;app&nbsp;=&nbsp;angular.module(<span class="js__string">&quot;RESTClientModule&quot;</span>,&nbsp;[<span class="js__string">'ngAnimate'</span>]);&nbsp;
<span class="js__brace">}</span>)();&nbsp;
&nbsp;
&nbsp;
app.controller(<span class="js__string">&quot;AngularJs_Controller&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;($scope,&nbsp;$timeout,&nbsp;$rootScope,&nbsp;$window,&nbsp;$http)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.date&nbsp;=&nbsp;<span class="js__operator">new</span><span class="js__object">Date</span>();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.MyName&nbsp;=&nbsp;<span class="js__string">&quot;shanu&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.sItemName&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemCount&nbsp;=&nbsp;<span class="js__num">5</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.selectedItem&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.chartTitle&nbsp;=&nbsp;<span class="js__string">&quot;SHANU&nbsp;Bar&nbsp;Chart&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.waterMark&nbsp;=&nbsp;<span class="js__string">&quot;SHANU&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.ItemValues&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.ItemNames&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showItemAdd&nbsp;=&nbsp;false;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.minsnew&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.maxnew&nbsp;=<span class="js__num">0</span>;&nbsp;
</pre>
</div>
</div>
</div>
<p><strong>2. Methods</strong></p>
<p><strong>Select Method&nbsp;</strong></p>
<p><img id="149969" src="149969-l_04.png" alt="" width="558" height="95"></p>
<p>Here we get all the data from WEB API and bind the result to our ComboBox and we have used another method to get the maximum and Minimum Value of Chart Value and bind in hidden field.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;This&nbsp;method&nbsp;is&nbsp;to&nbsp;get&nbsp;all&nbsp;the&nbsp;Item&nbsp;&nbsp;Details&nbsp;to&nbsp;bind&nbsp;in&nbsp;Combobox&nbsp;for&nbsp;plotting&nbsp;in&nbsp;Graph</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;selectuerRoleDetails($scope.sItemName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;This&nbsp;method&nbsp;is&nbsp;to&nbsp;get&nbsp;all&nbsp;the&nbsp;Item&nbsp;&nbsp;Details&nbsp;to&nbsp;bind&nbsp;in&nbsp;Combobox&nbsp;for&nbsp;plotting&nbsp;in&nbsp;Graph</span><span class="js__operator">function</span>&nbsp;selectuerRoleDetails(ItemName)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/ItemAPI/getItemDetails/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;ItemName:&nbsp;ItemName&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemData&nbsp;=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemCount&nbsp;=&nbsp;$scope.itemData.length;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.selectedItem&nbsp;=&nbsp;$scope.itemData[<span class="js__num">0</span>].SaleCount;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/ItemAPI/getItemMaxMinDetails/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;ItemNM:&nbsp;$scope.sItemName&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemDataMaxMin&nbsp;=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.minsnew&nbsp;=&nbsp;$scope.itemDataMaxMin[<span class="js__num">0</span>].MinValue;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.maxnew&nbsp;=&nbsp;$scope.itemDataMaxMin[<span class="js__num">0</span>].MaxValue;&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p><strong>Insert Method</strong></p>
<p>User can Insert or update Chart Item value by clicking Add Chart Item Details. After validation we pass the Chart Item name and Value to WEB API method to insert in to our database.</p>
<p><img id="149970" src="149970-l_4.png" alt="" width="555" height="193"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//Save&nbsp;File</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.saveDetails&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.IsFormSubmitted&nbsp;=&nbsp;true;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Message&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.ItemNames&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;Item&nbsp;Name&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">if</span>&nbsp;($scope.ItemValues&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;Item&nbsp;Value&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">if</span>&nbsp;($scope.IsFormValid)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert($scope.ItemNames);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/ItemAPI/insertItem/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;itemName:&nbsp;$scope.ItemNames,&nbsp;SaleCount:&nbsp;$scope.ItemValues&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.CharDataInserted&nbsp;=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert($scope.CharDataInserted);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selectuerRoleDetails($scope.sItemName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Message&nbsp;=&nbsp;<span class="js__string">&quot;All&nbsp;the&nbsp;fields&nbsp;are&nbsp;required.&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>;&nbsp;
&nbsp;
<span class="js__brace">}</span>);&nbsp;
</pre>
</div>
</div>
</div>
<h1><strong>Step 3: To draw our Chart using JQuery to our MVC page Canvas Tag</strong></h1>
<p>Here we will see in detail about how to draw our Line Chart on our MVC Web Application using JQuery.</p>
<p>Inside the java Script declare the global variables and initialize the Canvas in JavaScript. In the code I have used comments to easily understand the declarations.</p>
<p><strong>Script Detail Explanations</strong></p>
<p><strong>Script Global variable</strong></p>
<h2><strong>Chart Category Color Add</strong></h2>
<p>Adding the Chart category colors to array .Here we have fixed to 12 colors and 12 data&rsquo;s to add with Line Chart. If you want you can add more from here. Here we have 2 set of color combination one with Green base and one with Blue base. User can add
 as per your requirement here.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js"><span class="js__statement">var</span>&nbsp;pirChartColor&nbsp;=&nbsp;[<span class="js__string">&quot;#6CBB3C&quot;</span>,&nbsp;<span class="js__string">&quot;#F87217&quot;</span>,&nbsp;<span class="js__string">&quot;#EAC117&quot;</span>,&nbsp;<span class="js__string">&quot;#EDDA74&quot;</span>,&nbsp;<span class="js__string">&quot;#CD7F32&quot;</span>,&nbsp;<span class="js__string">&quot;#CCFB5D&quot;</span>,&nbsp;<span class="js__string">&quot;#FDD017&quot;</span>,&nbsp;<span class="js__string">&quot;#9DC209&quot;</span>,&nbsp;<span class="js__string">&quot;#E67451&quot;</span>,&nbsp;<span class="js__string">&quot;#728C00&quot;</span>,&nbsp;<span class="js__string">&quot;#617C58&quot;</span>,&nbsp;<span class="js__string">&quot;#64E986&quot;</span>];&nbsp;<span class="js__sl_comment">//&nbsp;green&nbsp;Color&nbsp;Combinations</span><span class="js__sl_comment">//&nbsp;var&nbsp;pirChartColor&nbsp;=&nbsp;[&quot;#3090C7&quot;,&nbsp;&quot;#BDEDFF&quot;,&nbsp;&quot;#78C7C7&quot;,&nbsp;&quot;#736AFF&quot;,&nbsp;&quot;#7FFFD4&quot;,&nbsp;&quot;#3EA99F&quot;,&nbsp;&quot;#EBF4FA&quot;,&nbsp;&quot;#F9B7FF&quot;,&nbsp;&quot;#8BB381&quot;,&nbsp;&quot;#BDEDFF&quot;,&nbsp;&quot;#B048B5&quot;,&nbsp;&quot;#4E387E&quot;];&nbsp;//&nbsp;Blue&nbsp;Color&nbsp;Combinations</span><span class="js__sl_comment">//This&nbsp;method&nbsp;will&nbsp;be&nbsp;used&nbsp;to&nbsp;check&nbsp;for&nbsp;user&nbsp;selected&nbsp;Color&nbsp;Theme&nbsp;and&nbsp;Change&nbsp;the&nbsp;color</span><span class="js__operator">function</span>&nbsp;ChangeChartColor()&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($(<span class="js__string">'#rdoColorGreen:checked'</span>).val()&nbsp;==&nbsp;<span class="js__string">&quot;Green&nbsp;Theme&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pirChartColor&nbsp;=&nbsp;[<span class="js__string">&quot;#6CBB3C&quot;</span>,&nbsp;<span class="js__string">&quot;#F87217&quot;</span>,&nbsp;<span class="js__string">&quot;#EAC117&quot;</span>,&nbsp;<span class="js__string">&quot;#EDDA74&quot;</span>,&nbsp;<span class="js__string">&quot;#CD7F32&quot;</span>,&nbsp;<span class="js__string">&quot;#CCFB5D&quot;</span>,&nbsp;<span class="js__string">&quot;#FDD017&quot;</span>,&nbsp;<span class="js__string">&quot;#9DC209&quot;</span>,&nbsp;<span class="js__string">&quot;#E67451&quot;</span>,&nbsp;<span class="js__string">&quot;#728C00&quot;</span>,&nbsp;<span class="js__string">&quot;#617C58&quot;</span>,&nbsp;<span class="js__string">&quot;#64E986&quot;</span>];&nbsp;<span class="js__sl_comment">//&nbsp;green&nbsp;Color&nbsp;Combinations</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lineColor&nbsp;=&nbsp;<span class="js__string">&quot;#3090C7&quot;</span>;&nbsp;<span class="js__sl_comment">//&nbsp;Blue&nbsp;Color&nbsp;for&nbsp;Line</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lineOuterCircleColor&nbsp;=&nbsp;<span class="js__string">&quot;#6CBB3C&quot;</span>;&nbsp;<span class="js__sl_comment">//&nbsp;Green&nbsp;Color&nbsp;for&nbsp;Outer&nbsp;Circle</span><span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pirChartColor&nbsp;=&nbsp;[<span class="js__string">&quot;#3090C7&quot;</span>,&nbsp;<span class="js__string">&quot;#BDEDFF&quot;</span>,&nbsp;<span class="js__string">&quot;#78C7C7&quot;</span>,&nbsp;<span class="js__string">&quot;#736AFF&quot;</span>,&nbsp;<span class="js__string">&quot;#7FFFD4&quot;</span>,&nbsp;<span class="js__string">&quot;#3EA99F&quot;</span>,&nbsp;<span class="js__string">&quot;#EBF4FA&quot;</span>,&nbsp;<span class="js__string">&quot;#F9B7FF&quot;</span>,&nbsp;<span class="js__string">&quot;#8BB381&quot;</span>,&nbsp;<span class="js__string">&quot;#BDEDFF&quot;</span>,&nbsp;<span class="js__string">&quot;#B048B5&quot;</span>,&nbsp;<span class="js__string">&quot;#4E387E&quot;</span>];&nbsp;<span class="js__sl_comment">//&nbsp;Blue&nbsp;Color&nbsp;Combinations</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lineColor&nbsp;=&nbsp;<span class="js__string">&quot;#F87217&quot;</span>;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Orange&nbsp;Color&nbsp;for&nbsp;the&nbsp;Line</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lineOuterCircleColor&nbsp;=&nbsp;<span class="js__string">&quot;#F70D1A&nbsp;&quot;</span>;&nbsp;<span class="js__sl_comment">//&nbsp;Red&nbsp;Color&nbsp;for&nbsp;the&nbsp;outer&nbsp;circle</span><span class="js__brace">}</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h2><strong>Draw Legend:</strong></h2>
<p><strong>&nbsp;</strong>If the Show Legend radio button is clicked then we draw a Legend for our Chart item inside Canvas Tag and also in this method we check to display Alert Image or not.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;This&nbsp;function&nbsp;is&nbsp;used&nbsp;to&nbsp;draw&nbsp;the&nbsp;Legend</span><span class="js__operator">function</span>&nbsp;drawLengends()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#7F462C&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillRect(rect.startX,&nbsp;rect.startY,&nbsp;rect.w,&nbsp;rect.h);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Drawing&nbsp;Inner&nbsp;White&nbsp;color&nbsp;Rectange&nbsp;with&nbsp;in&nbsp;Above&nbsp;brown&nbsp;rectangle&nbsp;to&nbsp;plot&nbsp;all&nbsp;the&nbsp;Lables&nbsp;with&nbsp;color,Text&nbsp;and&nbsp;Value.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#FFFFFF&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rectInner.startX&nbsp;=&nbsp;rect.startX&nbsp;&#43;&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rectInner.startY&nbsp;=&nbsp;rect.startY&nbsp;&#43;&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rectInner.w&nbsp;=&nbsp;rect.w&nbsp;-&nbsp;<span class="js__num">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rectInner.h&nbsp;=&nbsp;rect.h&nbsp;-&nbsp;<span class="js__num">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillRect(rectInner.startX,&nbsp;rectInner.startY,&nbsp;rectInner.w,&nbsp;rectInner.h);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;labelBarX&nbsp;=&nbsp;rectInner.startX&nbsp;&#43;&nbsp;<span class="js__num">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;labelBarY&nbsp;=&nbsp;rectInner.startY&nbsp;&#43;&nbsp;<span class="js__num">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;labelBarWidth&nbsp;=&nbsp;rectInner.w&nbsp;-&nbsp;<span class="js__num">10</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;labelBarHeight&nbsp;=&nbsp;(rectInner.h&nbsp;/&nbsp;noOfPlots)&nbsp;-&nbsp;<span class="js__num">5</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;colorval&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;here&nbsp;to&nbsp;draw&nbsp;all&nbsp;the&nbsp;rectangle&nbsp;for&nbsp;Lables&nbsp;with&nbsp;Image&nbsp;display</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#DropDownList1&nbsp;option'</span>).each(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;pirChartColor[colorval];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillRect(labelBarX,&nbsp;labelBarY,&nbsp;labelBarWidth,&nbsp;labelBarHeight);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Here&nbsp;we&nbsp;check&nbsp;for&nbsp;the&nbsp;rdoAlert&nbsp;Status&nbsp;is&nbsp;On&nbsp;-&nbsp;If&nbsp;the&nbsp;Alert&nbsp;is&nbsp;on&nbsp;then&nbsp;we&nbsp;display&nbsp;the&nbsp;Alert&nbsp;Image&nbsp;as&nbsp;per&nbsp;the&nbsp;&nbsp;Alert&nbsp;check&nbsp;value.</span><span class="js__statement">if</span>&nbsp;($(<span class="js__string">'#rdoAlaramOn:checked'</span>).val()&nbsp;==&nbsp;<span class="js__string">&quot;Alert&nbsp;On&quot;</span>)&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;Here&nbsp;we&nbsp;can&nbsp;see&nbsp;fo&nbsp;ever&nbsp;chart&nbsp;value&nbsp;we&nbsp;check&nbsp;with&nbsp;the&nbsp;condition&nbsp;.we&nbsp;have&nbsp;initially&nbsp;declare&nbsp;the&nbsp;alertCheckValue&nbsp;as&nbsp;300.</span><span class="js__sl_comment">//so&nbsp;if&nbsp;the&nbsp;Chart&nbsp;Plot&nbsp;value&nbsp;is&nbsp;Greater&nbsp;then&nbsp;or&nbsp;equal&nbsp;to&nbsp;the&nbsp;check&nbsp;value&nbsp;then&nbsp;we&nbsp;display&nbsp;the&nbsp;Green&nbsp;Image&nbsp;else&nbsp;we&nbsp;display&nbsp;the&nbsp;Red&nbsp;Image.</span><span class="js__sl_comment">//user&nbsp;can&nbsp;change&nbsp;this&nbsp;to&nbsp;your&nbsp;requiremnt&nbsp;if&nbsp;needed.This&nbsp;is&nbsp;optioan&nbsp;function&nbsp;for&nbsp;the&nbsp;Pie&nbsp;Chart.</span><span class="js__statement">if</span>&nbsp;(<span class="js__function">parseInt</span>($(<span class="js__operator">this</span>).val())&nbsp;&gt;=&nbsp;alertCheckValue)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.drawImage(greenImage,&nbsp;labelBarX,&nbsp;labelBarY&nbsp;&#43;&nbsp;(labelBarHeight&nbsp;/&nbsp;<span class="js__num">3</span>)&nbsp;-&nbsp;<span class="js__num">4</span>,&nbsp;imagesize,&nbsp;imagesize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.drawImage(redImage,&nbsp;labelBarX,&nbsp;labelBarY&nbsp;&#43;&nbsp;(labelBarHeight&nbsp;/&nbsp;<span class="js__num">3</span>)&nbsp;-&nbsp;<span class="js__num">4</span>,&nbsp;imagesize,&nbsp;imagesize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__sl_comment">//Draw&nbsp;the&nbsp;Pie&nbsp;Chart&nbsp;Label&nbsp;text&nbsp;and&nbsp;Value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#000000&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'10pt&nbsp;Calibri'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillText($(<span class="js__operator">this</span>).text(),&nbsp;labelBarX&nbsp;&#43;&nbsp;imagesize&nbsp;&#43;&nbsp;<span class="js__num">2</span>,&nbsp;labelBarY&nbsp;&#43;&nbsp;(labelBarHeight&nbsp;/&nbsp;<span class="js__num">2</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;To&nbsp;Increment&nbsp;and&nbsp;draw&nbsp;the&nbsp;next&nbsp;bar&nbsp;,label&nbsp;Text&nbsp;and&nbsp;Alart&nbsp;Image.</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;labelBarY&nbsp;=&nbsp;labelBarY&nbsp;&#43;&nbsp;labelBarHeight&nbsp;&#43;&nbsp;<span class="js__num">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;&nbsp;labelTextYXVal&nbsp;=&nbsp;labelBarY&nbsp;&#43;&nbsp;labelBarHeight&nbsp;-&nbsp;4;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;colorval&nbsp;=&nbsp;colorval&nbsp;&#43;&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h2><strong>Draw Chart:</strong></h2>
<p><strong>&nbsp;</strong>This is our main Function .Here we get all the details to draw our Line Chart. In this function we will draw Chart Titile, Chart Water Mark text, Chart Logo Image and finally call draw Line chart Method to draw our Line chart inside
 Canvas Tag.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">JavaScript</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;This&nbsp;is&nbsp;the&nbsp;main&nbsp;function&nbsp;to&nbsp;darw&nbsp;the&nbsp;Charts</span><span class="js__operator">function</span>&nbsp;drawChart()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ChangeChartColor();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;asign&nbsp;the&nbsp;images&nbsp;path&nbsp;for&nbsp;both&nbsp;Alert&nbsp;images</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;greenImage.src&nbsp;=&nbsp;<span class="js__string">'../images/Green.png'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;redImage.src&nbsp;=&nbsp;<span class="js__string">'../images/Red.png'</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LogoImage.src&nbsp;=&nbsp;<span class="js__string">'../images/shanu.jpg'</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Get&nbsp;the&nbsp;minumum&nbsp;and&nbsp;maximum&nbsp;value.here&nbsp;i&nbsp;have&nbsp;used&nbsp;the&nbsp;hidden&nbsp;filed&nbsp;from&nbsp;code&nbsp;behind&nbsp;wich&nbsp;will&nbsp;stored&nbsp;the&nbsp;Maximum&nbsp;and&nbsp;Minimum&nbsp;value&nbsp;of&nbsp;the&nbsp;Drop&nbsp;down&nbsp;list&nbsp;box.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minDataVal&nbsp;=&nbsp;$(<span class="js__string">'input:text[name=hidListMin]'</span>).val();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxDataVal&nbsp;=&nbsp;$(<span class="js__string">'input:text[name=hidListMax]'</span>).val();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Total&nbsp;no&nbsp;of&nbsp;plots&nbsp;we&nbsp;are&nbsp;going&nbsp;to&nbsp;draw.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;noOfPlots&nbsp;=&nbsp;$(<span class="js__string">&quot;#DropDownList1&nbsp;option&quot;</span>).length;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxValdivValue&nbsp;=&nbsp;<span class="js__object">Math</span>.round((maxDataVal&nbsp;/&nbsp;noOfPlots));&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//storing&nbsp;the&nbsp;Canvas&nbsp;Context&nbsp;to&nbsp;local&nbsp;variable&nbsp;ctx.This&nbsp;variable&nbsp;will&nbsp;be&nbsp;used&nbsp;to&nbsp;draw&nbsp;the&nbsp;Pie&nbsp;Chart</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;canvas&nbsp;=&nbsp;document.getElementById(<span class="js__string">&quot;canvas&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx&nbsp;=&nbsp;canvas.getContext(<span class="js__string">&quot;2d&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//globalAlpha&nbsp;-&nbsp;&gt;&nbsp;is&nbsp;used&nbsp;to&nbsp;display&nbsp;the&nbsp;100%&nbsp;opoacity&nbsp;of&nbsp;chart&nbsp;.because&nbsp;at&nbsp;the&nbsp;bottom&nbsp;of&nbsp;the&nbsp;code&nbsp;I&nbsp;have&nbsp;used&nbsp;the&nbsp;opacity&nbsp;to&nbsp;0.1&nbsp;to&nbsp;display&nbsp;the&nbsp;water&nbsp;mark&nbsp;text&nbsp;with&nbsp;fade&nbsp;effect.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.globalAlpha&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#000000&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.strokeStyle&nbsp;=&nbsp;<span class="js__string">'#000000'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Every&nbsp;time&nbsp;we&nbsp;clear&nbsp;the&nbsp;canvas&nbsp;and&nbsp;draw&nbsp;the&nbsp;chart</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.clearRect(<span class="js__num">0</span>,&nbsp;<span class="js__num">0</span>,&nbsp;canvas.width,&nbsp;canvas.height);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//If&nbsp;need&nbsp;to&nbsp;draw&nbsp;with&nbsp;out&nbsp;legend&nbsp;for&nbsp;the&nbsp;Line&nbsp;Chart</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartWidth&nbsp;=&nbsp;canvas.width&nbsp;-&nbsp;xSpace;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartHeight&nbsp;=&nbsp;canvas.height&nbsp;-&nbsp;ySpace;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;&nbsp;step&nbsp;1)&nbsp;Draw&nbsp;legend&nbsp;$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$########################</span><span class="js__statement">if</span>&nbsp;($(<span class="js__string">'#chkLegend:checked'</span>).val()&nbsp;==&nbsp;<span class="js__string">&quot;Show&nbsp;Legend&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartWidth&nbsp;=&nbsp;canvas.width&nbsp;-&nbsp;((canvas.width&nbsp;/&nbsp;<span class="js__num">3</span>)&nbsp;-&nbsp;(xSpace&nbsp;/&nbsp;<span class="js__num">2</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartHeight&nbsp;=&nbsp;canvas.height&nbsp;-&nbsp;ySpace&nbsp;-&nbsp;<span class="js__num">10</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;legendWidth&nbsp;=&nbsp;canvas.width&nbsp;-&nbsp;((canvas.width&nbsp;/&nbsp;<span class="js__num">3</span>)&nbsp;-&nbsp;xSpace);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;legendHeight&nbsp;=&nbsp;ySpace;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rect.startX&nbsp;=&nbsp;legendWidth;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rect.startY&nbsp;=&nbsp;legendHeight;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rect.w&nbsp;=&nbsp;canvas.width&nbsp;/&nbsp;<span class="js__num">3</span>&nbsp;-&nbsp;xSpace&nbsp;-&nbsp;<span class="js__num">10</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rect.h&nbsp;=&nbsp;canvas.height&nbsp;-&nbsp;ySpace&nbsp;-&nbsp;<span class="js__num">10</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//In&nbsp;this&nbsp;method&nbsp;i&nbsp;will&nbsp;draw&nbsp;the&nbsp;legend&nbsp;with&nbsp;the&nbsp;Alert&nbsp;Image.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;drawLengends();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;end&nbsp;step&nbsp;1)&nbsp;$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$</span><span class="js__statement">var</span>&nbsp;chartMidPosition&nbsp;=&nbsp;chartWidth&nbsp;/&nbsp;<span class="js__num">2</span>&nbsp;-&nbsp;<span class="js__num">60</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">////&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;//If&nbsp;need&nbsp;to&nbsp;draw&nbsp;with&nbsp;legend</span><span class="js__sl_comment">////&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartWidth&nbsp;=&nbsp;canvas.width&nbsp;-&nbsp;((canvas.width&nbsp;/&nbsp;3)&nbsp;-&nbsp;(xSpace&nbsp;/&nbsp;2));</span><span class="js__sl_comment">////&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartHeight&nbsp;=&nbsp;canvas.height&nbsp;-&nbsp;ySpace&nbsp;-&nbsp;10;</span><span class="js__sl_comment">//&nbsp;Step&nbsp;2&nbsp;)&nbsp;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&nbsp;To&nbsp;Add&nbsp;Chart&nbsp;Titel&nbsp;and&nbsp;&nbsp;Company&nbsp;Logo</span><span class="js__sl_comment">//To&nbsp;Add&nbsp;Logo&nbsp;to&nbsp;Chart</span><span class="js__statement">var</span>&nbsp;logoXVal&nbsp;=&nbsp;canvas.width&nbsp;-&nbsp;LogoImgWidth&nbsp;-&nbsp;<span class="js__num">10</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;logolYVal&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//here&nbsp;we&nbsp;draw&nbsp;the&nbsp;Logo&nbsp;for&nbsp;teh&nbsp;chart&nbsp;and&nbsp;i&nbsp;have&nbsp;used&nbsp;the&nbsp;alpha&nbsp;to&nbsp;fade&nbsp;and&nbsp;display&nbsp;the&nbsp;Logo.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.globalAlpha&nbsp;=&nbsp;<span class="js__num">0.6</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.drawImage(LogoImage,&nbsp;logoXVal,&nbsp;logolYVal,&nbsp;LogoImgWidth,&nbsp;LogoImgHeight);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.globalAlpha&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'22pt&nbsp;Calibri'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#15317E&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;titletxt&nbsp;=&nbsp;$(<span class="js__string">'input:text[name=txtTitle]'</span>).val();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillText(titletxt,&nbsp;chartMidPosition,&nbsp;chartHeight&nbsp;&#43;&nbsp;<span class="js__num">60</span>);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#000000&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'10pt&nbsp;Calibri'</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;end&nbsp;step&nbsp;2)&nbsp;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&nbsp;End&nbsp;of&nbsp;Title&nbsp;and&nbsp;Company&nbsp;Logo&nbsp;Add</span><span class="js__sl_comment">//&nbsp;Step&nbsp;3&nbsp;)&nbsp;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&nbsp;toDraw&nbsp;the&nbsp;X-Axis&nbsp;and&nbsp;Y-Axis</span><span class="js__sl_comment">//&nbsp;&nbsp;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&nbsp;Draw&nbsp;Y-Axis&nbsp;and&nbsp;X-Axis&nbsp;Line(Horizontal&nbsp;Line)</span><span class="js__sl_comment">//&nbsp;Draw&nbsp;the&nbsp;axises</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.beginPath();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.moveTo(xSpace,&nbsp;ySpace);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;first&nbsp;Draw&nbsp;Y&nbsp;Axis</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineTo(xSpace,&nbsp;chartHeight);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Next&nbsp;draw&nbsp;the&nbsp;X-Axis</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineTo(chartWidth,&nbsp;chartHeight);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.stroke();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;&nbsp;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&nbsp;End&nbsp;of&nbsp;X-Axis&nbsp;Line&nbsp;Draw</span><span class="js__sl_comment">//end&nbsp;step&nbsp;3)&nbsp;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;&#43;</span><span class="js__sl_comment">//&nbsp;Step&nbsp;4)&nbsp;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&nbsp;To&nbsp;Draw&nbsp;X&nbsp;-&nbsp;Axis&nbsp;Plot&nbsp;Values&nbsp;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&nbsp;}}}}}}</span><span class="js__sl_comment">//&nbsp;Draw&nbsp;the&nbsp;X&nbsp;value&nbsp;texts</span><span class="js__sl_comment">//&nbsp;---&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&nbsp;&nbsp;for&nbsp;the&nbsp;Line&nbsp;Chart&nbsp;i&nbsp;have&nbsp;draw&nbsp;the&nbsp;X-Axis&nbsp;plot&nbsp;in&nbsp;with&nbsp;drawLineChart</span><span class="js__sl_comment">//&nbsp;&nbsp;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&nbsp;End&nbsp;of&nbsp;X&nbsp;Axis&nbsp;Draw</span><span class="js__sl_comment">//&nbsp;end&nbsp;Step&nbsp;4)&nbsp;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;&lt;</span><span class="js__sl_comment">//&nbsp;Step&nbsp;5){{{{{{{{{{{{</span><span class="js__sl_comment">//&nbsp;{{{{{{{{{{{{{To&nbsp;Draw&nbsp;the&nbsp;Y&nbsp;Axis&nbsp;Plot&nbsp;Values}}}}}}}}}}}}}}</span><span class="js__statement">var</span>&nbsp;vAxisPoints&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;max&nbsp;=&nbsp;maxDataVal;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;max&nbsp;&#43;=&nbsp;<span class="js__num">10</span>&nbsp;-&nbsp;max&nbsp;%&nbsp;<span class="js__num">10</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;=&nbsp;maxDataVal;&nbsp;i&nbsp;&#43;=&nbsp;maxValdivValue)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;fotnColor;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;axisfontSize&nbsp;&#43;&nbsp;<span class="js__string">'pt&nbsp;Calibri'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillText(i,&nbsp;xSpace&nbsp;-&nbsp;<span class="js__num">40</span>,&nbsp;getYPlotVale(i));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Here&nbsp;we&nbsp;draw&nbsp;the&nbsp;Y-Axis&nbsp;point&nbsp;line</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.beginPath();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.moveTo(xSpace,&nbsp;getYPlotVale(i));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineTo(xSpace&nbsp;-&nbsp;<span class="js__num">10</span>,&nbsp;getYPlotVale(i));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.stroke();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;vAxisPoints&nbsp;=&nbsp;vAxisPoints&nbsp;&#43;&nbsp;maxValdivValue;&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__sl_comment">//}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">//Step&nbsp;5)&nbsp;*********************************************************</span><span class="js__sl_comment">//Function&nbsp;to&nbsp;Draw&nbsp;our&nbsp;Chart&nbsp;here&nbsp;we&nbsp;can&nbsp;Call/Line&nbsp;Chart/Line&nbsp;Chart&nbsp;or&nbsp;Pie&nbsp;Chart</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;drawLineChart();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;end&nbsp;step&nbsp;6)&nbsp;**************</span><span class="js__sl_comment">//Step&nbsp;7)&nbsp;&nbsp;::::::::::::::::::::&nbsp;to&nbsp;add&nbsp;the&nbsp;Water&nbsp;mark&nbsp;Text</span><span class="js__statement">var</span>&nbsp;waterMarktxt&nbsp;=&nbsp;$(<span class="js__string">'input:text[name=txtWatermark]'</span>).val();&nbsp;
&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Here&nbsp;add&nbsp;the&nbsp;Water&nbsp;mark&nbsp;text&nbsp;at&nbsp;center&nbsp;of&nbsp;the&nbsp;chart</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.globalAlpha&nbsp;=&nbsp;<span class="js__num">0.1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'86pt&nbsp;Calibri'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#000000&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillText(waterMarktxt,&nbsp;chartMidPosition&nbsp;-&nbsp;<span class="js__num">40</span>,&nbsp;chartHeight&nbsp;/&nbsp;<span class="js__num">2</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'10pt&nbsp;Calibri'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.globalAlpha&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;end&nbsp;step&nbsp;7)&nbsp;::::::::::::::::::::::::::::::::::::::</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h2><strong>Draw Line Chart:</strong></h2>
<p><strong></strong>In this function we get loop the ComboBox items and get all item Name and Value and draw Line chart and plot values one by one in our Canvas Tag.</p>
<div class="scriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">function drawLineChart()  
{  
    // For Drawing Line  
    ctx.lineWidth = 3;  
    var value = $('select#DropDownList1 option:selected').val();  
    ctx.beginPath();  
    // *************** To Draw the Line and Plot Value in Line  
    ctx.fillStyle = &quot;#FFFFFF&quot;;  
    ctx.strokeStyle = '#FFFFFF';  
    ctx.moveTo(getXPlotvalue(0), getYPlotVale(value));  
    ctx.fillStyle = &quot;#000000&quot;;  
    ctx.font = '12pt Calibri';  
    ctx.fillText(value, getXPlotvalue(0), getYPlotVale(value) - 12);  
    var ival = 0;  
    $('#DropDownList1').val($('#DropDownList1 option').eq(0).val());  
    $('#DropDownList1 option').each(function(i)  
    {  
        if (ival &gt; 0)  
        {  
            ctx.lineTo(getXPlotvalue(ival), getYPlotVale($(this).val()));  
            ctx.stroke();  
            ctx.fillStyle = &quot;#000000&quot;;  
            ctx.font = '12pt Calibri';  
            ctx.fillText($(this).val(), getXPlotvalue(ival), getYPlotVale($(this).val()) - 16);  
        }  
        ival = ival &#43; 1;  
        ctx.fillStyle = lineColor;  
        ctx.strokeStyle = lineColor;  
    });  
    // *************** To Draw the Line Dot Cericle  
    //For Outer Blue Dot  
    ival = 0;  
    $('#DropDownList1 option').each(function(i)  
    {  
        ctx.fillStyle = lineOuterCircleColor;  
        ctx.strokeStyle = lineOuterCircleColor;  
        ctx.beginPath();  
        ctx.arc(getXPlotvalue(ival), getYPlotVale($(this).val()), 7, 0, Math.PI * 2, true);  
        ctx.fill();  
        ctx.fillStyle = lineInnerCircleColor;  
        ctx.strokeStyle = lineInnerCircleColor;  
        ctx.beginPath();  
        ctx.arc(getXPlotvalue(ival), getYPlotVale($(this).val()), 4, 0, Math.PI * 2, true);  
        ctx.fill();  
        ival = ival &#43; 1;  
    });  
    ctx.lineWidth = 1;  
}  </pre>
<div class="preview">
<pre class="js"><span class="js__operator">function</span>&nbsp;drawLineChart()&nbsp;&nbsp;&nbsp;
<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;For&nbsp;Drawing&nbsp;Line&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineWidth&nbsp;=&nbsp;<span class="js__num">3</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;value&nbsp;=&nbsp;$(<span class="js__string">'select#DropDownList1&nbsp;option:selected'</span>).val();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.beginPath();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;***************&nbsp;To&nbsp;Draw&nbsp;the&nbsp;Line&nbsp;and&nbsp;Plot&nbsp;Value&nbsp;in&nbsp;Line&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#FFFFFF&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.strokeStyle&nbsp;=&nbsp;<span class="js__string">'#FFFFFF'</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.moveTo(getXPlotvalue(<span class="js__num">0</span>),&nbsp;getYPlotVale(value));&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#000000&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'12pt&nbsp;Calibri'</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillText(value,&nbsp;getXPlotvalue(<span class="js__num">0</span>),&nbsp;getYPlotVale(value)&nbsp;-&nbsp;<span class="js__num">12</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;ival&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#DropDownList1'</span>).val($(<span class="js__string">'#DropDownList1&nbsp;option'</span>).eq(<span class="js__num">0</span>).val());&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#DropDownList1&nbsp;option'</span>).each(<span class="js__operator">function</span>(i)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(ival&nbsp;&gt;&nbsp;<span class="js__num">0</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineTo(getXPlotvalue(ival),&nbsp;getYPlotVale($(<span class="js__operator">this</span>).val()));&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.stroke();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;<span class="js__string">&quot;#000000&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.font&nbsp;=&nbsp;<span class="js__string">'12pt&nbsp;Calibri'</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillText($(<span class="js__operator">this</span>).val(),&nbsp;getXPlotvalue(ival),&nbsp;getYPlotVale($(<span class="js__operator">this</span>).val())&nbsp;-&nbsp;<span class="js__num">16</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ival&nbsp;=&nbsp;ival&nbsp;&#43;&nbsp;<span class="js__num">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;lineColor;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.strokeStyle&nbsp;=&nbsp;lineColor;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;***************&nbsp;To&nbsp;Draw&nbsp;the&nbsp;Line&nbsp;Dot&nbsp;Cericle&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//For&nbsp;Outer&nbsp;Blue&nbsp;Dot&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ival&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#DropDownList1&nbsp;option'</span>).each(<span class="js__operator">function</span>(i)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;lineOuterCircleColor;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.strokeStyle&nbsp;=&nbsp;lineOuterCircleColor;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.beginPath();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.arc(getXPlotvalue(ival),&nbsp;getYPlotVale($(<span class="js__operator">this</span>).val()),&nbsp;<span class="js__num">7</span>,&nbsp;<span class="js__num">0</span>,&nbsp;<span class="js__object">Math</span>.PI&nbsp;*&nbsp;<span class="js__num">2</span>,&nbsp;true);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fill();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;lineInnerCircleColor;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.strokeStyle&nbsp;=&nbsp;lineInnerCircleColor;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.beginPath();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.arc(getXPlotvalue(ival),&nbsp;getYPlotVale($(<span class="js__operator">this</span>).val()),&nbsp;<span class="js__num">4</span>,&nbsp;<span class="js__num">0</span>,&nbsp;<span class="js__object">Math</span>.PI&nbsp;*&nbsp;<span class="js__num">2</span>,&nbsp;true);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fill();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ival&nbsp;=&nbsp;ival&nbsp;&#43;&nbsp;<span class="js__num">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineWidth&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span>&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">&nbsp;<span>Source Code Files</span></div>
<ul>
<li>shanuMVCAngularJSChart.zip </li></ul>
<h1>More Information</h1>
<p><strong>Note:&nbsp;</strong><span>Run the SQL Script in your SQL Server to created DB, Table and stored procedure. In web.config change the connection string to your local SQL Server connection. In the attached zip file you can find code for both Bar Chart
 and for Line chart.</span></p>
<p><strong>Tested Browsers:</strong></p>
<p>&sup2;&nbsp; Chrome</p>
<p>&sup2;&nbsp; Firefox</p>
<p>&sup2;&nbsp; IE10<strong>&nbsp;</strong></p>
