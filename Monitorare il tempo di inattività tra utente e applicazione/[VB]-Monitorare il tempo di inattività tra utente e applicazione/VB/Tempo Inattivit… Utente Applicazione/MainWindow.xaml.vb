'Richiamo dll del framework
Imports System.Runtime.InteropServices
Imports System.Windows.Threading

'Lo spazio dei nomi System.Runtime.InteropServices fornisce un'ampia varietà di membri che supportano l'interoperabilità COM e servizi di chiamata al sistema operativo.
'Lo spazio dei nomi System.Windows.Threading invece, contiene tipi per il supporto del sistema di threading Windows Presentation Foundation (WPF).
'Anche in questo caso per un maggiore approfondimento consultare la documentazione online di MSDN Library.

'Classe MainWindow
Partial Public Class MainWindow

    'A livello di modulo dichiariamo una variabile di tipo integer , la chiamiamo totaltime e la inizializziamo con valore zero.
    'Totaltime ci indicherà per quanti secondi noi non interagiamo con tastiera e mouse.
    Dim totaltime As Integer = 0

    'Dichiariamo una nuova istanza della struttura LASTINPUTINFO, contenuta in GetLastInputInfo
    Dim lastInputInf As New LASTINPUTINFO()

    'Il codice seguente richiama la libreria user32.dll e la funzione GetLastInputInfo.
    <DllImport("user32.dll")>
    Shared Function GetLastInputInfo(ByRef plii As LASTINPUTINFO) As Boolean
    End Function

    'L'attributo DllImport è molto utile quando si riutilizza del codice non gestito esistente in un'applicazione gestita.
    'Come e possibile notare all'interno della struttura LASTINPUTINFO troviamo il termine MarshalAs.
    'Marshal si occupa di eseguire il marshalling dei dati tra codice gestito e non gestito.
    <StructLayout(LayoutKind.Sequential)>
    Structure LASTINPUTINFO
        <MarshalAs(UnmanagedType.U4)>
        Public cbSize As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dwTime As Integer
    End Structure

    'Evento Loaded della Classe MainWindow.
    'All'interno dell'evento Loaded viene dichiarato un nuovo oggetto DispatcherTimer 
    'abbinato ad un evento Tick , tale evento richiama ad ogni intervento il metodo DisplayTime il quale    aggiorna un oggetto label visualizzando cosi il tempo di inattività dell'utente con l'applicazione.
    'Da notare che in Wpf non esiste il controllo Timer , tale controllo è disponibile nelle applicazioni di  tipo WindowsForm , ma come spiegato in precedenza essendo un applicazione di tipo WPF , gestiamo il tutto con il controllo DispatcherTimer integrato nello spazio dei nomi System.Windows.Threading.
    Private Sub MainWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim dt As DispatcherTimer = New DispatcherTimer()
        AddHandler dt.Tick, AddressOf dispatcherTimer_Tick
        dt.Interval = New TimeSpan(0, 0, 1)
        dt.Start()
    End Sub

    'Evento Tick dell'oggetto DispatcherTimer
    'Viene richiamato il metodo DisplayTime() ogni secondo , questo dipende dal valore di Intervallo impostato nella proprietà Interval di dispatcherTimer .
    Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        DisplayTime()
    End Sub

    'Funzione GetLastInputTime.
    'Questa funzione restituisce ed assegna alla variabile idletime il valore del tempo  di inattività trascorso,
    'Il tutto convertito in secondi perchè inizialmente il valore recuperato da questa intruzione
    'di codice idletime = Environment.TickCount - lastInputInf.dwTime e espresso in millisecondi.
    'All’interno della classe MainWindows, dichiariamo anche una  variabile di tipo integer e la chiamiamo idletime la quale verrà valorizzata mediante la Funzione GetLastInputTime.
    Public Function GetLastInputTime() As Integer
        Dim idletime As Integer
        idletime = 0
        lastInputInf.cbSize = Marshal.SizeOf(lastInputInf)
        lastInputInf.dwTime = 0

        If GetLastInputInfo(lastInputInf) Then
            idletime = Environment.TickCount - lastInputInf.dwTime
        End If

        If idletime <> 0 Then
            Return idletime \ 1000
        Else
            Return 0
        End If
    End Function

    'Sub DisplayTime()
    'Questa Sub non fa altro che visualizzare al suo interno il valore espresso in secondi
    'del tempo in cui non si interagisce con l'applicazione.
    '    All() 'interno di questa sub si può gestire secondo le proprie esigenze cosa e come eseguire determinate situazioni nell'applicazione , per esempio se si é eseguito in precedenza un login da parte dell'utente , dopo un certo tempo di inattività prestabilito dallo sviluppatore, si fa ripetere nuovamente il login e riprendere l'applicazione dove era stata lasciata l'utlima volta dall'utente.
    Private Sub DisplayTime()
        totaltime = GetLastInputTime()
        If GetLastInputTime().Equals(1) Then
            Label1.Content = "Tempo di inattività pari a" & " " & GetLastInputTime.ToString & " " & "secondo"
        Else
            Label1.Content = "Tempo di inattività pari a" & " " & GetLastInputTime.ToString & " " & "secondi"
        End If
    End Sub

    'Label1 visualizza a video il tempo di inattività dell’utente.
    'In un’applicazione reale la label non verrà mai usata in quanto il conteggio non deve essere mai mostrato all’utente , ciò e stato implementato solamente per mostrare il reale funzionamento dell'applicazione. Dopo un periodo di tempo l’applicazione si disconnette e quindi l’utente dovrà rieseguire nuovamente il login per poter riprendere ad intragire con l'applicazione.
    'A tale scopo , con l'evento Mouse Move del Form MainWindow si esegue al suo interno il controllo della variabile totaltime, se superiore a cinque secondi viene richiamato il form LoginForm1 e lo visualizza come Form Modale all'utente , diversamente tutto procede normanlemte senza che l'applicazione richieda il login da parte dell'utente.
    Private Sub Window_MouseMove(sender As System.Object, e As System.Windows.Input.MouseEventArgs) Handles MyBase.MouseMove
        If totaltime > 5 Then
            Dim log As New FrmLogin
            log.ShowDialog()
        End If
    End Sub
End Class



