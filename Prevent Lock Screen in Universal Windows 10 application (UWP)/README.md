# Prevent Lock Screen in Universal Windows 10 application (UWP)
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
- XAML
- universal windows app
- Windows 10
- Universal Windows Platform
- UWP
- windows phone 10
- Windows Mobile 10
## Topics
- C#
- XAML
- WinRT
- Windows Store app
- universal app
- Windows 10
- Universal Windows Platform
- UWP
## Updated
- 07/14/2016
## Description

<h1>Introduction</h1>
<p>After a certain amount of idle time, Windows 10 devices may dim the display, activate the lock screen, or power down the display in order to conserve power. The amount of idle time required before this occurs depends on a device&rsquo;s user settings. Unfortunately,
 this can turn out to be an inconvenient mistake if your app happens to be a passive experience like a video player, a music player, or a navigation app.Although users may be very engaged, Windows 10 devices will register watching and listening as user idle
 time since there is no direct interaction with the screen. How do you do this in UWP?&nbsp;<em><span><span><br>
</span></span><span><br>
</span></em></p>
<h1><span>Building the Sample</span></h1>
<p><em>VisualStudio 2015 update 1 or later , Windows 10 SDK</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span>The syntax turns out to be equally simple. You just need to make a display request</span><span>&nbsp;call.</span></p>
<p><img id="156499" src="156499-screenshot%20(69).png" alt="" width="499" height="352"></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;toggleSwitch_Toggled(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>(toggleSwitch.IsOn)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_displayRequest.RequestActive();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_displayRequest.RequestRelease();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p><span>Without any additional coding, though, your&nbsp;</span><span>RequestActive()</span><span>&nbsp;call will also be deactivated when your app no longer has the focus&mdash;in other words, when your app goes into a suspended state. On Windows Phone mobile
 and on a Windows 10 PC or Surface Pro in Tablet mode, &ldquo;your app no longer having focus&rdquo; means whenever your app is no longer in the foreground (snapped apps still count as being in the foreground, by the way). On a Windows 10 PC in desktop mode,
 however, it will mean whenever your app is minimized. In desktop mode, note that even when another app window covers up your app window, your app is still considered by the UWP app lifecycle</span><span>&nbsp;to be running in the foreground.</span></p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>MainPage.xaml.cs</em> </li></ul>
<h1>More Information</h1>
<p><em>For more information on X, see&nbsp;&nbsp;<a title="Introduction to UWP app" href="https://msdn.microsoft.com/en-us/windows/uwp/layout/design-and-ui-intro" target="_blank">Introduction to UWP app</a></em></p>
