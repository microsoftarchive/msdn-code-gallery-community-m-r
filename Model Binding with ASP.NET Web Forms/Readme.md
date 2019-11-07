# Model Binding with ASP.NET Web Forms
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- ADO.NET Entity Framework
- ASP.NET
- Entity Framework
- ASP.NET Web Forms
## Topics
- Data Binding
- Data Access
- Unit Testing
- Repository Pattern
- Code First
- ASP.NET Code Sample Downloads
## Updated
- 02/27/2014
## Description

<h1>Introduction</h1>
<p><em>This sample project accompanies the <a title="Model Binding and Web Forms" href="http://go.microsoft.com/fwlink/?LinkId=286117">
Model Binding and Web Forms</a> tutorial series. The tutorial series describes the steps to create this project.</em></p>
<p>This tutorial series demonstrates basic aspects of using model binding with an ASP.NET Web Forms project. Model binding makes data interaction more straight-forward than dealing with data source objects (such as ObjectDataSource or SqlDataSource). This series
 starts with introductory material and moves to more advanced concepts in later tutorials.</p>
<p>The model binding pattern works with any data access technology. In this tutorial, you will use Entity Framework, but you could use the data access technology that is most familiar to you. From a data-bound server control, such as a GridView, ListView, DetailsView,
 or FormView control, you specify the names of the methods to use for selecting, updating, deleting, and creating data.</p>
<p>This tutorial covers how to use model binding to:</p>
<ol>
<li>retrieve data </li><li>update, delete and create data </li><li>sort, page and filter data </li><li>integrate JQuery UI widgets </li><li>retrieve data based on a query string value </li><li>add business logic layer </li><li>add unit tests </li></ol>
<h1><span>Prerequisites</span></h1>
<p>This downloadable code works with either Visual Studio 2012 or Visual Studio 2013. Unzip the zip file into your Visual Studio Projects directory (My Documents\Visual Studio 2012 [or 2013]\Projects) and open the ContosoUniversity.sln solution.</p>
<p><strong>Install NuGet packages</strong></p>
<p>The NuGet packages that are not installed by default in a Web Forms project were not included in this project. You must manually install those packages for the project to work correctly. To install the packages, in the Package Manager Console, run:</p>
<p>install-package JuiceUI<br>
install-package DynamicDataTemplatesCS (or DynamicDataTemplatesVB)<br>
install-package ASPNet.ScriptManager.jQuery -Version 1.8.3<br>
install-package ASPNet.ScriptManager.jQuery.UI.Combined -Version 1.9.2</p>
<p><strong>Edit the DateTime_Edit.ascx file</strong></p>
<p>The dynamic data template package that was installed in the previous step must be edited to include the popup calender. This change is explained in the
<a href="http://go.microsoft.com/fwlink/?LinkId=286118">Integrating JQuery UI Datepicker with model binding and web forms</a> topic. Open DateTime_Edit.ascx file, and add the following line above the TextBox element:</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;juice:Datepicker ID=&quot;t1&quot; TargetControlID=&quot;TextBox1&quot; MinDate=&quot;1/1/2013&quot; runat=&quot;server&quot; /&gt;</pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;juice</span>:Datepicker&nbsp;<span class="xaml__attr_name">ID</span>=<span class="xaml__attr_value">&quot;t1&quot;</span>&nbsp;<span class="xaml__attr_name">TargetControlID</span>=<span class="xaml__attr_value">&quot;TextBox1&quot;</span>&nbsp;<span class="xaml__attr_name">MinDate</span>=<span class="xaml__attr_value">&quot;1/1/2013&quot;</span>&nbsp;<span class="xaml__attr_name">runat</span>=<span class="xaml__attr_value">&quot;server&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span></pre>
</div>
</div>
</div>
<p>Open DateTime_Edit.ascx.cs file and add the following code to the Page_Load method:</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span><span class="hidden">csharp</span>
<pre class="hidden">Dim ra = CType(Column.Attributes(GetType(RangeAttribute)), RangeAttribute)
If Not ra Is Nothing Then
    t1.MinDate = ra.Minimum.ToString()
    t1.MaxDate = ra.Maximum.ToString()
End If</pre>
<pre class="hidden">RangeAttribute ra = (RangeAttribute)Column.Attributes[typeof(RangeAttribute)];
if (ra != null)
{
    t1.MinDate = ra.Minimum.ToString();
    t1.MaxDate = ra.Maximum.ToString();
}</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Dim</span>&nbsp;ra&nbsp;=&nbsp;<span class="visualBasic__keyword">CType</span>(Column.Attributes(<span class="visualBasic__keyword">GetType</span>(RangeAttribute)),&nbsp;RangeAttribute)&nbsp;
<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;ra&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;t1.MinDate&nbsp;=&nbsp;ra.Minimum.ToString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;t1.MaxDate&nbsp;=&nbsp;ra.Maximum.ToString()&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span></pre>
</div>
</div>
</div>
<p>The project is now ready to run. Press Ctrl&#43;F5 to run without debugging.</p>
<h1><em>&nbsp;</em></h1>
<p><span style="font-family:Consolas; font-size:x-small"><span style="font-family:Consolas; font-size:x-small">&nbsp;</span></span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span style="font-family:Consolas; font-size:x-small"><span style="font-family:Consolas; font-size:x-small">&nbsp;</span></span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span style="font-family:Consolas; font-size:x-small">&nbsp;</span></p>
<p><span style="color:#0000ff; font-family:Consolas; font-size:x-small"><span style="color:#0000ff; font-family:Consolas; font-size:x-small">&nbsp;</span></span></p>
