using System;
using System.Configuration;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Client.Organizer.Desktop.Credentials;
using MyEvents.Client.Organizer.Desktop.Services.Facebook;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// Main page viewmodel
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navService;
        IFacebookApi _facebookService;

        Lazy<RelayCommand> _showMainDesktopCommand;
        Lazy<RelayCommand> _showMyEventsCommand;
        Lazy<RelayCommand> _showSettingsCommand;
        Lazy<RelayCommand> _saveSettingsCommand;
        Lazy<RelayCommand> _cancelSettingsCommand;

        bool _showFacebookLogin = true;
        bool _showSettings = false;

        bool _offlineMode = false;
        string _serviceUrl = string.Empty;

        /// <summary>
        /// Initialices a new instance of Main page viewmodel with navigation service param
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="facebookService"></param>
        public MainPageViewModel(INavigationService navigationService, IFacebookApi facebookService)
        {
            _navService = navigationService;
            _facebookService = facebookService;
            SuscribeCommands();

            LoadData();
        }

        /// <summary>
        /// Show or Hide facebook login screen
        /// </summary>
        public bool ShowFacebookLogin
        {
            get
            {
                return _showFacebookLogin;
            }
            set
            {
                _showFacebookLogin = value;
                if (value == false)
                {
                    _navService.NavigateToMain();
                    RaisePropertyChanged(() => Username);
                    RaisePropertyChanged(() => UserPhoto);
                }                

                RaisePropertyChanged(() => ShowFacebookLogin);
            }
        }

        /// <summary>
        /// Show or hide settings screen
        /// </summary>
        public bool ShowSettings
        {
            get
            {
                return _showSettings;
            }
            set
            {
                _showSettings = value;
                RaisePropertyChanged(() => ShowSettings);
            }
        }

        /// <summary>
        /// true if we dont have access to internet.
        /// </summary>
        public bool OfflineMode
        {
            get
            {
                return _offlineMode;
            }
            set
            {
                _offlineMode = value;
                RaisePropertyChanged(() => OfflineMode);
            }
        }

        /// <summary>
        /// Exposes service address stored in app.config
        /// </summary>
        public string ServiceUrl
        {
            get
            {
                return _serviceUrl;
            }
            set
            {
                _serviceUrl = value;
                RaisePropertyChanged(() => ServiceUrl);
            }
        }

        /// <summary>
        /// Command to show to Main desktop
        /// </summary>
        public ICommand ShowMainDesktopCommand
        {
            get { return _showMainDesktopCommand.Value; }
        }

        /// <summary>
        /// Command to show My events
        /// </summary>
        public ICommand ShowMyeEventsCommand
        {
            get { return _showMyEventsCommand.Value; }
        }

        /// <summary>
        /// Command to show the settings
        /// </summary>
        public ICommand ShowSettingsCommand
        {
            get { return _showSettingsCommand.Value; }
        }

        /// <summary>
        /// Save and apply settings.
        /// </summary>
        public ICommand SaveSettingsCommand
        {
            get { return _saveSettingsCommand.Value; }
        }

        /// <summary>
        /// Discard and hide settings.
        /// </summary>
        public ICommand CancelSettingsCommand
        {
            get { return _cancelSettingsCommand.Value; }
        }

        /// <summary>
        /// Exposes the username loged in the system.
        /// </summary>
        public string Username
        {
            get
            {
                return UserCredentials.Current.FullName;
            }
        }

        /// <summary>
        /// Current loged user photo
        /// </summary>
        public string UserPhoto
        {
            get
            {
                if (OfflineMode )
                    return "Resources/Images/FacebookUserWithoutPhoto.gif";

                return string.Format("https://graph.facebook.com/{0}/picture", UserCredentials.Current.FacebookId);
            }
        }

        private void SuscribeCommands()
        {
            _showMainDesktopCommand = new Lazy<RelayCommand>(() => new RelayCommand(ShowMainDesktopCommandExecute));
            _showMyEventsCommand = new Lazy<RelayCommand>(() => new RelayCommand(ShowMyEventsCommandExecute));
            _showSettingsCommand = new Lazy<RelayCommand>(() => new RelayCommand(ShowSettingsCommandExecute));
            _saveSettingsCommand = new Lazy<RelayCommand>(() => new RelayCommand(SaveSettingsCommandExecute));
            _cancelSettingsCommand = new Lazy<RelayCommand>(() => new RelayCommand(CancelSettingsCommandExecute));
        }

        private void LoadData()
        {
            bool.TryParse(ConfigurationManager.AppSettings.Get("OfflineMode"), out _offlineMode);
            ServiceUrl = ConfigurationManager.AppSettings.Get("ServicesAddress");
            RaisePropertyChanged(() => OfflineMode);
        }

        private void ShowSettingsCommandExecute()
        {
            ShowSettings = !ShowSettings;
        }

        private void ShowMainDesktopCommandExecute()
        {
            _navService.NavigateToMain();
        }

        private void ShowMyEventsCommandExecute()
        {
            _navService.NavigateToMyEvents();
        }

        private void SaveSettingsCommandExecute()
        { 
            ConfigurationManager.AppSettings["OfflineMode"] = OfflineMode.ToString();
            ConfigurationManager.AppSettings["ServicesAddress"] = ServiceUrl;
            ShowFacebookLogin = false;
            ShowFacebookLogin = true;
            ShowSettings = false;
        }

        private void CancelSettingsCommandExecute()
        {
            ShowSettings = false;
        }
    }
}
