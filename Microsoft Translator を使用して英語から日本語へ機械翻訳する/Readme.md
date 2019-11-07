# Microsoft Translator を使用して英語から日本語へ機械翻訳する
## Requires
- 
## License
- Apache License, Version 2.0
## Technologies
- Visual Studio 2008
- .NET Framework 3.5 SP1
## Topics
- その他
- 逆引きサンプル コード
## Updated
- 02/09/2011
## Description

<p>執筆者: <a href="http://msdn.microsoft.com/ja-jp/gg585574#matsue" target="_blank">
日本システムウエア株式会社 松江 祐輔 (JZ5)</a></p>
<p>動作確認環境: Visual Studio 2010、.NET Framework 3.5 SP1</p>
<hr>
<p>Microsoft Translator という機械翻訳を提供する Web サービスを使用して英語のテキストを日本語へ機械翻訳します。</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">Visual Basic</div>
<div class="pluginEditHolderLink">{#scriptcode_dlg.edit_script}</div>
<span class="hidden">vb</span>
<pre class="hidden">Imports System.Net

Module Module1

    Sub Main()
        Dim appId As String = &quot;Your AppID&quot; ' &larr; ※
        Dim text As String = Uri.EscapeDataString(&quot;This is a sample code.&quot;)

        Using client = New WebClient()
            client.Encoding = System.Text.Encoding.UTF8

            Dim format As String = &quot;http://api.microsofttranslator.com/v2/Http.svc/Translate?appId={0}&amp;text={1}&amp;from=en&amp;to=ja&quot;
            Dim address As String = String.Format(format, appId, text)

            Try
                Dim body As String = client.DownloadString(address)
                Dim translatedText As String = XDocument.Parse(body).Elements.First.Value
                Console.WriteLine(translatedText)
            Catch ex As Exception
                Console.WriteLine(&quot;例外が発生しました&quot; &amp; vbCrLf &amp; ex.ToString)
            End Try
        End Using
    End Sub

End Module</pre>
<pre id="codePreview" class="vb"><span class="visualBasic__keyword">Imports</span>&nbsp;System.Net&nbsp;<br>&nbsp;<br><span class="visualBasic__keyword">Module</span>&nbsp;Module1&nbsp;<br>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Main()&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;appId&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Your&nbsp;AppID&quot;</span>&nbsp;<span class="visualBasic__com">'&nbsp;&larr;&nbsp;※</span>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;text&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;Uri.EscapeDataString(<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;a&nbsp;sample&nbsp;code.&quot;</span>)&nbsp;<br>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Using</span>&nbsp;client&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;WebClient()&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;client.Encoding&nbsp;=&nbsp;System.Text.Encoding.UTF8&nbsp;<br>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;format&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;http://api.microsofttranslator.com/v2/Http.svc/Translate?appId={0}&amp;text={1}&amp;from=en&amp;to=ja&quot;</span>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;address&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">String</span>.Format(format,&nbsp;appId,&nbsp;text)&nbsp;<br>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;body&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;client.DownloadString(address)&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;translatedText&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;XDocument.Parse(body).Elements.First.Value&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(translatedText)&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="visualBasic__string">&quot;例外が発生しました&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;ex.ToString)&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Using</span>&nbsp;<br>&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;<br>&nbsp;<br><span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Module</span><br></pre>
</div>
</div>
<div class="endscriptcode"></div>
<p>Microsoft Translator の使用には、<a href="http://www.bing.com/developers/appids.aspx" target="_blank">Bing デベロッパー センター</a>でアプリケーションの登録とアプリケーション ID を作成する必要があります。次の手順でアプリケーション ID を作成してください。</p>
<ol style="padding-left:20px">
<li><a href="http://www.bing.com/developers/appids.aspx" target="_blank">Bing デベロッパー センター</a>に Windows Live ID アカウントでサイン インします。
</li><li>Web ページの左側にあるリンク、「Create an AppID 」をクリックします。
<p style="margin-left:0; margin-right:0"><img src="17316-image001.gif" alt="図 1" width="580" height="139"></p>
</li><li>必要事項を記入および利用規約に同意のうえ、「Agree」をクリックします。 </li><li>作成されたアプリケーション ID (AppID) をコピーします。
<p style="margin-left:0; margin-right:0"><img src="17317-image002.gif" alt="図 2" width="580" height="211"></p>
</li></ol>
<p>アプリケーション ID はコード中の ※部分の文字列に設定します。</p>
<p style="margin-top:25px">正しくアプリケーション ID を設定したコードの実行結果は、以下のように表示されます。</p>
<div style="margin:10px 0; padding:10px; background-color:#ffffff; border:solid 1px #333333">
これは、サンプル コードです。</div>
<div style="margin:10px 0; padding:10px; background-color:#efefef; border:solid 1px #333333">
<strong>Note:</strong> Microsoft Translator は Web サービスです。インターネットに接続できる環境で実行する必要があります。</div>
<p>MSDN ライブラリには Microsoft Translator について詳しい説明があります。こちらもご参照ください。</p>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/ff512423" target="_blank">Microsoft Translator (英語)</a>
</li></ul>
<hr style="clear:both; margin-bottom:8px; margin-top:20px">
<table>
<tbody>
<tr>
<td><a href="http://msdn.microsoft.com/ja-jp/samplecode.recipe"><img title="Code Recipe" src="-ff950935.coderecipe_180x70%28ja-jp,msdn.10%29.jpg" border="0" alt="Code Recipe" width="180" height="70" style="margin-top:3px"></a></td>
<td>
<ul>
<li>もっと他のコンテンツを見る &gt;&gt; <a href="http://msdn.microsoft.com/ja-jp/ff363212" target="_blank">
逆引きサンプル コード一覧へ</a> </li><li>もっと他のレシピを見る &gt;&gt; <a href="http://msdn.microsoft.com/ja-jp/samplecode.recipe">
Code Recipe へ</a> </li></ul>
<p>&nbsp;</p>
</td>
</tr>
</tbody>
</table>
<p style="margin-top:20px"><a href="#top"><img src="-top.gif" border="0" alt="">ページのトップへ</a></p>
