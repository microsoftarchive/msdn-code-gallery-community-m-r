# RDLC Report in C#
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- C#
- .NET Framework
- RDLC
## Topics
- Reports
- Reporting
- SQL Server Reporting Services
- RDLC
- ReportViewer
## Updated
- 05/03/2015
## Description

<h1>Introduction</h1>
<p><em>This sample shows how to generate RDLC report in C#, you can generate reports for small, medium and large scale business.</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>Visual Studio 2013, .Net Frameworm 4.5, MS SQL Server 2012</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>This sample shows in detail how to generate RDLC report in c#</em></p>
<p>I have explained a simple tutorial with an example and sample code to create RDLC Report in Windows Forms (WinForms) application using C# and VB.Net.<br>
The RDLC Report in in Windows Forms (WinForms) application will be populated using Typed DataSet. &nbsp; &nbsp;&nbsp;</p>
<h2>Create C# Project</h2>
<p>Here you can take your C# project.</p>
<h2><img id="137142" src="137142-slnrdlc.png" alt="" width="947" height="543"></h2>
<p>&nbsp;</p>
<h2>Create DataSet in Your Project</h2>
<p>Create Dataset to populate RDLC reports in to your project.</p>
<p><img id="137143" src="137143-dataset1.png" alt="" width="984" height="533"></p>
<p>&nbsp;</p>
<h2>ADD RDLC Report in Project</h2>
<p>Now you can add RDLC report into your project.</p>
<p><img id="137144" src="137144-rdlc_report.png" alt="" width="808" height="460"></p>
<p>&nbsp;</p>
<h2>Add Binding Source to your Project</h2>
<p>Now add binding source to your project so that can fill database to populate RDLC report.</p>
<p><img id="137145" src="137145-bindingsource.png" alt="" width="415" height="368"></p>
<p>&nbsp;</p>
<p>Insert Table in your RDLC report</p>
<p>Here you can drag required information to display Header and Footer in report.</p>
<p><img id="137146" src="137146-rdlc_insert_header_footer.png" alt="" width="654" height="467"></p>
<h2>Choose Dataset</h2>
<p>Here you can choose dataset to populate report in C#</p>
<p><img id="137147" src="137147-choose_dataset.png" alt="" width="888" height="544"></p>
<p>&nbsp;</p>
<h2>Add Report Viewer to display RDLC report</h2>
<p>Here we are adding ReportViewer to display report.</p>
<p><img id="137148" src="137148-report_viewer.png" alt="" width="329" height="445"></p>
<h2>Provide Datasource and Path to RDLC Report</h2>
<p>Here we can provide datasource and path of the RDLC report.</p>
<p><img id="137149" src="137149-choose_report1.png" alt="" width="822" height="379"></p>
<h2>Final View of RDLC report</h2>
<p><img id="137150" src="137150-city_details.png" alt="" width="741" height="374"></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;TODO:&nbsp;This&nbsp;line&nbsp;of&nbsp;code&nbsp;loads&nbsp;data&nbsp;into&nbsp;the&nbsp;'DataSet1.tblCityMaster'&nbsp;table.&nbsp;You&nbsp;can&nbsp;move,&nbsp;or&nbsp;remove&nbsp;it,&nbsp;as&nbsp;needed.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.tblCityMasterTableAdapter.Fill(<span class="cs__keyword">this</span>.DataSet1.tblCityMaster);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.reportViewer1.RefreshReport();</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>You can view in detail from here <a title="RDLC reports in C#" href="http://www.technologycrowds.com/2015/04/rdlc-reports-in-csharp.html">
RDLC report in C#</a>.</em> </li></ul>
<h1>More Information</h1>
<p><em>If you go in deep in RDLC report then can view from <a title="RDLC reports in C#" href="RDLC reports in C#">
here</a>&nbsp;</em></p>
<p>Database is under <strong>DB_Script </strong>folder.</p>
<p><em><br>
</em></p>
