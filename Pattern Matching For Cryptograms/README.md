# Pattern Matching For Cryptograms
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- LINQ
- Windows Forms
- .NET Framework
- Visual Basic .NET
- VB.Net
- .NET Framework 4.0
- Visual Studio 2012
## Topics
- Encryption
- Cryptography
- Encryption/Decryption
- Crypto
## Updated
- 01/18/2013
## Description

<h1>Please Remember To Rate This Contribution!</h1>
<p>Thanks ;)</p>
<p><img id="74757" src="74757-pattern.png" alt="" width="550" height="397"></p>
<h1>Pattern Matching For Cryptograms</h1>
<p><em>Not too long ago, I answered a question in the msdn forums about pattern matching, and how to isolate the realistic possibilites of words, and skip the jibberish. This example does that.</em></p>
<h1>Description</h1>
<p><em>This code iterates through each letter of an 'Unknown word', &nbsp;So let's say for example the word is &quot;kdfygrk&quot;.</em></p>
<p><em>The code will search each letter and do something like this:</em></p>
<p><span style="text-decoration:underline"><strong><em>First Iteration:(k)</em></strong></span></p>
<p><em>The letter 'k' has not been assigned an identifier, therefore we will assign the next available identifier( '0' in this case) to the letter k.</em></p>
<p><em>So far our pattern is 0??????</em></p>
<p><span style="text-decoration:underline"><strong><em>Next iteration(d)</em></strong></span></p>
<p><em>The letter 'd' has not been assigned an identifier, therefore we will assign the next available identifier( '1' in this case) to the letter d.</em></p>
<p><em>So far our pattern is 01?????</em></p>
<p><span style="text-decoration:underline"><strong><em>Next iteration(f)</em></strong></span></p>
<p><em>The letter 'f' has not been assigned an identifier, therefore we will assign the next available identifier( '2' in this case) to the letter f.</em></p>
<p><em>So far our pattern is 012????</em></p>
<p><span style="text-decoration:underline"><strong><em>Next iteration(y)</em></strong></span></p>
<p><em>The letter 'm' has not been assigned an identifier, therefore we will assign the next available identifier( '3' in this case) to the letter m.</em></p>
<p><em>So far our pattern is 0123???</em></p>
<p><span style="text-decoration:underline"><strong><em>Next iteration(g)</em></strong></span></p>
<p><em>The letter 'g' has not been assigned an identifier, therefore we will assign the next available identifier( '4' in this case) to the letter g.</em></p>
<p><em>So far our pattern is 01234??</em></p>
<p><span style="text-decoration:underline"><strong><em>Next iteration(r)</em></strong></span></p>
<p><em>The letter 'r' has not been assigned an identifier, therefore we will assign the next available identifier( '5' in this case) to the letter r.</em></p>
<p><em>So far our pattern is 012345?</em></p>
<p><span style="text-decoration:underline"><strong><em>Final iteration(k)</em></strong></span></p>
<p><em>The letter 'k' has already been assigned an identifier, therefore we will use the previously assigned identifier for 'k'('0' in this case)</em></p>
<p><em>So this leaves us with a pattern of '0123450'</em></p>
<p>&nbsp;</p>
<p><em>So now we shrink the dictionary to include only words that meet this criteria:</em></p>
<ul>
<li><em>Words of same length(7)</em> </li><li><em>Words that match the specified filter(* by default)</em> </li></ul>
<p>&nbsp;</p>
<p><em>So now we loop through each of the shrunken dictionaries words and check each word's pattern against the pattern of the unknown word. If they have like patterns then they are added to the results.</em></p>
<p>&nbsp;</p>
<p>This is the code involved with comments, but downloading the project is highly recommended, because the controls are already layed out.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>

