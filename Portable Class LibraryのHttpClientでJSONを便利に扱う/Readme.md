# Portable Class LibraryのHttpClientでJSONを便利に扱う
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- portable class library
## Topics
- Web API
## Updated
- 09/03/2013
## Description

<h1>サンプルプログラムの概要</h1>
<p>このサンプルプログラムは、Portable Class Libraryで公開されているHttpClientクラスを拡張してJSONでデータをやり取りするAPIを便利に使うメソッドの例です。Portable Class Libraryではない、.NET FrameworkのWebAPIのClientとして公開されているHttpClientには、同じ機能をもつメソッドが追加されているので、おそらく近いうちに、このサンプルプログラムで提供している機能は不要になると思いますが、それまでの繋ぎと、既存のコードを拡張メソッドを使って機能拡張を行う例として参照してください。</p>
<h1>サンプルプログラムの解説</h1>
<p>このサンプルプログラムのポイントとなる部分は、Codeplex.HttpClientExtensionsというPortable Class Libraryのプロジェクトの中のHttpClientExtensions.csファイルになります。このファイルの中身は以下のようになっています。</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Codeplex.HttpClientExtensions
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    /// &lt;summary&gt;
    /// HttpClientがシームレスにJSONと連携するための拡張メソッドを提供します。
    /// &lt;/summary&gt;
    public static class HttpClientExtensions
    {
        /// &lt;summary&gt;
        /// ボディ部にJSONを含むHTTPリクエストをPOSTします。
        /// &lt;/summary&gt;
        /// &lt;typeparam name=&quot;T&quot;&gt;JSONにシリアライズする型&lt;/typeparam&gt;
        /// &lt;param name=&quot;self&quot;&gt;拡張元のクラス&lt;/param&gt;
        /// &lt;param name=&quot;uri&quot;&gt;リクエストを送信する先のURI&lt;/param&gt;
        /// &lt;param name=&quot;obj&quot;&gt;ボディ部にJSONにシリアライズして含めるオブジェクト&lt;/param&gt;
        /// &lt;returns&gt;レスポンス&lt;/returns&gt;
        public static Task&lt;HttpResponseMessage&gt; PostAsJsonAsync&lt;T&gt;(this HttpClient self, string uri, T obj)
        {
            var content = CreateHttpContentFromObject(obj);
            return self.PostAsync(uri, content);
        }

        /// &lt;summary&gt;
        /// ボディ部にJSONを含むHTTPリクエストをPUTします。
        /// &lt;/summary&gt;
        /// &lt;typeparam name=&quot;T&quot;&gt;JSONにシリアライズする型&lt;/typeparam&gt;
        /// &lt;param name=&quot;self&quot;&gt;拡張元のクラス&lt;/param&gt;
        /// &lt;param name=&quot;uri&quot;&gt;リクエストを送信する先のURI&lt;/param&gt;
        /// &lt;param name=&quot;obj&quot;&gt;ボディ部にJSONにシリアライズして含めるオブジェクト&lt;/param&gt;
        /// &lt;returns&gt;レスポンス&lt;/returns&gt;
        public static Task&lt;HttpResponseMessage&gt; PutAsJsonAsync&lt;T&gt;(this HttpClient self, string uri, T obj)
        {
            var content = CreateHttpContentFromObject(obj);
            return self.PutAsync(uri, content);
        }

        /// &lt;summary&gt;
        /// HttpResponseMessageのContentからJSONをオブジェクトにデシリアライズするメソッド
        /// &lt;/summary&gt;
        /// &lt;typeparam name=&quot;T&quot;&gt;JSONをデシリアライズする型&lt;/typeparam&gt;
        /// &lt;param name=&quot;content&quot;&gt;HttpContent&lt;/param&gt;
        /// &lt;returns&gt;HttpContentから読み込んだJSONをデシリアライズした結果のオブジェクト&lt;/returns&gt;
        public static async Task&lt;T&gt; ReadAsJsonAsync&lt;T&gt;(this HttpContent content)
        {
            var binary = await content.ReadAsByteArrayAsync();
            var jsonText = Encoding.UTF8.GetString(binary, 0, binary.Length);
            return JsonConvert.DeserializeObject&lt;T&gt;(jsonText);
        }

        /// &lt;summary&gt;
        /// オブジェクトからJSONを含んだHttpContentを作成する
        /// &lt;/summary&gt;
        private static HttpContent CreateHttpContentFromObject(object obj)
        {
            var jsonText = JsonConvert.SerializeObject(obj);
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(jsonText));
            content.Headers.ContentType = new MediaTypeHeaderValue(&quot;application/json&quot;);
            return content;
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Codeplex.HttpClientExtensions&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;Newtonsoft.Json;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Net.Http;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Net.Http.Headers;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Threading.Tasks;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;HttpClientがシームレスにJSONと連携するための拡張メソッドを提供します。</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;HttpClientExtensions&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;ボディ部にJSONを含むHTTPリクエストをPOSTします。</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;typeparam&nbsp;name=&quot;T&quot;&gt;JSONにシリアライズする型&lt;/typeparam&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;self&quot;&gt;拡張元のクラス&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;uri&quot;&gt;リクエストを送信する先のURI&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;obj&quot;&gt;ボディ部にJSONにシリアライズして含めるオブジェクト&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;returns&gt;レスポンス&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;Task&lt;HttpResponseMessage&gt;&nbsp;PostAsJsonAsync&lt;T&gt;(<span class="cs__keyword">this</span>&nbsp;HttpClient&nbsp;self,&nbsp;<span class="cs__keyword">string</span>&nbsp;uri,&nbsp;T&nbsp;obj)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;content&nbsp;=&nbsp;CreateHttpContentFromObject(obj);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;self.PostAsync(uri,&nbsp;content);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;ボディ部にJSONを含むHTTPリクエストをPUTします。</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;typeparam&nbsp;name=&quot;T&quot;&gt;JSONにシリアライズする型&lt;/typeparam&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;self&quot;&gt;拡張元のクラス&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;uri&quot;&gt;リクエストを送信する先のURI&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;obj&quot;&gt;ボディ部にJSONにシリアライズして含めるオブジェクト&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;returns&gt;レスポンス&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;Task&lt;HttpResponseMessage&gt;&nbsp;PutAsJsonAsync&lt;T&gt;(<span class="cs__keyword">this</span>&nbsp;HttpClient&nbsp;self,&nbsp;<span class="cs__keyword">string</span>&nbsp;uri,&nbsp;T&nbsp;obj)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;content&nbsp;=&nbsp;CreateHttpContentFromObject(obj);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;self.PutAsync(uri,&nbsp;content);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;HttpResponseMessageのContentからJSONをオブジェクトにデシリアライズするメソッド</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;typeparam&nbsp;name=&quot;T&quot;&gt;JSONをデシリアライズする型&lt;/typeparam&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;content&quot;&gt;HttpContent&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;returns&gt;HttpContentから読み込んだJSONをデシリアライズした結果のオブジェクト&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;async&nbsp;Task&lt;T&gt;&nbsp;ReadAsJsonAsync&lt;T&gt;(<span class="cs__keyword">this</span>&nbsp;HttpContent&nbsp;content)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;binary&nbsp;=&nbsp;await&nbsp;content.ReadAsByteArrayAsync();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;jsonText&nbsp;=&nbsp;Encoding.UTF8.GetString(binary,&nbsp;<span class="cs__number">0</span>,&nbsp;binary.Length);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;JsonConvert.DeserializeObject&lt;T&gt;(jsonText);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;オブジェクトからJSONを含んだHttpContentを作成する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;HttpContent&nbsp;CreateHttpContentFromObject(<span class="cs__keyword">object</span>&nbsp;obj)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;jsonText&nbsp;=&nbsp;JsonConvert.SerializeObject(obj);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;content&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ByteArrayContent(Encoding.UTF8.GetBytes(jsonText));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;content.Headers.ContentType&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MediaTypeHeaderValue(<span class="cs__string">&quot;application/json&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;content;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode"></div>
<p>Json.NETと、PCL版のHttpClientを使ってPostやPut、Getの戻り値のHttpResponseMessageのContentからオブジェクトを取り出すための便利な拡張メソッドを定義しています。このコードの使用例は残りの以下の2つのプロジェクトで示しています。</p>
<ul>
<li>Sample.Serverプロジェクト<br>
HttpClientを使って呼び出すためのサンプルのREST APIを定義しています。 </li><li>Sample.Clientプロジェクト&nbsp;<br>
PCL版のHttpClientを使って、Sample.Serverで定義されているREST APIの呼び出しを行っています。 </li></ul>
<p>&nbsp;このサンプルコードを使用するためには、以下のライブラリをNuGetから追加する必要があります。このSample.Clientは.NET Framework 4をターゲットとするコンソールアプリケーションですが、各PCLはSL4, SL5, WP7.5, WP8, .NET 4, .NET 4.5, WinRTをサポートしているため、すべてのプラットフォームで同じコードが動きます。</p>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var root = &quot;http://localhost:9454/api/&quot;;

var c = new HttpClient();
{
    // Get
    var r = await c.GetAsync(root &#43; &quot;values&quot;);
    r.EnsureSuccessStatusCode();
    var ps = await r.Content.ReadAsJsonAsync&lt;IEnumerable&lt;Person&gt;&gt;();
    foreach (var p in ps)
    {
        Console.WriteLine(&quot;{0} {1}&quot;, p.Id, p.Name);
    }
}
{
    // Post
    await c.PostAsJsonAsync(root &#43; &quot;values&quot;, new Person { Id = -1, Name = &quot;くらいあんとたろう&quot; });
    // Put
    await c.PostAsJsonAsync(root &#43; &quot;values/10&quot;, new Person { Id = 10, Name = &quot;くらいあんとたろう&quot; });
}
</pre>
<div class="preview">
<pre class="csharp">var&nbsp;root&nbsp;=&nbsp;<span class="cs__string">&quot;http://localhost:9454/api/&quot;</span>;&nbsp;
&nbsp;
var&nbsp;c&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;HttpClient();&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;r&nbsp;=&nbsp;await&nbsp;c.GetAsync(root&nbsp;&#43;&nbsp;<span class="cs__string">&quot;values&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;r.EnsureSuccessStatusCode();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;ps&nbsp;=&nbsp;await&nbsp;r.Content.ReadAsJsonAsync&lt;IEnumerable&lt;Person&gt;&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;p&nbsp;<span class="cs__keyword">in</span>&nbsp;ps)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;{0}&nbsp;{1}&quot;</span>,&nbsp;p.Id,&nbsp;p.Name);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Post</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;await&nbsp;c.PostAsJsonAsync(root&nbsp;&#43;&nbsp;<span class="cs__string">&quot;values&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;Person&nbsp;{&nbsp;Id&nbsp;=&nbsp;-<span class="cs__number">1</span>,&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;くらいあんとたろう&quot;</span>&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Put</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;await&nbsp;c.PostAsJsonAsync(root&nbsp;&#43;&nbsp;<span class="cs__string">&quot;values/10&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;Person&nbsp;{&nbsp;Id&nbsp;=&nbsp;<span class="cs__number">10</span>,&nbsp;Name&nbsp;=&nbsp;<span class="cs__string">&quot;くらいあんとたろう&quot;</span>&nbsp;});&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p>このコードを実行すると、JSONでデータをやりとりしつつ、JSONを特に意識することなくREST APIを呼び出すことが出来ます。</p>
<h1>まとめ</h1>
<p>最近は、HttpClientにはじまりasync await、圧縮・解凍、WCF Data Servicesのクライアント、Reactive ExtensionsまでPortable Class Libraryで提供されています。これらのPortable Class Libraryで提供されているものには、最新のプラットフォーム固有の機能に比べると若干機能的に劣る部分があるかもしれませんが、このように拡張メソッドをつかって不足部分を既存のAPIの形に合わせて補って使うことで、後々のバージョンアップで機能が提供されたときにも、拡張メソッドを外すだけで済むため、個人的におすすめです。Portable
 Class Libraryで実装できる部分は今後どんどん増えていくと思われるので、PCLに寄せれる部分は、一度PCLに寄せるメリット（ポータビリティが一番ですよね）について一度考えてみてもいいかもしれません。</p>
