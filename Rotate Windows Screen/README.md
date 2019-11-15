# Rotate Windows Screen
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
- Windows Forms
- Visual Studio
- Visual Studio 2015
- windows form application
## Topics
- C#
- Rotate Screen
- Tilt Screen
## Updated
- 08/30/2017
## Description

<p><span style="font-size:small">This code sample demonstartes how to rotate screen in c# application.</span></p>
<p><span style="font-size:small">Build Instructions:</span></p>
<ul>
<li><span style="font-size:small">download source file </span></li><li><span style="font-size:small">extract it, open .sln file </span></li><li><span style="font-size:small">build and run </span></li></ul>
<p><span style="font-size:small">Note: while using this application, if your screen is tilted it migt be difficult for you to control mouse. you can press short key
<strong>CTRL &#43; ALT &#43; UP_KEY</strong> to make screen in default position.</span></p>
<p><span style="font-size:small">You can change orientation of screen by pressing CTRL &#43; ALT &#43; AEROKEYS [UP, DOWN, RIGHT, LEFT].</span></p>
<p><span style="font-size:small">this application uses same technique to change orientation of windows screen [potrait or landscape].</span></p>
<p><span style="font-size:small">in this code sample keys are pressing virtually to roate screen.</span></p>
<p>&nbsp;</p>
<div><span style="font-size:small">Create a new windows form application project</span></div>
<div><span style="font-size:small"><br>
</span></div>
<div><span style="font-size:small">Add 4 buttons and label, change the button properties as desired from properties windows&nbsp;</span></div>
<div>&nbsp; <img id="178602" src="178602-default.png" alt="" width="433" height="370"></div>
<div></div>
<div><span style="font-size:small">double click on each buttons to create event handling functions</span></div>
<div></div>
<div><span style="font-size:small">Now add following references in your code</span></div>
<div></div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span style="font-size:small">C#</span></div>
<div class="pluginLinkHolder"><span style="font-size:small"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></span></div>
<span class="hidden" style="font-size:small">csharp </span>

<div class="preview">
<pre class="csharp"><span style="font-size:small"><span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.Threading.aspx" target="_blank" title="Auto generated link to System.Threading">System.Threading</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.Runtime.InteropServices.aspx" target="_blank" title="Auto generated link to System.Runtime.InteropServices">System.Runtime.InteropServices</a>;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;we are using windows API so we need to reference &quot;user32.dll&quot; file</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span style="font-size:small">C#</span></div>
<div class="pluginLinkHolder"><span style="font-size:small"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></span></div>
<span class="hidden" style="font-size:small">csharp </span>

<div class="preview">
<pre class="csharp"><span style="font-size:small">&nbsp;[DllImport(<span class="cs__string">&quot;user32.dll&quot;</span>)]&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">extern</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;keybd_event(<span class="cs__keyword">byte</span>&nbsp;bVk,&nbsp;<span class="cs__keyword">byte</span>&nbsp;bScan,&nbsp;<span class="cs__keyword">uint</span>&nbsp;dwFlags,&nbsp;<span class="cs__keyword">int</span>&nbsp;dwExtraInfo);</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode"><span style="font-size:small">we need to define our button keys ,we are using hex code , their is different code for differnet virtual key</span></div>
<div class="endscriptcode"><span style="font-size:small">you can check it from</span></div>
<div class="endscriptcode"><span style="color:#008080; font-size:small">https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span style="font-size:small">C#</span></div>
<div class="pluginLinkHolder"><span style="font-size:small"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></span></div>
<span class="hidden" style="font-size:small">csharp </span>

<div class="preview">
<pre class="csharp"><span style="font-size:small"><span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_RETURN&nbsp;=&nbsp;0x0D;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">uint</span>&nbsp;KEYEVENTF_KEYUP&nbsp;=&nbsp;0x0002;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_CTRL&nbsp;=&nbsp;0x11;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_SNAPSHOT&nbsp;=&nbsp;0x2C;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_ALT&nbsp;=&nbsp;0x12;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_RIGHT&nbsp;=&nbsp;0x27;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_UP&nbsp;=&nbsp;0x26;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_LEFT&nbsp;=&nbsp;0x25;&nbsp;
<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;VK_DOWN&nbsp;=&nbsp;0x28;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">in order to release pressed keys use&nbsp;</span></div>
</div>
<div class="endscriptcode"><span style="color:#000080; font-size:small">const uint KEYEVENTF_KEYUP = 0x0002;</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">we must released pressed keys, otherwise it might create problem</span></div>
<div class="endscriptcode"><span style="color:#000080; font-size:small"><span style="color:#000000">&nbsp;</span><br>
</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="color:#000000; font-size:small">here are different functions for buttons to make screen rotate right, left, default position and upside down</span></div>
<div class="endscriptcode"><span style="color:#000000">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span style="font-size:small">C#</span></div>
<div class="pluginLinkHolder"><span style="font-size:small"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></span></div>
<span class="hidden" style="font-size:small">csharp </span>

<div class="preview">
<pre class="csharp"><span style="font-size:small">&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;btnDefault_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_UP,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread.Sleep(<span class="cs__number">100</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_UP,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;btnRotateRight_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_RIGHT,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread.Sleep(<span class="cs__number">100</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_RIGHT,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;btnRotateDown_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_DOWN,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread.Sleep(<span class="cs__number">100</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_DOWN,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;btnRotateLeft_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_LEFT,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread.Sleep(<span class="cs__number">100</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_CTRL,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_ALT,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keybd_event(VK_LEFT,&nbsp;<span class="cs__number">0</span>,&nbsp;KEYEVENTF_KEYUP,&nbsp;<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><img id="178641" src="https://i1.code.msdn.s-msft.com/rotate-windows-screen-d76d85c7/image/file/178641/1/right%20rotated.png" alt=""></div>
<div class="endscriptcode"><strong><span style="font-size:small">Potrait View when rotated right</span></strong></div>
<div class="endscriptcode">if you have any suggestions or queries you can ask me at Q &amp; A area. or email me</div>
<div class="endscriptcode">at</div>
<div class="endscriptcode">umairnadeem20@hotmail.com&nbsp;</div>
<br>
</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
</div>
<div></div>
<div></div>
