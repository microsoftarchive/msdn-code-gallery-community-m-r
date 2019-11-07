# REST servis nedir ?
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- WCF
- REST
## Topics
- REST
- CRUD
## Updated
- 01/14/2014
## Description

<h1><span style="font-size:10px">Bu makalemizde REST servisler ile ilgili aşağıdaki sorulara cevaplar arıyoruz. Makale boyunca aktarılan kodlar &ouml;rnek uygulamada mevcuttur.</span></h1>
<p>&nbsp;</p>
<p>1) REST servis nedir ? Avantajı nedir ?<br>
2) Nasıl geliştirilir ?<br>
3) Nasıl kullanılır ?</p>
<h1><br>
<br>
Giriş</h1>
<p>&nbsp;</p>
<table>
<tbody>
<tr>
<td>Bilgi teknolojilerinin hemen hemen her alana girmiş olmasından dolayı, farklı uygulamalar arasında veri alışveri ihtiyacı her ge&ccedil;en g&uuml;n artmaktadır. Farklı uygulamalar arasında veri alışverişi i&ccedil;in g&uuml;n itibari ile aşağıdaki &ccedil;alışmalardan
 birini geliştirip &ccedil;alışmanızdan faydalanacak karşı tarafa bunun url adresini<br>
vermeniz&nbsp;gerekir.<br>
&nbsp;</td>
</tr>
<tr>
<td>
<table>
<tbody>
<tr>
<td><em>RSS</em><br>
&nbsp;</td>
<td>Karşı tarafın kullanabileceği xml &uuml;reten bir web sayfanız olur. Sadece listeme yapılabilir. Yani RSS kaynağının olduğu tarafa bilgi g&ouml;nderilemez.<br>
&nbsp;</td>
</tr>
<tr>
<td><em>WebServis</em><br>
<br>
&nbsp;</td>
<td>Karşı tarafın kullanabileceği xml &uuml;reten bir servisiniz olur. Karşı tarafta &uuml;r&uuml;n geliştiricinin servisinizi projesine dahil etmesi gerekir.<br>
Dahil etme işlemi &ccedil;eşitli platformlarda &ccedil;eşitli şekillerde ger&ccedil;ekleştirilir. Hem listeleme hem kayıt işlemi yapılabilir.<br>
&nbsp;</td>
</tr>
<tr>
<td><em>WCF</em><br>
&nbsp;</td>
<td>Web servis gibi karşı tarafın kullanabileceği bir servisiniz olur. Web service g&ouml;re &ccedil;eşitli artıları vardır.<br>
&nbsp;</td>
</tr>
<tr>
<td><em>REST</em><br>
<br>
&nbsp;</td>
<td>Karşı tarafın kullanabileceği xml veya json formatında veri alışverişine olanak sağlayan bu yaklaşım,<br>
http &uuml;zerinden istemci-sunucu (client-server)&nbsp;mimarisi ile &ccedil;alışan bir servis altyapısı sağlar.<br>
&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>MVC ile birlikte anılmaya başlanan WebAPI'nin temelini oluşturur.</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>Hem veri &ccedil;ekmeye (GET) hem de g&ouml;ndermeye(POST, PUT) olanak tanır. Ayrıca silme (DELETE) işlemi de m&uuml;mk&uuml;nd&uuml;r.</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<p><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span></p>
<p>&nbsp;</p>
<h1><span id="ContentPlaceHolder1_LabelLongDesc">1) REST servis nedir ? Avantajı nedir ?</span></h1>
<table>
<tbody>
<tr>
<td>REpresentational State Transfer ifadesinin baş harflerinden oluşan bu kavram aslında g&uuml;n itibari ile&nbsp;uygulamalar arasında veri alışverinin en kolay yolu olarak&nbsp;d&uuml;ş&uuml;n&uuml;lebilir.&nbsp;<br>
<br>
REST neden en kolay yol olabilir ?<br>
<br>
Konunun en &ouml;nemli kavramı olan &quot;Stateless&quot; ifadesi bunu a&ccedil;ıklamaktadır. Sunucu tarafında istemci ile&nbsp;ilgili herhangi bir bilgi tutulmaz. Servisten faydalanmak i&ccedil;in<br>
istemci tarafında projenize bir dll,&nbsp;bir sınıf ya da bir nesne dahil etmek zorunda değilsiniz.<br>
<br>
Sunucu hernagi bir platformda geliştirilmiş olsun, istemcinin ne ile geliştirildiği &ccedil;ok &ouml;nemli değildir.&nbsp;<br>
Yeter ki istemci &quot;request&quot; yapabilecek yeteneğe sahip olsun.<br>
<br>
Bu kadar &quot;light&quot; bir model olduğu halde CRUD (Create-Read-Update-Delete) işlemlerinin ger&ccedil;ekleştirebiliyor olması&nbsp;b&uuml;y&uuml;k kolaylıktır.<br>
&nbsp;<br>
Burada kavram kargaşasına yol a&ccedil;mamak i&ccedil;in aşağıdaki tabloyu g&ouml;z &ouml;n&uuml;nde bulundurarak &ccedil;alışmamızı yapalım.</td>
</tr>
<tr>
<td>&nbsp;</td>
</tr>
<tr>
<td>
<table>
<tbody>
<tr>
<td><strong>Kavramsal</strong></td>
<td><strong>Veritabanı</strong></td>
<td><strong>REST</strong></td>
</tr>
<tr>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr>
<td>Create</td>
<td>insert</td>
<td>POST</td>
</tr>
<tr>
<td>Read</td>
<td>Select</td>
<td>GET</td>
</tr>
<tr>
<td>Update</td>
<td>Update</td>
<td>UPDATE</td>
</tr>
<tr>
<td>Delete</td>
<td>Delete</td>
<td>DELETE</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<p><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span></p>
<p>&nbsp;</p>
<p><span>2) Nasıl geliştirilir ?</span></p>
<p>&nbsp;</p>
<p><em><span id="ContentPlaceHolder1_LabelLongDesc">REST servis geliştirme işi &ccedil;eşitli IDE'lerle ger&ccedil;ekleştirilebilir. Ancak burada Microsoft VS2012 ortamında C# dili kullanılarak bir REST servis &ccedil;alışması yapılmıştır.<br>
<br>
G&ouml;r&uuml;nt&uuml;deki gibi wcf servis geliştirecek şekilde projemizin RESTserver tarafına başlayabiliriz.</span><br>
</em></p>
<p><img id="107286" src="107286-restbook_010_initprj.jpg" alt="" width="665" height="441"><br>
<br>
<br>
</p>
<table>
<tbody>
<tr>
<td>Rest servis projemizin adı RESTbook olsun. RESTbook isimli &ccedil;&ouml;z&uuml;m oluşturulurken ilk projemiz olan wcf projesini de oluşturuyoruz.</td>
</tr>
<tr>
<td>&nbsp; &nbsp; &nbsp;&nbsp;</td>
</tr>
</tbody>
</table>
<p><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span><br>
<img id="107287" src="107287-restbook_020_initprj.jpg" alt="" width="818" height="613"><br>
<br>
</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Servis ayağını ifade eden projenin i&ccedil;erisine RESTbookService adında bir servis ekliyoruz.</span></p>
<p><img id="107288" src="107288-restbook_030_addwcfserv.jpg" alt="" width="797" height="494"></p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">İlk aşamada şablon ile beraber oluşturulan Service1 silinebilir. Bu arada IRESTbookService adlı interface tarafımızdan projemize eklenir.</span></p>
<p>&nbsp;</p>
<p><br>
<img id="107289" src="107289-restbook_040_interfaceandwcf.jpg" alt="" width="820" height="618"><br>
<br>
</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Projemizde kitap konusunda hizmet veren REST servisleri geliştirmek i&ccedil;in &quot;Book&quot; adında aşağıdaki&nbsp; gibi bir sınıf geliştiriyoruz.<br>
<br>
<br>
<br>
</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp">[DataContract]&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Book&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;BookNo;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;BookName;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;publicationYear;&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>Tanımlamadaki [DataContract] ve [DataMember] ifadelerine dikkat edelim.&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Ayrıca ilgili &quot;namespace&quot; de using listemiz i&ccedil;inde yer almalı.&nbsp;</span></p>
<p>&nbsp;</p>
<p><img id="107290" src="107290-restbook_050_ns.jpg" alt="" width="372" height="217"></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">IRESTbookService i&ccedil;inde REST işlemlerini ger&ccedil;ekleştirecek metodların prototiplerini aşağıdaki gibi belirleyelim.&nbsp;&nbsp; &nbsp;&nbsp;<br>
G&ouml;r&uuml;ld&uuml;ğ&uuml; &uuml;zere [OperationContract] ifadesi ile t&uuml;m metodların servis olarak kullanılacağı ifade edilmiştir.<br>
<br>
Aşağıdaki kod par&ccedil;acığımızdan da g&ouml;r&uuml;ld&uuml;ğ&uuml; &uuml;zere POST, GET, PUT ve DELETE operasyonlarının hangi metodlarla ger&ccedil;ekleştirileceği belirlenmiştir.</span><br>
<br>
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Runtime.Serialization;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.ServiceModel;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.ServiceModel.Web;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;RESTbook&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;NOTE:&nbsp;You&nbsp;can&nbsp;use&nbsp;the&nbsp;&quot;Rename&quot;&nbsp;command</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;on&nbsp;the&nbsp;&quot;Refactor&quot;&nbsp;menu&nbsp;to&nbsp;change&nbsp;the&nbsp;interface&nbsp;name&nbsp;&quot;IRESTbookService&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;in&nbsp;both&nbsp;code&nbsp;and&nbsp;config&nbsp;file&nbsp;together.</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ServiceContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;IRESTbookService&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//[OperationContract]</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//void&nbsp;DoWork();</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//POST&nbsp;operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[WebInvoke(UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;POST&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Book&nbsp;CreateBook(Book&nbsp;book);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//GET&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[WebGet(UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Book&gt;&nbsp;GetAllBooks();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//GET&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[WebGet(UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;{id}&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Book&nbsp;GetAbook(<span class="cs__keyword">int</span>&nbsp;id);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//PUT&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[WebInvoke(UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;{id}&quot;</span>,&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;PUT&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Book&nbsp;UpdateBook(<span class="cs__keyword">int</span>&nbsp;id,&nbsp;Book&nbsp;book);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//DELETE&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[WebInvoke(UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;{id}&quot;</span>,&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;DELETE&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;DeleteBook(<span class="cs__keyword">int</span>&nbsp;id);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Book&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;BookNo;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;BookName;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;PublicationYear;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span id="ContentPlaceHolder1_LabelLongDesc">Bu aşamadan sonra IRESTbookService i&ccedil;indeki metodların &quot;implementation&quot;larını yani kod bloklarını ifade eden sınıfımızı geliştiriyoruz.&nbsp;<br>
&nbsp;<br>
Bunun i&ccedil;in RESTbookService sınıfının IRESTbookService &quot;interface&quot;inden t&uuml;remesi gerekir.</span></div>
<p><br>
<img id="107291" src="107291-restbook_060_service.jpg" alt="" width="812" height="506"></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Veritabanı işlemleri ile uğraşmamak i&ccedil;in &quot;static&quot; bir veri listesi geliştiriyoruz. Bunun i&ccedil;in Repository adında bir sınıf oluşturup i&ccedil;ini aşağıdaki gibi dolduruyoruz.&nbsp;</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Web;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;RESTbook&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Repository&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;List&lt;Book&gt;&nbsp;BookList;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;Repository()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookList&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;Book&gt;();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookList.Add(<span class="cs__keyword">new</span>&nbsp;Book&nbsp;{&nbsp;BookNo&nbsp;=&nbsp;<span class="cs__number">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookName&nbsp;=&nbsp;<span class="cs__string">&quot;For&nbsp;Whom&nbsp;the&nbsp;Bell&nbsp;Tolls&nbsp;&quot;</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PublicationYear&nbsp;=&nbsp;<span class="cs__number">1940</span>&nbsp;});&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookList.Add(<span class="cs__keyword">new</span>&nbsp;Book&nbsp;{&nbsp;BookNo&nbsp;=&nbsp;<span class="cs__number">2</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookName&nbsp;=&nbsp;<span class="cs__string">&quot;The&nbsp;Grapes&nbsp;of&nbsp;Wrath&quot;</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PublicationYear&nbsp;=&nbsp;<span class="cs__number">1939</span>&nbsp;});&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookList.Add(<span class="cs__keyword">new</span>&nbsp;Book&nbsp;{&nbsp;BookNo&nbsp;=&nbsp;<span class="cs__number">3</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookName&nbsp;=&nbsp;<span class="cs__string">&quot;The&nbsp;Captain's&nbsp;Daughter&quot;</span>,&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PublicationYear&nbsp;=&nbsp;<span class="cs__number">1836</span>&nbsp;});&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookList.Add(<span class="cs__keyword">new</span>&nbsp;Book&nbsp;{&nbsp;BookNo&nbsp;=&nbsp;<span class="cs__number">4</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookName&nbsp;=&nbsp;<span class="cs__string">&quot;Madame&nbsp;Bovary&quot;</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PublicationYear&nbsp;=&nbsp;<span class="cs__number">1856</span>&nbsp;});&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookList.Add(<span class="cs__keyword">new</span>&nbsp;Book&nbsp;{&nbsp;BookNo&nbsp;=&nbsp;<span class="cs__number">5</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BookName&nbsp;=&nbsp;<span class="cs__string">&quot;Father&nbsp;Goriot&quot;</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PublicationYear&nbsp;=&nbsp;<span class="cs__number">1835</span>&nbsp;});&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;List&lt;Book&gt;&nbsp;GetBookList()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;BookList;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">interface metodlarını i&ccedil;eren sınıfımız i&ccedil;in mevcut metodların i&ccedil;ini ihtiyacımıza g&ouml;re değiştiriyoruz.&nbsp;<br>
Geliştirme işleminden sonra RESTbookService sınfının i&ccedil;i aşağıdaki gibi olacaktır.</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Runtime.Serialization;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.ServiceModel;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.ServiceModel.Activation;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;RESTbook&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;NOTE:&nbsp;You&nbsp;can&nbsp;use&nbsp;the&nbsp;&quot;Rename&quot;&nbsp;command&nbsp;on&nbsp;the&nbsp;&quot;Refactor&quot;&nbsp;menu</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;to&nbsp;change&nbsp;the&nbsp;class&nbsp;name&nbsp;&quot;RESTbookService&quot;&nbsp;in&nbsp;code,&nbsp;svc&nbsp;and&nbsp;config&nbsp;file&nbsp;together.</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;NOTE:&nbsp;In&nbsp;order&nbsp;to&nbsp;launch&nbsp;WCF&nbsp;Test&nbsp;Client&nbsp;for&nbsp;testing&nbsp;this&nbsp;service,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;please&nbsp;select&nbsp;RESTbookService.svc&nbsp;or&nbsp;RESTbookService.svc.cs</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;at&nbsp;the&nbsp;Solution&nbsp;Explorer&nbsp;and&nbsp;start&nbsp;debugging.</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Basically&nbsp;this&nbsp;code&nbsp;is&nbsp;developed&nbsp;for&nbsp;HTTP&nbsp;GET,&nbsp;PUT,&nbsp;POST&nbsp;&amp;&nbsp;DELETE&nbsp;operation.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[AspNetCompatibilityRequirements&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(RequirementsMode&nbsp;=&nbsp;AspNetCompatibilityRequirementsMode.Allowed)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ServiceBehavior(InstanceContextMode&nbsp;=&nbsp;InstanceContextMode.Single)]&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;RESTbookService&nbsp;:&nbsp;IRESTbookService&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//public&nbsp;void&nbsp;DoWork(){}</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//POST&nbsp;operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Book&nbsp;CreateBook(Book&nbsp;book)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;bookNo&nbsp;=&nbsp;Repository.GetBookList().Last().BookNo;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;book.BookNo&nbsp;=&nbsp;bookNo&nbsp;&#43;&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Repository.GetBookList().Add(book);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;book;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//GET&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;List&lt;Book&gt;&nbsp;GetAllBooks()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Repository.GetBookList();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//GET&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Book&nbsp;GetAbook(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Repository.GetBookList().&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FirstOrDefault(b&nbsp;=&gt;&nbsp;b.BookNo.Equals(Convert.ToInt32(id)));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//PUT&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Book&nbsp;UpdateBook(<span class="cs__keyword">string</span>&nbsp;id,&nbsp;Book&nbsp;book)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Book&nbsp;bo&nbsp;=&nbsp;Repository.GetBookList().&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FirstOrDefault(b&nbsp;=&gt;&nbsp;b.BookNo.Equals(Convert.ToInt32(id)));&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bo.BookName&nbsp;=&nbsp;book.BookName;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bo.PublicationYear&nbsp;=&nbsp;book.PublicationYear;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;bo;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//DELETE&nbsp;Operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;DeleteBook(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Repository.GetBookList().&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RemoveAll(e&nbsp;=&gt;&nbsp;e.BookNo.Equals(Convert.ToInt32(id)));&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__string">&quot;1&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__string">&quot;opps!&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ex.Message;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"><span id="ContentPlaceHolder1_LabelLongDesc">Bu aşamadan sonra projemizi &ccedil;alıştırıp test edebiliriz.<br>
<br>
Normal şartlarda aşağıdaki şekilde g&ouml;r&uuml;ld&uuml;ğ&uuml; gibi projemizde yer alan &quot;RESTbookService.svc&quot;&nbsp;&uuml;zerinde ctrl&#43;f5 ile &quot;Wcf Test Client&quot;<br>
uygulamasını kullanarak geliştirdiğimiz REST servisleri test edebiliriz.<br>
<br>
Sol taraftaki servislerden birini se&ccedil;ip testimizi yapabiliriz.</span><br>
<br>
<br>
</div>
<div class="endscriptcode"><img id="107292" src="107292-restbook_070_wcf_test_client.jpg" alt="" width="806" height="566"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span id="ContentPlaceHolder1_LabelLongDesc">Ancak uygulamamızı hem bir tarayıcı aracılığı ile test etmek hem de bir client tarafından<br>
REST servis kullanım modeli ile kullanabilmek i&ccedil;in projemizde birka&ccedil; değişiklik yapmamız gerekiyor.<br>
<br>
Aslında burada yapacaklarımız REST servislerini i&ccedil;eren uygulamamıza &quot;asp.net web&quot; karakteristiği kazandırma &ccedil;abası olacaktır.<br>
<br>
Bunun i&ccedil;in projemize bir ader Global.asax ekliyoruz.<br>
<br>
Aşağıdaki gibi gerekli değişiklikleri yapıyoruz.</span><br>
<br>
<img id="107293" src="107293-restbook_080_restasweb.jpg" alt="" width="842" height="451"></div>
<div class="endscriptcode"><br>
<br>
<br>
<span id="ContentPlaceHolder1_LabelLongDesc">Daha sonra web.config dosyamızda aşagdaki satıra aspNetCompatibilityEnabled=&quot;true&quot; ifadesini ekliyoruz.<br>
<br>
&lt;serviceHostingEnvironment multipleSiteBindingsEnabled=&quot;true&quot;&nbsp; /&gt;<br>
<br>
Ekleme işinden sonra &quot;serviceHostingEnvironment&quot; adlı element aşağıdaki gibi olacaktır.<br>
<br>
&lt;serviceHostingEnvironment multipleSiteBindingsEnabled=&quot;true&quot;&nbsp; aspNetCompatibilityEnabled=&quot;true&quot; /&gt;<br>
<br>
<br>
Daha sonra projemize web projesi muamelesi yapıp aşağıdaki şekilde olduğu gibi bir tarayıcı aracılığı ile t&uuml;m verileri tarayıcıda g&ouml;rebileceğimiz bir test yapabiliriz.</span><br>
<br>
<br>
<br>
<br>
<img id="107294" src="107294-restbook_090_getallinbrowser.jpg" alt="" width="393" height="657"><br>
<br>
</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Benzer şekilde tek bir kayıda ait bilgi i&ccedil;in aşağıdaki gibi bir test yapılabilir.</span><br>
<br>
<br>
<img id="107295" src="107295-restbook_100_getsingleinbrowser.jpg" alt="" width="408" height="248"><br>
<br>
<br>
<br>
<span id="ContentPlaceHolder1_LabelLongDesc">Bu aşamaden sonra bir istemci geliştirmek &ouml;rneğimizi daha anlamlı hale getirecektir.<br>
&nbsp; &nbsp; &nbsp;&nbsp;</span><br>
<br>
</p>
<h1><span id="ContentPlaceHolder1_LabelLongDesc"><br>
3) Nasıl kullanılır ?</span></h1>
<p><br>
<span id="ContentPlaceHolder1_LabelLongDesc">2. b&ouml;l&uuml;mde geliştirdiğimiz REST servisi kullanacak bir istemci(client) uygulama geliştiriyoruz.&nbsp;Bunun i&ccedil;in &quot;Console&quot;, &quot;Windows&quot;, veya &quot;Web&quot; uygulaması<br>
istemci olarak geliştirilebilir.<br>
&nbsp;<br>
Biz bu b&ouml;l&uuml;mde iki projeli bir &ccedil;&ouml;z&uuml;m geliştiriyoruz.&nbsp;<br>
&nbsp;<br>
Rest servislerle iletişimi sağlayacak bir &quot;ClassLibrary&quot; projesi ile bu &quot;library&quot; projesini kullanacak bir windows uygulaması geliştirelim.<br>
<br>
Windows projemizi i&ccedil;eren &ccedil;&ouml;z&uuml;m&uuml;m&uuml;z&uuml; aşağıdaki şekilde olduğu gibi oluturalım.</span><br>
<br>
<br>
<img id="107296" src="107296-restbook_0310.jpg" alt="" width="800" height="537"><br>
<br>
<span id="ContentPlaceHolder1_LabelLongDesc">&quot;ClassLibrary&quot; projemizi de yine aşağıdaki şekilde olduğu gibi &ccedil;&ouml;z&uuml;m&uuml;m&uuml;ze ekleyelim.</span><br>
<br>
<br>
<img id="107297" src="107297-restbook_0320_classlib.jpg" alt="" width="760" height="447"><br>
<br>
<br>
</p>
<table>
<tbody>
<tr>
<td>&quot;Windows&quot; uygulamamızın ana formu &uuml;zerine g&ouml;r&uuml;nt&uuml;deki butonları yerleştirdikten sonra her butona tıklandığında&nbsp;a&ccedil;ılacak formları da projemize aşağıdaki&nbsp;g&ouml;r&uuml;nt&uuml;deki gibi ekleyelim.</td>
</tr>
<tr>
<td>&nbsp; &nbsp; &nbsp;&nbsp;</td>
</tr>
</tbody>
</table>
<p><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span></p>
<table>
<tbody>
<tr>
<td>&nbsp;</td>
</tr>
</tbody>
</table>
<p><img id="107298" src="107298-restbook_0330_winscreen.jpg" alt="" width="779" height="440"></p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Daha sonra ClassLirary projemizi geliştiriyoruz. &quot;RESTbookProvider&quot; adlı proje ile amacımız REST servislerin kullanımı i&ccedil;in başka projeler tarafından da&nbsp;<br>
kullanılabilecek bir &quot;provider&quot; geliştirmektir.<br>
<br>
Bunun i&ccedil;in aşağıdaki gibi bir fonksiyona ihtiyacımız olacaktır.</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__preproc">#region&nbsp;Functions</span>&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;DataTable&nbsp;xml2DataTable(<span class="cs__keyword">string</span>&nbsp;xmlStr)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;DataTable&nbsp;dt&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DataTable();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;DataSet&nbsp;dataSet&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DataSet();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;dataSet.ReadXml(<span class="cs__keyword">new</span>&nbsp;StringReader(xmlStr));&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//return&nbsp;single&nbsp;table&nbsp;inside&nbsp;of&nbsp;dataset</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(dataSet.Tables.Count&nbsp;&gt;&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dt&nbsp;=&nbsp;dataSet.Tables[<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;dt;&nbsp;
}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;
#endregion</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Sonrasında REST servislerinden t&uuml;m veriyi &ccedil;ekip ekrana basmak i&ccedil;in aşağıdaki bir kod geliştirebiliriz.&nbsp;</span><br>
<br>
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__preproc">&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;restOperations</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;uri&nbsp;=&nbsp;<span class="cs__string">&quot;http://localhost:30523/RESTbookService&quot;</span>;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;DataTable&nbsp;RESTgetALL()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;GET&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebRequest&nbsp;req&nbsp;=&nbsp;WebRequest.Create(uri)&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebRequest;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.KeepAlive&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.Method&nbsp;=&nbsp;Method.ToUpper();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebResponse&nbsp;resp&nbsp;=&nbsp;req.GetResponse()&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebResponse;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encoding&nbsp;enc&nbsp;=&nbsp;System.Text.Encoding.GetEncoding(<span class="cs__number">1254</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StreamReader&nbsp;loResponseStream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StreamReader(resp.GetResponseStream(),&nbsp;enc);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;ResponseStr&nbsp;=&nbsp;loResponseStream.ReadToEnd();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;loResponseStream.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resp.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataTable&nbsp;dt&nbsp;=&nbsp;xml2DataTable(ResponseStr);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;dt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTgetByid(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTpost(<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTput(<span class="cs__keyword">string</span>&nbsp;id,&nbsp;<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTdelete(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Yukarıdaki kod ile elde edilecek ekran aşağıdaki gibi olacaktır.</span></p>
<p>&nbsp;</p>
<p><img id="107299" src="107299-restbook_0340_wingetall.jpg" alt="" width="729" height="512"></p>
<p>&nbsp;</p>
<p><br>
<span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span></p>
<table>
<tbody>
<tr>
<td>Benzer şekilde tek bir kayıt &ccedil;ekmek (GET) i&ccedil;in aşağıdaki gibi bir kod geliştirilebilir.&nbsp;</td>
</tr>
<tr>
<td>&nbsp; &nbsp; &nbsp;&nbsp;</td>
</tr>
</tbody>
</table>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__preproc">&nbsp;&nbsp;&nbsp;#region&nbsp;restOperations</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;uri&nbsp;=&nbsp;<span class="cs__string">&quot;http://localhost:30523/RESTbookService&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;DataTable&nbsp;RESTgetALL()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTgetByid(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;GET&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;uriStr&nbsp;=&nbsp;uri&nbsp;&#43;&nbsp;<span class="cs__string">&quot;/&quot;</span>&nbsp;&#43;&nbsp;id;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebRequest&nbsp;req&nbsp;=&nbsp;WebRequest.Create(uriStr)&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebRequest;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.KeepAlive&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.Method&nbsp;=&nbsp;Method.ToUpper();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebResponse&nbsp;resp&nbsp;=&nbsp;req.GetResponse()&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebResponse;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encoding&nbsp;enc&nbsp;=&nbsp;System.Text.Encoding.GetEncoding(<span class="cs__number">1254</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StreamReader&nbsp;loResponseStream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StreamReader(resp.GetResponseStream(),&nbsp;enc);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;loResponseStream.ReadToEnd();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;loResponseStream.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resp.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;ERROR&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ex.Message.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ResponseStr;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTpost(<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTput(<span class="cs__keyword">string</span>&nbsp;id,&nbsp;<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTdelete(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">B&ouml;ylece aşağıdaki gibi bir ekran g&ouml;r&uuml;nt&uuml;s&uuml; elde edilecektir.</span><br>
<br>
<br>
<img id="107300" src="107300-restbook_0340_wingetall.jpg" alt="" width="729" height="512"></p>
<p><br>
<br>
</p>
<table>
<tbody>
<tr>
<td>Kayıt (POST) i&ccedil;in aşağıdaki gibi bir kod geliştirilebilir.<br>
&nbsp;</td>
</tr>
<tr>
<td>xmlns=&quot;http://schemas.datacontract.org/2004/07/RESTbook&quot; ifadesine dikkat edilmesi gerekir.</td>
</tr>
</tbody>
</table>
<p><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span><br>
<br>
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__preproc">&nbsp;#region&nbsp;restOperations</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;uri&nbsp;=&nbsp;<span class="cs__string">&quot;http://localhost:30523/RESTbookService&quot;</span>;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;DataTable&nbsp;RESTgetALL()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTgetByid(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTpost(<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;content;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;POST&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebRequest&nbsp;req&nbsp;=&nbsp;WebRequest.Create(uri)&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebRequest;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.KeepAlive&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.Method&nbsp;=&nbsp;Method.ToUpper();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//--------------&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&lt;Book&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&lt;BookName&gt;The&nbsp;Grapes&nbsp;of&nbsp;Wrath&lt;/BookName&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&lt;BookNo&gt;0&lt;/BookNo&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&lt;PublicationYear&gt;1939&lt;/PublicationYear&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&lt;/Book&gt;</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;content&nbsp;=&nbsp;<span class="cs__string">&quot;&lt;Book&nbsp;xmlns=\&quot;http://schemas.datacontract.org/2004/07/RESTbook\&quot;&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;BookName&gt;&quot;</span>&nbsp;&#43;&nbsp;bookName&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&lt;/BookName&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;BookNo&gt;0&lt;/BookNo&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;PublicationYear&gt;&quot;</span>&nbsp;&#43;&nbsp;pubYear&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&lt;/PublicationYear&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/Book&gt;&quot;</span>;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">byte</span>[]&nbsp;buffer&nbsp;=&nbsp;Encoding.ASCII.GetBytes(content);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.ContentLength&nbsp;=&nbsp;buffer.Length;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.ContentType&nbsp;=&nbsp;<span class="cs__string">&quot;text/xml&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stream&nbsp;PostData&nbsp;=&nbsp;req.GetRequestStream();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PostData.Write(buffer,&nbsp;<span class="cs__number">0</span>,&nbsp;buffer.Length);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PostData.Close();&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//--------------</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebResponse&nbsp;resp&nbsp;=&nbsp;req.GetResponse()&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebResponse;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encoding&nbsp;enc&nbsp;=&nbsp;System.Text.Encoding.GetEncoding(<span class="cs__number">1254</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StreamReader&nbsp;loResponseStream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StreamReader(resp.GetResponseStream(),&nbsp;enc);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;loResponseStream.ReadToEnd();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;loResponseStream.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resp.Close();&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;&nbsp;<span class="cs__string">&quot;ERROR&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ex.Message.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ResponseStr;&nbsp;
&nbsp;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTput(<span class="cs__keyword">string</span>&nbsp;id,&nbsp;<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTdelete(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p><span id="ContentPlaceHolder1_LabelLongDesc">G&uuml;ncelleme i&ccedil;in (PUT) aşağıdaki bir kod geliştirilebilir.</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__preproc">&nbsp;#region&nbsp;restOperations</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;uri&nbsp;=&nbsp;<span class="cs__string">&quot;http://localhost:30523/RESTbookService&quot;</span>;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;DataTable&nbsp;RESTgetALL()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTgetByid(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTpost(<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//..</span>&nbsp;
&nbsp;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTput(<span class="cs__keyword">string</span>&nbsp;id,&nbsp;<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;uriStr&nbsp;=&nbsp;uri&nbsp;&#43;&nbsp;<span class="cs__string">&quot;/&quot;</span>&nbsp;&#43;&nbsp;id;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;content;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;PUT&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebRequest&nbsp;req&nbsp;=&nbsp;WebRequest.Create(uriStr)&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebRequest;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.KeepAlive&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.Method&nbsp;=&nbsp;Method.ToUpper();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//--------------&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&lt;Book&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&lt;BookName&gt;The&nbsp;Grapes&nbsp;of&nbsp;Wrath&lt;/BookName&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&lt;BookNo&gt;2&lt;/BookNo&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&lt;PublicationYear&gt;1939&lt;/PublicationYear&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&lt;/Book&gt;</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;content&nbsp;=&nbsp;<span class="cs__string">&quot;&lt;Book&nbsp;xmlns=\&quot;http://schemas.datacontract.org/2004/07/RESTbook\&quot;&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;BookName&gt;&quot;</span>&nbsp;&#43;&nbsp;bookName&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&lt;/BookName&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;BookNo&gt;&quot;</span>&nbsp;&#43;&nbsp;id&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&lt;/BookNo&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;PublicationYear&gt;&quot;</span>&nbsp;&#43;&nbsp;pubYear&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&lt;/PublicationYear&gt;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/Book&gt;&quot;</span>;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">byte</span>[]&nbsp;buffer&nbsp;=&nbsp;Encoding.ASCII.GetBytes(content);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.ContentLength&nbsp;=&nbsp;buffer.Length;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.ContentType&nbsp;=&nbsp;<span class="cs__string">&quot;text/xml&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stream&nbsp;PostData&nbsp;=&nbsp;req.GetRequestStream();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PostData.Write(buffer,&nbsp;<span class="cs__number">0</span>,&nbsp;buffer.Length);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PostData.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//--------------</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebResponse&nbsp;resp&nbsp;=&nbsp;req.GetResponse()&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebResponse;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encoding&nbsp;enc&nbsp;=&nbsp;System.Text.Encoding.GetEncoding(<span class="cs__number">1254</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StreamReader&nbsp;loResponseStream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StreamReader(resp.GetResponseStream(),&nbsp;enc);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;loResponseStream.ReadToEnd();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;loResponseStream.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resp.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;ERROR&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ex.Message.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ResponseStr;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTdelete(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p><span id="ContentPlaceHolder1_LabelLongDesc">Silme (DELETE) i&ccedil;in aşağıdaki gibi bir kod geliştirilebilir.</span><br>
<br>
<br>
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">D&uuml;zenle</span>|<span class="pluginRemoveHolderLink">Kaldır</span></div>
<div class="preview">
<pre class="csharp"><span class="cs__preproc">&nbsp;#region&nbsp;restOperations</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;uri&nbsp;=&nbsp;<span class="cs__string">&quot;http://localhost:30523/RESTbookService&quot;</span>;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;DataTable&nbsp;RESTgetALL()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTgetByid(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//...</span>&nbsp;
&nbsp;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTpost(<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//..</span>&nbsp;
&nbsp;&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTput(<span class="cs__keyword">string</span>&nbsp;id,&nbsp;<span class="cs__keyword">string</span>&nbsp;bookName,&nbsp;<span class="cs__keyword">string</span>&nbsp;pubYear)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//..</span>&nbsp;
}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;RESTdelete(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;uriStr&nbsp;=&nbsp;uri&nbsp;&#43;&nbsp;<span class="cs__string">&quot;/&quot;</span>&nbsp;&#43;&nbsp;id;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;Method&nbsp;=&nbsp;<span class="cs__string">&quot;DELETE&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebRequest&nbsp;req&nbsp;=&nbsp;WebRequest.Create(uriStr)&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebRequest;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.KeepAlive&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;req.Method&nbsp;=&nbsp;Method.ToUpper();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpWebResponse&nbsp;resp&nbsp;=&nbsp;req.GetResponse()&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpWebResponse;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encoding&nbsp;enc&nbsp;=&nbsp;System.Text.Encoding.GetEncoding(<span class="cs__number">1254</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StreamReader&nbsp;loResponseStream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StreamReader(resp.GetResponseStream(),&nbsp;enc);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;loResponseStream.ReadToEnd();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;loResponseStream.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resp.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseStr&nbsp;=&nbsp;<span class="cs__string">&quot;ERROR&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ex.Message.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;
}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p><br>
<br>
</p>
<p>&nbsp;</p>
<p><span id="ContentPlaceHolder1_LabelLongDesc">&nbsp;</span></p>
<table>
<tbody>
<tr>
<td>G&ouml;r&uuml;ld&uuml;ğ&uuml; &uuml;zere REST servisler i&ccedil;in adresler aşağıdaki gibi olmuştur.</td>
</tr>
<tr>
<td>&nbsp;</td>
</tr>
<tr>
<td>
<table>
<tbody>
<tr>
<td>&nbsp;</td>
<td><strong>Uri</strong></td>
<td><strong>Method</strong></td>
</tr>
<tr>
<td>&nbsp;</td>
<td>-------</td>
<td>----------------------------------------</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>POST</td>
<td>http://localhost:30523/RESTbookService/</td>
</tr>
<tr>
<td>id</td>
<td>GET</td>
<td>http://localhost:30523/RESTbookService/</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>GET</td>
<td>http://localhost:30523/RESTbookService/{ID}</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>PUT</td>
<td>http://localhost:30523/RESTbookService/{ID}</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>DELETE&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td>http://localhost:30523/RESTbookService/{ID}</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td>&nbsp;<br>
<br>
B&ouml;ylece bu &ccedil;alışmada REST servisin ne olduğu, nasıl geliştirilebileceği ve nasıl kullanılabileceği konularında bilgiler aktarılmıştır.&nbsp;<br>
<br>
Ayrıca REST servislerin MVC ile anılmaya başlanan &quot;WebApi&quot; i&ccedil;in temel olduğunu tekrar hatırlayalım.&nbsp;<br>
<br>
Umarım ilgilisi i&ccedil;in faydalı bir yazı olmuştur.&nbsp;</td>
</tr>
</tbody>
</table>
