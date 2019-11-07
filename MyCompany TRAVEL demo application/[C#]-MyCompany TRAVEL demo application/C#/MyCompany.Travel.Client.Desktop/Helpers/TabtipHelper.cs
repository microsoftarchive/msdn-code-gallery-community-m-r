namespace MyCompany.Travel.Client.Desktop.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;

    /// <summary>
    /// This class manages the Tabtip process (touch keyboard).
    /// </summary>
    public class TabtipHelper
    {
        private const string PROCESS_NAME = "TabTip";
        private const string PROCESS_PATH = @"C:\Program Files\Common Files\microsoft shared\ink\tabtip.exe";
        private Process tabtipProcess;
        private static TabtipHelper instance;
        private CancellationTokenSource cts;
        private CancellationToken ct;
        private ProcessStartInfo psi;

        private TabtipHelper()
        {
            psi = new ProcessStartInfo
                      {
                          FileName = PROCESS_PATH,
                          WindowStyle = ProcessWindowStyle.Maximized
                      };
        }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public static TabtipHelper Instance
        {
            get { return instance ?? (instance = new TabtipHelper()); }
        }

        /// <summary>
        /// Is tabtip running.
        /// </summary>
        public bool IsTabtipRunning
        {
            get
            {
                this.tabtipProcess = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();       
                return (this.tabtipProcess != null);
            }
        }

        /// <summary>
        /// Try to kill Tabtip process.
        /// </summary>
        public void TryKillTabtipProcess()
        {
            if (IsTabtipRunning)
            {
                this.cts = new CancellationTokenSource();
                this.ct = cts.Token;

                // Wait for some miliseconds before hide the Taptip.
                TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    if (!ct.IsCancellationRequested)
                    {
                        this.cts = null;
                        if (!IsFullScreen())
                        {
                            RestoreWindow();
                        }
                        this.tabtipProcess.Kill();
                    }
                }, ct, TaskCreationOptions.None, uiScheduler);
            }
        }

        /// <summary>
        /// Try to create Tabtip process.
        /// </summary>
        public void TryCreateTabtipProcess()
        {
            if (IsTabtipRunning && (cts != null))
            {
                this.cts.Cancel();
            }   
 
            // Check if current device has touch screen.
            if (HasTouchInput())
            {
                this.tabtipProcess = Process.Start(psi);
            }
        }
        private bool HasTouchInput()
        {
            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }
            return false;
        }
        private void RestoreWindow()
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.UseLayoutRounding = true;
                App.Current.MainWindow.Width = Screen.PrimaryScreen.Bounds.Width;
                App.Current.MainWindow.Height = Screen.PrimaryScreen.Bounds.Height;
                App.Current.MainWindow.WindowState = WindowState.Normal;
                App.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
        private bool IsFullScreen()
        {
            return ((App.Current.MainWindow.Width >= Screen.PrimaryScreen.Bounds.Width)
                && App.Current.MainWindow.Height >= Screen.PrimaryScreen.Bounds.Height);
        }
    }
}
