# MVVMパターンのシンプルなサンプル（Prism + ReactiveProperty)
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- WPF
## Topics
- MVVM
- Prism
- ReactiveProperty
## Updated
- 02/18/2015
## Description

<h1>サンプルプログラムの概要</h1>
<p>このサンプルプログラムは、MVVMパターンでシンプルなデータをメンテナンスするプログラムです。タイトルにもある通り以下のライブラリを使用しています。</p>
<ul>
<li>Prism<br>
http://compositewpf.codeplex.com/ </li><li>ReactiveProperty(preリリースのv2系を使用しています)<br>
https://github.com/runceel/reactiveproperty </li></ul>
<h1>サンプルプログラムの起動方法</h1>
<ol>
<li>&nbsp;サンプルをダウンロード </li><li>NuGetの復元の有効化 </li><li>NuGetパッケージの復元 </li><li>ビルドして実行 </li></ol>
<h2>サンプルプログラムの動作</h2>
<p>サンプルプログラムを起動すると以下のように、少量のデータが表示された画面が起動します。</p>
<p><img id="133950" src="133950-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="387" height="228"></p>
<p>DataGridのデータを選択するとEditとDeleteが有効になります。Editを押すと以下のような編集画面が起動します。</p>
<p><img id="133951" src="133951-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="386" height="226"></p>
<p>Deleteを押すと、確認画面が出て、OKを押すとデータが削除されます。</p>
<p><img id="133952" src="133952-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="388" height="226"></p>
<p>名前と年齢を入れると、追加ボタンが押せるようになり、データの追加が出来ます。</p>
<p><img id="133953" src="133953-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="385" height="223"></p>
<h1>サンプルプログラムの解説</h1>
<p><span>このサンプルプログラムのポイントを解説します。Modelは特に目新しいことのない通常のC#のクラス群になるので説明は割愛します。Prismの機能の特徴の1つであるViewとViewModelの紐づけ機能を利用して、このプログラムではViewのDataContextにViewModelを設定しています。</span></p>
<p><span>Prismでは、デフォルトでViewModels名前空間のViewのクラス名＋ViewModelというクラスをViewに自動的に割り当てる機能があります。これは、ViewにIViewインターフェースを実装したあと、ViewModelLocator.AutoWiredViewModel添付プロパティをTrueに設定することで有効になります。該当箇所のコードを以下に示します。</span></p>
<p><span></span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">public&nbsp;partial&nbsp;class&nbsp;MainWindow&nbsp;:&nbsp;Window,&nbsp;IView&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;MainWindow()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>XAML配下のようになります。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>

<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;Window</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">xmlns</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">x</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">i</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/expression/2010/interactivity&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">ViewModels</span>=<span class="xaml__attr_value">&quot;clr-namespace:PrismWPFSampleApp.ViewModels&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">Custom</span>=<span class="xaml__attr_value">&quot;http://www.codeplex.com/prism&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">local</span>=<span class="xaml__attr_value">&quot;clr-namespace:PrismWPFSampleApp.Views&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">mvvm</span>=<span class="xaml__attr_value">&quot;clr-namespace:<a class="libraryLink" href="https://msdn.microsoft.com/ja-JP/library/Microsoft.Practices.Prism.Mvvm.aspx" target="_blank" title="Auto generated link to Microsoft.Practices.Prism.Mvvm">Microsoft.Practices.Prism.Mvvm</a>;assembly=Microsoft.Practices.Prism.Mvvm.Desktop&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">d</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/expression/blend/2008&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">mc</span>=<span class="xaml__attr_value">&quot;http://schemas.openxmlformats.org/markup-compatibility/2006&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;mc:<span class="xaml__attr_name">Ignorable</span>=<span class="xaml__attr_value">&quot;d&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Class</span>=<span class="xaml__attr_value">&quot;PrismWPFSampleApp.Views.MainWindow&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;mvvm:ViewModelLocator.<span class="xaml__attr_name">AutoWireViewModel</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Title</span>=<span class="xaml__attr_value">&quot;MainWindow&quot;</span>&nbsp;<span class="xaml__attr_name">Height</span>=<span class="xaml__attr_value">&quot;325.94&quot;</span>&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;452.256&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;d:<span class="xaml__attr_name">DataContext</span>=<span class="xaml__attr_value">&quot;{d:DesignInstance&nbsp;{x:Type&nbsp;ViewModels:MainWindowViewModel},&nbsp;IsDesignTimeCreatable=True}&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>ｄ：DataContextを使ってデザイナ用のDataContextを設定している点もポイントです。このようにすることで、XAMLのインテリセンスや、XAMLのデザイナ上でViewModelのプロパティ名の補完などの支援を受けることができるようになります。</p>
<p>ViewModelからダイアログを出す等のユーザーとの対話機能ですが、PrismではInteractionRequest&lt;T&gt;クラスを使用します。これは、INotifycationを実装したクラスをViewに投げてInteractionRequestTriggerで、それを受け取りActionを実行するという流れになります。PrismではデフォルトでPopupWindowActionというActionを定義しています。</p>
<p>これは、アラートダイアログを出してくれたり、ViewContentプロパティにユーザーコントロールを設定することで、そのユーザーコントロールをコンテンツにもったWindowを表示したりすることができます。これを使っているEditを押したときの処理を以下に示します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__operator">this</span>.EditCommand&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.Model.Detail.SetEditTarget(<span class="js__operator">this</span>.SelectedPerson.Value.Model.ID);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.EditRequest.Raise(<span class="js__operator">new</span>&nbsp;Notification&nbsp;<span class="js__brace">{</span>&nbsp;Title&nbsp;=&nbsp;<span class="js__string">&quot;編集&quot;</span>&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>

<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;Custom</span>:InteractionRequestTrigger&nbsp;<span class="xaml__attr_name">SourceObject</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;EditRequest}&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Custom</span>:PopupWindowAction&nbsp;<span class="xaml__attr_name">CenterOverAssociatedObject</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;<span class="xaml__attr_name">IsModal</span>=<span class="xaml__attr_value">&quot;True&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Custom</span>:PopupWindowAction.WindowContent<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;local</span>:EditView<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/Custom:PopupWindowAction.WindowContent&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/Custom:PopupWindowAction&gt;</span>&nbsp;
<span class="xaml__tag_end">&lt;/Custom:InteractionRequestTrigger&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h1>まとめ</h1>
<p>本文中で紹介した以外にも、InteractionRequest&lt;T&gt;をIObservableのチェインにつなげるための拡張メソッドなど、便利なクラスも定義しているので、DLして参照してみてください。Prismは、対応プラットフォームがWPFに絞られていないため、WPF特化機能という点では、WPFに特化したフレームワークに劣りますが、別プラットフォームでもノウハウが活かせるという点が強みになると思います。</p>
