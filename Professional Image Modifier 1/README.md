# Professional Image Modifier 1
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- Controls
- C#
- Direct2D
- Silverlight
- File System
- Windows Security
- Win32
- .NET
- Class Library
- User Interface
- Windows Forms
- Multithreading
- WPF
- Microsoft Azure
- Windows Phone 7
- Windows 7
- threading
- DirectX
- .NET Framework 4
- custom controls
- .NET Framework
- Windows
- Visual Basic .NET
- Visual Basic.NET
- Console
- VB.Net
- Parallel Programming
- .NET Framework 4.0
- Image manipulation
- Library
- Windows General
- Windows UI
- Windows Phone
- C# Language
- Async
- Windows Azure Storage
- WinForms
- Windows Phone 7.1
- printer
- System.Media Namespace
- .NET Framework 4.5
- Windows 8
- C# 3.0
- Graphics Functions
- Visual C Sharp .NET
- System.Drawing.Drawing2D
- System.Windows.Forms.UserControl
- Image process
- Visual Studio 2012
- Windows Phone 7.5
- Manipulation
- Transformation
- .NET 4.5
- Windows 8 Store Apps
- Windows Phone 8
- Windows Store app
- Computer Vision
- CUDA
- Windows Store app Development
- .NET Development
- Windows Phone Development
- Edge Detection
- Windows Desktop App Development
- Image Processing
- Digital Signal Processing
- Image Transformation
- Image Software
## Topics
- Controls
- Animation
- Graphics
- C#
- Asynchronous Programming
- Security
- Silverlight
- Authentication
- File System
- User Controls
- Class Library
- User Interface
- Windows Forms
- Graphics and 3D
- Globalization and Localization
- Architecture and Design
- Multithreading
- Microsoft Azure
- threading
- Images
- customization
- video
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
- Collections
- Cryptography
- Direct3D
- Logging
- VB.Net
- Parallel Programming
- Visual How To
- Image manipulation
- Authorization
- Code Sample
- Automation
- Background thread
- Getting Started
- Image Gallery
- GridView
- Encryption/Decryption
- Printing
- Upload
- Download
- Windows Phone
- Image
- Speech recognition
- Geolocation
- Globalization
- Async
- Console Window
- CPU
- .NET 4
- Imaging
- Drawing
- Chat
- Notifications
- How to
- Threads
- UI Design
- desktop
- File Systems
- Files
- Storage
- Image Optimization
- general
- Windows 8
- Windows Forms Controls
- C# Language Features
- Language Samples
- Graphics Functions
- Cloud
- System.Drawing.Drawing2D
- Audio and video
- Devices and sensors
- User Control
- .Net Programming
- Windows web services
- User Experience
- Async Programming
- BitmapImage
- Windows Store app
- Load Image
- Dynamically Image
- data and storage
- image rendering
- Image Filters
- Image Transformation
## Updated
- 01/10/2014
## Description

<div>
<h1>1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Introduction</h1>
</div>
<p>Today we are looking at graphic functions from zooming, to cropping, rotating, image detection, colour correction and anything else you can think of! Again, this is going to be a series of samples so come back often to see if a new episode has been released.</p>
<div>
<h1><img id="107039" src="107039-picture%202014-01-10%2023_09_39.png" alt="" width="795" height="447"></h1>
<h1>2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Building the Sample</h1>
</div>
<p>The sample is built in Visual Studio 2012 Ultimate as an x86 targeted application using .Net Framework 4. We will be using NuGet packages, a number of 3rd party libraries. All of which will be fully explained to you ensuring that the final compilation of
 your application will be hassle free. Oh! And the sample code is verbosely commented so you should have no problem in working out what the code does.</p>
<div>
<h1>3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Let&rsquo;s Get Started!</h1>
</div>
<p>Open Visual Studio 2012 and create a new windows forms application. I am will be commenting in this sample using C# but a VB.NET version of the code is available for downloading as well!</p>
<p>Before we start adding a lot of image conversion functions to our application we need to create our basic application GUI. This will be increased and improved upon as we continue through the series. Our app currently looks like this:</p>
<p>&nbsp;</p>
<p>We have an image loader with an image drop down list as well as an image viewer on the right which is scrollable and zoomable. We did not create this but adopted a third-party viewer because re-inventing the wheel is the most stupid thing a software developer
 can waste their time on. This is the features that the PictureBox already offers:</p>
<p>&nbsp;</p>
<ul>
<li>Zooming with the mouse is now smoother, and the control attempts to keep the area under the mouse before the zoom in the same area after the zoon.
</li><li>Added a ZoomLevels property which allows you to configure the different zoom levels supported by the control. Now instead of the control trying to guess the next zoom level, it cycles appropriately through the defined levels. Currently ZoomLevels (apart
 from the default series) can only be set at runtime. </li><li>The ZoomIncrement property has been removed due to the introduction of the new zoom levels.
