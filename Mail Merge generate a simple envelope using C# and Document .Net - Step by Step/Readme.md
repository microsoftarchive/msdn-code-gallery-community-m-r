# Mail Merge: generate a simple envelope using C# and Document .Net - Step by Step
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
- ASP.NET
- .NET Framework
## Topics
- Controls
- Graphics
- C#
- ASP.NET
- User Interface
- How to
- Office 2010 101 code samples
## Updated
- 10/28/2016
## Description

<h1>Introduction</h1>
<p class="CommonText">Mail Merge is the feature of Document .Net which allows to easy generate documents (reports, letters, envelopes) using templates. For example, it's will be very helpful to generate hundreds of identical documents but with different data.</p>
<p class="CommonText">To illustrate how works Mail Merge function let's create C# application which generates envelopes &quot;Happy New Year&quot;' for Simpson family. As result, we'll get 5 (five) same congratulatory envelopes in the single document.</p>
<h1><span>Our Steps:</span></h1>
<ol class="CommonText">
<li>Create an envelope template (envelope-template.docx) in MS Word with Fields &quot;Name&quot; and &quot;FamilyName&quot;.
</li><li>Type C# code which executes Mail Merge between Simpson persons and &quot;envelope-template.docx&quot;.
</li><li>See result: &quot;simpson-family.docx: which contains 5 (five) named envelopes. </li></ol>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<h2 class="H2Text">1. Create an envelope template in MS Word:&nbsp;<span class="CommonText">envelope-template.docx</span></h2>
<p class="CommonText">To start, let's open MS Word and create new empty document with name &quot;envelope-template.docx&quot;. Next, you may add some images and text, anything what you want to see to gongratulate the Simpson's family. Our main goal is add two&nbsp;<strong>Fields</strong>&nbsp;&quot;Name&quot;
 and &quot;FamilyName&quot;.</p>
<ol class="CommonText">
<li>Select &quot;INSERT&quot; tab. </li><li>Click by the pictogram &quot;Quick Parts&quot;. </li><li>Select and click by &quot;Field..&quot; menu item. </li><li>In the opened window, follow to the &quot;Field names:&quot; and select &quot;MergeField&quot;. </li><li>In the &quot;Field name:&quot; type &quot;Name&quot;. </li><li>Click &quot;OK&quot; </li></ol>
<p><span>By the same steps, please add another Field with name &quot;FamilyName&quot;.</span></p>
<p><img id="162799" src="162799-envelope-create-template.jpg" alt="" width="800" height="429"></p>
<p><span class="CommonText"><br>
</span></p>
<p><em>&nbsp;</em></p>
<h2 class="H2Text">2. C# code which executes Mail Merge between Simpson persons and &quot;envelope-template.docx&quot;</h2>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">DocumentCore dc = DocumentCore.Load(@&quot;envelope-template.docx&quot;);

var dataSource = new[] { new { Name = &quot;Homer&quot;, FamilyName = &quot;Simpson&quot; }, 
                    new { Name = &quot;Marge &quot;, FamilyName = &quot;Simpson&quot; },
                    new { Name = &quot;Bart&quot;, FamilyName = &quot;Simpson&quot; },
                    new { Name = &quot;Lisa&quot;, FamilyName = &quot;Simpson&quot; },
                    new { Name = &quot;Maggie&quot;, FamilyName = &quot;Simpson&quot; }};

dc.MailMerge.Execute(dataSource);
dc.Save(@&quot;Simpson-family.docx&quot;);</pre>
<div class="preview">
<pre class="csharp">DocumentCore&nbsp;dc&nbsp;=&nbsp;DocumentCore.Load(@<span class="cs__string">&quot;envelope-template.docx&quot;</span>);&nbsp;
&nbsp;
var&nbsp;dataSource&nbsp;=&nbsp;<span class="cs__keyword">new</span>[]&nbsp;{&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;Homer&quot;</span>,&nbsp;FamilyName&nbsp;=&nbsp;<span class="cs__string">&quot;Simpson&quot;</span>&nbsp;},&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;Marge&nbsp;&quot;</span>,&nbsp;FamilyName&nbsp;=&nbsp;<span class="cs__string">&quot;Simpson&quot;</span>&nbsp;},&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;Bart&quot;</span>,&nbsp;FamilyName&nbsp;=&nbsp;<span class="cs__string">&quot;Simpson&quot;</span>&nbsp;},&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;Lisa&quot;</span>,&nbsp;FamilyName&nbsp;=&nbsp;<span class="cs__string">&quot;Simpson&quot;</span>&nbsp;},&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;Maggie&quot;</span>,&nbsp;FamilyName&nbsp;=&nbsp;<span class="cs__string">&quot;Simpson&quot;</span>&nbsp;}};&nbsp;
&nbsp;
dc.MailMerge.Execute(dataSource);&nbsp;
dc.Save(@<span class="cs__string">&quot;Simpson-family.docx&quot;</span>);</pre>
</div>
</div>
</div>
<p><span>Here we create an array of objects by anonymous type. After execution of mail merge method we'll get the same number of copies document with filled fields as number of objects in the array.</span></p>
<h2 class="H2Text">3. See result: &quot;simpson-family.docx&quot;</h2>
<p><span><img id="162800" src="162800-envelope-result.png" alt=""><br>
</span></p>
<h1>Source Code Files</h1>
<div><em>Related Links:</em></div>
<div><em><br>
Website:&nbsp;<a href="http://www.sautinsoft.com/">www.sautinsoft.com</a><br>
Product Home:&nbsp;<a href="http://sautinsoft.com/products/document/index.php">Document.Net</a><br>
Download:&nbsp;<em><a href="http://sautinsoft.com/thankyou.php?download=document_net.zip">Document.Net</a></em><br>
</em></div>
<h2 class="H2Text">Requrements and Technical Information</h2>
<p class="CommonText"><span>&nbsp;Requires only .Net 4.0 or above. Our product is compatible with all .Net languages and supports all Operating Systems where .Net Framework can be used.</span><br>
<br>
<span>Note that &laquo;Document .Net&raquo; is entirely written in managed C#, which makes it absolutely standalone and an independent library. Of course, No dependency on Microsoft Word.</span></p>
