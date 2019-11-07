# Prism.Mvvm + ReactiveProperty サンプル
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- Prism
- ReactiveProperty
## Topics
- MVVM
- Reactive Programming
## Updated
- 09/06/2014
## Description

<h1>概要</h1>
<p>このサンプルアプリケーションは、Prism.Mvvmと、Prism.Interactivityと、Unityと、ReactivePropertyを使った簡単な割り算を行うサンプルアプリケーションです。サンプルアプリケーションは、0除算を行うとエラーを処理するように作っているため、正常系だけでなく異常系をどのように扱うかの例にもなります。</p>
<p>起動直後の画面</p>
<p><img id="124904" src="124904-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="302" height="301"></p>
<p>計算をする前に確認ダイアログが出ます。</p>
<p><img id="124905" src="124905-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="305" height="308"></p>
<p>計算結果が表示されます。</p>
<p><img id="124906" src="124906-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="302" height="301"></p>
<p>0除算をするとエラーが表示されます。</p>
<p><img id="124907" src="124907-%e3%82%ad%e3%83%a3%e3%83%97%e3%83%81%e3%83%a3.png" alt="" width="303" height="302"></p>
<h1>サンプルアプリケーションの構造</h1>
<p>サンプルアプリケーションは、MVVMパターンで作成されています。Modelのルートになるオブジェクトと、Model間でのイベントの通知に使うEventAggregatorクラスをUnity内でSingletonになるように管理しています。こうすることでModelをViewModel間で引き渡したり、Model内やModelからViewModelへのイベント通知がシンプルに行えるようになっています。</p>
<p>PrismでUnityを使うには、従来の方法だとUnityBootstrapperクラスを継承するなどの必要がありましたが、このサンプルアプリケーションでは、ViewModelLocationProviderを使ってViewModelをUnityから取得するようにしています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public partial class App : Application
{
    private UnityContainer container;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        this.container = new UnityContainer();
        this.container.RegisterType&lt;AppContext&gt;(new ContainerControlledLifetimeManager());
        this.container.RegisterType&lt;EventAggregator&gt;(new ContainerControlledLifetimeManager());

        ViewModelLocationProvider.SetDefaultViewModelFactory(t =&gt; this.container.Resolve(t));
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;App&nbsp;:&nbsp;Application&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;UnityContainer&nbsp;container;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Application_Startup(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;StartupEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.container&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;UnityContainer();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.container.RegisterType&lt;AppContext&gt;(<span class="cs__keyword">new</span>&nbsp;ContainerControlledLifetimeManager());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.container.RegisterType&lt;EventAggregator&gt;(<span class="cs__keyword">new</span>&nbsp;ContainerControlledLifetimeManager());&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ViewModelLocationProvider.SetDefaultViewModelFactory(t&nbsp;=&gt;&nbsp;<span class="cs__keyword">this</span>.container.Resolve(t));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>ViewModelLocationProviderのSetDefaultViewModelFactoryメソッドで、ViewModelの生成方法とUnityを使うように設定しています。</p>
<h2>ModelとViewModel間の接続</h2>
<p>ModelとViewModel間の接続はReactivePropertyを使用しています。Modelは通常のINotifyPropertyChanged(正確には、Prism.Mvvm内のBindableBaseクラス）を継承したオブジェクトになります。このオブジェクトのプロパティをViewModelのReactivePropertyとして双方向同期をとるようにしています。</p>
<p>例として、1つのプロパティのコードを示します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// 左辺値
this.Lhs = model.Calculator.ObserveProperty(c =&gt; c.Lhs)
    .Select(d =&gt; d.ToString())
    .ToReactiveProperty()
    .SetValidateAttribute(() =&gt; this.Lhs);
// エラーが無いときはModelへ代入する
this.Lhs
    .Where(_ =&gt; !this.Lhs.HasErrors)
    .Subscribe(s =&gt; model.Calculator.Lhs = double.Parse(s));
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;左辺値</span>&nbsp;
<span class="cs__keyword">this</span>.Lhs&nbsp;=&nbsp;model.Calculator.ObserveProperty(c&nbsp;=&gt;&nbsp;c.Lhs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Select(d&nbsp;=&gt;&nbsp;d.ToString())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveProperty()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.SetValidateAttribute(()&nbsp;=&gt;&nbsp;<span class="cs__keyword">this</span>.Lhs);&nbsp;
<span class="cs__com">//&nbsp;エラーが無いときはModelへ代入する</span>&nbsp;
<span class="cs__keyword">this</span>.Lhs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Where(_&nbsp;=&gt;&nbsp;!<span class="cs__keyword">this</span>.Lhs.HasErrors)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Subscribe(s&nbsp;=&gt;&nbsp;model.Calculator.Lhs&nbsp;=&nbsp;<span class="cs__keyword">double</span>.Parse(s));&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>ReactivePropertyのObservePropertyを使ってPOCOをReactivePropertyに変換しています。ReactivePropertyのエラーが無い場合のみ、ReactivePropertyからModelへのデータの受け渡しを行っています。</p>
<h2>ViewとViewModel間の通信</h2>
<p>Prism.InteractivityのInteractionRequestを使用しています。InteractionRequestをReactiveCommandと合成できるように以下のような拡張メソッドを定義しています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public static IObservable&lt;T&gt; RaiseAsObservable&lt;T&gt;(this InteractionRequest&lt;T&gt; self, T n)
    where T : INotification
{
    return Observable.Create&lt;T&gt;(o =&gt;
    {
        self.Raise(n, result =&gt; { o.OnNext(result); o.OnCompleted(); });
        return Disposable.Empty;
    });
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;IObservable&lt;T&gt;&nbsp;RaiseAsObservable&lt;T&gt;(<span class="cs__keyword">this</span>&nbsp;InteractionRequest&lt;T&gt;&nbsp;self,&nbsp;T&nbsp;n)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;where&nbsp;T&nbsp;:&nbsp;INotification&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Observable.Create&lt;T&gt;(o&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;self.Raise(n,&nbsp;result&nbsp;=&gt;&nbsp;{&nbsp;o.OnNext(result);&nbsp;o.OnCompleted();&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Disposable.Empty;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>このようなメソッドを定義することで、以下のようにReactive ExtensionsのメソッドチェインにInteractionRequestの処理をのせることができます。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// 計算を実行するコマンド
this.CalcCommand
    .SelectMany(_ =&gt; this.ConfirmRequest.RaiseAsObservable(new Confirmation
    {
        Title = &quot;確認&quot;,
        Content = &quot;計算を実行してもいいですか&quot;
    }))
    .Where(c =&gt; c.Confirmed)
    .Subscribe(_ =&gt; model.Calculator.Div());
</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;計算を実行するコマンド</span>&nbsp;
<span class="js__operator">this</span>.CalcCommand&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.SelectMany(_&nbsp;=&gt;&nbsp;<span class="js__operator">this</span>.ConfirmRequest.RaiseAsObservable(<span class="js__operator">new</span>&nbsp;Confirmation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Title&nbsp;=&nbsp;<span class="js__string">&quot;確認&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Content&nbsp;=&nbsp;<span class="js__string">&quot;計算を実行してもいいですか&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Where(c&nbsp;=&gt;&nbsp;c.Confirmed)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Subscribe(_&nbsp;=&gt;&nbsp;model.Calculator.Div());&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
