# Professional Image Modifier 5
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- Controls
- C#
- GDI+
- Silverlight
- ASP.NET
- Office
- File System
- Diagnostics
- Windows Security
- .NET
- Class Library
- User Interface
- Windows Forms
- Architecture and Design
- Microsoft Azure
- XAML
- Windows Phone 7
- Windows 7
- SQL Azure
- threading
- DirectX
- themes
- custom controls
- Web Services
- .NET Framework
- Windows
- Visual Basic .NET
- ASP.NET Web Forms
- Visual Basic.NET
- Console
- VB.Net
- Parallel Programming
- .NET Framework 4.0
- Image manipulation
- Library
- Windows General
- Windows UI
- Network
- DataGridView
- Windows Phone
- C# Language
- Converter
- DirectX SDK
- Async
- Windows Azure Storage
- Task Parallel Library
- Internet
- Networking
- printer
- System.Media Namespace
- Animations
- Windows 8
- C# 3.0
- Graphics Functions
- HttpClient
- Visual C Sharp .NET
- System.Drawing.Drawing2D
- System.Windows.Forms.UserControl
- Image process
- extended controls
- Visual Studio 2012
- Graph API
- Manipulation
- Transformation
- .NET 4.5
- Windows Phone 8
- Windows Store app
- Computer Vision
- .NET Development
- Edge Detection
- Image Blur
- Image Processing
- Image Transformation
- Image Software
## Topics
- Controls
- Animation
- Graphics
- C#
- Asynchronous Programming
- Security
- GDI+
- Authentication
- File System
- User Controls
- Class Library
- User Interface
- Windows Forms
- Graphics and 3D
- Architecture and Design
- Multithreading
- Drag and Drop
- threading
- Images
- customization
- Media
- ImageViewer
- custom controls
- Web Services
- UI Layout
- Windows Form Controls
- 2d graphics
- Audio
- Visual Basic .NET
- Performance
- Cryptography
- VB.Net
- Parallel Programming
- Image manipulation
- Code Sample
- Getting Started
- Image Gallery
- GridView
- Printing
- Download
- Windows Phone
- Image
- Globalization
- Async
- .NET 4
- Imaging
- Drawing
- Notifications
- How to
- Charts
- UI Design
- File Systems
- Files
- Networking
- Storage
- Image Optimization
- general
- Windows Forms Controls
- C# Language Features
- Language Samples
- Graphics Functions
- System.Drawing.Drawing2D
- Audio and video
- Devices and sensors
- User Control
- User Experience
- BitmapImage
- Load Image
- Dynamically Image
- data and storage
- Extension methods
## Updated
- 01/19/2014
## Description

<div>
<h1>1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Introduction</h1>
</div>
<p>As this application gets more complicated with ever more functions, and we have only just started, the user interface started to show its limitations. So today we will do a massive refactoring, and completely overhaul the interface to enable docking.</p>
<div>
<h1>2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Building the Sample</h1>
</div>
<p>The sample is built in Visual Studio 2012 Ultimate as an x86 targeted application using .Net Framework 4. We will be using NuGet packages, a number of 3rd party libraries. All of which will be fully explained to you ensuring that the final compilation of
 your application will be hassle free. Oh! And the sample code is verbosely commented so you should have no problem in working out what the code does.</p>
<p>Our app currently looks like this:</p>
<p>&nbsp;<img id="107490" src="107490-picture%202014-01-19%2009_50_26.png" alt="" width="653" height="367"></p>
<p>As you can see we have done a lot of work over the last couple of days. It is really important that you read this document if you want to adapt this application to your own needs. For you to be able to adapt it you need to understand it!</p>
<h2>2.1&nbsp;&nbsp;&nbsp; Adding The NuGet features.</h2>
<p>We are now using Weifen Luo&rsquo;s &ldquo;DockPanel Suite&rdquo; Which you will install from NuGet. Right click on your solution in the Solution Explorer &ndash; Clever place to put it. Now click on Manage NuGet Packages for Solution. Make sure you have
 selected Online sources on the left side pane and Include Prerelease on the top left dropdown. Search for and install DockPanel Suite &ndash; make sure it&rsquo;s the 3.x version.</p>