</li><li>New CenterAt and ScrollTo methods allow you to scroll to a given location in the source image.
</li><li>Split shortcut handling into two methods ProcessScrollingShortcuts for handling arrow keys and ProcessImageShortcuts for handling pretty much anything else.
</li><li>Added EnableShortcuts property, allowing the built in keyboard support to be disabled. When this property is true, ProcessImageShortcuts is not called, allowing the control to still be scrolled via the keyboard, but not zoomed etc.
</li><li>Zooming can now be performed by the -/&#43; keys (OemMinus and Oemplus). </li><li>When zooming (except via mouse action), if the AutoCenter property is set, the control will always center the image even when scrollbars are present.
</li><li>Nestable BeginUpdate and EndUpdate methods allow you to disable and enable painting of the control, for example when changing multiple properties at once.
</li><li>Added a new GetSelectedImage method which creates a new Bitmap based on the current selection.
</li><li>Added new FitRectangle method which takes a given rectangle and ensure it fits within the image boundaries
</li><li>The AllowClickZoom property now defaults to false. </li><li>The PointToImage function no longer adds &#43;1 to the result of the function. </li><li>Added a new ZoomToRegion method. This will caculate and appropriate zoom level and scrollbar positions to fit a given rectangle.
</li><li>Added new SelectionMode.Zoom. When this mode is selected, drawing a region will automatically zoom and position the control to fit the region, after which the region is automatically cleared.
</li></ul>
<p>We currently only have two methods in our code one loads a new image and places it in the listbox for later re-selection. And the other is the ListBox SelectedIndexChanged event which loads the image that the user selects from the listbox.</p>
<div>
<h1>4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; btnLoadNewImage_Click(object sender, EventArgs e)</h1>
</div>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;btnLoadNewImage_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DialogResult&nbsp;dr&nbsp;=&nbsp;openFile.ShowDialog();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(dr&nbsp;==&nbsp;System.Windows.Forms.DialogResult.OK)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(<span class="cs__keyword">string</span>&nbsp;file&nbsp;<span class="cs__keyword">in</span>&nbsp;openFile.FileNames)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;listBox1.Items.Add(file);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbImage.Image&nbsp;=&nbsp;Image.FromFile(file);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>All this method does is shows an OpenFileDialog when the button beside the listbox is clicked. It loads one or more files into the listbox and displays that image in the PictureBox.</p>
<div>
<h1>5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; private void listBox1_SelectedIndexChanged(object sender, EventArgs e)</h1>
</div>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;listBox1_SelectedIndexChanged(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(listBox1.SelectedIndex&nbsp;&gt;&nbsp;ilDefault.Images.Count&nbsp;-&nbsp;<span class="cs__number">1</span>)&nbsp;<span class="cs__com">//&nbsp;Must&nbsp;be&nbsp;added&nbsp;by&nbsp;the&nbsp;user</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbImage.Image&nbsp;=&nbsp;Image.FromFile(listBox1.SelectedItem.ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__com">//&nbsp;Must&nbsp;be&nbsp;the&nbsp;default&nbsp;images&nbsp;in&nbsp;the&nbsp;ImageList</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbImage.Image&nbsp;=&nbsp;ilDefault.Images[listBox1.SelectedIndex];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;</pre>
</div>
</div>
</div>
<p></p>
<h2>5.1&nbsp;&nbsp;&nbsp; &nbsp;Source Code Files</h2>
<ul>
<li><em>Source code Form1.cs &ndash; The main interface to our application </em></li><li><em>User Control - </em></li></ul>
<h2>5.2&nbsp;&nbsp;&nbsp; More Information</h2>
<p>To convert the solution to a previous version of Visual Studio you can use this free application:
<a href="http://vsprojectconverter.codeplex.com/">http://vsprojectconverter.codeplex.com/</a> or download and use Visual Studio 2013 Express which is freely available from Microsoft from here:
<a href="http://www.visualstudio.com/downloads/download-visual-studio-vs">http://www.visualstudio.com/downloads/download-visual-studio-vs</a>.</p>
<p>Cyotek.Windows.Forms.ImageBox.dll (http://cyotek.com/blog/imagebox-update-version-1-1-0-0)</p>
<p>The Visual Basic version of this code was generated by the free tool: Instant VB from
<a href="http://www.tangiblesoftwaresolutions.com/">http://www.tangiblesoftwaresolutions.com/</a></p>
<p>&nbsp;</p>
