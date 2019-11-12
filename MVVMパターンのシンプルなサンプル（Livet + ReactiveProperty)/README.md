# MVVMパターンのシンプルなサンプル（Livet + ReactiveProperty)
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- WPF
## Topics
- MVVM
- ReactiveProperty
- Livet
## Updated
- 02/18/2015
## Description

<h1>サンプルプログラムの概要</h1>
<p>このサンプルプログラムは、MVVMパターンでシンプルなデータをメンテナンスするプログラムです。タイトルにもある通り以下のライブラリを使用しています。</p>
<ul>
<li>Livet<br>
http://ugaya40.hateblo.jp/entry/Livet&nbsp; </li><li>ReactiveProperty(preリリースのv2系を使用しています)<br>
https://github.com/runceel/reactiveproperty </li></ul>
<h1>サンプルプログラムの起動方法</h1>
<ol>
<li>&nbsp;サンプルをダウンロード </li><li>NuGetの復元の有効化 </li><li>NuGetパッケージの復元 </li><li>ビルドして実行 </li></ol>
<h2>サンプルプログラムの動作</h2>
<p>サンプルプログラムを起動すると以下のように、少量のデータが表示された画面が起動します。</p>
<p><img id="133942" src="133942-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="432" height="243"></p>
<p>DataGridのデータを選択するとEditとDeleteが有効になります。Editを押すと以下のような編集画面が起動します。</p>
<p><img id="133943" src="133943-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="434" height="245"></p>
<p>Deleteを押すと、確認画面が出て、OKを押すとデータが削除されます。</p>
<p><img id="133945" src="133945-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="434" height="244"></p>
<p>名前と年齢を入れると、追加ボタンが押せるようになり、データの追加が出来ます。</p>
<p><img id="133946" src="133946-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="433" height="245"></p>
<h1>サンプルプログラムの解説</h1>
<p>このサンプルプログラムのポイントを解説します。Modelは特に目新しいことのない通常のC#のクラス群になるので説明は割愛します。ViewModelでは、LivetのLivetCallMethodActionを使用するため、Commandを使用せずにEnabledかどうかを表すReactiveProperty&lt;bool&gt;と、メソッドのペアを定義しています。</p>
<p>個人的には、このアプローチはEventTriggerなどを使ったときに、CommandのCanExecutedとEnabledを同期できないという制約の中での、ちょうどいい妥協点だと思っています。</p>
<p>コードを以下に示します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;定義</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;追加</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;ReactiveProperty&lt;<span class="cs__keyword">bool</span>&gt;&nbsp;AddEnabled&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;組み立て</span>&nbsp;
<span class="cs__com">//&nbsp;入力対象のPersonにエラーがないときだけ押せる</span>&nbsp;
<span class="cs__keyword">this</span>.AddEnabled&nbsp;=&nbsp;<span class="cs__keyword">this</span>.InputPerson&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Where(x&nbsp;=&gt;&nbsp;x&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.SelectMany(x&nbsp;=&gt;&nbsp;x.HasErrors)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Select(x&nbsp;=&gt;&nbsp;!x)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveProperty(<span class="cs__keyword">false</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.AddTo(<span class="cs__keyword">this</span>.CompositeDisposable);&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;メソッド</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Add()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Model.Master.AddPerson();&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>上記のAdd関連のプロパティとメソッドをXAMLで紐付けをおこなっている箇所のコードは以下のようになります。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>

<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Content</span>=<span class="xaml__attr_value">&quot;追加&quot;</span>&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;75&quot;</span>&nbsp;<span class="xaml__attr_name">IsEnabled</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;AddEnabled.Value}&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;i</span>:Interaction.Triggers<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;i</span>:EventTrigger&nbsp;<span class="xaml__attr_name">EventName</span>=<span class="xaml__attr_value">&quot;Click&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;l</span>:LivetCallMethodAction&nbsp;<span class="xaml__attr_name">MethodName</span>=<span class="xaml__attr_value">&quot;Add&quot;</span>&nbsp;<span class="xaml__attr_name">MethodTarget</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;Mode=OneWay}&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/i:EventTrigger&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/i:Interaction.Triggers&gt;</span>&nbsp;
<span class="xaml__tag_end">&lt;/Button&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>また、Livetの特徴として、Viewだけで簡単なメッセージを発行して、その結果をViewModelのメソッドに渡すという機能があります。これは他のMVVMライブラリにはない機能で、とても強力です。ユーザーとの対話からはじまる一連の処理をとてもスマートに書くことができます。</p>
<p>このサンプルプログラムでは、Deleteの箇所で使用しています。DeleteのXAMLを以下に示します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>

<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;MenuItem</span>&nbsp;<span class="xaml__attr_name">Header</span>=<span class="xaml__attr_value">&quot;Delete&quot;</span>&nbsp;<span class="xaml__attr_name">IsEnabled</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;DeleteEnabled.Value}&quot;</span>&nbsp;<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;i</span>:Interaction.Triggers<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;i</span>:EventTrigger&nbsp;<span class="xaml__attr_name">EventName</span>=<span class="xaml__attr_value">&quot;Click&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;l</span><span class="xaml__tag_start">:ConfirmationDialogInteractionMessageAction&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;l</span>:DirectInteractionMessage&nbsp;<span class="xaml__attr_name">CallbackMethodTarget</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;Mode=OneWay}&quot;</span>&nbsp;<span class="xaml__attr_name">CallbackMethodName</span>=<span class="xaml__attr_value">&quot;Delete&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;l</span>:ConfirmationMessage&nbsp;<span class="xaml__attr_name">Button</span>=<span class="xaml__attr_value">&quot;OKCancel&quot;</span>&nbsp;<span class="xaml__attr_name">Caption</span>=<span class="xaml__attr_value">&quot;確認&quot;</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;削除してもいいですか&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/l:DirectInteractionMessage&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/l:ConfirmationDialogInteractionMessageAction&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/i:EventTrigger&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/i:Interaction.Triggers&gt;</span>&nbsp;
<span class="xaml__tag_end">&lt;/MenuItem&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>じゃっかん複雑ですが、メニューを押すと、ConfirmationMessageを受け取ったConfirmationDialogInteractionMessageActionが起動されます。そして、ユーザーとの対話の結果がDirectInteractionMessageに定義されているCallback****プロパティで指定したメソッド（コマンドも指定可能）に渡されます。</p>
<p>Deleteメソッドは以下のようになっています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">public&nbsp;<span class="js__operator">void</span>&nbsp;Delete(ConfirmationMessage&nbsp;message)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(message.Response&nbsp;!=&nbsp;true)&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__statement">return</span>;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.Model.Master.Delete(<span class="js__operator">this</span>.SelectedPerson.Value.Model.ID);&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<h1>まとめ</h1>
<p>LivetのWPFアプリのプロパティ部分とコレクション部分をReactivePropertyの機能で置き換えてみました。ReactivePropertyの機能でModelとViewModelの接続が容易になり、かつWPFに特化したLivetの機能も使えるWPFアプリを開発するなら割とありかなと思う組み合わせだと感じました。</p>
