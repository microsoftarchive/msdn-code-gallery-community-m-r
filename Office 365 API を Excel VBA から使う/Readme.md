# Office 365 API を Excel VBA から使う
## Requires
- 
## License
- Apache License, Version 2.0
## Technologies
- Microsoft Azure
- Office 365
## Topics
- Office 365
## Updated
- 03/26/2015
## Description

<h1>Introduction</h1>
<p><em>Office 365 API は、オープンでシンプルなプロトコルを提供し、ライブラリーやユーティリティを使用せずに多くの言語環境でプログラミングできます。ここでは、その一例として、使い慣れた Excel を使って Office 365 API を処理する VBA &nbsp;(Visual Basic for Applications) のサンプルを構築してみます。</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>ボタンを押すとログイン画面を表示し、ログイン後、Exchange Online のメールの一覧を取得してセルに書き込む簡単なサンプルを構築します。</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="text-decoration:underline">事前準備</span></p>
<p><em>ここで紹介するプログラミングをおこなう前に、事前に以下のアカウント取得をおこなってください。</em></p>
<ul>
<li><em>Office 365 Business または Enterprise のアカウントの取得 (試用版は <a href="http://www.microsoft.com/ja-jp/office/365/">
Office 365</a> のページからお申込みいただけます)</em> </li><li><em>上記 Office 365 の管理者アカウント (組織アカウント) を使用した Microsoft Azure サブスクリプションの作成</em>
</li></ul>
<p><em>つぎに、Microsoft Azure 管理ポータルに上記のアカウントでログインし、<em>「<a href="http://blogs.msdn.com/b/tsmatsuz/archive/2014/06/02/office-365-api-programming.aspx">Office 365 API 入門</a>」で紹介している手順で、</em>今回作成するアプリケーションを Azure Active Directory に登録します。(この際、設定した Redirect URI、取得した
 Client Id をメモしておいてください。)</em></p>
