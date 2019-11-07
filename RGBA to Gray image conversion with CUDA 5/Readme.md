# RGBA to Gray image conversion with CUDA 5
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C++
- Parallel Programming
- CUDA
## Topics
- Parallel Programming
- CUDA
- GPGPU
## Updated
- 02/25/2013
## Description

<h1>Introduction</h1>
<p style="font-size:small">We will demonstrate a simple conversion from RGBA to grayscale image using CUDA 5, we will build on a
<a title="CUDA 5.0 and Visual Studio 2012 Configuration" href="http://code.msdn.microsoft.com/vstudio/CUDA-50-and-Visual-Studio-20e71aa1" target="_blank">
previous example</a> of a simple CUDA kernel. For example, the following RGB picture of flowers</p>
<p style="font-size:small"><img id="76206" src="76206-flowers.jpg" alt="" width="598" height="373"></p>
<p style="font-size:small">will look like this after running the filter:</p>
<p style="font-size:small"><img id="76207" src="76207-flowers_copy.jpg" alt="" width="599" height="410"></p>
<p style="font-size:small">and to execute the application we will call it like this:</p>
<p style="font-size:small"><img id="76287" src="76287-call.png" alt="" width="1314" height="499"></p>
<p style="font-size:small">&nbsp;</p>
<p style="font-size:small">This code will work with RGBA images where each channel (Red, Green, Blue, and Alpha) is represented by one byte (8-bits) and a range of values between 0 and 255 (2^8 - 1) for a total of 4-bytes per pixel.&nbsp;</p>
<p style="font-size:small">Gray scale images are represented by a single intensity value per pixel where each pixel is only 1 byte, so after conversion we will have an image with only one channel and a pixel size of 1 byte.</p>
<p style="font-size:small">Human eyes&nbsp;are more sensitive to green and least to blue. For that reason we will use weighted formula (<a href="http://en.wikipedia.org/wiki/Grayscale">http://en.wikipedia.org/wiki/Grayscale</a>):</p>
<p style="font-size:small">I = 0.2126 * R &#43; 0.7152 * G &#43; 0.0722 * B</p>
<p style="font-size:small">&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p style="font-size:small">Required prerequisites are: CUDA 5.0, NSight 3.0 RC1, <a title="OpenCV download" href="http://opencv.org/downloads.html" target="_blank">
OpenCV</a>, Visual Studio 2012</p>
<h1>Description</h1>
<p style="font-size:small">Start with configuring CUDA and OpenCV. The project will require the following header files and libraries:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">#pragma once

#define NOMINMAX			// Use standard library min/max

#include &lt;cassert&gt;
#include &lt;chrono&gt;
#include &lt;cmath&gt;
#include &lt;iomanip&gt;
#include &lt;iostream&gt;
#include &lt;string&gt;

#include &lt;opencv2\core\core.hpp&gt;
#include &lt;opencv2\highgui\highgui.hpp&gt;
#include &lt;opencv2\opencv.hpp&gt;

#include &lt;cuda.h&gt;
#include &lt;cuda_runtime.h&gt;
#include &lt;cuda_runtime_api.h&gt;

// My headers
#include &quot;GpuTimer.h&quot;
#include &quot;Utilities.h&quot;

// Load libraries
#pragma comment(lib, &quot;cudart&quot;)

// opencv requires debug libraries when running indebug mode
#if _DEBUG
#pragma comment(lib, &quot;opencv_core243d&quot;)
#pragma comment(lib, &quot;opencv_imgproc243d&quot;)
#pragma comment(lib, &quot;opencv_highgui243d&quot;)
#pragma comment(lib, &quot;opencv_ml243d&quot;)
#pragma comment(lib, &quot;opencv_video243d&quot;)
#pragma comment(lib, &quot;opencv_features2d243d&quot;)
#pragma comment(lib, &quot;opencv_calib3d243d&quot;)
#pragma comment(lib, &quot;opencv_objdetect243d&quot;)
#pragma comment(lib, &quot;opencv_contrib243d&quot;)
#pragma comment(lib, &quot;opencv_legacy243d&quot;)
#pragma comment(lib, &quot;opencv_flann243d&quot;)
#else
#pragma comment(lib, &quot;opencv_core243&quot;)
#pragma comment(lib, &quot;opencv_imgproc243&quot;)
#pragma comment(lib, &quot;opencv_highgui243&quot;)
#pragma comment(lib, &quot;opencv_ml243&quot;)
#pragma comment(lib, &quot;opencv_video243&quot;)
#pragma comment(lib, &quot;opencv_features2d243&quot;)
#pragma comment(lib, &quot;opencv_calib3d243&quot;)
#pragma comment(lib, &quot;opencv_objdetect243&quot;)
#pragma comment(lib, &quot;opencv_contrib243&quot;)
#pragma comment(lib, &quot;opencv_legacy243&quot;)
#pragma comment(lib, &quot;opencv_flann243&quot;)
#endif</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__preproc">#pragma&nbsp;once</span><span class="cpp__preproc">&nbsp;
&nbsp;
#define&nbsp;NOMINMAX&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;//&nbsp;Use&nbsp;standard&nbsp;library&nbsp;min/max</span><span class="cpp__preproc">&nbsp;
&nbsp;
#include&nbsp;&lt;cassert&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;chrono&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;cmath&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;iomanip&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;iostream&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;string&gt;</span><span class="cpp__preproc">&nbsp;
&nbsp;
#include&nbsp;&lt;opencv2\core\core.hpp&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;opencv2\highgui\highgui.hpp&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;opencv2\opencv.hpp&gt;</span><span class="cpp__preproc">&nbsp;
&nbsp;
#include&nbsp;&lt;cuda.h&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;cuda_runtime.h&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;cuda_runtime_api.h&gt;</span>&nbsp;
&nbsp;
<span class="cpp__com">//&nbsp;My&nbsp;headers</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&quot;GpuTimer.h&quot;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&quot;Utilities.h&quot;</span>&nbsp;
&nbsp;
<span class="cpp__com">//&nbsp;Load&nbsp;libraries</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;cudart&quot;)</span>&nbsp;
&nbsp;
<span class="cpp__com">//&nbsp;opencv&nbsp;requires&nbsp;debug&nbsp;libraries&nbsp;when&nbsp;running&nbsp;indebug&nbsp;mode</span><span class="cpp__preproc">&nbsp;
#if&nbsp;_DEBUG</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_core243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_imgproc243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_highgui243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_ml243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_video243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_features2d243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_calib3d243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_objdetect243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_contrib243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_legacy243d&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_flann243d&quot;)</span><span class="cpp__preproc">&nbsp;
#else</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_core243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_imgproc243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_highgui243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_ml243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_video243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_features2d243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_calib3d243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_objdetect243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_contrib243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_legacy243&quot;)</span><span class="cpp__preproc">&nbsp;
#pragma&nbsp;comment(lib,&nbsp;&quot;opencv_flann243&quot;)</span><span class="cpp__preproc">&nbsp;
#endif</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p style="font-size:small">The easiest way to configure OpenCV is to copy header files, lib files, and dlls to the appropriate folders on Windows.</p>
<p style="font-size:small">Copy include files to Visual Studio VC include folder</p>
<p>&nbsp;</p>
<p><img id="76197" src="76197-opencv-include.png" alt=""></p>
<p>&nbsp;</p>
<p><img id="76198" src="76198-opencv-include-vc.png" alt="" width="650" height="611">&nbsp;</p>
<p style="font-size:small">&nbsp;</p>
<p style="font-size:small">lib files go to arm64 folder (we are building for x64 architecture).</p>
<p style="font-size:small"><img id="76201" src="76201-lib.png" alt="" width="651" height="484"></p>
<p>&nbsp;</p>
<p><img id="76200" src="76200-lib-vc.png" alt="" width="746" height="670"></p>
<p>&nbsp;</p>
<p style="font-size:small">And dlls go to system32 folder.</p>
<p><img id="76202" src="76202-dll.png" alt="" width="635" height="481"></p>
<p>&nbsp;</p>
<p><img id="76203" src="76203-dll-vc.png" alt="" width="678" height="423"></p>
<p>&nbsp;</p>
<p style="font-size:small">Note that OpenCV requires debug libraries (end with letter d) when running in debug mode.</p>
<p style="font-size:small">We'll create a console application where our main function will get RGB and gray image paths from the input arguments and call into a helper class to do all the processing.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">// Main entry into the application
int main(int argc, char** argv)
{
	string imagePath;
	string outputPath;

	if (argc &gt; 2)
	{
		imagePath = string(argv[1]);
		outputPath = string(argv[2]);
	}
	else
	{
		cerr &lt;&lt; &quot;Please provide input and output image files as arguments to this application.&quot; &lt;&lt; endl;
		exit(1);
	}

	Image image;

	try
	{
		image.ConvertRGBAtoGray(imagePath, outputPath);
	}
	catch(exception&amp; e)
	{
		cerr &lt;&lt; endl &lt;&lt; &quot;ERROR: &quot; &lt;&lt; e.what() &lt;&lt; endl;
		exit(1);
	}

	cout &lt;&lt; &quot;Done!&quot; &lt;&lt; endl &lt;&lt; endl;
	return 0;
}
</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__com">//&nbsp;Main&nbsp;entry&nbsp;into&nbsp;the&nbsp;application</span>&nbsp;
<span class="cpp__datatype">int</span>&nbsp;main(<span class="cpp__datatype">int</span>&nbsp;argc,&nbsp;<span class="cpp__datatype">char</span>**&nbsp;argv)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">string</span>&nbsp;imagePath;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">string</span>&nbsp;outputPath;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">if</span>&nbsp;(argc&nbsp;&gt;&nbsp;<span class="cpp__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;imagePath&nbsp;=&nbsp;<span class="cpp__datatype">string</span>(argv[<span class="cpp__number">1</span>]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;outputPath&nbsp;=&nbsp;<span class="cpp__datatype">string</span>(argv[<span class="cpp__number">2</span>]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cerr&nbsp;&lt;&lt;&nbsp;<span class="cpp__string">&quot;Please&nbsp;provide&nbsp;input&nbsp;and&nbsp;output&nbsp;image&nbsp;files&nbsp;as&nbsp;arguments&nbsp;to&nbsp;this&nbsp;application.&quot;</span>&nbsp;&lt;&lt;&nbsp;endl;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;exit(<span class="cpp__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Image&nbsp;image;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;image.ConvertRGBAtoGray(imagePath,&nbsp;outputPath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">catch</span>(exception&amp;&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cerr&nbsp;&lt;&lt;&nbsp;endl&nbsp;&lt;&lt;&nbsp;<span class="cpp__string">&quot;ERROR:&nbsp;&quot;</span>&nbsp;&lt;&lt;&nbsp;e.what()&nbsp;&lt;&lt;&nbsp;endl;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;exit(<span class="cpp__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cout&nbsp;&lt;&lt;&nbsp;<span class="cpp__string">&quot;Done!&quot;</span>&nbsp;&lt;&lt;&nbsp;endl&nbsp;&lt;&lt;&nbsp;endl;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">return</span>&nbsp;<span class="cpp__number">0</span>;&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p style="font-size:small">The image class will contain representation of images on the host (CPU) and the device (GPU) and one function to do the conversion from RGBA to gray.</p>
<div style="font-size:small">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">namespace Bisque
{
	using cv::Mat;
	using std::string;

	// class Image
	class Image
	{
	public:
		Image(void);
		~Image(void);

		void ConvertRGBAtoGray(const string&amp; imagePath, const string&amp; outputPath);

	private:
		struct ImageHandle
		{
			Mat rgba;
			Mat gray;
		};

		// struct KernelMap
		struct KernelMap
		{
			uchar4*			rgba;				// rgba image: 4 bytes per image
			unsigned char*	gray;				// gray image: 1 byte per image
		};

	private:
		void RGBAtoGrayOnCPU		(unsigned char* gray, const uchar4* const rgba, int rows, int cols);
		void InitializeKernel		(KernelMap&amp; host, KernelMap&amp; device, const string&amp; imagePath);
		void SaveGrayImageToDisk	(const string&amp; imagePath);
		void VerifyGpuComputation	(const uchar4* const rgba);

	private:
		KernelMap		m_device;
		ImageHandle		m_host;
	};
}</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__keyword">namespace</span>&nbsp;Bisque&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">using</span>&nbsp;cv::Mat;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">using</span>&nbsp;std::<span class="cpp__datatype">string</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;class&nbsp;Image</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">class</span>&nbsp;Image&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">public</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image(<span class="cpp__keyword">void</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;~Image(<span class="cpp__keyword">void</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">void</span>&nbsp;ConvertRGBAtoGray(<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">string</span>&amp;&nbsp;imagePath,&nbsp;<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">string</span>&amp;&nbsp;outputPath);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">private</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">struct</span>&nbsp;ImageHandle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Mat&nbsp;rgba;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Mat&nbsp;gray;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;struct&nbsp;KernelMap</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">struct</span>&nbsp;KernelMap&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uchar4*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rgba;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;rgba&nbsp;image:&nbsp;4&nbsp;bytes&nbsp;per&nbsp;image</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;unsigned&nbsp;<span class="cpp__datatype">char</span>*&nbsp;&nbsp;&nbsp;&nbsp;gray;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;gray&nbsp;image:&nbsp;1&nbsp;byte&nbsp;per&nbsp;image</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">private</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">void</span>&nbsp;RGBAtoGrayOnCPU&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(unsigned&nbsp;<span class="cpp__datatype">char</span>*&nbsp;gray,&nbsp;<span class="cpp__keyword">const</span>&nbsp;uchar4*&nbsp;<span class="cpp__keyword">const</span>&nbsp;rgba,&nbsp;<span class="cpp__datatype">int</span>&nbsp;rows,&nbsp;<span class="cpp__datatype">int</span>&nbsp;cols);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">void</span>&nbsp;InitializeKernel&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(KernelMap&amp;&nbsp;host,&nbsp;KernelMap&amp;&nbsp;device,&nbsp;<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">string</span>&amp;&nbsp;imagePath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">void</span>&nbsp;SaveGrayImageToDisk&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">string</span>&amp;&nbsp;imagePath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">void</span>&nbsp;VerifyGpuComputation&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cpp__keyword">const</span>&nbsp;uchar4*&nbsp;<span class="cpp__keyword">const</span>&nbsp;rgba);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">private</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;KernelMap&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_device;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageHandle&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_host;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;The convert to gray function has to do the following 3 things:</div>
<ol>
<li>
<div class="endscriptcode">Initialize kernel and copy RGBA image to the GPU memory;</div>
</li><li>
<div class="endscriptcode">Execute CUDA kernel;</div>
</li><li>
<div class="endscriptcode">Copy gray image from the GPU back to main memory.</div>
</li></ol>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">//Converts r8g8b8a8 image to one channel gray
void Image::ConvertRGBAtoGray(const string&amp; imagePath, const string&amp; outputPath)
{
	// Load image and initialize kernel
	KernelMap host;
	KernelMap device;

	InitializeKernel(host, device, imagePath);

	// Run kernel: convert rgba image to gray
	RunRGBAtoGrayKernel(
		device.gray, 
		device.rgba, 
		m_host.rgba.rows, 
		m_host.rgba.cols
		);

	// Save gray image to disk
	SaveGrayImageToDisk(outputPath);

#if 1			// Change to 1 to enable
	// Validate GPU convertion against CPU result.
	// Only turn it when you want to run validation because CPU calculation will be slow.
	VerifyGpuComputation(host.rgba);
#endif

	// Release cuda
	hr = cudaFree(m_device.gray);
	hr = cudaFree(m_device.rgba);
}
</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__com">//Converts&nbsp;r8g8b8a8&nbsp;image&nbsp;to&nbsp;one&nbsp;channel&nbsp;gray</span>&nbsp;
<span class="cpp__keyword">void</span>&nbsp;Image::ConvertRGBAtoGray(<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">string</span>&amp;&nbsp;imagePath,&nbsp;<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">string</span>&amp;&nbsp;outputPath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Load&nbsp;image&nbsp;and&nbsp;initialize&nbsp;kernel</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;KernelMap&nbsp;host;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;KernelMap&nbsp;device;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;InitializeKernel(host,&nbsp;device,&nbsp;imagePath);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Run&nbsp;kernel:&nbsp;convert&nbsp;rgba&nbsp;image&nbsp;to&nbsp;gray</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;RunRGBAtoGrayKernel(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;device.gray,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;device.rgba,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_host.rgba.rows,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_host.rgba.cols&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Save&nbsp;gray&nbsp;image&nbsp;to&nbsp;disk</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SaveGrayImageToDisk(outputPath);<span class="cpp__preproc">&nbsp;
&nbsp;
#if&nbsp;1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;//&nbsp;Change&nbsp;to&nbsp;1&nbsp;to&nbsp;enable</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Validate&nbsp;GPU&nbsp;convertion&nbsp;against&nbsp;CPU&nbsp;result.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Only&nbsp;turn&nbsp;it&nbsp;when&nbsp;you&nbsp;want&nbsp;to&nbsp;run&nbsp;validation&nbsp;because&nbsp;CPU&nbsp;calculation&nbsp;will&nbsp;be&nbsp;slow.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;VerifyGpuComputation(host.rgba);<span class="cpp__preproc">&nbsp;
#endif</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Release&nbsp;cuda</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;hr&nbsp;=&nbsp;cudaFree(m_device.gray);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;hr&nbsp;=&nbsp;cudaFree(m_device.rgba);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">In the kernel.cu CUDA file, we have to allocate number of parallel executions on which we will process each pixel in the image and call actual kernel</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">// Runs r8g8b8a8 to gray kernel
void RunRGBAtoGrayKernel(
		unsigned char*	gray,				// gray image: 1 byte per image --&gt; this will be returned from this function
		uchar4*			rgba,				// rgba image: 4 bytes per image
		int				rows,				// image size: number of rows
		int				cols				// image size: number of columns
	)
{
	const char* func = &quot;RunGrayKernel&quot;;

	cudaError hr = cudaSuccess;

	int x = static_cast&lt;int&gt;(ceilf(static_cast&lt;float&gt;(cols) / BLOCK_WIDTH));
	int y = static_cast&lt;int&gt;(ceilf(static_cast&lt;float&gt;(rows) / BLOCK_WIDTH));

	const dim3 grid (x, y, 1);								// number of blocks
	const dim3 block(BLOCK_WIDTH, BLOCK_WIDTH, 1);			// block width: number of threads per block
		
	rgba_to_grayscale&lt;&lt;&lt;grid, block&gt;&gt;&gt;(gray, rgba, rows, cols);

	hr = cudaDeviceSynchronize();																CHECK_CUDA_ERROR(hr, func, &quot;rgba_to_grayscale failed.&quot;);
}
</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__com">//&nbsp;Runs&nbsp;r8g8b8a8&nbsp;to&nbsp;gray&nbsp;kernel</span>&nbsp;
<span class="cpp__keyword">void</span>&nbsp;RunRGBAtoGrayKernel(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;unsigned&nbsp;<span class="cpp__datatype">char</span>*&nbsp;&nbsp;&nbsp;&nbsp;gray,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;gray&nbsp;image:&nbsp;1&nbsp;byte&nbsp;per&nbsp;image&nbsp;--&gt;&nbsp;this&nbsp;will&nbsp;be&nbsp;returned&nbsp;from&nbsp;this&nbsp;function</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uchar4*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rgba,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;rgba&nbsp;image:&nbsp;4&nbsp;bytes&nbsp;per&nbsp;image</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rows,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;image&nbsp;size:&nbsp;number&nbsp;of&nbsp;rows</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cols&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;image&nbsp;size:&nbsp;number&nbsp;of&nbsp;columns</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">char</span>*&nbsp;func&nbsp;=&nbsp;<span class="cpp__string">&quot;RunGrayKernel&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cudaError&nbsp;hr&nbsp;=&nbsp;cudaSuccess;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;x&nbsp;=&nbsp;<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">int</span>&gt;(ceilf(<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">float</span>&gt;(cols)&nbsp;/&nbsp;BLOCK_WIDTH));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;y&nbsp;=&nbsp;<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">int</span>&gt;(ceilf(<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">float</span>&gt;(rows)&nbsp;/&nbsp;BLOCK_WIDTH));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">const</span>&nbsp;dim3&nbsp;grid&nbsp;(x,&nbsp;y,&nbsp;<span class="cpp__number">1</span>);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;number&nbsp;of&nbsp;blocks</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">const</span>&nbsp;dim3&nbsp;block(BLOCK_WIDTH,&nbsp;BLOCK_WIDTH,&nbsp;<span class="cpp__number">1</span>);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;block&nbsp;width:&nbsp;number&nbsp;of&nbsp;threads&nbsp;per&nbsp;block</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rgba_to_grayscale&lt;&lt;&lt;grid,&nbsp;block&gt;&gt;&gt;(gray,&nbsp;rgba,&nbsp;rows,&nbsp;cols);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;hr&nbsp;=&nbsp;cudaDeviceSynchronize();&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHECK_CUDA_ERROR(hr,&nbsp;func,&nbsp;<span class="cpp__string">&quot;rgba_to_grayscale&nbsp;failed.&quot;</span>);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">Here, before calling the kernel, we have to decide on the number of threads that we want to process the image on. Naturally, we want threads to be the same or close to the image size: x corresponding to image rows, and y to image
 columns. That's what this code does where M is number of threads per block, I selected 32. Variable block below means block width, or number of threads per block. Variable grid is number of blocks. To remind, GPU runs blocks of threads on Streaming Multiprocessors
 where a block can have up to 1024 threads. Block of threads executes on the same SM.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">	int x = static_cast&lt;int&gt;(ceilf(static_cast&lt;float&gt;(cols) / BLOCK_WIDTH));
	int y = static_cast&lt;int&gt;(ceilf(static_cast&lt;float&gt;(rows) / BLOCK_WIDTH));

	const dim3 grid (x, y, 1);								// number of blocks
	const dim3 block(BLOCK_WIDTH, BLOCK_WIDTH, 1);			// block width: number of threads per block
		
	rgba_to_grayscale&lt;&lt;&lt;grid, block&gt;&gt;&gt;(gray, rgba, rows, cols);
</pre>
<div class="preview">
<pre class="cplusplus">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;x&nbsp;=&nbsp;<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">int</span>&gt;(ceilf(<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">float</span>&gt;(cols)&nbsp;/&nbsp;BLOCK_WIDTH));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;y&nbsp;=&nbsp;<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">int</span>&gt;(ceilf(<span class="cpp__keyword">static_cast</span>&lt;<span class="cpp__datatype">float</span>&gt;(rows)&nbsp;/&nbsp;BLOCK_WIDTH));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">const</span>&nbsp;dim3&nbsp;grid&nbsp;(x,&nbsp;y,&nbsp;<span class="cpp__number">1</span>);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;number&nbsp;of&nbsp;blocks</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">const</span>&nbsp;dim3&nbsp;block(BLOCK_WIDTH,&nbsp;BLOCK_WIDTH,&nbsp;<span class="cpp__number">1</span>);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;block&nbsp;width:&nbsp;number&nbsp;of&nbsp;threads&nbsp;per&nbsp;block</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rgba_to_grayscale&lt;&lt;&lt;grid,&nbsp;block&gt;&gt;&gt;(gray,&nbsp;rgba,&nbsp;rows,&nbsp;cols);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode"></div>
</div>
<div class="endscriptcode">In the kernel, we have to compute pixel location from block id along x and y coordinates, block width (32 threads per block), and thread id within the block.</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>
<pre class="hidden">// Converts RGBA image to gray scale intensity using the following formula:
// I = 0.2126 * R &#43; 0.7152 * G &#43; 0.0722 * B
__global__ 
void rgba_to_grayscale(unsigned char* const gray, const uchar4* const rgba, int rows, int cols)
{
	int r			=  blockIdx.y * blockDim.y &#43; threadIdx.y;		// current row
	int c			=  blockIdx.x * blockDim.x &#43; threadIdx.x;		// current column

	if ((r &lt; rows) &amp;&amp; (c &lt; cols))
	{
		int idx			= c &#43; cols * r;		// current pixel index

		uchar4 pixel	= rgba[idx];
		float intensity = 0.2126f * pixel.x &#43; 0.7152f * pixel.y &#43; 0.0722f * pixel.z;

		gray[idx]		= (unsigned char)intensity;
	}
}
</pre>
<div class="preview">
<pre class="cplusplus"><span class="cpp__com">//&nbsp;Converts&nbsp;RGBA&nbsp;image&nbsp;to&nbsp;gray&nbsp;scale&nbsp;intensity&nbsp;using&nbsp;the&nbsp;following&nbsp;formula:</span>&nbsp;
<span class="cpp__com">//&nbsp;I&nbsp;=&nbsp;0.2126&nbsp;*&nbsp;R&nbsp;&#43;&nbsp;0.7152&nbsp;*&nbsp;G&nbsp;&#43;&nbsp;0.0722&nbsp;*&nbsp;B</span>&nbsp;
__global__&nbsp;&nbsp;
<span class="cpp__keyword">void</span>&nbsp;rgba_to_grayscale(unsigned&nbsp;<span class="cpp__datatype">char</span>*&nbsp;<span class="cpp__keyword">const</span>&nbsp;gray,&nbsp;<span class="cpp__keyword">const</span>&nbsp;uchar4*&nbsp;<span class="cpp__keyword">const</span>&nbsp;rgba,&nbsp;<span class="cpp__datatype">int</span>&nbsp;rows,&nbsp;<span class="cpp__datatype">int</span>&nbsp;cols)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;r&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;&nbsp;blockIdx.y&nbsp;*&nbsp;blockDim.y&nbsp;&#43;&nbsp;threadIdx.y;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;current&nbsp;row</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;c&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;&nbsp;blockIdx.x&nbsp;*&nbsp;blockDim.x&nbsp;&#43;&nbsp;threadIdx.x;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;current&nbsp;column</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">if</span>&nbsp;((r&nbsp;&lt;&nbsp;rows)&nbsp;&amp;&amp;&nbsp;(c&nbsp;&lt;&nbsp;cols))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">int</span>&nbsp;idx&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;c&nbsp;&#43;&nbsp;cols&nbsp;*&nbsp;r;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;current&nbsp;pixel&nbsp;index</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uchar4&nbsp;pixel&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rgba[idx];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">float</span>&nbsp;intensity&nbsp;=&nbsp;<span class="cpp__number">0</span>.2126f&nbsp;*&nbsp;pixel.x&nbsp;&#43;&nbsp;<span class="cpp__number">0</span>.7152f&nbsp;*&nbsp;pixel.y&nbsp;&#43;&nbsp;<span class="cpp__number">0</span>.0722f&nbsp;*&nbsp;pixel.z;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gray[idx]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(unsigned&nbsp;<span class="cpp__datatype">char</span>)intensity;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">Finally, we check conversion results by runnig the same calculation on CPU and comparing pixels.</div>
</div>
<p style="font-size:small">&nbsp;</p>
<h1>Source Code Files</h1>
<dl><dt>program.cpp - main function.</dt></dl>
<p>image.h and image.cpp - responsible for loading and saving images, initializing CUDA kernel and managing memory;</p>
<p>utilities - checks runtime errors and compares pixels from GPU conversion to reference conversion on CPU;</p>
<p>gputimer - events to measure execution time on the GPU in milliseconds</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<dl></dl>
