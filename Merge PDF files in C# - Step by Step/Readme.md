# Merge PDF files in C# - Step by Step
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
- Silverlight
- ASP.NET
- VB.Net
- .NET Framework 4.0
## Topics
- Controls
- Graphics
- C#
- User Interface
- Graphics and 3D
- How to
## Updated
- 04/26/2016
## Description

<h1>Introduction</h1>
<p><em>This is a C# example to merge PDFs via a free C# PDF library.</em></p>
<p><em><br>
Stop the searching - you're in the right place! The PDF Vison .Net is only the one library designed for this purpose.</em></p>
<p><em><br>
Only the .Net platform and nothing else, 32 and 64-bit support, Full Trust level, converting of all types of Images, merge PDF, works under Windows, Mac, Linux and a lot of other nuances</em><em>.</em></p>
<p><span style="font-size:20px; font-weight:bold">Main Functions</span></p>
<p><span style="font-size:20px; font-weight:bold"><img id="152357" src="152357-pdf%20merge_.png" alt=""></span></p>
<p><em>The code:</em></p>
<p><em><img id="152360" src="152360-code.png" alt="" width="608" height="414"><br>
</em></p>
<p><em><br>
</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;
using System.IO;
using System.Collections;


namespace Sample
{
    class Test
    {
        static void Main(string[] args)
        {
            // Merge PDF files		
            SautinSoft.PdfVision v = new SautinSoft.PdfVision();
           
            //merge 3 PDF files
            string[] files = new string[3];
            files[0] = @&quot;d:\Tempos\image1.pdf&quot;;
            files[1] = @&quot;d:\Tempos\image2.pdf&quot;;
            files[2] = @&quot;d:\Tempos\image3.pdf&quot;;

            // You may download the latest version of SDK here:   
            // www.sautinsoft.com/products/convert-html-pdf-and-tiff-pdf-asp.net/download.php 
            int ret = v.MergePDFFileArrayToPDFFile(files, @&quot;D:\Tempos\hardcopy.pdf&quot;);

            //0 - merged successfully
            //1 - error, can't merge PDF documents
            //2 - error, can't create output file, probably it used by another application
            //3 - merging failed
            //4 - merged successfully, but some files were not merged
            
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.IO;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections;&nbsp;
&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;Sample&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Test&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Merge&nbsp;PDF&nbsp;files&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SautinSoft.PdfVision&nbsp;v&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SautinSoft.PdfVision();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//merge&nbsp;3&nbsp;PDF&nbsp;files</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;files&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">string</span>[<span class="cs__number">3</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;files[<span class="cs__number">0</span>]&nbsp;=&nbsp;@<span class="cs__string">&quot;d:\Tempos\image1.pdf&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;files[<span class="cs__number">1</span>]&nbsp;=&nbsp;@<span class="cs__string">&quot;d:\Tempos\image2.pdf&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;files[<span class="cs__number">2</span>]&nbsp;=&nbsp;@<span class="cs__string">&quot;d:\Tempos\image3.pdf&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;You&nbsp;may&nbsp;download&nbsp;the&nbsp;latest&nbsp;version&nbsp;of&nbsp;SDK&nbsp;here:&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;www.sautinsoft.com/products/convert-html-pdf-and-tiff-pdf-asp.net/download.php&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;ret&nbsp;=&nbsp;v.MergePDFFileArrayToPDFFile(files,&nbsp;@<span class="cs__string">&quot;D:\Tempos\hardcopy.pdf&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//0&nbsp;-&nbsp;merged&nbsp;successfully</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//1&nbsp;-&nbsp;error,&nbsp;can't&nbsp;merge&nbsp;PDF&nbsp;documents</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//2&nbsp;-&nbsp;error,&nbsp;can't&nbsp;create&nbsp;output&nbsp;file,&nbsp;probably&nbsp;it&nbsp;used&nbsp;by&nbsp;another&nbsp;application</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//3&nbsp;-&nbsp;merging&nbsp;failed</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//4&nbsp;-&nbsp;merged&nbsp;successfully,&nbsp;but&nbsp;some&nbsp;files&nbsp;were&nbsp;not&nbsp;merged</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<h1>How to do it:</h1>
<p><em>So, here we'll show you in details how to merge PDF in C#.</em></p>
<p><em><strong><span class="blue12b">Very simple example.</span></strong>&nbsp;For example, we've the three PDF files file: image1.jpg,
<em>image2.jpg,&nbsp;<em>image3</em></em></em><em><em><em>.jpg</em></em>&nbsp;(please see in att. file) and we need to create one PDF document from hardcopy.jpg</em></p>
<p><em><span class="blue12b"><strong>Step 1</strong>:</span>&nbsp;Launch Visual Studio 2010 (2013). Click File-&gt;New Project-&gt;Visual C# Console Application.</em></p>
<p><em>Type the application name and location, for example &quot;merge PDF&quot; and &quot;c:\samples&quot;.</em></p>
<p><em><span class="blue12b"><strong>Step 2</strong>:</span>&nbsp;In the Solution Explorer right-click on &quot;References&quot; and select &quot;Add Reference&quot;. Next add a reference to the &quot;SautinSoft.PdfVison.dll&quot;</em><em>.</em></p>
<p><em><span class="blue12b"><strong>Step 3</strong>:</span>&nbsp;So, we've created an empty C# console application. Now type the C# code to merge PDF files</em></p>
<p><em><strong>Step 4</strong>: Please insert c# code in your console application.&nbsp;Now build the application and launch it.</em></p>
<p><em><strong><span class="blue12b">Well done!</span>&nbsp;</strong>Our congratulations, with help of the PDF Visoin.Net library we've created an PDF file.</em></p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<div><em>Related Links:</em></div>
<div><em><br>
Website:&nbsp;<a href="http://www.sautinsoft.com/">www.sautinsoft.com</a><br>
Product Home:&nbsp;<a href="http://sautinsoft.com/products/convert-html-pdf-and-tiff-pdf-asp.net/html-to-pdf-jpeg-tiff-gif-to-pdf-asp.net.php">PDF Vision .NET</a><br>
Download:&nbsp;<a href="http://sautinsoft.com/thankyou.php?download=5">PDF Vision .NET</a><br>
</em></div>
<h2 class="H2Text">Requrements and Technical Information</h2>
<p class="CommonText"><em>Requires only .Net 2.0 or above. Our product is compatible with all .Net languages and supports all Operating Systems where .Net Framework can be used. Note that PDF Vision.Net is entirely written in managed C#, which makes it absolutely
 standalone and an independent library</em></p>
<p><em><br>
</em></p>
