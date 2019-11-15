# Pick Your Azure File Upload Control: Silverlight with TPL or HTML5 with AJAX
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- AJAX
- ASP.NET
- Microsoft Azure
- jQuery
- HTML5
- Windows Azure Storage
- MVC 3
## Topics
- Controls
- Performance
- client side upload and validation
- Windows Azure Application
- Windows Azure Storage
- Task Parallelism
## Updated
- 05/29/2012
## Description

<h1>Introduction</h1>
<p style="text-align:justify">The solution consists of two controls:</p>
<ol>
<li><strong>Silverlight and TPL based control&nbsp;</strong>to upload files faster and more reliably to Windows Azure blob storage. It makes full use of available network bandwidth.
</li><li><strong>HTML5, AJAX, MVC 3 based control&nbsp;</strong>that takes a pessimistic approach towards making file uploads to Windows Azure blob storage. It simply scores big over the traditional file upload control.
</li></ol>
<p><span style="font-size:20px; font-weight:bold">Building The Control</span></p>
<p style="text-align:justify">You require the following prior to getting started with running the attached solution:</p>
<ol>
<li>For <strong>Silverlight Control</strong>:
<ol>
<li>Silverlight 4 </li><li>Visual Studio 2010 (.net 4.0) </li></ol>
</li><li>For <strong>HTML5 Control</strong>:
<ol>
<li>MVC 3 </li><li>Visual Studio 2010 (.net 4.0) </li><li>HTML5 supported browser. IE 10&#43;, FF 3.6&#43; (7&#43; for visuals), Chrome 7&#43; (11&#43; for visuals)
</li></ol>
</li></ol>
<p>After you have loaded the solution, please follow the instructions given below:</p>
<p>For <strong>Silverlight Control</strong> (Uses <span style="text-decoration:underline">
<em>BlobUploader.Web</em></span>, <span style="text-decoration:underline"><em>SLBlobUploader</em></span> and
<span style="text-decoration:underline"><em>SLBlobUploader.Code</em></span> projects in solution):</p>
<ol>
<li style="text-align:justify">Change the Windows Azure storage connection string in config file of the test web project.
</li><li style="text-align:justify">Specify the name of container which you want to use for upload in code behind of test web page.
</li><li style="text-align:justify">By default SAS is generated for 10 minutes. To reconfigure this, change the value in code behind of test web page.
</li></ol>
<p>For <strong>HTML5 Control</strong> (Uses <span style="text-decoration:underline">
<em>BlobUploader.Html5.Web</em></span> project in solution):</p>
<ol>
<li style="text-align:justify">Change the Windows Azure storage connection string in config file of the HTML5 project.
</li><li style="text-align:justify">Specify the name of container which you want to use for upload in Infrastructure.Constants class.
</li><li style="text-align:justify">You can modify chunk size from Index.cshtml and retry count and retry duration from BlockBlobUpload.js file in Scripts folder.
</li></ol>
<p><strong>TIP</strong>: If you don't need any one of the controls, unload the projects associated with the control.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p style="text-align:justify"><strong>Silverlight control</strong>: The details of development, functionality and limitations of this control are present as a field note on
<strong>Windows Azure Portal</strong> at: <a href="http://www.microsoft.com/windowsazure/learn/real-world-guidance/field-notes/silverlight-azure-blob-parallel-upload/" target="_blank">
Click to view field note</a>. <br>
<strong>HTML5 control</strong>: The details of development, functionality and limitations of this control are present as a field note on
<strong>Windows Azure Portal</strong> at:<a href="http://msdn.microsoft.com/en-us/library/windowsazure/hh824678.aspx" target="_blank">Click to view field note</a>.</p>
<p style="text-align:justify"><span style="font-size:20px; font-weight:bold">Screens</span></p>
<p style="text-align:justify"><span style="text-decoration:underline"><em><span style="font-weight:bold; font-size:small">Silverlight Control</span></em></span></p>
<p><strong>1. Control Load</strong></p>
<p><strong><img src="44222-1.jpg" alt="" width="810" height="337"><br>
</strong></p>
<p><strong>2. File Upload in Progress</strong></p>
<p><strong><img src="44223-2.jpg" alt="" width="402" height="142"></strong></p>
<p><strong>3. Successful File Upload (depends on bandwidth)</strong></p>
<p><img src="44224-3.jpg" alt="" width="405" height="142"></p>
<p><strong>4. File Upload Cancelled</strong></p>
<p><strong><img src="44225-4.jpg" alt="" width="402" height="143"></strong></p>
<p><strong>5. SAS Signature Expired (No Active Upload\Active Upload)</strong></p>
<p><strong><strong><img src="44226-5.jpg" alt="" width="403" height="142"><br>
</strong></strong></p>
<p style="text-align:justify"><span style="text-decoration:underline"><em><span style="font-weight:bold; font-size:small">HTML5 Control</span></em></span></p>
<p><strong>1. Control Load</strong></p>
<p><strong><img src="http://i1.code.msdn.s-msft.com/silverlight/silverlight-azure-blob-3b773e26/image/file/45481/1/untitled.jpg" alt="" width="934" height="223"><br>
</strong></p>
<p><strong>2. File Upload in Progress</strong></p>
<p><strong><img src="http://i1.code.msdn.s-msft.com/silverlight/silverlight-azure-blob-3b773e26/image/file/45487/1/capture.jpg" alt="" width="425" height="155"><br>
</strong></p>
<p><strong>3. Successful File Upload (depends on bandwidth)</strong></p>
<p><img src="http://i1.code.msdn.s-msft.com/silverlight/silverlight-azure-blob-3b773e26/image/file/45486/1/start.jpg" alt="" width="425" height="155"></p>
<p><strong>4. File Upload Cancelled</strong></p>
<p><strong><img src="http://i1.code.msdn.s-msft.com/silverlight/silverlight-azure-blob-3b773e26/image/file/45488/1/capture.jpg" alt="" width="425" height="155"><br>
</strong></p>
<p><strong>5. Retry upload of chunk in case of failure.</strong></p>
<p><strong><strong><img src="http://i1.code.msdn.s-msft.com/silverlight/silverlight-azure-blob-3b773e26/image/file/45489/1/capture.jpg" alt="" width="425" height="155"><br>
</strong></strong></p>
<p><strong>6. Upload failed to resume and retries maxed out.</strong></p>
<p><strong><strong><img src="http://i1.code.msdn.s-msft.com/silverlight/silverlight-azure-blob-3b773e26/image/file/45491/1/capture.jpg" alt="" width="425" height="155"><br>
</strong></strong></p>
<h1><span>Source Code Files</span></h1>
<ul>
<li>Source.zip - Contains the solution having source code of both the controls as well as a test website for Silverlight control to assist you with testing the control. Please read the system requirements carefully before working with the samples.
</li></ul>
<h1><span>Doubts?</span></h1>
<p>This tool may not fulfill all your requirements. If you need any help with extending the control for your use, Q&amp;A is open for such discussion. Please do rate the sample and do let me know about your experience with the controls. I would try to respond
 to your queries at the earliest.</p>
