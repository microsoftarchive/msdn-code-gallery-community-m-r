# MVC 5 with 2FA, email confirmation, password reset SMS,Two-Factor Authentication
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- ASP.NET MVC
## Topics
- Security
## Updated
- 02/07/2019
## Description

<h1>Introduction</h1>
<p>Follow the tutorials:</p>
<ul>
<li><a href="http://www.asp.net/mvc/overview/getting-started/aspnet-mvc-5-app-with-sms-and-email-two-factor-authentication">ASP.NET MVC 5 app with SMS and email Two-Factor Authentication<br>
</a></li><li><a href="http://www.asp.net/mvc/overview/getting-started/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset">Create a secure ASP.NET MVC 5 web app with log in, email confirmation and password reset<br>
</a></li></ul>
<p>This is the source code with debug helpers that goes with the above tutorials.</p>
<h1><span>Building the Sample</span></h1>
<p>The&nbsp;first time you build the solution NuGet will download missing libraries.</p>
<p>You must create a file in called&nbsp;<em>AppSettingsSecrets.config.ignore</em> which will contain the&nbsp;&lt;appSettings&gt; secrets for SendGrid, Twilio and Google authentication. This must be in the same folder as the root
<em>web.config</em> file. See the <em>web.config</em> file. See the sample markup below.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;appSettings&gt;   
   &lt;!-- SendGrid--&gt;
   &lt;add key=&quot;mailAccount&quot; value=&quot;your account&quot; /&gt;
   &lt;add key=&quot;mailPassword&quot; value=&quot;your password&quot; /&gt;
   &lt;!-- Twilio--&gt;
   &lt;add key=&quot;TwilioSid&quot; value=&quot;Twilio SID&quot; /&gt;
   &lt;add key=&quot;TwilioToken&quot; value=&quot;Twilio Token&quot; /&gt;
   &lt;add key=&quot;TwilioFromPhone&quot; value=&quot;&#43;12065551234&quot; /&gt;

   &lt;add key=&quot;GoogClientID&quot; value=&quot;1234.apps.googleusercontent.com&quot; /&gt;
   &lt;add key=&quot;GoogClientSecret&quot; value=&quot;111111&quot; /&gt;
&lt;/appSettings&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;appSettings</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__comment">&lt;!--&nbsp;SendGrid--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;mailAccount&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;your&nbsp;account&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;mailPassword&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;your&nbsp;password&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__comment">&lt;!--&nbsp;Twilio--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;TwilioSid&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;Twilio&nbsp;SID&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;TwilioToken&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;Twilio&nbsp;Token&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;TwilioFromPhone&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;&#43;12065551234&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;GoogClientID&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;1234.apps.googleusercontent.com&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;GoogClientSecret&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;111111&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/appSettings&gt;</span></pre>
</div>
</div>
</div>
<pre>This tutorial was written  by  <a href="http://blogs.msdn.com/rickAndy">Rick Anderson</a>&nbsp;&nbsp;( Twitter: <a href="https://twitter.com/RickAndMSFT">@RickAndMSFT</a> ).<br></pre>
<pre>Clicking on the <strong> Remember this browser</strong> check box will exempt you from needing to use 2FA  to log on with that computer and browser. Enabling 2FA and clicking on the <strong> Remember this browser</strong> will provide you with strong 2FA protection from  malicious users trying to access your account, as long as they don't have access  to your computer. You can do this on any private machine you regularly use. By  setting <strong> Remember this browser</strong>, you get the added security of 2FA from computers  you don't regularly use, and you get the convenience on not having to go through  2FA on your own computers.&nbsp;<br></pre>
