# MetroTips #91 OCR（光学文字認識）機能を実装するには？［ユニバーサルWindowsアプリ開発］
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- Windows Runtime
- Windows RT
- Windows Store app
- Windows 8.1
- OCR
- Windows Phone 8.1
- universal windows app
- universal project
- Windows Phone Windows Runtime app
- ユニバーサル Windows アプリ
- ユニバーサル プロジェクト
- Converged Apps
- Microsoft OCR Library for Windows Runtime
## Topics
- OCR
- Microsoft OCR Library for Windows Runtime
- OcrEngine
## Updated
- 10/11/2014
## Description

<div><img src="111788-image.png" alt="" align="middle"></div>
<div>&nbsp;</div>
<h1>Introduction</h1>
<div>これは次の記事のサンプルコードです。</div>
<blockquote>
<div>@IT 2014/10/09 掲載<br>
<img id="68637" src="http://i1.code.msdn.s-msft.com/windowsapps/metrotips-10-cb520e60/image/file/68637/1/gyoumu_newallarticle_icon_61_1349480145.gif" alt="" width="80" height="60"><br>
<strong><a href="http://www.atmarkit.co.jp/ait/subtop/features/dotnet/app/winrttips_index.html" target="_blank">WinRT／Metro TIPS：</a><br>
<a href="http://www.atmarkit.co.jp/ait/articles/1410/09/news119.html" target="_blank">OCR（光学文字認識）機能を実装するには？［ユニバーサルWindowsアプリ開発］</a></strong><br>
<br>
アプリの中でOCR（光学文字認識）機能を使いたいと思ったことはないだろうか？ 例えば、名刺を読み取って電話番号やメールアドレスなどをデータベースに登録したい、あるいは、商品コードを読み取って検索したいといったような場合だ。そのようなことが、マイクロソフトから提供された「<a href="http://blogs.windows.com/buildingapps/2014/09/18/microsoft-ocr-library-for-windows-runtime/" target="_blank">Microsoft
 OCR Library for Windows Runtime</a>」のOcrEngineで可能になった。</div>
