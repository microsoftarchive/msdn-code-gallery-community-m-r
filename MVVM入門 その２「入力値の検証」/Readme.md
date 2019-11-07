# MVVM入門 その２「入力値の検証」
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- WPF
- ViewModel pattern (MVVM)
## Topics
- MVVM入門
## Updated
- 11/19/2012
## Description

<h1>概要</h1>
<p>この記事は、<a href="http://code.msdn.microsoft.com/MVVM-d8261534" target="_blank">MVVM入門 その１「シンプル四則演算アプリケーションの作成」</a>の続きにあたります。その１を見ていない方は、そちらを見てから、この記事を見てください。</p>
<p>この回では、その１で作成したシンプル四則演算アプリケーションのテキストボックスに入力値の妥当性検&#35388;を実装します。その１で作成した電卓は、テキストボックスに数字以外を入力することが出来ました。しかも、その状態で計算実行ボタンを押すことが出来て、計算結果も正しくないというお粗末なものでした。今回は、以下のように正しくない値（ここでは数字でない値）を入力した場合に、ユーザに視覚的にフィードバックをし、エラーの内容をツールチップで表示するように四則演算アプリケーションを改造します。</p>
<p><img src="22320-ws000000.jpg" alt="" width="298" height="165"></p>
<h1>このサンプルで学べること</h1>
<p>このサンプルでは、以下のことが学べます。</p>
<ul>
<li>IDataErrorInfoインターフェースを使った入力値の妥当性検&#35388; </li><li>IDataErrorInfoのエラー内容を画面にフィードバックする方法 </li></ul>
<h1>サンプルアプリケーションの作成</h1>
<p>ここでは、サンプルアプリケーションの作成方法を示します。その１からダウンロードできる四則演算アプリケーションをベースに作業を進めていきます。</p>
<h2>ViewModelBaseクラスの拡張</h2>
<p>まず、ViewModelBaseクラスに入力値の検&#35388;機能を追加するためにIDataErrorInfoインターフェースを実装します。IDataErrorInfoインターフェースはErrorプロパティとインデクサが定義されています。通常使うのはインデクサなので、Errorプロパティはここでは実装しません。インデクサでは、引数で渡された名前のプロパティのエラー情報を返します。</p>
<p>ViewModelBaseにIDataErrorInfoインターフェースを実装して、以下のコードを追加します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">/// &lt;summary&gt;
/// プロパティに紐づいたエラーメッセージを&#26684;納します。
/// &lt;/summary&gt;
private Dictionary&lt;string, string&gt; errors = new Dictionary&lt;string, string&gt;();


/// &lt;summary&gt;
/// columnNameで指定したプロパティのエラーを返します。
/// &lt;/summary&gt;
/// &lt;param name=&quot;columnName&quot;&gt;プロパティ名&lt;/param&gt;
/// &lt;returns&gt;エラーメッセージ&lt;/returns&gt;
public string this[string columnName]
{
    get 
    {
        if (this.errors.ContainsKey(columnName))
        {
            return this.errors[columnName];
        }

        return null;
    }
}

/// &lt;summary&gt;
/// プロパティにエラーメッセージを設定します。
/// &lt;/summary&gt;
/// &lt;param name=&quot;propertyName&quot;&gt;プロパティ名&lt;/param&gt;
/// &lt;param name=&quot;errorMessage&quot;&gt;エラーメッセージ&lt;/param&gt;
protected void SetError(string propertyName, string errorMessage)
{
    this.errors[propertyName] = errorMessage;
    this.RaisePropertyChanged(&quot;HasError&quot;);
}

/// &lt;summary&gt;
/// プロパティのエラーをクリアします。
/// &lt;/summary&gt;
/// &lt;param name=&quot;propertyName&quot;&gt;プロパティ名&lt;/param&gt;
protected void ClearError(string propertyName)
{
    if (this.errors.ContainsKey(propertyName))
    {
        this.errors.Remove(propertyName);
        this.RaisePropertyChanged(&quot;HasError&quot;);
    }
}

/// &lt;summary&gt;
/// 全てのエラーをクリアします。
/// &lt;/summary&gt;
protected void ClearErrors()
{
    this.errors.Clear();
    this.RaisePropertyChanged(&quot;HasError&quot;);
}

