# Microsoft SQL Server Reporting Services (SSRS) Example Reports
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- XML
- SSRS
- T-SQL
- VB script
- SQL Server Reporting Services
- Visual Studio 2015
- SQL Server 2016
## Topics
- SQL Server Reporting Services
- SQL Server 2016
- Example Reports
## Updated
- 01/24/2019
## Description

<h1>
<p style="margin-bottom:16px; color:#24292e; font-size:16px; font-weight:400; margin-top:0px!important">
<a rel="noopener noreferrer" href="https://github.com/SQL-Server-projects/Reporting-Services-examples/blob/master/Images/ReadMe/App.png" target="_blank" style="background-color:initial; color:#0366d6"><img src=":-app.png" alt="" width="64px" align="left" style="border-style:none; max-width:100%; padding-right:20px"></a></p>
</h1>
<h1 style="margin:24px 0px 16px; line-height:1.25; border-bottom:1px solid #eaecef; padding-bottom:0.3em; color:#24292e">
<span style="font-size:x-large"><a id="user-content-microsoft-sql-server-reporting-services-ssrs" class="anchor" href="https://github.com/SQL-Server-projects/Reporting-Services-examples#microsoft-sql-server-reporting-services-ssrs" style="background-color:initial; color:#0366d6; float:left; line-height:1; margin-left:-20px; padding-right:4px"></a>SQL
 Server Reporting Services (SSRS)</span></h1>
