# Professional WebBrowser - Step-By-Step - Part 1
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- COM
- C#
- AJAX
- ASP.NET
- File System
- Win32
- .NET
- Class Library
- ADO.NET
- WMI
- User Interface
- Windows Forms
- Microsoft Azure
- XAML
- Windows Phone 7
- Web Services
- .NET Framework
- Javascript
- Windows
- Visual Basic .NET
- VB.Net
- .NET Framework 4.0
- Windows General
- Windows Phone
- C# Language
- Silverlight 5
- Internet Explorer
- Async
- HTML5
- WinForms
- Windows 8
- Graphics Functions
- HttpClient
- System.Drawing.Drawing2D
- Image process
- Windows Azure SQL Database
- Visual Studio 2012
- Windows PowerShell 3.0
- Windows 8 Store Apps
- Windows Phone 8
- Windows Store app
- .NET Development
- Windows Phone Development
- Windows Desktop App Development
## Topics
- Interop
- Controls
- Animation
- Graphics
- C#
- Data Binding
- AJAX
- Asynchronous Programming
- Security
- GDI+
- Authentication
- ASP.NET
- File System
- LINQ
- User Interface
- Windows Forms
- Graphics and 3D
- Architecture and Design
- XML
- Data Access
- XAML
- threading
- Powershell
- Images
- customization
- custom controls
- Web Services
- 2d graphics
- Visual Basic .NET
- Performance
- Image manipulation
- Getting Started
- Printing
- Windows Phone
- Image
- WebBrowser
- HTML5
- .NET 4
- Imaging
- Drawing
- How to
- UI Design
- Generic C# resuable code
- File Systems
- Networking
- Storage
- Windows PowerShell
- Image Optimization
- general
- C# Language Features
- Language Samples
- Graphics Functions
- Audio and video
- Devices and sensors
- Windows web services
- User Experience
- BitmapImage
- Windows Store app
- Load Image
- data and storage
- New for Windows 8.1 Preview
## Updated
- 12/17/2013
## Description

<div>
<h1>1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Introduction</h1>
</div>
<p>There are literally hundreds of Web Browsers available, so how come, there does not seem to be one single browser that meets all our needs? Well, I know that is true for me at least. This series of articles will show you how you can have the perfect web
 browser.</p>
<div>
<h1>2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Building the Sample</h1>
</div>
<p>The sample is built in Visual Studio 2012 Ultimate as an x86 targeted application using .Net Framework 4. We will be using NuGet packages, a number of 3rd party libraries. All of which will be fully explained to you ensuring that the final compilation of
 your browser will be hassle free. Oh! And the sample code is verbosely commented so you should have no problem in working out what the code does.</p>
<div>
<h1>3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Description</h1>
</div>
<p>The only way to truly get a program to do what you want it to do in the way you want it to; is to write it yourself. Do not expect to have a fully operation commercial application available after an hour or two of coding. Software development takes time
 and perseverance. However, following this series of articles will save you an immense amount of time and hassle.</p>
<p><em>&nbsp;<img id="101220" src="101220-picture%202013-11-16%2000_09_35.png" alt="" width="1920" height="54"><img id="101215" src="101215-picture%202013-11-16%2000_12_05.png" alt="" width="710" height="57"><img id="101216" src="101216-picture%202013-11-16%2000_11_23.png" alt="" width="826" height="59"><img id="101217" src="101217-picture%202013-11-16%2000_10_43.png" alt="" width="958" height="113"></em></p>
<p>As you can see from the above pictures Browsers come in all kinds of designs. But in essence they all have the same components:</p>
<ol>
<li>A menu bar </li><li>A Tool bar </li><li>An Address Bar and Search Bar </li><li>A multi-tab Interface </li><li>The main display section for showing web pages </li><li>A status bar </li></ol>
<p>The biggest changes in the design are the order of the components and the graphics that are used. Our browser will have the Menu bar at the top, followed by a toolbar, then the multi-tab interface, followed by the address bar (and search bar), the main display
 section, and finally showing the status bar.</p>
<p><em><img id="101214" src="101214-picture%202013-11-16%2000_47_58.png" alt="" width="1920" height="117"></em></p>
<p>This is not an arbitrary design decision but one based on providing you the ability to create your own web browser without causing you unnecessary difficulty. In creating our web browser in this way the toolbar, which will hold our favourites will be uniformed
 throughout our web browser &ndash; that is synched between all instances of tabs being displayed; this is also true of our main menu. Our address bar will be synchronised with the instance of browser that it is linked to as is the status bar.</p>
