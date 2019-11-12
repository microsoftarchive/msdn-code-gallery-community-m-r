# Multiple windowing in UWP app like Microsoft Edge
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Animation
- C#
- XAML
- WinRT
- Visual C#
- XAML/C#
- Visual Studio 2015
- Windows 10
- Microsoft Edge
- Universal Windows App Development
- Universal Windows Platform
- UWP
## Topics
- Animation
- C#
- XAML
- WinRT
- XAML Animation
- Visual Studio 2015
- Windows 10
- Microsoft Edge
- Universal Windows Platform
- Universal Windows Applicaitons
- UWP
## Updated
- 01/31/2017
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates how to create multiple windowing Universal Windows Platform App like the Microsoft Edge.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">For building and runing this sample you neen to have Visual Studio 2015 (with update 1 or later), Windows 10 (build 10240 or later ) and
<span>&nbsp;Windows 10&nbsp;</span>SDK.<br>
</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small"><span>This sample demonstrates, how to create a multiple windowing Universal Windows Platform App like the Microsoft Edge. The app show the basic concept of how to create simple multiple windowing app using Drag &amp; Drop &nbsp;and
 Multiple Views Univerasl Windows APIs. This sample is for demonstration only and may contains some mistakes.</span></span></p>
<p><strong><span style="font-size:small">Here we can how the app looks like</span></strong></p>
<p><img id="160295" src="160295-screenshot%20(112).png" alt="" width="1411" height="887"></p>
<p>&nbsp;</p>
<p><strong><span style="font-size:small">The following screenshot demonstrate process of dragging &quot;Tab&quot; between windows</span></strong></p>
<p>&nbsp;</p>
<p><img id="160296" src="160296-screenshot%20(113).png" alt="" width="1424" height="878"></p>
<p><span style="font-size:small">This piece of code respond for creating new window&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;<span class="cs__keyword">private</span>&nbsp;async&nbsp;<span class="cs__keyword">void</span>&nbsp;CreateNewView(<span class="cs__keyword">object</span>&nbsp;model)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CoreApplicationView&nbsp;newView&nbsp;=&nbsp;CoreApplication.CreateNewView();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;newViewId&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;await&nbsp;newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,&nbsp;()&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;frame&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Frame();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frame.Navigate(<span class="cs__keyword">typeof</span>(MainPage),&nbsp;model);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Window.Current.Content&nbsp;=&nbsp;frame;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Window.Current.Activate();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newViewId&nbsp;=&nbsp;ApplicationView.GetForCurrentView().Id;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;viewShown&nbsp;=&nbsp;await&nbsp;ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<pre><strong><span style="font-size:small">Here is some healpful referances </span></strong></pre>
<pre><span style="font-size:small"><a href="https://msdn.microsoft.com/en-us/windows/uwp/app-to-app/drag-and-drop?f=255&MSPPError=-2147217396" target="_blank">Drag and drop</a> in UWP apps and Microsoft </span><span style="font-size:small">official </span><span style="font-size:small">sample on </span><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlDragAndDrop" target="_blank" style="font-size:small">GitHub</a></pre>
<pre><span style="font-size:small"><a href="https://msdn.microsoft.com/en-us/windows/uwp/layout/show-multiple-views">Show multiple views</a> for UWP apps and Microsoft</span><span style="font-size:small"> official</span><span style="font-size:small"> sample on </span><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MultipleViews" target="_blank" style="font-size:small">GitHub</a></pre>
<pre><span style="font-size:small">Extended Title Bar for UWP apps Microsoft official sample on <a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TitleBar" target="_blank">GitHub</a></span></pre>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>MainPage.xaml</em> </li></ul>
<ul>
<li><em>MainPage.xaml.cs</em> </li></ul>
<ul>
</ul>
<ul>
<li><em>App.Xaml.cs</em> </li></ul>
<h1>More Information</h1>
<p><em><em>For more info about the programming models, platforms, languages, and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference topics provided in the Windows&nbsp;10 documentation available in the Windows Developer
 Center.</em></em></p>
