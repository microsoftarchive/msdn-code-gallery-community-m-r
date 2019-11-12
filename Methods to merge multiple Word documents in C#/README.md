# Methods to merge multiple Word documents in C#
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- C#
- ASP.NET
- Class Library
- Windows Forms
- .NET Framework 4.0
- Visual Studio 2013
- Word API
- Word .NET library
## Topics
- Controls
- C#
- ASP.NET
- Windows Forms
- Code Sample
- How to
- Language Samples
- merge  Word files
## Updated
- 02/27/2018
## Description

<h1><strong style="font-size:10px">Introduction</strong></h1>
<p>This sample is aimed to introduce two methods of merging multiple Word documents with C# code. The full code of each method will give you the clear information of how to operate.</p>
<p><strong>The tools we need</strong>: Spire.Doc for .NET</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;Visual Studio 2013</p>
<p><strong>Notice: </strong>You should<strong> </strong>download and install Spire.Doc for .NET before coding, then add the dll file into the project as a reference.</p>
<p>&nbsp;</p>
<p><strong>Sample 1: </strong>Merge documents by inserting content from one document into the new page of another.</p>
<p><img id="182211" src="182211-11.png" alt="" width="424" height="571"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">编辑脚本</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Spire.Doc;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;MergeWord_Doc&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Create&nbsp;two&nbsp;instances&nbsp;of&nbsp;Document&nbsp;class,&nbsp;and&nbsp;load&nbsp;the&nbsp;documents&nbsp;that&nbsp;you&nbsp;need&nbsp;to&nbsp;merge</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;doc1&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\TradeNegotiation.docx&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;doc2&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\DisputeSettlement.docx&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Call&nbsp;InsertTextFromFile()&nbsp;method&nbsp;to&nbsp;merge&nbsp;the&nbsp;doc2&nbsp;into&nbsp;the&nbsp;doc1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;fileName&nbsp;=&nbsp;@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\DisputeSettlement.docx&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc1.InsertTextFromFile(fileName,&nbsp;FileFormat.Docx2013);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Save&nbsp;the&nbsp;merged&nbsp;document&nbsp;into&nbsp;a&nbsp;new&nbsp;one</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc1.SaveToFile(<span class="cs__string">&quot;MergedDocument.docx&quot;</span>,&nbsp;FileFormat.Docx2013);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;</pre>
</div>
</div>
</div>
<p><a name="OLE_LINK7"></a><span style="font-size:small"><a name="OLE_LINK6"><strong><span lang="EN-US">Sample 2:
</span></strong><span lang="EN-US">Merge documents by inserting content from one document into the end of another.</span></a></span></p>
<h1><img id="182212" src="182212-12.png" alt="" width="642" height="534"></h1>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">编辑脚本</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Spire.Doc;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Spire.Doc.Documents;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;MergeWord2_Doc&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span><span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Create&nbsp;two&nbsp;instances&nbsp;of&nbsp;Document&nbsp;class</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;doc1&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\TradeNegotiation.docx&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;doc2&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\DisputeSettlement.docx&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Get&nbsp;the&nbsp;last&nbsp;section&nbsp;of&nbsp;doc1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Section&nbsp;lastSection&nbsp;=&nbsp;doc1.LastSection;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Traverse&nbsp;the&nbsp;section&nbsp;and&nbsp;paragraph&nbsp;of&nbsp;doc2,&nbsp;and&nbsp;clone&nbsp;the&nbsp;paragraphs&nbsp;to&nbsp;the&nbsp;last&nbsp;section&nbsp;of&nbsp;doc1</span><span class="cs__keyword">foreach</span>&nbsp;(Section&nbsp;section&nbsp;<span class="cs__keyword">in</span>&nbsp;doc2.Sections)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(Paragraph&nbsp;paragraph&nbsp;<span class="cs__keyword">in</span>&nbsp;section.Paragraphs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lastSection.Paragraphs.Add(paragraph.Clone()&nbsp;<span class="cs__keyword">as</span>&nbsp;Paragraph);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Save&nbsp;the&nbsp;merged&nbsp;document&nbsp;into&nbsp;a&nbsp;new&nbsp;one</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc1.SaveToFile(<span class="cs__string">&quot;Merged.docx&quot;</span>,&nbsp;FileFormat.Docx2013);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><strong>More about Spire.Doc for .NET</strong></p>
<p><strong>Main functions:</strong></p>
<p><strong>Converting</strong></p>
<p>&nbsp;</p>
<ul>
<li>save Word Doc/Docx to stream </li><li>save as web response and convert Word Doc/Docx to XML, RTF, EMF, TXT, XPS, EPUB, HTML, SVG and vice versa
</li><li>convert Word Doc/Docx to PDF and HTML to image. </li></ul>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><strong>Creating, Editing, Operation And Printing</strong></p>
<ul>
<li>Create Word document dynamically from scratch </li><li>Support types of Word document elements, including page, sections, headers, footers, footnotes, paragraphs, lists, tables, text, fields, hyperlinks, bookmarks, comments, images, styles, background settings, printing features, document settings and protection
</li></ul>
<p>&nbsp;</p>
<p><strong>Process pre-existing Word documents</strong></p>
<p>&nbsp;</p>
<ul>
<li>Search and replace function </li><li>Alignment, page break, fill field </li><li>Document concatenate, document copy/print </li><li>Complex and deeply nested mail merge </li></ul>
<p>&nbsp;</p>
<p><strong>&nbsp;</strong></p>
<p><strong>Related links:</strong></p>
<p><strong>Website: </strong><a href="https://www.e-iceblue.com/">https://www.e-iceblue.com/</a></p>
<p><strong>Product introduction:</strong> <a href="https://www.e-iceblue.com/Introduce/free-doc-component.html">
https://www.e-iceblue.com/Introduce/free-doc-component.html</a></p>
<p><strong>Download: </strong><a href="https://www.e-iceblue.com/Download/download-word-for-net-free.html">https://www.e-iceblue.com/Download/download-word-for-net-free.html</a></p>
