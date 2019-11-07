# MVVM入門 その３「ViewModelからViewを操作する」
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- MVVM
## Topics
- MVVM入門
## Updated
- 05/30/2011
## Description

<h1>概要</h1>
<p><a href="http://code.msdn.microsoft.com/MVVM-850a376f" target="_blank">前回</a>までで、基本的な四則演算アプリケーションが出来上がりました。今回は、MVVMパターンをやる上で多くの人が躓くであろうViewModelからViewを操作する方法と、ViewModelからViewを操作した結果をViewModelで受け取る方法について紹介します。</p>
<p>今回のサンプルプログラムは以下のような動きをします。</p>
<p>普通に計算出来るときは今までと同じ動き<br>
<img src="22759-ws000006.jpg" alt="" width="299" height="166"></p>
<p>右辺値を0に指定して割り算を実行すると、計算結果が実数ではなくなったとダイアログが表示されます。<br>
<img src="22760-ws000007.jpg" alt="" width="414" height="343"></p>
<p>キャンセルを押すと、四則演算アプリケーションはそのままですが、OKを押すと以下のように入力内容が初期化されます。<br>
<img src="22761-ws000008.jpg" alt="" width="302" height="165"></p>
<h1>ViewからViewModelを操作する方法</h1>
<p>MVVMパターンが世に登場してから、この方法を実現するためにこれまで様々な方法が試行錯誤されてきました。今ではMessengerと呼ばれるクラスを使いViewModelからViewにMessageを送信して、ViewではTriggerでMessageを受信して任意のTriggerActionを実行するという方法がよく使われています。ViewのActionで処理が終わるとMessageと一緒に通知されているViewModelのコールバックの処理を呼び出してTriggerActionの実行結果がViewModelに戻されます。</p>
<p>ここでは、MessengerクラスとMessageクラスとMessageに反応するMessageTriggerクラスと確認ダイアログを出すConfirmActionクラスを作成して、この四則演算アプリケーションに組み込む方法を説明します。BehaviorやTrigger&amp;TriggerActionについてご存じない方は、先に以下のサンプルを見てBehaviorの定義方法などを確認してください。</p>
<ul>
<li><a href="http://code.msdn.microsoft.com/Behavior-beae13a6" target="_blank">Behavior入門</a>
</li></ul>
<h1>Messenger関連クラスの作成</h1>
<p>Messenger関連のクラスのコードを以下に示します。</p>
<h2>Messageクラス</h2>
<p>まず、ViewModelとViewの間の情報の伝播を行うMessageクラスを作成します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace MVVMCalc.Common
{
    /// &lt;summary&gt;
    /// ViewModelとViewの間での情報のやり取りを行うメッセージ
    /// &lt;/summary&gt;
    public class Message
    {
        /// &lt;summary&gt;
        /// メッセージの本体
        /// &lt;/summary&gt;
        public object Body { get; private set; }

        /// &lt;summary&gt;
        /// ViewからViewModelへのメッセージのレスポンス
        /// &lt;/summary&gt;
        public object Response { get; set; }

        /// &lt;summary&gt;
        /// Bodyを指定してMessageを作成する
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;body&quot;&gt;&lt;/param&gt;
        public Message(object body)
        {
            this.Body = body;
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;MVVMCalc.Common&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;ViewModelとViewの間での情報のやり取りを行うメッセージ</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Message&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;メッセージの本体</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">object</span>&nbsp;Body&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;ViewからViewModelへのメッセージのレスポンス</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">object</span>&nbsp;Response&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Bodyを指定してMessageを作成する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;body&quot;&gt;&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Message(<span class="cs__keyword">object</span>&nbsp;body)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Body&nbsp;=&nbsp;body;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;このクラスは、ViewModelからViewへ通知する情報としてBodyプロパティを、ViewからViewModelへの結果の情報としてResponseプロパティを定義しています。このクラスをViewとViewModel間のデータの受け渡しのインターフェースとして利用します。</div>
<div class="endscriptcode"></div>
<h2 class="endscriptcode">Messengerクラス</h2>
<p>次に、上記で定義したMessageクラスをViewに送信するためのMessengerクラスを作成します。送信するといっても実態はMessageをくるんだイベント引数を渡すイベントを発行するだけです。このイベントをTriggerで購読してViewの処理の契機として使用します。まず、以下のようなMessageとコールバックを受け取るイベント引数を定義します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace MVVMCalc.Common
{
    using System;

    /// &lt;summary&gt;
    /// Messengerの通知イベント用のイベント引数
    /// &lt;/summary&gt;
    public class MessageEventArgs : EventArgs
    {
        /// &lt;summary&gt;
        /// 送信するメッセージ
        /// &lt;/summary&gt;
        public Message Message { get; private set; }

        /// &lt;summary&gt;
        /// ViewModelのコールバック
        /// &lt;/summary&gt;
        public Action&lt;Message&gt; Callback { get; private set; }

        /// &lt;summary&gt;
        /// メッセージとコールバックを指定してイベント引数を作成する
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;message&quot;&gt;メッセージ&lt;/param&gt;
        /// &lt;param name=&quot;callback&quot;&gt;コールバック&lt;/param&gt;
        public MessageEventArgs(Message message, Action&lt;Message&gt; callback)
        {
            this.Message = message;
            this.Callback = callback;
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;MVVMCalc.Common&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Messengerの通知イベント用のイベント引数</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MessageEventArgs&nbsp;:&nbsp;EventArgs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;送信するメッセージ</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Message&nbsp;Message&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;ViewModelのコールバック</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Action&lt;Message&gt;&nbsp;Callback&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;メッセージとコールバックを指定してイベント引数を作成する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;message&quot;&gt;メッセージ&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;callback&quot;&gt;コールバック&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;MessageEventArgs(Message&nbsp;message,&nbsp;Action&lt;Message&gt;&nbsp;callback)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Message&nbsp;=&nbsp;message;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Callback&nbsp;=&nbsp;callback;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">次に、このイベント引数を持つイベントを発行するMessengerクラスを定義します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace MVVMCalc.Common
{
    using System;

    /// &lt;summary&gt;
    /// Messageを送信するクラス。
    /// &lt;/summary&gt;
    public class Messenger
    {
        /// &lt;summary&gt;
        /// メッセージが送信されたことを通知するイベント
        /// &lt;/summary&gt;
        public event EventHandler&lt;MessageEventArgs&gt; Raised;

        /// &lt;summary&gt;
        /// 指定したメッセージとコールバックでメッセージを送信する
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;message&quot;&gt;メッセージ&lt;/param&gt;
        /// &lt;param name=&quot;callback&quot;&gt;コールバック&lt;/param&gt;
        public void Raise(Message message, Action&lt;Message&gt; callback)
        {
            var h = this.Raised;
            if (h != null)
            {
                h(this, new MessageEventArgs(message, callback));
            }
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;MVVMCalc.Common&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Messageを送信するクラス。</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Messenger&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;メッセージが送信されたことを通知するイベント</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">event</span>&nbsp;EventHandler&lt;MessageEventArgs&gt;&nbsp;Raised;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;指定したメッセージとコールバックでメッセージを送信する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;message&quot;&gt;メッセージ&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;callback&quot;&gt;コールバック&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Raise(Message&nbsp;message,&nbsp;Action&lt;Message&gt;&nbsp;callback)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;h&nbsp;=&nbsp;<span class="cs__keyword">this</span>.Raised;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(h&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;h(<span class="cs__keyword">this</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;MessageEventArgs(message,&nbsp;callback));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">以上で、Messengerクラスの定義は終わりです。主にこれらのクラスはViewModel側で使用します。仕組み自体は、イベントを発行しているだけなのでC#の一般的な知識があれば難しくないコードだと思います。</div>
<div class="endscriptcode"></div>
<h2 class="endscriptcode">Trigger&amp;TriggerActionの実装</h2>
<p>次に、View側での処理を受け取るTriggerと処理を実行するTriggerActionを定義します。Triggerは、EventTriggerを継承してRaisedイベントを購読するように指定します。このように、純粋にイベントを購読するだけのTriggerはEventTriggerのGetEventNameメソッドをオーバーライドするだけで定義できるようになっています。以下にコードを示します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace MVVMCalc.Common
{
    using System.Windows.Interactivity;

    /// &lt;summary&gt;
    /// MessengerのRaisedイベントを受信するトリガー
    /// &lt;/summary&gt;
    public class MessageTrigger : EventTrigger
    {
        protected override string GetEventName()
        {
            return &quot;Raised&quot;;
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;MVVMCalc.Common&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Windows.Interactivity;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;MessengerのRaisedイベントを受信するトリガー</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MessageTrigger&nbsp;:&nbsp;EventTrigger&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;GetEventName()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__string">&quot;Raised&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>Triggerが出来たので、TriggerActionを作成します。ここでは、四則演算アプリケーションの確認ダイアログを表示するアクションを作成します。動きとしてはViewModelから指定されたメッセージを表示し、ユーザがOKを押した場合はtrueを、キャンセルを押した場合はfalseをMessageのResponseプロパティに設定してコールバックを呼びます。コードを以下に示します。</p>
<p>&nbsp;</p>
<h1 class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace MVVMCalc.Common
{
    using System.Windows;
    using System.Windows.Interactivity;

    /// &lt;summary&gt;
    /// 確認ダイアログを表示するアクション
    /// &lt;/summary&gt;
    public class ConfirmAction : TriggerAction&lt;DependencyObject&gt;
    {
        protected override void Invoke(object parameter)
        {
            // MessageEventArgs意外の場合は何もしない
            var args = parameter as MessageEventArgs;
            if (args == null)
            {
                return;
            }

            // メッセージボックスを表示して
            var result = MessageBox.Show(
                args.Message.Body.ToString(), 
                &quot;確認&quot;, 
                MessageBoxButton.OKCancel);

            // ボタンの押された結果をResponseに&#26684;納して
            args.Message.Response = result == MessageBoxResult.OK;
            // コールバックを呼ぶ
            args.Callback(args.Message);
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;MVVMCalc.Common&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Windows;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Windows.Interactivity;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;確認ダイアログを表示するアクション</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ConfirmAction&nbsp;:&nbsp;TriggerAction&lt;DependencyObject&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Invoke(<span class="cs__keyword">object</span>&nbsp;parameter)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;MessageEventArgs意外の場合は何もしない</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;args&nbsp;=&nbsp;parameter&nbsp;<span class="cs__keyword">as</span>&nbsp;MessageEventArgs;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(args&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;メッセージボックスを表示して</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;result&nbsp;=&nbsp;MessageBox.Show(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;args.Message.Body.ToString(),&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;確認&quot;</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBoxButton.OKCancel);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;ボタンの押された結果をResponseに&#26684;納して</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;args.Message.Response&nbsp;=&nbsp;result&nbsp;==&nbsp;MessageBoxResult.OK;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;コールバックを呼ぶ</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;args.Callback(args.Message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</h1>
<p>&nbsp;</p>
<h1>四則演算アプリケーションの改造</h1>
<p>基本となるクラスが出来たので、第二回までのアプリケーションを改造していきます。ここでは重要なコードを抜き出して解説しています。完全なコードはサンプルプログラムをダウンロードして確認してください。</p>
<p>まず、MainViewModelクラスにViewModelからViewへ通知を行うためのMessenger型のプロパティを定義します。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">private Messenger errorMessenger = new Messenger();


/// &lt;summary&gt;
/// 計算結果にエラーがあったことを通知するメッセージを送信するメッセンジャーを取得する。
/// &lt;/summary&gt;
public Messenger ErrorMessenger
{
    get
    {
        return this.errorMessenger;
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;Messenger&nbsp;errorMessenger&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Messenger();&nbsp;
&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;計算結果にエラーがあったことを通知するメッセージを送信するメッセンジャーを取得する。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;Messenger&nbsp;ErrorMessenger&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.errorMessenger;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>そして、計算処理でdoubleがNaNやInfinityになった場合にユーザにダイアログを表示するようにMessageを通知します。コールバックでは、MessageのResponseを見て処理内容を分岐しています。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">/// &lt;summary&gt;
/// 計算処理のコマンドの実行を行います。
/// &lt;/summary&gt;
private void CalculateExecute()
{
    // 現在の入力値を元に計算を行う
    var calc = new Calculator();
    this.Answer = calc.Execute(
        double.Parse(this.Lhs), 
        double.Parse(this.Rhs), 
        this.SelectedCalculateType.CalculateType);

    if (IsInvalidAnswer())
    {
        // 計算結果が実数の範囲から外れてる場合はViewに通知する
        this.ErrorMessenger.Raise(
            new Message(&quot;計算結果が実数の範囲を超えました。入力値を初期化しますか？&quot;),
            m =&gt;
            {
                // Viewから入力を初期化すると指定された場合はプロパティの初期化を行う
                if (!(bool)m.Response)
                {
                    return;
                }

                InitializeProperties();
            });
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;計算処理のコマンドの実行を行います。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;CalculateExecute()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;現在の入力値を元に計算を行う</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;calc&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Calculator();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Answer&nbsp;=&nbsp;calc.Execute(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>.Parse(<span class="cs__keyword">this</span>.Lhs),&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>.Parse(<span class="cs__keyword">this</span>.Rhs),&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SelectedCalculateType.CalculateType);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(IsInvalidAnswer())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;計算結果が実数の範囲から外れてる場合はViewに通知する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.ErrorMessenger.Raise(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Message(<span class="cs__string">&quot;計算結果が実数の範囲を超えました。入力値を初期化しますか？&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Viewから入力を初期化すると指定された場合はプロパティの初期化を行う</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!(<span class="cs__keyword">bool</span>)m.Response)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeProperties();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;</pre>
</div>
</div>
</div>
<p></p>
<p>IsInvalidAnswerメソッドは、doubleがNaNかInfinityになっているか確認するメソッドです。今回のポイントはthis.ErrorMessage.Raiseメソッドを呼び出している部分になります。ここで先ほど定義したクラスを使ってViewにMessageを通知しています。</p>
<h2>Viewの改造</h2>
<p>次に、ViewのXAMLを変更します。Viewでは、先ほど作成したTriggerを使用しています。MessageTriggerのSourceObjectは、ViewModelのErrorMessengerプロパティをバインドします。これでViewModelのErrorMessengerプロパティから送信されるMessageを受信することができるようになります。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;i:Interaction.Triggers&gt;
    &lt;common:MessageTrigger SourceObject=&quot;{Binding Path=ErrorMessenger}&quot;&gt;
        &lt;common:ConfirmAction /&gt;
    &lt;/common:MessageTrigger&gt;
&lt;/i:Interaction.Triggers&gt;
</pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;i</span>:Interaction.Triggers<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;common</span>:MessageTrigger&nbsp;<span class="xaml__attr_name">SourceObject</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;Path=ErrorMessenger}&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;common</span>:ConfirmAction&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/common:MessageTrigger&gt;</span>&nbsp;
<span class="xaml__tag_end">&lt;/i:Interaction.Triggers&gt;</span>&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>以上で、コードのポイントは終わりです。</p>
<h1>まとめ</h1>
<p>これまで、3回に渡ってMVVMパターンで使用する基本的なクラス（主に各種MVVM対応をうたっているクラスライブラリで用意されています）を作成しながら、簡単な四則演算アプリケーションを作成しました。世に出回ってるライブラリでは、ここで作成したクラスよりも多くの基本クラスや、メモリリークなどへの対処など、多くの先人がはまってきた落とし穴を回避するためのノウハウが満載です。</p>
<p>この三回ではすべて自作してきましたが、内部実装をイメージしてもらうためにやったもので、普通は何かしらのライブラリを採用するのが良いと思います。次回以降では、この四則演算アプリケーションを、何らかのMVVMをサポートするライブラリを使用して書き直してみようと思います。</p>
<p>&nbsp;</p>