<p>&nbsp;</p>
<p>Here are examples of some SQL Server Reporting Services reports. &nbsp;The following examples are used to query the database &amp; report server. I have included some useful scripts and documents as well.&nbsp;This project is now on
<a href="https://github.com/SQL-Server-projects/Reporting-Services-examples">GitHub</a>.&nbsp;</p>
<p>Please rate this code. :)</p>
<p><span style="font-family:&quot;Segoe UI&quot;,&quot;Lucida Grande&quot;,Verdana,Arial,Helvetica,sans-serif; font-size:13.008px">&nbsp;</span><a rel="nofollow" href="https://gitter.im/SqlServerReportingServices?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge" style="color:#0366d6; font-size:16px"><img src=":-68747470733a2f2f6261646765732e6769747465722e696d2f53716c5365727665725265706f7274696e6753657276696365732f4c6f6262792e737667" alt="Join the chat at https://gitter.im/SqlServerReportingServices" style="border-style:none; max-width:100%"></a><span style="color:#24292e; font-size:16px">&nbsp;</span><a title="MIT License Copyright © Anthony Duguid" href="https://github.com/SQL-Server-projects/Reporting-Services-examples/blob/master/LICENSE" style="color:#0366d6; font-size:16px"><img src=":-68747470733a2f2f696d672e736869656c64732e696f2f62616467652f4c6963656e73652d4d49542d79656c6c6f772e737667" alt="License: MIT" style="border-style:none; max-width:100%"></a><span style="color:#24292e; font-size:16px">&nbsp;</span><a href="https://github.com/SQL-Server-projects/Reporting-Services-examples/releases" style="color:#0366d6; font-size:16px"><img src=":-68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f72656c656173652f53514c2d5365727665722d70726f6a656374732f5265706f7274696e672d53657276696365732d6578616d706c65732e7376673f6c6162656c3d6c617465737425323072656c65617365" alt="Latest Release" style="border-style:none; max-width:100%"></a><span style="color:#24292e; font-size:16px">&nbsp;</span><a href="https://github.com/SQL-Server-projects/Reporting-Services-examples" style="color:#0366d6; font-size:16px"><img src=":-68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f636f6d6d6974732d73696e63652f53514c2d5365727665722d70726f6a656374732f5265706f7274696e672d53657276696365732d6578616d706c65732f6c61746573742e737667" alt="Github commits (since latest release)" style="border-style:none; max-width:100%"></a><span style="color:#24292e; font-size:16px">&nbsp;</span><a rel="noopener noreferrer" href="https://camo.githubusercontent.com/9d325f947120cf38cf4b8b36b1b22d7f1cad5eb6/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f63757272656e745f6275696c642d323031362d7265642e737667" target="_blank" style="color:#0366d6; font-size:16px"><img src=":-68747470733a2f2f696d672e736869656c64732e696f2f62616467652f63757272656e745f6275696c642d323031362d7265642e737667" alt="current_build 2016" style="border-style:none; max-width:100%"></a></p>
<h2><span style="font-size:large">Table of Contents</span></h2>
<ul>
<li><span style="font-size:small">File List </span></li><li><span style="font-size:small">Glossary of Terms </span></li><li><span style="font-size:small">Report Variable Examples </span></li><li><span style="font-size:small">Screenshots</span> </li></ul>
<p>&nbsp;</p>
<h2>File List</h2>
<p class="MsoListParagraphCxSpFirst" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:18.0pt; text-indent:-17.85pt">
<span style="font-family:Wingdings">v</span>Miscellaneous&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:54.0pt; text-indent:-17.85pt">
<span style="font-family:Wingdings">&sect;</span>Documentation&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Report Requirements.docx</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Report Style Guide.docx</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Report Unit Testing Checklist.docx</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:54.0pt; text-indent:-17.85pt">
<span style="font-family:Wingdings">&sect;</span>Scripts&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>CandyStripe.vb</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>HeatMapColor.vb</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>HeatMapColorGradate.vb</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>UpdateSubscriptionOwner.sql</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:18.0pt; text-indent:-17.85pt">
<span style="font-family:Wingdings">v</span>Reports&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:54.0pt; text-indent:-17.85pt">
<span style="font-family:Wingdings">&sect;</span>Database Server &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Activity Moniter</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Database Dictionary</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Job Search</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Scheduled Jobs</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:54.0pt; text-indent:-17.85pt">
<span style="font-family:Wingdings">&sect;</span>Report Server &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Data Sources</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Execution Log</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Heat Map Calendar (using execution log)</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Permissions</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Report Documentation</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span>Report List</p>
<p class="MsoListParagraphCxSpMiddle" style="margin-top:0cm; margin-right:0cm; margin-bottom:.0001pt; margin-left:90.0pt; text-indent:-17.85pt">
<span style="font-family:Symbol">&uml;</span><span style="text-indent:-17.85pt">Subscriptions</span></p>
<p>&nbsp;</p>
<p><strong><span style="font-size:medium">Glossary of Terms</span></strong></p>
<h2>
<table class="MsoNormalTable" border="0" cellspacing="0" cellpadding="0" width="0" style="width:416.0pt; margin-left:5.4pt; border-collapse:collapse">
<tbody>
<tr style="height:12.75pt">
<td width="129" style="width:97.0pt; border:solid black 1.0pt; background:black; padding:0cm 5.4pt 0cm 5.4pt; height:12.75pt">
<p class="MsoNormal" style="text-align:center"><strong><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif; color:white">Term</span></strong></p>
</td>
<td width="425" style="width:319.0pt; border:solid black 1.0pt; border-left:none; background:black; padding:0cm 5.4pt 0cm 5.4pt; height:12.75pt">
<p class="MsoNormal" style="text-align:center"><strong><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif; color:white">Meaning</span></strong></p>
</td>
</tr>
<tr style="height:51.0pt">
<td width="129" valign="top" style="width:97.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:51.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">Cartesian Product</span></p>
</td>
<td width="425" valign="top" style="width:319.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:51.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">The Cartesian product, also referred to as a cross-join, returns all the rows in all the tables listed in the query. Each row in the first table is paired with all the rows
 in the second table. This happens when there is no relationship defined between the two tables.</span></p>
</td>
</tr>
<tr style="height:38.25pt">
<td width="129" valign="top" style="width:97.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:38.25pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">RDL</span></p>
</td>
<td width="425" valign="top" style="width:319.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:38.25pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">Report Definition Language (RDL) is a standard proposed by Microsoft for defining reports. RDL is an XML application primarily used with Microsoft SQL Server Reporting Services.</span></p>
</td>
</tr>
<tr style="height:51.0pt">
<td width="129" valign="top" style="width:97.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:51.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">SQL
</span></p>
</td>
<td width="425" valign="top" style="width:319.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:51.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">Structured Query Language is a domain-specific language used in programming and designed for managing data held in a relational database management system (RDBMS), or for
 stream processing in a relational data stream management system (RDSMS).</span></p>
