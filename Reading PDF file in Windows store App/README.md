# Reading PDF file in Windows store App
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
- Win32
- XAML
- .NET Framework
- Javascript
- HTML5
- Windows Runtime
- Windows 8
- PDF
- Visual Studio 2012
- Javascript with Windows RT
- Windows RT
## Topics
- C#
- Code Sample
- ReadFile
- WriteableBitmap
- Lists
- Text
- Offline HTML5 application
- Coding
- Item Templates
- Windows Store integration
- Slideshow
- BitmapImage
- ItemsSource
- HTML 5 Canvas ID
- HTML5/JavaScript
- PDF file
## Updated
- 10/07/2013
## Description

<h1>Introduction</h1>
<p><em>MUPDF is a free pdf library that help in pdf parsing and <span>renderer , today we ported the MUPDF c&#43;&#43; library in a WinRT component , that will help you to show PDF's file in your HTML/JS or XAML/c# app.</span></em></p>
<h1><span>Building the Sample</span></h1>
<p><em>Our Project Consist of 2 Parts the&nbsp;MuPDFWinRT which is the C&#43;&#43; WinRT Component it is a Win32 Component that can be compiled for both x86 and ARM Architects X64 is not supported yet.</em></p>
<p><em>And a client Project that can be XAML/C# or HTML/JS project which have a refrence for the MUPDFWinRT component.</em></p>
<p>&nbsp;</p>
<p><em>The PDF files must have a value &quot;content&quot; as package action and &quot;Copy if newer&quot; or &quot;Copy always&quot; as a value for the Copy to output Folder to be sure that the PDF file will be copied to the app local files so it can be founded by the WinRTComponent.</em></p>
<p>&nbsp;</p>
<p><em><img id="83936" src="83936-screenshot1.png" alt="" width="1363" height="726"><br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>In order to make it easier for the JS developer , to show his pdf we wrapped the PDF basic Fonction in a namespace called &quot;PDF&quot; defined in MUPDF.Js File , it consist of 2 Public Methods and one private one.</em></p>
<p>&nbsp;</p>
<p><em>1- openPDF: this nethod return a promise that will be complited after a pdf file is opened it will return an object that hold this pdf file.</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js">&nbsp;openPDF:&nbsp;<span class="js__operator">function</span>&nbsp;(filename)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;Windows.ApplicationModel.Package.current.installedLocation.getFileAsync(filename).then(<span class="js__operator">function</span>(file)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;Windows.Storage.FileIO.readBufferAsync(file).then(<span class="js__operator">function</span>(fileBuffer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;MuPDFWinRT.Document.create(fileBuffer,&nbsp;MuPDFWinRT.DocumentType.pdf,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Windows.Graphics.Display.DisplayProperties.logicalDpi);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,</pre>
</div>
</div>
</div>
<p><em>2-&nbsp;Draw: this method called the drawPage method to draw a draw the next page when the flipview is fliped , each time the user flip a page this method will call the drawPage to draw the next pages</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js">&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Draw:&nbsp;<span class="js__operator">function</span>&nbsp;Draw&nbsp;(pdfDocument)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;array&nbsp;=&nbsp;[];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;pdfDocument.pageCount;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;array.push(i);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;dataList&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;WinJS.Binding.List(array);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;flipViewDOM&nbsp;=&nbsp;document.getElementById(<span class="js__string">&quot;flipView&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;flipView&nbsp;=&nbsp;flipViewDOM.winControl;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;width&nbsp;=&nbsp;flipViewDOM.clientWidth;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;height&nbsp;=&nbsp;flipViewDOM.clientHeight;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;flipView.itemDataSource&nbsp;=&nbsp;dataList.dataSource;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;flipView.itemTemplate&nbsp;=&nbsp;(<span class="js__operator">function</span>&nbsp;(itemPromise)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;itemPromise.then(<span class="js__operator">function</span>&nbsp;(item)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;root&nbsp;element&nbsp;for&nbsp;the&nbsp;item</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;canvas&nbsp;=&nbsp;document.createElement(<span class="js__string">&quot;canvas&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;canvas.style.width&nbsp;=&nbsp;width;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;canvas.style.height&nbsp;=&nbsp;height;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;canvas.id&nbsp;=&nbsp;<span class="js__string">'canvas1'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;canvasContext&nbsp;=&nbsp;canvas.getContext(<span class="js__string">&quot;2d&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;drawPage(pdfDocument,&nbsp;canvasContext,&nbsp;item.index);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;canvas;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>3-&nbsp;drawPage: this private method interact with the WinRT component asking it to draw a specific page and return it in an array of&nbsp;ARGB Data After it is done , we will take the resulted array and convert it to&nbsp;ABGR and send it to the canvas.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__operator">function</span>&nbsp;drawPage&nbsp;(pdfDocument,&nbsp;canvasContext,&nbsp;index)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;size&nbsp;=&nbsp;pdfDocument.getPageSize(index);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;canvasContext.canvas.width&nbsp;=&nbsp;size.x;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;canvasContext.canvas.height&nbsp;=&nbsp;size.y;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;imageData&nbsp;=&nbsp;canvasContext.createImageData(size.x,&nbsp;size.y);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;current&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;Int32Array(size.x&nbsp;*&nbsp;size.y);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pdfDocument.drawPage(index,&nbsp;current,&nbsp;<span class="js__num">0</span>,&nbsp;<span class="js__num">0</span>,&nbsp;size.x,&nbsp;size.y,&nbsp;false);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;from&nbsp;&nbsp;ARGB&nbsp;to&nbsp;ABGR</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;current.length;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;val&nbsp;=&nbsp;current[i];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;cursor&nbsp;=&nbsp;i&nbsp;*&nbsp;<span class="js__num">4</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;imageData.data[cursor]&nbsp;=&nbsp;(val&nbsp;&gt;&gt;&nbsp;<span class="js__num">16</span>)&nbsp;&amp;&nbsp;0xFF;&nbsp;<span class="js__sl_comment">//r</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;imageData.data[cursor&nbsp;&#43;&nbsp;<span class="js__num">1</span>]&nbsp;=&nbsp;(val&nbsp;&gt;&gt;&nbsp;<span class="js__num">8</span>)&nbsp;&amp;&nbsp;0xFF;&nbsp;<span class="js__sl_comment">//g</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;imageData.data[cursor&nbsp;&#43;&nbsp;<span class="js__num">2</span>]&nbsp;=&nbsp;val&nbsp;&amp;&nbsp;0xFF;&nbsp;<span class="js__sl_comment">//b</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;imageData.data[cursor&nbsp;&#43;&nbsp;<span class="js__num">3</span>]&nbsp;=&nbsp;<span class="js__num">255</span>;&nbsp;<span class="js__sl_comment">//&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;canvasContext.putImageData(imageData,&nbsp;<span class="js__num">0</span>,&nbsp;<span class="js__num">0</span>);&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h1>More Information</h1>
<p><em><a href="http://www.mupdf.com/">http://www.mupdf.com/</a></em></p>
