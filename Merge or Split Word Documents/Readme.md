# Merge or Split Word Documents
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- C#
- Visual Studio 2013
## Topics
- Merge or Split Word Documents
## Updated
- 04/26/2017
## Description

<h1><span style="font-size:medium">&nbsp;</span></h1>
<h1><span style="font-size:medium">Introduction</span></h1>
<div><span style="font-size:medium"><span style="font-size:small">&nbsp;</span></span></div>
<div><span style="font-size:medium"><span style="font-size:small">When dealing with Word
</span><span style="font-size:small">documents, sometimes developers need to merge multiple Word documents into a
</span><span style="font-size:small">single Word document or </span><span style="font-size:small">split one Word document into multiple Word documents. This sample aims at introducing a solution to implement the function.</span></span></div>
<div><span style="font-size:medium"><strong>&nbsp;</strong></span></div>
<div><span style="font-size:medium"><strong>Tools we need</strong></span></div>
<div><span style="font-size:small">&nbsp;</span></div>
<div><span style="font-size:small">-Free Spire.Doc for .NET</span></div>
<div><span style="font-size:small">-Visual&nbsp; Studio</span></div>
<div></div>
<div><span style="font-size:medium"><span style="font-size:small">&nbsp;</span></span></div>
<div><span style="font-size:medium"><span style="font-size:small">Free Spire.Doc for .NET is a professional word
</span><span style="font-size:small">component for commercial and personal use. With this component, developers can quickly
</span><span style="font-size:small">and easily &nbsp;generate, read, write, save,</span><span style="font-size:small">print and convert Word documents in .NET application.</span></span></div>
<div><span style="font-size:medium"><strong>&nbsp;</strong></span></div>
<div><span style="font-size:medium"><strong>Using the code</strong></span></div>
<div><span style="font-size:medium">&nbsp;</span></div>
<div><span style="font-size:medium">Merge</span></div>
<div><span>
<h1 class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">编辑脚本</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace merge_word_documents
{
    class Program
    {
        static void Main(string[] args)
        {   //Initialize a new word document object and load the original word file &quot;A good man.docx&quot;.
            Document document1 = new Document();
            document1.LoadFromFile(@&quot;C:\Users\Administrator\Desktop\A good man.docx&quot;, FileFormat.Docx);
            //Merge another word file &quot;The Kite Runner.docx&quot; to the original one.
            document1.InsertTextFromFile(@&quot;C:\Users\Administrator\Desktop\The Kite Runner.docx&quot;, FileFormat.Docx);
            //Save and launch the file.
            document1.SaveToFile(&quot;MergedFile.docx&quot;, FileFormat.Docx);
            System.Diagnostics.Process.Start(&quot;MergedFile.docx&quot;);

        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Spire.Doc;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;merge_word_documents&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span><span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;<span class="cs__com">//Initialize&nbsp;a&nbsp;new&nbsp;word&nbsp;document&nbsp;object&nbsp;and&nbsp;load&nbsp;the&nbsp;original&nbsp;word&nbsp;file&nbsp;&quot;A&nbsp;good&nbsp;man.docx&quot;.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;document1&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document1.LoadFromFile(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\A&nbsp;good&nbsp;man.docx&quot;</span>,&nbsp;FileFormat.Docx);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Merge&nbsp;another&nbsp;word&nbsp;file&nbsp;&quot;The&nbsp;Kite&nbsp;Runner.docx&quot;&nbsp;to&nbsp;the&nbsp;original&nbsp;one.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document1.InsertTextFromFile(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\The&nbsp;Kite&nbsp;Runner.docx&quot;</span>,&nbsp;FileFormat.Docx);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Save&nbsp;and&nbsp;launch&nbsp;the&nbsp;file.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document1.SaveToFile(<span class="cs__string">&quot;MergedFile.docx&quot;</span>,&nbsp;FileFormat.Docx);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Diagnostics.Process.Start(<span class="cs__string">&quot;MergedFile.docx&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</h1>
</span><span style="font-size:medium"><strong>The&nbsp; effective screenshot:</strong></span></div>
<h1><span style="font-size:medium"><img id="172594" src="172594-%e5%90%88%e5%b9%b6%e6%95%88%e6%9e%9c%e5%9b%be.png" alt="" width="625" height="465"></span></h1>
<div><br>
<span style="font-size:medium">Split by section break</span></div>
<h1><span>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">编辑脚本</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Split_Word_Document
{
    class Program
    {
        static void Main(string[] args)
        {   //Initialize a new word document object and load the original word document which has two sections.
            Document document2 = new Document();
            document2.LoadFromFile(@&quot;C:\Users\Administrator\Desktop\Text.docx&quot;,FileFormat.Docx);
            //Define another new word document object.
            Document newWord;
            //Traverse through all sections of the original word document, clone each section and add it to a new word document as new section, then save the document to specific path.
            for (int i = 0; i &lt; document2.Sections.Count; i&#43;&#43;)
            {
                newWord = new Document();
                newWord.Sections.Add(document2.Sections[i].Clone());
                newWord.SaveToFile(String.Format(@&quot;test\out_{0}.docx&quot;, i));

            }
        }
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Spire.Doc;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Spire.Doc.Documents;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Drawing;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;Split_Word_Document&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span><span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;<span class="cs__com">//Initialize&nbsp;a&nbsp;new&nbsp;word&nbsp;document&nbsp;object&nbsp;and&nbsp;load&nbsp;the&nbsp;original&nbsp;word&nbsp;document&nbsp;which&nbsp;has&nbsp;two&nbsp;sections.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;document2&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document2.LoadFromFile(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\Text.docx&quot;</span>,FileFormat.Docx);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Define&nbsp;another&nbsp;new&nbsp;word&nbsp;document&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;newWord;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Traverse&nbsp;through&nbsp;all&nbsp;sections&nbsp;of&nbsp;the&nbsp;original&nbsp;word&nbsp;document,&nbsp;clone&nbsp;each&nbsp;section&nbsp;and&nbsp;add&nbsp;it&nbsp;to&nbsp;a&nbsp;new&nbsp;word&nbsp;document&nbsp;as&nbsp;new&nbsp;section,&nbsp;then&nbsp;save&nbsp;the&nbsp;document&nbsp;to&nbsp;specific&nbsp;path.</span><span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;document2.Sections.Count;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newWord&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newWord.Sections.Add(document2.Sections[i].Clone());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newWord.SaveToFile(String.Format(@<span class="cs__string">&quot;test\out_{0}.docx&quot;</span>,&nbsp;i));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
</span><span style="font-size:medium"><strong>The&nbsp; effective screenshot:</strong></span></h1>
<h1><span><img id="172598" src="172598-%e5%88%86%e9%a1%b5%e6%95%88%e6%9e%9c%e5%9b%be.png" alt="" width="659" height="430"></span></h1>
<div><br>
<span style="font-size:medium">Split by page break</span></div>
<h1><span>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">编辑脚本</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace another
{
    class Program
    {
        static void Main(string[] args)
        {   //Create a word document and load the original word document.
            Document original = new Document();
            original.LoadFromFile(@&quot;C:\Users\Administrator\Desktop\daffodil.docx&quot;,FileFormat.Docx);
            //Create a new word document and add a section to it.
            Document document3 = new Document();
            Section section = document3.AddSection();
            //Split the original word document into separate documents according to page break.
            int index = 0;
            foreach (Section sec in original.Sections)
            {
                foreach (DocumentObject obj in sec.Body.ChildObjects)
                {
                    if (obj is Paragraph)
                    {
                        Paragraph para = obj as Paragraph;
                        section.Body.ChildObjects.Add(para.Clone());

                        foreach (DocumentObject parobj in para.ChildObjects)
                        {
                            if (parobj is Break &amp;&amp; (parobj as Break).BreakType == BreakType.PageBreak)
                            {
                                int i = para.ChildObjects.IndexOf(parobj);
                                section.Body.LastParagraph.ChildObjects.RemoveAt(i);
                                document3.SaveToFile(String.Format(&quot;result/out-{0}.docx&quot;, index), FileFormat.Docx);
                                index&#43;&#43;;

                                document3 = new Document();
                                section = document3.AddSection();
                                section.Body.ChildObjects.Add(para.Clone());
                                if (section.Paragraphs[0].ChildObjects.Count == 0)
                                {
                                    section.Body.ChildObjects.RemoveAt(0);
                                }
                                else
                                {
                                    while (i &gt;= 0)
                                    {
                                        section.Paragraphs[0].ChildObjects.RemoveAt(i);
                                        i--;
                                    }
                                }
                            }
                        }
                    }
                    if (obj is Table)
                    {
                        section.Body.ChildObjects.Add(obj.Clone());
                    }
                }
            }
            //save the file
            document3.SaveToFile(String.Format(@&quot;result/out-{0}.docx&quot;, index), FileFormat.Docx);

        }
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Spire.Doc;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Spire.Doc.Documents;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;another&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span><span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;<span class="cs__com">//Create&nbsp;a&nbsp;word&nbsp;document&nbsp;and&nbsp;load&nbsp;the&nbsp;original&nbsp;word&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;original&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;original.LoadFromFile(@<span class="cs__string">&quot;C:\Users\Administrator\Desktop\daffodil.docx&quot;</span>,FileFormat.Docx);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Create&nbsp;a&nbsp;new&nbsp;word&nbsp;document&nbsp;and&nbsp;add&nbsp;a&nbsp;section&nbsp;to&nbsp;it.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;document3&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Section&nbsp;section&nbsp;=&nbsp;document3.AddSection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Split&nbsp;the&nbsp;original&nbsp;word&nbsp;document&nbsp;into&nbsp;separate&nbsp;documents&nbsp;according&nbsp;to&nbsp;page&nbsp;break.</span><span class="cs__keyword">int</span>&nbsp;index&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(Section&nbsp;sec&nbsp;<span class="cs__keyword">in</span>&nbsp;original.Sections)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(DocumentObject&nbsp;obj&nbsp;<span class="cs__keyword">in</span>&nbsp;sec.Body.ChildObjects)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(obj&nbsp;<span class="cs__keyword">is</span>&nbsp;Paragraph)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paragraph&nbsp;para&nbsp;=&nbsp;obj&nbsp;<span class="cs__keyword">as</span>&nbsp;Paragraph;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section.Body.ChildObjects.Add(para.Clone());&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(DocumentObject&nbsp;parobj&nbsp;<span class="cs__keyword">in</span>&nbsp;para.ChildObjects)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(parobj&nbsp;<span class="cs__keyword">is</span>&nbsp;Break&nbsp;&amp;&amp;&nbsp;(parobj&nbsp;<span class="cs__keyword">as</span>&nbsp;Break).BreakType&nbsp;==&nbsp;BreakType.PageBreak)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;para.ChildObjects.IndexOf(parobj);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section.Body.LastParagraph.ChildObjects.RemoveAt(i);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document3.SaveToFile(String.Format(<span class="cs__string">&quot;result/out-{0}.docx&quot;</span>,&nbsp;index),&nbsp;FileFormat.Docx);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;index&#43;&#43;;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document3&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Document();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section&nbsp;=&nbsp;document3.AddSection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section.Body.ChildObjects.Add(para.Clone());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(section.Paragraphs[<span class="cs__number">0</span>].ChildObjects.Count&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section.Body.ChildObjects.RemoveAt(<span class="cs__number">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(i&nbsp;&gt;=&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section.Paragraphs[<span class="cs__number">0</span>].ChildObjects.RemoveAt(i);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i--;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(obj&nbsp;<span class="cs__keyword">is</span>&nbsp;Table)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;section.Body.ChildObjects.Add(obj.Clone());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//save&nbsp;the&nbsp;file</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document3.SaveToFile(String.Format(@<span class="cs__string">&quot;result/out-{0}.docx&quot;</span>,&nbsp;index),&nbsp;FileFormat.Docx);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
</span><span style="font-size:medium"><strong>The&nbsp; effective screenshot:</strong></span></h1>
<h1><span><img id="172599" src="172599-%e5%88%86%e9%a1%b52%e6%95%88%e6%9e%9c%e5%9b%be.png" alt="" width="661" height="432"></span></h1>
<h1><span style="font-size:medium">&nbsp;</span></h1>
<h1><span style="font-size:medium">More Information</span></h1>
<div><span style="font-size:small">&nbsp;</span></div>
<div><span style="font-size:small">Free Spire.Doc for .NET&nbsp;supports converting Word documents (Word 97-2003, Word 2007, Word
</span><span style="font-size:small">2010, Word2013) to commonly used file format, such as XML, RTF, TXT, PDF, XPS, EPUB, HTML and Image etc. Free Spire.Doc for .NET is many times faster than Microsoft Word Automation and with much better stability and scalability.</span></div>
<div><br>
<span style="font-size:medium"><strong>Main Features:</strong></span></div>
<ul>
<li><span style="font-size:small">No Microsoft Automation</span> </li><li><span style="font-size:small">High quality conversion</span> </li><li><span style="font-size:small">Speed and Easy-to-use</span> </li><li><span style="font-size:small">Inserting, Editing and Removing Objects</span> </li><li><span style="font-size:small">Mail Merge</span> </li><li><span style="font-size:small">Totally Free</span> </li></ul>
<div><span style="font-size:medium"><strong>Related Links:</strong></span></div>
<div><span style="font-size:small">&nbsp;</span></div>
<div><span style="font-size:small">Website: <a title="www.e-iceblue.com" href="http://www.e-iceblue.com/">
www.e-iceblue.com</a>&nbsp; </span></div>
<div><span style="font-size:small">&nbsp;</span></div>
<div><span style="font-size:small">Product: <a href="https://www.e-iceblue.com/Introduce/free-doc-component.html">
https://www.e-iceblue.com/Introduce/free-doc-component.html</a></span></div>
<div><span style="font-size:small">&nbsp;</span></div>
<div><span style="font-size:small">Download: <a href="https://www.e-iceblue.com/Download/download-word-for-net-free.html">
Free Spire.Doc for .NET</a></span><strong>&nbsp;</strong></div>
<div><em>&nbsp;</em></div>
<div><em>&nbsp;</em></div>
