# Microsoft Office File Viewer in WPF
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- COM
- Interop
- C#
- Win32
- WPF
- XAML
- .NET Framework
## Topics
- COM
- COM Interop
- Microsoft Office
- Microsoft Office Word AddIn
## Updated
- 12/09/2013
## Description

<h1>Introduction:</h1>
<p><span style="font-size:small">Microsoft Office Documents like Word , Excel, PowerPoint are very difficult to View in WPF. I have seen a lot of posts related to viewing office files in forums. So to be straight forward there is no direct method to view office
 documents in WPF. Office files should be converted to a suitable format so that it could be viewed in WPF. Here are the steps to view an office Document in WPF.</span></p>
<h1><span style="font-size:medium">Features Available in the Sample:</span></h1>
<ul>
<li><span style="font-size:small">Open and View microsoft office files like .docx, .xlsx, .pptx
</span></li><li><span style="font-size:small">Convert All the pages of a document to a Image </span>
</li><li><span style="font-size:small">Convert a paticular page to a Image </span></li><li><span style="font-size:small">Convert the document to XPS format</span> </li></ul>
<h1>Steps to Run the application:</h1>
<p><span style="font-size:small">Before running the application,</span></p>
<p><span style="font-size:small">1.) <span>Right click &ldquo;</span><strong><em>References -&gt; Add Reference&rdquo;.&nbsp;</em></strong><span>In the&nbsp;</span><strong><em>Reference Manager</em></strong><span>&nbsp;Dialog. Under&nbsp;</span><strong><em>&ldquo;COM
 -&gt; Type Libraries&rdquo;</em></strong><span>&nbsp;Section.&nbsp;</span>Add the Following Com Components in your References</span></p>
<ul>
<li><span style="font-size:small">Microsoft Office 14.0 Object Library</span> </li><li><span style="font-size:small">Microsoft Word 14.0 Object Library</span> </li><li><span style="font-size:small">Microsoft Excel 14.0 Object Library</span> </li><li><span style="font-size:small">Microsoft PowerPoint 14.0 Object Library</span>
</li></ul>
<p><span style="font-size:small">Ensure that you are having version 12.0 or Above. Which means Microsoft word 2007 or higher versions.</span></p>
<p><span style="font-size:small">Scrrenshot of adding M<span>icrosoft Excel 14.0 Object Library</span></span></p>
<p><span style="font-size:small"><span><img id="104277" src="104277-excel.png" alt="" width="793" height="558"><br>
</span></span></p>
<p><br>
<span style="font-size:small">2.) Then Add a Reference to,</span></p>
<ul>
<li><span style="font-size:small">ReachFramework.dll&nbsp;</span> </li></ul>
<p><img id="104278" src="104278-reachframework.png" alt="" width="795" height="544"></p>
<p>&nbsp;</p>
<h1>Screenshot of the Application:</h1>
<p><img id="104279" src="104279-sc.png" alt="" width="751" height="350"></p>
