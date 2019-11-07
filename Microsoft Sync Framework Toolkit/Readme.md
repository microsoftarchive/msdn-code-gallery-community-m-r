# Microsoft Sync Framework Toolkit
## Requires
- Visual Studio 2010
## License
- Custom
## Technologies
- Silverlight
- Windows Phone 7
- Microsoft Sync Framework
- Microsoft Synchronization Services
- HTML5
## Topics
- Data Synchronization
- Database Synchronization
- Offline HTML5 application
- Offline Silverlight application
- Offline Mobile application
## Updated
- 08/10/2012
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The <strong>Sync Framework Toolkit</strong> extends the Sync Framework capabilities for building offline applications, making it easier to expose data for synchronization to applications running on any client platform.&nbsp;
 Sync Framework 2.1 required clients to be based on Windows. This toolkit allows other Microsoft platforms to be used for offline clients such as Silverlight, Windows Phone 7, and Windows Mobile; in addition non-Microsoft platforms such as iPhone, Android,
 and Blackberry can be utilized as well as HTML.</span></p>
<p><span style="font-size:small">The Sync Framework Toolkit provides all the features enabled by the
<a href="http://blogs.msdn.com/b/sync/archive/2010/11/16/sync-framework-4-0-october-2010-ctp-refreshed-on-11-16.aspx">
Sync Framework 4.0 October 2010 CTP</a>. We are releasing the toolkit as source code samples on MSDN with the source code utilizing Sync Framework 2.1.&nbsp; Source code provides the flexibility to customize or extend the capabilities we have provided to suit
 your specific requirements. The client-side source code in the package is released under the Apache 2.0 license and the server-side source code under the MS-LPL license.</span></p>
<p><span style="font-size:small">Please follow the section &quot;Building the sample&quot; to extract the package and build the solutions.
</span></p>
<p><span style="font-size:small">We will keep the <a href="http://social.microsoft.com/Forums/en-US/synclab/threads">
f</a><a href="http://social.microsoft.com/Forums/en-US/synclab/threads">orum</a> used for Sync Framework V4 CTP available for community discussion and community support.</span></p>
<p><span style="font-size:20px; font-weight:bold">Building the sample</span></p>
<p class="introduction"><span style="font-size:small">The Sync Framework Toolkit package includes source code for the toolkit, please extract the .zip file under the folder: &quot;c:\syncopensrc&quot; and rename the C# folder to the name &quot;src&quot;. To compile the source
 code, follow the steps described below:<br>
</span></p>
<ol>
<li>
<div class="introduction"><span style="font-size:small">Install <a href="http://download.microsoft.com/download/1/7/7/177D6AF8-17FA-40E7-AB53-00B7CED31729/vm_web.exe">
Windows Phone Developer Tools 7.0</a> if it is not already installed.</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">Install <a href="http://www.microsoft.com/download/en/details.aspx?id=18149">
Silverlight 4 Tools for Visual Studio 2010</a> if it is not already installed.</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">Start <span class="ui">
Visual Studio 2010</span>.</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">Open <span class="ui">
syncfxtoolkit.sln</span> solution in Visual Studio.</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">Build the solution, which will generate the following files:</span></div>
<ul>
<li>
<div class="introduction"><span style="font-size:small">syncopensrc\bin\SyncSvcUtil.exe</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">syncopensrc\bin\SyncSvcUtilUI.exe</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">syncopensrc\server\Microsoft.Synchronization.Services.dll</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">syncopensrc\Client\WP7\Microsoft.Synchronization.ClientServices.dll</span></div>
</li><li>
<div class="introduction"><span style="font-size:small">syncopensrcClient\Silverlight\Microsoft.Synchronization.ClientServices.dll</span></div>
</li></ul>
</li><li>
<div class="introduction"><span style="font-size:small">Move the folder &quot;Samples&quot; from &quot;c:\syncopensrc\src\Samples&quot; to &quot;c:\syncopensrc\Samples&quot;.</span></div>
</li></ol>
<p><span style="font-size:small">Now you are ready to follow the documentation and start writing your sync service and offline applications using the source code.
</span></p>
<p><span style="font-size:small">(If you have trouble opening the chm file, try r</span><span style="font-size:small">ight-clicking, properties and unblock it. Don't forget to rename the &quot;C#&quot; folder to &quot;src&quot; because it could cause problem when opening the chm
 due to the # symbol in the path.)</span></p>
