namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.Resources;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.Services.Notification;
    using MyCompany.Expenses.Client.WP.Settings;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Settings page viewmodel
    /// </summary>
    public class VMSettings : VMBase
    {
        private readonly INavigationService navService;

        RelayCommand saveSettingsCommand;

        private string serverUrl;
        private bool testMode;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="navService"></param>
        public VMSettings(INavigationService navService)
        {
            this.navService = navService;

            InitializeData();
            InitializeCommands();
        }

        /// <summary>
        /// Command to save settings and navigate to last page.
        /// </summary>
        public ICommand SaveSettingsCommand
        {
            get { return this.saveSettingsCommand; }
        }

        /// <summary>
        /// Server url configured.
        /// </summary>
        public string ServerUrl
        {
            get { return this.serverUrl; }
            set 
            {
                if (serverUrl == value) return;

                this.serverUrl = value;
                base.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Test mode configured.
        /// </summary>
        public bool TestMode
        {
            get { return this.testMode; }
            set
            {
                this.testMode = value;
                base.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Initialize relay commands used on this viewmodel.
        /// </summary>
        private void InitializeCommands()
        {
            this.saveSettingsCommand = new RelayCommand(SaveSettingsExecute);
        }

        private void InitializeData()
        {
            ServerUrl = AppSettings.ApiUri.ToString();
            testMode = AppSettings.TestMode;
        }

        /// <summary>
        /// Save settings to app isolatedstorage
        /// </summary>
        private void SaveSettingsExecute()
        {
            Uri serverUri;
            if (Uri.TryCreate(this.serverUrl, UriKind.Absolute, out serverUri))
            {
                AppSettings.SaveSettingsInfo(this.serverUrl, this.testMode);
                MessengerInstance.Send<ReloadMainViewMessage>(new ReloadMainViewMessage());
            }
            else
            {
                MessageBox.Show(AppResources.ErrorInvalidUri);
                this.serverUrl = AppSettings.ApiUri.ToString();
            }
        }
    }
}
