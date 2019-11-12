# Menu JavaScript Ajax
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- AJAX
- SQL Server
- LINQ to SQL
- ASP.NET
- jQuery
- Javascript
- Visual Basic .NET
## Topics
- AJAX
- Menu
- Javascript
## Updated
- 06/04/2014
## Description

<p>&nbsp;</p>
<h1>Introduction</h1>
<p><em>Load javascript menu with ajax from client</em></p>
<p><em>Cargar menu javascript con ajax desde el cliente</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>1 Downloading Menu.Zip</em></p>
<p><em>2 Run sql script dbProyecto.sql server database&nbsp;</em></p>
<p><em>3 Change Connection string in web.config &quot;dbProyectoConnectionString&quot;&nbsp;</em></p>
<p><em>4 Compile</em></p>
<p>&nbsp;</p>
<p><em>1 Descargar Menu.Zip</em></p>
<p><em>2 Ejecutar script sql dbProyecto.sql en servidor de base de datos</em></p>
<p><em>3 Cambiar cadena de conexion en el web.config &quot;dbProyectoConnectionString&quot;</em></p>
<p><em>4 Compilar<br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>Upload a menu client so this can improve the application loading thanks to the work is done by the browser (client)</p>
<p>Cargar un men&uacute; de cliente de tal forma esto puede mejorar la carga del aplicativo gracias a que el trabajo lo realiza el navegador (Cliente)</p>
<p><em><img id="116196" src="116196-menu1.png" alt="" width="518" height="123"></em></p>
<p><em><img id="116197" src="116197-menu2.png" alt="" width="514" height="202"><br>
</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span><span class="hidden">vb</span>


