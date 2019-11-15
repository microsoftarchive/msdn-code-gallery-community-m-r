# Professional Image Modifier 4
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
- GDI+
- Office
- File System
- Diagnostics
- Windows Shell
- .NET
- User Interface
- Windows Forms
- Windows Phone 7
- threading
- DirectX
- .NET Framework 4
- .NET Framework
- Visual Basic .NET
- Console
- VB.Net
- Parallel Programming
- .NET Framework 4.0
- Image manipulation
- Windows General
- Windows UI
- Network
- Windows Phone
- C# Language
- Converter
- Async
- Office 2010
- WinForms
- printer
- .NET Framework 4.5
- Windows 8
- C# 3.0
- Graphics Functions
- HttpClient
- System.Drawing.Drawing2D
- Image process
- Windows Phone 7.5
- Manipulation
- .NET 4.5
- Windows Phone 8
- Windows Store app
- .NET Development
- Windows Desktop App Development
- Image Processing
- Image Transformation
## Topics
- Controls
- Graphics
- C#
- Asynchronous Programming
- Security
- GDI+
- Authentication
- Azure
- File System
- Class Library
- User Interface
- Windows Forms
- Graphics and 3D
- Architecture and Design
- Multithreading
- Microsoft Azure
- threading
- Images
- Media
- ImageViewer
- custom controls
- UI Layout
- 2d graphics
- Audio
- Visual Basic .NET
- Parallel Programming
- Image manipulation
- Code Sample
- Getting Started
- Image Gallery
- GridView
- Printing
- Download
- Image
- Async
- Console Window
- Imaging
- Drawing
- How to
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
- Audio and video
- Devices and sensors
- User Control
- Windows web services
- User Experience
- BitmapImage
- Load Image
- data and storage
## Updated
- 01/16/2014
## Description

<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>1<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Introduction</span></h1>
</div>
<p class="MsoNormal">Today we have added two more Corner Detection algorythms, and their respective property controls; the Harris and the FAST systems, and added the Exif (Statistics Information) control. We have a great deal to get through so let's get started.<span>Building
 the Sample</span></p>
<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>2<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Building the Sample</span></h1>
</div>
<p class="MsoNormal"><span>The sample is built in Visual Studio 2012 Ultimate as an x86 targeted application using .Net Framework 4. We will be using NuGet packages, a number of 3rd party libraries. All of which will be fully explained to you ensuring that
 the final compilation of your application will be hassle free. Oh! And the sample code is verbosely commented so you should have no problem in working out what the code does.
</span></p>
<p class="MsoNormal"><span>We also use the Graphics Magic Package and the Accord.Net package &ndash; which is making our application large. But still many GBs smaller than Photoshop and Gimp. I will explain the installation of these packages now.</span></p>
<h2><span><span>2.1<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp; </span>
</span></span><span>Graphics Magic Installation</span></h2>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Download GraphicsMagic from here: <a href="http://sourceforge.net/projects/graphicsmagick/files/graphicsmagick-binaries/1.3.19/">
http://sourceforge.net/projects/graphicsmagick/files/graphicsmagick-binaries/1.3.19/</a> Choose the latest release suitable for your computer x86 or x64.</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Run the installation and find the install directory, mine is here or my computer: C:\Program Files\GraphicsMagick-1.3.19-Q16</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Open your application in Visual Studio and Create Two folders. Binn and within Binn another called gm. You cannot call the Binn folder Bin, as Visual Studio creates one for you when you compile your program; hence the standard Binn
 directory.</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Copy gm.exe from the Graphics Magic install folder to the gm folder in your solution.</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Right click on gm.exe after it is copied and select properties.</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Change &ldquo;Copy to Output Directory&rdquo; to Copy Always. This will make sure the file actually is where your application expects it to be after compiling and running.</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Now copy ALL the .dll files to the root of your application (where you have Form1.cs) and Program.cs &ndash; There are a lot! This is what makes our application larger &ndash; but also gives us a lot of power. Select all the newly
 copied files and change their properties to Copy Always as well. Just select them all and right click and select properties to do this.</span></p>
