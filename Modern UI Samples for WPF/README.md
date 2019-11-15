# Modern UI Samples for WPF
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- WPF
## Topics
- Navigation
- Theme
- ModernUI
## Updated
- 12/02/2014
## Description

<h1>Introduction</h1>
<p><em><span>Modern UI is a&nbsp;set of controls and styles converting our&nbsp;WPF application into a great looking Modern UI app. The Modern UI project can be find in&nbsp;</span><a rel="nofollow" href="https://mui.codeplex.com/" target="_blank">mui.codeplex.com</a><span>,
 here is possible to&nbsp;</span><a rel="nofollow" href="https://mui.codeplex.com/downloads/get/890165" target="_blank">get the WPF app that demostrate&nbsp;</a><span>the features provided by &ldquo;mui&rdquo;.</span></em></p>
<h1><span>Building the Sample</span></h1>
<p><em><em>You only need Visual Studio 2012/Visual Studio 2013 and Windows 8/Windows 8.1, both the RTM version.</em></em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>Samples for WPF using Modern UI</p>
<p>&nbsp;</p>
<p>&gt;&nbsp;<a href="http://wp.me/p4LXhq-GQ">Modern UI for WPF application by example (Default Window)</a></p>
<p><span>The MainWindows is defined by</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>

<div class="preview">
<pre class="js">&lt;mui:ModernWindow&nbsp;x:Class=<span class="js__string">&quot;ModernUIForWPFSample.DefaultModernUI.MainWindow&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xmlns=<span class="js__string">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xmlns:x=<span class="js__string">&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xmlns:mui=<span class="js__string">&quot;http://firstfloorsoftware.com/ModernUI&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Title=<span class="js__string">&quot;Default&nbsp;Modern&nbsp;UI&nbsp;Window&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Width=<span class="js__string">&quot;650&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Height=<span class="js__string">&quot;450&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IsTitleVisible=<span class="js__string">&quot;True&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:ModernWindow.TitleLinks&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:Link&nbsp;DisplayName=<span class="js__string">&quot;ModernUI&nbsp;Project&quot;</span>&nbsp;Source=<span class="js__string">&quot;https://mui.codeplex.com/&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:Link&nbsp;DisplayName=<span class="js__string">&quot;ModernUI&nbsp;For&nbsp;WPF&nbsp;Sample&quot;</span>&nbsp;Source=<span class="js__string">&quot;https://github.com/saramgsilva&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/mui:ModernWindow.TitleLinks&gt;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:ModernWindow.MenuLinkGroups&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:LinkGroup&nbsp;DisplayName=<span class="js__string">&quot;Helper&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:LinkGroup.Links&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:Link&nbsp;DisplayName=<span class="js__string">&quot;Steps&quot;</span>&nbsp;Source=<span class="js__string">&quot;/Views/StepsControl.xaml&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mui:Link&nbsp;DisplayName=<span class="js__string">&quot;Other&nbsp;resources&quot;</span>&nbsp;Source=<span class="js__string">&quot;/Views/ResourcesControl.xaml&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/mui:LinkGroup.Links&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/mui:LinkGroup&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/mui:ModernWindow.MenuL</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p>There are two title links at the Top, based in Web url, and there is a menu link group which contain only two link group:Steps and Resources, which are UserControls.</p>
<p>&nbsp;</p>
<p><strong>Note</strong>:</p>
<p>1. TitleLink and LinkGroup allow to define a path for a Web Url or for a Control/User Control/Window.</p>
<p>2.&nbsp;Is required to add the themes from Modern UI to the App.xaml.</p>
<p>&nbsp;</p>
<p>When we run the sample we will have something like</p>
<p>&nbsp;</p>
<p><a href="https://camo.githubusercontent.com/fad596cd3e085712cc933cabf5c1af5b613498d1/687474703a2f2f7331332e706f7374696d672e6f72672f396a3861383036316a2f696d6167652e706e67" target="_blank"><img src="https://camo.githubusercontent.com/fad596cd3e085712cc933cabf5c1af5b613498d1/687474703a2f2f7331332e706f7374696d672e6f72672f396a3861383036316a2f696d6167652e706e67" alt="" width="450" height="300" style="display:block; margin-left:auto; margin-right:auto"></a></p>
<p>&nbsp;</p>
<p>&gt;&nbsp;<a href="http://wp.me/p4LXhq-H5">Modern UI for WPF application by example (Blank Window)</a></p>
<p>&nbsp;</p>
<p><a href="https://camo.githubusercontent.com/0049af763af5cd93816ac39c8221c511907a627b/687474703a2f2f73392e706f7374696d672e6f72672f356b796935336f767a2f696d6167652e706e67" target="_blank"><img src="https://camo.githubusercontent.com/0049af763af5cd93816ac39c8221c511907a627b/687474703a2f2f73392e706f7374696d672e6f72672f356b796935336f767a2f696d6167652e706e67" alt="" width="450" height="300" style="display:block; margin-left:auto; margin-right:auto"></a></p>
<p>&nbsp;</p>
<p>&gt;&nbsp;<a href="http://wp.me/p4LXhq-Hi">Modern UI for WPF application by example (Default Window without back button)</a></p>
<p>&nbsp;</p>
<p><a href="https://camo.githubusercontent.com/3bf4ee7ec60dd66871a8ec3f4ee4f70d6a98ebe6/687474703a2f2f7332392e706f7374696d672e6f72672f6479673335757637622f696d6167652e706e67" target="_blank"><img src="https://camo.githubusercontent.com/3bf4ee7ec60dd66871a8ec3f4ee4f70d6a98ebe6/687474703a2f2f7332392e706f7374696d672e6f72672f6479673335757637622f696d6167652e706e67" alt="" width="450" height="300" style="display:block; margin-left:auto; margin-right:auto"></a></p>
<p>&nbsp;</p>
<p>&gt;&nbsp;<a href="http://wp.me/p4LXhq-Hj">Modern UI for WPF application by example (Handle Navigation: (Default))</a></p>
<p><a href="http://wp.me/p4LXhq-Hj"></a>&gt;&nbsp;<a href="http://wp.me/p4LXhq-HI">Modern UI for WPF application by example ( NavigationMessageService - MVVM )</a></p>
<p><a href="http://wp.me/p4LXhq-HI"></a>&gt;&nbsp;<a href="http://wp.me/p4LXhq-HJ">Modern UI for WPF application by example ( NavigationService - MVVM )</a></p>
<p>&nbsp;</p>
<h1>Follow me</h1>
<h2><span style="font-size:small"><a href="http://www.saramgsilva.com/" style="font-size:10px">My blog: typeof(saramgsilva)</a></span></h2>
<h2><span style="font-size:small"><a href="http://www.saramgsilva.com/" style="font-size:10px"></a><a href="https://twitter.com/saramgsilva" style="font-size:10px">My twitter @saramgsilva</a></span></h2>
<p>&nbsp;</p>
<h1><span>Source Code&nbsp;</span></h1>
<p>The original repository for the source code is <a href="https://github.com/saramgsilva/ModernUISamples">
here</a>.</p>
<p>&nbsp;</p>
<h1>More Information</h1>
<p><em><em>Ask me on twitter @saramgsilva</em></em></p>
<p>&nbsp;</p>
<h1>Visual Studio extension used</h1>
<p><a href="https://www.jetbrains.com/resharper/"><img src="https://camo.githubusercontent.com/c3e0923d1ecd7c361e09dcccfc169a69833c1cc0/687474703a2f2f7332382e706f7374696d672e6f72672f6f65766135786f72682f7265736861727065725f6c6f676f2e706e67" alt="resharper" width="88" height="88" style="float:left"></a>&nbsp;
 &nbsp;&nbsp;<a href="http://submain.com/products/ghostdoc.aspx"><img src="https://camo.githubusercontent.com/080d52bfc141fc57baae9c030cc9f680b5f6ba3c/687474703a2f2f7331352e706f7374696d672e6f72672f623964376a67366a622f315f67686f7374646f63313030783130302e676966" alt="ghostdoc" width="81" height="83"></a>&nbsp;
 &nbsp; &nbsp;&nbsp;<a href="https://stylecop.codeplex.com/"><img src="https://camo.githubusercontent.com/c52dab41492e48ccc28a0ff940a51e858da392d7/687474703a2f2f7331342e706f7374696d672e6f72672f7a62706875386d64702f5374796c655f436f705f4c6f676f2e706e67" alt="stylecop" width="84" height="84"></a></p>
<p><em><em><br>
</em></em></p>
