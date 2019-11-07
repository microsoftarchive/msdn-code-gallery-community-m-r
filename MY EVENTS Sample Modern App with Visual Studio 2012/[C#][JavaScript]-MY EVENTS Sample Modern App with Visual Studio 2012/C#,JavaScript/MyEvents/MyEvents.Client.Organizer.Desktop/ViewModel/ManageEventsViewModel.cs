using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Credentials;
using MyEvents.Client.Organizer.Desktop.Resources.Language;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// ManageEvents viewmodel.
    /// </summary>
    public class ManageEventsViewModel : ViewModelBase
    {
        private INavigationService _navService;
        private IMyEventsClient _eventsClient;
        private Lazy<RelayCommand<string>> _searchEventCommand;
        private Lazy<RelayCommand<EventDefinition>> _deleteEventCommand;
        private Lazy<RelayCommand<EventDefinition>> _navigateToUsersCommand;
        private Lazy<RelayCommand<EventDefinition>> _navigateToSessionsCommand;
        private Lazy<RelayCommand> _messageDialogAcceptCommand;
        private Lazy<RelayCommand> _messageDialogDeclineCommand;

        private ObservableCollection<EventDefinition> _events = null;
        private EventDefinition _selectedEvent = null;
        private bool _isBusy = true;
        private bool _showConfirmation = false;
        private string _textFilter = string.Empty;
        private string _messageText = string.Empty;

        /// <summary>
        /// Constructor of ManageEventsViewModel with parameters
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="eventsClient"></param>
        public ManageEventsViewModel(INavigationService navigationService, IMyEventsClient eventsClient)
        {
            IsBusy = true;
            _navService = navigationService;
            _eventsClient = eventsClient;
            LoadData();
            SuscribeCommands();
        }

        /// <summary>
        /// Property used to show a progressbar when the app is busy retrieving data
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// Show confirmation messagebox.
        /// </summary>
        public bool ShowConfirmation
        {
            get { return _showConfirmation; }
            set
            {
                _showConfirmation = value;
                RaisePropertyChanged(() => ShowConfirmation);
            }
        }

        /// <summary>
        /// Command that searchs for events
        /// </summary>
        public ICommand SearchEventCommand 
        { 
            get 
            { 
                return _searchEventCommand.Value; 
            } 
        }

        /// <summary>
        /// Delete the selected event.
        /// </summary>
        public ICommand DeleteEventCommand
        {
            get
            {
                return _deleteEventCommand.Value;
            }
        }

        /// <summary>
        /// Navigate to the registered users of the selected event.
        /// </summary>
        public ICommand NavigateToUsersCommand
        {
            get
            {
                return _navigateToUsersCommand.Value;
            }
        }

        /// <summary>
        /// Navigate to the session schedule for the selected event.
        /// </summary>
        public ICommand NavigateToSessionsCommand
        {
            get
            {
                return _navigateToSessionsCommand.Value;
            }
        }

        /// <summary>
        /// Delete the event and close the dialog.
        /// </summary>
        public ICommand MessageDialogAcceptCommand
        {
            get
            {
                return _messageDialogAcceptCommand.Value;
            }
        }

        /// <summary>
        /// Close the confirmation dialog without deleting the event.
        /// </summary>
        public ICommand MessageDialogDeclineCommand
        {
            get
            {
                return _messageDialogDeclineCommand.Value;
            }
        }

        /// <summary>
        /// List of events.
        /// </summary>
        public ObservableCollection<EventDefinition> Events
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(_textFilter))
                    return _events;
                else
                    return new ObservableCollection<EventDefinition>(_events.Where(e => e.Name.ToLowerInvariant().Contains(_textFilter.ToLowerInvariant())).ToList());
            }
            set
            {
                _events = value;
                RaisePropertyChanged(() => Events);
            }
        }

        /// <summary>
        /// SelectedEvent, if not null, navigate to details.
        /// </summary>
        public EventDefinition SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                if (value != null)
                    _navService.NavigateToEventDetails(_selectedEvent);
                RaisePropertyChanged(() => SelectedEvent);
            }
        }

        /// <summary>
        /// Text to filter events.
        /// </summary>
        public string TextFilter
        {
            get
            {
                return _textFilter;
            }
            set
            {
                _textFilter = value;
                RaisePropertyChanged(() => TextFilter);
            }
        }

        /// <summary>
        /// Sets the confirmation message shown when deleting an event.
        /// </summary>
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            set
            {
                _messageText = value;
                RaisePropertyChanged(() => MessageText);
            }
        }

        private void SuscribeCommands()
        {
            _searchEventCommand = new Lazy<RelayCommand<string>>(() => { return new RelayCommand<string>(SearchEventCommandExecute); });
            _deleteEventCommand = new Lazy<RelayCommand<EventDefinition>>(() => { return new RelayCommand<EventDefinition>(DeleteEventCommandExecute); });
            _navigateToSessionsCommand = new Lazy<RelayCommand<EventDefinition>>(() => { return new RelayCommand<EventDefinition>(NavigateToSessionsCommandExecute); });
            _navigateToUsersCommand = new Lazy<RelayCommand<EventDefinition>>(() => { return new RelayCommand<EventDefinition>(NavigateToUsersCommandExecute); });
            _messageDialogAcceptCommand = new Lazy<RelayCommand>(() => { return new RelayCommand(MessageDialogAcceptExecute); });
            _messageDialogDeclineCommand = new Lazy<RelayCommand>(() => { return new RelayCommand(MessageDialogDeclineExecute); });
        }

        private void MessageDialogDeclineExecute()
        {
            _selectedEvent = null;
            ShowConfirmation = false;
        }

        private void MessageDialogAcceptExecute()
        {
            ShowConfirmation = false;
            _selectedEvent.RegisteredUsers = null;
            _selectedEvent.Sessions = null;
            _eventsClient.EventDefinitionService.DeleteEventDefinitionAsync(_selectedEvent.EventDefinitionId, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    LoadData();
                });
            });
        }

        private void NavigateToUsersCommandExecute(EventDefinition eventDefinition)
        {
            _navService.NavigateToRegisteredUsers(eventDefinition);
        }

        private void NavigateToSessionsCommandExecute(EventDefinition eventDefinition)
        {
            _navService.NavigateToManageSessions(eventDefinition);
        }

        private void DeleteEventCommandExecute(EventDefinition eventDefinition)
        {
            _selectedEvent = eventDefinition;
            MessageText = string.Format(LanguageResources.Dialog_delete_message, eventDefinition.Name);
            ShowConfirmation = true;
        }

        private void SearchEventCommandExecute(string parameter)
        {
            TextFilter = parameter;
            RaisePropertyChanged(() => Events);
        }

        private void LoadData()
        {
            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
            _eventsClient.EventDefinitionService.GetEventDefinitionByOrganizerIdAsync(UserCredentials.Current.UserId, string.Empty, 500, 0, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Events = new ObservableCollection<EventDefinition>(result);

                    RaisePropertyChanged(() => Events);
                    IsBusy = false;
                });
            });
        }
    }
}