<p class="MsoNoSpacing" style="margin-left:36.0pt; text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Save your progress!</span></p>
<h2><span><span>2.2<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp; </span>
</span></span><span>Accord.Net Installation</span></h2>
<p class="MsoListParagraphCxSpFirst" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>You will be glad to hear that this is quicker and easier. Right click on your Solution name in the Solution Explorer and select Manage NuGet Packages&hellip; Search for Accord.Net and install the Imaging and install it &ndash; it
 will also install any dependencies it needs.</span></p>
<p class="MsoListParagraphCxSpLast" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>That&rsquo;s you. Save your progress.</span></p>
<p><span>&nbsp;</span></p>
<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>3<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Let&rsquo;s Get Started!</span></h1>
</div>
<p class="MsoNormal"><span>The Harris corner detection algorithm</span></p>
<p class="MsoNormal"><span>This is an improvement over the Moravec detector shown in the previous sample. Harris and Stephensimproved upon Moravec's corner detector by considering the differential of the corner score with respect to direction directly, instead
 of using shifted patches. (This corner score is often referred to as autocorrelation, since the term is used in the paper in which this detector is described. However, the mathematics in the paper clearly indicate that the sum of squared differences is used.)</span></p>
<p class="MsoNormal"><span>Corner Detection requires corners, if your image is complicated it will see corners everywhere &ndash; so this is really better for simple images but you can use it on an Image you want.</span></p>
<p class="MsoNormal"><span><img id="107378" src="107378-picture%202014-01-15%2023_23_57.png" alt="" width="601" height="338"><br>
</span></p>
<p class="MsoNormal"><span>We have created a &ldquo;Harris Corner Detection Settings&rdquo; user control to allow the end user to select the properties they want the Harris Corner Detection to run with. Right Clicking on the Image itself will allow you to
 reset the image to its original un-modified self.</span></p>
<p class="MsoNormal"><span>We have used the third party, and excellent, open source Accord Imaging library which handles the actual corner detection work for us.
</span></p>
<p class="MsoNormal"><span>This means our Harris class looks like this:</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">class</span>&nbsp;Harris&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;CurrentImage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">delegate</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ImageCompleteHandler(List&lt;IntPoint&gt;&nbsp;Corners);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">event</span>&nbsp;ImageCompleteHandler&nbsp;ImageComplete;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Harris(<span class="cs__keyword">string</span>&nbsp;CurrentImage)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.CurrentImage&nbsp;=&nbsp;CurrentImage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;GetCorners(<span class="cs__keyword">int</span>&nbsp;threshold,&nbsp;<span class="cs__keyword">int</span>&nbsp;sigma)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;create&nbsp;corners&nbsp;detector's&nbsp;instance</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HarrisCornersDetector&nbsp;hcd&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;HarrisCornersDetector(HarrisCornerMeasure.Noble,threshold,sigma);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Apply&nbsp;the&nbsp;filter&nbsp;and&nbsp;return&nbsp;the&nbsp;points</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;IntPoint&gt;&nbsp;corners&nbsp;=&nbsp;hcd.ProcessImage(AForge.Imaging.Image.FromFile(<span class="cs__keyword">this</span>.CurrentImage));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ImageComplete&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;ImageComplete(corners);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p class="MsoNormal"><span>Which raises an ImageComplete event when the processing has &hellip; well, completed; at which point we process the coordinates returned by Accord.Net and draw those coordinates onto the image using the DrawCorners() method explained
 in the previous article. We did the exact same with the FAST class as well. We then created two controls which are self-explanatory; and is easily found in the &hellip; well Controls folder of the solution.</span></p>
