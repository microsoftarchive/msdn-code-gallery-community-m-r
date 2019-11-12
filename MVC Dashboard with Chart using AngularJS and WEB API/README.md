# MVC Dashboard with Chart using AngularJS and WEB API
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- ASP.NET MVC
- jQuery
- Chart
- AngularJS
- dashboard
## Topics
- ASP.NET MVC
- jQuery
- AngularJS
- dashboard
## Updated
- 01/23/2017
## Description

<h1>Introduction</h1>
<p><img id="154027" src="154027-shanudashboard1.gif" alt="" width="600" height="645"></p>
<p>In this article we will see in detail about how to create a Dynamic MVC Dashboard with Chart and Data display using AngularJS and WEB API. Using this web application now you can write your own SQL query to bind dynamic Dashboard with Chart and Data. This
 program makes your work easy to display any Table/Columns details with your entered where Condition, Order BY and with Group By Option for the selected database on your home page with both data and Chart.</p>
<p><img id="154028" src="154028-9.png" alt="" width="547" height="337"></p>
<p>In our previous article <a title="https://code.msdn.microsoft.com/MVC-Dashboard-using-91748fc8" href="https://code.msdn.microsoft.com/MVC-Dashboard-using-91748fc8" target="_blank">
https://code.msdn.microsoft.com/MVC-Dashboard-using-91748fc8</a>&nbsp;&nbsp;we have explained in detail about how to display any data on home page dashboard on our MVC web application. In this article we will see in detail about how to display data and Chart
 on dashboard in MVC Web application using AngularJs and Web API.</p>
<p>In this demo application we have drawn Pie Chart in our MVC Dashboard page. You can draw any chart as per your requirement In our previous article&nbsp;<a title="https://code.msdn.microsoft.com/Draw-MVC-Line-Chart-using-5c6c9f49" href="https://code.msdn.microsoft.com/Draw-MVC-Line-Chart-using-5c6c9f49">https://code.msdn.microsoft.com/Draw-MVC-Line-Chart-using-5c6c9f49&nbsp;</a>&nbsp;we
 have explained about how to draw Chart like,Line, Pie, Bar, Donut, Bubble and Line &amp; Bar Chart in MVC application .We have used the same logic to draw chart on our MVC dashboard page.</p>
<h2><strong>Features in Shanu MVC Dashboard</strong></h2>
<p><img id="154029" src="154029-1.png" alt="" width="544" height="334"></p>
<p><strong>1) Dynamic SQL Query:</strong></p>
<p><strong>2) Column Names:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p>
<p><strong>3) Table Names:</strong></p>
<p><strong>4) Where Condition:</strong></p>
<p><strong>5) Group By:</strong></p>
<p><strong>6) Order By:</strong></p>
<p><strong>7) Chart SQL Query</strong></p>
<p><strong>8) Chart Setting and Draw Chart</strong><strong>&nbsp;</strong></p>
<p>Here we will see details of each part</p>
<p>Kindly refer our previous article <a title="https://code.msdn.microsoft.com/MVC-Dashboard-using-91748fc8" href="https://code.msdn.microsoft.com/MVC-Dashboard-using-91748fc8" target="_blank">
https://code.msdn.microsoft.com/MVC-Dashboard-using-91748fc8</a>&nbsp; for sections from 1 to 6.We have explained in detail about each section with animated images.</p>
<p>This article has all same features with additional Chart Feature to be displayed on our MVC Dashboard.</p>
<p><strong>7) Chart SQL Query: </strong>To display chart first we need write our select query to display both Chart Item and Value.</p>
<p><img id="154030" src="154030-shanudashboard2.gif" alt="" width="646" height="385"></p>
<p>Here is the sample query to display Chart on our MVC dashboard page. Here for chart binding user can enter the complete select query to bind the result in the combobox.</p>
<p>Sample Select query to be used for our application:&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>

<div class="preview">
<pre class="js">Select&nbsp;ItemName&nbsp;as&nbsp;Name,SUM(Price)&nbsp;as&nbsp;Value&nbsp;FROM&nbsp;ItemDetail&nbsp;GROUP&nbsp;BY&nbsp;&nbsp;ItemName&nbsp;ORDER&nbsp;BY&nbsp;Value,Name.</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;For draw chart we have fixed the standard as always display 2 columns one is Name and another one is Value. Here name is the any name (Legend) to be displayed for chart and value is the actual value to draw the chart.In search
 button click we first bind the chart item reault to the combobox .We will be using this combobox result draw chart.&nbsp;</div>
<p>&nbsp;</p>
<p><strong>8) Chart Setting and Draw Chart</strong></p>
<p>User can add Chart Title, Watermark Text as per your requirement at runtime and click on &ldquo;Click to Draw Chart) button to draw your chart on Dashboard.</p>
<p><img id="154031" src="154031-shanudashboard3.gif" alt="" width="638" height="453"></p>
<p>Note: You can display any chart data from any table from the given database. All you need to do is write the select query for chart with Name and Value column.</p>
<h1><span>Building the Sample</span></h1>
<p><strong>Visual Studio 2015:</strong> You can download it from <a href="https://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs.aspx" target="_blank">
here</a>.</p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<h2><strong>Step 1:<span lang="EN-US">Create a sample database and Table&nbsp;</span>
</strong></h2>
<p><strong>&nbsp;</strong>Create a sample database and Table to for testing this application. Here is a SQL script to create database and Table with insert query. Kindly run the below code in your SQL Server to create DB and Tables.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>

