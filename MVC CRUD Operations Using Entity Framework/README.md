# MVC CRUD Operations Using Entity Framework
## Requires
- Visual Studio 2017
## License
- MIT
## Technologies
- C#
- ASP.NET
- .NET
- .NET Framework
- .NET Framework 4.0
- C# Language
- MVC Examples
- ASP.NET MVC 5
- MVC5
- MVC Scaffolding
## Topics
- C#
- ASP.NET
- Model View Controller (MVC)
- ASP.NET MVC
- Master Detail CRUD with MVC 3 and EF
- MVC Samples
- MVC Example
## Updated
- 01/23/2018
## Description

<h1>Introduction</h1>
<p><em><span style="font-size:small">In this article i am going talkig about MVC CRUD Operations Using Entity Framework 6 without writting any code.before that you should learn about what is MVC &amp; entity framework and basics.</span></em></p>
<p><strong><span style="font-size:x-large">MVC</span></strong></p>
<ul>
<li><span style="font-size:small">The View is responsible for the look and feel.</span>
</li><li><span style="font-size:small">Model represents the real world object and provides data to the View.</span>
</li><li><span style="font-size:small">The Controller is responsible for taking the end user request and loading the appropriate Model and View.</span>
</li></ul>
<h1><span>Building the Sample</span></h1>
<p><em><span style="font-size:small">1.Create sample employee sql table</span></em></p>
<p><em><span style="font-size:small">&nbsp;</span></em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><em><span>SQL</span></em></div>
<div class="pluginLinkHolder"><em><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></em></div>
<em><span class="hidden">mysql</span>

<div class="preview">
<pre class="mysql"><span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">Employee</span>](&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">EmployeeId</span>]&nbsp;[<span class="sql__keyword">int</span>]&nbsp;<span class="sql__keyword">primary</span>&nbsp;<span class="sql__keyword">key</span>&nbsp;<span class="sql__id">identity</span>(<span class="sql__number">1</span>,<span class="sql__number">1</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">FirstName</span>]&nbsp;[<span class="sql__keyword">nvarchar</span>](<span class="sql__number">50</span>)&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">LastName</span>]&nbsp;[<span class="sql__keyword">nvarchar</span>](<span class="sql__number">50</span>)&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Address1</span>]&nbsp;[<span class="sql__keyword">nvarchar</span>](<span class="sql__number">50</span>)&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Address2</span>]&nbsp;[<span class="sql__keyword">nvarchar</span>](<span class="sql__number">50</span>)&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">EmailID</span>]&nbsp;[<span class="sql__keyword">nvarchar</span>](<span class="sql__number">50</span>)&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;)</pre>
</div>
</em></div>
</div>
<div class="endscriptcode"><em>&nbsp;</em></div>
<p>&nbsp;</p>
<p><span style="font-size:small">2.Open Visual studio -&gt; Create New Project Asp.net WebApplication with MVC</span></p>
<p><img id="186790" src="186790-2018-01-23%20(6).png" alt="" width="750" height="450"></p>
<p><span style="font-size:small">3.Right Click on the Project and go to Manage NuGet Packages..</span></p>
<p>&nbsp;</p>
<p><span style="font-size:small"><img id="186791" src="186791-2018-01-23%20(9).png" alt="" width="750" height="450" style="vertical-align:baseline"><br>
</span></p>
<p><span style="font-size:small">4.Click on the browse button then find entity framework. In this article i have selected framework 6</span></p>
<p><img id="186792" src="186792-2018-01-23%20(10).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p><span style="font-size:small">5.Right Click on the project -&gt; go to Add -&gt; New Item - &gt;Select ADO.NET Entity Data Model.Give Modle Name</span></p>
<p><span style="font-size:small"><img id="186793" src="186793-2018-01-23%20(12).png" alt="" width="750" height="450" style="vertical-align:baseline"><br>
</span></p>
<p><span style="font-size:small">6.Click on the&nbsp; New Connection -&gt; Select Data Source as Microsoft SQL Server</span></p>
<p><span style="font-size:small"><img id="186803" src="186803-2018-01-23%20(14).png" alt="" width="750" height="450" style="vertical-align:baseline"></span></p>
<p>&nbsp;</p>
<p><span style="font-size:small">7.Choose Server Name &amp; Database Name Then Click OK</span></p>
<p><img id="186805" src="186805-2018-01-23%20(15).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p><span style="font-size:small">8.By Default Connection String will be generated -&gt; Select Table Data
</span></p>
<p><img id="186806" src="186806-2018-01-23%20(16).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p><img id="186808" src="186808-2018-01-23%20(17).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p><span style="font-size:small">9.Add New Controller with Name as &quot;Employee&quot; the Controller Template as MVC 5 Controller with views,...</span></p>
<p><img id="186809" src="186809-2018-01-23%20(18).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p><img id="186810" src="186810-2018-01-23%20(19).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p>&nbsp;</p>
<p><span style="font-size:small">10.Select Model class,Data context class from combo box.Select Generate View Checkbox and give controller Name.</span></p>
<p><img id="186811" src="186811-2018-01-23%20(20).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p>&nbsp;</p>
<p><img id="186812" src="186812-2018-01-23%20(21).png" alt="" width="750" height="450"></p>
<p><span style="font-size:small"><br>
</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small"><em>11.Change default <span style="font-size:small">
<em>&quot;Home&quot; </em></span>controller</em><em> to&nbsp;</em></span><span class="cs__string"><span style="font-size:small"><em> &quot;</em>Employees&quot;. and Hit F5 to run the sample.</span><br>
</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;RouteConfig&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;RegisterRoutes(RouteCollection&nbsp;routes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;routes.IgnoreRoute(<span class="cs__string">&quot;{resource}.axd/{*pathInfo}&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;routes.MapRoute(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="cs__string">&quot;Default&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;<span class="cs__string">&quot;{controller}/{action}/{id}&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;defaults:&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;controller&nbsp;=&nbsp;<span class="cs__string">&quot;Employees&quot;</span>,&nbsp;action&nbsp;=&nbsp;<span class="cs__string">&quot;Index&quot;</span>,&nbsp;id&nbsp;=&nbsp;UrlParameter.Optional&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<p><img id="186822" src="186822-2018-01-23%20(23).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p>&nbsp;</p>
<p><span style="font-size:small"><em>12.Without write any single of code application is created with CRUD views.</em></span></p>
<p>&nbsp;</p>
<p><span style="font-size:small"><em><img id="186824" src="186824-2018-01-23%20(24).png" alt="" width="750" height="450" style="vertical-align:baseline"></em></span></p>
<p>&nbsp;</p>
<p><span style="font-size:small"><em><img id="186825" src="186825-2018-01-23%20(25).png" alt="" width="750" height="450" style="vertical-align:baseline"></em></span></p>
<p>&nbsp;</p>
<p><img id="186826" src="186826-2018-01-23%20(26).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p>&nbsp;</p>
<p><img id="186827" src="186827-2018-01-23%20(29).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><img id="186828" src="186828-2018-01-23%20(30).png" alt="" width="750" height="450" style="vertical-align:baseline"></p>
<p>&nbsp;</p>
<p><span style="font-size:small"><em><br>
</em></span></p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em>MVC_CRUD_Entity.Zip</em></span> </li></ul>
<p><em>&nbsp;</em></p>
