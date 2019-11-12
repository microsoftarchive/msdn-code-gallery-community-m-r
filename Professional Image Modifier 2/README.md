# Professional Image Modifier 2
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
- GDI+
- Silverlight
- SQL Server
- ASP.NET
- Office
- File System
- Win32
- .NET
- Class Library
- User Interface
- Windows Forms
- WPF
- Microsoft Azure
- Windows Phone 7
- DirectX
- .NET Framework 4
- .NET Framework
- Windows
- Visual Basic .NET
- Visual Basic.NET
- VB.Net
- .NET Framework 4.0
- Image manipulation
- Library
- Windows General
- Windows UI
- Windows Phone
- C# Language
- Converter
- WinForms
- printer
- .NET Framework 4.5
- Windows 8
- .NET Framwork
- Graphics Functions
- Visual C Sharp .NET
- System.Drawing.Drawing2D
- Image process
- Windows Phone 7.5
- Manipulation
- .NET 4.5
- Windows Phone 8
- Windows Store app
- .NET Development
- Image Processing
- Digital Signal Processing
- Image Transformation
## Topics
- Controls
- Animation
- Graphics
- C#
- GDI+
- Silverlight
- Authentication
- File System
- Class Library
- User Interface
- Windows Forms
- Graphics and 3D
- Architecture and Design
- Microsoft Azure
- threading
- Images
- customization
- Media
- ImageViewer
- custom controls
- UI Layout
- Windows Form Controls
- 2d graphics
- Audio
- Visual Basic .NET
- Performance
- Image manipulation
- Image Gallery
- Printing
- Windows Phone
- Image
- Console Window
- .NET 4
- Imaging
- Drawing
- How to
- UI Design
- File Systems
- Storage
- Image Optimization
- general
- Windows 8
- Windows Forms Controls
- C# Language Features
- Graphics Functions
- Audio and video
- Devices and sensors
- User Control
- User Experience
- BitmapImage
- Windows Store app
- Load Image
- Dynamically Image
- data and storage
## Updated
- 01/12/2014
## Description

<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>1<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Introduction</span></h1>
</div>
<p class="MsoNormal"><span>Today we are looking at graphic functions today we are looking at Susan Corner Detection.</span></p>
<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>2<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Building the Sample</span></h1>
</div>
<p class="MsoNormal"><span>The sample is built in Visual Studio 2012 Ultimate as an x86 targeted application using .Net Framework 4. We will be using NuGet packages, a number of 3rd party libraries. All of which will be fully explained to you ensuring that
 the final compilation of your application will be hassle free. Oh! And the sample code is verbosely commented so you should have no problem in working out what the code does.</span></p>
<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>3<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Let&rsquo;s Get Started!</span></h1>
</div>
<p class="MsoNormal"><span>The class implements Susan corners detector, which is described by S.M. Smith in: S.M. Smith, &quot;SUSAN - a new approach to low level image processing&quot;, Internal Technical Report TR95SMS1, Defense Research Agency, Chobham Lane, Chertsey,
 Surrey, UK, 1995.</span></p>
<p class="MsoNormal"><span>Some implementation notes:</span></p>
<p class="MsoNormal"><span>Analyzing each pixel and searching for its USAN area, the 7x7 mask is used, which is comprised of 37 pixels. The mask has circle shape:</span></p>
<p class="MsoNoSpacing"><span><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span>&nbsp;</span><span>&nbsp;&nbsp;&nbsp;
</span></span><span style="font-family:&quot;Lucida Console&quot;">xxx</span></p>
<p class="MsoNoSpacing"><span style="font-family:&quot;Lucida Console&quot;"><span>&nbsp;&nbsp;&nbsp;&nbsp;
</span>xxxxx</span></p>
<p class="MsoNoSpacing"><span style="font-family:&quot;Lucida Console&quot;"><span>&nbsp;&nbsp;&nbsp;
</span>xxxxxxx</span></p>
<p class="MsoNoSpacing"><span style="font-family:&quot;Lucida Console&quot;"><span>&nbsp;&nbsp;&nbsp;
</span>xxxxxxx</span></p>
<p class="MsoNoSpacing"><span style="font-family:&quot;Lucida Console&quot;"><span>&nbsp;&nbsp;&nbsp;
</span>xxxxxxx</span></p>
<p class="MsoNoSpacing"><span style="font-family:&quot;Lucida Console&quot;"><span>&nbsp;&nbsp;&nbsp;&nbsp;
</span>xxxxx</span></p>
<p class="MsoNoSpacing"><span style="font-family:&quot;Lucida Console&quot;"><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span>xxx</span></p>
<p class="MsoNormal"><span>&nbsp;</span></p>
<p class="MsoListParagraphCxSpFirst" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>In the case if USAN's center of mass has the same coordinates as nucleus (central point), the pixel is not a corner.</span></p>
<p class="MsoListParagraphCxSpLast" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>For noise suppression the 5x5 square window is used.</span></p>
<p class="MsoNormal"><span>The class processes only grayscale 8 bpp and color 24/32 bpp images. In the case of color image, it is converted to grayscale internally using GrayscaleBT709 filter.</span></p>
<p class="MsoNormal"><span>Open Visual Studio 2012 and select your previous created windows forms application. I will be commenting in this sample using C# but a VB.NET version of the code is available for downloading as well!
</span></p>
<p class="MsoNormal"><span>Corner Detection requires corners, if your image is complicated it will see corners everywhere &ndash; so this is really better for simple images but you can use it on an Image you want.</span></p>
<p class="MsoNormal"><span><img id="107119" src="107119-picture%202014-01-12%2021_20_49.png" alt="" width="973" height="547"></span></p>
<p class="MsoNormal"><span>We have created a &ldquo;Susan Corner Detection Settings&rdquo; user control to allow the end user select the properties they want the Susan Corner Detection to run with. Right Clicking on the Image itself will allow you to reset
 the image to it&rsquo;s original un-modified self.</span></p>
