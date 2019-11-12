# Reloj Digital
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
## Topics
- C#
## Updated
- 01/15/2017
## Description

<h1>Introduction</h1>
<p>Sencillo programa para la inserci&oacute;n de un reloj digital en cualquiera de nuestras aplicaciones.</p>
<p><span id="mt8" class="sentence">El&nbsp;<span class="selflink">ToString<span class="mtpsTagOuterHtml">(String)</span></span>&nbsp;m&eacute;todo devuelve la representaci&oacute;n de cadena de un valor de fecha y hora en un formato espec&iacute;fico
 que usa las convenciones de formato de la referencia cultural actual; para obtener m&aacute;s informaci&oacute;n, consulte&nbsp;<span>CultureInfo.CurrentCulture</span>.</span></p>
<p><span id="mt9" class="sentence">El&nbsp;<em>format</em>&nbsp;par&aacute;metro debe contener un car&aacute;cter especificador de formato &uacute;nico (vea&nbsp;Cadenas con formato de fecha y hora est&aacute;ndar) o un modelo de formato personalizado (vea&nbsp;Cadenas
 con formato de fecha y hora personalizado) que define el formato de la cadena devuelta.</span><span id="mt10" class="sentence">&nbsp;Si&nbsp;<em>format</em>&nbsp;es&nbsp;<strong>null</strong>&nbsp;o una cadena vac&iacute;a, el especificador de formato
 general &quot;G&quot;, se utiliza.</span></p>
<p><span id="mt11" class="sentence">Algunos usos de este m&eacute;todo son:</span></p>
<ul class="unordered">
<li>
<p><span id="mt12" class="sentence">C&oacute;mo obtener una cadena que muestra la fecha y hora en formato de hora y fecha corta de la referencia cultural actual.</span><span id="mt13" class="sentence">&nbsp;Para ello, utilice el especificador de formato
 &quot;G&quot;.</span></p>
</li><li>
<p><span id="mt14" class="sentence">C&oacute;mo obtener una cadena que contiene el mes y el a&ntilde;o.</span><span id="mt15" class="sentence">&nbsp;Para ello, utilice la cadena de formato &quot;MM/aaaa&quot;.</span><span id="mt16" class="sentence">&nbsp;La
 cadena de formato utiliza el separador de fecha de la referencia cultural actual.</span></p>
</li><li>
<p><span id="mt17" class="sentence">C&oacute;mo obtener una cadena que contiene la fecha y hora en un formato concreto.</span><span id="mt18" class="sentence">&nbsp;Por ejemplo, el &quot;MM/dd/yyyyHH:mm&quot; cadena de formato muestra la cadena de fecha y hora
 en un formato fijo como &quot;19 2013 / / 03 / / 18:06&quot;.</span><span id="mt19" class="sentence">&nbsp;Utiliza la cadena de formato &quot;/&quot; como un separador de fecha fija, independientemente de la configuraci&oacute;n espec&iacute;fica de la referencia cultural.</span></p>
</li><li>
<p><span id="mt20" class="sentence">Obtenci&oacute;n de una fecha en un formato comprimido podr&iacute;a usarse para serializar una cadena de fecha.</span><span id="mt21" class="sentence">&nbsp;Por ejemplo, la cadena de formato &quot;AAAAMMDD&quot; muestra un
 a&ntilde;o de cuatro d&iacute;gitos seguido por un mes de dos d&iacute;gitos y un d&iacute;a de dos d&iacute;gitos sin separador de fecha.</span></p>
</li></ul>
<p>&nbsp;</p>
<p>https://msdn.microsoft.com/es-es/library/zdtaw1bw(v=vs.110).aspx</p>
<h1>Description</h1>
<p>&nbsp;</p>
<p><img id="168129" src="168129-captura2.png" alt="" width="329" height="237" style="display:block; margin-left:auto; margin-right:auto"></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Editar script</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Collections.Generic.aspx" target="_blank" title="Auto generated link to System.Collections.Generic">System.Collections.Generic</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.ComponentModel.aspx" target="_blank" title="Auto generated link to System.ComponentModel">System.ComponentModel</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Data.aspx" target="_blank" title="Auto generated link to System.Data">System.Data</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Drawing.aspx" target="_blank" title="Auto generated link to System.Drawing">System.Drawing</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Linq.aspx" target="_blank" title="Auto generated link to System.Linq">System.Linq</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Text.aspx" target="_blank" title="Auto generated link to System.Text">System.Text</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Threading.Tasks.aspx" target="_blank" title="Auto generated link to System.Threading.Tasks">System.Threading.Tasks</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Windows.Forms.aspx" target="_blank" title="Auto generated link to System.Windows.Forms">System.Windows.Forms</a>;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;Reloj_digital_I&nbsp;
&nbsp;{&nbsp;
&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;Form1&nbsp;:&nbsp;Form&nbsp;
&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Form1()&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Form1_Load(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;timer1_Tick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;label1.Text&nbsp;=&nbsp;DateTime.Now.ToLongTimeString();&nbsp;
&nbsp;&nbsp;&nbsp;label2.Text&nbsp;=&nbsp;DateTime.Now.ToShortDateString();&nbsp;
&nbsp;&nbsp;&nbsp;label3.Text&nbsp;=&nbsp;DateTime.Now.ToString(<span class="cs__string">&quot;dddd&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;}&nbsp;
&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>source code file name #1 - summary for this source code file.</em> </li><li><em><em>source code file name #2 - summary for this source code file.</em></em>
</li></ul>
<h1>More Information</h1>
<p><em>For more information on X, see ...?</em></p>
