# Prism.PubSubEventのサンプル
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- Prism
## Topics
- Prism for Windows Runtime
## Updated
- 07/30/2013
## Description

<h1>サンプルプログラムの概要</h1>
<p>Prism for Windows Runtimeが提供するPrism.PubSubEventの動作を確認するサンプルプログラムです。Prism.PubSubEventは、ポータブルライブラリとして提供されているため、Windows Runtimeだけではなく以下のプラットフォームでも動作します。</p>
<ul>
<li>Silverlight 4以上 </li><li>Windows Phone 7以上 </li><li>.NET Framework 4以上 </li></ul>
<h1>Prism.PubSubEventとは</h1>
<p>Prism.PubSubEventは、EventAggregatorという仲介役を介してクラス間でイベントの発行と購読を行うためのライブラリです。通常の.NETのイベントと異なる点は、EventAggregatorを間に仲介することでイベントの発行を行うクラスと、購読を行うクラスの間に直接の依存関係を持たせないような構造にすることが容易だという点があります。<br>
EventAggregatorさえ、共有していれば後からイベントの購読を行うクラスを追加したり、発行を行うクラスを追加したり、購読を一時的に解除したりといったことが簡単にできる点がメリットです。</p>
<p>その他に、Prism.PubSubEventでは、イベントの購読を行うときのスレッドをどこにするかを設定可能です。</p>
<ul>
<li>ThreadOption.PublisherThread<br>
イベントが発行されたスレッドで購読を行います。 </li><li>ThreadOption.UIThread<br>
UIスレッドで購読を行います。 </li><li>ThreadOption.BackgroundThread<br>
バックグラウンドスレッドで購読を行います。 </li></ul>
<p>UIを持ったアプリケーションでよく悩まされる、UIスレッド以外で発生したイベントからUIコントロールにアクセスして例外が発生するといった問題も、イベント購読時にUIスレッドで購読するように設定することで簡単に対応が可能です。</p>
<h2>イベントの発行方法</h2>
<p>Prism.PubSubEventでは、以下のようにしてイベントを発行します。]</p>
<ol>
<li>EventAggregatorのGetEvent&lt;PubSubEvent&lt;TPayload&gt;&gt;()を呼び出してPubSubEvent&lt;T&gt;のインスタンスを取得する
</li><li>PubSubEvent&lt;T&gt;のPublishメソッドで引数にTを渡して呼び出す </li></ol>
<h2>イベントの購読方法</h2>
<p>Prism.PubSubEventでは、以下のようにしてイベントを購読します。</p>
<ol>
<li>EventAggregatorのGetEvent&lt;PubSubEvent&lt;TPayload&gt;&gt;()を呼び出してPubSubEvent&lt;T&gt;のインスタンスを取得する
</li><li>PubSubEvent&lt;T&gt;のSubscribeメソッドで引数にAction&lt;T&gt;を渡してイベントが発行されたときに実行する処理を設定する。
<ul>
<li>Subscribeの引数にThreadOptionを設定することで、イベント購読時のスレッドを設定可能 </li></ul>
</li></ol>
<h1><br>
サンプルプログラムの内容</h1>
<p>サンプルプログラムは、Prism.PubSubEventを.NET Framework 4.5のコンソールアプリケーションで使用しています。サンプルプログラムには以下のクラスが定義されています。</p>
<ul>
<li>TimestampPayload<br>
サンプルプログラム内で発行しているイベントで伝搬する情報を詰めるためのクラス </li><li>TimestampPublisher<br>
EventAggregatorを使ってイベントを発行するクラス </li><li>TimestampSubscriber<br>
EventAggregatorを使ってイベントを購読するクラス </li></ul>
<p>TimestampPublisherクラスとTimestampSubscriberクラスのインスタンスはMainメソッド内で以下のように組み立てています。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">class</span>&nbsp;Program&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;aggregator&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;EventAggregator();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;イベントの発行を行うPublisherを作成</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;pub&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;TimestampPublisher(aggregator);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;イベントの購読を行うSubscriberを作成</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;sub&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;TimestampSubscriber(aggregator);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;イベントの購読開始</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sub.Subscribe();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;イベントを発行</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pub.Publish(<span class="cs__string">&quot;Hello&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pub.Publish(<span class="cs__string">&quot;world&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;イベントの購読解除</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sub.Unsbscribe();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;イベントの発行</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pub.Publish(<span class="cs__string">&quot;こんにちは&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pub.Publish(<span class="cs__string">&quot;世界&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>TimestampPublisherのPublishメソッドでは、以下のようにEventAggregatorからPubSubEvent&lt;T&gt;のインスタンスを取得して、イベントの発行を行っています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;<span class="js__sl_comment">///&nbsp;イベントを発行する</span>&nbsp;
<span class="js__sl_comment">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;Publish(string&nbsp;message)&nbsp;
&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.eventAggregator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetEvent&lt;PubSubEvent&lt;TimestampPayload&gt;&gt;()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Publish(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">new</span>&nbsp;TimestampPayload&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Timestamp&nbsp;=&nbsp;DateTimeOffset.Now,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Message&nbsp;=&nbsp;message&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>TimestampSubscriberクラスのSubscribeメソッドでは、以下のようにEventAggregatorからPubSubEvent&lt;T&gt;クラスのインスタンスを取得してイベントの購読を行っています。あとで、イベントの購読を解除するためにSubscribeメソッドの戻り値のSubscriptionTokenを保存しています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;<span class="cs__com">///&nbsp;まだ、イベントを購読してないときにイベントを購読する</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Subscribe()&nbsp;
&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">this</span>.token&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.token&nbsp;=&nbsp;<span class="cs__keyword">this</span>.eventAggregator.GetEvent&lt;PubSubEvent&lt;TimestampPayload&gt;&gt;()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Subscribe(<span class="cs__keyword">this</span>.PrintTimestampPayload);ThreadOption&nbsp;
&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>最後に、UnsbscribeメソッドでSubscriptionTokenクラスのDisposeメソッドを呼び出して、イベントの購読の解除を行っています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;<span class="js__sl_comment">///&nbsp;イベントの購読を解除する</span>&nbsp;
<span class="js__sl_comment">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;Unsbscribe()&nbsp;
&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__operator">this</span>.token&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.token.Dispose();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.token&nbsp;=&nbsp;null;&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">プログラムを実行すると、前半の&quot;Hello&quot;と&quot;world&quot;はTimestampSubscriberでイベントを購読中なのでコンソールに結果が出力されますが、&quot;こんにちは&quot;と&quot;世界&quot;はコンソールに出力されません。</div>
<p></p>
<h1>まとめ</h1>
<p>このサンプルプログラムでは非常にシンプルですが2つのクラスの間で相互に依存関係を持たないでEventAggregatorを介してイベントの発行と購読を行う例を示しました。そこそこ大きなプログラムになってくると、このような疎結合なイベントの発行と購読の仕組みをうまく取り入れると保守性の高いプログラムを組めると思います。</p>