<p>We are not actually going to use tabbed browsing as such but docked browsing allowing us to move pages around as we wish &ndash; similar to the visual studio interface.</p>
<div>
<h1>4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Let&rsquo;s Get Started!</h1>
</div>
<p>Open Visual Studio 2012 and select new project. We will be creating a Windows Form application. The code explained will be provided in C# (VB.Net will also be provided as a sample download) but you can use any Windows Forms language that you wish.</p>
<h2>4.1&nbsp;&nbsp;&nbsp; Adding The Components</h2>
<ul>
<li>Add a MenuStrip to the form. </li><li>Right click on the MenuStrip click on Insert Standard Items. This will populate the menu for us &ndash; for just now.
</li><li>Add a ToolStrip and right click on it &ndash; selecting Insert Standard Items. This will populate the toolbar for us &ndash; which we will change later.
</li><li>In the Solution Explorer &ndash; Right click on your application and add a new folder called Forms. In here we will very cleverly place our forms. Genius!
</li><li>Right click on the Forms Folder and Add a New Windows Form &ndash; Call it FrmBrowser. This is our actual browser that will be displayed with the contents of various web sites.
</li><li>In FrmBrowser add a ToolStrip and a StatusStrip. </li><li>Call the ToolStrip &ldquo;AddressBar&rdquo; and do not worry about the StatusStrip.
</li></ul>
<h2>4.2&nbsp;&nbsp;&nbsp; Adding The NuGet features.</h2>
<p>Right click on your solution in the Solution Explorer &ndash; Clever place to put it. Now click on Manage NuGet Packages for Solution. Make sure you have selected Online sources on the left side pane and Include Prerelease on the top left dropdown.</p>
<p>Search for and install DockPanel Suite &ndash; make sure it&rsquo;s the 3.x version. Next go to:
<a href="http://www.awesomium.com/download/">http://www.awesomium.com/download/</a> and install Awesomium &ndash; you will need to close Visual Studio for this. Once it has complete re-start Visual Studio.</p>
<h2>4.3&nbsp;&nbsp;&nbsp; Adding The Components part 2</h2>
<p>You will find new Awesomium.Net components in your Toolbox now. Add the AddressBox to your AddressBar, add another 2 buttons, a dropdown button, a textbox, another 2 buttons and finally another dropdown button.</p>
<p>Now add the Awesomium Webcontrol to the form and the WebSessionProvider which you will call webSession in its properties. In the Webcontrol set the WebSessionProvider to the newly added webSession. Name the WebControl Browser in its properties. Set the WebControl
 (Browser) Source property to your favourite website.</p>
<p>Name the buttons and textboxes in the top AddressBar the following names.</p>
<ul>
<li>btnBack </li><li>btnForward </li><li>btnSecure </li><li>tbAddressBox </li><li>btnFavourites </li><li>btnReload </li><li>btnSearchProvider </li><li>tbSearchBox </li><li>btnSearch </li><li>btnHome </li><li>btnMenu </li></ul>
<p>Did I mention we were using docking and not tabs? Yes I did&hellip; now we have to do that &ndash; hey, we get to do some coding&hellip; ok not much.</p>
<h2>4.4&nbsp;&nbsp;&nbsp; Finally</h2>
<p>In your toolbox you will find DockPanel plus some themes called VS20xxTheme. Choose VS2012LightTheme. It allows for the X button to be displayed on each &ldquo;tab&rdquo;. Add DockPanel to Form1 &ndash; make sure it is FORM1 and not the previous FrmBrowser!
 Name it dockPanel in its properties.</p>
<h2>4.5&nbsp;&nbsp;&nbsp; A little coding</h2>
<p>Right click on the FrmBrowser and select go to Code. Change the Class from FrmBrowser : Form to FrmBrowser : DockingContent. Now add using WeifenLuo.WinFormsUI.Docking; assembly to the top of your code.</p>
<p>I told you it was a little coding. Believe it or not that is it! You have just created the basics of your very own WebBrowser. Now that we have the basics done &ndash; our next part will start to add some functionality.</p>
<h3>4.5.1&nbsp;&nbsp;&nbsp; Source Code Files</h3>
<ul>
<li><em>source code Form1.cs &ndash; The main interface to our Browsing application</em>
</li><li><em>source code Forms/FrmBrowser.cs &ndash; This is our tab-able, dock-able and free-floating browser interface.</em>
</li></ul>
<p>If you like my samples then please nominate me for an MVP. <a href="http://mvp.microsoft.com/en-us/nominate-an-mvp.aspx">
http://mvp.microsoft.com/en-us/nominate-an-mvp.aspx</a>. Leave me a message if you nominated me!</p>
<div>
<h1>5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; More Information</h1>
</div>
<p>To convert the solution to a previous version of Visual Studio you can use this free application:
<a href="http://vsprojectconverter.codeplex.com/">http://vsprojectconverter.codeplex.com/</a> or download and use Visual Studio 2013 Express which is freely available from Microsoft from here:
<a href="http://www.visualstudio.com/downloads/download-visual-studio-vs">http://www.visualstudio.com/downloads/download-visual-studio-vs</a>.</p>
<p>&nbsp;http://www.awesomium.com/download/</p>
