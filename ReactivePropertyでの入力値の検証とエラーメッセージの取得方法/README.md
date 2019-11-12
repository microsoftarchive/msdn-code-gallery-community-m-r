# ReactivePropertyでの入力値の検証とエラーメッセージの取得方法
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- Reactive Extensions
- ReactiveProperty
## Topics
- Data Validation
## Updated
- 10/12/2014
## Description

<h1>サンプルプログラムの概要</h1>
<p>このサンプルプログラムは、ReactivePropertyの入力値の検&#35388;方法と、検&#35388;エラーが出たときにエラーメッセージを取得する方法や、エラーがなくなったときに押せるボタンのためのCommandの作り方を示しています。</p>
<h1>サンプルプログラムの実行方法</h1>
<p>ソリューションでNuGetパッケージの復元を有効化してリビルドしてから実行してください。</p>
<h1>サンプルプログラムの解説</h1>
<p>ReactivePropertyには、大きく分けて2通りの入力値の検&#35388;方法を提供しています。1つがSystem.ComponentModel.DataAnnotationsを使ったもので、2つ目が、ラムダ式によるエラーの検&#35388;です。DataAnnotationsによるエラーの検&#35388;はWindows Phoneではサポートしていないため、対象のプラットフォームをターゲットとする場合は、ラムダ式による入力値の検&#35388;のみが使えます。</p>
<h2>DataAnnotationsによる入力値の検&#35388;</h2>
<p>サンプルプログラムのMainPageViewModelクラスのAttrValidationプロパティがDataAnnotationsによる入力値の検&#35388;方法を示しています。DataAnnotationsによる入力値の検&#35388;を行うには2つのステップを踏む必要があります。1つ目が、ReactiveProperty型のプロパティにDataAnnotationsの属性をつけることです。コードを以下に示します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">[Required(ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;その１&nbsp;入力してください&quot;</span>)]&nbsp;
<span class="cs__keyword">public</span>&nbsp;ReactiveProperty&lt;<span class="cs__keyword">string</span>&gt;&nbsp;AttrValidation&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>2つ目のステップは、ReactivePropertyをインスタンス化したあとで、属性の検&#35388;を有効化するようにSetValidateAttributeメソッドを呼び出すことです。コードを以下に示します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;属性による入力値の検&#35388;</span>&nbsp;
<span class="cs__keyword">this</span>.AttrValidation&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveProperty&lt;<span class="cs__keyword">string</span>&gt;()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.SetValidateAttribute(()&nbsp;=&gt;&nbsp;<span class="cs__keyword">this</span>.AttrValidation);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h2>ラムダ式による入力値の検&#35388;</h2>
<p>ラムダ式による入力値の検&#35388;を有効にするには、ReactivePropertyをインスタンス化するところで、SetValidateNotifyErrorを呼び出して検&#35388;ロジックを設定します。コードを以下に示します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;ReactiveProperty&lt;<span class="cs__keyword">string</span>&gt;&nbsp;NotifyValidation&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;ラムダ式による入力値の検&#35388;</span>&nbsp;
<span class="cs__keyword">this</span>.NotifyValidation&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveProperty&lt;<span class="cs__keyword">string</span>&gt;()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.SetValidateNotifyError(x&nbsp;=&gt;&nbsp;<span class="cs__keyword">string</span>.IsNullOrEmpty(x)&nbsp;?&nbsp;<span class="cs__string">&quot;その２&nbsp;入力してください&quot;</span>&nbsp;:&nbsp;<span class="cs__keyword">null</span>);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>プロパティに設定された値を受け取り、エラーがなければnullを返し、エラーがあればエラーのメッセージを返します。このオーバーライドの他に非同期にエラーの検&#35388;を行うTaskを返すメソッドなどがあります。</p>
<h2>エラーがなくなった時に押せるコマンド</h2>
<p>ReactiveProperty v1.0.3.0から、ReactiveProperty&lt;T&gt;がエラーがなくなったときにTrueを流すObserveHasNoErrorプロパティが追加されました。これと、Codeplex.Reactive.Extensions名前空間で定義されているCombineLatestValuesAreAllTrueメソッドを使うことで簡単にすべての入力にエラーがなくなったときに押せるコマンドを作成することができます。コード例を以下に示します。</p>
<h2 class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;エラーがなくなった時に押せるコマンド</span>&nbsp;
<span class="cs__keyword">this</span>.AlertCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>[]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;エラーがあるときにになるObserveHasErrorを束ねて</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.AttrValidation.ObserveHasError,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.NotifyValidation.ObserveHasError&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;全てFalseだったら</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.CombineLatestValuesAreAllFalse()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;コマンドに変換</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveCommand();&nbsp;
</pre>
</div>
</div>
</h2>
<h2>&nbsp;エラーメッセージの取得方法</h2>
<p>ReactivePropertyで発生したエラーメッセージを取得するにはObserveErrorChangedかObserveErrors(v1.0.3.0で追加)を使ってエラー情報を監視して取得します。前者は、Subscribeしたあとの値を流すのに対して、後者は、Subscribeした時に、最後の状態を最初に流す点が異なります。ObserveErrorsメソッドを使うことで、ReactivePropertyのエラー情報できるので、これをCombineLatestで合成して加工することで、全てのエラーメッセージを列挙したり、今あるエラーの中から1つだけ取り出して表示するといったことが可能になります。</p>
<p>コード例を以下に示します。</p>
<p>&nbsp;</p>
<h2 class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;とりあえず最初になおしてほしいエラーを出す</span>&nbsp;
<span class="cs__keyword">this</span>.ErrorMessage&nbsp;=&nbsp;<span class="cs__keyword">new</span>[]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;エラーを束ねて</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.AttrValidation.ObserveErrorChanged,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.NotifyValidation.ObserveErrorChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.CombineLatest(x&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;null(エラーなし）を省いて</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;r&nbsp;=&nbsp;x.Where(y&nbsp;=&gt;&nbsp;y&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;最初のエラーを返す</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Select(y&nbsp;=&gt;&nbsp;y.OfType&lt;<span class="cs__keyword">string</span>&gt;())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault(y&nbsp;=&gt;&nbsp;y.Any());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;無ければ無し、エラーがあれば最初のものを返す</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;r&nbsp;==&nbsp;<span class="cs__keyword">null</span>&nbsp;?&nbsp;<span class="cs__keyword">null</span>&nbsp;:&nbsp;r.FirstOrDefault();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveProperty();&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;全部のエラーを出す</span>&nbsp;
<span class="cs__keyword">this</span>.ErrorMessages&nbsp;=&nbsp;<span class="cs__keyword">new</span>[]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;エラーを束ねて</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.AttrValidation.ObserveErrorChanged,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.NotifyValidation.ObserveErrorChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.CombineLatest(x&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;null(エラーなし)を省いて</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;x.Where(y&nbsp;=&gt;&nbsp;y&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;IE&lt;string&gt;を平らに慣らす</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.SelectMany(y&nbsp;=&gt;&nbsp;y.OfType&lt;<span class="cs__keyword">string</span>&gt;());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveProperty();&nbsp;
</pre>
</div>
</div>
</h2>
<p>&nbsp;</p>
<p>&nbsp;このデータをバインドすることで、画面にエラー情報を出力することができます。</p>
