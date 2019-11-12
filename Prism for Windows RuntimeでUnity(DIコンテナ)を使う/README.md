# Prism for Windows RuntimeでUnity(DIコンテナ)を使う
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- Windows Store app
## Topics
- Unity
- Prism for Windows Runtime
## Updated
- 10/10/2014
## Description

<h1>サンプルプログラムの概要</h1>
<p>このサンプルプログラムは、Prism for Windows RuntimeでUnity（ゲームではなくてDIコンテナ）を使う方法をしめしています。サンプルプログラムは、以下のサンプルコードをベースにUnityを使うように書き換えています。</p>
<ul>
<li>Prism for Windows Runtimeを使ったODataの更新アプリサンプル<br>
<a href="https://code.msdn.microsoft.com/Prism-for-Windows-49e7e2da">https://code.msdn.microsoft.com/Prism-for-Windows-49e7e2da</a>
</li></ul>
<h1>サンプルプログラムの実行方法</h1>
<p>ソリューションのNuGetパッケージの復元を有効化して、リビルドしてください。リビルド後実行することでサンプルを実行することができます。</p>
<h1>サンプルプログラムの解説</h1>
<p>Unityを使えるようにするためには、NuGetからUnityをインストールします。Unityをインストールしたら、AppクラスでUnityを使うようにセットアップを行います。まず、UnityコンテナのインスタンスをAppクラスのフィールドに保持します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">sealed</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;App&nbsp;:&nbsp;MvvmAppBase&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;UnityContainer&nbsp;container&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;UnityContainer();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;省略</span>&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>次に、OnInitializeAsyncメソッドで、UnityContainerに対して必要なクラスの登録を行います。このサンプルプログラムではAppContextクラスをアプリ内で唯一のインスタンスとして管理したいため、AppContextクラスの登録と、INavigationServiceもViewModelで使うために登録しています。OnInitializeAsyncメソッドのコードを以下に示します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;Task&nbsp;OnInitializeAsync(IActivatedEventArgs&nbsp;args)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Modelのインスタンスをコンテナでシングルトンに管理してもらう</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;container.RegisterType&lt;AppContext&gt;(<span class="cs__keyword">new</span>&nbsp;ContainerControlledLifetimeManager());&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;NavigationServiceのインスタンスをコンテナに登録</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;container.RegisterInstance(<span class="cs__keyword">this</span>.NavigationService);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Task.FromResult&lt;<span class="cs__keyword">object</span>&gt;(<span class="cs__keyword">null</span>);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>次に、Prism for Windows Runtimeがクラスのインスタンス化を行う方法をカスタマイズします。これは、MvvmAppBaseクラスのResolveメソッドをオーバーライドします。Resolveメソッドは引数にTypeを受け取って、戻り値でTypeをインスタンス化したものをかえすメソッドです。Unityコンテナから取得したインスタンスを返すように書き換えます。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">protected&nbsp;override&nbsp;object&nbsp;Resolve(Type&nbsp;type)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;<span class="js__operator">this</span>.container.Resolve(type);&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>最後に、MainPageViewModelクラスとEditPageViewModelクラスのデザインタイム用ではない引数のあるコンストラクタにUnityがインスタンス化するときに使用するようにするInjectionConstructorAttributeを追加して完了です。（コンストラクタが1つしかないクラスでは不要です）</p>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="js__sl_comment">///&nbsp;Modelと画面遷移サービスで初期化するコンストラクタ</span>&nbsp;
<span class="js__sl_comment">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="js__sl_comment">///&nbsp;&lt;param&nbsp;name=&quot;model&quot;&gt;&lt;/param&gt;</span>&nbsp;
<span class="js__sl_comment">///&nbsp;&lt;param&nbsp;name=&quot;navigationService&quot;&gt;&lt;/param&gt;</span>&nbsp;
[InjectionConstructor]&nbsp;
public&nbsp;MainPageViewModel(AppContext&nbsp;model,&nbsp;INavigationService&nbsp;navigationService)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.model&nbsp;=&nbsp;model;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.navigationService&nbsp;=&nbsp;navigationService;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.LoadPeopleCommand&nbsp;=&nbsp;DelegateCommand.FromAsyncHandler(<span class="js__operator">this</span>.LoadPeopleAsync);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.NavigateEditPageCommand&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DelegateCommand&lt;Person&gt;(<span class="js__operator">this</span>.NavigateEditPage);&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</h1>
<h1>まとめ</h1>
<p>Unityを使うようにすると、画面数が増えても、AppクラスのOnInitializeAsyncメソッドの中身が膨らんでいくことがなくなります。また、インスタンス管理を自前で行っていた箇所が不要になるため、コードが若干すっきりします。</p>