<div class="preview">
<pre class="vb"><span class="visualBasic__com">'''&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;This&nbsp;function&nbsp;calculates&nbsp;likely&nbsp;word&nbsp;matches&nbsp;for&nbsp;cryptogram&nbsp;words.</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;param&nbsp;name=&quot;Word&quot;&gt;The&nbsp;encrypted&nbsp;word&lt;/param&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;param&nbsp;name=&quot;Dictionary&quot;&gt;A&nbsp;list&nbsp;of&nbsp;words&nbsp;to&nbsp;match&nbsp;the&nbsp;encrypted&nbsp;word&nbsp;against.&lt;/param&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;param&nbsp;name=&quot;Filter&quot;&gt;A&nbsp;filter&nbsp;pattern&nbsp;for&nbsp;reducing&nbsp;results.&lt;/param&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;param&nbsp;name=&quot;PB&quot;&gt;Optional&nbsp;Progressbar&nbsp;to&nbsp;report&nbsp;progress.&lt;/param&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;param&nbsp;name=&quot;UpdateLabel&quot;&gt;Optional&nbsp;Label&nbsp;to&nbsp;report&nbsp;current&nbsp;match&nbsp;count.&lt;/param&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;returns&gt;&lt;/returns&gt;</span>&nbsp;
<span class="visualBasic__com">'''&nbsp;&lt;remarks&gt;&lt;/remarks&gt;</span>&nbsp;
<span class="visualBasic__keyword">Function</span>&nbsp;GetWordPatternMatches(Word&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dictionary&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;List(<span class="visualBasic__keyword">Of</span>&nbsp;<span class="visualBasic__keyword">String</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;Filter&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;*&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProgressBar&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;UpdateLabel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Label&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>)&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ListViewItem()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Maximum&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'A&nbsp;list&nbsp;of&nbsp;identifications&nbsp;for&nbsp;pattern&nbsp;matching</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;Legend&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;01234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'return&nbsp;an&nbsp;empty&nbsp;array&nbsp;if&nbsp;there&nbsp;is&nbsp;no&nbsp;word&nbsp;to&nbsp;match</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Word.Length&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;{}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Create&nbsp;a&nbsp;new&nbsp;pattern&nbsp;table</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;map&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;List(<span class="visualBasic__keyword">Of</span>&nbsp;pt),&nbsp;I&nbsp;=&nbsp;<span class="visualBasic__number">0</span>,&nbsp;WordPattern&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Maximum&nbsp;&#43;=&nbsp;Word.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Examine&nbsp;each&nbsp;letter&nbsp;in&nbsp;the&nbsp;encrypted&nbsp;word</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;S&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;Word&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Increment(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'search&nbsp;the&nbsp;pattern&nbsp;table&nbsp;to&nbsp;see&nbsp;if&nbsp;the&nbsp;letter&nbsp;was&nbsp;already&nbsp;assigned&nbsp;an&nbsp;identification</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Q1&nbsp;=&nbsp;From&nbsp;P&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;map&nbsp;Where&nbsp;P.Letter&nbsp;=&nbsp;S&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;P&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;it&nbsp;has&nbsp;then&nbsp;use&nbsp;the&nbsp;same&nbsp;identification&nbsp;for&nbsp;that&nbsp;letter</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Q1.ToArray.Count&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;map.Add(<span class="visualBasic__keyword">New</span>&nbsp;pt(Q1.ToArray(<span class="visualBasic__number">0</span>).ID,&nbsp;S))&nbsp;:&nbsp;<span class="visualBasic__keyword">Continue</span>&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;it&nbsp;has&nbsp;not,&nbsp;then&nbsp;assign&nbsp;a&nbsp;new&nbsp;pattern&nbsp;identification</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map.Add(<span class="visualBasic__keyword">New</span>&nbsp;pt((Legend)(I),&nbsp;S))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Increment&nbsp;the&nbsp;next&nbsp;pattern&nbsp;id&nbsp;index&nbsp;number</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I&nbsp;&#43;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Maximum&nbsp;&#43;=&nbsp;map.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Go&nbsp;through&nbsp;each&nbsp;mapped&nbsp;letter</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;P&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;pt&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;map&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Increment(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Assemble&nbsp;the&nbsp;encrypted&nbsp;word's&nbsp;pattern</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WordPattern&nbsp;=&nbsp;WordPattern&nbsp;&amp;&nbsp;P.ID&nbsp;:&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Get&nbsp;all&nbsp;word&nbsp;from&nbsp;the&nbsp;dictionary&nbsp;that&nbsp;are:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'A.)&nbsp;The&nbsp;same&nbsp;length&nbsp;of&nbsp;the&nbsp;bord</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'B.)&nbsp;Match&nbsp;the&nbsp;FILTER&nbsp;specified</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Q2&nbsp;=&nbsp;From&nbsp;W&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;Dictionary&nbsp;Where&nbsp;(W.Length&nbsp;=&nbsp;Word.Length)&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;(W&nbsp;<span class="visualBasic__keyword">Like</span>&nbsp;Filter)&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;W&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Create&nbsp;a&nbsp;list&nbsp;for&nbsp;holding&nbsp;the&nbsp;result</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;results&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;List(<span class="visualBasic__keyword">Of</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Maximum&nbsp;&#43;=&nbsp;Q2.ToArray.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Go&nbsp;through&nbsp;each&nbsp;dictionary&nbsp;word&nbsp;from&nbsp;the&nbsp;LINQ&nbsp;result</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;W&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;Q2.ToArray&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Increment(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Create&nbsp;a&nbsp;pattern&nbsp;map&nbsp;for&nbsp;each&nbsp;word&nbsp;from&nbsp;the&nbsp;LINQ&nbsp;result,&nbsp;create&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;legend&nbsp;index&nbsp;counter,&nbsp;create&nbsp;a&nbsp;dictionary&nbsp;word&nbsp;pattern&nbsp;to&nbsp;compare&nbsp;against&nbsp;the&nbsp;encrypted&nbsp;word&nbsp;pattern</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;map2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;List(<span class="visualBasic__keyword">Of</span>&nbsp;pt),&nbsp;I2&nbsp;=&nbsp;<span class="visualBasic__number">0</span>,&nbsp;DictPattern&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Go&nbsp;through&nbsp;each&nbsp;character,&nbsp;of&nbsp;each&nbsp;word&nbsp;from&nbsp;the&nbsp;LINQ&nbsp;result</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;S&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;W&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'search&nbsp;the&nbsp;pattern&nbsp;table&nbsp;to&nbsp;see&nbsp;if&nbsp;the&nbsp;letter&nbsp;was&nbsp;already&nbsp;assigned&nbsp;an&nbsp;identification</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Q3&nbsp;=&nbsp;From&nbsp;P&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;map2&nbsp;Where&nbsp;P.Letter&nbsp;=&nbsp;S&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;P&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;it&nbsp;has&nbsp;then&nbsp;use&nbsp;the&nbsp;same&nbsp;identification&nbsp;for&nbsp;that&nbsp;letter</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Q3.ToArray.Count&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;map2.Add(<span class="visualBasic__keyword">New</span>&nbsp;pt(Q3.ToArray(<span class="visualBasic__number">0</span>).ID,&nbsp;S))&nbsp;:&nbsp;<span class="visualBasic__keyword">Continue</span>&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;it&nbsp;has&nbsp;not,&nbsp;then&nbsp;assign&nbsp;a&nbsp;new&nbsp;pattern&nbsp;identification</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map2.Add(<span class="visualBasic__keyword">New</span>&nbsp;pt((Legend)(I2),&nbsp;S))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Increment&nbsp;the&nbsp;next&nbsp;pattern&nbsp;id&nbsp;index&nbsp;number</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I2&nbsp;&#43;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;:&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Go&nbsp;through&nbsp;each&nbsp;mapped&nbsp;letter</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;P&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;pt&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;map2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Assemble&nbsp;the&nbsp;dictionary&nbsp;word's&nbsp;pattern</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DictPattern&nbsp;=&nbsp;DictPattern&nbsp;&amp;&nbsp;P.ID&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Compare&nbsp;the&nbsp;encrypted&nbsp;word's&nbsp;pattern&nbsp;to&nbsp;the&nbsp;pattern&nbsp;of&nbsp;each&nbsp;result&nbsp;from&nbsp;the&nbsp;LINQ&nbsp;query(Q2)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;DictPattern&nbsp;=&nbsp;WordPattern&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;results.Add(W)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;provided&nbsp;a&nbsp;label&nbsp;to&nbsp;update&nbsp;status</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;UpdateLabel&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Change&nbsp;the&nbsp;label's&nbsp;text&nbsp;to&nbsp;reflect&nbsp;the&nbsp;current&nbsp;matches&nbsp;found</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UpdateLabel.Text&nbsp;=&nbsp;results.Count&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;matches&nbsp;found&nbsp;so&nbsp;far...&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'refresh&nbsp;the&nbsp;label/app</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application.DoEvents()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Create&nbsp;a&nbsp;list&nbsp;for&nbsp;returning&nbsp;the&nbsp;final&nbsp;results</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Items&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;List(<span class="visualBasic__keyword">Of</span>&nbsp;ListViewItem)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Maximum&nbsp;&#43;=&nbsp;results.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;S&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;results&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;the&nbsp;user&nbsp;specified&nbsp;a&nbsp;progressbar,&nbsp;then&nbsp;update&nbsp;the&nbsp;values</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;PB&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;PB.Increment(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Create&nbsp;a&nbsp;new&nbsp;listview&nbsp;item&nbsp;with&nbsp;subitem(0)&nbsp;being&nbsp;the&nbsp;encrypted&nbsp;word</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Item&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;ListViewItem(Word)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Add&nbsp;2&nbsp;subitems&nbsp;to&nbsp;the&nbsp;item(Dictionary&nbsp;word,&nbsp;the&nbsp;pattern&nbsp;that&nbsp;they&nbsp;were&nbsp;matched&nbsp;with)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Item.SubItems.AddRange({S,&nbsp;WordPattern})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Add&nbsp;the&nbsp;item&nbsp;to&nbsp;the&nbsp;final&nbsp;results</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Items.Add(Item)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'convert&nbsp;the&nbsp;resuts&nbsp;and&nbsp;return&nbsp;it&nbsp;as&nbsp;an&nbsp;array&nbsp;of&nbsp;Listviewitem</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;Items.ToArray&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Class</span>&nbsp;pt&nbsp;<span class="visualBasic__com">'&nbsp;Pattern&nbsp;Table</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'I.e.&nbsp;The&nbsp;letter&nbsp;can&nbsp;only&nbsp;recieve&nbsp;this&nbsp;ID,&nbsp;this&nbsp;ID&nbsp;can&nbsp;only&nbsp;represent&nbsp;this&nbsp;letter</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;ID,&nbsp;Letter&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;<span class="visualBasic__keyword">New</span>(ID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>,&nbsp;Letter&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Populate&nbsp;the&nbsp;ID&nbsp;and&nbsp;Letter&nbsp;values&nbsp;of&nbsp;this&nbsp;pattern&nbsp;table</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.ID&nbsp;=&nbsp;ID&nbsp;:&nbsp;<span class="visualBasic__keyword">Me</span>.Letter&nbsp;=&nbsp;Letter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Class</span></pre>
</div>
</div>
</div>
<p><em>&nbsp;</em></p>