/// &lt;summary&gt;
/// エラーの有無を取得します。
/// &lt;/summary&gt;
public bool HasError
{
    get
    {
        return this.errors.Count != 0;
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;プロパティに紐づいたエラーメッセージを&#26684;納します。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">private</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;errors&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;columnNameで指定したプロパティのエラーを返します。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;columnName&quot;&gt;プロパティ名&lt;/param&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;returns&gt;エラーメッセージ&lt;/returns&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;<span class="cs__keyword">this</span>[<span class="cs__keyword">string</span>&nbsp;columnName]&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">this</span>.errors.ContainsKey(columnName))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.errors[columnName];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;プロパティにエラーメッセージを設定します。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;propertyName&quot;&gt;プロパティ名&lt;/param&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;errorMessage&quot;&gt;エラーメッセージ&lt;/param&gt;</span>&nbsp;
<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;SetError(<span class="cs__keyword">string</span>&nbsp;propertyName,&nbsp;<span class="cs__keyword">string</span>&nbsp;errorMessage)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.errors[propertyName]&nbsp;=&nbsp;errorMessage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.RaisePropertyChanged(<span class="cs__string">&quot;HasError&quot;</span>);&nbsp;
}&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;プロパティのエラーをクリアします。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;propertyName&quot;&gt;プロパティ名&lt;/param&gt;</span>&nbsp;
<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ClearError(<span class="cs__keyword">string</span>&nbsp;propertyName)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">this</span>.errors.ContainsKey(propertyName))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.errors.Remove(propertyName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.RaisePropertyChanged(<span class="cs__string">&quot;HasError&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;全てのエラーをクリアします。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ClearErrors()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.errors.Clear();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.RaisePropertyChanged(<span class="cs__string">&quot;HasError&quot;</span>);&nbsp;
}&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;エラーの有無を取得します。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;HasError&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.errors.Count&nbsp;!=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;内部でDictionary&lt;string, string&gt;を用いてプロパティ名に紐づくエラーメッセージを管理しています。インデクサでは、そのDictionary&lt;string, string&gt;からエラーメッセージを返しています。その他のSetErrorとClearErrorとClearErrorsメソッドは、ViewModelBaseを継承した先のクラスでDictionaryを直接使わなくてもいいように用意したユーテリティメソッドです。それぞれ、プロパティに対するエラーの設定、クリア、全てのエラーをクリアさせる機能を提供しています。</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">その他に、ViewModelにエラーが有るか無いかを確認するためのHasErrorプロパティを定義しています。</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:15px; font-weight:bold">MainViewModelの改造</span></div>
<p>次に、MainViewModelを上記で作成した機能を使って改造します。まず、テキストボックスからの入力値を受け取るLhsプロパティとRhsプロパティを、どのような入力値でも受け入れるようにstring型に変更します。そして、プロパティのset内で受け取った値が期待する値かどうか判定して、エラーがあった場合はエラーメッセージを設定します。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">/// &lt;summary&gt;
/// 計算の左辺値
/// &lt;/summary&gt;
public string Lhs
{
    get
    {
        return this.lhs;
    }

    set
    {
        this.lhs = value;
        if (!this.IsDouble(value))
        {
            this.SetError(&quot;Lhs&quot;, &quot;数字を入力してください&quot;);
        }
        else
        {
            this.ClearError(&quot;Lhs&quot;);
        }

        this.RaisePropertyChanged(&quot;Lhs&quot;);
    }
}

/// &lt;summary&gt;
/// 計算の右辺値
/// &lt;/summary&gt;
public string Rhs
{
    get
    {
        return this.rhs;
    }

    set
    {
        this.rhs = value;
        if (!this.IsDouble(value))
        {
            this.SetError(&quot;Rhs&quot;, &quot;数字を入力してください&quot;);
        }
        else
        {
            this.ClearError(&quot;Rhs&quot;);
        }

        this.RaisePropertyChanged(&quot;Rhs&quot;);
    }
}

