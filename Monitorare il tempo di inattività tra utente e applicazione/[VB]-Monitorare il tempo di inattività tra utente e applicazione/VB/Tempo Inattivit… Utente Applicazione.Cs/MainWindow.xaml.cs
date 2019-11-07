//Richiamo dll del framework
using System;
using System.Windows;
//Su quest'ultimo file ossia MainWindow.xaml.vb dobbiamo importare i seguenti namespaces:
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Tempo_Inattività_Utente_Applicazione.Cs
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //A livello di modulo dichiariamo una variabile di tipo integer , la chiamiamo totaltime e la inizializziamo con valore zero.
        //Totaltime ci indicherà per quanti secondi noi non interagiamo con tastiera e mouse.
        int totaltime = 0;

        //Dichiariamo una nuova istanza della struttura LASTINPUTINFO, contenuta in GetLastInputInfo
        LASTINPUTINFO lastInputInf = new LASTINPUTINFO();
        //Il codice seguente richiama la libreria user32.dll e la funzione GetLastInputInfo.
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        //L'attributo DllImport è molto utile quando si riutilizza del codice non gestito esistente in un'applicazione gestita.
        //Come e possibile notare all'interno della struttura LASTINPUTINFO troviamo il termine MarshalAs.
        //Marshal si occupa di eseguire il marshalling dei dati tra codice gestito e non gestito.
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTime;
        }

        //Evento Loaded della Classe MainWindow.
        //All'interno dell'evento Loaded viene dichiarato un nuovo oggetto DispatcherTimer 
        //abbinato ad un evento Tick , tale evento richiama ad ogni intervento il metodo DisplayTime il quale    aggiorna un oggetto label visualizzando cosi il tempo di inattività dell'utente con l'applicazione.
        //Da notare che in Wpf non esiste il controllo Timer , tale controllo è disponibile nelle applicazioni di  tipo WindowsForm , ma come spiegato in precedenza essendo un applicazione di tipo WPF , gestiamo il tutto con il controllo DispatcherTimer integrato nello spazio dei nomi System.Windows.Threading.
        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += dispatcherTimer_Tick;
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();
        }

        //Evento Tick dell'oggetto DispatcherTimer
        //Viene richiamato il metodo DisplayTime() ogni secondo , questo dipende dal valore di Intervallo impostato nella proprietà Interval di dispatcherTimer .
        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DisplayTime();
        }

        //Funzione GetLastInputTime.
        //Questa funzione restituisce ed assegna alla variabile idletime il valore del tempo  di inattività trascorso,
        //Il tutto convertito in secondi perchè inizialmente il valore recuperato da questa intruzione
        //di codice idletime = Environment.TickCount - lastInputInf.dwTime e espresso in millisecondi.
        //All’interno della classe MainWindows, dichiariamo anche una  variabile di tipo integer e la chiamiamo idletime la quale verrà valorizzata mediante la Funzione GetLastInputTime.
        public int GetLastInputTime()
        {
            int idletime = 0;
            idletime = 0;
            lastInputInf.cbSize = Marshal.SizeOf(lastInputInf);
            lastInputInf.dwTime = 0;

            if (GetLastInputInfo(ref lastInputInf))
            {
                idletime = Environment.TickCount - lastInputInf.dwTime;
            }

            if (idletime != 0)
            {
                return idletime / 1000;
            }
            else
            {
                return 0;
            }
        }

        //Sub DisplayTime()
        //Questa Sub non fa altro che visualizzare al suo interno il valore espresso in secondi
        //del tempo in cui non si interagisce con l'applicazione.
        //    All() 'interno di questa sub si può gestire secondo le proprie esigenze cosa e come eseguire determinate situazioni nell'applicazione , per esempio se si é eseguito in precedenza un login da parte dell'utente , dopo un certo tempo di inattività prestabilito dallo sviluppatore, si fa ripetere nuovamente il login e riprendere l'applicazione dove era stata lasciata l'utlima volta dall'utente.
        private void DisplayTime()
        {
            totaltime = GetLastInputTime();
            if (GetLastInputTime().Equals(1))
            {
                Label1.Content = "Tempo di inattività pari a" + " " + GetLastInputTime().ToString() + " " + "secondo";
            }
            else
            {
                Label1.Content = "Tempo di inattività pari a" + " " + GetLastInputTime().ToString() + " " + "secondi";
            }
        }

        //Label1 visualizza a video il tempo di inattività dell’utente.
        //In un’applicazione reale la label non verrà mai usata in quanto il conteggio non deve essere mai mostrato all’utente , ciò e stato implementato solamente per mostrare il reale funzionamento dell'applicazione. Dopo un periodo di tempo l’applicazione si disconnette e quindi l’utente dovrà rieseguire nuovamente il login per poter riprendere ad intragire con l'applicazione.
        //A tale scopo , con l'evento Mouse Move del Form MainWindow si esegue al suo interno il controllo della variabile totaltime, se superiore a cinque secondi viene richiamato il form LoginForm1 e lo visualizza come Form Modale all'utente , diversamente tutto procede normanlemte senza che l'applicazione richieda il login da parte dell'utente.
        private void Window_MouseMove(System.Object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (totaltime > 5)
            {
                FrmLogin log = new FrmLogin();
                log.ShowDialog();
            }
        }
    }
}
