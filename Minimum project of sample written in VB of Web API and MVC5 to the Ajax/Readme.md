# Minimum project of sample written in VB of Web API and MVC5 to the Ajax
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- AJAX
- jQuery
- Visual Basic .NET
- MVC5
- ASP.NET Web API 2
## Topics
- A minimum sample
## Updated
- 07/07/2016
## Description

<h1>Introduction</h1>
<p>Because the sample of MVC5 is a few written in VB, I made a minimum of project you can experience the ajax.</p>
<h1><span>Building the Sample</span></h1>
<p><span>This sample was created by VS2015Update3 pro.<br>
</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>For a beginner like me, it is important to create a minimum of code to decent ajax at first.</p>
<h1><span style="font-size:10px">A Visual Basic code is a web API &quot;api/foods&quot; called from MVC5 web application pages with Ajax.</span></h1>
<p style="padding-left:30px">Instead of using the database, I have prepared an array of &quot;foods&quot; with 5 stringｓ of food names.<br>
A http GET operation &quot;/api/foods/&quot; will return the array of &quot;foods&quot;.&nbsp;</p>
<p style="padding-left:30px">Another http Get operation &rdquo;/api/foods/{id}&rdquo; is&nbsp;available. It return the food name of the&nbsp;specifid &quot;id&quot; number. However, it is not used&nbsp;at this sample.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class FoodsController
        Inherits ApiController

        Private foods As String() = {&quot;bread&quot;, &quot;rice&quot;, &quot;noodles&quot;, &quot;sushi&quot;, &quot;spaghetti&quot;, &quot;pizza&quot;}

        ' GET: api/Foods
        Public Function GetValues() As IEnumerable(Of String)
            Return foods
        End Function

        ' GET: api/Foods/5
        Public Function GetValue(ByVal id As Integer) As String
            If id &gt; 0 And id &lt;= foods.Length Then
                Return foods(id - 1)
            Else
                Return &quot;I don't know.&quot;
            End If
        End Function

    End Class
