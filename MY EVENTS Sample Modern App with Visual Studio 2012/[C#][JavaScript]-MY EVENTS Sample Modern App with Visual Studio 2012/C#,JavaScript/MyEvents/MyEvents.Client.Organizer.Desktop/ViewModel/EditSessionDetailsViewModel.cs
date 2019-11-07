using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// Class that holds the EditSessionDetailsViewModel 
    /// </summary>
    public class EditSessionDetailsViewModel : ViewModelBase
    {
        Session _currentSession;
        Session _temporalSession = new Session();
        Lazy<RelayCommand> _messageSaveCommand;
        Lazy<RelayCommand> _messageCancelCommand;
        INavigationService _navService;
        IMyEventsClient _eventsClient;
        Lazy<RelayCommand> _navigateBackCommand;
        Lazy<RelayCommand> _editThisSession;

        /// <summary>
        /// Default constructor of  EditSessionDetailsViewModel
        /// </summary>
        public EditSessionDetailsViewModel(INavigationService navService, IMyEventsClient eventsClient)
        {
            _navService = navService;
            _eventsClient = eventsClient;
            Materials = new ObservableCollection<Material>();
            SuscribeCommands();

            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
        }

        /// <summary>
        /// Command that saves the changes made to the session and navigates back
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return _messageSaveCommand.Value;
            }
        }

        /// <summary>
        /// Command that navigates back from the edit session view
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                return _messageCancelCommand.Value;
            }
        }

        /// <summary>
        /// Command to navigate to last page.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return _navigateBackCommand.Value; }
        }

        /// <summary>
        /// Command that navigates to edit the actual session from the read only view.
        /// </summary>
        public ICommand EditThisSessionCommand
        {
            get
            {
                return _editThisSession.Value;
            }
        }

        /// <summary>
        /// Session information from the last screen, in wich the info is going to be saved
        /// </summary>
        public Session CurrentSession
        {
            get { return _currentSession; }
            set
            {
                _currentSession = value;
                _eventsClient.SessionService.GetSessionAsync(value.SessionId, (result) =>
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Materials.Clear();
                        foreach (var material in result.Materials.OrderBy(m => m.Name))
                        {
                            Materials.Add(material);
                        };
                    });
                });

                if (_currentSession != null)
                {
                    _temporalSession.Description = value.Description;
                    _temporalSession.Biography = value.Biography;
                    _temporalSession.TwitterAccount = value.TwitterAccount;
                    _temporalSession.Title = value.Title;
                    _temporalSession.Speaker = value.Speaker;
                    _temporalSession.StartTime = value.StartTime;
                    _temporalSession.Duration = value.Duration;
                    _temporalSession.RoomNumber = value.RoomNumber +1;

                    RaisePropertyChanged(() => TemporalSession);
                }
                RaisePropertyChanged(() => CurrentSession);
                RaisePropertyChanged(() => SessionTimeZone);
            }
        }

        /// <summary>
        /// event timezone offset
        /// </summary>
        public string SessionTimeZone
        {
            get
            {
                if (CurrentSession != null)
                {
                    if (CurrentSession.TimeZoneOffset > -1)
                        return string.Format("(GMT+{0})", CurrentSession.TimeZoneOffset);
                    else
                        return string.Format("(GMT-{0})", CurrentSession.TimeZoneOffset);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Property to hold the changes of data on the view
        /// </summary>
        public Session TemporalSession
        {
            get { return _temporalSession; }
            set
            {
                _temporalSession = value;
                RaisePropertyChanged(() => TemporalSession);
            }
        }

        private void SuscribeCommands()
        {
            _messageCancelCommand = new Lazy<RelayCommand>(() => new RelayCommand(CancelCommandExecute));
            _messageSaveCommand = new Lazy<RelayCommand>(() => new RelayCommand(SaveCommandExecute));
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));
            _editThisSession = new Lazy<RelayCommand>(() => new RelayCommand(EditThisSessionExecute));
        }


        private void SaveCommandExecute()
        {
            var tempEvent = _currentSession.EventDefinition;
            _currentSession.Title = _temporalSession.Title;
            _currentSession.Description = _temporalSession.Description;
            _currentSession.Biography = _temporalSession.Biography;
            _currentSession.TwitterAccount = _temporalSession.TwitterAccount;
            _currentSession.Speaker = _temporalSession.Speaker;
            _currentSession.EventDefinition = null;

            _eventsClient.SessionService.UpdateSessionAsync(_currentSession, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    _navService.NavigateBack();
                });
            });
        }

        private void CancelCommandExecute()
        {
            _navService.NavigateBack();
        }

        private void EditThisSessionExecute()
        {
            _navService.NavigateToEditSessionDetail(CurrentSession);
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }

        /// <summary>
        /// List of materials to be shown on the session details
        /// </summary>
        public ObservableCollection<Material> Materials { get; set; }
    }
}
