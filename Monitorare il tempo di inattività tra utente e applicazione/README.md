# Monitorare il tempo di inattività tra utente e applicazione
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- COM
- C#
- WPF
- XAML
- .NET Framework
- Visual Basic .NET
- .NET Framework 4.0
## Topics
- Interop
- WPF
- XAML
- COM Interop
## Updated
- 04/15/2012
## Description

<h1>Introduction</h1>
<div><em><span style="color:#008000; font-family:Arial; font-size:xx-small"><span style="color:#008000; font-family:Arial; font-size:xx-small"><span style="color:#008000; font-family:Arial; font-size:xx-small">
<div><span style="color:#000000">In questo articolo verr&agrave; spiegato come monitorare il tempo in cui l'utente non interagisce con l'applicazione&nbsp; mediante l'utilizzo delle funzione api di Windows.</span></div>
</span></span></span></em></div>
<h1><span>Building the Sample</span></h1>
<div><em>Per poter eseguire questo esempio e necessario avere il framework 4.0 installato sul properio pc.</em></div>
<div><span style="font-size:20px; font-weight:bold">Description</span></div>
<div><em><span style="color:#008000; font-family:Arial; font-size:xx-small"><span style="color:#008000; font-family:Arial; font-size:xx-small"><span style="color:#008000; font-family:Arial; font-size:xx-small">&nbsp;</span></span></span></em></div>
<div><em>
<div><em><span style="color:#008000; font-family:Arial; font-size:xx-small"><span style="color:#008000; font-family:Arial; font-size:xx-small"><span style="color:#008000; font-family:Arial; font-size:xx-small">
<div><span style="color:#000000">L&rsquo;esempio che andremo ad implementare lo faremo tramite il Visual Studio 2010, framework di destinazione 4.0.Tutto questo &egrave; fattibile come sopramenzionato mediante la libreria user32.dll presente nativamente nel
 sistema operativo Microsoft Windows.Il codice seguente utilizza tale libreria sfruttando la funzione GetLastInputInfo.GetLastInputInfo &egrave; una funzione applicabile solo su software per Windows, (quindi non su siti Web). GetLastInputInfo restituisce una
 valore di tipo booleano ,se la funzione fallisce restituisce zero, altrimenti un qualunque valore diverso da zero. Questo valore espresso in millisecondi sar&agrave; in fine il tempo di inattivit&agrave; dell'utente con l'applicazione.L&rsquo;applicazione
 sulla quale eseguiremo l'esempio sar&agrave; di tipo WPF come tecnologia , linguaggio di sviluppo Microsoft VisualBasic Net.Diamo solamente una breve introduzione sulla tecnologia che utilizzeremo per creare il nostro esempio, WPF &egrave; l&rsquo;acronimo
 di Windows Presentation Foundation ed &egrave; stato introdotto nel .NET Framework 3.0 uscito nel Gennaio 2007 con Windows Vista. Tramite WPF si possono creare delle applicazioni desktop con grafiche accattivanti&nbsp; tramite il linguaggio XAML, che altro
 non &egrave; che un XML (la base &egrave; quella) con l&rsquo;aggiunta di tanti nuovi tag tra oggetti, stili, template triggers e molto altro.Per un maggiore approfondimento di questo linguaggio consultare la documentazione online di MSDN Library.Tornando
 al nostro esempio ,una volta che abbiamo aperto Microsoft Visual Studio 2010 e creata l&rsquo;applicazione di tipo WPF, in automatico saranno creati da VisualStudio due file , MainWindow.xaml e MainWindow.xaml.vb.Il primo &egrave; il file che contiene il markup,
 ossia la parte grafica creata mediante XAML , da ricordare che XAML e detto anche codice dichiarativo , mentre il secondo viene chiamato file di code-behind , con la quale gestiamo gli eventi degli oggetti creati mediante XAML ed altre funzioni secondo le
 proprie esigenze.</span></div>
</span></span></span></em></div>
</em>
<div></div>
</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Modifica script</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span><span class="hidden">vb</span>