</td>
</tr>
<tr style="height:25.5pt">
<td width="129" valign="top" style="width:97.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:25.5pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">T-SQL</span></p>
</td>
<td width="425" valign="top" style="width:319.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:25.5pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">Transact-SQL (T-SQL) is Microsoft's and Sybase's proprietary extension to SQL.
</span></p>
</td>
</tr>
<tr style="height:25.5pt">
<td width="129" valign="top" style="width:97.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:25.5pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">UNION</span></p>
</td>
<td width="425" valign="top" style="width:319.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:25.5pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">The SQL UNION operator combines the result of two or more SELECT statements.</span></p>
</td>
</tr>
<tr style="height:38.25pt">
<td width="129" valign="top" style="width:97.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:38.25pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">XML</span></p>
</td>
<td width="425" valign="top" style="width:319.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:38.25pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">XML stands for eXtensible Markup Language. XML was designed to store and transport data. XML was designed to be both human- and machine-readable.</span></p>
</td>
</tr>
</tbody>
</table>
</h2>
<h2><span style="font-size:medium"><strong>Report Variable Examples</strong></span></h2>
<h2>
<table class="MsoNormalTable" border="0" cellspacing="0" cellpadding="0" width="0" style="width:511.0pt; margin-left:5.4pt; border-collapse:collapse">
<tbody>
<tr style="height:12.75pt">
<td width="136" valign="bottom" style="width:101.0pt; border:solid black 1.0pt; background:black; padding:0cm 5.4pt 0cm 5.4pt; height:12.75pt">
<p class="MsoNormal"><strong><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif; color:white">Name</span></strong></p>
</td>
<td width="547" valign="bottom" style="width:410.0pt; border:solid black 1.0pt; border-left:none; background:black; padding:0cm 5.4pt 0cm 5.4pt; height:12.75pt">
<p class="MsoNormal"><strong><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif; color:white">Value</span></strong></p>
</td>
</tr>
<tr style="height:15.0pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">ColorTableHeader</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">Gainsboro</span></p>
</td>
</tr>
<tr style="height:15.0pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">FormatDate</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">dd-MMM-yyyy</span></p>
</td>
</tr>
<tr style="height:15.0pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">FormatDateTime</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">dd-MMM-yyyy&nbsp; hh:mm tt</span></p>
</td>
</tr>
<tr style="height:45.0pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:45.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">ReportTitle</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:45.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">=IIF(Parameters!document_map_name.Value = &quot;&lt;N/A&gt;&quot;, Globals!ReportName,<br>
&nbsp;Parameters!document_map_name.Value<br>
&nbsp;&#43; &quot; &quot; <br>
&nbsp;&#43; Globals!ReportName)</span></p>
</td>
</tr>
<tr style="height:247.65pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:247.65pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">ReportParameters</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:247.65pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">=IIF(InStr(Join(Parameters!site_name.Value, &quot; &quot;) , Parameters!all_value.Value)&gt;0, &quot;&quot;,
<br>
&nbsp;&quot;Site(s): &quot; &#43; Join(Parameters!site_name.Value, &quot;; &quot;) <br>
&nbsp;)<br>
&nbsp;&#43; IIF(InStr(Join(Parameters!department_name.Value, &quot; &quot;) , Parameters!all_value.Value)&gt;0, &quot;&quot;,
<br>
&nbsp;Environment.NewLine &#43; &quot;Department(s): &quot; &#43; Join(Parameters!department_name.Value, &quot;; &quot;)
<br>
&nbsp;)<br>
&nbsp;&#43; IIF(InStr(Join(Parameters!category_name.Value, &quot; &quot;) , Parameters!all_value.Value)&gt;0, &quot;&quot;,
<br>
&nbsp;Environment.NewLine &#43; &quot;Category(s): &quot; &#43; Join(Parameters!category_name.Value, &quot;; &quot;)
<br>
&nbsp;)<br>
&nbsp;&#43; IIF(InStr(Join(Parameters!unique_course_name.Value, &quot; &quot;) , Parameters!all_value.Value)&gt;0, &quot;&quot;,
<br>
&nbsp;Environment.NewLine &#43; &quot;Course(s): &quot; &#43; Join(Parameters!unique_course_name.Value, &quot;; &quot;)
<br>
&nbsp;)<br>
&nbsp;&#43; IIF(InStr(Join(Parameters!unique_room_name.Value, &quot; &quot;) , Parameters!all_value.Value)&gt;0, &quot;&quot;,
<br>
&nbsp;Environment.NewLine &#43; &quot;Room(s): &quot; &#43; Join(Parameters!unique_room_name.Label, &quot;; &quot;)
<br>
&nbsp;)<br>
&nbsp;&#43; IIF(InStr(Join(Parameters!unique_staff_name.Value, &quot; &quot;) , Parameters!all_value.Value)&gt;0, &quot;&quot;,
<br>
&nbsp;Environment.NewLine &#43; &quot;Teacher(s): &quot; &#43; Join(Parameters!unique_staff_name.Label, &quot;; &quot;)
<br>
&nbsp;)<br>
&nbsp;&#43; Environment.NewLine <br>
&nbsp;&#43; &quot;(&quot; &#43; DateDiff(DateInterval.WeekOfYear, Parameters!week_date_start.Value, Parameters!week_date_end.Value).ToString() &#43; &quot;) Weeks&quot;
<br>
&nbsp;&#43; IIf(IsNothing(Parameters!week_date_start.Value), &quot;&quot;, &quot; from &quot; &amp; Format(Parameters!week_date_start.Value, Variables!FormatDate.Value))<br>
&nbsp;&#43; IIf(IsNothing(Parameters!week_date_end.Value), &quot;&quot;, &quot; to &quot; &amp; Format(Parameters!week_date_end.Value, Variables!FormatDate.Value))</span></p>
</td>
</tr>
<tr style="height:15.0pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">ReportRunDateTime</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">=Format(DateTime.Now, Variables!FormatDateTime.Value)</span></p>
</td>
</tr>
<tr style="height:33.75pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:33.75pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">UrlReport</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:33.75pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">=Globals!ReportServerUrl &#43; &quot;/ReportServer?&quot;
<br>
&#43; Replace(Globals!ReportFolder, &quot; &quot;, &quot;&#43;&quot;) &#43; &quot;%2f&quot; <br>
&#43; Replace(Globals!ReportName, &quot; &quot;, &quot;&#43;&quot;) &#43; &quot;&amp;rs:Command=Render&quot;</span></p>
</td>
</tr>
<tr style="height:125.25pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; padding:0cm 5.4pt 0cm 5.4pt; height:125.25pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">UrlReportWithParameters</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; padding:0cm 5.4pt 0cm 5.4pt; height:125.25pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">=Variables!UrlReport.Value<br>
&nbsp;&#43; &quot;&amp;document_map_name=&quot; &#43; Parameters!document_map_name.Value <br>
&nbsp;&#43; &quot;&amp;site_name=&quot; &#43; Join(Parameters!site_name.Value, &quot;&amp;site_name=&quot;) <br>
&nbsp;&#43; &quot;&amp;unique_course_name=&quot; &#43; Join(Parameters!unique_course_name.Value, &quot;&amp;unique_course_name=&quot;)
<br>
&nbsp;&#43; &quot;&amp;unique_room_name=&quot; &#43; Join(Parameters!unique_room_name.Value, &quot;&amp;unique_room_name=&quot;)
<br>
&nbsp;&#43; &quot;&amp;unique_staff_name=&quot; &#43; Join(Parameters!unique_staff_name.Value, &quot;&amp;unique_staff_name=&quot;)
<br>
&nbsp;&#43; &quot;&amp;department_name=&quot; &#43; Join(Parameters!department_name.Value, &quot;&amp;department_name=&quot;)
<br>
&nbsp;&#43; &quot;&amp;category_name=&quot; &#43; Join(Parameters!category_name.Value, &quot;&amp;category_name=&quot;)
<br>
&nbsp;&#43; IIf(IsNothing(Parameters!week_date_start.Value), &quot;&amp;week_date_start:isnull=True&quot;, &quot;&amp;week_date_start=&quot; &amp; Format(Parameters!week_date_start.Value, Variables!FormatDate.Value))<br>
&nbsp;&#43; IIf(IsNothing(Parameters!week_date_end.Value), &quot;&amp;week_date_end:isnull=True&quot;, &quot;&amp;week_date_end=&quot; &amp; Format(Parameters!week_date_end.Value, Variables!FormatDate.Value))</span></p>
</td>
</tr>
<tr style="height:15.0pt">
<td width="136" valign="top" style="width:101.0pt; border:solid black 1.0pt; border-top:none; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">UserName</span></p>
</td>
<td width="547" valign="top" style="width:410.0pt; border-top:none; border-left:none; border-bottom:solid black 1.0pt; border-right:solid black 1.0pt; background:#D9D9D9; padding:0cm 5.4pt 0cm 5.4pt; height:15.0pt">
<p class="MsoNormal"><span style="font-size:8.0pt; font-family:&quot;Calibri&quot;,sans-serif">=Right(User!UserID, len(User!UserID)-instr(User!UserID, &quot;\&quot;))</span></p>
</td>
</tr>
</tbody>
</table>
</h2>
<h2><span style="font-size:medium">Screenshots</span></h2>
<h2>
<table>
<tbody>
<tr valign="top">
<td width="33%">
<h6><span style="font-size:xx-small">Data Dictionary&nbsp;</span></h6>
<h6><span style="font-size:xx-small"><a href="https://raw.githubusercontent.com/aduguid/SoftwarePortfolio/master/Images/ReadMe/ssrsdatadictionary.png" target="_blank"><img title="Data Dictionary" src=":-ssrsdatadictionary.png" alt="" width="256px" align="top"></a></span></h6>
<h6><span style="font-size:xx-small">This report is used for querying the data dictionary of a SQL Server database.&nbsp;</span></h6>
</td>
<td width="33%">
<h6><span style="font-size:xx-small">Scheduled Jobs Gantt Chart&nbsp;</span></h6>
<h6><span style="font-size:xx-small"><a href="https://raw.githubusercontent.com/aduguid/SoftwarePortfolio/master/Images/ReadMe/ssrsscheduledjobs.png" target="_blank"><img title="Scheduled Jobs Gantt Chart" src=":-ssrsscheduledjobs.png" alt="" width="256px" align="top"></a></span></h6>
<h6><span style="font-size:xx-small">This report is used for querying the scheduled jobs for a SQL Server database.&nbsp;</span></h6>
</td>
</tr>
<tr valign="top">
<td width="33%">
<h6><span style="font-size:xx-small">Report Listing&nbsp;</span></h6>
<h6><span style="font-size:xx-small"><a href="https://raw.githubusercontent.com/aduguid/SoftwarePortfolio/master/Images/ReadMe/ssrsreportlisting.png" target="_blank"><img title="Report Listing" src=":-ssrsreportlisting.png" alt="" width="256px" align="top"></a></span></h6>
<h6><span style="font-size:xx-small">This report is used for querying the deployed SSRS reports, their subscriptions and their execution logs.&nbsp;</span></h6>
</td>
<td width="33%">
<h6><span style="font-size:xx-small">Report Subscriptions&nbsp;</span></h6>
<h6><span style="font-size:xx-small"><a href="https://raw.githubusercontent.com/aduguid/SoftwarePortfolio/master/Images/ReadMe/ssrsreportsubscriptions.png" target="_blank"><img title="Report Subscriptions" src=":-ssrsreportsubscriptions.png" alt="" width="256px" align="top"></a></span></h6>
<h6><span style="font-size:xx-small">This report is used for querying the deployed SSRS subscriptions.&nbsp;</span></h6>
</td>
</tr>
<tr valign="top">
<td width="33%">
<h6><span style="font-size:xx-small">Report Documentation&nbsp;</span></h6>
<h6><span style="font-size:xx-small"><a href="https://raw.githubusercontent.com/aduguid/SoftwarePortfolio/master/Images/ReadMe/ssrsreportdocumentation.png" target="_blank"><img title="Report Documentation" src=":-ssrsreportdocumentation.png" alt="" width="256px" align="top"></a></span></h6>
<h6><span style="font-size:xx-small">This report is used for querying the deployed report XML.&nbsp;</span></h6>
</td>
<td width="33%">
<h6><span style="font-size:xx-small">Heat Map Calendar</span></h6>
<h6><a href="https://raw.githubusercontent.com/aduguid/SoftwarePortfolio/master/Images/ReadMe/ssrsheatmap_calendar.png" target="_blank" style="font-size:xx-small"><img title="Heat Map Calendar" src=":-ssrsheatmap_calendar.png" alt="" width="256px" align="top"></a></h6>
<h6><span style="font-size:xx-small">This report is a heat map calendar of events. It uses a variable for the base color.</span></h6>
</td>
</tr>
</tbody>
</table>
</h2>
<div></div>
<div class="mcePaste" id="_mcePaste" style="left:-10000px; top:0px; width:1px; height:1px; overflow:hidden">
<table border="0" cellspacing="0" cellpadding="0" width="514" style="border-collapse:collapse; width:386pt">
<colgroup><col width="128" style="width:96pt"><col width="386" style="width:290pt"></colgroup>
<tbody>
<tr height="16" style="height:12.0pt">
<td class="xl63" width="128" height="16" style="height:12pt; width:96pt; font-size:9pt; color:white; font-weight:bold; font-family:Calibri; background:black; border:1pt 0.5pt solid black">
Term</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; color:white; font-weight:bold; font-family:Calibri; background:black; border:1pt 0.5pt solid black">
Meaning</td>
</tr>
<tr height="64" style="height:48.0pt">
<td class="xl63" width="128" height="64" style="height:48pt; width:96pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black; background:#d9d9d9">
Cartesian Product</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black; background:#d9d9d9">
The Cartesian product, also referred to as a cross-join, returns all the rows in all the tables listed in the query. Each row in the first table is paired with all the rows in the second table. This happens when there is no relationship defined between the
 two tables.</td>
