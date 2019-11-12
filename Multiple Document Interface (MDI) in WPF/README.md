# Multiple Document Interface (MDI) in WPF
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- Windows Forms
- WPF
- WPF MDI Form
## Topics
- Windows Forms
- WPF MDI Form
## Updated
- 01/09/2014
## Description

<h1>Introduction</h1>
<p><em>Multiple Document Interface(MDI) in WPF. </em></p>
<p><em>MDI Form doesn't exist in WPF. But this functionality can be achieved by using Tab Control or Third Party Control. I Used WPF.MDI.DLL assembly.
<br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>For implementing the MDI form in WPF, You need to download third party Tool &quot;WPF.MDI.DLL&quot;. You can download this library from this link.</em></p>
<p><em><a title="WPF.MDI.DLL Download" href="http://wpfmdi.codeplex.com/"><span style="font-size:small"><strong>Download</strong></span></a></em></p>
<p><em><span style="font-size:small"><strong>Screen:</strong></span></em></p>
<p><em><span style="font-size:small"><strong><img id="106854" src="106854-wpfmdiform.gif" alt="" width="998" height="648"><br>
</strong></span></em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><strong>WPF.MDI.DLL</strong> assembly contain two conrol </em></p>
<p><em>1) MDI Container</em></p>
<p><em>2) MDI Child<br>
</em></p>
<p>To implement MDI form first Place MDI Container in a main window and Add User Control in your project to use as a MDI Child form. Two use this assembly place
<strong>WPF.MDI.DLL</strong> in to <strong>bin</strong> folder and <strong>Add references</strong> to it. Add that assembly in
<strong>ToolBox</strong> so you can easily drag and drop the MDI Control.</p>
<p>Add this namespace in your mainwindow.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;xmlns:mdi=<span class="cs__string">&quot;clr-namespace:WPF.MDI;assembly=WPF.MDI&quot;</span></pre>
</div>
</div>
</div>
<p>Add Menu in your MainWindow Form.&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;DockPanel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;Menu&nbsp;Name=<span class="cs__string">&quot;MainMenu&quot;</span>&nbsp;VerticalAlignment=<span class="cs__string">&quot;Top&quot;</span>&nbsp;DockPanel.Dock=<span class="cs__string">&quot;Top&quot;</span>&nbsp;Height=<span class="cs__string">&quot;30&quot;</span>&nbsp;FontSize=<span class="cs__string">&quot;15&quot;</span>&nbsp;&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;MenuItem&nbsp;Name=<span class="cs__string">&quot;menuHome&quot;</span>&nbsp;Header=<span class="cs__string">&quot;Home&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/MenuItem&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;MenuItem&nbsp;Name=<span class="cs__string">&quot;menuMaster&quot;</span>&nbsp;Header=<span class="cs__string">&quot;Master&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;MenuItem&nbsp;Name=<span class="cs__string">&quot;userRegistration&quot;</span>&nbsp;Header=<span class="cs__string">&quot;User&nbsp;Registration&quot;</span>&nbsp;Click=<span class="cs__string">&quot;userRegistration_Click&quot;</span>&nbsp;&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;MenuItem&nbsp;Name=<span class="cs__string">&quot;compRegistration&quot;</span>&nbsp;Header=<span class="cs__string">&quot;Company&nbsp;Registration&quot;</span>&nbsp;Click=<span class="cs__string">&quot;compRegistration_Click&quot;</span>&gt;&lt;/MenuItem&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/MenuItem&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;MenuItem&nbsp;Name=<span class="cs__string">&quot;menuExit&quot;</span>&nbsp;Header=<span class="cs__string">&quot;Exit&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;MenuItem&nbsp;Name=<span class="cs__string">&quot;Exit&quot;</span>&nbsp;Header=<span class="cs__string">&quot;Exit&quot;</span>&nbsp;Click=<span class="cs__string">&quot;menuExit_Click&quot;</span>&gt;&lt;/MenuItem&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/MenuItem&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/Menu&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/DockPanel&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>Then After p<span style="font-size:x-small">lace MDI Container in a MainWindow.</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;DockPanel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;mdi:MdiContainer&nbsp;Theme=<span class="cs__string">&quot;Aero&quot;</span>&nbsp;DockPanel.Dock=<span class="cs__string">&quot;Top&quot;</span>&nbsp;Margin=<span class="cs__string">&quot;0&nbsp;20&nbsp;0&nbsp;0&quot;</span>&nbsp;Name=<span class="cs__string">&quot;MainMdiContainer&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/DockPanel&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>By default MdiContainer contains three different Theme (Aero,Luna,Generic), You can apply any of them as per your requirement.</p>
<p>I Used Two User Control to use as a MDI Child UserRegistration.xaml and CompRegistration.</p>
<p>Add the name space in MainWindow.cs.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span><span class="hidden">csharp</span>


