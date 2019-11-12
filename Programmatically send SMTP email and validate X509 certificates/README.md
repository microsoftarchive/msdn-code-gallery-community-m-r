# Programmatically send SMTP email and validate X509 certificates
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- C#
- SMTP
- System.Net.Mail
- System.Net.Mail.SmtpClient
- System.Net.Security
- System.Security.Cryptography.X509Certificates
## Topics
- Send Email
- SMTP
- certificates
## Updated
- 11/17/2015
## Description

<h1>Introduction</h1>
<p><em>How to send remote SMTP email programmatically using .Net Framework and validate the certificates?</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>No special considerations or libraries needed. Just download and compile the app</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>This sample application will help you to send remote SMTP email programmatically using .Net Framework.
<em>For this activity, i used <a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.Net.Mail.aspx" target="_blank" title="Auto generated link to System.Net.Mail">System.Net.Mail</a> API/Namespace which&nbsp;contains classes used to send electronic mail to a Simple Mail Transfer Protocol (SMTP) server for delivery.</em>&nbsp;</em></p>
<p><em>The sender, recipient, subject, and body of an e-mail message may be specified as parameters when a MailMessage is used to initialize a MailMessage object. These parameters may also be set or accessed using properties on the MailMessage object.</em></p>
<p><em>Best part is no 3rd party API/library is needed and you can just use .Net Framework itself. Also you can validate the certificates as well.</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__mlcom">/*Create&nbsp;new&nbsp;Mail&nbsp;Message&nbsp;*/</span>&nbsp;
MailMessage&nbsp;mail&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MailMessage();&nbsp;
&nbsp;
<span class="cs__mlcom">/*&nbsp;Set&nbsp;From,&nbsp;To,&nbsp;Subject,&nbsp;Body&nbsp;of&nbsp;the&nbsp;email&nbsp;*/</span>&nbsp;
mail.From&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MailAddress(<span class="cs__string">&quot;frommailaddress@domain.com&quot;</span>);&nbsp;
mail.To.Add(<span class="cs__string">&quot;Tomailaddress@domain.com&quot;</span>);&nbsp;
mail.Subject&nbsp;=&nbsp;<span class="cs__string">&quot;Test&nbsp;Mail&nbsp;-&nbsp;SmtpClientEmail&quot;</span>;&nbsp;
mail.Body&nbsp;=&nbsp;<span class="cs__string">&quot;This&nbsp;is&nbsp;for&nbsp;testing&nbsp;SMTP&nbsp;mail&nbsp;from&nbsp;SmtpClientEmail&quot;</span>;&nbsp;
&nbsp;
<span class="cs__mlcom">/*Specify&nbsp;SMTPClient&nbsp;info&nbsp;-&nbsp;smtpserver,&nbsp;port,&nbsp;credentials,&nbsp;EnableSSL&nbsp;-&nbsp;if&nbsp;it&nbsp;needs&nbsp;SSL&nbsp;*/</span>&nbsp;
SmtpClient&nbsp;smtpServer&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SmtpClient(<span class="cs__string">&quot;smtpserveraddress.com&quot;</span>);&nbsp;
smtpServer.Port&nbsp;=&nbsp;<span class="cs__number">587</span>;&nbsp;
smtpServer.Credentials&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.Net.NetworkCredential.aspx" target="_blank" title="Auto generated link to System.Net.NetworkCredential">System.Net.NetworkCredential</a>(<span class="cs__string">&quot;frommailaddress@domain.com&quot;</span>,&nbsp;<span class="cs__string">&quot;password&quot;</span>);&nbsp;
smtpServer.EnableSsl&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;
<span class="cs__mlcom">/*&nbsp;Certificate&nbsp;Validation&nbsp;*/</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
ServicePointManager.ServerCertificateValidationCallback&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">delegate</span>(<span class="cs__keyword">object</span>&nbsp;s,&nbsp;X509Certificate&nbsp;certificate,&nbsp;X509Chain&nbsp;chain,&nbsp;SslPolicyErrors&nbsp;sslPolicyErrors)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;};&nbsp;
&nbsp;
<span class="cs__mlcom">/*Now&nbsp;you&nbsp;can&nbsp;send&nbsp;the&nbsp;email*/</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;smtpServer.Send(mail);</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Refer the solution</em> </li></ul>
<h1>More Information</h1>
<ul>
<li><em>For more information, you can get more related info from the following:</em>
<ul>
<li><em><a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.Net.Mail.aspx" target="_blank" title="Auto generated link to System.Net.Mail">System.Net.Mail</a> Namespace -&nbsp;<a href="https://msdn.microsoft.com/en-us/library/system.net.mail(v=vs.110).aspx">https://msdn.microsoft.com/en-us/library/system.net.mail(v=vs.110).aspx</a>&nbsp;</em>
</li><li><em>MailMessage Class -&nbsp;<a href="https://msdn.microsoft.com/en-us/library/system.net.mail.mailmessage(v=vs.110).aspx">https://msdn.microsoft.com/en-us/library/system.net.mail.mailmessage(v=vs.110).aspx</a></em>
</li><li>SmtpClient Class -&nbsp;<a href="https://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient(v=vs.110).aspx">https://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient(v=vs.110).aspx</a>
</li></ul>
</li></ul>
