# QR Code Painter w/Fukuchi libqrencode
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- Graphics
- Win32
- C++
- Windows SDK
- Windows
- Graphics Functions
## Topics
- Graphics
- Graphics Functions
## Updated
- 05/18/2015
## Description

<h1>Introduction</h1>
<p>The program paints a QR code, using the Fukuchi libqrencode library to generate the QR code<em>.
</em>&nbsp;The code generation painting is done in a helper class that can be used in isolation from the rest of the program.</p>
<h1><span>Building the Sample</span></h1>
<p>The sample requires the <a href="https://fukuchi.org/works/qrencode/" target="_blank">
Fukuchi libqrencode</a> library. &nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>The sample is based on the basic Visual Studio Win32 template. &nbsp;The program paints a QR code of an argument passed on the command line, defaulting to 'qrdemo.' &nbsp;Below is a screenshot.</p>
<p>The modifications to the main CPP file is minimal: &nbsp;Some initialization code to include the global QR Painter instance, and a modification to the handling of WM_PAINT in the main window procedure.</p>
<p>Most of the sample code is in QRPainter.cpp. &nbsp;The CQRPainter class takes a string from which to generate a QR code, and a PaintData() method that takes an HWND, HDC, and RECT to paint into.&nbsp;</p>
<p>CQRPainter caches the HBITMAP for the QR code, so that repainting requires minimal work. &nbsp;If the QR code gets changed, which happens through a call to InitializeData() method, the HBITMAP will be invalidated and recreated on the next paint operation.</p>
<p>Since QR readers work best when the QR code image is square, the CQRPainter paint method produces the image inside the largest possible square in the RECT that was passed in.&nbsp;</p>
<p>&nbsp;</p>
<p><img id="137852" src="137852-qrdemoscreenshot.png" alt="" width="720" height="372"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">	case WM_PAINT:
		hdc = BeginPaint(hWnd, &amp;ps);
        g_qrPainter.PaintData(hWnd, hdc, ps.rcPaint);
		EndPaint(hWnd, &amp;ps);
</pre>
<div class="preview">
<pre class="cplusplus">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">case</span>&nbsp;WM_PAINT:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;hdc&nbsp;=&nbsp;BeginPaint(hWnd,&nbsp;&amp;ps);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;g_qrPainter.PaintData(hWnd,&nbsp;hdc,&nbsp;ps.rcPaint);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EndPaint(hWnd,&nbsp;&amp;ps);&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>QRPainter.cpp</em> </li></ul>
<h1>More Information</h1>
<p>The sample requires the Fukuchi libqrencode library. &nbsp;You must <a href="https://fukuchi.org/works/qrencode/">
download</a> it separately and the library has additional licensing requirements.</p>
