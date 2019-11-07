# Popup Control in universal application Windows 10
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
- Windows 10
- Windows Universal Platform
## Topics
- Popup Window
- PopUp Control
- Windows 10
- Universal Windows Platform
## Updated
- 01/10/2016
## Description

<h1 style="text-align:justify">Introduction</h1>
<p style="text-align:justify">Today we will discuss the Popup control of XAML files in universal Applications.&nbsp;</p>
<h1 style="text-align:justify"><span>Building the Sample</span></h1>
<p>Completing the project requires the following:</p>
<ul>
<li>Visual Studio 2015, which supports universal Windows app projects. </li></ul>
<p style="text-align:justify"><span style="font-size:20px; font-weight:bold">Description</span></p>
<p style="text-align:justify">The popup control has a property IsOpen; this property takes a boolean value as&nbsp;input. In this application we will take a popup control that is associated with a Click event of a button control; on the click event we will
 swap the value of the IsOpen property to open and close the popup control. We will use any XAML control as a child control of the popup control such as image, canvas, StackPanel etc. Here we will use a StackPanel control.&nbsp;</p>
<p style="text-align:justify"><span>In the Solution Explorer there are two files that we will primarily work with; the MainPage.xaml and MainPage.xaml.cs files.</span></p>
<p style="text-align:justify"><span>In the following we are&nbsp;including the entire code of the XAML file and code behind file to create this application.</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title" style="text-align:justify"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden"> private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Height is only important if we want the Popup sized to the screen
            ppup.Height = Window.Current.Bounds.Height;
            ppup.IsOpen = true;
        }</pre>
<div class="preview">
<pre class="csharp">&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Height&nbsp;is&nbsp;only&nbsp;important&nbsp;if&nbsp;we&nbsp;want&nbsp;the&nbsp;Popup&nbsp;sized&nbsp;to&nbsp;the&nbsp;screen</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ppup.Height&nbsp;=&nbsp;Window.Current.Bounds.Height;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ppup.IsOpen&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden"> &lt;Popup x:Name=&quot;ppup&quot; IsOpen=&quot;False&quot; IsLightDismissEnabled=&quot;True&quot; 
     Width=&quot;320&quot; HorizontalAlignment=&quot;Left&quot;&gt;
            &lt;Popup.ChildTransitions&gt;
                &lt;TransitionCollection&gt;
                    &lt;!--&lt;EdgeUIThemeTransition Edge=&quot;Left&quot; /&gt;--&gt;
                    &lt;PaneThemeTransition Edge=&quot;Left&quot; /&gt;
                &lt;/TransitionCollection&gt;
            &lt;/Popup.ChildTransitions&gt;
            &lt;Grid Width=&quot;380&quot; Height=&quot;{Binding ElementName=flyoutPane, Path=Height}&quot;  Background=&quot;{ThemeResource FlyoutBackgroundThemeBrush}&quot; &gt;
                &lt;TextBlock Text=&quot;Grid contents here&quot; /&gt;
            &lt;/Grid&gt;
        &lt;/Popup&gt;</pre>
<div class="preview">
<pre class="xaml">&nbsp;<span class="xaml__tag_start">&lt;Popup</span>&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;ppup&quot;</span>&nbsp;<span class="xaml__attr_name">IsOpen</span>=<span class="xaml__attr_value">&quot;False&quot;</span>&nbsp;<span class="xaml__attr_name">IsLightDismissEnabled</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;320&quot;</span>&nbsp;<span class="xaml__attr_name">HorizontalAlignment</span>=<span class="xaml__attr_value">&quot;Left&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Popup</span>.ChildTransitions<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;TransitionCollection</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__comment">&lt;!--&lt;EdgeUIThemeTransition&nbsp;Edge=&quot;Left&quot;&nbsp;/&gt;--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;PaneThemeTransition</span>&nbsp;<span class="xaml__attr_name">Edge</span>=<span class="xaml__attr_value">&quot;Left&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/TransitionCollection&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/Popup.ChildTransitions&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Grid</span>&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;380&quot;</span>&nbsp;<span class="xaml__attr_name">Height</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;ElementName=flyoutPane,&nbsp;Path=Height}&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Background</span>=<span class="xaml__attr_value">&quot;{ThemeResource&nbsp;FlyoutBackgroundThemeBrush}&quot;</span>&nbsp;<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;TextBlock</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Grid&nbsp;contents&nbsp;here&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/Grid&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/Popup&gt;</span></pre>
</div>
</div>
</div>
</h1>
<p><span>If you're doing many of these you can create a custom control with an attached property.</span></p>
<p><span style="font-size:xx-small">In that there is requirement to show menu from left side when User clicks on a button.</span></p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>source code file Mainpage.xaml&nbsp;</em> </li></ul>
<h1>More Information</h1>
<p><em>Feel free to contact me on Twitter @CHJ_GeekGirl for any question about this and visit my blog for more code sample :</em><a href="http://www.hjaiejchourouk.com/"><em>&nbsp;http://hjaiejchourouk.com/</em></a><strong>&nbsp;</strong></p>