</tr>
<tr height="112" style="height:84.0pt">
<td class="xl63" width="128" height="112" style="height:84pt; width:96pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black">
CTE</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black">
A common table expression (CTE) can be thought of as a temporary result set that is defined within the execution scope of a single SELECT, INSERT, UPDATE, DELETE, or CREATE VIEW statement. A CTE is similar to a derived table in that it is not stored as an object
 and lasts only for the duration of the query. Unlike a derived table, a CTE can be self-referencing and can be referenced multiple times in the same query.</td>
</tr>
<tr height="48" style="height:36.0pt">
<td class="xl63" width="128" height="48" style="height:36pt; width:96pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black; background:#d9d9d9">
RDL</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black; background:#d9d9d9">
Report Definition Language (RDL) is a standard proposed by Microsoft for defining reports. RDL is an XML application primarily used with Microsoft SQL Server Reporting Services.</td>
</tr>
<tr height="64" style="height:48.0pt">
<td class="xl63" width="128" height="64" style="height:48pt; width:96pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black">
SQL&nbsp;</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black">
Structured Query Language is a domain-specific language used in programming and designed for managing data held in a relational database management system (RDBMS), or for stream processing in a relational data stream management system (RDSMS).</td>
</tr>
<tr height="32" style="height:24.0pt">
<td class="xl63" width="128" height="32" style="height:24pt; width:96pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black; background:#d9d9d9">
T-SQL</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black; background:#d9d9d9">
Transact-SQL (T-SQL) is Microsoft's and Sybase's proprietary extension to SQL.&nbsp;</td>
</tr>
<tr height="32" style="height:24.0pt">
<td class="xl63" width="128" height="32" style="height:24pt; width:96pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black">
UNION</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; border:0.5pt solid black">
The SQL UNION operator combines the result of two or more SELECT statements.</td>
</tr>
<tr height="48" style="height:36.0pt">
<td class="xl63" width="128" height="48" style="height:36pt; width:96pt; font-size:9pt; font-family:Calibri; background:#d9d9d9; border:0.5pt 0.5pt 1pt solid black">
XML</td>
<td class="xl63" width="386" style="width:290pt; font-size:9pt; font-family:Calibri; background:#d9d9d9; border:0.5pt 0.5pt 1pt solid black">
XML stands for eXtensible Markup Language. XML was designed to store and transport data. XML was designed to be both human- and machine-readable.</td>
</tr>
</tbody>
</table>
</div>