<p>Repeat the process for Aforge.Controls, Aforge.Imaging, and Accord.Imaging. You should already have GraphicsMagic installed if not follow the instructions here and come back:
<a href="http://code.msdn.microsoft.com/Professional-Image-5bf5866e">http://code.msdn.microsoft.com/Professional-Image-5bf5866e</a>, read this document to install Cyotek&rsquo;s ImageBox
<a href="http://code.msdn.microsoft.com/Professional-Image-5bf5866e">http://code.msdn.microsoft.com/Professional-Image-5bf5866e</a></p>
<p>I am not going to explain in every detail what I have done over the last three or four days as it would take too long. Just use the code I have provided as a starting block for the next stage of the application.</p>
<p>What I am going to do is describe how the program is working. With this knowledge you will be able to add your own touches to it successfully without spending days trying to work it out for yourself.</p>
<div>
<h1>3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Let&rsquo;s Get Started!</h1>
</div>
<p>All the functionality that was in part 4 is still present with a few refinements and we have added a couple of new features. To open a file click the new file icon on the toolstrip &ndash; we have not implemented it in the menu yet. Select one file at a
 time, we have not implemented multi-load in this incarnation of the app.</p>
<p>We currently have 8 dockable forms which are:</p>
<ul>
<li>FrmConsole: This is our log output screen which we have defaulted to dock at the bottom.
</li><li>FrmCommandBox: This is our command input box which is docked below the Console. This form is not implemented at this time.
</li><li>FrmFileLoader: This is now a thumbnail control container which allows us to easily switch between different images we have had open during this session. This is not fully implemented. When we click on a previous image it reloads it instead of showing us
 the file as we had last modified it &ndash; this will be implemented later. </li><li>FrmHistogram: This form shows the histogram of the current image. </li><li>FrmModificationType: This is where the user decides how they are going to modify their media. This will be enhanced later as more modifications become available.
</li><li>FrmModificationProperties: This form holds the controls that allow the End User to set the properties of the selected Modification Types.
</li><li>FrmStatistics: This shows the user a great deal of information concerning the currently selected file.
</li><li>FrmImageDisplay: This form shows the Image that the user is currently working on, plus any modifications that they have made to the image.
</li><li>We also have the main application Form1 into which all the previously mentioned forms are docked.
</li></ul>
<h2>3.1&nbsp;&nbsp;&nbsp; Form1:</h2>
<p>&nbsp;<img id="107492" src="107492-picture%202014-01-19%2010_36_11.png" alt="" width="483" height="373"></p>
<p>The large central black area contains our DockingPanel. We have a StatusBar at the bottom, A menu and ToolStrip at the top.</p>
<h2>3.2&nbsp;&nbsp;&nbsp; The Code:</h2>
<p>All the forms we create are instantiated in a region cleverly named Forms. Later we will change this so that only forms that are required are instantiated so that we do not waste resources, but for now this is fine. Every time we create a new form we must
 add it to the list.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">        private DeserializeDockContent m_deserializeDockContent;
        private FrmConsole ConsoleWindow = new FrmConsole();
        private FrmFileLoader FileLoader = new FrmFileLoader();
        private FrmHistogram Histogram = new FrmHistogram();
        private FrmModificationProperties ModificationProperties = new FrmModificationProperties();
        private FrmModificationType ModificationTypes = new FrmModificationType();
        private FrmStatistics Statistics = new FrmStatistics();
        private FrmImageDisplay ImageDisplay = new FrmImageDisplay();
        private FrmCommandBox CommandBox = new FrmCommandBox();</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;DeserializeDockContent&nbsp;m_deserializeDockContent;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmConsole&nbsp;ConsoleWindow&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmConsole();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmFileLoader&nbsp;FileLoader&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmFileLoader();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmHistogram&nbsp;Histogram&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmHistogram();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmModificationProperties&nbsp;ModificationProperties&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmModificationProperties();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmModificationType&nbsp;ModificationTypes&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmModificationType();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmStatistics&nbsp;Statistics&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmStatistics();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmImageDisplay&nbsp;ImageDisplay&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmImageDisplay();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmCommandBox&nbsp;CommandBox&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;FrmCommandBox();</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>In Form1_Load We attempt to open a previously saved DockingPanel layout or create the default, which the user can change as they like.</p>