<div class="preview">
<pre class="js">----&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;Create&nbsp;date&nbsp;:&nbsp;<span class="js__num">2016</span><span class="js__num">-05</span><span class="js__num">-12</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;Description&nbsp;:&nbsp;To&nbsp;Create&nbsp;Database,Table&nbsp;and&nbsp;Sample&nbsp;Insert&nbsp;Query&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;Latest&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;Modify&nbsp;date&nbsp;:&nbsp;<span class="js__num">2016</span><span class="js__num">-05</span><span class="js__num">-12</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
----&nbsp;=============================================&nbsp;
----Script&nbsp;to&nbsp;create&nbsp;DB,Table&nbsp;and&nbsp;sample&nbsp;Insert&nbsp;data&nbsp;
&nbsp;
USE&nbsp;MASTER&nbsp;
GO&nbsp;
&nbsp;
&nbsp;--<span class="js__num">1</span>)&nbsp;Check&nbsp;<span class="js__statement">for</span>&nbsp;the&nbsp;Database&nbsp;Exists&nbsp;.If&nbsp;the&nbsp;database&nbsp;is&nbsp;exist&nbsp;then&nbsp;drop&nbsp;and&nbsp;create&nbsp;<span class="js__operator">new</span>&nbsp;DB&nbsp;
&nbsp;
IF&nbsp;EXISTS&nbsp;(SELECT&nbsp;[name]&nbsp;FROM&nbsp;sys.databases&nbsp;WHERE&nbsp;[name]&nbsp;=&nbsp;<span class="js__string">'DashboardDB'</span>&nbsp;)&nbsp;
DROP&nbsp;DATABASE&nbsp;DashboardDB&nbsp;
&nbsp;
GO&nbsp;
&nbsp;
CREATE&nbsp;DATABASE&nbsp;DashboardDB&nbsp;
GO&nbsp;
&nbsp;
USE&nbsp;DashboardDB&nbsp;
GO&nbsp;
&nbsp;
&nbsp;
--&nbsp;<span class="js__num">1</span>)&nbsp;<span class="js__sl_comment">////////////&nbsp;ItemDetails&nbsp;table</span>&nbsp;
&nbsp;
--&nbsp;Create&nbsp;Table&nbsp;ItemDetails,This&nbsp;table&nbsp;will&nbsp;be&nbsp;used&nbsp;to&nbsp;store&nbsp;the&nbsp;details&nbsp;like&nbsp;Item&nbsp;Information&nbsp;
IF&nbsp;EXISTS&nbsp;(&nbsp;SELECT&nbsp;[name]&nbsp;FROM&nbsp;sys.tables&nbsp;WHERE&nbsp;[name]&nbsp;=&nbsp;<span class="js__string">'ItemDetail'</span>&nbsp;)&nbsp;
DROP&nbsp;TABLE&nbsp;ItemDetail&nbsp;
GO&nbsp;
&nbsp;
CREATE&nbsp;TABLE&nbsp;[dbo].[ItemDetail](&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ID]&nbsp;[int]&nbsp;IDENTITY(<span class="js__num">1</span>,<span class="js__num">1</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ItemNo]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL&nbsp;,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ItemName]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Comments]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Price]&nbsp;INT&nbsp;NOT&nbsp;NULL,&nbsp;
PRIMARY&nbsp;KEY&nbsp;CLUSTERED&nbsp;&nbsp;
(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ID]&nbsp;ASC&nbsp;
)WITH&nbsp;(PAD_INDEX&nbsp;=&nbsp;OFF,&nbsp;STATISTICS_NORECOMPUTE&nbsp;=&nbsp;OFF,&nbsp;IGNORE_DUP_KEY&nbsp;=&nbsp;OFF,&nbsp;ALLOW_ROW_LOCKS&nbsp;=&nbsp;ON,&nbsp;ALLOW_PAGE_LOCKS&nbsp;=&nbsp;ON)&nbsp;ON&nbsp;[PRIMARY]&nbsp;
)&nbsp;ON&nbsp;[PRIMARY]&nbsp;
&nbsp;
GO&nbsp;
&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;ItemDetail(ItemNo,ItemName,Comments,Price)&nbsp;values&nbsp;
(<span class="js__string">'101'</span>,<span class="js__string">'NoteBook'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'HP&nbsp;Notebook&nbsp;15&nbsp;Inch'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__num">24500</span>)&nbsp;
&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;ItemDetail(ItemNo,ItemName,Comments,Price)&nbsp;values&nbsp;
(<span class="js__string">'102'</span>,<span class="js__string">'MONITOR'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'SAMSNG'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'8500'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;ItemDetail(ItemNo,ItemName,Comments,Price)&nbsp;values&nbsp;
(<span class="js__string">'103'</span>,<span class="js__string">'MOBILE'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'SAMSUNG&nbsp;NOTE&nbsp;5'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__num">42500</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;ItemDetail(ItemNo,ItemName,Comments,Price)&nbsp;values&nbsp;
(<span class="js__string">'104'</span>,<span class="js__string">'MOBILE'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'SAMSUNG&nbsp;S7&nbsp;Edge'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__num">56000</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;ItemDetail(ItemNo,ItemName,Comments,Price)&nbsp;values&nbsp;
(<span class="js__string">'105'</span>,<span class="js__string">'MOUSE'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'ABKO'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__num">780</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;ItemDetail(ItemNo,ItemName,Comments,Price)&nbsp;values&nbsp;
(<span class="js__string">'106'</span>,<span class="js__string">'HDD'</span>&nbsp;&nbsp;&nbsp;&nbsp;,<span class="js__string">'LG'</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__num">3780</span>)&nbsp;
&nbsp;
select&nbsp;*&nbsp;from&nbsp;ItemDetail&nbsp;
&nbsp;
&nbsp;
select&nbsp;ItemName,SUM(convert(int,Price))&nbsp;as&nbsp;totalCost&nbsp;&nbsp;
from&nbsp;ItemDetail&nbsp;
GROUP&nbsp;BY&nbsp;ItemName&nbsp;
&nbsp;
&nbsp;
&nbsp;
--&nbsp;<span class="js__num">2</span>)&nbsp;User&nbsp;table&nbsp;&nbsp;
&nbsp;
IF&nbsp;EXISTS&nbsp;(&nbsp;SELECT&nbsp;[name]&nbsp;FROM&nbsp;sys.tables&nbsp;WHERE&nbsp;[name]&nbsp;=&nbsp;<span class="js__string">'UserDetails'</span>&nbsp;)&nbsp;
DROP&nbsp;TABLE&nbsp;UserDetails&nbsp;
GO&nbsp;
&nbsp;
CREATE&nbsp;TABLE&nbsp;[dbo].UserDetails(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserID]&nbsp;[int]&nbsp;IDENTITY(<span class="js__num">1</span>,<span class="js__num">1</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserName]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserType]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Phone]&nbsp;[varchar](<span class="js__num">20</span>)&nbsp;NOT&nbsp;NULL,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
PRIMARY&nbsp;KEY&nbsp;CLUSTERED&nbsp;&nbsp;
(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserID]&nbsp;ASC&nbsp;
)WITH&nbsp;(PAD_INDEX&nbsp;=&nbsp;OFF,&nbsp;STATISTICS_NORECOMPUTE&nbsp;=&nbsp;OFF,&nbsp;IGNORE_DUP_KEY&nbsp;=&nbsp;OFF,&nbsp;ALLOW_ROW_LOCKS&nbsp;=&nbsp;ON,&nbsp;ALLOW_PAGE_LOCKS&nbsp;=&nbsp;ON)&nbsp;ON&nbsp;[PRIMARY]&nbsp;
)&nbsp;ON&nbsp;[PRIMARY]&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'SHANU'</span>,<span class="js__string">'Admin'</span>,<span class="js__string">'01039124503'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Afraz'</span>,<span class="js__string">'user'</span>,<span class="js__string">'01039120984'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Afreen'</span>,<span class="js__string">'user'</span>,<span class="js__string">'01039120005'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Raj'</span>,<span class="js__string">'Admin'</span>,<span class="js__string">'01039120006'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Mak'</span>,<span class="js__string">'Manager'</span>,<span class="js__string">'01039124567'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Jack'</span>,<span class="js__string">'Manager'</span>,<span class="js__string">'01039120238'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Pak'</span>,<span class="js__string">'User'</span>,<span class="js__string">'01039125409'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Ninu'</span>,<span class="js__string">'Accountant'</span>,<span class="js__string">'01039126810'</span>)&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserDetails(UserName,UserType,Phone)&nbsp;values&nbsp;
(<span class="js__string">'Nanu'</span>,<span class="js__string">'Accountant'</span>,<span class="js__string">'01039152011'</span>)&nbsp;
&nbsp;
--&nbsp;select&nbsp;*&nbsp;from&nbsp;Userdetails&nbsp;
--&nbsp;<span class="js__num">3</span>&nbsp;UserAddress&nbsp;
&nbsp;
IF&nbsp;EXISTS&nbsp;(&nbsp;SELECT&nbsp;[name]&nbsp;FROM&nbsp;sys.tables&nbsp;WHERE&nbsp;[name]&nbsp;=&nbsp;<span class="js__string">'UserAddress'</span>&nbsp;)&nbsp;
DROP&nbsp;TABLE&nbsp;UserAddress&nbsp;
GO&nbsp;
&nbsp;
CREATE&nbsp;TABLE&nbsp;[dbo].UserAddress(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserAddID]&nbsp;[int]&nbsp;IDENTITY(<span class="js__num">1</span>,<span class="js__num">1</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserID]&nbsp;[int]&nbsp;,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Address]&nbsp;[varchar](<span class="js__num">200</span>)&nbsp;NOT&nbsp;NULL,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Email]&nbsp;[varchar](<span class="js__num">100</span>)&nbsp;NOT&nbsp;NULL,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
PRIMARY&nbsp;KEY&nbsp;CLUSTERED&nbsp;&nbsp;
(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[UserAddID]&nbsp;ASC&nbsp;
)WITH&nbsp;(PAD_INDEX&nbsp;=&nbsp;OFF,&nbsp;STATISTICS_NORECOMPUTE&nbsp;=&nbsp;OFF,&nbsp;IGNORE_DUP_KEY&nbsp;=&nbsp;OFF,&nbsp;ALLOW_ROW_LOCKS&nbsp;=&nbsp;ON,&nbsp;ALLOW_PAGE_LOCKS&nbsp;=&nbsp;ON)&nbsp;ON&nbsp;[PRIMARY]&nbsp;
)&nbsp;ON&nbsp;[PRIMARY]&nbsp;
&nbsp;
Insert&nbsp;into&nbsp;UserAddress(UserID,Address,Email)&nbsp;values&nbsp;
(<span class="js__num">1</span>,<span class="js__string">'Madurai,Tamil&nbsp;Nadu,&nbsp;India'</span>,<span class="js__string">'syedshanumcain@gmail.com'</span>)&nbsp;
Insert&nbsp;into&nbsp;UserAddress(UserID,Address,Email)&nbsp;values&nbsp;
(<span class="js__num">2</span>,<span class="js__string">'Madurai,Tamil&nbsp;Nadu,&nbsp;India'</span>,<span class="js__string">'afraz@afrazmail.com'</span>)&nbsp;
Insert&nbsp;into&nbsp;UserAddress(UserID,Address,Email)&nbsp;values&nbsp;
(<span class="js__num">3</span>,<span class="js__string">'Seoul,South&nbsp;Korea'</span>,<span class="js__string">'afreen@afrazmail.com'</span>)&nbsp;
&nbsp;
select&nbsp;*&nbsp;from&nbsp;UserAddress&nbsp;
&nbsp;
select&nbsp;A.UserName,A.UserType,A.Phone,B.Address,B.Email&nbsp;
From&nbsp;
Userdetails&nbsp;A&nbsp;Left&nbsp;Outer&nbsp;JOIN&nbsp;UserAddress&nbsp;B&nbsp;
on&nbsp;
A.UserID=B.UserID&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><strong>Create Stored Procedure to run Dynamic Query:</strong></p>
<p>This is our main Stored procedure used to run all our Dynamic SQL Select query and return the result to bind in our MVC page.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>

