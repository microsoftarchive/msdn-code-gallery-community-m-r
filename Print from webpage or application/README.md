# Print from webpage or application
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- C#
- ASP.NET
## Topics
- Printing
## Updated
- 04/24/2013
## Description

<h1>Introduction</h1>
<p>This demo program shows how to send data to a printer from a webpage. All the printing handling is handled in the PrinterHandler class. So this class can easily be used by other websites or windows forms applications.
<br>
The printing page are setup with Graphics object so images and custom text can be customized.&nbsp;
<br>
<br>
<img id="81104" src="81104-print.png" alt="" width="405" height="428"></p>
<h1><span>Building the Sample</span></h1>
<p>At least Visual Studio 2010 is required to run the sample. &nbsp; <em><br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>This demo program shows how easy it is to print pages from a website. The printing module can also be applied on a windows form application.
<br>
In order to send data to the printer you need to specify a printer name and tray to print from. For some reason sometimes you need to set a space before the tray e.g &quot; Tray 1&quot;.
<br>
When printing a long text the printing module is dividing the text into several lines, else will the text just continue to the right.<br>
The text is setup on the page with the Graphics object, method DrawString which means that you can print the text anywhere on the page, just specify x and y coordinates. Images can also be included on the page and are included with &quot;e.Graphics.DrawImage&quot; in
 the printing module.</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li>
<p><em>Default.aspx - Start page <br>
</em></p>
</li><li>
<p><em><em>PrinterHandler - Setup printer configuration and the page to send to the printer.&nbsp;&nbsp;</em></em></p>
</li></ul>
<h1><br>
Other info</h1>
<p>Margins for the printing page are 20 px for each direction.</p>
<p>&nbsp;</p>
