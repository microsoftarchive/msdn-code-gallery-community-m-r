# MVC 4 Razor Design Sample Demo Project
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- Razor
- ASP.NET MVC 4
- ASP.NET Web API
- RadiobuttonFor Jquery Preselect
- Jquery slide down animation
- Jquery Ajax
- Htmlhelper
- htmlpartial
## Topics
- Asp.net MVC 4 Razor
## Updated
- 03/05/2013
## Description

<h1>Introduction</h1>
<div><em>This is a demo project in MCV 4 razor design which encompases the general design of MVC pattern. The objective is to help with scenarios involving navigations,resuable component,html helper, html partial ,html control ,event flow,intrinsic&nbsp;methods&nbsp;and
 Jquery. It also focus on real time scenario of calling controller action on radio button click such as autopost back mechanism. The project is simple and some what mimics real time example available in asp.net mvc site.This is not full pledged application
 but kind of proof of concepts involved to fellow developer to jumpstart with mvc. Suggest to focus on details.chtml for more gain in subject.</em></div>
<h1><span>Content</span></h1>
<ol>
<li>Razor Design </li><li>Html helper Example. Created custom Image and Hyperlink control @Html.Image </li><li>@Html.Partial ,act as a usercontrol </li><li>@Html.ActionLink&nbsp;&nbsp;&nbsp;&nbsp; </li><li>Jquery Ajax calls on Radio Button click event and server side call to controller class
</li><li>Radio Button Preselect(Checked==true) with Model data . On click event on radio button to invoke controller and client side Jquery to call Ajax URL .
</li></ol>
<div><span style="font-size:20px; font-weight:bold">Description</span></div>
<div><em>Sample Html.ActionLink with CSS Class. The Radio button with preselect checked options with respect to model object. We have something called Model.IsBookMarked==true.
</em></div>
<div>&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>

<div class="preview">
<pre class="html">@Html.ActionLink(&quot;Delete&quot;,&nbsp;&quot;Delete&quot;,&nbsp;new&nbsp;{&nbsp;id&nbsp;=&nbsp;item.ID&nbsp;},&nbsp;new&nbsp;{&nbsp;id&nbsp;=&nbsp;&quot;myDelete&quot;,&nbsp;@class&nbsp;=&nbsp;&quot;deleteCSS&quot;&nbsp;&#43;&nbsp;item.ID&nbsp;})</pre>
</div>
</div>
</div>
<h1><span>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>

<div class="preview">
<pre class="html">@Html.RadioButtonFor(m&nbsp;=&gt;&nbsp;Model.Title,&nbsp;Model.ID,&nbsp;Model.IsBookMarked&nbsp;==&nbsp;true&nbsp;?&nbsp;new&nbsp;{&nbsp;@checked&nbsp;=&nbsp;&quot;checked&quot;&nbsp;}&nbsp;:&nbsp;null)</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Details()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;albums&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;Album&gt;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Album&nbsp;{ID=<span class="cs__number">1</span>,Title&nbsp;=&nbsp;<span class="cs__string">&quot;Album&nbsp;1&quot;</span>&nbsp;,SubTitle=<span class="cs__string">&quot;Classic&quot;</span>,Description=<span class="cs__string">&quot;This&nbsp;is&nbsp;Elvis&nbsp;Prisley&quot;</span>,URL=<span class="cs__string">&quot;../img/music.png&quot;</span>,IsBookMarked=<span class="cs__keyword">true</span>},&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Album&nbsp;{ID=<span class="cs__number">2</span>,Title&nbsp;=&nbsp;<span class="cs__string">&quot;Album&nbsp;2&quot;</span>&nbsp;,SubTitle=<span class="cs__string">&quot;Musical&quot;</span>,Description=<span class="cs__string">&quot;This&nbsp;is&nbsp;Michael&nbsp;Jackson&quot;</span>,URL=<span class="cs__string">&quot;../img/music.png&quot;</span>,IsBookMarked=<span class="cs__keyword">false</span>},&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Album&nbsp;{ID=<span class="cs__number">3</span>,&nbsp;Title&nbsp;=&nbsp;<span class="cs__string">&quot;Album&nbsp;3&quot;</span>&nbsp;,SubTitle=<span class="cs__string">&quot;Jazz&quot;</span>,Description=<span class="cs__string">&quot;Bryan&nbsp;Adam&quot;</span>,URL=<span class="cs__string">&quot;../img/music.png&quot;</span>,IsBookMarked=<span class="cs__keyword">false</span>}&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;View(albums);&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<a href="http://www.santoshpoojari.blogspot.com">www.santoshpoojari.blogspot.com</a></div>
</div>
</span></h1>
<h1><em>References</em></h1>
<div><a href="http://pastebin.com/pN9H5rUA">http://pastebin.com/pN9H5rUA</a></div>
<div><a href="http://stackoverflow.com/questions/4444022/syntax-for-asp-net-mvc-phil-haacks-repeater-syntax-using-razor-mvc-3">http://stackoverflow.com/questions/4444022/syntax-for-asp-net-mvc-phil-haacks-repeater-syntax-using-razor-mvc-3</a></div>
<div><a href="http://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx">http://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx</a></div>
<div><a href="http://httputility.com/various/razorhelperscheatsheet.html">http://httputility.com/various/razorhelperscheatsheet.html</a></div>
<div><a href="http://www.martinwilley.com/net/asp/mvc/htmlhelpers.html">http://www.martinwilley.com/net/asp/mvc/htmlhelpers.html</a></div>
<div><a href="http://mvc4beginner.com/Tutorial/MVC-Best-Practice-Managing-Scripts.html">http://mvc4beginner.com/Tutorial/MVC-Best-Practice-Managing-Scripts.html</a></div>
<div><a href="http://stackoverflow.com/questions/5621013/pass-enum-to-html-radiobuttonfor-mvc3">http://stackoverflow.com/questions/5621013/pass-enum-to-html-radiobuttonfor-mvc3</a></div>
<div><a href="http://stackoverflow.com/questions/8952953/calling-asp-net-mvc-action-methods-from-javascript">http://stackoverflow.com/questions/8952953/calling-asp-net-mvc-action-methods-from-javascript</a></div>
<div><a href="http://ittecture.wordpress.com/2009/05/03/tip-of-the-day-201-asp-net-mvc-radiobutton-controls">http://ittecture.wordpress.com/2009/05/03/tip-of-the-day-201-asp-net-mvc-radiobutton-controls</a></div>