<div class="preview">
<pre class="js"><span class="js__operator">function</span>&nbsp;Menu()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;menu;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.ajax(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;type:&nbsp;<span class="js__string">&quot;POST&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;<span class="js__string">&quot;Default.aspx/CargarMenu&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contentType:&nbsp;<span class="js__string">&quot;application/json;&nbsp;charset=utf-8&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dataType:&nbsp;<span class="js__string">&quot;json&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;success:&nbsp;<span class="js__operator">function</span>&nbsp;(msg)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;lsMenu&nbsp;=&nbsp;msg.d;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;_menu&nbsp;=&nbsp;lsMenu;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;_hijo;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;div&nbsp;=&nbsp;document.getElementById(<span class="js__string">'nav'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;listItems&nbsp;=&nbsp;[];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;str&nbsp;=&nbsp;<span class="js__string">''</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;str2&nbsp;=&nbsp;<span class="js__string">''</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(lsMenu.length&nbsp;&gt;&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;key&nbsp;<span class="js__operator">in</span>&nbsp;lsMenu)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(lsMenu[key].intIdMenu&nbsp;==&nbsp;lsMenu[key].intIdPadre)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_hijo&nbsp;=&nbsp;SubMenuVertical(_menu,&nbsp;lsMenu[key].intIdMenu);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str&nbsp;=&nbsp;<span class="js__string">'&lt;li&nbsp;class=&quot;top&quot;&gt;&lt;a&nbsp;href=&quot;'</span>&nbsp;&#43;&nbsp;lsMenu[key].strUrl&nbsp;&#43;&nbsp;<span class="js__string">'&quot;&nbsp;onclick=&quot;event.preventDefault();&quot;&nbsp;class=&quot;top_link&quot;&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(_hijo)&nbsp;<span class="js__brace">{</span>&nbsp;str&nbsp;=&nbsp;str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;span&nbsp;class=&quot;down&quot;&gt;'</span>;&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;str&nbsp;=&nbsp;str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;span&gt;'</span>;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str&nbsp;=&nbsp;str&nbsp;&#43;&nbsp;lsMenu[key].strDescripcion&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/span&gt;&lt;/a&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//CargarMenuVertical</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str&nbsp;=&nbsp;str&nbsp;&#43;&nbsp;MenuVertical(_menu,&nbsp;lsMenu[key].intIdMenu,&nbsp;_hijo);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str&nbsp;=&nbsp;str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/li&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;listItems.push(str);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;div.innerHTML&nbsp;=&nbsp;div.innerHTML&nbsp;&#43;&nbsp;listItems.join(<span class="js__string">''</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;No&nbsp;records&nbsp;found&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;error:&nbsp;<span class="js__operator">function</span>&nbsp;(XMLHttpRequest,&nbsp;textStatus,&nbsp;errorThrown)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//alert(textStatus);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
<span class="js__brace">}</span>&nbsp;
<span class="js__operator">function</span>&nbsp;MenuVertical(_menu,&nbsp;ID,&nbsp;MenVertical)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;_str&nbsp;=&nbsp;<span class="js__string">''</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(MenVertical)&nbsp;<span class="js__brace">{</span>&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;ul&nbsp;class=&quot;sub&quot;&gt;'</span>;&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;ul&gt;'</span>;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;key&nbsp;<span class="js__operator">in</span>&nbsp;_menu)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_MenuHo&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(_menu[key].intIdMenu&nbsp;!=&nbsp;_menu[key].intIdPadre&nbsp;&amp;&nbsp;_menu[key].intIdPadre&nbsp;==&nbsp;ID)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;li&gt;&lt;a&nbsp;href=&quot;'</span>&nbsp;&#43;&nbsp;_menu[key].strUrl&nbsp;&#43;&nbsp;<span class="js__string">'&quot;&nbsp;target=&quot;content&quot;&nbsp;onclick=&quot;event.preventDefault();&quot;&nbsp;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(SiMenuHorizotal(_menu,&nbsp;_menu[key].intIdMenu))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'class=&quot;fly&quot;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'&gt;'</span>&nbsp;&#43;&nbsp;_menu[key].strDescripcion&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/a&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//MenuHorizontal</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;MenuHorizontal(_menu,&nbsp;_menu[key].intIdMenu);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/li&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;_str&nbsp;=&nbsp;_str&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/ul&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;_str;&nbsp;
<span class="js__brace">}</span>&nbsp;
<span class="js__operator">function</span>&nbsp;SiMenuHorizotal(_menu,&nbsp;ID_)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;sw&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;key&nbsp;<span class="js__operator">in</span>&nbsp;_menu)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(_menu[key].intIdMenu&nbsp;!=&nbsp;_menu[key].intIdPadre&nbsp;&amp;&nbsp;_menu[key].intIdPadre&nbsp;==&nbsp;ID_&nbsp;&amp;&nbsp;sw&nbsp;==&nbsp;false)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sw&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;sw;&nbsp;
<span class="js__brace">}</span>&nbsp;
<span class="js__operator">function</span>&nbsp;MenuHorizontal(_menu,&nbsp;ID)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;str_&nbsp;=&nbsp;<span class="js__string">''</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;sw&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;key&nbsp;<span class="js__operator">in</span>&nbsp;_menu)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(_menu[key].intIdMenu&nbsp;!=&nbsp;_menu[key].intIdPadre&nbsp;&amp;&nbsp;_menu[key].intIdPadre&nbsp;==&nbsp;ID)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(sw&nbsp;==&nbsp;false)&nbsp;<span class="js__brace">{</span>&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;<span class="js__string">'&lt;ul&gt;'</span>;&nbsp;sw&nbsp;=&nbsp;true;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;<span class="js__string">'&lt;li&gt;&lt;a&nbsp;href=&quot;'</span>&nbsp;&#43;&nbsp;_menu[key].strUrl&nbsp;&#43;&nbsp;<span class="js__string">'&quot;&nbsp;target=&quot;content&quot;&nbsp;onclick=&quot;event.preventDefault();&quot;&nbsp;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(SiMenuHorizotal(_menu,&nbsp;_menu[key].intIdMenu))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;<span class="js__string">'class=&quot;fly&quot;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;<span class="js__string">'&gt;'</span>&nbsp;&#43;&nbsp;_menu[key].strDescripcion&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/a&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//MenuHorizontal</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;MenuHorizontal(_menu,&nbsp;_menu[key].intIdMenu);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/li&gt;'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(sw)&nbsp;<span class="js__brace">{</span>&nbsp;str_&nbsp;=&nbsp;str_&nbsp;&#43;&nbsp;<span class="js__string">'&lt;/ul&gt;'</span>;&nbsp;sw&nbsp;=&nbsp;false;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;str_;&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;SubMenuVertical(_menu,&nbsp;ID)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;sw&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;str&nbsp;=&nbsp;<span class="js__string">''</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;key&nbsp;<span class="js__operator">in</span>&nbsp;_menu)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(_menu[key].intIdMenu&nbsp;==&nbsp;_menu[key].intIdPadre&nbsp;&amp;&nbsp;sw&nbsp;==&nbsp;false)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;_key&nbsp;<span class="js__operator">in</span>&nbsp;_menu)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(_menu[_key].intIdMenu&nbsp;!=&nbsp;_menu[_key].intIdPadre&nbsp;&amp;&nbsp;_menu[_key].intIdPadre&nbsp;==&nbsp;ID&nbsp;&amp;&nbsp;sw&nbsp;==&nbsp;false)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sw&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;sw;&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Menu.7z</em> </li></ul>
<p>&nbsp;</p>
<h1>Information</h1>
<p>New trends in the implementation of development methodologies customers you are marking the starting point for new developments and nothing better than having a menu that is loaded asynchronous using.</p>
<p>One of the things is that we use supplements and open source technologies such as jQuery and other free framework</p>
<p>En las nuevas tendencias de desarrollo la implementaci&oacute;n de metodologias clientes estas marcando el punto de partida en los nuevos desarrollos y nada mejor que tener un menu que se cargue asincronamente.</p>
<p>Una de las cosas es que utilizamos complementos y tecnolog&iacute;as libres como lo son jquery y otros framework gratuitos</p>