<p class="MsoNoSpacing">&nbsp;</p>
<div style="border:none; border-bottom:solid #595959 1.0pt; padding:0cm 0cm 1.0pt 0cm; margin-left:21.3pt; margin-right:0cm">
<h1 style="margin-left:21.6pt"><span><span>4<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span>The Exif Statistics</h1>
</div>
<p class="MsoNormal"><span>It is always nice to have some information about the images you are viewing and modifying and Exif is the best information source found in numerous media types. There are a number of complex and less than friendly ways of extracting
 Exif data, but in truth it is best left to experts who spend all their time devoted to making a good Exif parser &ndash; it is not as easy as it may seem. You can find a lot of information about Exif here including the many problems that surround Exif. (<a href="http://en.wikipedia.org/wiki/Exchangeable_image_file_format">http://en.wikipedia.org/wiki/Exchangeable_image_file_format</a>)</span></p>
<p class="MsoNormal"><span>&nbsp;</span></p>
<p class="MsoNormal"><span>When an image is loaded we collect the Exif data, if it has any, and any other statistics that we can find. This is displayed in our Statistics Tab. The task runs as follows.
</span></p>
<p class="MsoListParagraphCxSpFirst" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Run GM.exe as a child process of our application.</span></p>
<p class="MsoListParagraphCxSpMiddle" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Give it the arguments identify &ndash; verbose &lt;our filename&gt;. Our filename needs to be enclosed in quotes as the path may have spaces in which could throw and exception.</span></p>
<p class="MsoListParagraphCxSpMiddle" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>We have also provided the &ldquo;process&rdquo;, not the gm.exe, how the gm.exe should be run. We tell it not to create a window for displaying its output because of this, we redirect StandardOutput so that we can grab the output
 and save it for later parsing.</span></p>
<p class="MsoListParagraphCxSpLast" style="text-indent:-18.0pt"><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Now we have the output we need to parse it. Again, Exif is not always kind to us, the following code parses the output &ndash; I wont explain it, it will only make you cry. It did me. If however, you do want it explained, you can
 ask in the Q&amp;A section.</span></p>
<p>&nbsp;</p>
<p class="MsoNormal">&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Process&nbsp;the&nbsp;results&nbsp;of&nbsp;GM.EXE</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OutputDataReceived(<span class="cs__keyword">string</span>&nbsp;output)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;lines&nbsp;=&nbsp;output.Split(<span class="cs__string">'\r'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;result&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>(<span class="cs__keyword">int</span>&nbsp;idx&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;idx&nbsp;&lt;&nbsp;lines.Count();&nbsp;idx&nbsp;&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;parts&nbsp;=&nbsp;lines[idx].Replace(<span class="cs__string">&quot;\\&quot;</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>).Split(<span class="cs__string">':'</span>);&nbsp;<span class="cs__com">//&nbsp;May&nbsp;not&nbsp;produce&nbsp;desired&nbsp;results&nbsp;if&nbsp;part[1]&nbsp;contains&nbsp;a&nbsp;:&nbsp;But&nbsp;we&nbsp;will&nbsp;know&nbsp;that&nbsp;if&nbsp;there&nbsp;are&nbsp;more&nbsp;that&nbsp;2&nbsp;parts.</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stats.Add(parts[<span class="cs__number">0</span>].ToString());&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(idx&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;This&nbsp;contains&nbsp;the&nbsp;path&nbsp;to&nbsp;the&nbsp;file&nbsp;so&nbsp;needs&nbsp;special&nbsp;treatment&nbsp;*fudging*</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;=&nbsp;lines[idx].Replace(parts[<span class="cs__number">0</span>].ToString(),<span class="cs__string">&quot;&quot;</span>).Substring(<span class="cs__number">2</span>).Trim();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(parts.Count()&nbsp;&gt;&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;idx2&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;idx2&nbsp;&lt;&nbsp;parts.Count();&nbsp;idx2&#43;&#43;)&nbsp;<span class="cs__com">//&nbsp;Start&nbsp;loop&nbsp;on&nbsp;1&nbsp;as&nbsp;we&nbsp;already&nbsp;added&nbsp;part[0]&nbsp;to&nbsp;the&nbsp;array.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;&#43;=&nbsp;parts[idx2].ToString().Trim();&nbsp;<span class="cs__com">//&nbsp;Remove&nbsp;additional&nbsp;spaces</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stats.Add(result);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<p><span>All this is run on a background task so that we have a nice and speedy application, although the actual creation of the Statistics data and drawing the data to the GridView is very quick indeed. Nevertheless, because it is run in a background task
 we need to invoke the function to ensure that there are no cross thread communication.</span></p>
<p>&nbsp;</p>
<p class="MsoNormal"><span style="font-size:9.5pt; line-height:107%; font-family:Consolas; color:gainsboro">&nbsp;</span></p>
<h1 class="MsoNormal"><span>Source Code Files</span></h1>
<ul>
<li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span>Forms - Form1.cs &ndash; The main interface to our application
</span></em><span>&nbsp;</span> </li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span>Forms &ndash; Forms/FrmProcessing.cs &ndash; Display a message that the process currently running will take a little while.</span></em>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><img id="107380" src="107380-picture%202014-01-12%2021_33_45.png" alt="" width="594" height="90"><em><span>&nbsp;</span></em>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><em><span>User Control &ndash; Controls/SusanCornerProperties.cs &ndash; Displays the properties available to set the Susan Corner Detector.</span></em><span>&nbsp;</span>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><img id="107381" src="107381-picture%202014-01-12%2021_34_02.png" alt="" width="351" height="166"><span>&nbsp;</span>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>User Control &ndash; Controls/MoravecCornerProperties &ndash; Displays the properties available to set the Moravec Corner Detector.</span>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><img id="107383" src="107383-picture%202014-01-14%2015_21_23.png" alt="" width="352" height="166"><span>&nbsp;</span>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>User Control &ndash; Controls/HarrisCornerProperties - Displays the properties available to set the Harris Corner Detector.</span>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span><img id="107384" src="107384-picture%202014-01-15%2023_24_59.png" alt="" width="351" height="167">
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>User Control &ndash; Controls/FASTCornerProperties - Displays the properties available to set the FAST Corner Detector.</span>
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span><img id="107385" src="107385-picture%202014-01-15%2023_24_35.png" alt="" width="351" height="164">
</li><li><span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Third Party Application &ndash; GM.EXE the GraphicsMagic command line application. Used for getting Exif Information, and will be used for many more processes later.</span>
</li></ul>
<h2><span><span>4.1<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp; </span>
</span></span><span>More Information</span></h2>
<p class="MsoListParagraphCxSpFirst" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>To convert the solution to a previous version of Visual Studio you can use this free application:
</span><a href="http://vsprojectconverter.codeplex.com/"><span style="font-size:12.0pt; line-height:107%; font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">http://vsprojectconverter.codeplex.com/</span></a><span> or download and use Visual Studio 2013 Express which is
 freely available from Microsoft from here: </span><a href="http://www.visualstudio.com/downloads/download-visual-studio-vs"><span style="font-size:12.0pt; line-height:107%; font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">http://www.visualstudio.com/downloads/download-visual-studio-vs</span></a><span>.</span></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Cyotek.Windows.Forms.ImageBox.dll (</span><a href="http://cyotek.com/blog/imagebox-update-version-1-1-0-0"><span>http://cyotek.com/blog/imagebox-update-version-1-1-0-0</span></a><span>)</span></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><span>Win8ProgressRing, used in FrmProcessing is available here: http://www.codeproject.com/Articles/648664/Win8ProgressRing-Control</span></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img id="107406" src="http://i1.code.msdn.s-msft.com/professional-image-5bf5866e/image/file/107406/1/picture%202014-01-16%2013_38_55.png" alt="" width="159" height="141" style="float:left">&nbsp;&nbsp;
</span></span></span>The Visual Basic version of this code was generated by the tool: Instant VB from
<a href="http://www.tangiblesoftwaresolutions.com/">http://www.tangiblesoftwaresolutions.com/</a></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span>Moravec Corner Detection: <a href="http://en.wikipedia.org/wiki/Corner_detection#The_Moravec_corner_detection_algorithm">
<span>http://en.wikipedia.org/wiki/Corner_detection#The_Moravec_corner_detection_algorithm</span></a><span>&nbsp;</span></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span class="MsoHyperlink"><span style="font-family:Symbol; color:windowtext; text-decoration:none"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span></span>Microsoft Chart Controls: <a href="http://www.microsoft.com/en-gb/download/details.aspx?id=14422">
http://www.microsoft.com/en-gb/download/details.aspx?id=14422</a><span class="MsoHyperlink">&nbsp;</span></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span>GraphicsMagic <a href="http://sourceforge.net/projects/graphicsmagick/files/graphicsmagick-binaries/1.3.19/">
http://sourceforge.net/projects/graphicsmagick/files/graphicsmagick-binaries/1.3.19/</a></p>
<p class="MsoListParagraphCxSpMiddle" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span>Exif information: <a href="http://en.wikipedia.org/wiki/Exchangeable_image_file_format">
http://en.wikipedia.org/wiki/Exchangeable_image_file_format</a></p>
<p class="MsoListParagraphCxSpLast" style="margin-left:42.9pt; text-indent:-21.6pt">
<span style="font-family:Symbol"><span>&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span>&nbsp;</p>
