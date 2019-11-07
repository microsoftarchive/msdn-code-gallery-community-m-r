using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Client.Organizer.Services.Navigation;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// AppSettings view model.
    /// </summary>
    public class AppSettingsViewModel : ViewModelBase
    {
        INavigationService _navService;

        string _apiUrlPrefix = string.Empty;
        string _webUrlPrefix = string.Empty;
        bool _isOfflineMode = false;
        Lazy<RelayCommand> _saveCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingsViewModel(INavigationService navService)
        {
            ApiUrlPrefix = GlobalConfig.ApiUrlPrefix;
            WebUrlPrefix = GlobalConfig.WebUrlPrefix;
            IsOfflineMode = GlobalConfig.IsOfflineMode;
            _saveCommand = new Lazy<RelayCommand>(() => new RelayCommand(SaveCommandExecute));
            _navService = navService;
        }

        /// <summary>
        /// API service adress
        /// </summary>
        public string ApiUrlPrefix
        {
            get
            {
                return _apiUrlPrefix;
            }
            set
            {
                _apiUrlPrefix = value;
                RaisePropertyChanged(() => ApiUrlPrefix);
            }
        }

        /// <summary>
        /// Web service adress
        /// </summary>
        public string WebUrlPrefix
        {
            get
            {
                return _webUrlPrefix;
            }
            set
            {
                _webUrlPrefix = value;
                RaisePropertyChanged(() => WebUrlPrefix);
            }
        }


        /// <summary>
        /// true if the app is offline
        /// </summary>
        public bool IsOfflineMode
        {
            get
            {
                return _isOfflineMode;
            }
            set
            {
                _isOfflineMode = value;
                RaisePropertyChanged(() => IsOfflineMode);
            }
        }

        /// <summary>
        /// SaveCommand
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand.Value;
            }
        }

        /// <summary>
        /// Save the settings
        /// </summary>
        public void SaveCommandExecute()
        {
            GlobalConfig.ApiUrlPrefix = ApiUrlPrefix;
            GlobalConfig.WebUrlPrefix = WebUrlPrefix;
            GlobalConfig.IsOfflineMode = IsOfflineMode;

            _navService.NavigateToSplashScreen();
        }
    }
}
