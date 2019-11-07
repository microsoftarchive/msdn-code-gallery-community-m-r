# MFC Direct2D Hello World sample
## Requires
- Visual Studio 2010
## License
- MS-LPL
## Technologies
- Direct2D
- MFC
## Topics
- Graphics
- Drawing
- How to
## Updated
- 12/13/2011
## Description

<h1>Introduction</h1>
<p><em>Shows how to create a very simple MFC Direct2D application, or how to convert an existing MFC application to Direct2D.</em></p>
<p><em>This code is the illustration of this blog article: <a href="http://www.blogmfc.com/n2011/12/14/mfc-and-direct2d-part-1n/">
MFC and Direct2D, part 1/N</a></em></p>
<h1><span>Building the Sample</span></h1>
<p><em><em>To build this sample, open the solution (.sln) file titled Helloworld.sln (C&#43;&#43; project) &nbsp;from Visual Studio 2010 SP1 (any SKU). Press F7 or go to Build-&gt;Build Solution from the top menu after the sample has loaded.</em></em></p>
<p><em><em>The Service Pack 1 for Visual Studio 2010 is required.</em></em></p>
<h1>Runing the Sample</h1>
<div><em>To run this sample after building it, press F5 (run with debugging enabled) or Ctrl-F5 (run without debugging enabled) from Visual Studio 2010 SP1 (any SKU). (Or select the corresponding options from the Debug menu.)</em></div>
<div><em><br>
</em></div>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>Description : see blog article on www.blogmfc.com</em></p>
<p><em><a href="http://www.blogmfc.com/n2011/12/14/mfc-and-direct2d-part-1n/">http://www.blogmfc.com/n2011/12/14/mfc-and-direct2d-part-1n/</a><br>
</em></p>
<p><em>The blog article shows how to convert an MFC application which uses GDI to a Direct2D MFC application, and focuses on explaining how the things work under the hood.</em></p>
<p><em>The code sample uses a simple MFC AppWizard generated application. Only the code of the View is modified (file HelloWorldApp.cpp / .h)</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">using namespace D2D1;

CHelloWorldView::CHelloWorldView()
{
	// Initialize Direct2D
	EnableD2DSupport();

	// Create D2D graphic resources
	m_pBlueBrush = new CD2DSolidColorBrush(GetRenderTarget(), ColorF(ColorF::RoyalBlue));

	m_pTextFormat = new CD2DTextFormat(GetRenderTarget(), _T(&quot;Gabriola&quot;), 50);
	m_pTextFormat-&gt;Get()-&gt;SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER);
	m_pTextFormat-&gt;Get()-&gt;SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_CENTER);
}



// AFX_WM_DRAW2D event handler
afx_msg LRESULT CHelloWorldView::OnDraw2d(WPARAM wParam, LPARAM lParam)
{
	CHwndRenderTarget* pRenderTarget = (CHwndRenderTarget*)lParam;
	ASSERT_VALID(pRenderTarget);

	// Clear window background
	pRenderTarget-&gt;Clear(ColorF(ColorF::Beige));

	// Draw text
	CRect rect;
	GetClientRect(rect);
	pRenderTarget-&gt;DrawText(_T(&quot;Hello, World!&quot;), rect, m_pBlueBrush, m_pTextFormat);
	return TRUE;
}
</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__keyword">using</span>&nbsp;<span class="cpp__keyword">namespace</span>&nbsp;D2D1;&nbsp;
&nbsp;
CHelloWorldView::CHelloWorldView()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Initialize&nbsp;Direct2D</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;EnableD2DSupport();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Create&nbsp;D2D&nbsp;graphic&nbsp;resources</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;m_pBlueBrush&nbsp;=&nbsp;<span class="cpp__keyword">new</span>&nbsp;CD2DSolidColorBrush(GetRenderTarget(),&nbsp;ColorF(ColorF::RoyalBlue));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;m_pTextFormat&nbsp;=&nbsp;<span class="cpp__keyword">new</span>&nbsp;CD2DTextFormat(GetRenderTarget(),&nbsp;_T(<span class="cpp__string">&quot;Gabriola&quot;</span>),&nbsp;<span class="cpp__number">50</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;m_pTextFormat-&gt;Get()-&gt;SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;m_pTextFormat-&gt;Get()-&gt;SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_CENTER);&nbsp;
}&nbsp;
&nbsp;
&nbsp;
&nbsp;
<span class="cpp__com">//&nbsp;AFX_WM_DRAW2D&nbsp;event&nbsp;handler</span>&nbsp;
afx_msg&nbsp;<span class="cpp__datatype">LRESULT</span>&nbsp;CHelloWorldView::OnDraw2d(<span class="cpp__datatype">WPARAM</span>&nbsp;wParam,&nbsp;<span class="cpp__datatype">LPARAM</span>&nbsp;lParam)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CHwndRenderTarget*&nbsp;pRenderTarget&nbsp;=&nbsp;(CHwndRenderTarget*)lParam;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ASSERT_VALID(pRenderTarget);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Clear&nbsp;window&nbsp;background</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pRenderTarget-&gt;Clear(ColorF(ColorF::Beige));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Draw&nbsp;text</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CRect&nbsp;rect;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;GetClientRect(rect);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pRenderTarget-&gt;DrawText(_T(<span class="cpp__string">&quot;Hello,&nbsp;World!&quot;</span>),&nbsp;rect,&nbsp;m_pBlueBrush,&nbsp;m_pTextFormat);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">return</span>&nbsp;TRUE;&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>helloworldview.cpp - the code of the View window which displays contents on the screen. The code of this file is modified to use Direct2D instead of GDI</em>
</li><li><em><em>helloworldview.h - header file for helloworldview.cpp</em></em> </li></ul>