<div class="preview">
<pre class="csharp"><span class="cs__com">//Richiamo dll del framework</span> 
<span class="cs__keyword">using</span> System; 
<span class="cs__keyword">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Windows.aspx" target="_blank" title="Auto generated link to System.Windows">System.Windows</a>; 
<span class="cs__com">//Su quest'ultimo file ossia MainWindow.xaml.vb dobbiamo importare i seguenti namespaces:</span> 
<span class="cs__keyword">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Runtime.InteropServices.aspx" target="_blank" title="Auto generated link to System.Runtime.InteropServices">System.Runtime.InteropServices</a>; 
<span class="cs__keyword">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Windows.Threading.aspx" target="_blank" title="Auto generated link to System.Windows.Threading">System.Windows.Threading</a>; 
 
<span class="cs__com">//Lo spazio dei nomi <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Runtime.InteropServices.aspx" target="_blank" title="Auto generated link to System.Runtime.InteropServices">System.Runtime.InteropServices</a> fornisce un'ampia variet&agrave; di membri che supportano l'interoperabilit&agrave; COM e servizi di chiamata al sistema operativo.</span> 
<span class="cs__com">//Lo spazio dei nomi <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Windows.Threading.aspx" target="_blank" title="Auto generated link to System.Windows.Threading">System.Windows.Threading</a> invece, contiene tipi per il supporto del sistema di threading Windows Presentation Foundation (WPF).</span> 
<span class="cs__com">//Anche in questo caso per un maggiore approfondimento consultare la documentazione online di MSDN Library.</span> 
 