<p class="MsoNormal"><span>We have used the third party, and excellent, open source Aforge Imaging library which handles the actual corner detection work for us. We install this into our application by right clicking on our Solution in the Solutiuon Explorer
 and selecting Manage NuGet Packages for solution&hellip; Search for AForge and install the Aforge.Imaging and Aforge.Controls libraries.</span></p>
<p class="MsoNormal"><span>This means our Susan class looks like this:</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">class</span>&nbsp;Susan&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;CurrentImage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;FrmProcessing&nbsp;processing;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">delegate</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ImageCompleteHandler(List&lt;IntPoint&gt;&nbsp;Corners);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">event</span>&nbsp;ImageCompleteHandler&nbsp;ImageComplete;&nbsp;
&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Susan(<span class="cs__keyword">string</span>&nbsp;CurrentImage)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.CurrentImage&nbsp;=&nbsp;CurrentImage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;GetCorners(<span class="cs__keyword">int</span>&nbsp;diff,&nbsp;<span class="cs__keyword">int</span>&nbsp;geo)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;create&nbsp;corners&nbsp;detector's&nbsp;instance</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SusanCornersDetector&nbsp;scd&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SusanCornersDetector(diff,&nbsp;geo);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;process&nbsp;image&nbsp;searching&nbsp;for&nbsp;corners</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;IntPoint&gt;&nbsp;corners&nbsp;=&nbsp;scd.ProcessImage(AForge.Imaging.Image.FromFile(<span class="cs__keyword">this</span>.CurrentImage));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ImageComplete&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;ImageComplete(corners);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>Which raises an ImageComplete event when the processing has &hellip; well, completed; at which point we process the coordinates returned by Aforge and draw those coordinates onto the image using this method:<br>
<br>
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;DrawCorners(List&lt;IntPoint&gt;&nbsp;Corners)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Load&nbsp;image&nbsp;and&nbsp;create&nbsp;everything&nbsp;you&nbsp;need&nbsp;for&nbsp;drawing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bitmap&nbsp;image&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Bitmap(pbImage.Image);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Graphics&nbsp;graphics&nbsp;=&nbsp;Graphics.FromImage(image);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SolidBrush&nbsp;brush&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SolidBrush(Color.Red);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pen&nbsp;pen&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Pen(brush);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Visualization:&nbsp;Draw&nbsp;3x3&nbsp;boxes&nbsp;around&nbsp;the&nbsp;corners</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(IntPoint&nbsp;corner&nbsp;<span class="cs__keyword">in</span>&nbsp;Corners)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;graphics.DrawRectangle(pen,&nbsp;corner.X&nbsp;-&nbsp;<span class="cs__number">1</span>,&nbsp;corner.Y&nbsp;-&nbsp;<span class="cs__number">1</span>,&nbsp;<span class="cs__number">3</span>,&nbsp;<span class="cs__number">3</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Display</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbImage.Image&nbsp;=&nbsp;image;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;

</pre>
</div>
</div>
</div>
<h2><span><span>3.1<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp; </span>
</span></span><span>Source Code Files</span></h2>
<p>&nbsp;</p>
<ul>
<li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span>Forms - Form1.cs &ndash; The main interface to our application
</span></em></li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span>Forms &ndash; Forms/FrmProcessing.cs &ndash; Display a message that the process currently running will take a little while.</span></em>
</li></ul>
<p>&nbsp;</p>
<p class="MsoListParagraphCxSpMiddle" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span><img id="107120" src="107120-picture%202014-01-12%2021_33_45.png" alt="" width="594" height="90"></span></em><em>&nbsp;</em></p>
<p>&nbsp;</p>
<ul>
<li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span>User Control &ndash; Controls/SusanCornerProperties.cs &ndash; Displays the properties available to set the Susan Corner Detector.</span></em>
</li></ul>
<p>&nbsp;</p>
<p class="MsoListParagraphCxSpLast" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span><img id="107121" src="107121-picture%202014-01-12%2021_34_02.png" alt="" width="351" height="166"></span></p>
<h2><span><span>3.2<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp; </span>
</span></span><span>More Information</span></h2>
<p class="MsoNormal"><span>To convert the solution to a previous version of Visual Studio you can use this free application:
</span><a href="http://vsprojectconverter.codeplex.com/"><span style="font-size:12.0pt; line-height:107%; font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">http://vsprojectconverter.codeplex.com/</span></a><span> or download and use Visual Studio 2013 Express which is
 freely available from Microsoft from here: </span><a href="http://www.visualstudio.com/downloads/download-visual-studio-vs"><span style="font-size:12.0pt; line-height:107%; font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">http://www.visualstudio.com/downloads/download-visual-studio-vs</span></a><span>.</span></p>
<p class="MsoNormal"><span>Cyotek.Windows.Forms.ImageBox.dll (<a href="http://cyotek.com/blog/imagebox-update-version-1-1-0-0">http://cyotek.com/blog/imagebox-update-version-1-1-0-0</a>)</span></p>
<p class="MsoNormal"><span>Win8ProgressRing, used in FrmProcessing is available here: http://www.codeproject.com/Articles/648664/Win8ProgressRing-Control</span></p>
<p class="MsoNormal">The Visual Basic version of this code was generated by the free tool: Instant VB from
<a href="http://www.tangiblesoftwaresolutions.com/">http://www.tangiblesoftwaresolutions.com/</a></p>
<p class="MsoNormal">&nbsp;</p>
