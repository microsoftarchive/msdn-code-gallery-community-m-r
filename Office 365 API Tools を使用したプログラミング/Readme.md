# Office 365 API Tools を使用したプログラミング
## Requires
- 
## License
- Apache License, Version 2.0
## Technologies
- Microsoft Azure
- Office 365
- Visual Studio 2013
## Topics
- Office アプリケーション
## Updated
- 12/04/2014
## Description

<h1>Introduction</h1>
<p><em>Office 365 API は、オープンでシンプルなプロトコルを提供し、ライブラリーやユーティリティを使用せずに多くの言語環境でプログラミングできますが、ツールを使用することで、その生産性はさらに向上します。</em></p>
<p><em>ここでは、Office 365 API Tools for Visual Studio を使用した開発方法と、そのメリットを簡単に紹介します。</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>Office 365 API Tools for Visual Studio を使用して、認&#35388;処理、Exchange Online のエンドポイントへの要求をおこなって、Inbox のメールの一覧を取得するアプリケーションを構築します。</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="text-decoration:underline">開発環境の準備</span></p>
<p>ここで紹介するプログラミングをおこなう前に、事前に以下のコンポーネントのインストールやアカウント取得をおこなってください。</p>
<ul>
<li>Visual Studio 2013 のインストール </li><li>Office 365 Business または Enterprise のアカウントの取得 (試用版は <a href="http://www.microsoft.com/ja-jp/office/365/">
Office 365</a> のページからお申込みいただけます) </li><li>上記 Office 365 の管理者アカウント (組織アカウント) を使用した Microsoft Azure サブスクリプションの作成 (作成手順は <a href="http://blogs.msdn.com/b/tsmatsuz/archive/2014/06/02/office-365-api-programming.aspx">
こちら</a> で紹介しています) </li></ul>
<p>&nbsp;</p>
<p><span style="text-decoration:underline">Office 365 API Tools for Visual Studio 2013 のインストール</span></p>
<p>Visual Studio 2013 を起動します。</p>
<p>[ツール ] - [拡張機能と更新プログラム] メニューを選択して、オンラインのギャラリーから「Office 365 API Tools」を検索してインストールします。(ダウンロード / インストール後に Visual Studio の再起動を促されます。)</p>
<p><img id="127478" src="127478-install_o365apitools.jpg" alt="" width="621" height="350"></p>
<p>&nbsp;</p>
<p><span style="text-decoration:underline">プロジェクト作成と Azure Active Directory へのアプリケーション登録</span></p>
<p>Office 365 API Tools では、Windows ストア アプリ、Windows ストア ユニバーサル アプリ、Windows フォーム、Windows Presentation Foundation (以降、WPF と記載)、ASP.NET (ASP.NET Web フォーム、ASP.NET MVC の双方)、Xamarin、Windows Phone アプリ (および、Cordova によるハイブリッド アプリ用のツールも提供されています) での利用が可能ですが、今回は WPF を使用してデスクトップ
 アプリケーションで処理をおこないます。</p>