<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Imports</span>&nbsp;WPF.MDI</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>and the below code in MenuItem Click Event.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span><span class="hidden">csharp</span>


<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;userRegistration_Click(sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;RoutedEventArgs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MainMdiContainer.Children.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Here&nbsp;UserRegistration&nbsp;is&nbsp;the&nbsp;class&nbsp;that&nbsp;you&nbsp;have&nbsp;created&nbsp;for&nbsp;mainWindow.xaml&nbsp;user&nbsp;control.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MainMdiContainer.Children.Add(<span class="visualBasic__keyword">New</span>&nbsp;MdiChild()&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;{&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&nbsp;User&nbsp;Registration&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Height&nbsp;=&nbsp;(System.Windows.SystemParameters.PrimaryScreenHeight&nbsp;-&nbsp;<span class="visualBasic__number">15</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Width&nbsp;=&nbsp;(System.Windows.SystemParameters.PrimaryScreenWidth&nbsp;-&nbsp;<span class="visualBasic__number">15</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Content&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;UserRegistration()&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;})&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;compRegistration_Click(sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;RoutedEventArgs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MainMdiContainer.Children.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Here&nbsp;compRegistration&nbsp;is&nbsp;the&nbsp;class&nbsp;that&nbsp;you&nbsp;have&nbsp;created&nbsp;for&nbsp;mainWindow.xaml&nbsp;user&nbsp;control.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MainMdiContainer.Children.Add(<span class="visualBasic__keyword">New</span>&nbsp;MdiChild()&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;{&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&nbsp;Company&nbsp;Registration&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Height&nbsp;=&nbsp;(System.Windows.SystemParameters.PrimaryScreenHeight&nbsp;-&nbsp;<span class="visualBasic__number">15</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Width&nbsp;=&nbsp;(System.Windows.SystemParameters.PrimaryScreenWidth&nbsp;-&nbsp;<span class="visualBasic__number">15</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Content&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;CompRegistration()&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;})&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>Here MainMdiContainer.Children.Clear() is used for removing the previous UserControl. This method will display One Control at a time. If you want to display Two Control at once just remove or comment it.</p>
<p>Style=null is used to remove minimize, maximize and close icon from the User Control.</p>
<p><span style="font-size:x-small"><br>
</span></p>
<h1><span>Source Code Files</span></h1>
<p>This library to add the traditional windows forms Multiple Document Interface (MDI) features to WPF.</p>
<ul>
<li><em>source code file MDIForm Library - <a title="Download MDI Form Library" href="https://wpfmdi.codeplex.com/">
Download MDIForm Library</a></em> </li><li><em><em>source code file Source File - <a id="106857" href="/windowsdesktop/site/view/file/106857/1/WPFMDIForm.zip">
WPFMDIForm.zip</a><br>
</em></em></li></ul>
<h1>More Information</h1>
<p><em>For more information on X, see ...?</em></p>
<div class="mcePaste" id="_mcePaste" style="left:-10000px; top:41px; width:1px; height:1px; overflow:hidden">
first</div>
