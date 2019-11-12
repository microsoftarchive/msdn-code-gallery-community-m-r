namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using System.Windows.Input;

    /// <summary>
    /// Configuration panel viewmodel.
    /// </summary>
    public class VMConfigurationPanel : ViewModelBase
    {
        private readonly INavigationService navService;

        private RelayCommand saveSettingsCommand;

        private string apiUrl;
        private bool   isInTestMode;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="navService">Navigation service inyected by DI</param>
        public VMConfigurationPanel(INavigationService navService)
        {
            this.navService = navService;

            InitializeCommands();
            InitializeData();
        }

        /// <summary>
        /// Command to save new settings values.
        /// </summary>
        public ICommand SaveSettingsCommand
        {
            get { return this.saveSettingsCommand; }
        }

        /// <summary>
        /// API Url.
        /// </summary>
        public string ApiUrl
        {
            get { return this.apiUrl; }
            set
            {
                this.apiUrl = value;
                RaisePropertyChanged(() => ApiUrl);
            }
        }

        /// <summary>
        /// Is in test mode.
        /// </summary>
        public bool IsInTestMode
        {
            get { return this.isInTestMode; }
            set
            {
                this.isInTestMode = value;
                RaisePropertyChanged(() => IsInTestMode);
            }
        }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitializeCommands()
        {
            this.saveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
        }

        /// <summary>
        /// Initialize screen data.
        /// </summary>
        private void InitializeData()
        {
            ApiUrl = AppSettings.ApiUri.ToString();
            IsInTestMode = AppSettings.TestMode;
        }

        /// <summary>
        /// Save new values in storage.
        /// </summary>
        private void SaveSettingsCommandExecute()
        {
            AppSettings.UpdateKeyValue(AppSettings.STORAGEKEY_TESTMODE, this.isInTestMode.ToString());
            AppSettings.UpdateKeyValue(AppSettings.STORAGEKEY_APIURI, this.apiUrl.ToLower().Trim());

            AppSettings.RemoveKeyValue(AppSettings.STORAGEKEY_SECURITYTOKEN);

            var locator = (ViewModelLocator)App.Current.Resources["Locator"];
            locator.Register();
            AppSettings.EmployeeInformation = null;
            if (AppSettings.TestMode)
                this.navService.NavigateToMainPage();
            else
                this.navService.NavigateToAuthentication();
        }
    }
}
