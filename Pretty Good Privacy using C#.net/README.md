# Pretty Good Privacy using C#.net
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- C#
- System.Security.Cryptography.X509Certificates
## Topics
- Security
## Updated
- 04/16/2019
## Description

<h1>Introduction</h1>
<p>Pretty Good Privacy (PGP) &nbsp;was created by Phil Zimmermann in 1991, Is a data encryption and decryption technique. Signing will play vital role in this encryption and Decryption.</p>
<p>&nbsp;</p>
<h1>Conceptual View</h1>
<p>&nbsp;</p>
<p>Step 1 : passphrase[Signature] ------- &gt; Key Generator&nbsp;</p>
<p>Step 2 : Key Generator ----------Gives ------- Public key and Private Key</p>
<p>Step 3: Original Text[Plain Text] &nbsp;&#43; Public Key &#43; Private Key &#43; Signature = PGP Encryption</p>
<p>Step 4: PGP Encrypted Text &#43; Private Key &#43;&nbsp;passphrase[Signature] =&nbsp;Original Text[Plain Text]</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><img id="148100" src="148100-500px-pgp_diagram.svg.png" alt="" width="500" height="521"></p>
<p>&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p><em>To build the sample we need an API to get and generate symmetric and cipher keys.So i used&nbsp;BouncyCastle.Crypto.dll to acheive it.</em></p>
<p><em><span>Nuget Package Information : Install-Package BouncyCastle-Ext&nbsp;</span><br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>To Implement PGP program we need to do work for generate keys, encryption and dercyption. From the conceptual view you must give sign to get keys. So most of the people using several third things like&nbsp;https://www.igolder.com/pgp/generate-key/ to
 get keys.</em></p>
<p><em>From this sample you can have a solution block to get keys also.</em></p>
<p><em>&nbsp;&nbsp;</em></p>
<p><em>The below code snippet works to generate keys based on signature, password parametre is the signature value.</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;KeyGeneration()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;PublicKey&nbsp;and&nbsp;Private&nbsp;Key&nbsp;Generation</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PGPSnippet.KeyGeneration.KeysForPGPEncryptionDecryption.GenerateKey(<span class="cs__string">&quot;Username&quot;</span>,&nbsp;<span class="cs__string">&quot;password&quot;</span>,&nbsp;@<span class="cs__string">&quot;[D:\Keys\]filelocation&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Keys&nbsp;Generated&nbsp;Successfully&quot;</span>);<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<p>Step 2: Encryption part with public key, Private Key and signature</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Encryption()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;PGP&nbsp;Encryption</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PgpEncryptionKeys&nbsp;encryptionKeys&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;PgpEncryptionKeys(@<span class="cs__string">&quot;D:\Keys\PGPPublicKey.asc&quot;</span>,&nbsp;@<span class="cs__string">&quot;D:\Keys\PGPPrivateKey.asc&quot;</span>,&nbsp;<span class="cs__string">&quot;P@ll@m@lli&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PgpEncrypt&nbsp;encrypter&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;PgpEncrypt(encryptionKeys);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;(Stream&nbsp;outputStream&nbsp;=&nbsp;File.Create(<span class="cs__string">&quot;D:\\Keys\\EncryptData.txt&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;encrypter.EncryptAndSign(outputStream,&nbsp;<span class="cs__keyword">new</span>&nbsp;FileInfo(@<span class="cs__string">&quot;D:\Keys\PlainText.txt&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Encryption&nbsp;Done&nbsp;!&quot;</span>);<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">Step 3: PGP Encrypted Text &#43; Private Key &#43;&nbsp;passphrase[Signature] =&nbsp;Original Text[Plain Text]</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Decryption()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;PGP&nbsp;Decryption</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PGPDecrypt.Decrypt(<span class="cs__string">&quot;D:\\Keys\\EncryptData.txt&quot;</span>,&nbsp;@<span class="cs__string">&quot;D:\Keys\PGPPrivateKey.asc&quot;</span>,&nbsp;<span class="cs__string">&quot;P@ll@m@lli&quot;</span>,&nbsp;<span class="cs__string">&quot;D:\\Keys\\OriginalText.txt&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Decryption&nbsp;Done&quot;</span>);<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Step 4: <em>Implementation&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Program&nbsp;objPGP&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Program();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;objPGP.KeyGeneration();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;objPGP.Encryption();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;objPGP.Decryption();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Some&nbsp;thing&nbsp;went&nbsp;wrong&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Read();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</em></div>
</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Program.cs - Implementation and Calling about Series of steps</em> </li><li><em><em>KeysForPGPEncryptionDecryption.cs&nbsp; - Public and Private key generated with your signing and saved to defined physical location</em></em>
</li><li><em><em>PgpEncryptionKeys.cs - Keys Validations, thrown error if wrong key found or no keys found<br>
</em></em></li><li><em><em>PgpEncrypt.cs - PGP Encryption Logic<br>
</em></em></li><li><em><em>PGPDecrypt.cs - PGP Decyption Logic<br>
</em></em></li></ul>
<h1>More Information</h1>
<p><em>For more information on this artical please verify</em></p>
<p><em>https://en.wikipedia.org/wiki/Pretty_Good_Privacy</em></p>
<p><em>https://www.bouncycastle.org/</em></p>
<p><em>https://www.nuget.org/packages/BouncyCastle-Ext/</em></p>
<p><em>http://www.c-sharpcorner.com/UploadFile/0d5b44/pgp-pretty-good-privacy-using-C-Sharp/<br>
</em></p>
<p><em><br>
</em></p>