</blockquote>
<div><img id="126526" src="http://i1.code.msdn.s-msft.com/windowsapps/metrotips-91-2aa58172/image/file/126526/1/08.png" alt="" width="253" height="454"><br>
<br>
以下の解説は記事の概要である。ぜひ記事のほうもお読みいただきたい。</div>
<div>&nbsp;</div>
<h1>Building the Sample</h1>
<div>ユニバーサルプロジェクトを使ってユニバーサルWindowsアプリを開発するには、以下の開発環境が必要である。本稿では、無償のVisual Studio Express 2013 Update 3 for Windowsを使っている。</div>
<div><br>
・ <a href="http://blogs.msdn.com/b/aonishi/archive/2012/11/01/10364540.aspx" target="_blank">
SLAT対応のPC</a><br>
・ <a href="http://support.microsoft.com/kb/2919355">2014年4月のアップデート</a>適用済みの64bit版Windows 8.1 Pro版以上<br>
・ <a href="http://www.microsoft.com/ja-jp/download/details.aspx?id=42666" target="_blank">
Visual Studio 2013 Update 2</a>（またはそれ以降）を適用済みのVisual Studio 2013（以降、VS 2013）</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<h1>Description</h1>
<h2>●サンプルコードについて</h2>
<div>Visual Studio 2013 Update 2のRTMがリリースされたが、残念なことにVB用のユニバーサルプロジェクトのテンプレートは <a href="http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/5729640-provide-vb-net-templates-for-developing-universal" target="_blank">
Visual Studio 「14」での提供になる</a>という。 そのため、本稿で紹介するコードはC#のユニバーサルプロジェクトだけとさせていただく。</div>
<div>&nbsp;</div>
<h3 style="color:red">【注意】</h3>
<div>先に MetroTips #75 ～ #90 をアンインストールしておくこと。</div>
<div>同じソースをベースにしているため、 Windows Phone 上では同居できない。 Windows Phone にそれらがインストールされていると、 配置に失敗する。</div>
<div>&nbsp;</div>
<h2>●OcrEngineで画像から文字列を読み取るには？</h2>
<p>OcrEngineのRecognizeAsyncメソッドに画像データを渡せばよい。</p>
<p>OcrEngineの導入方法は、記事をご覧いただきたい。WriteableBitmapオブジェクトを渡して文字列を認識させ、その結果をテキストボックスに表示するメソッドは、次のように書ける。</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;async&nbsp;System.Threading.Tasks.Task&nbsp;&nbsp;
&nbsp;&nbsp;ExtractTextAsync(Windows.UI.Xaml.Media.Imaging.WriteableBitmap&nbsp;bitmap,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WindowsPreview.Media.Ocr.OcrLanguage&nbsp;language)&nbsp;
{&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;OcrEngineオブジェクトを生成する</span>&nbsp;
&nbsp;&nbsp;var&nbsp;ocrEngine&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;WindowsPreview.Media.Ocr.OcrEngine(language);&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;OcrEngineに画像を渡して、文字列を認識させる</span>&nbsp;
&nbsp;&nbsp;var&nbsp;ocrResult&nbsp;=&nbsp;await&nbsp;ocrEngine.RecognizeAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cs__keyword">uint</span>)bitmap.PixelHeight,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;画像の高さ</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cs__keyword">uint</span>)bitmap.PixelWidth,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;画像の幅</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bitmap.PixelBuffer.ToArray()&nbsp;&nbsp;<span class="cs__com">//&nbsp;画像のデータ（バイト配列）</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ocrResult.Lines&nbsp;==&nbsp;<span class="cs__keyword">null</span>&nbsp;||&nbsp;ocrResult.Lines.Count&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.ReadText.Text&nbsp;=&nbsp;<span class="cs__string">&quot;(何も読み取れませんでした)&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>;&nbsp;
&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;この時点で、読み取りは終わっている。以降は、読み取り結果を表示するためのコードである</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;Word間の区切り（日本語では無し、英語ではスペースとする）</span>&nbsp;
&nbsp;&nbsp;var&nbsp;separater&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(language&nbsp;==&nbsp;WindowsPreview.Media.Ocr.OcrLanguage.English)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;separater&nbsp;=&nbsp;<span class="cs__string">&quot;&nbsp;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;結果を表示するテキストボックスをクリアする</span>&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">this</span>.ReadText.Text&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;行番号（0始まり）を定義</span>&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;lineIndex&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;認識結果を行ごとに処理する</span>&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;line&nbsp;<span class="cs__keyword">in</span>&nbsp;ocrResult.Lines)&nbsp;
&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;1行分の文字列を&#26684;納するためのバッファー</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;sb&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/ja-JP/library/System.Text.StringBuilder.aspx" target="_blank" title="Auto generated link to System.Text.StringBuilder">System.Text.StringBuilder</a>();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;認識結果の行を、Wordごとに処理する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;word&nbsp;<span class="cs__keyword">in</span>&nbsp;line.Words)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;認識した文字列をバッファーに追加していく</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sb.Append(word.Text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sb.Append(separater);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;ここでは、読み取った1行を以下のフォーマットで表示することにした</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.ReadText.Text&nbsp;&#43;=&nbsp;<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;[{0}{1}]&nbsp;{2}{3}&quot;</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(lineIndex&#43;&#43;).ToString(),&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;行番号（0始まり）</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;line.IsVertical&nbsp;?&nbsp;<span class="cs__string">&quot;V&quot;</span>&nbsp;:&nbsp;<span class="cs__string">&quot;H&quot;</span>,&nbsp;&nbsp;<span class="cs__com">//&nbsp;縦書き／横書きの区別</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sb.ToString().TrimEnd(),&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;読み取った文字列</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Environment.NewLine&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;改行</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div>&nbsp;</div>
<h1>Source Code Files</h1>
<ul>
<li style="font-family:Verdana"><em>MetroTips091CS - ユニバーサルプロジェクトのサンプルコード (C#)</em>
</li></ul>
<div>&nbsp;</div>
<h1>More Information</h1>
<h2>著作権について</h2>
<div>このソースコードは MS-LPL ライセンスで提供しておりますので、 ご自由に利用いただけます。<br>
ただし、@ITの記事(ここへ転載・引用した部分も含む)そのものの著作権は、筆者とデジタルアドバンテージが保有しており、サンプルコード部分を除く記事の無断使用は固くお断りいたします。</div>
