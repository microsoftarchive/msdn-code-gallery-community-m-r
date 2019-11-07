# Multi-Touch Example
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- Direct2D
- DirectX
- Multi-touch
- Windows 8 Store App
- Windows 8 Store Apps
- Windows Store app
- SharpDX
## Topics
- C#
- User Interface
- input
- Multi-touch
## Updated
- 05/22/2013
## Description

<h1>Introduction</h1>
<p>A example to deal with Windows 8 store app Multi-Touch feature.</p>
<p><img id="82459" src="82459-%e6%9c%aa%e5%91%bd%e5%90%8d3.png" alt="" width="760" height="427"></p>
<p>Touch with 2 fingers</p>
<p><img id="82460" src="82460-%e6%9c%aa%e5%91%bd%e5%90%8d.png" alt=""></p>
<p>Mouse move</p>
<p><img id="82461" src="82461-%e6%9c%aa%e5%91%bd%e5%90%8d5.png" alt="" width="760" height="428"></p>
<h1><strong>Description</strong></h1>
<p>In Windows 8 Store APP, touch releted events are used below:</p>
<ul>
<li><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerpressed.aspx"><strong>PointerPressed</strong></a>&nbsp;fires when user press down the screen using finger or mouse
</li><li><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointermoved.aspx"><strong>PointerMoved</strong></a>&nbsp;fires when the finger (or mouse pointer) moved during user press down&nbsp;
</li><li><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerreleased.aspx"><strong>PointerReleased</strong></a>&nbsp;fires when user lifts the finger or release mouse left-button&nbsp;<strong>IN</strong>&nbsp;controls
</li><li><strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerexited.aspx">PointerExited</a>&nbsp;</strong>fires when user move
<strong><span style="color:#000000">OUT OFF</span></strong> the controls or lifts the finger
<strong>WITH-IN</strong> controls&nbsp;&nbsp;&nbsp; </li></ul>
<p>&nbsp;</p>
<p>Here is some code&nbsp;snippet,</p>
<p>With in&nbsp;<strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerpressed.aspx"><strong>PointerPressed</strong></a></strong>, system will gave you</p>
<ul>
<li><strong>e.GetCurrentPoint(_Rect).Position</strong> &nbsp; &nbsp; Return a <strong>
<a href="http://msdn.microsoft.com/library/windows/apps/Windows.Foundation.point">Point</a></strong> class presents the Position of user input
</li><li><strong>e.Pointer.PointerDeviceType</strong> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Return&nbsp;<strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.pointer.pointerdevicetype.aspx">PointerDeviceType</a>&nbsp;</strong>presents
 type of user input (Mouse/Touch/Pen) </li><li><strong>e.Pointer.PointerId</strong> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Return&nbsp;<strong>a unique