End Namespace</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Imports</span>&nbsp;System.Net&nbsp;
<span class="visualBasic__keyword">Imports</span>&nbsp;System.Web.Http&nbsp;
&nbsp;
<span class="visualBasic__keyword">Namespace</span>&nbsp;Controllers&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Class</span>&nbsp;FoodsController&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Inherits</span>&nbsp;ApiController&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;foods&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>()&nbsp;=&nbsp;{<span class="visualBasic__string">&quot;bread&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;rice&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;noodles&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;sushi&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;spaghetti&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;pizza&quot;</span>}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;GET:&nbsp;api/Foods</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;GetValues()&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;IEnumerable(<span class="visualBasic__keyword">Of</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;foods&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;GET:&nbsp;api/Foods/5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;GetValue(<span class="visualBasic__keyword">ByVal</span>&nbsp;id&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;id&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;id&nbsp;&lt;=&nbsp;foods.Length&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;foods(id&nbsp;-&nbsp;<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;<span class="visualBasic__string">&quot;I&nbsp;don't&nbsp;know.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Class</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Namespace</span></pre>
</div>
</div>
</div>
<h1><span style="font-size:10px">A HTML code is an index page for Web API called by Ajax.&nbsp;</span></h1>
<p style="padding-left:30px">This web application of &quot;/FoodList/Index/&quot; has a button of &quot;#display&quot;.<br>
On the click event of the button(&quot;#display&quot;), an Ajax operation will start.<br>
The web API&nbsp;&quot;/api/foods/&quot; is called, and It returns the strings of &quot;foods&quot;.<br>
JQuery will get the strings, and make a food list code of &quot;html&quot; string.<br>
JQuery will display the list at the location of &quot;#list&quot;.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">@Code
    ViewData(&quot;Title&quot;) = &quot;Index&quot;
End Code

&lt;h2&gt;Please Push Button&lt;/h2&gt;

&lt;button id=&quot;display&quot; class=&quot;btn btn-success&quot;&gt;Display List&lt;/button&gt;

&lt;h2&gt;Foods List&lt;/h2&gt;
&lt;div id=&quot;list&quot;&gt;&lt;/div&gt;

&lt;h2&gt;End of List&lt;/h2&gt;

@Section scripts
&lt;script&gt;
    $(&quot;#display&quot;).click(function () {
        var url = &quot;/api/foods/&quot;;
        $.getJSON(url)
            .done(function (data) {
                var html = &quot;&lt;ul&gt;&quot;;
                $.each(data, function (index, value) {
                    html &#43;= &quot;&lt;li&gt;&quot; &#43; value &#43; &quot;&lt;/li&gt;&quot;;
                })
                html &#43;= &quot;&lt;/ul&gt;&quot;;
                $(&quot;#list&quot;).html(html);
            })
            .fail(function(data){
                $(&quot;#list&quot;).text(&quot;Error on Ajax!!!&quot;);
            });
    });

&lt;/script&gt;
End section
</pre>
<div class="preview">
<pre class="html">@Code&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ViewData(&quot;Title&quot;)&nbsp;=&nbsp;&quot;Index&quot;&nbsp;
End&nbsp;Code&nbsp;
&nbsp;
<span class="html__tag_start">&lt;h2</span><span class="html__tag_start">&gt;</span>Please&nbsp;Push&nbsp;Button<span class="html__tag_end">&lt;/h2&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;button</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;display&quot;</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;btn&nbsp;btn-success&quot;</span><span class="html__tag_start">&gt;</span>Display&nbsp;List<span class="html__tag_end">&lt;/button&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;h2</span><span class="html__tag_start">&gt;</span>Foods&nbsp;List<span class="html__tag_end">&lt;/h2&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;list&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;h2</span><span class="html__tag_start">&gt;</span>End&nbsp;of&nbsp;List<span class="html__tag_end">&lt;/h2&gt;</span>&nbsp;
&nbsp;
@Section&nbsp;scripts&nbsp;
<span class="html__tag_start">&lt;script</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;$(&quot;#display&quot;).click(function&nbsp;()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;url&nbsp;=&nbsp;&quot;/api/foods/&quot;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.getJSON(url)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.done(function&nbsp;(data)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;html&nbsp;=&nbsp;&quot;<span class="html__tag_start">&lt;ul</span><span class="html__tag_start">&gt;</span>&quot;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.each(data,&nbsp;function&nbsp;(index,&nbsp;value)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;html&nbsp;&#43;=&nbsp;&quot;<span class="html__tag_start">&lt;li</span><span class="html__tag_start">&gt;</span>&quot;&nbsp;&#43;&nbsp;value&nbsp;&#43;&nbsp;&quot;<span class="html__tag_end">&lt;/li&gt;</span>&quot;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;html&nbsp;&#43;=&nbsp;&quot;<span class="html__tag_end">&lt;/ul&gt;</span>&quot;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(&quot;#list&quot;).html(html);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.fail(function(data){&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(&quot;#list&quot;).text(&quot;Error&nbsp;on&nbsp;Ajax!!!&quot;);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;
<span class="html__tag_end">&lt;/script&gt;</span>
End section
</pre>
</div>
</div>
</div>
<div class="endscriptcode">
<h1>Run the example</h1>
<div></div>
</div>
<div class="endscriptcode">Before the button&quot;#display&quot;<br>
<img id="155536" src="155536-before.jpg" alt="" width="510" height="313"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">After the button&quot;#display&quot;<br>
<img id="155537" src="155537-after.jpg" alt="" width="510" height="313"></div>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>All files of the project of VB Web API ;and MVC5 web site.;</em> </li></ul>
<h1>More Information</h1>
<p>In order to make a simple project, there is no communication with the database in this sample.</p>
