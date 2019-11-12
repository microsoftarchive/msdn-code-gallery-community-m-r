# Professional WebBrowser - Step-By-Step - Part 3
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- COM
- C#
- AJAX
- GDI+
- ASP.NET
- File System
- Win32
- .NET
- Class Library
- ADO.NET
- WMI
- LINQ
- Windows Forms
- WPF
- XML
- Microsoft Azure
- XAML
- Visual Studio 2010
- Windows Phone 7
- Windows 7
- SQL Azure
- .NET Framework 4
- .NET Framework
- Visual Basic .NET
- ASP.NET Web Forms
- VB.Net
- .NET Framework 4.0
- Windows General
- Windows Phone
- C# Language
- Internet Explorer
- Async
- HTML5
- Windows Azure Storage
- WinForms
- Windows Runtime
- .NET Framework 4.5
- Windows 8
- Windows Azure Service Bus
- .NET Framwork
- Graphics Functions
- ASP.NET Web API
- Visual C Sharp .NET
- Image process
- Visual Studio 2012
- Windows Phone 7.5
- .NET 4.5
- Windows Phone 8
- Windows Store app
- .NET Development
- Windows Phone Development
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
- User Interface
- Windows Forms
- Graphics and 3D
- Architecture and Design
- XML
- Microsoft Azure
- Data Access
- XAML
- threading
- Images
- customization
- custom controls
- Web Services
- 2d graphics
- Performance
- Image manipulation
- Automation
- Getting Started
- GridView
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
## Updated
- 12/17/2013
## Description

<div>
<h1>1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Introduction</h1>
</div>
<p>A web browser needs more than just to look good &ndash; it needs good functionality as well. In this sample we will add basic functionality to the Addressbar so that we can start navigating the internet like we can with other browsers.</p>
<div>
<h1>2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Building the Sample</h1>
</div>
<p>The sample is built in Visual Studio 2012 Ultimate as an x86 targeted application using .Net Framework 4. We will be using NuGet packages, a number of 3rd party libraries. All of which will be fully explained to you ensuring that the final compilation of
 your browser will be hassle free. Oh! And the sample code is verbosely commented so you should have no problem in working out what the code does.</p>
<div>
<h1>3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Description</h1>
</div>
<p>Our Addressbar has the following buttons and input boxes which we need to give functionality to.</p>
<p>Back, Forward, Security, AddressBox, Favourites, Reload, Search, Home and our Menu. We will provide functionality to all but the Favourites as that is a large topic just for itself.</p>
<div>
<h1>4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Let&rsquo;s Get Started!</h1>
</div>
<p>Open Visual Studio 2012 and select the project. Our Addressbar you will remember is on our FrmBrowser.cs form.</p>
<h2>4.1&nbsp;&nbsp;&nbsp; Back Button</h2>
<p>Double click on the first button &ndash; btnBack. This will automatically create the click event and take you to the code view. The btnBack should only be enabled when the browser can actually move back in the history. Fortunately this is really simple to
 do.</p>
<p>The enabled property of the button is a Boolean (true or false) and the browser has a method which we can call which provides a true or false concerning whether we can travel backwards in our history. So our code looks like this:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">void</span>&nbsp;Browser_DocumentReady(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;Awesomium.Core.UrlEventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnBack.Enabled&nbsp;=&nbsp;Browser.CanGoBack();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>As you can see we have placed the Boolean check in the DocumentReady event. This means every time a document is loaded it will check to see if the browser can go back in history and enable or disable our back button accordingly. When the button is clicked
 &ndash; only works when enabled the following code is executed:</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;BtnBackClick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browser.GoBack();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>That is the Back button now implemented. Now do the same for the Forward button.</p>