<a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.pointer.pointerid">
PointerId</a></strong> except mouse = 1. </li></ul>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void _Rect_PointerPressed(object sender, PointerRoutedEventArgs e)
{
    // The unique PointerId
    Debug.WriteLine(&quot;[PointerPressed] Pointer ID:&quot; &#43; e.Pointer.PointerId);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;_Rect_PointerPressed(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;PointerRoutedEventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;The&nbsp;unique&nbsp;PointerId</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(<span class="cs__string">&quot;[PointerPressed]&nbsp;Pointer&nbsp;ID:&quot;</span>&nbsp;&#43;&nbsp;e.Pointer.PointerId);&nbsp;
}</pre>
</div>
</div>
</div>
<p>With in&nbsp;<strong><strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointermoved.aspx"><strong>PointerMoved</strong></a></strong></strong>, try to find the&nbsp;<strong><strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.pointer.pointerid">PointerId</a></strong></strong>&nbsp;you
 want.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void _Rect_PointerMoved(object sender, PointerRoutedEventArgs e)
{
    if (pData.PointerId == e.Pointer.PointerId)
    {
        // ... Do Something with this pointer
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;_Rect_PointerMoved(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;PointerRoutedEventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(pData.PointerId&nbsp;==&nbsp;e.Pointer.PointerId)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;...&nbsp;Do&nbsp;Something&nbsp;with&nbsp;this&nbsp;pointer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">With in&nbsp;<strong><strong><strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerreleased.aspx"><strong>PointerReleased</strong></a></strong></strong></strong>:</div>
<div class="endscriptcode"></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void _Rect_PointerReleased(object sender, PointerRoutedEventArgs e)
{
     // Find the correct PointerId
    if (pData.PointerId == e.Pointer.PointerId)
    {
          // ... Do Something with this pointer
    }
    Debug.WriteLine(&quot;[PointerReleased] Pointer ID:&quot; &#43; e.Pointer.PointerId);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;_Rect_PointerReleased(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;PointerRoutedEventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Find&nbsp;the&nbsp;correct&nbsp;PointerId</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(pData.PointerId&nbsp;==&nbsp;e.Pointer.PointerId)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;...&nbsp;Do&nbsp;Something&nbsp;with&nbsp;this&nbsp;pointer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(<span class="cs__string">&quot;[PointerReleased]&nbsp;Pointer&nbsp;ID:&quot;</span>&nbsp;&#43;&nbsp;e.Pointer.PointerId);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">With in&nbsp;<strong><strong><strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerexited.aspx">PointerExited</a></strong></strong></strong>, just do Something same as
<strong><strong><strong><strong><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerreleased.aspx"><strong>PointerReleased</strong></a></strong></strong></strong>
</strong>at this sample</div>
<div class="endscriptcode"></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void _Rect_PointerExited(object sender, PointerRoutedEventArgs e)
{
    // ... Do Something same as PointerReleased

    Debug.WriteLine(&quot;[PointerExited] Pointer ID:&quot; &#43; e.Pointer.PointerId);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;_Rect_PointerExited(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;PointerRoutedEventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;...&nbsp;Do&nbsp;Something&nbsp;same&nbsp;as&nbsp;PointerReleased</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(<span class="cs__string">&quot;[PointerExited]&nbsp;Pointer&nbsp;ID:&quot;</span>&nbsp;&#43;&nbsp;e.Pointer.PointerId);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<h1>Building the Sample</h1>
<p>This example using&nbsp;<strong>SharpDX</strong>&nbsp;for drawing.&nbsp;(Direct2D)</p>
<p>SharpDX is managed lib with Directx</p>
<p>More infomation about SharpDX, please refer&nbsp;<a title="http://sharpdx.org/" href="http://sharpdx.org/">http://sharpdx.org/</a></p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>App.xaml</em> </li><li><em>App.xaml.cs</em> </li></ul>
<ul>
<li><em>MainPage.xaml</em> </li><li><em>MainPage.xaml.cs - Main page for example</em> </li></ul>
<ul>
<li><em>PointerData.cs - the&nbsp;data structure to store pointers</em> </li><li><em>ShapeRenderer.cs - the renderer functions tells sharpDX how to paint</em>
</li></ul>
<p style="padding-left:30px">Files and DLLs from SharpDX</p>
<ul>
<li><em>SurfaceImageSourceTarget.cs</em> </li><li><em>TargetBase.cs</em> </li><li><em>DeviceManager.cs</em> </li><li><em>FpsRenderer.cs</em> </li></ul>
<ul>
<li><em>SharpDX.Direct2D1.dll</em> </li><li><em>SharpDX.Direct3D11.dll</em> </li><li><em>SharpDX.dll</em> </li><li><em>SharpDX.DXGI.dll</em> </li></ul>
<h1>More Information</h1>
<p><em><a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh465387.aspx">Quickstart: Touch input (Windows Store apps using C#/VB/C&#43;&#43; and XAML)</a></em></p>
<p><em><a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh974457.aspx">Quickstart: Capturing ink data (Windows Store apps using C#/VB/C&#43;&#43; and XAML)</a></em></p>
<p><em><a href="http://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.pointer">Pointer class</a><br>
</em></p>
<p><strong>Samples</strong></p>
<p><em><a href="http://code.msdn.microsoft.com/windowsapps/Input-3dff271b">Input: XAML user input events sample</a></em></p>
<p><em><a href="http://code.msdn.microsoft.com/windowsapps/Input-device-capabilities-31b67745">Input: Device capabilities sample</a><br>
</em></p>
