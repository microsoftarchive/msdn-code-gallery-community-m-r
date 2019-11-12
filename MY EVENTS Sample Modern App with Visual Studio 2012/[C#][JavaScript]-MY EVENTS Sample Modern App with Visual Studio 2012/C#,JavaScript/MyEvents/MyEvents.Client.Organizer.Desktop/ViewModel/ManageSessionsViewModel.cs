using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Resources.Language;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// Manage sessions viewmodel.
    /// </summary>
    public class ManageSessionsViewModel : ViewModelBase
    {
        EventDefinition _eventDefinition;
        bool _showConfirmation = false;
        Session _selectedSession = null;
        Lazy<RelayCommand<Session>> _editSession;
        Lazy<RelayCommand<Session>> _materials;
        Lazy<RelayCommand<Session>> _deleteSession;
        Lazy<RelayCommand> _messageDialogAcceptCommand;
        Lazy<RelayCommand> _messageDialogDeclineCommand;
        Lazy<RelayCommand> _navigateBackCommand;
        IMyEventsClient _eventsClient;
        INavigationService _navService;
        string _messageText = string.Empty;
        bool _isBusy = true;

        /// <summary>
        /// Consctructor of the ManageSessionsViewModel with parameters
        /// </summary>
        public ManageSessionsViewModel(INavigationService navigationService, IMyEventsClient eventsClient)
        {
            SuscribeCommands();
            _navService = navigationService;
            _eventsClient = eventsClient;
            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
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
        /// Property that holds the Event from wich the sessiones are going to be obtained
        /// </summary>
        public EventDefinition EventDefinition
        {
            get
            {
                return _eventDefinition;
            }
            set
            {
                _eventDefinition = value;
                if (value != null)
                {
                    if (_eventDefinition.Sessions.Any())
                        PopulateSessions();
                    else
                        LoadSessions();
                }

                RaisePropertyChanged(() => EventDefinition);                
            }
        }

        /// <summary>
        /// Command to edit the session details
        /// </summary>
        public ICommand EditSessionCommand
        {
            get { return _editSession.Value; }
        }

        /// <summary>
        /// Command to manage the session materials
        /// </summary>
        public ICommand ManageMaterialsCommand
        {
            get { return _materials.Value; }
        }

        /// <summary>
        /// Command to manage the session materials
        /// </summary>
        public ICommand DeleteSessionCommand
        {
            get { return _deleteSession.Value; }
        }

        /// <summary>
        /// Delete the session and close the dialog.
        /// </summary>
        public ICommand MessageDialogAcceptCommand
        {
            get
            {
                return _messageDialogAcceptCommand.Value;
            }
        }

        /// <summary>
        /// Close the confirmation dialog without deleting the session.
        /// </summary>
        public ICommand MessageDialogDeclineCommand
        {
            get
            {
                return _messageDialogDeclineCommand.Value;
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
        /// Session list to be shown on the datagrid
        /// </summary>
        public ObservableCollection<Session> Sessions
        {
            set;
            get;
        }
        
        /// <summary>
        /// Sets the confirmation message shown when deleting a session.
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

        /// <summary>
        /// reload sessions.
        /// </summary>
        public void PopulateSessions()
        {
            Sessions = new ObservableCollection<Session>(EventDefinition.Sessions.OrderBy(s => s.Title).ToList());
            RaisePropertyChanged(() => Sessions);
        }

        private void LoadSessions()
        {
            _eventsClient.SessionService.GetAllSessionsAsync(EventDefinition.EventDefinitionId, (sessions) =>
                {
                    EventDefinition.Sessions = sessions;
                    PopulateSessions();
                });
        }

        private void SuscribeCommands()
        {
            _editSession = new Lazy<RelayCommand<Session>>(() => { return new RelayCommand<Session>(EditSessionExecute); });
            _materials = new Lazy<RelayCommand<Session>>(() => { return new RelayCommand<Session>(ManageMaterialsExecute); });
            _deleteSession = new Lazy<RelayCommand<Session>>(() => { return new RelayCommand<Session>(DeleteSessionExecute); });
            _messageDialogAcceptCommand = new Lazy<RelayCommand>(() => { return new RelayCommand(MessageDialogAcceptExecute); });
            _messageDialogDeclineCommand = new Lazy<RelayCommand>(() => { return new RelayCommand(MessageDialogDeclineExecute); });
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));
        }

        private void EditSessionExecute(Session selectedSession)
        {
            _navService.NavigateToEditSessionDetail(selectedSession);
        }

        private void ManageMaterialsExecute(Session selectedSession)
        {
            _navService.NavigateToUploadMaterials(selectedSession);
        }

        private void DeleteSessionExecute(Session selectedSession)
        {
            _selectedSession = selectedSession;
            MessageText = LanguageResources.Dialog_delete_session;
            ShowConfirmation = true;
        }

        private void MessageDialogDeclineExecute()
        {
            ShowConfirmation = false;
        }

        private void MessageDialogAcceptExecute()
        {
            _selectedSession.EventDefinition = null;
            _eventsClient.SessionService.DeleteSessionAsync(_selectedSession.SessionId, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    EventDefinition.Sessions.Remove(_selectedSession);
                    Sessions.Remove(_selectedSession);
                    RaisePropertyChanged(() => Sessions);
                });
            });

            ShowConfirmation = false;
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }
    }
}
