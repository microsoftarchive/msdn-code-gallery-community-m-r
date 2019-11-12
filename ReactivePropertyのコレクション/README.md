# ReactivePropertyのコレクション
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- ReactiveProperty
## Topics
- ReactiveProperty
## Updated
- 10/18/2014
## Description

<h1>サンプルプログラムの概要</h1>
<p>このサンプルプログラムは、ReactivePropertyのReadOnlyReactiveCollectionの使い方についてしめします。ReadOnlyReactiveCollectionを使うと、ModelにあるIObservable&lt;T&gt;やObservableCollection&lt;T&gt;から簡単に読み取り専用のCollectionを作成することができます。</p>
<h1>サンプルプログラムの動作方法</h1>
<p>サンプルプログラムを動作させるには、ソリューションのNuGetパッケージの復元を有効化してリビルドをしてください。リビルド後実行することで動作を確認できます。</p>
<h1>サンプルプログラムの解説</h1>
<p>サンプルプログラムでは、以下の方法でReadOnlyReactiveCollection&lt;T&gt;型を作成しています。</p>
<ul>
<li>IObservable&lt;T&gt;から作成 </li><li>IObservable&lt;CollectoionChanged&lt;T&gt;&gt;から作成 </li><li>ObservableCollection&lt;T&lt;&gt;から作成 </li></ul>
<h2>IObservable&lt;T&gt;から作成</h2>
<p>一番シンプルな作成方法です。IObservable&lt;T&gt;の拡張メソッドのToReadOnlyReactiveCollectionを呼び出すことで作成できます。もとになるIObservable&lt;T&gt;から値が発行されるたびにコレクションに値が追加されます。ToReadOnlyReactiveCollectionの引数にIObservable&lt;Unit&gt;を渡すことで、コレクションの値をリセットするタイミングを指定することもできます。</p>
<p>コード例を以下に示します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;IO&lt;T&gt;からReacOnlyReactiveCollection&lt;T&gt;</span>&nbsp;
<span class="cs__keyword">private</span>&nbsp;Subject&lt;<span class="cs__keyword">int</span>&gt;&nbsp;source&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Subject&lt;<span class="cs__keyword">int</span>&gt;();&nbsp;
&nbsp;
<span class="cs__keyword">public</span>&nbsp;ReadOnlyReactiveCollection&lt;<span class="cs__keyword">int</span>&gt;&nbsp;SimpleCollection&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
<span class="cs__keyword">public</span>&nbsp;ReactiveCommand&nbsp;SimpleAddCommand&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
<span class="cs__keyword">public</span>&nbsp;ReactiveCommand&nbsp;SimpleResetCommand&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__operator">this</span>.SimpleAddCommand&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="js__operator">this</span>.SimpleAddCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.source.OnNext(random.Next());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
<span class="js__operator">this</span>.SimpleResetCommand&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ReactiveCommand();&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;IObservaboe&lt;T&gt;からコレクションへ変換。オプションとしてコレクションをリセットするIO&lt;Unit&gt;を渡せる</span>&nbsp;
<span class="js__operator">this</span>.SimpleCollection&nbsp;=&nbsp;<span class="js__operator">this</span>.source.ToReadOnlyReactiveCollection(<span class="js__operator">this</span>.SimpleResetCommand.ToUnit());&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>これで、SimpleAddCommandが実行されるたびにランダムな数字がコレクションに追加され、SimpleResetCommandが実行されると、コレクションがクリアされます。</p>
<h2>IObservable&lt;CollectoionChanged&lt;T&gt;&gt;から作成</h2>
<p>次に、コレクションの変更通知を笑わす、IObservable&lt;CollectionChanged&lt;T&gt;&gt;から作成する方法をしめします。CollectionChanged&lt;T&gt;は、int型のIndexプロパティと、NotifyCollectionChangedAction型のActionプロパティとT型のValueプロパティをもつシンプルな型です。Add, Reset, Replace, Removeを簡単に作成できるようにファクトリメソッドが定義されています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;CollectionChanged&nbsp;action</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;typeparam&nbsp;name=&quot;T&quot;&gt;&lt;/typeparam&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;CollectionChanged&lt;T&gt;&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Reset&nbsp;action</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;CollectionChanged&lt;T&gt;&nbsp;Reset&nbsp;;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Create&nbsp;Remove&nbsp;action</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;index&quot;&gt;&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;returns&gt;&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;CollectionChanged&lt;T&gt;&nbsp;Remove(<span class="cs__keyword">int</span>&nbsp;index);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Create&nbsp;add&nbsp;action</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;index&quot;&gt;&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;value&quot;&gt;&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;returns&gt;&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;CollectionChanged&lt;T&gt;&nbsp;Add(<span class="cs__keyword">int</span>&nbsp;index,&nbsp;T&nbsp;<span class="cs__keyword">value</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Create&nbsp;replace&nbsp;action</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;index&quot;&gt;&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;value&quot;&gt;&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;returns&gt;&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;CollectionChanged&lt;T&gt;&nbsp;Replace(<span class="cs__keyword">int</span>&nbsp;index,&nbsp;T&nbsp;<span class="cs__keyword">value</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;T&nbsp;Value&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Index&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Support&nbsp;action&nbsp;is&nbsp;Add&nbsp;and&nbsp;Remove&nbsp;and&nbsp;Reset&nbsp;and&nbsp;Replace.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;NotifyCollectionChangedAction&nbsp;Action&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>この型を使ったコレクションの作成例を以下に示します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;IO&lt;CollectionChanged&lt;T&gt;&gt;からReadOnlyReactiveCollection&lt;T&gt;</span>&nbsp;
private&nbsp;Subject&lt;CollectionChanged&lt;int&gt;&gt;&nbsp;collectionChangedSource&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;Subject&lt;CollectionChanged&lt;int&gt;&gt;();&nbsp;
&nbsp;
public&nbsp;ReadOnlyReactiveCollection&lt;int&gt;&nbsp;CollectionChangedCollection&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;
public&nbsp;ReactiveCommand&nbsp;CollectionChangedAddCommand&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
public&nbsp;ReactiveCommand&nbsp;CollectionChangedRemoveCommand&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
public&nbsp;ReactiveCommand&nbsp;CollectionChangedClearCommand&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">this</span>.CollectionChangedCollection&nbsp;=&nbsp;<span class="cs__keyword">this</span>.collectionChangedSource.ToReadOnlyReactiveCollection();&nbsp;
<span class="cs__keyword">this</span>.CollectionChangedAddCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.CollectionChangedAddCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.collectionChangedSource.OnNext(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CollectionChanged&lt;<span class="cs__keyword">int</span>&gt;.Add(<span class="cs__number">0</span>,&nbsp;random.Next()));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;
<span class="cs__keyword">this</span>.CollectionChangedClearCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.CollectionChangedClearCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.collectionChangedSource.OnNext(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CollectionChanged&lt;<span class="cs__keyword">int</span>&gt;.Reset);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;
<span class="cs__keyword">this</span>.CollectionChangedRemoveCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.CollectionChangedRemoveCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.collectionChangedSource.OnNext(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CollectionChanged&lt;<span class="cs__keyword">int</span>&gt;.Remove(<span class="cs__number">0</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>この方法は、一番コード量は多くなりますが、一番細かくコレクションの状態を操作できる方法になります。</p>
<h2>ObservableCollection&lt;T&gt;から作成</h2>
<p>MVVMパターンで作成していると、すでにModel側にObservableCollection&lt;T&gt;型があって、それをもとにVMで表示するためのコレクションを作成することが多いと思います。そういうケースに対応するため、ObservableCollection&lt;T&gt;からReadOnlyReactiveCollection＜U&gt;を作成する方法を提供しています。コード例を以下に示します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;ObservableCollection&lt;T&gt;からReadOnlyObservableCollection&lt;T&gt;</span>&nbsp;
private&nbsp;ObservableCollection&lt;string&gt;&nbsp;sourceCollection&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ObservableCollection&lt;string&gt;();&nbsp;
public&nbsp;ReadOnlyReactiveCollection&lt;string&gt;&nbsp;ObservableCollectionToReadOnlyReactiveCollection&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
public&nbsp;ReactiveCommand&nbsp;SourceCollectionAddCommand&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
public&nbsp;ReactiveCommand&nbsp;SourceCollectionRemoveCommand&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
public&nbsp;ReactiveCommand&nbsp;SourceCollectionResetCommand&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;private&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;ObservableCollection&lt;T&gt;からReadOnlyObservableCollection&lt;U&gt;への変換</span>&nbsp;
<span class="cs__keyword">this</span>.ObservableCollectionToReadOnlyReactiveCollection&nbsp;=&nbsp;<span class="cs__keyword">this</span>.sourceCollection&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReadOnlyReactiveCollection(x&nbsp;=&gt;&nbsp;x&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;value.&quot;</span>);&nbsp;
&nbsp;
<span class="cs__keyword">this</span>.SourceCollectionAddCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.SourceCollectionAddCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.sourceCollection.Add(random.Next().ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
<span class="cs__keyword">this</span>.SourceCollectionRemoveCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.SourceCollectionRemoveCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.sourceCollection.RemoveAt(<span class="cs__keyword">this</span>.sourceCollection.Count&nbsp;-&nbsp;<span class="cs__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
<span class="cs__keyword">this</span>.SourceCollectionResetCommand&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.SourceCollectionResetCommand.Subscribe(_&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.sourceCollection.Clear();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
ToReadOnlyReactiveCollection拡張メソッドに、T型からU型へ変換するラムダ式を渡すことで、ObservableCollection&lt;T&gt;の変更に追従するReadOnlyReactiveCollection＜U&gt;が作成できます。
<p></p>