<p>We have implemented a Windows Menu which let&rsquo;s us open and close windows as we choose.</p>
<p><img id="107493" src="107493-picture%202014-01-19%2010_44_36.png" alt="" width="350" height="198" style="float:left; margin-left:10px; margin-right:10px"></p>
<p>&nbsp;Having the ability to open and close windows at the End Users whim means we need to check to see if a Window exists before we tell it to do something, but this is relatively straightforward and as long as we don&rsquo;t forget to check we will have
 no problems.</p>
<p>To Write to the Log window we use the following <span style="background-color:#ffffff; color:#0000ff">
ConsoleWindow.Log(&quot;User Canceled Media Selection &quot;);</span> - the Console Window does the rest including making sure that no illegal cross thread calls are being made.</p>
<p>As the whole program is a series of forms we use a lot of events to communicate back to Form1. Form1 currently subscribes to events from the IdentifyFileType class, and the Histogram, ImageDisplay, Statistics, FileLoader, ModificationTypes and ModificationProperties
 forms. As well as events from the Menu, ToolStrip, and StatusBar. All the events are clearly explained. Form1 calls public methods within the various Forms to get them to process requests from the End User, and from other forms.</p>
<p>For instance, when the End User selects a ModificationType, that type is sent to Form1, which tells ModificationProperties which in turn shows the correct ModificationProperties Control to the End User.</p>
<h2>3.3&nbsp;&nbsp;&nbsp; Changes</h2>
<h3>3.3.1&nbsp;&nbsp;&nbsp; Histogram</h3>
<p>We have added new charts, and histogram options to it&rsquo;s context menu and additional information to the bottom of the chart. However, in images with large palettes the chart looks a little corrupt</p>
<p>&nbsp;<img id="107494" src="107494-picture%202014-01-19%2000_17_49.png" alt="" width="326" height="408" style="float:left"><img id="107495" src="107495-picture%202014-01-19%2000_18_08.png" alt="" width="326" height="348"></p>
<p>By simply making the panel larger the corruption is removed.</p>
<h3>3.3.2&nbsp;&nbsp;&nbsp; Image display</h3>
<p>The Image display has been given a small information box at the bottom.</p>
<p><img id="107496" src="107496-picture%202014-01-19%2011_06_57.png" alt="" width="773" height="516"></p>
<h3>3.3.3&nbsp;&nbsp;&nbsp; File Loader</h3>
<p>Finally, the Fileloader keeps the aspect ratio of the loaded file&rsquo;s thumbnails.</p>
<p><img id="107497" src="107497-picture%202014-01-19%2011_08_27.png" alt="" width="386" height="385"></p>
<p>Tomorrow we will get back to some real coding.</p>
<p>Source Code Files</p>
<ul>
<li><em>Forms - Form1.cs &ndash; The main interface to our application </em></li><li><em>Forms &ndash; Forms/FrmProcessing.cs &ndash; Display a message that the process currently running will take a little while.</em>
</li><li><em>&nbsp;</em><em>&nbsp;</em> </li><li><em>Forms &ndash; FrmAboutBox.cs &ndash; Displays basic information about the app &ndash; will be greatly enhanced in the future.</em>
</li><li><em>&nbsp;</em><em>&nbsp;</em> </li><li><em>Forms &ndash; FrmCommandBox &ndash; Will in the future allow the user to set properties and execute commands and possibly scripts in real time.</em>
</li><li><em>&nbsp;</em><em>&nbsp;</em> </li><li><em>Forms &ndash; FrmConsole &ndash; Shows what is happening to the End User, plus other information.</em>
</li><li><em>&nbsp;</em><em>&nbsp;</em> </li><li><em>Forms &ndash; FileLoader &ndash; Shows icons of previously loaded files</em>
</li><li><em>Forms &ndash; FrmHistogram.cs &ndash; Shows the Histogram associated with the current file.</em>
</li><li><em>Forms &ndash; FrmImageDisplay.cs &ndash; The main image display window</em>
</li><li><em>Forms &ndash; FrmModificationType &ndash; allows the User to decide how they wish to modify the current image</em>
</li><li><em>Forms &ndash; FrmModificationProperties &ndash; shows any properties the user can set the current modification type.</em>
</li><li><em>Forms &ndash; FrmStatistics &ndash; Shows a great deal of information about the current file.</em>
</li><li><em>User Control &ndash; Controls/SusanCornerProperties.cs &ndash; Displays the properties available to set the Susan Corner Detector.</em>
</li><li>User Control &ndash; Controls/MoravecCornerProperties &ndash; Displays the properties available to set the Moravec Corner Detector.
</li><li>User Control &ndash; Controls/HarrisCornerProperties - Displays the properties available to set the Harris Corner Detector.
</li><li>User Control &ndash; Controls/FASTCornerProperties - Displays the properties available to set the FAST Corner Detector.
</li><li>Third Party Application &ndash; GM.EXE the GraphicsMagic command line application. Used for getting Exif Information, and will be used for many more processes later.
</li><li>Third Part Application &ndash; TrID.EXE, used to identify files in a more robust way that simply accepting the extension of the file.
<ul>
</ul>
</li></ul>
<h2>3.4&nbsp;&nbsp;&nbsp; More Information</h2>
<ul>
<li>To convert the solution to a previous version of Visual Studio you can use this free application:
<a href="http://vsprojectconverter.codeplex.com/"><em>http://vsprojectconverter.codeplex.com/</em></a> or download and use Visual Studio 2013 Express which is freely available from Microsoft from here:
<a href="http://www.visualstudio.com/downloads/download-visual-studio-vs"><em>http://www.visualstudio.com/downloads/download-visual-studio-vs</em></a>.
</li><li>Cyotek.Windows.Forms.ImageBox.dll (<a href="http://cyotek.com/blog/imagebox-update-version-1-1-0-0">http://cyotek.com/blog/imagebox-update-version-1-1-0-0</a>)
</li><li>Win8ProgressRing, used in FrmProcessing is available here: <a href="http://www.codeproject.com/Articles/648664/Win8ProgressRing-Control">
http://www.codeproject.com/Articles/648664/Win8ProgressRing-Control</a> </li><li>The Visual Basic version of this code was generated by the tool: Instant VB from
<a href="http://www.tangiblesoftwaresolutions.com/">http://www.tangiblesoftwaresolutions.com/</a> &nbsp;A free version is also available.
</li><li>Moravec Corner Detection: <a href="http://en.wikipedia.org/wiki/Corner_detection#The_Moravec_corner_detection_algorithm">
http://en.wikipedia.org/wiki/Corner_detection#The_Moravec_corner_detection_algorithm</a>
</li><li>Microsoft Chart Controls: <a href="http://www.microsoft.com/en-gb/download/details.aspx?id=14422">
http://www.microsoft.com/en-gb/download/details.aspx?id=14422</a> </li><li>GraphicsMagic <a href="http://sourceforge.net/projects/graphicsmagick/files/graphicsmagick-binaries/1.3.19/">
http://sourceforge.net/projects/graphicsmagick/files/graphicsmagick-binaries/1.3.19/</a>
</li><li>Exif information: <a href="http://en.wikipedia.org/wiki/Exchangeable_image_file_format">
http://en.wikipedia.org/wiki/Exchangeable_image_file_format</a> </li><li>TrID is available from here: <a href="http://mark0.net/soft-trid-e.html">http://mark0.net/soft-trid-e.html</a>
</li></ul>
