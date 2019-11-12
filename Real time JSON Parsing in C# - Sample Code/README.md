# Real time JSON Parsing in C# - Sample Code
## Requires
- Visual Studio 2015
## License
- MS-LPL
## Technologies
- C#
- JSON
- ASP.NET Web API
- Json.NET
## Topics
- C#
- JSON
- Classes
- REST/JSON Web Services Communication
- Json Web Service
- json parsing in windows phone
- Post Json data in WindowsPhone 8
- json parsing in Windows Universal App
## Updated
- 04/06/2016
## Description

<h1>Introduction</h1>
<p><em>Parsing JSON response from a API server with C# and converting it into real usable properties in C# code<br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>Use Visual Studio 2015 for building the code</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><em>Parsing JSON response from a API server with C# and converting it into real usable properties in C# code</em><strong>&nbsp;</strong><em></em>.</em></p>
<p><em>This code sample uses a API&nbsp; response&nbsp; and parse it into a C#&nbsp; written classes and properties so that it can be used anywhere inside a C# project with ease.</em></p>
<p><em>Programmers could create classes for which&nbsp; Json needs parsed and then&nbsp; use JSON.Net to parse the API response into defined classes<br>
</em></p>
<p><em><em>Parsing JSON response from a API server with C# and converting it into real usable properties in C# code</em><strong></strong><em></em>.</em></p>
<p><em>This code sample uses a API&nbsp; response&nbsp; and parse it into a C#&nbsp; written classes and properties so that it can be used anywhere inside a C# project with ease.</em></p>
<p><em>Programmers could create classes for which&nbsp; Json needs parsed and then&nbsp; use JSON.Net to parse the API response into defined classes</em><strong></strong><em></em></p>
<p><em><em>Parsing JSON response from a API server with C# and converting it into real usable properties in C# code</em><strong></strong><em></em>.</em></p>
<p><em>This code sample uses a API&nbsp; response&nbsp; and parse it into a C#&nbsp; written classes and properties so that it can be used anywhere inside a C# project with ease.</em></p>
<p><em>Programmers could create classes for which&nbsp; Json needs parsed and then&nbsp; use JSON.Net to parse the API response into defined classes</em><strong></strong><em></em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__preproc">&nbsp;#region&nbsp;For&nbsp;Orders</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;jObject&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;JObject();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dynamic&nbsp;json&nbsp;=&nbsp;JValue.Parse(getOrders(r));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dynamic&nbsp;dynJsonc&nbsp;=&nbsp;json[<span class="cs__string">&quot;order&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Orders&gt;&nbsp;orders&nbsp;=&nbsp;dynJsonc.ToObject&lt;List&lt;Orders&gt;&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;item&nbsp;<span class="cs__keyword">in</span>&nbsp;orders)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(item.shipments[<span class="cs__number">0</span>].order_id&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;item.shipments[<span class="cs__number">0</span>].cart_vendor&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;item.shipments[<span class="cs__number">0</span>].status);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<em>Source code files attached</em> </ul>
<h1>More Information</h1>
<p><em>For more information on www.nucleon.in </em></p>