private bool IsDouble(string value)
{
    var temp = default(double);
    return double.TryParse(value, out temp);
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;計算の左辺値</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Lhs&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.lhs;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.lhs&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!<span class="cs__keyword">this</span>.IsDouble(<span class="cs__keyword">value</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SetError(<span class="cs__string">&quot;Lhs&quot;</span>,&nbsp;<span class="cs__string">&quot;数字を入力してください&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.ClearError(<span class="cs__string">&quot;Lhs&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.RaisePropertyChanged(<span class="cs__string">&quot;Lhs&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;計算の右辺値</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Rhs&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.rhs;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.rhs&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!<span class="cs__keyword">this</span>.IsDouble(<span class="cs__keyword">value</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SetError(<span class="cs__string">&quot;Rhs&quot;</span>,&nbsp;<span class="cs__string">&quot;数字を入力してください&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.ClearError(<span class="cs__string">&quot;Rhs&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.RaisePropertyChanged(<span class="cs__string">&quot;Rhs&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsDouble(<span class="cs__keyword">string</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;temp&nbsp;=&nbsp;<span class="cs__keyword">default</span>(<span class="cs__keyword">double</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">double</span>.TryParse(<span class="cs__keyword">value</span>,&nbsp;<span class="cs__keyword">out</span>&nbsp;temp);&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;そして、計算処理の実行も入力値のエラーが無い状態でのみ実行できるようにCanCalculateExecuteメソッドでHasErrorプロパティを確認するように変更します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">/// &lt;summary&gt;
/// 計算処理が実行可能かどうかの判定を行います。
/// &lt;/summary&gt;
/// &lt;returns&gt;&lt;/returns&gt;
private bool CanCalculateExecute()
{
    // 現在選択されている計算方法がNone以外かつ入力にエラーがなければコマンドの実行が可能
    return this.SelectedCalculateType.CalculateType != CalculateType.None 
        &amp;&amp; !this.HasError;
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;計算処理が実行可能かどうかの判定を行います。</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;returns&gt;&lt;/returns&gt;</span>&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;CanCalculateExecute()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;現在選択されている計算方法がNone以外かつ入力にエラーがなければコマンドの実行が可能</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.SelectedCalculateType.CalculateType&nbsp;!=&nbsp;CalculateType.None&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;!<span class="cs__keyword">this</span>.HasError;&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">
<div class="endscriptcode">最後に、LhsとRhsプロパティの値が文字列型になっているのでCalculateExecuteメソッドでModelに渡すデータをdouble型に変換して渡すようにします。ここでは、CanCalculateExecuteメソッドで値の妥当性検&#35388;が行われていることが確定しているので、値のチェックは行わずに変換しています。</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
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
} 
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">///&nbsp;&lt;summary&gt;&nbsp;</span>&nbsp;
<span class="cs__com">///&nbsp;計算処理のコマンドの実行を行います。&nbsp;</span>&nbsp;
<span class="cs__com">///&nbsp;&lt;/summary&gt;&nbsp;</span>&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;CalculateExecute()&nbsp;&nbsp;
{&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;現在の入力値を元に計算を行う&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;calc&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Calculator();&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Answer&nbsp;=&nbsp;calc.Execute(&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>.Parse(<span class="cs__keyword">this</span>.Lhs),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>.Parse(<span class="cs__keyword">this</span>.Rhs),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SelectedCalculateType.CalculateType);&nbsp;&nbsp;
}&nbsp;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">&nbsp;そして、初期状態で入力値の妥当性検&#35388;が行われているようにするために、コンストラクタでLhsとRhsプロパティに値を設定します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">csharp</span>
<pre class="hidden">public MainViewModel()
{
    this.CalculateTypes = CalculateTypeViewModel.Create();
    this.SelectedCalculateType = this.CalculateTypes.First();

    // 入力値の検&#35388;を行う
    this.Lhs = string.Empty;
    this.Rhs = string.Empty;
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;MainViewModel()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.CalculateTypes&nbsp;=&nbsp;CalculateTypeViewModel.Create();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SelectedCalculateType&nbsp;=&nbsp;<span class="cs__keyword">this</span>.CalculateTypes.First();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;入力値の検&#35388;を行う</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Lhs&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Rhs&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
}&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">以上で、ViewModelの変更は完了です。</div>
</div>
</div>
<div class="endscriptcode"></div>
<h2 class="endscriptcode">Viewの改造</h2>
<p>次に、Viewの改造を行います。Bindingはデフォルトでは入力値の検&#35388;エラーは通知しないように設定されているため、これを有効にします。具体的には以下のValidatesOnDataErrorsプロパティをTrueにします。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">xaml</span>
<pre class="hidden">ValidatesOnDataErrors=True</pre>
<div class="preview">
<pre class="xaml">ValidatesOnDataErrors=True&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;このようにすると、入力にエラーがあるとTextBoxに赤枠が表示されるようになります。これだけでは通常のエラー表示としては弱いので、今回は背景をピンクにしてツールチップにエラーメッセージを表示するようにします。このサンプルではTextBoxのStyleを定義して、実現しています。App.xamlに以下のようにStyleを定義します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;!-- 入力値のエラーのフィードバック機能を持たせたTextBoxのスタイル --&gt;
&lt;Style TargetType=&quot;{x:Type TextBox}&quot;&gt;
    &lt;Style.Triggers&gt;
        &lt;!-- バリデーションエラーがある場合に --&gt;
        &lt;Trigger Property=&quot;Validation.HasError&quot; Value=&quot;True&quot;&gt;
            &lt;!-- ツールチップにエラーメッセージと --&gt;
            &lt;Setter Property=&quot;ToolTip&quot; 
                    Value=&quot;{Binding (Validation.Errors)[0].ErrorContent, RelativeSource={RelativeSource Mode=Self}}&quot; /&gt;
            &lt;!-- 背景色を設定する --&gt;
            &lt;Setter Property=&quot;Background&quot; 
                    Value=&quot;Pink&quot; /&gt;
        &lt;/Trigger&gt;
    &lt;/Style.Triggers&gt;
&lt;/Style&gt;
</pre>
<div class="preview">
<pre class="xaml"><span class="xaml__comment">&lt;!--&nbsp;入力値のエラーのフィードバック機能を持たせたTextBoxのスタイル&nbsp;--&gt;</span>&nbsp;
<span class="xaml__tag_start">&lt;Style</span>&nbsp;<span class="xaml__attr_name">TargetType</span>=<span class="xaml__attr_value">&quot;{x:Type&nbsp;TextBox}&quot;</span><span class="xaml__tag_start">&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;<span class="css__element">Style</span><span class="css__class">.Triggers</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;バリデーションエラーがある場合に&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;<span class="css__element">Trigger</span>&nbsp;<span class="css__element">Property</span>=&quot;<span class="css__element">Validation</span><span class="css__class">.HasError</span>&quot;&nbsp;<span class="css__element">Value</span>=&quot;<span class="css__element">True</span>&quot;&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;ツールチップにエラーメッセージと&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;<span class="css__element">Setter</span>&nbsp;<span class="css__element">Property</span>=&quot;<span class="css__element">ToolTip</span>&quot;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="css__element">Value</span>=&quot;{Binding&nbsp;(Validation.Errors)[<span class="css__number">0</span>].ErrorContent,&nbsp;RelativeSource={RelativeSource&nbsp;Mode=Self}}&quot;&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;背景色を設定する&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;<span class="css__element">Setter</span>&nbsp;<span class="css__element">Property</span>=&quot;<span class="css__element">Background</span>&quot;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="css__element">Value</span>=&quot;<span class="css__element">Pink</span>&quot;&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/<span class="css__element">Trigger</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/<span class="css__element">Style</span><span class="css__class">.Triggers</span>&gt;&nbsp;
<span class="xaml__tag_end">&lt;/Style&gt;</span>&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;以上で完成です。実行してみると、最初に示したように入力値にエラーがある場合にテキストボックスの色が変わったりツールチップでエラーメッセージが表示されるようになります。また入力にエラーがある場合に、ボタンが押せないようになります。</div>
</div>
<div class="endscriptcode"></div>
<h1 class="endscriptcode">まとめ</h1>
<p>ここでは、MVVMパターンで一般的に使用されるであろうIDataErrorInfoを使用した入力値の検&#35388;方法を示しました。WPFでは、これ以外にもValidationRuleやプロパティのset内で例外を発生させるといった方法でも入力値の検&#35388;ができますが、今回のようにViewModel内でエラーの入力内容も保持していたほうが、何かと潰しがきくためお勧めです。</p>
<p>また、今回の実装は簡略化は何も試みずに愚直に実装したため少しコード量が多くなっています。DataAnnotationsなどをうまく活用することで、プロパティに属性を指定するだけで妥当性検&#35388;を行うようにすることも可能です。興味のあるかたは調べてみてください。</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