<p>まず、Visual Studio を開いて [WPF アプリケーション] を新規作成します。</p>
<p><img id="127479" src="127479-create_project.jpg" alt="" width="605" height="341"></p>
<p>Office 365 API を使用するアプリケーションでは、事前に Microsoft Azure Active Directory にアプリケーション登録をおこないますが、Office 365 API Tools を使用すると作成したプロジェクトからこのアプリケーションの作成と管理 (変更) をおこなうことができます。</p>
<p>上記で作成されたプロジェクトを右クリックして、[追加] - [接続済みサービス] を選択します。</p>
<p><img id="127480" src="127480-connected_services.jpg" alt="" width="522" height="447"></p>
<p>表示される画面で、[Register your app] をクリックすると Azure Active Directory へのログイン画面 (ブラウザー) が表示されるので、Office 365 の管理者アカウントとパスワードを入力してログインをおこないます。</p>
<p>今回は Exchange Online からメールの読み込みをおこなう簡単なアプリケーションを作成するため、ログイン後、[Mail] を選択して、[Permissions] をクリックします。(下図)</p>
<p><img id="127481" src="127481-start_permission.jpg" alt="" width="557" height="279"></p>
<p>表示される画面で、[Read user's mail] を選択して [Apply] ボタンを押し、[OK] ボタンを押してダイアログを閉じます。</p>
<p><img id="127482" src="127482-set_permission.jpg" alt="" width="309" height="169"></p>
<p>上記の設定により、Azure Active Directory にこのアプリケーションが登録され、アプリケーションの必要な設定情報 (Client ID など) が、プロジェクトの App.config に記述されます。(この登録されたアプリケーションのエントリを削除するには、Microsoft Azure 管理ポータルへのログインが必要です。)</p>
<p>&nbsp;</p>
<p><span style="text-decoration:underline">ライブラリを使用したプログラミング</span></p>
<p>では、プログラミングをおこなってみましょう。今回、ボタンとリストを配置し、ボタンを押すと Office 365 にログインをおこなって、Exchange Online からメールの一覧を取得してリストに表示します。</p>
<p>まず、MainWindow.xaml を表示して、ここに、下図の通り、ListBox と Button を配置します。(それぞれ、Name を「ListBox1」、「Button1」とします。)</p>
<p><img id="127483" src="127483-wpf_design.jpg" alt="" width="497" height="311"></p>
<p>プロジェクトを右クリックして [NuGet パッケージの管理] メニューを選択し、Microsoft Office 365 Authentication Library (Microsoft.Office365.OAuth) を選択してプロジェクトにインストールします。</p>
<p><img id="130763" src="130763-add_oauth_library.png" alt="" width="767" height="222"></p>
<p>上図のボタンをダブルクリックして、下記の通り、ボタンクリックのイベント処理を実装します。(Client ID, Redirect Uri は App.config に記述されています。System.Configuration を参照追加して、コードでこれらの値を参照しても構いません。)</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.OAuth;
using Microsoft.Office365.OutlookServices;

// 途中省略

private AuthenticationContext ac;

private async void Button_Click(object sender, RoutedEventArgs e)
{
  ac = new AuthenticationContext(&quot;https://login.windows.net/common&quot;);
  OutlookServicesClient cl = new OutlookServicesClient(
    new Uri(&quot;https://outlook.office365.com/api/v1.0&quot;),
    () =&gt;
    {
      AuthenticationResult ar = ac.AcquireToken(
        &quot;https://outlook.office365.com&quot;,
        &quot;69c98d4b-90d0-4985-920a-536183663e85&quot;,
        new Uri(&quot;http://localhost/625edc9f768f590a21fb1b567c0ee576&quot;));
      return Task.Factory.StartNew(() =&gt; { return ar.AccessToken; });
    });
  var res = await (from m in cl.Me.Messages
            select new { m.Subject }).ExecuteAsync();
  ListBox1.ItemsSource = res.CurrentPage;
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Microsoft.IdentityModel.Clients.ActiveDirectory;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Office365.OAuth;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Office365.OutlookServices;&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;途中省略</span>&nbsp;
&nbsp;
<span class="cs__keyword">private</span>&nbsp;AuthenticationContext&nbsp;ac;&nbsp;
&nbsp;
<span class="cs__keyword">private</span>&nbsp;async&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;ac&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;AuthenticationContext(<span class="cs__string">&quot;https://login.windows.net/common&quot;</span>);&nbsp;
&nbsp;&nbsp;OutlookServicesClient&nbsp;cl&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OutlookServicesClient(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://outlook.office365.com/api/v1.0&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;()&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AuthenticationResult&nbsp;ar&nbsp;=&nbsp;ac.AcquireToken(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;https://outlook.office365.com&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;69c98d4b-90d0-4985-920a-536183663e85&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://localhost/625edc9f768f590a21fb1b567c0ee576&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Task.Factory.StartNew(()&nbsp;=&gt;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;ar.AccessToken;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;var&nbsp;res&nbsp;=&nbsp;await&nbsp;(from&nbsp;m&nbsp;<span class="cs__keyword">in</span>&nbsp;cl.Me.Messages&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;select&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;m.Subject&nbsp;}).ExecuteAsync();&nbsp;
&nbsp;&nbsp;ListBox1.ItemsSource&nbsp;=&nbsp;res.CurrentPage;&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p><span style="text-decoration:underline">動作確認</span></p>
<p>F5 ボタンを押してデバッグ実行をおこなってみましょう。</p>
<p>画面のボタンをクリックすると、下図のログイン画面の表示と、内部で Access Token, Refresh Token の取得がおこなわれます。</p>
<p><img id="127484" src="127484-show_login.jpg" alt="" width="538" height="393"></p>
<p>ログインをおこなうと、下図のような、このアプリへの権限付与を確認するコンセント UI が表示されます。</p>
<p><img id="127486" src="127486-show_consent.jpg" alt="" width="463" height="361"></p>
<p>上図で [OK] を押すと、下図の通り、Inbox のメールの一覧のタイトル (Subject) が表示されます。</p>
<p><img id="127487" src="127487-execute_result.jpg" alt="" width="413" height="276"></p>
<p>&nbsp;</p>
<p><span style="text-decoration:underline">Access Token と Refresh Token</span></p>
<p>アプリケーションが Exchange のサービスに接続する際、内部で Access Token と呼ばれる文字列が使用されています。この Access Token を使用して、アプリケーションは、その他の処理も継続して実行できます。<br>
また、Access Token には有効期限があり、1 時間で使用できなくなりますが、この場合は Refresh Token と呼ばれるものを使用して、再度、Access Token を取り直します。<br>
上記の AcquireTokenSilentAsync では、こうした面倒なトークンの扱いも自動化してくれています。</p>
<p>また、Access Token や Refresh Token をキャッシュしてアプリケーション終了後もこの値を保持することで、同じアカウント情報で継続して利用することが可能です。(上記のサンプルでは、このキャッシュの処理はおこなっていません。)&nbsp;</p>
<p>&nbsp;</p>
<h1>More Information</h1>
<p><a href="http://blogs.msdn.com/b/tsmatsuz/archive/2014/06/02/office-365-api-programming.aspx">Office 365 API 入門</a></p>
<p><a href="https://code.msdn.microsoft.com/Office-365-API-Excel-VBA-cffdbb44">Office 365 API を VBA から使用する</a></p>
