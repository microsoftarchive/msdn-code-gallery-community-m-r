# Master Detail CRUD Operations using EF and ASP.net MVC 3
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- ADO.NET Entity Framework
- ASP.NET MVC 3
## Topics
- Master Detail CRUD with MVC 3 and EF
## Updated
- 01/17/2012
## Description

<h1>Introduction</h1>
<p>A sample Visual Studio project which shows how to perform Master-Detail CRUD operation using Entity Framework (Code First Approach) and ASP.net MVC 3.<br>
The code illustrates the following topics:<br>
&bull;&nbsp;&nbsp;&nbsp; Creating a data model (SalesMain and SalesSub). <br>
&bull;&nbsp;&nbsp;&nbsp; Performing Master-Detail CRUD operations. <br>
&bull;&nbsp;&nbsp;&nbsp; AJAX post and JSON Data Passing</p>
<h1><span>Getting Started <br>
</span></h1>
<p>To build and run this sample, you must have Visual Studio 2010 SP1 and MVC 3 with the MVC 3 Tools Update installed. Extract the master_detail_MVC3.zip file and open MasterDetail.sln solution.</p>
<p><span style="font-size:20px; font-weight:bold">Running Sample<br>
</span></p>
<p>Open the solution. Press Ctrl and F5. <br>
Then Go to Sales Tab.</p>
<p><br>
<strong>&nbsp;</strong></p>
<p><img src="http://i1.code.msdn.s-msft.com/detail-crud-operations-fbe935ef/image/file/25350/1/save.jpg" alt=""></p>
<p>Fig 1: <strong>Creating New Sales Record with multiple sales Sub Record</strong></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><img src="http://i1.code.msdn.s-msft.com/detail-crud-operations-fbe935ef/image/file/25351/1/edit.jpg" alt="" width="836" height="575"></p>
<p><strong>Fig 2: Editing existing Sales Record with multiple sales Sub Record</strong><em><br>
&nbsp;</em></p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>App_Data</em> folder - Holds the SQL Server&nbsp;Compact database file. </li><li><em>Content</em> - Holds CSS files. </li><li>Controllers - Holds controller classes. </li><li><em>Models</em> folder - Holds model classes. </li><li><em>Properties</em> or <em>MyProject</em> folder - Project properties. </li><li><em>Scripts</em> folder - Script files. </li><li><em>Views</em>&nbsp;folder - Holds view classes. </li><li><em>Global.asax</em> file - Includes database initializer code. </li><li>Web.config file -&nbsp;Includes the connection string to the database. </li><li>SalesMain.cs&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Model Class </li><li>SalesSub.cs&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Model Class </li><li>MasterDetailContext.cs Data Context Class </li><li>SalesController.cs Controller </li><li>Create.cshtml&nbsp;&nbsp; Create and Edit View </li><li>Delete.cshtml&nbsp;&nbsp; Delete View </li><li>Details.cshtml&nbsp; Details View </li><li>Index.cshtml&nbsp; List View </li></ul>
<h1>More Information</h1>
<p><em><a title="http://hasibulhaque.com/?p=200" href="http://hasibulhaque.com/?p=200">http://hasibulhaque.com/?p=200</a></em></p>