<ul>
<li><em>クライアントの種類として [Native Client Application] を選択してください。</em> </li><li><em>アプリケーションの権限 (Permission) に、[Office 365 Exchange Online] の [read user's mail] 付与してください。</em>
</li></ul>
<p>&nbsp;</p>
<p><em><span style="text-decoration:underline">VBA によるプログラミング</span><br>
</em></p>
<p>Excel を起動してオプション画面を表示し、リボンの [開発] タブを表示するように設定してください。(下図は Excel 2013 の場合の設定画面です。)</p>
<p><img id="127470" src="127470-show_devtab.jpg" alt="" width="628" height="396"></p>
<p>[開発] タブを選択して、シート上にボタンを配置します。この際、マクロの登録画面が表示されるので、[新規作成] ボタンを押して、ボタン クリック時の処理を以下の通り記述します。</p>
<p><img id="127472" src="127472-create_macro.jpg" alt="" width="372" height="358"></p>
<p>下記の [redirect url]、[client id] には、上記で取得した内容を設定してください。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ボタン1_Click()
'-- IE を起動する
Dim objBrowser As Object
Set objBrowser = CreateObject(&quot;InternetExplorer.Application&quot;)
objBrowser.Visible = True

'-- ログイン画面を表示する
objBrowser.Navigate &quot;https://login.microsoftonline.com/common/oauth2/authorize?response_type=code&amp;client_id=[client id]&amp;resource=https%3a%2f%2foutlook.office365.com%2f&amp;redirect_uri=[redirect url の URL エンコード文字列]&quot;

'-- リダイレクトページに戻ってくるまで待つ
While objBrowser.ReadyState &lt;&gt; 4 Or objBrowser.Busy = True Or (StrComp(Left(objBrowser.LocationURL, Len(&quot;[redirect url の文字列]&quot;)), &quot;[redirect url の文字列]&quot;) &lt;&gt; 0)
    DoEvents
Wend

'-- URL から code を取り出す
strQuery = Split(objBrowser.LocationURL, &quot;?&quot;)(1)
arrQuery = Split(strQuery, &quot;&amp;&quot;)
strCode = &quot;&quot;
For I = LBound(arrQuery) To UBound(arrQuery)
    arrElem = Split(arrQuery(I), &quot;=&quot;)
    If StrComp(arrElem(0), &quot;code&quot;) = 0 Then
        strCode = arrElem(1)
    End If
Next

'-- ブラウザー終了
objBrowser.Quit
Set objBrowser = Nothing

'-- Access Token の取得 (HTTP POST 要求)
Dim objXHttp1 As Object
Set objXHttp1 = CreateObject(&quot;msxml2.xmlhttp&quot;)
objXHttp1.Open &quot;POST&quot;, &quot;https://login.microsoftonline.com/common/oauth2/token&quot;, False
objXHttp1.setRequestHeader &quot;Content-Type&quot;, &quot;application/x-www-form-urlencoded&quot;
objXHttp1.send &quot;grant_type=authorization_code&amp;code=&quot; &amp; strCode &amp; &quot;&amp;client_id=[client id]&amp;redirect_uri=[redirect url の URL エンコード文字列]&quot;
'--Debug
'--Range(&quot;B5&quot;).Value = objXHttp1.Status
strToken1 = StrConv(objXHttp1.responsebody, vbUnicode)
Set objXHttp1 = Nothing

'-- Access Token のパース
strToken2 = Mid(strToken1, InStr(strToken1, &quot;{&quot;) &#43; 1, Len(strToken1) - InStr(strToken1, &quot;{&quot;) - (Len(strToken1) - InStrRev(strToken1, &quot;}&quot;) &#43; 1))
arrToken1 = Split(strToken2, &quot;,&quot;)
strAccessToken = &quot;&quot;
For I = LBound(arrToken1) To UBound(arrToken1)
    strElem = arrToken1(I)
    arrToken2 = Split(strElem, &quot;:&quot;)
    If (StrComp(Trim(arrToken2(0)), &quot;access_token&quot;) = 0) Or (StrComp(Trim(arrToken2(0)), &quot;&quot;&quot;access_token&quot;&quot;&quot;) = 0) Then
        strAccessToken = Replace(Trim(arrToken2(1)), &quot;&quot;&quot;&quot;, &quot;&quot;)
    End If
Next

'-- Exchange REST サービスの呼び出し
Dim objXHttp2 As Object
Set objXHttp2 = CreateObject(&quot;msxml2.xmlhttp&quot;)
objXHttp2.Open &quot;GET&quot;, &quot;https://outlook.office365.com/api/v1.0/me/messages?$top=20&amp;$select=Subject&quot;, False
objXHttp2.setRequestHeader &quot;Authorization&quot;, &quot;Bearer &quot; &amp; strAccessToken
objXHttp2.send
strResult = StrConv(objXHttp2.responsebody, vbUnicode)
Set objXHttp2 = Nothing

'-- 受け取った Json 文字列の結果をそのまま張り付ける
'-- (実際の開発では、ScriptControl オブジェクトなどでパースしてください)
Range(&quot;B5&quot;).Value = strResult

End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ボタン1_Click()&nbsp;
<span class="visualBasic__com">'--&nbsp;IE&nbsp;を起動する</span>&nbsp;
<span class="visualBasic__keyword">Dim</span>&nbsp;objBrowser&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>&nbsp;
<span class="visualBasic__keyword">Set</span>&nbsp;objBrowser&nbsp;=&nbsp;CreateObject(<span class="visualBasic__string">&quot;InternetExplorer.Application&quot;</span>)&nbsp;
objBrowser.Visible&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;ログイン画面を表示する</span>&nbsp;
objBrowser.Navigate&nbsp;<span class="visualBasic__string">&quot;https://login.microsoftonline.com/common/oauth2/authorize?response_type=code&amp;client_id=[client&nbsp;id]&amp;resource=https%3a%2f%2foutlook.office365.com%2f&amp;redirect_uri=[redirect&nbsp;url&nbsp;の&nbsp;URL&nbsp;エンコード文字列]&quot;</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;リダイレクトページに戻ってくるまで待つ</span>&nbsp;
<span class="visualBasic__keyword">While</span>&nbsp;objBrowser.ReadyState&nbsp;&lt;&gt;&nbsp;<span class="visualBasic__number">4</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;objBrowser.Busy&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;(StrComp(Left(objBrowser.LocationURL,&nbsp;Len(<span class="visualBasic__string">&quot;[redirect&nbsp;url&nbsp;の文字列]&quot;</span>)),&nbsp;<span class="visualBasic__string">&quot;[redirect&nbsp;url&nbsp;の文字列]&quot;</span>)&nbsp;&lt;&gt;&nbsp;<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;DoEvents&nbsp;
<span class="visualBasic__keyword">Wend</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;URL&nbsp;から&nbsp;code&nbsp;を取り出す</span>&nbsp;
strQuery&nbsp;=&nbsp;Split(objBrowser.LocationURL,&nbsp;<span class="visualBasic__string">&quot;?&quot;</span>)(<span class="visualBasic__number">1</span>)&nbsp;
arrQuery&nbsp;=&nbsp;Split(strQuery,&nbsp;<span class="visualBasic__string">&quot;&amp;&quot;</span>)&nbsp;
strCode&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
<span class="visualBasic__keyword">For</span>&nbsp;I&nbsp;=&nbsp;LBound(arrQuery)&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;UBound(arrQuery)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;arrElem&nbsp;=&nbsp;Split(arrQuery(I),&nbsp;<span class="visualBasic__string">&quot;=&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;StrComp(arrElem(<span class="visualBasic__number">0</span>),&nbsp;<span class="visualBasic__string">&quot;code&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;strCode&nbsp;=&nbsp;arrElem(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;ブラウザー終了</span>&nbsp;
objBrowser.Quit&nbsp;
<span class="visualBasic__keyword">Set</span>&nbsp;objBrowser&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;Access&nbsp;Token&nbsp;の取得&nbsp;(HTTP&nbsp;POST&nbsp;要求)</span>&nbsp;
<span class="visualBasic__keyword">Dim</span>&nbsp;objXHttp1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>&nbsp;
<span class="visualBasic__keyword">Set</span>&nbsp;objXHttp1&nbsp;=&nbsp;CreateObject(<span class="visualBasic__string">&quot;msxml2.xmlhttp&quot;</span>)&nbsp;
objXHttp1.Open&nbsp;<span class="visualBasic__string">&quot;POST&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;https://login.microsoftonline.com/common/oauth2/token&quot;</span>,&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
objXHttp1.setRequestHeader&nbsp;<span class="visualBasic__string">&quot;Content-Type&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;application/x-www-form-urlencoded&quot;</span>&nbsp;
objXHttp1.send&nbsp;<span class="visualBasic__string">&quot;grant_type=authorization_code&amp;code=&quot;</span>&nbsp;&amp;&nbsp;strCode&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&amp;client_id=[client&nbsp;id]&amp;redirect_uri=[redirect&nbsp;url&nbsp;の&nbsp;URL&nbsp;エンコード文字列]&quot;</span>&nbsp;
<span class="visualBasic__com">'--Debug</span>&nbsp;
<span class="visualBasic__com">'--Range(&quot;B5&quot;).Value&nbsp;=&nbsp;objXHttp1.Status</span>&nbsp;
strToken1&nbsp;=&nbsp;StrConv(objXHttp1.responsebody,&nbsp;vbUnicode)&nbsp;
<span class="visualBasic__keyword">Set</span>&nbsp;objXHttp1&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;Access&nbsp;Token&nbsp;のパース</span>&nbsp;
strToken2&nbsp;=&nbsp;Mid(strToken1,&nbsp;InStr(strToken1,&nbsp;<span class="visualBasic__string">&quot;{&quot;</span>)&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>,&nbsp;Len(strToken1)&nbsp;-&nbsp;InStr(strToken1,&nbsp;<span class="visualBasic__string">&quot;{&quot;</span>)&nbsp;-&nbsp;(Len(strToken1)&nbsp;-&nbsp;InStrRev(strToken1,&nbsp;<span class="visualBasic__string">&quot;}&quot;</span>)&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>))&nbsp;
arrToken1&nbsp;=&nbsp;Split(strToken2,&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>)&nbsp;
strAccessToken&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
<span class="visualBasic__keyword">For</span>&nbsp;I&nbsp;=&nbsp;LBound(arrToken1)&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;UBound(arrToken1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;strElem&nbsp;=&nbsp;arrToken1(I)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;arrToken2&nbsp;=&nbsp;Split(strElem,&nbsp;<span class="visualBasic__string">&quot;:&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;(StrComp(Trim(arrToken2(<span class="visualBasic__number">0</span>)),&nbsp;<span class="visualBasic__string">&quot;access_token&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__number">0</span>)&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;(StrComp(Trim(arrToken2(<span class="visualBasic__number">0</span>)),&nbsp;<span class="visualBasic__string">&quot;&quot;</span><span class="visualBasic__string">&quot;access_token&quot;</span><span class="visualBasic__string">&quot;&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__number">0</span>)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;strAccessToken&nbsp;=&nbsp;Replace(Trim(arrToken2(<span class="visualBasic__number">1</span>)),&nbsp;<span class="visualBasic__string">&quot;&quot;</span><span class="visualBasic__string">&quot;&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;Exchange&nbsp;REST&nbsp;サービスの呼び出し</span>&nbsp;
<span class="visualBasic__keyword">Dim</span>&nbsp;objXHttp2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>&nbsp;
<span class="visualBasic__keyword">Set</span>&nbsp;objXHttp2&nbsp;=&nbsp;CreateObject(<span class="visualBasic__string">&quot;msxml2.xmlhttp&quot;</span>)&nbsp;
objXHttp2.Open&nbsp;<span class="visualBasic__string">&quot;GET&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;https://outlook.office365.com/api/v1.0/me/messages?$top=20&amp;$select=Subject&quot;</span>,&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
objXHttp2.setRequestHeader&nbsp;<span class="visualBasic__string">&quot;Authorization&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Bearer&nbsp;&quot;</span>&nbsp;&amp;&nbsp;strAccessToken&nbsp;
objXHttp2.send&nbsp;
strResult&nbsp;=&nbsp;StrConv(objXHttp2.responsebody,&nbsp;vbUnicode)&nbsp;
<span class="visualBasic__keyword">Set</span>&nbsp;objXHttp2&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'--&nbsp;受け取った&nbsp;Json&nbsp;文字列の結果をそのまま張り付ける</span>&nbsp;
<span class="visualBasic__com">'--&nbsp;(実際の開発では、ScriptControl&nbsp;オブジェクトなどでパースしてください)</span>&nbsp;
Range(<span class="visualBasic__string">&quot;B5&quot;</span>).Value&nbsp;=&nbsp;strResult&nbsp;
&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">
<p>実際の開発では、access token だけでなく refresh token も取得します。<br>
取得した access token を保持しておき、access token が期限切れになったら refresh token を使ってaccess token を取り直します。また、refresh token が期限切れになった場合も、refresh token の取り直しをします。(最長で 90 日まで使用できます。)<br>
refresh token は、次回 ブック利用時に使用することで、2 回目以降はログインをおこなうことなく Exchange にアクセスできます。<br>
<strong>今回は、これらの処理は省略しています。</strong></p>
<p>作成したブックは、マクロ有効ブック (.xlsm) として保存してください。</p>
<p>&nbsp;</p>
</div>
<div class="endscriptcode"><span style="text-decoration:underline">動作確認</span></div>
<p>Excel に張り付けたボタンをクリックすると、下図の通り、ブラウザーが起動してログイン画面が表示されます。</p>
<p><img id="127474" src="127474-show_login.jpg" alt="" width="555" height="387"></p>
<p>上図で Office 365 のアカウントとパスワードを入力すると、下図の通り、Inbox のメールの一覧の Json 文字列がセルに記述されます。実際の開発では、この文字列のパースをおこなって Subject の一覧を表示するなど、処理を記述してください。</p>
<p><img id="127475" src="127475-execute_result.jpg" alt="" width="462" height="322"></p>
<p>なお、はじめてログインするユーザーの場合はアプリへの権限を付与するためのコンセント UI が表示されます。</p>
<p>&nbsp;</p>
<h1>More Information</h1>
<p><a href="http://blogs.msdn.com/b/tsmatsuz/archive/2014/06/02/office-365-api-programming.aspx">Office 365 API 入門</a></p>
