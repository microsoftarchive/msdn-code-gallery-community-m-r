# Reconocimiento de Voz
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
- Visual C#
## Topics
- C#
- Windows Forms
## Updated
- 01/15/2017
## Description

<h1>Introduction</h1>
<p><span id="mt2" class="sentence">El software de la tecnolog&iacute;a de voz de escritorio de Windows ofrece una infraestructura de reconocimiento de voz b&aacute;sicas que digitaliza se&ntilde;ales ac&uacute;stica y recupera las palabras y los elementos
 de voz desde la entrada de audio.</span></p>
<p><span id="mt3" class="sentence">Las aplicaciones utilizan el&nbsp;<span class="selflink"><a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Speech.Recognition.aspx" target="_blank" title="Auto generated link to System.Speech.Recognition">System.Speech.Recognition</a></span>&nbsp;espacio de nombres para obtener acceso y ampliar esta tecnolog&iacute;a de reconocimiento de voz b&aacute;sica mediante la
 definici&oacute;n de algoritmos para identificar y actuar sobre los patrones de palabras o frases espec&iacute;ficas y administrar el comportamiento en tiempo de ejecuci&oacute;n de esta infraestructura de voz.</span></p>
<p>El reconocimiento de voz incluye un tiempo de ejecuci&oacute;n de voz, varias API de reconocimiento para programar el tiempo de ejecuci&oacute;n, gram&aacute;ticas listas para usar para el dictado y la b&uacute;squeda en Internet, y una interfaz de usuario
 predeterminada del sistema que ayuda a los usuarios a descubrir y usar las funciones de reconocimiento de voz.</p>
<h2 id="configurar-la-fuente-de-audio">Configurar la fuente de audio</h2>
<p>Comprueba que el dispositivo tenga un micr&oacute;fono o un equivalente.</p>
<p>Configura la funci&oacute;n&nbsp;<span>Micr&oacute;fono</span>&nbsp;del dispositivo (<span>DeviceCapability</span>) en el&nbsp;Manifiesto del paquete de la aplicaci&oacute;n&nbsp;(archivo&nbsp;<span>package.appxmanifest</span>) para obtener acceso a la fuente
 de audio del micr&oacute;fono. Esto permite que la aplicaci&oacute;n grabe audio con los micr&oacute;fonos conectados.</p>
<p>Consulta el tema sobre&nbsp;declaraciones de funcionalidades de aplicaci&oacute;n.</p>
<h2 id="reconocer-la-entrada-de-voz">Reconocer la entrada de voz</h2>
<p>Una&nbsp;<em>restricci&oacute;n</em>&nbsp;define las palabras y las frases (vocabulario) que una aplicaci&oacute;n reconoce en una entrada de voz. Las restricciones son fundamentales para el reconocimiento de voz y mejoran la precisi&oacute;n del reconocimiento
 de voz de tu aplicaci&oacute;n.</p>
<p><span class="sentence">https://msdn.microsoft.com/es-sv/library/system.speech.recognition.aspx?tduid=(9e470a736dcc454cedb3e6175a4911fd)(256380)(2459594)(TnL5HPStwNw-QMsorr4C3E8RNLMzqgHxBg)()<br>
</span></p>
<p><span style="font-size:2em">Description</span></p>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold"><img id="168132" src="168132-captura1.png" alt="" width="553" height="252" style="display:block; margin-left:auto; margin-right:auto"></span></p>
<p><span style="font-size:20px; font-weight:bold"><br>
</span></p>
<p><img id="168133" src="168133-captura.png" alt="" width="640" height="444"></p>
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
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/es-ES/library/System.Speech.Recognition.aspx" target="_blank" title="Auto generated link to System.Speech.Recognition">System.Speech.Recognition</a>;&nbsp;<span class="cs__com">//&nbsp;Para&nbsp;el&nbsp;reconocimiento&nbsp;de&nbsp;voz</span>&nbsp;
&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;Reconocimiento_de_voz_1&nbsp;
&nbsp;{&nbsp;
&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;Form1&nbsp;:&nbsp;Form&nbsp;
&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__com">//&nbsp;Inicializamos&nbsp;motor&nbsp;de&nbsp;reconocimiento.</span>&nbsp;
&nbsp;&nbsp;SpeechRecognitionEngine&nbsp;reconocimiento_de_voz&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SpeechRecognitionEngine();&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;palabras;&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Form1()&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;button1_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;<span class="cs__com">//Boton&nbsp;escuchar.&nbsp;Configuraci&oacute;n&nbsp;del&nbsp;reconocimiento</span>&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__com">//Inicia&nbsp;la&nbsp;escucha&nbsp;con&nbsp;el&nbsp;dispositivo&nbsp;de&nbsp;entrada&nbsp;de&nbsp;audio&nbsp;predeterminado</span>&nbsp;
&nbsp;&nbsp;&nbsp;reconocimiento_de_voz.SetInputToDefaultAudioDevice();&nbsp;<span class="cs__com">//&nbsp;Usaremos&nbsp;el&nbsp;microfono&nbsp;predeterminado&nbsp;del&nbsp;sistema</span>&nbsp;
&nbsp;&nbsp;&nbsp;reconocimiento_de_voz.LoadGrammar(<span class="cs__keyword">new</span>&nbsp;DictationGrammar());&nbsp;<span class="cs__com">//Carga&nbsp;la&nbsp;gramatica&nbsp;de&nbsp;Windows</span>&nbsp;
&nbsp;&nbsp;&nbsp;reconocimiento_de_voz.SpeechRecognized&nbsp;&#43;=&nbsp;te_escucho;&nbsp;<span class="cs__com">//&nbsp;Controlador&nbsp;de&nbsp;eventos.&nbsp;Se&nbsp;ejecutara&nbsp;al&nbsp;reconocer</span>&nbsp;
&nbsp;&nbsp;&nbsp;reconocimiento_de_voz.RecognizeAsync(RecognizeMode.Multiple);&nbsp;<span class="cs__com">//Iniciamos&nbsp;reconocimiento</span>&nbsp;
&nbsp;&nbsp;&nbsp;label1.Text&nbsp;=&nbsp;<span class="cs__string">&quot;Te&nbsp;estoy&nbsp;escuchando&nbsp;cuentame:&nbsp;&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;te_escucho(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;SpeechRecognizedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;palabras&nbsp;=&nbsp;e.Result.Text;&nbsp;<span class="cs__com">//&nbsp;La&nbsp;variable&nbsp;palabras&nbsp;del&nbsp;tipo&nbsp;string&nbsp;toma&nbsp;las&nbsp;palabras&nbsp;reconocidas.</span>&nbsp;
&nbsp;&nbsp;&nbsp;textBox1.Text&nbsp;=&nbsp;palabras;&nbsp;<span class="cs__com">//&nbsp;Muestra&nbsp;las&nbsp;palabras&nbsp;reconocidas&nbsp;en&nbsp;el&nbsp;textbox</span>&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;button3_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;<span class="cs__com">//&nbsp;Boton&nbsp;detener&nbsp;escucha</span>&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;reconocimiento_de_voz.RecognizeAsyncStop();&nbsp;<span class="cs__com">//Detiene&nbsp;la&nbsp;escucha</span>&nbsp;
&nbsp;&nbsp;&nbsp;textBox1.Clear();&nbsp;<span class="cs__com">//limpia&nbsp;el&nbsp;textbox</span>&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;button2_Click(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;<span class="cs__com">//&nbsp;Boton&nbsp;Salir</span>&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;Application.Exit();&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;}&nbsp;
&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
