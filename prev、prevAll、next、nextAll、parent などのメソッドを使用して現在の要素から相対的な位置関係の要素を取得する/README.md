# prev、prevAll、next、nextAll、parent などのメソッドを使用して現在の要素から相対的な位置関係の要素を取得する
## License
- Apache License, Version 2.0
## Technologies
- Visual Studio 2010
- jQuery 1.4.4
## Topics
- 逆引きサンプル コード
- jQuery
## Updated
- 02/22/2011
## Description

<p>執筆者: <a href="http://msdn.microsoft.com/ja-jp/gg585574#yamada" target="_blank">
有限会社 WINGS プロジェクト 山田 祥寛</a></p>
<p>動作確認環境: Visual Studio 2010、jQuery 1.4.4</p>
<hr>
<p>prev、prevAll、next、nextAll、parent などのメソッドを利用することで、現在の要素を基点にして、その前、次など相対的な関係にある要素を取り出すことができます。</p>
<p>たとえば、以下のサンプルは、id 属性が &quot;iam&quot; である要素を基点として、兄弟要素や親子要素を取り出し、そのスタイルを変更する例です。</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">html</span>

<div class="preview">
<pre id="codePreview" class="html"><span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;祖先&nbsp;
&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;親&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;border&quot;</span><span class="html__tag_start">&gt;兄</span>1<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;兄</span>2<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;iam&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ぼく&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;子</span>供&nbsp;1<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;子</span>供&nbsp;2<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;border&quot;</span><span class="html__tag_start">&gt;弟</span>1<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;弟</span>2<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;../Scripts/jquery-1.4.4.min.js&quot;</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span><span class="html__tag_start">&gt;</span>&nbsp;
$(<span class="js__string">'#iam'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;id&nbsp;属性が&nbsp;&quot;iam&quot;&nbsp;である要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'background-color'</span>,&nbsp;<span class="js__string">'Yellow'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;背景を黄色に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.children()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;子要素群を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'font-weight'</span>,&nbsp;<span class="js__string">'bold'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;フォントを太字に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.prev()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;直前の兄要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'background-color'</span>,&nbsp;<span class="js__string">'Lime'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;背景をライム色に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.prevAll()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;すべての兄要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'color'</span>,&nbsp;<span class="js__string">'Red'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;テキストを赤色に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.prevUntil(<span class="js__string">'.border'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;class&nbsp;属性が&nbsp;&quot;border&quot;&nbsp;である兄要素まで取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'font-style'</span>,&nbsp;<span class="js__string">'italic'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;フォントを斜体に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.next()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;直後の弟要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'background-color'</span>,&nbsp;<span class="js__string">'Aqua'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;背景をアクア色に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.nextAll()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;すべての弟要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'color'</span>,&nbsp;<span class="js__string">'Blue'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;テキストを青色に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.nextUntil(<span class="js__string">'.border'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;class&nbsp;属性が&nbsp;&quot;border&quot;&nbsp;である弟要素まで取得&nbsp;(=該当なし)</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'font-size'</span>,&nbsp;<span class="js__string">'xx-small'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;フォントサイズを最小に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.parent()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;親要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'border'</span>,&nbsp;<span class="js__string">'solid&nbsp;1px&nbsp;Black'</span>);&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;黒の枠線を付与</span>&nbsp;
<span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<p><img src="18627-arrow.gif" alt="" width="35" height="42"></p>
<p><img src="18631-image001.jpg" alt="図 1" width="337" height="310"></p>
<p>サンプルではそれぞれのメソッドでカレント要素を移動し、スタイル変更した後、基点 (id 属性が &quot;iam&quot;) の要素に戻り、また移動して&hellip;という動作を繰り返しています。それぞれのメソッドの意味については、サンプル内のコメントを参照すれば直感的に理解できると思いますので、注意すべき点のみ以下に補足しておきます。</p>
<p><strong>(1) prevUntil、nextUntil メソッドの境界</strong></p>
<p>prevAll、nextAll は、セレクター式に合致する要素が見つかるまで、その兄/弟要素を取得します。ただし、セレクター式に合致した要素そのものは結果に含まれない点に注意してください。よって、サンプルでも「nextUntil('.border')」に合致する要素は存在しないことになります。</p>
<p><strong>(2) その他のメソッドでセレクター式を指定した場合</strong></p>
<p>prev/prevAll、next/nextAll、parents メソッドでも引数としてセレクター式を指定できます。その場合、たとえば prev メソッドであれば、直前の兄要素がセレクター式に合致した場合のみ取得しますし、prevAll メソッドであれば、セレクター式に合致した兄要素のみを取得します。</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginEditHolderLink">スクリプトの編集</div>
<span class="hidden">html</span>

<div class="preview">
<pre id="codePreview" class="html"><span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;兄</span>1<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;new&quot;</span><span class="html__tag_start">&gt;兄</span>2<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;兄</span>3<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;iam&quot;</span><span class="html__tag_start">&gt;ぼ</span>く<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;new&quot;</span><span class="html__tag_start">&gt;弟</span>1<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span><span class="html__tag_start">&gt;弟</span>2<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;new&quot;</span><span class="html__tag_start">&gt;弟</span>3<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;../Scripts/jquery-1.4.4.min.js&quot;</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span><span class="html__tag_start">&gt;</span>&nbsp;
$(<span class="js__string">'#iam'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;id&nbsp;属性が&nbsp;&quot;iam&quot;&nbsp;である要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'background-color'</span>,&nbsp;<span class="js__string">'Yellow'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;背景を黄色に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.prev(<span class="js__string">'.new'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;直前の兄要素が&nbsp;class=&quot;new&quot;&nbsp;である場合のみ取得&nbsp;(＝該当なし)</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'background-color'</span>,&nbsp;<span class="js__string">'Aqua'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;背景をアクア色に</span>&nbsp;
&nbsp;&nbsp;.end()&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;カレント要素を元の位置に</span>&nbsp;
&nbsp;
&nbsp;&nbsp;.nextAll(<span class="js__string">'.new'</span>)&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;class=&quot;new&quot;&nbsp;である弟要素を取得</span>&nbsp;
&nbsp;&nbsp;.css(<span class="js__string">'background-color'</span>,&nbsp;<span class="js__string">'Lime'</span>);&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;背景をライム色に</span>&nbsp;
<span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<p><img src="18628-arrow.gif" alt="" width="35" height="42"></p>
<p><img src="18630-image002.jpg" alt="図 2" width="337" height="272"></p>
<hr style="clear:both; margin-bottom:8px; margin-top:20px">
<table>
<tbody>
<tr>
<td><a href="http://msdn.microsoft.com/ja-jp/samplecode.recipe"><img src="-ff950935.coderecipe_180x70%28ja-jp,msdn.10%29.jpg" border="0" alt="Code Recipe" width="180" height="70" style="margin-top:3px"></a></td>
<td><a href="http://msdn.microsoft.com/ja-jp/asp.net/" target="_blank"><img src="-ff950935.asp_net_180x70%28ja-jp,msdn.10%29.jpg" border="0" alt="ASP.NET デベロッパーセンター" width="180" height="70" style="margin-top:3px"></a></td>
<td>
<ul>
<li>もっと他のコンテンツを見る &gt;&gt; <a href="http://msdn.microsoft.com/ja-jp/ff363212" target="_blank">
逆引きサンプル コード一覧へ</a> </li><li>もっと他のレシピを見る &gt;&gt; <a href="http://msdn.microsoft.com/ja-jp/samplecode.recipe">
Code Recipe へ</a> </li><li>もっと ASP.NET の情報を見る &gt;&gt; <a href="http://msdn.microsoft.com/ja-jp/asp.net" target="_blank">
ASP.NET デベロッパーセンターへ</a> </li></ul>
</td>
</tr>
</tbody>
</table>
<p style="margin-top:20px"><a href="#top"><img src="-top.gif" border="0" alt="">ページのトップへ</a></p>