<span class="cs__com">//Classe MainWindow</span> 
<span class="cs__keyword">namespace</span> WpfApplication2 
{ 
    <span class="cs__com">/// &lt;summary&gt;</span> 
    <span class="cs__com">/// Logica di interazione per MainWindow.xaml</span> 
    <span class="cs__com">/// &lt;/summary&gt;</span> 
    <span class="cs__keyword">public</span> partial <span class="cs__keyword">class</span> MainWindow : Window 
    { 
        <span class="cs__keyword">public</span> MainWindow() 
        { 
            InitializeComponent(); 
        } 
 
        <span class="cs__com">//A livello di modulo dichiariamo una variabile di tipo integer , la chiamiamo totaltime e la inizializziamo con valore zero.</span> 
        <span class="cs__com">//Totaltime ci indicher&agrave; per quanti secondi noi non interagiamo con tastiera e mouse.</span> 
        <span class="cs__keyword">int</span> totaltime = <span class="cs__number">0</span>; 
 
        <span class="cs__com">//Dichiariamo una nuova istanza della struttura LASTINPUTINFO, contenuta in GetLastInputInfo</span> 
        LASTINPUTINFO lastInputInf = <span class="cs__keyword">new</span> LASTINPUTINFO(); 
        <span class="cs__com">//Il codice seguente richiama la libreria user32.dll e la funzione GetLastInputInfo.</span> 
        [DllImport(<span class="cs__string">&quot;user32.dll&quot;</span>)] 
        <span class="cs__keyword">public</span> <span class="cs__keyword">static</span> <span class="cs__keyword">extern</span> <span class="cs__keyword">bool</span> GetLastInputInfo(<span class="cs__keyword">ref</span> LASTINPUTINFO plii); 
 
        <span class="cs__com">//L'attributo DllImport &egrave; molto utile quando si riutilizza del codice non gestito esistente in un'applicazione gestita.</span> 
        <span class="cs__com">//Come e possibile notare all'interno della struttura LASTINPUTINFO troviamo il termine MarshalAs.</span> 
        <span class="cs__com">//Marshal si occupa di eseguire il marshalling dei dati tra codice gestito e non gestito.</span> 
        [StructLayout(LayoutKind.Sequential)] 
        <span class="cs__keyword">public</span> <span class="cs__keyword">struct</span> LASTINPUTINFO 
        { 
            [MarshalAs(UnmanagedType.U4)] 
            <span class="cs__keyword">public</span> <span class="cs__keyword">int</span> cbSize; 
            [MarshalAs(UnmanagedType.U4)] 
            <span class="cs__keyword">public</span> <span class="cs__keyword">int</span> dwTime; 
        } 
 
        <span class="cs__com">//Evento Loaded della Classe MainWindow.</span> 
        <span class="cs__com">//All'interno dell'evento Loaded viene dichiarato un nuovo oggetto DispatcherTimer </span> 
        <span class="cs__com">//abbinato ad un evento Tick , tale evento richiama ad ogni intervento il metodo DisplayTime il quale    aggiorna un oggetto label visualizzando cosi il tempo di inattivit&agrave; dell'utente con l'applicazione.</span> 
        <span class="cs__com">//Da notare che in Wpf non esiste il controllo Timer , tale controllo &egrave; disponibile nelle applicazioni di  tipo WindowsForm , ma come spiegato in precedenza essendo un applicazione di tipo WPF , gestiamo il tutto con il controllo DispatcherTimer integrato nello spazio dei nomi System.Windows.Threading.</span> 
        <span class="cs__keyword">private</span> <span class="cs__keyword">void</span> MainWindow_Loaded(<span class="cs__keyword">object</span> sender, <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Windows.RoutedEventArgs.aspx" target="_blank" title="Auto generated link to System.Windows.RoutedEventArgs">System.Windows.RoutedEventArgs</a> e) 
        { 
            DispatcherTimer dt = <span class="cs__keyword">new</span> DispatcherTimer(); 
            dt.Tick &#43;= dispatcherTimer_Tick; 
            dt.Interval = <span class="cs__keyword">new</span> TimeSpan(<span class="cs__number">0</span>, <span class="cs__number">0</span>, <span class="cs__number">1</span>); 
            dt.Start(); 
        } 
 
        <span class="cs__com">//Evento Tick dell'oggetto DispatcherTimer</span> 
        <span class="cs__com">//Viene richiamato il metodo DisplayTime() ogni secondo , questo dipende dal valore di Intervallo impostato nella propriet&agrave; Interval di dispatcherTimer .</span> 
        <span class="cs__keyword">public</span> <span class="cs__keyword">void</span> dispatcherTimer_Tick(<span class="cs__keyword">object</span> sender, EventArgs e) 
        { 
            DisplayTime(); 
        } 
 
        <span class="cs__com">//Funzione GetLastInputTime.</span> 
        <span class="cs__com">//Questa funzione restituisce ed assegna alla variabile idletime il valore del tempo  di inattivit&agrave; trascorso,</span> 
        <span class="cs__com">//Il tutto convertito in secondi perch&egrave; inizialmente il valore recuperato da questa intruzione</span> 
        <span class="cs__com">//di codice idletime = Environment.TickCount - lastInputInf.dwTime e espresso in millisecondi.</span> 
        <span class="cs__com">//All&rsquo;interno della classe MainWindows, dichiariamo anche una  variabile di tipo integer e la chiamiamo idletime la quale verr&agrave; valorizzata mediante la Funzione GetLastInputTime.</span> 
        <span class="cs__keyword">public</span> <span class="cs__keyword">int</span> GetLastInputTime() 
        { 
            <span class="cs__keyword">int</span> idletime = <span class="cs__number">0</span>; 
            idletime = <span class="cs__number">0</span>; 
            lastInputInf.cbSize = Marshal.SizeOf(lastInputInf); 
            lastInputInf.dwTime = <span class="cs__number">0</span>; 
 
            <span class="cs__keyword">if</span> (GetLastInputInfo(<span class="cs__keyword">ref</span> lastInputInf)) 
            { 
                idletime = Environment.TickCount - lastInputInf.dwTime; 
            } 
 
            <span class="cs__keyword">if</span> (idletime != <span class="cs__number">0</span>) 
            { 
                <span class="cs__keyword">return</span> idletime / <span class="cs__number">1000</span>; 
            } 
            <span class="cs__keyword">else</span> 
            { 
                <span class="cs__keyword">return</span> <span class="cs__number">0</span>; 
            } 
        } 
 
        <span class="cs__com">//Sub DisplayTime()</span> 
        <span class="cs__com">//Questa Sub non fa altro che visualizzare al suo interno il valore espresso in secondi</span> 
        <span class="cs__com">//del tempo in cui non si interagisce con l'applicazione.</span> 
        <span class="cs__com">//    All() 'interno di questa sub si pu&ograve; gestire secondo le proprie esigenze cosa e come eseguire determinate situazioni nell'applicazione , per esempio se si &eacute; eseguito in precedenza un login da parte dell'utente , dopo un certo tempo di inattivit&agrave; prestabilito dallo sviluppatore, si fa ripetere nuovamente il login e riprendere l'applicazione dove era stata lasciata l'utlima volta dall'utente.</span> 
        <span class="cs__keyword">private</span> <span class="cs__keyword">void</span> DisplayTime() 
        { 
            totaltime = GetLastInputTime(); 
            <span class="cs__keyword">if</span> (GetLastInputTime().Equals(<span class="cs__number">1</span>)) 
            { 
                Label1.Content = <span class="cs__string">&quot;Tempo di inattivit&agrave; pari a&quot;</span> &#43; <span class="cs__string">&quot; &quot;</span> &#43; GetLastInputTime().ToString() &#43; <span class="cs__string">&quot; &quot;</span> &#43; <span class="cs__string">&quot;secondo&quot;</span>; 
            } 
            <span class="cs__keyword">else</span> 
            { 
                Label1.Content = <span class="cs__string">&quot;Tempo di inattivit&agrave; pari a&quot;</span> &#43; <span class="cs__string">&quot; &quot;</span> &#43; GetLastInputTime().ToString() &#43; <span class="cs__string">&quot; &quot;</span> &#43; <span class="cs__string">&quot;secondi&quot;</span>; 
            } 
        } 
 
        <span class="cs__com">//Label1 visualizza a video il tempo di inattivit&agrave; dell&rsquo;utente.</span> 
        <span class="cs__com">//In un&rsquo;applicazione reale la label non verr&agrave; mai usata in quanto il conteggio non deve essere mai mostrato all&rsquo;utente , ci&ograve; e stato implementato solamente per mostrare il reale funzionamento dell'applicazione. Dopo un periodo di tempo l&rsquo;applicazione si disconnette e quindi l&rsquo;utente dovr&agrave; rieseguire nuovamente il login per poter riprendere ad intragire con l'applicazione.</span> 
        <span class="cs__com">//A tale scopo , con l'evento Mouse Move del Form MainWindow si esegue al suo interno il controllo della variabile totaltime, se superiore a cinque secondi viene richiamato il form LoginForm1 e lo visualizza come Form Modale all'utente , diversamente tutto procede normanlemte senza che l'applicazione richieda il login da parte dell'utente.</span> 
        <span class="cs__keyword">private</span> <span class="cs__keyword">void</span> Window_MouseMove(<a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Object.aspx" target="_blank" title="Auto generated link to System.Object">System.Object</a> sender, <a class="libraryLink" href="http://msdn.microsoft.com/it-IT/library/System.Windows.Input.MouseEventArgs.aspx" target="_blank" title="Auto generated link to System.Windows.Input.MouseEventArgs">System.Windows.Input.MouseEventArgs</a> e) 
        { 
            <span class="cs__keyword">if</span> (totaltime &gt; <span class="cs__number">5</span>) 
            { 
                FrmLogin log = <span class="cs__keyword">new</span> FrmLogin(); 
                log.ShowDialog(); 
            } 
        } 
    } 
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"></div>
<h1>More Information</h1>
<div><em><span style="font-size:x-small"><span>&nbsp;</span></span></em></div>
<div><em>
<div>Questo &egrave; il primo articolo nato dalla collaborazione da parte di Piero Sbressa e Carmelo La Monica.Potete contattarli ai seguenti riferimenti.</div>
<div></div>
<div>Piero Sbressa</div>
<div></div>
<div><a href="mailto:pierosbressa@crystalweb.it">pierosbressa@crystalweb.it</a></div>
<div></div>
<div><a href="http://www.crystalweb.it"><span style="text-decoration:underline"><span style="font-size:x-small"><span>www.crystalweb.it</span></span></span></a>
<br>
<span style="font-size:x-small">&nbsp;</span></div>
<div><span style="font-size:x-small">&nbsp;</span></div>
<div><span style="font-size:x-small">Carmelo La Monica</span></div>
<div><span style="font-size:x-small">&nbsp;</span></div>
<div><span style="font-size:x-small"><a href="http://community.visual-basic.it/carmelolamonica/default.aspx">http://community.visual-basic.it/carmelolamonica/default.aspx</a></span></div>
</em></div>
<div></div>
<h1><em>&nbsp;</em></h1>