<div class="preview">
<pre class="js">USE&nbsp;[DashboardDB]&nbsp;
GO&nbsp;
/******&nbsp;Object:&nbsp;&nbsp;StoredProcedure&nbsp;[dbo].[USP_Dashboard_Select]&nbsp;&nbsp;&nbsp;&nbsp;******/&nbsp;
SET&nbsp;ANSI_NULLS&nbsp;ON&nbsp;
GO&nbsp;
SET&nbsp;QUOTED_IDENTIFIER&nbsp;ON&nbsp;
GO&nbsp;
&nbsp;
--&nbsp;<span class="js__num">1</span>)&nbsp;select&nbsp;top&nbsp;<span class="js__num">10</span>&nbsp;random&nbsp;kidsLearnerMaster&nbsp;records&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Create&nbsp;date&nbsp;:&nbsp;&nbsp;<span class="js__num">2016</span><span class="js__num">-05</span><span class="js__num">-14</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Description&nbsp;:To&nbsp;run&nbsp;dymanic&nbsp;Query&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;Dynamic&nbsp;Table&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;<span class="js__num">2016</span><span class="js__num">-05</span><span class="js__num">-14</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
--&nbsp;To&nbsp;Select&nbsp;all&nbsp;user&nbsp;roles&nbsp;&nbsp;
--&nbsp;EXEC&nbsp;USP_Dashboard_Select&nbsp;@columnName&nbsp;=&nbsp;<span class="js__string">'UserName,UserType,Phone'</span>&nbsp;,@TableNames&nbsp;=&nbsp;<span class="js__string">'UserDetails'</span>&nbsp;,@isCondition=<span class="js__num">0</span>,@ConditionList=<span class="js__string">'UserType='</span><span class="js__string">'ADMIN'</span><span class="js__string">'&nbsp;'</span>,@isGroupBY&nbsp;=<span class="js__num">1</span>,@GroupBYList&nbsp;=&nbsp;<span class="js__string">'UserName,UserType,Phone'</span>,&nbsp;@isOrderBY&nbsp;=<span class="js__num">1</span>,@OrderBYList&nbsp;=&nbsp;<span class="js__string">'&nbsp;UserType&nbsp;'</span>&nbsp;
&nbsp;
--&nbsp;EXEC&nbsp;USP_Dashboard_Select&nbsp;@columnName&nbsp;=&nbsp;<span class="js__string">'ItemName,SUM(Price)&nbsp;as&nbsp;totalCost'</span>&nbsp;,@TableNames&nbsp;=&nbsp;<span class="js__string">'ItemDetail'</span>&nbsp;,@isCondition=<span class="js__num">0</span>,@ConditionList=<span class="js__string">'Price&gt;'</span><span class="js__string">'400'</span><span class="js__string">'&nbsp;'</span>,@isGroupBY&nbsp;=<span class="js__num">1</span>,@GroupBYList&nbsp;=&nbsp;<span class="js__string">'ItemName'</span>&nbsp;
--&nbsp;EXEC&nbsp;USP_Dashboard_Select&nbsp;@sqlQuery&nbsp;=&nbsp;<span class="js__string">'Select&nbsp;*&nbsp;from&nbsp;ItemDetail'</span>&nbsp;
--&nbsp;EXEC&nbsp;USP_Dashboard_Select&nbsp;@sqlQuery&nbsp;=&nbsp;<span class="js__string">'select&nbsp;ID,ItemNo&nbsp;,ItemName&nbsp;,Comments&nbsp;,Price&nbsp;from&nbsp;ItemDetail'</span>&nbsp;
&nbsp;
--&nbsp;=============================================&nbsp;&nbsp;&nbsp;
ALTER&nbsp;PROCEDURE&nbsp;[dbo].[USP_Dashboard_Select]&nbsp;&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@sqlQuery&nbsp;varchar(MAX)=<span class="js__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@columnName&nbsp;varchar(MAX)=<span class="js__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@TableNames&nbsp;varchar(MAX)=<span class="js__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@isCondition&nbsp;INT=<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@ConditionList&nbsp;varchar(MAX)=<span class="js__string">''</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@isGroupBY&nbsp;INT=<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@GroupBYList&nbsp;varchar(MAX)=<span class="js__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@isOrderBY&nbsp;INT=<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@OrderBYList&nbsp;varchar(MAX)=<span class="js__string">''</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
AS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
BEGIN&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
BEGIN&nbsp;TRY&nbsp;
IF&nbsp;@sqlQuery&nbsp;=<span class="js__string">''</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SET&nbsp;@sqlQuery&nbsp;=&nbsp;<span class="js__string">'SELECT&nbsp;'</span>&nbsp;&#43;&nbsp;@columnName&nbsp;&#43;&nbsp;<span class="js__string">'&nbsp;FROM&nbsp;'</span>&nbsp;&#43;&nbsp;@TableNames&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IF&nbsp;@isCondition=<span class="js__num">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SET&nbsp;@sqlQuery&nbsp;=&nbsp;@sqlQuery&#43;&nbsp;<span class="js__string">'&nbsp;WHERE&nbsp;'</span>&nbsp;&#43;&nbsp;@ConditionList&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IF&nbsp;@isGroupBY=<span class="js__num">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SET&nbsp;@sqlQuery&nbsp;=&nbsp;@sqlQuery&#43;&nbsp;<span class="js__string">'&nbsp;GROUP&nbsp;BY&nbsp;'</span>&nbsp;&#43;&nbsp;@GroupBYList&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IF&nbsp;@isOrderBY=<span class="js__num">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SET&nbsp;@sqlQuery&nbsp;=&nbsp;@sqlQuery&#43;&nbsp;<span class="js__string">'&nbsp;Order&nbsp;BY&nbsp;'</span>&nbsp;&#43;&nbsp;@OrderBYList&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EXEC&nbsp;(@sqlQuery)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
ELSE&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BEGIN&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EXEC&nbsp;(@sqlQuery)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;END&nbsp;
END&nbsp;TRY&nbsp;
BEGIN&nbsp;CATCH&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SELECT&nbsp;ERROR_NUMBER()&nbsp;AS&nbsp;ErrorNumber&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,ERROR_MESSAGE()&nbsp;AS&nbsp;ErrorMessage;&nbsp;
END&nbsp;CATCH&nbsp;
END&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<h2><strong>Step 2:</strong><strong> </strong><strong>Create your MVC Web Application in Visual Studio 2015</strong><strong>&nbsp;</strong></h2>
<p>After installing our Visual Studio 2015 click Start, then Programs and select <strong>
Visual Studio 2015</strong> - Click <strong>Visual Studio 2015</strong>. Click New, then Project, select Web and then select
<strong>ASP.NET Web Application</strong>. Enter your project name and click OK.</p>
<p><img id="154032" src="154032-2.png" alt="" width="410" height="234"></p>
<p>Select MVC, WEB API and click OK.</p>
<p><img id="154033" src="154033-3.png" alt="" width="431" height="332"></p>
<p>Now we have created our MVC Application as a next step we add our connection string in our Web.Config file. Here we are not using Entity Frame work .Here we will directly get the data from our MVC Web API controller method using normal ADO.NET method.<strong>&nbsp;</strong></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>

