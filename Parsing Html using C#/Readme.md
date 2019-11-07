# Parsing Html using C#
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- C#
- XAML
- WindowsPhone8
## Topics
- Windows Phone 8
## Updated
- 08/06/2014
## Description

<h1>Introduction</h1>
<p><span>Usually Website has a Rss File then we can parse it to have the latest news , however , there are some that didn't make this Rss file so we should parse directly HTML of this Website.&nbsp;</span></p>
<p><span style="font-size:2em">Building the Sample</span></p>
<p>Fisrt of all , we should add to the reference the&nbsp;<code><a href="http://htmlagilitypack.codeplex.com/" target="_self">Htmlagilitypack</a>&nbsp;</code>, you can download it from nuget on you visual studio.</p>
<p>Ps : If you are working on Windows Phone , it will have some problems with that dll , so you must add these two dll file&nbsp;<strong><code>System.net.http</code></strong>&nbsp;and&nbsp;<code><strong>System.Xml.Xpat</strong>h&nbsp;</code>you can also find
 it on nuget.</p>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span>We creat a new Function that take as parameter the website that you want to parse ,&nbsp;<span>Then we send a request to the website to get all html page.</span></span></p>
<p><span><span><strong>Ps : you should pay attention on the Encoding , each website has an Encoding , in this example it uses &quot;<code>utf-8&quot;&nbsp;</code>, you can find it on the attribut&nbsp;<code>charset&nbsp;</code>on the website html.</strong></span></span></p>
<p><span><span><strong><img id="122350" src="122350-charset.png" alt="" width="648" height="43"><br>
</strong></span></span></p>
<p><span>After that we inspect the element that &nbsp;we want to parse it and get it's id or class then we can retrieve it easely.</span></p>
<p><span><img id="122351" src="122351-example.png" alt=""><br>
</span></p>
<p>as you can see on the picture , we want to parse information of these devices that are all wrapped in&nbsp;<code>ul</code>, but before that we must find the&nbsp;ancestor&nbsp;<code>div</code>&nbsp;that has an id or a class , in this example the div have
 a class named<code>block_content.</code></p>
<p>So now we will filter the html with only the content of this div, then we get all tag&nbsp;of&nbsp;<code>li</code>&nbsp;that contains information that we want to get.</p>
<p>&nbsp;</p>
<p><span>After each filter you do , it is preferred to beakpoint the project to verify our work.</span></p>
<p><span><img id="122352" src="122352-breakpoint.png" alt=""></span></p>
<p><span><span>As a result we get 11 div that have class named&nbsp;</span><code>block_content&nbsp;</code><span>, &nbsp;so you should verify which item contains information that we want to get.in his example it' the item N&deg;6.</span></span></p>
<p>inside each item of&nbsp;<code>li,&nbsp;</code>we will get the link , image and Title.</p>
<p><strong><code>Descendants&nbsp;</code></strong>allow you to get all tag with specified name inside the item.</p>
<p><strong><code>GetAttributeValue&nbsp;</code></strong>allow you to get the attribut of the tag.</p>
<p><strong><code>InnerText&nbsp;</code></strong>allow you to get Text betweens tags.</p>
<p><strong><code>InnerHtml&nbsp;</code></strong>allow you to get HTML.</p>
<p><span><span><br>
</span></span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden"> private async void Parsing(string website)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(website);
                String source = Encoding.GetEncoding(&quot;utf-8&quot;).GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);

                List&lt;HtmlNode&gt; toftitle = resultat.DocumentNode.Descendants().Where
                (x =&gt; (x.Name == &quot;div&quot; &amp;&amp; x.Attributes[&quot;class&quot;] != null &amp;&amp; x.Attributes[&quot;class&quot;].Value.Contains(&quot;block_content&quot;))).ToList();

                var li = toftitle[6].Descendants(&quot;li&quot;).ToList();
                foreach (var item in li)
                {
                    var link = item.Descendants(&quot;a&quot;).ToList()[0].GetAttributeValue(&quot;href&quot;, null);
                    var img = item.Descendants(&quot;img&quot;).ToList()[0].GetAttributeValue(&quot;src&quot;, null);
                    var title = item.Descendants(&quot;h5&quot;).ToList()[0].InnerText;

                    listproduct.Add(new Product()
                    {
                        Img = img,
                        Title = title,
                        Link = link
                    });
                }

            }
            catch (Exception)
            {

                MessageBox.Show(&quot;Network Problem!&quot;);
            }

        }</pre>
<div class="preview">
<pre class="csharp">&nbsp;<span class="cs__keyword">private</span>&nbsp;async&nbsp;<span class="cs__keyword">void</span>&nbsp;Parsing(<span class="cs__keyword">string</span>&nbsp;website)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpClient&nbsp;http&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;HttpClient();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;response&nbsp;=&nbsp;await&nbsp;http.GetByteArrayAsync(website);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;String&nbsp;source&nbsp;=&nbsp;Encoding.GetEncoding(<span class="cs__string">&quot;utf-8&quot;</span>).GetString(response,&nbsp;<span class="cs__number">0</span>,&nbsp;response.Length&nbsp;-&nbsp;<span class="cs__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;source&nbsp;=&nbsp;WebUtility.HtmlDecode(source);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HtmlDocument&nbsp;resultat&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;HtmlDocument();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resultat.LoadHtml(source);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;HtmlNode&gt;&nbsp;toftitle&nbsp;=&nbsp;resultat.DocumentNode.Descendants().Where&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(x&nbsp;=&gt;&nbsp;(x.Name&nbsp;==&nbsp;<span class="cs__string">&quot;div&quot;</span>&nbsp;&amp;&amp;&nbsp;x.Attributes[<span class="cs__string">&quot;class&quot;</span>]&nbsp;!=&nbsp;<span class="cs__keyword">null</span>&nbsp;&amp;&amp;&nbsp;x.Attributes[<span class="cs__string">&quot;class&quot;</span>].Value.Contains(<span class="cs__string">&quot;block_content&quot;</span>))).ToList();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;li&nbsp;=&nbsp;toftitle[<span class="cs__number">6</span>].Descendants(<span class="cs__string">&quot;li&quot;</span>).ToList();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;item&nbsp;<span class="cs__keyword">in</span>&nbsp;li)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;link&nbsp;=&nbsp;item.Descendants(<span class="cs__string">&quot;a&quot;</span>).ToList()[<span class="cs__number">0</span>].GetAttributeValue(<span class="cs__string">&quot;href&quot;</span>,&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;img&nbsp;=&nbsp;item.Descendants(<span class="cs__string">&quot;img&quot;</span>).ToList()[<span class="cs__number">0</span>].GetAttributeValue(<span class="cs__string">&quot;src&quot;</span>,&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;title&nbsp;=&nbsp;item.Descendants(<span class="cs__string">&quot;h5&quot;</span>).ToList()[<span class="cs__number">0</span>].InnerText;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;listproduct.Add(<span class="cs__keyword">new</span>&nbsp;Product()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Img&nbsp;=&nbsp;img,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Title&nbsp;=&nbsp;title,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Link&nbsp;=&nbsp;link&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="cs__string">&quot;Network&nbsp;Problem!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1>More Information</h1>
<p><span>Difficulty of parsing &nbsp;html depends on the structure of website.</span></p>
<p><span><img id="122354" src="122354-wp_ss_20140806_0002.png" alt=""><br>
</span></p>
