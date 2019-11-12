# Optical character recognition form screen copy
## Requires
- Visual Studio 2010
## License
- MS-LPL
## Technologies
- Windows Forms
## Topics
- Controls
- Data Binding
- User Interface
- .NET 2
- Visual Studio 2010 MSBuild
## Updated
- 01/31/2013
## Description

<p>Optical character recognition form screen copy&nbsp;</p>
<h1>Introduction</h1>
<p><em>sample solve for ocr in&nbsp;tessnet2_32.dll using</em></p>
<h1><span>Building the Sample</span></h1>
<p>using&nbsp;<em>Optical character recognition the tessnet .dll &nbsp;,tesact data&nbsp;</em></p>
<p><span style="font-size:20px; font-weight:bold">Description </span></p>
<div class="endscriptcode">&nbsp;
<p><strong>Optical character recognition</strong>, usually abbreviated to&nbsp;<strong>OCR</strong>, is the&nbsp;<a title="Machine" href="http://en.wikipedia.org/wiki/Machine">mechanical</a>&nbsp;or&nbsp;<a title="Electronics" href="http://en.wikipedia.org/wiki/Electronics">electronic</a>&nbsp;conversion
 of scanned&nbsp;<a title="Image" href="http://en.wikipedia.org/wiki/Image">images</a>&nbsp;of handwritten, typewritten or printed text into machine-encoded text. It is widely used as a form of data entry from some sort of original paper data source, whether
 documents, sales receipts, mail, or any number of printed records. It is a common method of digitizing printed texts so that they can be electronically searched, stored more compactly, displayed on-line, and used in machine processes such as<a title="Machine translation" href="http://en.wikipedia.org/wiki/Machine_translation">machine
 translation</a>,&nbsp;<a class="mw-redirect" title="Text-to-speech" href="http://en.wikipedia.org/wiki/Text-to-speech">text-to-speech</a>&nbsp;and&nbsp;<a title="Text mining" href="http://en.wikipedia.org/wiki/Text_mining">text mining</a>. OCR is a field
 of research in&nbsp;<a title="Pattern recognition" href="http://en.wikipedia.org/wiki/Pattern_recognition">pattern recognition</a>,&nbsp;<a title="Artificial intelligence" href="http://en.wikipedia.org/wiki/Artificial_intelligence">artificial intelligence</a>&nbsp;and&nbsp;<a title="Computer vision" href="http://en.wikipedia.org/wiki/Computer_vision">computer
 vision</a>.</p>
<p>Early versions needed to be programmed with images of each character, and worked on one font at a time. &quot;Intelligent&quot; systems with a high degree of recognition accuracy for most fonts are now common. Some systems are capable of reproducing formatted output
 that closely approximates the original scanned page including images, columns and other non-textual components.</p>
</div>
<p><em>How does this sample solve the problem?</em></p>
<p>tessnet data suppted</p>
<h1><span>Source Code Files</span></h1>
<h1><span>&nbsp;</span></h1>
<ul>
<li>OCRImage = &quot;&quot;&nbsp; &nbsp; &nbsp; &nbsp; Dim oOCR As New tessnet2.Tesseract&nbsp; &nbsp; &nbsp; &nbsp; oOCR.Init(path, language, False)&nbsp; &nbsp; &nbsp; &nbsp; Dim WordList As New List(Of tessnet2.Word)&nbsp; &nbsp; &nbsp; &nbsp; WordList = oOCR.DoOCR(bm,
 Rectangle.Empty)&nbsp; &nbsp; &nbsp; &nbsp; Dim LineCount As Integer = tessnet2.Tesseract.LineCount(WordList)&nbsp; &nbsp; &nbsp; &nbsp; For i As Integer = 0 To LineCount - 1&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; OCRImage &amp;= tessnet2.Tesseract.GetLineText(WordList,
 i) &amp; vbCrLf&nbsp; &nbsp; &nbsp; &nbsp; Next&nbsp; &nbsp; &nbsp; &nbsp; oOCR.Dispose()
</li></ul>