<div class="preview">
<pre class="js">&lt;add&nbsp;name=<span class="js__string">&quot;dashboard&quot;</span>&nbsp;connectionString=<span class="js__string">&quot;Data&nbsp;Source=SQLSERVERNAME;Initial&nbsp;Catalog=DashboardDB;Persist&nbsp;Security&nbsp;Info=True;User&nbsp;ID=UID;Password=PWD&quot;</span>&nbsp;providerName=<span class="js__string">&quot;System.Data.SqlClient&quot;</span>&nbsp;/&gt;</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p>Kindly update with your SQL server connection.<strong>&nbsp;</strong></p>
<h2><strong>Step 3: Add web API Controller</strong></h2>
<p>Right click Controllers folder and click Add and click on Controller.</p>
<p><img id="154034" src="154034-4.png" alt="" width="534" height="159"></p>
<p>Here we will add a WEB API Controller to be used for our AngularJS.</p>
<p>Select Web API 2 Controller &ndash; Empty and click Add .next enter the controller name as DashboardAPIController</p>
<p><img id="154035" src="154035-5.png" alt="" width="492" height="332"></p>
<p><strong>Get Method </strong></p>
<p>Here use the Http Get method to get all our dynamic data from database using normal ADO.Net method.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;string&nbsp;getDashboardDetails(string&nbsp;sqlQuery,&nbsp;string&nbsp;columnName,&nbsp;string&nbsp;tableNames,&nbsp;Nullable&lt;int&gt;&nbsp;isCondition,&nbsp;string&nbsp;conditionList,&nbsp;Nullable&lt;int&gt;&nbsp;isGroupBY,&nbsp;string&nbsp;groupBYList,&nbsp;Nullable&lt;int&gt;&nbsp;isOrderBY,&nbsp;string&nbsp;orderBYList)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;(sqlQuery&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sqlQuery&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(columnName&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;columnName&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(tableNames&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tableNames&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(isCondition&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;isCondition&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(conditionList&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;conditionList&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(isGroupBY&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;isGroupBY&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(groupBYList&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;groupBYList&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(isOrderBY&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;isOrderBY&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(orderBYList&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderBYList&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;connectionString&nbsp;=&nbsp;ConfigurationManager.ConnectionStrings[<span class="js__string">&quot;dashboard&quot;</span>].ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataSet&nbsp;ds&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DataSet();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;using&nbsp;(SqlConnection&nbsp;connection&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlConnection(connectionString))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;Create&nbsp;the&nbsp;SQL&nbsp;command&nbsp;and&nbsp;add&nbsp;Sp&nbsp;name</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand&nbsp;command&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlCommand();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Connection&nbsp;=&nbsp;connection;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.CommandText&nbsp;=&nbsp;<span class="js__string">&quot;USP_Dashboard_Select&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.CommandType&nbsp;=&nbsp;CommandType.StoredProcedure;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;for&nbsp;Query.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@sqlQuery&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter.Value&nbsp;=&nbsp;sqlQuery;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;for&nbsp;Column&nbsp;Names</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter1&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter1.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@columnName&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter1.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter1.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter1.Value&nbsp;=&nbsp;columnName;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter1);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;for&nbsp;Table&nbsp;names</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter2&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter2.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@tableNames&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter2.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter2.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter2.Value&nbsp;=&nbsp;tableNames;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter2);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;to&nbsp;check&nbsp;for&nbsp;&nbsp;Where&nbsp;condition</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter3&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter3.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@isCondition&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter3.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter3.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter3.Value&nbsp;=&nbsp;isCondition;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter3);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;for&nbsp;Where&nbsp;conditions</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter4&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter4.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@ConditionList&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter4.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter4.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter4.Value&nbsp;=&nbsp;conditionList;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter4);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;to&nbsp;check&nbsp;for&nbsp;&nbsp;Group&nbsp;By&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter5&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter5.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@isGroupBY&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter5.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter5.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter5.Value&nbsp;=&nbsp;isGroupBY;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter5);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;for&nbsp;Group&nbsp;By</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter6&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter6.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@groupBYList&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter6.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter6.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter6.Value&nbsp;=&nbsp;groupBYList;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter6);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;to&nbsp;check&nbsp;for&nbsp;Order&nbsp;By</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter7&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter7.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@isOrderBY&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter7.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter7.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter7.Value&nbsp;=&nbsp;isOrderBY;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter7);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;parameter&nbsp;&nbsp;for&nbsp;OrderBY</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlParameter&nbsp;parameter8&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlParameter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter8.ParameterName&nbsp;=&nbsp;<span class="js__string">&quot;@orderBYList&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter8.SqlDbType&nbsp;=&nbsp;SqlDbType.NVarChar;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter8.Direction&nbsp;=&nbsp;ParameterDirection.Input;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter8.Value&nbsp;=&nbsp;orderBYList;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.Add(parameter8);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connection.Open();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;using&nbsp;(SqlDataAdapter&nbsp;da&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SqlDataAdapter(command))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;da.Fill(ds);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connection.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__statement">return</span>&nbsp;DataTableToJSONWithJavaScriptSerializer(ds.Tables[<span class="js__num">0</span>]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<h2><strong>Step 4: </strong><strong>Creating AngularJs Controller</strong></h2>
<p>First create a folder inside the Script Folder and we give the folder name as &ldquo;MyAngular&rdquo;</p>
<p>Now add your Angular Controller inside the folder.</p>
<p>Right Click the MyAngular Folder and click Add and New Item &gt; Select Web &gt; Select AngularJs Controller and give name to Controller. We have given my AngularJs Controller as &ldquo;Controller.js&rdquo;</p>
<p>&nbsp;<img id="154036" src="154036-6.png" alt="" width="602" height="366"></p>
<p>If the Angular JS package is missing then add the package to your project.</p>
<p>Right Click your MVC project and Click-&gt; Manage NuGet Packages. Search for AngularJs and click Install.</p>
<p><img id="154037" src="154037-7.png" alt="" width="266" height="219"></p>
<p><strong>Modules.js:</strong> Here we will add the reference to the AngularJS JavaScript and create an Angular Module named &ldquo;<strong>AngularJs_Module</strong>&rdquo;.&nbsp;</p>
<p><span style="white-space:pre">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span><span class="js__statement">var</span>&nbsp;app;&nbsp;
(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;app&nbsp;=&nbsp;angular.module(<span class="js__string">&quot;dashbordModule&quot;</span>,&nbsp;[<span class="js__string">'ngAnimate'</span>]);&nbsp;
<span class="js__brace">}</span>)();&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><strong>Controllers:</strong> In AngularJS Controller we have done all the business logic and returned the data from Web API to our MVC HTML page.</p>
<p><strong>1. Variable declarations<br>
</strong><br>
Firstly, we declared all the local variables need to be used.&nbsp;</p>
<p><span style="white-space:pre">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js">app.controller(<span class="js__string">&quot;AngularJs_Controller&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;($scope,&nbsp;$filter,&nbsp;$timeout,&nbsp;$rootScope,&nbsp;$window,&nbsp;$http)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.date&nbsp;=&nbsp;<span class="js__operator">new</span><span class="js__object">Date</span>();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.MyName&nbsp;=&nbsp;<span class="js__string">&quot;shanu&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.isQuerys&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Querys&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.ColumnNames&nbsp;=&nbsp;<span class="js__string">&quot;UserName,UserType,Phone&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.TableNames&nbsp;=&nbsp;<span class="js__string">&quot;UserDetails&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.isCondition&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.whereCondition&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Conditions&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.isGroupBy&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.GroupBy&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.GroupBys&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.isOrderBy&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderBy&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderBys&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Array&nbsp;value&nbsp;to&nbsp;check&nbsp;for&nbsp;SQL&nbsp;Injection</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.sqlInjectionArray&nbsp;=&nbsp;[<span class="js__string">'create'</span>,&nbsp;<span class="js__string">'drop'</span>,&nbsp;<span class="js__string">'delete'</span>,&nbsp;<span class="js__string">'insert'</span>,&nbsp;<span class="js__string">'update'</span>,&nbsp;<span class="js__string">'truncate'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'grant'</span>,&nbsp;<span class="js__string">'print'</span>,&nbsp;<span class="js__string">'sp_executesql'</span>,&nbsp;<span class="js__string">'objects'</span>,&nbsp;<span class="js__string">'declare'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'table'</span>,&nbsp;<span class="js__string">'into'</span>,&nbsp;<span class="js__string">'sqlcancel'</span>,&nbsp;<span class="js__string">'sqlsetprop'</span>,&nbsp;<span class="js__string">'sqlexec'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'sqlcommit'</span>,&nbsp;<span class="js__string">'revoke'</span>,&nbsp;<span class="js__string">'rollback'</span>,&nbsp;<span class="js__string">'sqlrollback'</span>,&nbsp;<span class="js__string">'values'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'sqldisconnect'</span>,&nbsp;<span class="js__string">'sqlconnect'</span>,&nbsp;<span class="js__string">'system_user'</span>,&nbsp;<span class="js__string">'schema_name'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'schemata'</span>,&nbsp;<span class="js__string">'information_schema'</span>,&nbsp;<span class="js__string">'dbo'</span>,&nbsp;<span class="js__string">'guest'</span>,&nbsp;<span class="js__string">'db_owner'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'db_'</span>,&nbsp;<span class="js__string">'table'</span>,&nbsp;<span class="js__string">'@@'</span>,&nbsp;<span class="js__string">'Users'</span>,&nbsp;<span class="js__string">'execute'</span>,&nbsp;<span class="js__string">'sysname'</span>,&nbsp;<span class="js__string">'sp_who'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'sysobjects'</span>,&nbsp;<span class="js__string">'sp_'</span>,&nbsp;<span class="js__string">'sysprocesses'</span>,&nbsp;<span class="js__string">'master'</span>,&nbsp;<span class="js__string">'sys'</span>,&nbsp;<span class="js__string">'db_'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'is_'</span>,&nbsp;<span class="js__string">'exec'</span>,&nbsp;<span class="js__string">'end'</span>,&nbsp;<span class="js__string">'xp_'</span>,&nbsp;<span class="js__string">';&nbsp;--'</span>,&nbsp;<span class="js__string">'alter'</span>,&nbsp;<span class="js__string">'begin'</span>,&nbsp;<span class="js__string">'cursor'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'kill'</span>,&nbsp;<span class="js__string">'--'</span>,&nbsp;<span class="js__string">'tabname'</span>,&nbsp;<span class="js__string">'sys'</span>];&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;Declaration&nbsp;for&nbsp;Chart</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.chartQuerys&nbsp;=&nbsp;<span class="js__string">&quot;Select&nbsp;ItemName&nbsp;as&nbsp;Name,SUM(Price)&nbsp;as&nbsp;Value&nbsp;FROM&nbsp;ItemDetail&nbsp;GROUP&nbsp;BY&nbsp;&nbsp;ItemName&nbsp;ORDER&nbsp;BY&nbsp;Value,Name&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.sItemName&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemCount&nbsp;=&nbsp;<span class="js__num">5</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.selectedItem&nbsp;=&nbsp;<span class="js__string">&quot;MOUSE&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.chartTitle&nbsp;=&nbsp;<span class="js__string">&quot;SHANU&nbsp;Item&nbsp;Sales&nbsp;Chart&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.waterMark&nbsp;=&nbsp;<span class="js__string">&quot;SHANU&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.ItemValues&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.ItemNames&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.minsnew&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.maxnew&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><strong>Search Method:</strong></p>
<p>This method we call on search button click. Here we check for all the validation of user entered data before passing all the parameter to our Web API method.In this method we have commented for each condition checking.<strong>
</strong></p>
<p>In this method we call the &quot;searchbildChartData&rdquo; method to bind the select result to the combo box.</p>
<p><span style="white-space:pre">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//search&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.searchDetails&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;1.&nbsp;Check&nbsp;for&nbsp;Select&nbsp;Query&nbsp;-&gt;&nbsp;In&nbsp;this&nbsp;fucntion&nbsp;we&nbsp;check&nbsp;for&nbsp;SQL&nbsp;injection&nbsp;in&nbsp;user&nbsp;entered&nbsp;select&nbsp;query&nbsp;if&nbsp;any&nbsp;key&nbsp;word&nbsp;from&nbsp;the&nbsp;array&nbsp;list&nbsp;is&nbsp;found&nbsp;then&nbsp;we&nbsp;give&nbsp;msg&nbsp;to&nbsp;user&nbsp;to&nbsp;entert&nbsp;he&nbsp;valid&nbsp;select&nbsp;query</span><span class="js__statement">if</span>&nbsp;($scope.isQuerys&nbsp;==&nbsp;true)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($scope.Querys&nbsp;!=&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.whereCondition&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length<span class="js__num">-1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.Querys).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;select&nbsp;query&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;searchTableDetails($scope.Querys,&nbsp;$scope.ColumnNames,&nbsp;$scope.TableNames,&nbsp;$scope.whereCondition,&nbsp;$scope.Conditions,&nbsp;$scope.GroupBy,&nbsp;$scope.GroupBys,&nbsp;$scope.OrderBy,&nbsp;$scope.OrderBys);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;Your&nbsp;Select&nbsp;Query&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Querys&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;2.&nbsp;Check&nbsp;for&nbsp;Column&nbsp;Names&nbsp;-&gt;&nbsp;If&nbsp;user&nbsp;entered&nbsp;the&nbsp;valid&nbsp;column&nbsp;names&nbsp;the&nbsp;details&nbsp;will&nbsp;be&nbsp;checkd&nbsp;and&nbsp;binded&nbsp;in&nbsp;page</span><span class="js__statement">if</span>&nbsp;($scope.ColumnNames&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;the&nbsp;Column&nbsp;Details&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span><span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length&nbsp;-&nbsp;<span class="js__num">1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.ColumnNames).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;Column&nbsp;Names&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;3.&nbsp;Check&nbsp;for&nbsp;Table&nbsp;Names&nbsp;-&gt;&nbsp;If&nbsp;user&nbsp;entered&nbsp;the&nbsp;valid&nbsp;Table&nbsp;names&nbsp;the&nbsp;details&nbsp;will&nbsp;be&nbsp;checkd&nbsp;and&nbsp;binded&nbsp;in&nbsp;page</span><span class="js__statement">if</span>&nbsp;($scope.TableNames&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;the&nbsp;Table&nbsp;Details&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span><span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length&nbsp;-&nbsp;<span class="js__num">1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.TableNames).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;Table&nbsp;Names&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;4.&nbsp;Check&nbsp;for&nbsp;Where&nbsp;condition&nbsp;-&gt;&nbsp;If&nbsp;user&nbsp;check&nbsp;the&nbsp;Where&nbsp;condition&nbsp;check&nbsp;box,&nbsp;the&nbsp;user&nbsp;entered&nbsp;where&nbsp;condition&nbsp;will&nbsp;be&nbsp;added&nbsp;to&nbsp;the&nbsp;select&nbsp;query&nbsp;</span><span class="js__statement">if</span>&nbsp;($scope.isCondition&nbsp;==&nbsp;true)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($scope.Conditions&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;the&nbsp;Where&nbsp;Condition&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span><span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length&nbsp;-&nbsp;<span class="js__num">1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.Conditions).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;Where&nbsp;Condition&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.whereCondition&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.whereCondition&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;5.&nbsp;Check&nbsp;for&nbsp;GroupBy&nbsp;condition&nbsp;-&gt;&nbsp;If&nbsp;user&nbsp;check&nbsp;the&nbsp;GroupBy&nbsp;condition&nbsp;check&nbsp;box,&nbsp;the&nbsp;user&nbsp;entered&nbsp;GroupBy&nbsp;condition&nbsp;will&nbsp;be&nbsp;added&nbsp;to&nbsp;the&nbsp;select&nbsp;query&nbsp;</span><span class="js__statement">if</span>&nbsp;($scope.isGroupBy&nbsp;==&nbsp;true)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($scope.GroupBys&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;the&nbsp;Group&nbsp;By&nbsp;Details&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span><span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length&nbsp;-&nbsp;<span class="js__num">1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.GroupBys).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;GroupBy&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.GroupBy&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.GroupBy&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;6.&nbsp;Check&nbsp;for&nbsp;OrderBy&nbsp;condition&nbsp;-&gt;&nbsp;If&nbsp;user&nbsp;check&nbsp;the&nbsp;OrderBy&nbsp;condition&nbsp;check&nbsp;box,&nbsp;the&nbsp;user&nbsp;entered&nbsp;OrderBy&nbsp;condition&nbsp;will&nbsp;be&nbsp;added&nbsp;to&nbsp;the&nbsp;select&nbsp;query&nbsp;</span><span class="js__statement">if</span>&nbsp;($scope.isOrderBy&nbsp;==&nbsp;true)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($scope.OrderBys&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;the&nbsp;Group&nbsp;By&nbsp;details&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span><span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length&nbsp;-&nbsp;<span class="js__num">1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.OrderBys).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;OrderBy&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderBy&nbsp;=&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderBy&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;searchTableDetails($scope.Querys,&nbsp;$scope.ColumnNames,&nbsp;$scope.TableNames,&nbsp;$scope.whereCondition,&nbsp;$scope.Conditions,&nbsp;$scope.GroupBy,&nbsp;$scope.GroupBys,&nbsp;$scope.OrderBy,&nbsp;$scope.OrderBys);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;7.&nbsp;Check&nbsp;for&nbsp;Chart&nbsp;Select&nbsp;Query&nbsp;-&gt;&nbsp;In&nbsp;this&nbsp;fucntion&nbsp;we&nbsp;check&nbsp;for&nbsp;SQL&nbsp;injection&nbsp;in&nbsp;user&nbsp;entered&nbsp;select&nbsp;query&nbsp;if&nbsp;any&nbsp;key&nbsp;word&nbsp;from&nbsp;the&nbsp;array&nbsp;list&nbsp;is&nbsp;found&nbsp;then&nbsp;we&nbsp;give&nbsp;msg&nbsp;to&nbsp;user&nbsp;to&nbsp;entert&nbsp;he&nbsp;valid&nbsp;select&nbsp;query</span><span class="js__statement">if</span>&nbsp;($scope.chartQuerys&nbsp;!=&nbsp;<span class="js__string">&quot;&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.whereCondition&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;$scope.sqlInjectionArray.length&nbsp;-&nbsp;<span class="js__num">1</span>;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;($filter(<span class="js__string">'lowercase'</span>)($scope.chartQuerys).match($scope.sqlInjectionArray[i]))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Sorry&nbsp;&quot;</span>&nbsp;&#43;&nbsp;$scope.sqlInjectionArray[i]&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;keyword&nbsp;is&nbsp;not&nbsp;accepted&nbsp;in&nbsp;select&nbsp;query&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;searchbildChartData($scope.chartQuerys,&nbsp;$scope.ColumnNames,&nbsp;$scope.TableNames,&nbsp;$scope.whereCondition,&nbsp;$scope.Conditions,&nbsp;$scope.GroupBy,&nbsp;$scope.GroupBys,&nbsp;$scope.OrderBy,&nbsp;$scope.OrderBys);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;Enter&nbsp;Your&nbsp;Chart&nbsp;Select&nbsp;Query&nbsp;!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><strong>Main Search Method</strong></p>
<p>Finally after validation we call our main bind method to pass all the parameter to our WEB API to get the dynamic data from the database.</p>
<p><span style="white-space:pre">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;Main&nbsp;Select&nbsp;and&nbsp;Bind&nbsp;function</span><span class="js__sl_comment">//All&nbsp;query&nbsp;details&nbsp;entered&nbsp;by&nbsp;user&nbsp;after&nbsp;validation&nbsp;this&nbsp;method&nbsp;will&nbsp;be&nbsp;called&nbsp;to&nbsp;bind&nbsp;the&nbsp;result&nbsp;to&nbsp;the&nbsp;Dashboard&nbsp;page.</span><span class="js__operator">function</span>&nbsp;searchTableDetails(sqlQuery,&nbsp;columnName,&nbsp;tableNames,&nbsp;isCondition,&nbsp;conditionList,&nbsp;isGroupBY,&nbsp;groupBYList,&nbsp;isOrderBY,&nbsp;orderBYList)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/DashboardAPI/getDashboardDetails/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;sqlQuery:&nbsp;sqlQuery,&nbsp;columnName:&nbsp;columnName,&nbsp;tableNames:&nbsp;tableNames,&nbsp;isCondition:&nbsp;isCondition,&nbsp;conditionList:&nbsp;conditionList,&nbsp;isGroupBY:&nbsp;isGroupBY,&nbsp;groupBYList:&nbsp;groupBYList,&nbsp;isOrderBY:&nbsp;isOrderBY,&nbsp;orderBYList:&nbsp;orderBYList&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.dashBoadData&nbsp;=&nbsp;angular.fromJson(data);;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//alert($scope.dashBoadData.length);</span><span class="js__sl_comment">//if&nbsp;($scope.dashBoadData.length&nbsp;&gt;&nbsp;0)&nbsp;{</span><span class="js__sl_comment">//}</span><span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><strong>Chart Data Bind Method</strong></p>
<p>This method will be called from our main method to bind the result to combobox to draw our Pie chart.&nbsp;</p>
<p><span style="white-space:pre">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;For&nbsp;binding&nbsp;the&nbsp;Chart&nbsp;result&nbsp;to&nbsp;Listbox&nbsp;before&nbsp;bind&nbsp;result&nbsp;to&nbsp;Chart</span><span class="js__operator">function</span>&nbsp;searchbildChartData(sqlQuery,&nbsp;columnName,&nbsp;tableNames,&nbsp;isCondition,&nbsp;conditionList,&nbsp;isGroupBY,&nbsp;groupBYList,&nbsp;isOrderBY,&nbsp;orderBYList)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/DashboardAPI/getDashboardDetails/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;sqlQuery:&nbsp;sqlQuery,&nbsp;columnName:&nbsp;columnName,&nbsp;tableNames:&nbsp;tableNames,&nbsp;isCondition:&nbsp;isCondition,&nbsp;conditionList:&nbsp;conditionList,&nbsp;isGroupBY:&nbsp;isGroupBY,&nbsp;groupBYList:&nbsp;groupBYList,&nbsp;isOrderBY:&nbsp;isOrderBY,&nbsp;orderBYList:&nbsp;orderBYList&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemData&nbsp;=&nbsp;angular.fromJson(data);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemCount&nbsp;=&nbsp;$scope.itemData.length;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.selectedItem&nbsp;=&nbsp;$scope.itemData[<span class="js__num">0</span>].Name;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.minsnew&nbsp;=&nbsp;$scope.itemData[<span class="js__num">0</span>].Value;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.maxnew&nbsp;=&nbsp;$scope.itemData[$scope.itemData.length<span class="js__num">-1</span>].Value;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<h2><strong>Step 5: Draw Pie Chart for our Dashboard.</strong></h2>
<p>We are using the jQuery to draw our Pie Chart.In Draw Chart button Click event we call the drawPieChart jQuery method to draw our chart .In this method we get chart value and name from the combobox and draw the chart on the Canvas tag which we placed on
 our MVC Dashboard main page.</p>
<p><span style="white-space:pre">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__operator">function</span>&nbsp;drawPieChart()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;lastend&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;XvalPosition&nbsp;=&nbsp;xSpace;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartWidth&nbsp;=&nbsp;(canvas.width&nbsp;/&nbsp;<span class="js__num">2</span>)&nbsp;-&nbsp;xSpace;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chartHeight&nbsp;=&nbsp;(canvas.height&nbsp;/&nbsp;<span class="js__num">2</span>)&nbsp;-&nbsp;(xSpace&nbsp;/&nbsp;<span class="js__num">2</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;widthcalculation&nbsp;=&nbsp;<span class="js__function">parseInt</span>(((<span class="js__function">parseInt</span>(chartWidth)&nbsp;-&nbsp;<span class="js__num">100</span>)&nbsp;/&nbsp;noOfPlots));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Draw&nbsp;Xaxis&nbsp;Line</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//--&nbsp;draw&nbsp;bar&nbsp;X-Axis&nbsp;and&nbsp;Y-Axis&nbsp;Line</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;XLineStartPosition&nbsp;=&nbsp;xSpace;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;yLineStartPosition&nbsp;=&nbsp;xSpace;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;yLineHeight&nbsp;=&nbsp;chartHeight;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;xLineWidth&nbsp;=&nbsp;chartWidth;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;colorval&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;chartTotalResult&nbsp;=&nbsp;getChartTotal();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#DropDownList1&nbsp;option'</span>).each(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__function">isNaN</span>(<span class="js__function">parseInt</span>($(<span class="js__operator">this</span>).val())))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fillStyle&nbsp;=&nbsp;pirChartColor[colorval];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.beginPath();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.moveTo(chartWidth,&nbsp;chartHeight);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Here&nbsp;we&nbsp;draw&nbsp;the&nbsp;each&nbsp;Pic&nbsp;Chart&nbsp;arc&nbsp;with&nbsp;values&nbsp;and&nbsp;size.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.arc(chartWidth,&nbsp;chartHeight&nbsp;&#43;&nbsp;<span class="js__num">6</span>,&nbsp;chartHeight,&nbsp;lastend,&nbsp;lastend&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="js__object">Math</span>.PI&nbsp;*&nbsp;<span class="js__num">2</span>&nbsp;*&nbsp;(<span class="js__function">parseInt</span>($(<span class="js__operator">this</span>).val())&nbsp;/&nbsp;chartTotalResult)),&nbsp;false);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.lineTo(chartWidth,&nbsp;chartHeight);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ctx.fill();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lastend&nbsp;&#43;=&nbsp;<span class="js__object">Math</span>.PI&nbsp;*&nbsp;<span class="js__num">2</span>&nbsp;*&nbsp;(<span class="js__function">parseInt</span>($(<span class="js__operator">this</span>).val())&nbsp;/&nbsp;chartTotalResult);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//END&nbsp;Draw&nbsp;Bar&nbsp;Graph&nbsp;&nbsp;**************==================********************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;colorval&nbsp;=&nbsp;colorval&nbsp;&#43;&nbsp;<span class="js__num">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<img id="154038" src="154038-8.png" alt="" width="549" height="479"></div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<p>&nbsp;</p>
<ul>
<li><em><em>shanuMVCDashboardPart2.zip.</em></em> </li></ul>
<h1>More Information</h1>
<p><em style="font-family:&quot;Segoe UI&quot;,&quot;Lucida Grande&quot;,Verdana,Arial,Helvetica,sans-serif; font-size:13.008px">Change the WEb.Config file connection string to your SQL Server connection.</em></p>
<div class="mcePaste" id="_mcePaste" style="left:-10000px; top:13854px; width:1px; height:1px; overflow:hidden">
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">function</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> drawPieChart() {</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;
</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> lastend = 0;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> XvalPosition = xSpace;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; chartWidth = (canvas.width / 2) - xSpace;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; chartHeight = (canvas.height / 2) - (xSpace / 2);</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; widthcalculation = parseInt(((parseInt(chartWidth) - 100) / noOfPlots));</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:green">//Draw Xaxis Line</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:green">//-- draw bar X-Axis and Y-Axis Line</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> XLineStartPosition = xSpace;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> yLineStartPosition = xSpace;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> yLineHeight = chartHeight;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> xLineWidth = chartWidth;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; colorval = 0;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">var</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> chartTotalResult = getChartTotal();</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; $(</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:#a31515">'#DropDownList1
 option'</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">).each(</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">function</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> () {</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">if</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas"> (isNaN(parseInt($(</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">this</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">).val())))
 {</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">else</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctx.fillStyle = pirChartColor[colorval];</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctx.beginPath();</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctx.moveTo(chartWidth, chartHeight);</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:green">//Here we draw the each Pic Chart arc with values and size.</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctx.arc(chartWidth, chartHeight &#43; 6, chartHeight,
 lastend, lastend &#43;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Math.PI * 2 * (parseInt($(</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">this</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">).val())
 / chartTotalResult)), </span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">false</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">);</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctx.lineTo(chartWidth, chartHeight);</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctx.fill();</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lastend &#43;= Math.PI * 2 * (parseInt($(</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:blue">this</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">).val())
 / chartTotalResult);</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas; color:green">//END Draw Bar Graph&nbsp; **************==================********************</span><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; colorval = colorval &#43; 1;</span></p>
<p class="MsoNormal" style="margin-bottom:0.0001pt; word-break:keep-all"><span lang="EN-US" style="font-size:9.5pt; font-family:Consolas">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</span></p>
<p class="MsoNormal"><span lang="EN-US" style="font-size:9.5pt; line-height:107%; font-family:Consolas">&nbsp;&nbsp;&nbsp; }</span><strong><span lang="EN-US" style="font-size:12pt; line-height:107%; font-family:&quot;Times New Roman&quot;,serif">&nbsp;</span></strong></p>
</div>
