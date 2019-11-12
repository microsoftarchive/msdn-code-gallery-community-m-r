# Print PDF file in C#
## Requires
- Visual Studio 2010
## License
- MS-LPL
## Technologies
- Controls
- C#
- .NET Framework
- .NET Framwork
- Office Development
## Topics
- Controls
- C#
- Web Services
- Visual Studio
- HTML
- C# Language Features
- .Net Programming
## Updated
- 09/23/2014
## Description

<p>This code example focus on introducing how to print PDF files via a .NET PDF API. With the help of
<a href="http://www.e-iceblue.com/Introduce/pdf-for-net-introduce.html">Spire.PDF for .NET</a>, developers can finish the print function in a few lines codes to print the PDF files with the default printer or any other network connected printer. You can also
 print all the PDF pages or only print the selected pages you want.</p>
<p>Many rich features can be supported by the .NET PDF API, such as security setting (including digital signature), PDF text/attachment/image extract, PDF merge/split, metadata update, section and paragraph optimizing, graph/image drawing and inserting, table
 creation and processing, and importing data etc. Besides, Spire.PDF for .NET can be applied to easily converting Text, Image and HTML to PDF with C#/VB.NET&nbsp;in high quality.</p>
<p>Here are the sample codes.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;PrintPDF&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PdfDocument&nbsp;doc&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;PdfDocument();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.LoadFromFile(<span class="cs__string">&quot;sample.pdf&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Use&nbsp;the&nbsp;default&nbsp;printer&nbsp;to&nbsp;print&nbsp;all&nbsp;the&nbsp;pages</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//doc.PrintDocument.Print();</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;the&nbsp;printer&nbsp;and&nbsp;select&nbsp;the&nbsp;pages&nbsp;you&nbsp;want&nbsp;to&nbsp;print</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PrintDialog&nbsp;dialogPrint&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;PrintDialog();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.AllowPrintToFile&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.AllowSomePages&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.PrinterSettings.MinimumPage&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.PrinterSettings.MaximumPage&nbsp;=&nbsp;doc.Pages.Count;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.PrinterSettings.FromPage&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.PrinterSettings.ToPage&nbsp;=&nbsp;doc.Pages.Count;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(dialogPrint.ShowDialog()&nbsp;==&nbsp;DialogResult.OK)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;the&nbsp;pagenumber&nbsp;which&nbsp;you&nbsp;choose&nbsp;as&nbsp;the&nbsp;start&nbsp;page&nbsp;to&nbsp;print</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.PrintFromPage&nbsp;=&nbsp;dialogPrint.PrinterSettings.FromPage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;the&nbsp;pagenumber&nbsp;which&nbsp;you&nbsp;choose&nbsp;as&nbsp;the&nbsp;final&nbsp;page&nbsp;to&nbsp;print</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.PrintToPage&nbsp;=&nbsp;dialogPrint.PrinterSettings.ToPage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;the&nbsp;name&nbsp;of&nbsp;the&nbsp;printer&nbsp;which&nbsp;is&nbsp;to&nbsp;print&nbsp;the&nbsp;PDF</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.PrinterName&nbsp;=&nbsp;dialogPrint.PrinterSettings.PrinterName;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PrintDocument&nbsp;printDoc&nbsp;=&nbsp;doc.PrintDocument;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogPrint.Document&nbsp;=&nbsp;printDoc;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;printDoc.Print();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<p>There a lot of PDF tasks can be operated by using this .NET PDF library. Programmers can merge PDF files into one PDF as well as split a huge PDF with the given number range accordingly. Furthermore, it can protect your PDF document by encryption with both
 owner password and user password, create PDF digital signature. If you do not need to lock your PDF, you can decrypt it quickly. Besides, you can set PDF property, PDF template and view preference as you like. Finally, webpage, image and text all can be converted
 to PDF which are already referred above.</p>
<p><strong>Useful Links:</strong></p>
<p><strong>Website</strong>:&nbsp;<a href="http://www.e-icbelue.com/">http://www.e-icbelue.com</a></p>
<p><strong>Product Home</strong>:&nbsp;<a href="http://www.e-iceblue.com/Introduce/pdf-for-net-introduce.html">Spire.PDF for .NET introduction</a></p>
<p><strong>Free Trial</strong>:&nbsp;<a href="http://www.e-iceblue.com/Download/download-pdf-for-net-now.html">Free evalution on Spire.PDF</a></p>
<p><strong>Forum</strong>:&nbsp;<a href="http://www.e-iceblue.com/forum/viewforum.php?f=4">Spire.PDF Forums</a></p>
