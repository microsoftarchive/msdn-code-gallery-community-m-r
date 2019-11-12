# Real Time chat application using SingalR, ASP.Net MVC and Angular JS
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- jQuery
- Bootstrap
- ASP.NET Web API
- SignalR
- ASP.NET MVC 5
- AngularJS
## Topics
- Chat
- Chat Application
- SignalR
- AngularJS
- Real-time application
## Updated
- 01/26/2016
## Description

<h1>Introduction</h1>
<p><em><span><strong>SignalR</strong> provides a simple API for creating server-to-client remote procedure calls (RPC) that call JavaScript functions in client browsers (and other client platforms) from server-side .NET code. SignalR also includes API for connection
 management (for instance, connect and disconnect events), and grouping connections</span></em></p>
<p><em><strong><em>Angular JS</em> </strong>automatically synchronizes data from your UI (view) with your JavaScript objects (model) through 2-way data binding.I<span>t helps with server-side communication, taming async callbacks with promises and deferreds.</span></em></p>
<h1><span>Building the Sample</span></h1>
<p>&nbsp;You need Visual Studio 2013 to run/build this application</p>
<ul>
<li>Add MVC 5 project </li><li>Install Signalr and Angular Js to your project using below command in nuget package manager.
</li></ul>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>PowerShell</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">powershell</span>

<div class="preview">
<pre class="powershell">PM&gt;&nbsp;Install<span class="powerShell__operator">-</span>Package&nbsp;AngularJS.Core&nbsp;
&nbsp;
PM&gt;&nbsp;Install<span class="powerShell__operator">-</span>Package&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/Microsoft.AspNet.SignalR.aspx" target="_blank" title="Auto generated link to Microsoft.AspNet.SignalR">Microsoft.AspNet.SignalR</a></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<ul>
<li>Build the project, it will automaticaly resolves all its package dependancies for nuget.
</li><li>Create database and run the database script from &quot;App_data&quot; folder. </li><li>Configure you connection string in webconfig file to map your database. </li></ul>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>This is a basic sample that creates a Real-time chat applixation using powers of SignalR and angularJs. It includes various features of signalR and Angular Js.</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;UpdateStatus(<span class="cs__keyword">string</span>&nbsp;status)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;userId&nbsp;=&nbsp;Context.ConnectionId;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;loggedInUsers.FirstOrDefault(x&nbsp;=&gt;&nbsp;x.ConnectionId&nbsp;==&nbsp;userId).status&nbsp;=status;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Clients.Others.statusChanged(userId,status);&nbsp;
<span class="cs__com">//&nbsp;Other&nbsp;connected&nbsp;clients</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;UserTyping(<span class="cs__keyword">string</span>&nbsp;connectionId,&nbsp;<span class="cs__keyword">string</span>&nbsp;msg)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(connectionId&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;id&nbsp;=&nbsp;Context.ConnectionId;&nbsp;
<span class="cs__com">//&nbsp;client&nbsp;with&nbsp;connection&nbsp;specific&nbsp;connectionid's&nbsp;&quot;isTyping&quot;&nbsp;method&nbsp;will&nbsp;be&nbsp;called</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Clients.Client(connectionId).isTyping(id,&nbsp;msg);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<ul>
</ul>
<h1>More Information</h1>
<p><em>For more information on SignalR and AngularJs visit :</em></p>
<p><em><em><a href="http://www.asp.net/signalr" target="_blank">http://www.asp.net/signalr</a>&nbsp;&nbsp;</em></em></p>
<p><em><em><a href="https://angularjs.org/" target="_blank">https://angularjs.org
</a>&nbsp;</em></em></p>
<p><em><em><br>
</em></em></p>
