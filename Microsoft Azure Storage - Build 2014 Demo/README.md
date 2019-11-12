# Microsoft Azure Storage - Build 2014 Demo
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- Javascript
- CORS
- Microsoft Azure Storage
## Topics
- Microsoft Azure Storage
## Updated
- 05/21/2014
## Description

<h1>Introduction</h1>
<p>This sample was used in the Build 2014 talk &quot;Microsoft Azure Storage - What's New, Best Practices and Patterns&quot;. It demonstrates a Web service that distributes Shared Access Signature (SAS) tokens to clients, a Windows Phone application that uploads photos
 to Azure Storage using a SAS token, and finally a Website that shows uploaded photos using Javascript and Azure Storage's CORS support.</p>
<h1><span>Building the Sample</span></h1>
<ol>
<li>Open the solution in Visual Studio. </li><li>Go to Package Manager Console and execute the following commands:
<ol>
<li>Install-Package jQuery </li><li>Install-Package bootstrap </li><li>Install-Package Modernizr </li><li>Install-Package Respond </li></ol>
</li><li>Open Web.config in the Gallery project and replace &quot;&lt;CONNECTION STRING&gt;&quot; with a valid Azure Storage connection string.
</li><li>Build and run. </li></ol>
<h1><span>Source Code Files</span></h1>
<ul>
<li>Gallery\Controllers\HomeController.cs - Main ASP.NET MVC controller for the Web site. Also responsible for distributing SAS tokens.
</li><li>Gallery\Views\Home\Index.cshtml - Home page of the Web site. Contains Javascript code to download photos from Blob service using CORS and SAS.
</li><li>PhotoUploader\MainPage.xaml.cs - Windows Phone application code to take a photo and upload it to Blob service using SAS.
</li></ul>
<h1>More Information</h1>
<p>Please leave your comments and questions in the Q&amp;A tab. You can also visit our blog at
<a href="http://blogs.msdn.com/b/windowsazurestorage/">http://blogs.msdn.com/b/windowsazurestorage/</a> for more information on Microsoft Azure Storage. The associated talk can be found at
<a href="http://channel9.msdn.com/Events/Build/2014/3-628">http://channel9.msdn.com/Events/Build/2014/3-628</a></p>
