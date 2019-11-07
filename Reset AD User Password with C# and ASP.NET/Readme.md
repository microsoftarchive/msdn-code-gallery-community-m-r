# Reset AD User Password with C# and ASP.NET
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- ASP.NET
- ADSI
- Active Directory
## Topics
- Security
- Active Directory
## Updated
- 09/15/2011
## Description

<h1>Introduction</h1>
<p>A sample project to show how to search for a user in Active Directory and reset that users password to the string password.</p>
<h1><span>Building the Sample</span></h1>
<p>The sample will build out of the box. To make it work you will need the following information in the web.config</p>
<ul>
<li>adAdminUser - Username that has permission to change the password </li><li>adAdminPassword - Password for the account above </li><li>adDomainFull - Domain in full format eg domain.com.global </li></ul>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>This sample project shows how to search for a user by using just the username rather than the full distinguished name. The code is quite easy to follow.</p>
<p>Using the directory searcher object to find the user by username only</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var deSearch = new DirectorySearcher(de)
                                             {SearchRoot = de, Filter = &quot;(&amp;(objectCategory=user)(cn=&quot; &#43; userName &#43; &quot;))&quot;};
            
            var results = deSearch.FindOne();</pre>
<div class="preview">
<pre class="csharp">var&nbsp;deSearch&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DirectorySearcher(de)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{SearchRoot&nbsp;=&nbsp;de,&nbsp;Filter&nbsp;=&nbsp;<span class="cs__string">&quot;(&amp;(objectCategory=user)(cn=&quot;</span>&nbsp;&#43;&nbsp;userName&nbsp;&#43;&nbsp;<span class="cs__string">&quot;))&quot;</span>};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;results&nbsp;=&nbsp;deSearch.FindOne();</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Code to reset the users password</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">directoryEntry.Invoke(&quot;SetPassword&quot;, new object[] {&quot;password&quot;});
            directoryEntry.Properties[&quot;LockOutTime&quot;].Value = 0; </pre>
<div class="preview">
<pre class="csharp">directoryEntry.Invoke(<span class="cs__string">&quot;SetPassword&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">object</span>[]&nbsp;{<span class="cs__string">&quot;password&quot;</span>});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;directoryEntry.Properties[<span class="cs__string">&quot;LockOutTime&quot;</span>].Value&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">More Information</h1>
</div>
<p>&nbsp;</p>
<p>For more information on the different aspects please see the following</p>
<ul>
<li>DirectoryEntry - <a href="http://msdn.microsoft.com/en-us/library/system.directoryservices.directoryentry.aspx">
http://msdn.microsoft.com/en-us/library/system.directoryservices.directoryentry.aspx</a>
</li><li>Invoke Method - <a href="http://msdn.microsoft.com/en-us/library/system.directoryservices.directoryentry.invoke.aspx">
http://msdn.microsoft.com/en-us/library/system.directoryservices.directoryentry.invoke.aspx</a>
</li><li>DirectorySearcher - <a href="http://msdn.microsoft.com/en-us/library/system.directoryservices.directorysearcher.aspx">
http://msdn.microsoft.com/en-us/library/system.directoryservices.directorysearcher.aspx</a>
</li><li>AuthenticationTypes Enumeration - <a href="http://msdn.microsoft.com/en-us/library/system.directoryservices.authenticationtypes.aspx">
http://msdn.microsoft.com/en-us/library/system.directoryservices.authenticationtypes.aspx</a>
</li></ul>
