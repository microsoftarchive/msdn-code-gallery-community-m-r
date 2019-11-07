# Multi Language Application
## Requires
- Visual Studio 2005
## License
- Apache License, Version 2.0
## Technologies
- Windows Forms
## Topics
- Multiple Language Support Application
## Updated
- 07/28/2011
## Description

<h1>Introduction</h1>
<p><em>Develop multi support Language Desktop Application using VB.NET</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>this application will help you to develop multi support language... i developed it for two language support that are English and Urdu... you can modify it.. as you want.</em></p>
<p><em>Thank you&nbsp;</em></p>
<p>helping code is below</p>
<p>&nbsp;Private Sub setCulture()<br>
&nbsp; &nbsp; &nbsp; &nbsp; culture = CultureInfo.CreateSpecificCulture(Lang)&nbsp; &nbsp; &nbsp; &nbsp; Dim rm As New ResourceManager(&quot;MultiLanguageApplication.Login&quot;, GetType(LoginForm).Assembly)<br>
&nbsp; &nbsp; &nbsp; &nbsp; Me.Text = rm.GetString(&quot;FormTitle&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.UsernameLabel.Text = rm.GetString(&quot;LblUserName&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.PasswordLabel.Text = rm.GetString(&quot;LblPassword&quot;, culture)&nbsp; &nbsp;
 &nbsp; &nbsp; Me.OK.Text = rm.GetString(&quot;BttnOK&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.Cancel.Text = rm.GetString(&quot;BttnCancel&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errActive = rm.GetString(&quot;ErrActive&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errBlock =
 rm.GetString(&quot;ErrBlock&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errDelete = rm.GetString(&quot;ErrDelete&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errInvalid = rm.GetString(&quot;ErrInvalid&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errNormal = rm.GetString(&quot;ErrNormal&quot;,
 culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errActive_Heading = rm.GetString(&quot;ErrActive_Heading&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errBlock_Heading = rm.GetString(&quot;ErrBlock_Heading&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errDelete_Heading = rm.GetString(&quot;ErrDelete_Heading&quot;,
 culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errInvalid_Heading = rm.GetString(&quot;ErrInvalid_Heading&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.errNormal_Heading = rm.GetString(&quot;ErrNormal_Heading&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.LblLanguage.Text = rm.GetString(&quot;LblLanguage&quot;,
 culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.gbErrMessage.Text = rm.GetString(&quot;gBoxError&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.rBttnActive.Text = rm.GetString(&quot;rBttnActive&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.rBttnBlocked.Text = rm.GetString(&quot;rBttnBlocked&quot;,
 culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.rBttnDeleted.Text = rm.GetString(&quot;rBttnDeleted&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.rBttnNormal.Text = rm.GetString(&quot;rBttnNormal&quot;, culture)&nbsp; &nbsp; &nbsp; &nbsp; Me.rBttnInvalid.Text = rm.GetString(&quot;ErrInvalid_Heading&quot;,
 culture)&nbsp; &nbsp; End Sub</p>
<p>&nbsp;</p>
<p><img src="25606-pic_urdu.jpg" alt="" width="564" height="232"></p>
<p>&nbsp;</p>
<p><img src="25608-eng_pic.jpg" alt="" width="562" height="226"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden"> Private Sub setCulture()

        culture = CultureInfo.CreateSpecificCulture(Lang)
        Dim rm As New ResourceManager(&quot;MultiLanguageApplication.Login&quot;, GetType(LoginForm).Assembly)

        Me.Text = rm.GetString(&quot;FormTitle&quot;, culture)
        Me.UsernameLabel.Text = rm.GetString(&quot;LblUserName&quot;, culture)
        Me.PasswordLabel.Text = rm.GetString(&quot;LblPassword&quot;, culture)
        Me.OK.Text = rm.GetString(&quot;BttnOK&quot;, culture)
        Me.Cancel.Text = rm.GetString(&quot;BttnCancel&quot;, culture)
        Me.errActive = rm.GetString(&quot;ErrActive&quot;, culture)
        Me.errBlock = rm.GetString(&quot;ErrBlock&quot;, culture)
        Me.errDelete = rm.GetString(&quot;ErrDelete&quot;, culture)
        Me.errInvalid = rm.GetString(&quot;ErrInvalid&quot;, culture)
        Me.errNormal = rm.GetString(&quot;ErrNormal&quot;, culture)
        Me.errActive_Heading = rm.GetString(&quot;ErrActive_Heading&quot;, culture)
        Me.errBlock_Heading = rm.GetString(&quot;ErrBlock_Heading&quot;, culture)
        Me.errDelete_Heading = rm.GetString(&quot;ErrDelete_Heading&quot;, culture)
        Me.errInvalid_Heading = rm.GetString(&quot;ErrInvalid_Heading&quot;, culture)
        Me.errNormal_Heading = rm.GetString(&quot;ErrNormal_Heading&quot;, culture)
        Me.LblLanguage.Text = rm.GetString(&quot;LblLanguage&quot;, culture)
        Me.gbErrMessage.Text = rm.GetString(&quot;gBoxError&quot;, culture)
        Me.rBttnActive.Text = rm.GetString(&quot;rBttnActive&quot;, culture)
        Me.rBttnBlocked.Text = rm.GetString(&quot;rBttnBlocked&quot;, culture)
        Me.rBttnDeleted.Text = rm.GetString(&quot;rBttnDeleted&quot;, culture)
        Me.rBttnNormal.Text = rm.GetString(&quot;rBttnNormal&quot;, culture)
        Me.rBttnInvalid.Text = rm.GetString(&quot;ErrInvalid_Heading&quot;, culture)
    End Sub</pre>
<div class="preview">
<pre class="vb">&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;setCulture()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;culture&nbsp;=&nbsp;CultureInfo.CreateSpecificCulture(Lang)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rm&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;ResourceManager(<span class="visualBasic__string">&quot;MultiLanguageApplication.Login&quot;</span>,&nbsp;<span class="visualBasic__keyword">GetType</span>(LoginForm).Assembly)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;FormTitle&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.UsernameLabel.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;LblUserName&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.PasswordLabel.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;LblPassword&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.OK.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;BttnOK&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Cancel.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;BttnCancel&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errActive&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrActive&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errBlock&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrBlock&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errDelete&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrDelete&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errInvalid&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrInvalid&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errNormal&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrNormal&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errActive_Heading&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrActive_Heading&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errBlock_Heading&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrBlock_Heading&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errDelete_Heading&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrDelete_Heading&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errInvalid_Heading&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrInvalid_Heading&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.errNormal_Heading&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrNormal_Heading&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblLanguage.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;LblLanguage&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.gbErrMessage.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;gBoxError&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.rBttnActive.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;rBttnActive&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.rBttnBlocked.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;rBttnBlocked&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.rBttnDeleted.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;rBttnDeleted&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.rBttnNormal.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;rBttnNormal&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.rBttnInvalid.Text&nbsp;=&nbsp;rm.GetString(<span class="visualBasic__string">&quot;ErrInvalid_Heading&quot;</span>,&nbsp;culture)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>source code file MultiLanguageApplication</em> </li><li><em><br>
</em></li></ul>
<h1>More Information</h1>
<p><em>For more information on X,</em></p>
<p><em>www.a1vbcode.com</em></p>
<p><em>you can find it from above mentioned site.</em></p>