<p>&nbsp;</p>
<h2>4.2&nbsp;&nbsp;&nbsp; Security Button</h2>
<p>The Security button should change according to whether or not the page being displayed is a HTTPS page or not. For now we will just show some page information including the page&rsquo;s security information. We need to create a new Browser event:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">Browser.ShowPageInfo&nbsp;&#43;=&nbsp;Browser_ShowPageInfo;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>Which when fired calls the following method:</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">void</span>&nbsp;Browser_ShowPageInfo(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;Awesomium.Core.PageInfoEventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;res&nbsp;=&nbsp;<span class="cs__string">&quot;Certificate&nbsp;Error:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;e.Info.CertError.ToString();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;res&nbsp;&#43;=&nbsp;Environment.NewLine;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;res&nbsp;&#43;=&nbsp;e.Info.ContentStatus.ToString();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;res&nbsp;&#43;=&nbsp;Environment.NewLine;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;res&nbsp;&#43;=&nbsp;e.Info.SecurityStatus.ToString();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(res);&nbsp;
&nbsp;
&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>The event is fired when the BtnSecure is clicked as shown below:</p>
<p>&nbsp;</p>
<h2>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;BtnSecureClick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browser.RequestPageInfo();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
4.3&nbsp;&nbsp;&nbsp; The Address Input TextBox</h2>
<p>We are using the Awesomium supplied AddressBox control. It&rsquo;s WebControl property is set to our Browser control. This automatically links the AddressBox control to the WebControl&hellip; there is nothing more that we need to do.</p>
<h2>4.4&nbsp;&nbsp;&nbsp; Reload Button</h2>
<p>Again double click on the Reload button and in the method that is created for us place the following simple code:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;BtnReloadClick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browser.Reload(<span class="cs__keyword">true</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>The Boolean for the Reload Method states whether the Browser control should reload from the site, download the page again, or reload from the cache; simply redisplay the already downloaded page. I have set it to re-download the page.</p>
<p>&nbsp;</p>
<h2>4.5&nbsp;&nbsp;&nbsp; Search Provider</h2>
<p>At the moment we are only going to use Google as our search provider. You can add other search providers as you see fit. To add more search providers simply repeat the following process.</p>
<p>First of all download your search provider Icon and add it to your resources as shown in Part 2. (<a href="http://code.msdn.microsoft.com/Professional-WebBrowser-70e450b2">http://code.msdn.microsoft.com/Professional-WebBrowser-70e450b2</a>)</p>
<p><img id="101352" src="101352-picture%202013-11-19%2005_26_14.png" alt="" width="152" height="78" style="float:left; margin-left:5px; margin-right:5px">Click on the SplitButton we have created for our search provider. Add a new menu Item
 with the name of your Search Provider and its image. Also set the same image to the SplitButton.</p>
<p>&nbsp;</p>
<h2>4.6&nbsp;&nbsp;&nbsp; Search Input Box</h2>
<p>Go to you search provider and search for &ldquo;fish fingers&rdquo;. In google we get this.</p>
<p><a href="https://www.google.co.uk/#q=fish&#43;fingers">https://www.google.co.uk/#q=fish&#43;fingers</a></p>
<p>Our Query string, the item we are looking for has been changed from &ldquo;fish fingers&rdquo; to &ldquo;fish&#43;fingers&rdquo; and that has been suffixed to this url: https://www.google.co.uk/#q= Now we have everything we need to implement our search provider.</p>
<p>In the designer click on the SearchBox TextBox and click the events icon in the properties window. Double click on the KeyUp event and add the following code:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;TbSearchBoxKeyUp(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;KeyEventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(e.KeyCode&nbsp;==&nbsp;Keys.Return&nbsp;||&nbsp;e.KeyCode&nbsp;==&nbsp;Keys.Enter&nbsp;&amp;&amp;&nbsp;!<span class="cs__keyword">string</span>.IsNullOrWhiteSpace(tbSearchBox.Text))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browser.Source&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://www.google.co.uk/#q=&quot;</span>&nbsp;&#43;&nbsp;tbSearchBox.Text.Replace(<span class="cs__string">&quot;&nbsp;&quot;</span>,&nbsp;<span class="cs__string">&quot;&#43;&quot;</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>All we are doing here is checking to see if the SearchBox is not empty, if it is not empty we wait for the person to press and Release&hellip; as we are using the KeyUp event, either the Enter Key or the Return Key. When that has been done we create the
 Uri which we tell the browser to navigate to. In the SearchBox we replace the spaces with a &ldquo;&#43;&rdquo; sign just as google does, and add the result to the Google URL.</p>
<p>&nbsp;</p>
<p>More coding is required if you have multiple Search Providers &ndash; as you need to use the correct provider for the search. In this example we just have one provider so it is nice a simple. Also, the search results are shown in the current window. In the
 next article we will start creating and using other browser windows.</p>
<p>Search Button Press</p>
<p>The code for the search button press is basically the same, except we do not need to look at if any keys are pressed:</p>
<h2>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;BtnSearchClick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!<span class="cs__keyword">string</span>.IsNullOrWhiteSpace(<span class="cs__keyword">this</span>.tbSearchBox.Text))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browser.Source&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://www.google.co.uk/#q=&quot;</span>&nbsp;&#43;&nbsp;tbSearchBox.Text.Replace(<span class="cs__string">&quot;&nbsp;&quot;</span>,&nbsp;<span class="cs__string">&quot;&#43;&quot;</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
4.7&nbsp;&nbsp;&nbsp; Home Button</h2>
<p>The Browser has a built in method called GoToHome(). Which we will use when the Home button is pressed. This requires us to setup a HomeURL which we will hard code for just now. So our code looks like this for the Home Button:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;BtnHomeClick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Browser.GoToHome();&nbsp;not&nbsp;using&nbsp;this&nbsp;just&nbsp;now</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browser.Source&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(home);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>That&rsquo;s it for just now &ndash; we will deal with more functionality in the next episode &ndash; implementing the tabbed (docking) interface.</p>
<p>If you like my samples then please nominate me for an MVP. <a href="http://mvp.microsoft.com/en-us/nominate-an-mvp.aspx">
http://mvp.microsoft.com/en-us/nominate-an-mvp.aspx</a>. Leave me a message if you nominated me!</p>
<h3>4.7.1&nbsp;&nbsp;&nbsp; Source Code Files</h3>
<ul>
<li><em>source code Form1.cs &ndash; The main interface to our Browsing application</em>
</li><li><em>source code Forms/FrmBrowser.cs &ndash; This is our tab-able, dock-able and free-floating browser interface.</em>
</li></ul>
<div>
<h1>5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; More Information</h1>
</div>
<p>To convert the solution to a previous version of Visual Studio you can use this free application:
<a href="http://vsprojectconverter.codeplex.com/">http://vsprojectconverter.codeplex.com/</a> or download and use Visual Studio 2013 Express which is freely available from Microsoft from here:
<a href="http://www.visualstudio.com/downloads/download-visual-studio-vs">http://www.visualstudio.com/downloads/download-visual-studio-vs</a>.</p>
<p>&nbsp;<a href="http://www.awesomium.com/download/">http://www.awesomium.com/download/</a></p>
<p>ToolStrip Images and Button Sizing: <a href="http://forums.codeguru.com/showthread.php?402345-RESOLVED-Size-of-Buttons-in-a-toolStrip&s=89af90e4d3d369895ca033758eb0c7ff">
http://forums.codeguru.com/showthread.php?402345-RESOLVED-Size-of-Buttons-in-a-toolStrip&amp;s=89af90e4d3d369895ca033758eb0c7ff</a></p>
