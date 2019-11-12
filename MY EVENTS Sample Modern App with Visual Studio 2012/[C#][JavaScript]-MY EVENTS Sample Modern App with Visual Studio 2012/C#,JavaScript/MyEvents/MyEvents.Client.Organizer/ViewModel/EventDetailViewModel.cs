using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Helper;
using MyEvents.Client.Organizer.Model;
using MyEvents.Client.Organizer.Services.Navigation;
using MyEvents.Client.Organizer.Services.Twitter;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Event details view model.
    /// </summary>
    public class EventDetailViewModel : ViewModelBase
    {        
        INavigationService _navService;
        ITwitterService _twitterService;
        IMyEventsClient _myEventsService;        

        Lazy<RelayCommand<Session>> _navigateToSessionDetailsCommand;
        Lazy<RelayCommand> _navigateBackCommand;
        Lazy<RelayCommand> _updateTwitterStatusCommand;
        Lazy<RelayCommand> _logInTwitterCommand;
        Lazy<RelayCommand<Rect>> _pinToStartCommand;
        Lazy<RelayCommand<Session>> _setToPinCommand;

        string _tweetMessage = string.Empty;
        bool _showTwitterProgress = false;
        string _twitterBtnContent = string.Empty;
        EventDefinition _event = new EventDefinition();
        Session _selectedSession = new Session();
        bool _isAppBarOpen = false;
        bool isAppBarSticky = true;
        ObservableCollection<TwitterItem> _timeline = new ObservableCollection<TwitterItem>();
        ResourceLoader _loader = new ResourceLoader();
        DispatcherTimer _tmr;
        DataTransferManager _datatransferManager;

        /// <summary>
        /// Initializes a new instance of the EventDetailViewModel class.
        /// </summary>
        /// <param name="navSrv">Navigation Service</param>
        /// <param name="twitterSrv">Twitter Service</param>
        /// <param name="myEventSrv">MyEvents Service</param>
        public EventDetailViewModel(INavigationService navSrv, ITwitterService twitterSrv, IMyEventsClient myEventSrv)
        {
            _navService = navSrv;
            _twitterService = twitterSrv;
            _myEventsService = myEventSrv;

            TwitterBtnContent = _loader.GetString("LogInTwitter");
            InitializeCommands();

            _myEventsService.SetAccessToken(UserCredentials.Current.CurrentUser.MyEventsToken);
        }


        private void InitializeCommands()
        {
            _navigateToSessionDetailsCommand = new Lazy<RelayCommand<Session>>(() => new RelayCommand<Session>(NavigateToSessionDetailsCommandExecute));
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackCommandExecute));
            _updateTwitterStatusCommand = new Lazy<RelayCommand>(() => new RelayCommand(UpdateTwitterStatusCommandExecute));
            _logInTwitterCommand = new Lazy<RelayCommand>(() => new RelayCommand(LogInTwitterCommandExecute));
            _pinToStartCommand = new Lazy<RelayCommand<Rect>>(() => new RelayCommand<Rect>(PinToStartCommandExecute));
            _setToPinCommand = new Lazy<RelayCommand<Session>>(() => new RelayCommand<Session>(SetToPinCommandExecute));
        }

        /// <summary>
        /// Load Page
        /// </summary>
        public void Load()
        {
            try
            {
                _datatransferManager = DataTransferManager.GetForCurrentView();
                _datatransferManager.DataRequested += datatransferManager_DataRequested;
            }
            catch
            {
                // TODO: CHANGE THIS CATCH BEFORE FIXING THE BUG
            }
        }

        /// <summary>
        /// Unload Page
        /// </summary>
        public void UnLoad()
        {
            _datatransferManager.DataRequested -= datatransferManager_DataRequested;
        }

        void datatransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.Properties.Title =  Event.Name;
            args.Request.Data.Properties.Description = Event.Description;
            args.Request.Data.SetUri(new Uri(String.Format(CultureInfo.InvariantCulture, "{0}Event/Detail/{1}", GlobalConfig.WebUrlPrefix, Event.EventDefinitionId)));
        } 

        /// <summary>
        /// EventDefinition property.
        /// </summary>
        public EventDefinition Event 
        { 
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                InitializeTwitterService(value);
                RaisePropertyChanged(() => Event);         
            }
        }

        /// <summary>
        /// Collection of hours for the duration of the event.
        /// </summary>
        public ObservableCollection<string> EventHours
        {
            get
            {
                if (Event != null)
                {
                    var hours = new ObservableCollection<string>();
                    for (DateTime i = Event.StartTime; i < Event.EndTime; i = i.AddHours(1))
                    {
                        hours.Add(i.ToString("hh:mm tt"));
                    }

                    return hours;
                }

                return null;
            }
        }

        /// <summary>
        /// Collection of rooms for the event.
        /// </summary>
        public ObservableCollection<string> EventRooms
        {
            get
            {
                if (Event != null)
                {
                    var rooms = new ObservableCollection<string>();
                    for (int i = 1; i <= Event.RoomNumber; i++)
                    {
                        rooms.Add(string.Format("room {0}", i));
                    }

                    return rooms;
                }

                return null;
            }
        }

        /// <summary>
        /// Session currently selected in the view.
        /// </summary>
        public Session SelectedSession
        {
            get 
            {
                return _selectedSession;
            }
            set 
            {
                _selectedSession = value;
                if (_selectedSession != null)
                {
                    IsAppBarSticky = true;
                    IsAppBarOpen = true;
                }
                else 
                {
                    IsAppBarSticky = false;
                    IsAppBarOpen = false;                    
                }                
                RaisePropertyChanged(() => SelectedSession);
            }
        }

        /// <summary>
        /// Manages the visibility of the AppBar.
        /// </summary>
        public bool IsAppBarOpen 
        { 
            get
            {
                return _isAppBarOpen;
            }
            set
            {
                _isAppBarOpen = value;
                RaisePropertyChanged (()=> IsAppBarOpen);
            }
        }


        public bool IsAppBarSticky
        {
            get
            {
                return isAppBarSticky;
            }
            set
            {
                isAppBarSticky = value;
                RaisePropertyChanged(() => IsAppBarSticky);
            }
        }


        /// <summary>
        /// Text shwon in twitter button.
        /// </summary>
        public string TwitterBtnContent 
        {
            get 
            {
                return _twitterBtnContent;
            }
            set
            {
                _twitterBtnContent = value;
                RaisePropertyChanged(() => TwitterBtnContent);
            }
        }

        /// <summary>
        /// Retrieve the event information to be displyed
        /// </summary>
        /// <param name="eventDefinition">Event to display</param>
        public void InitializeData(EventDefinition eventDefinition)
        {

            _myEventsService.SessionService.GetAllSessionsAsync(eventDefinition.EventDefinitionId, (sessionsResult) => 
            {
                App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                {
                    this.Event = eventDefinition;
                    this.Event.Sessions = new ObservableCollection<Session>(sessionsResult);   
                    RaisePropertyChanged(() => Event);
                    RaisePropertyChanged(() => EventHours);
                    RaisePropertyChanged(() => EventRooms);
                    
                })).AsTask().Wait();

            });        
        }

        /// <summary>
        /// Expose command logic to navigate to session details.
        /// </summary>
        public ICommand NavigateToSessionDetailsCommand
        {
            get
            {
                return _navigateToSessionDetailsCommand.Value;
            }
        }

        /// <summary>
        /// Expose command logic to navigate to previous visited page.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get
            {
                return _navigateBackCommand.Value;
            }
        }

        /// <summary>
        /// Put a tweet to the configured account.
        /// </summary>
        public ICommand UpdateTwitterStatusCommand
        {
            get
            {
                return _updateTwitterStatusCommand.Value;
            }
        }
        
        /// <summary>
        /// Show twitter login screen.
        /// </summary>
        public ICommand LogInTwitterCommand
        {
            get
            {
                return _logInTwitterCommand.Value;
            }
        }


        public ICommand PinToStartCommand
        { 
            get
            {
                return _pinToStartCommand.Value;
            }
        }

        public ICommand SetToPinCommand 
        {
            get 
            {
                return _setToPinCommand.Value;
            }
        }

        /// <summary>
        /// Tweet message.
        /// </summary>
        public string TweetMessage
        {
            get
            {
                return _tweetMessage;
            }
            set
            {
                _tweetMessage = value;
                RaisePropertyChanged(() => TweetMessage);
            }
        }

        /// <summary>
        /// Twitter timeline.
        /// </summary>
        public ObservableCollection<TwitterItem> Timeline
        {
            get
            {
                return _timeline;
            }
            set
            {
                _timeline = value;
                RaisePropertyChanged(() => Timeline);
            }
        }

        /// <summary>
        /// Shows or hides the progress bar for twitter.
        /// </summary>
        public bool ShowTwitterProgress
        {
            get
            {
                return _showTwitterProgress;
            }
            set
            {
                _showTwitterProgress = value;
                RaisePropertyChanged(() => ShowTwitterProgress);
            }
        }

        /// <summary>
        /// Pin a tile for the event in the start menu.
        /// </summary>
        public async void PinToStartCommandExecute(Rect rect)
        {

            Uri logo = new Uri("ms-appx:///Assets/EventTileSmall.png");
            Uri wideLogo = new Uri("ms-appx:///Assets/EventTileWide.png");

            SecondaryTile tile = new SecondaryTile(TileHelper.SetEventTileId(Event.EventDefinitionId),
                                                    Event.Name,
                                                    Event.Name,
                                                    TileHelper.SetEventTileActivationArguments(Event.EventDefinitionId),
                                                    TileOptions.ShowNameOnLogo | TileOptions.ShowNameOnWideLogo,
                                                    logo,
                                                    wideLogo);

            bool isPinned = await tile.RequestCreateForSelectionAsync((Rect)rect, Placement.Above);
        }


        public void SetToPinCommandExecute(Session session) 
        {
            if (session != SelectedSession)
            {
                SelectedSession = session;  
            }
            else
            {
                SelectedSession = null;                       
            }
        }

        /// <summary>
        /// When selecting an event, navigate to event details.
        /// </summary>
        public void NavigateToSessionDetailsCommandExecute(Session session)
        {
            _navService.NavigateToSessionDetails(session);
        }

        /// <summary>
        /// Navigate to previous visited page.
        /// </summary>
        public void NavigateBackCommandExecute()
        {
            if (ApplicationData.Current.LocalSettings.Values["TileId"].ToString() == "App")
                _navService.NavigateBack();
            else
                _navService.NavigateToMainPage();            
        }

        /// <summary>
        /// Post message to twitter.
        /// </summary>
        public async void UpdateTwitterStatusCommandExecute()
        {
            if (_twitterService.AccessGranted)
            {
                ShowTwitterProgress = true;
                var result = await _twitterService.UpdateStatus(TweetMessage);

                if (result)
                {
                    Timeline.Add(new TwitterItem() { Tweet = TweetMessage, User = new TwitterUser() { Username = _twitterService.ScreenName, Fullname = _twitterService.ScreenName } });
                    RaisePropertyChanged(() => Timeline);
                }

                TweetMessage = string.Empty;
            }
        }

        /// <summary>
        /// Show the twitter login UX.
        /// </summary>
        public async void LogInTwitterCommandExecute()
        {
            if (_tmr != null)
                _tmr.Stop();
            await _twitterService.GainAccessToTwitter();

            TwitterBtnContent = TwitterBtnContent = _loader.GetString("ChangeAccTwitter");
            RetrieveTimeline();
        }

        /// <summary>
        /// Check if we have access to twitter with this event.
        /// </summary>
        private void RetrieveTimeline()
        {
            if (GlobalConfig.IsOfflineMode)
                return;

            if (_twitterService.AccessGranted)
            {
                InitializeTwitterTimer();
            }
            else
            {
                Timeline = null;
                TwitterBtnContent = _loader.GetString("LogInTwitter");
            }
        }

        /// <summary>
        /// Get the EventDefinition data
        /// </summary>
        /// <param name="eventId">Event identificator</param>
        private void InitializeTwitterService(EventDefinition eventDefinition)
        {
            var twitterIdentifier = string.Format("{0}{1}", eventDefinition.Name, eventDefinition.EventDefinitionId.ToString());
            _twitterService.InitializeTwitterService(twitterIdentifier, GlobalConfig.TwitterConsumerKey, GlobalConfig.TwitterConsumerSecret, GlobalConfig.TwitterReturnUrl);
            RetrieveTimeline();
        }

        /// <summary>
        /// Initialize twitter.
        /// </summary>
        private void InitializeTwitterTimer()
        {
            _tmr = new DispatcherTimer();
            _tmr.Interval = new TimeSpan(0, 0, 30);
            _tmr.Tick += tmr_Tick;
            _tmr.Start();
            GetEventTimeline();
        }

        void tmr_Tick(object sender, object e)
        {
            GetEventTimeline();
        }

        /// <summary>
        /// Get the twitter timeline for our configured account.
        /// </summary>
        private async void GetEventTimeline()
        {
            if (_twitterService.AccessGranted)
            {
                TwitterBtnContent = _loader.GetString("ChangeAccTwitter");
                ShowTwitterProgress = true;
                var result = await _twitterService.GetTimeline();
                Timeline = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<TwitterItem>>(result);
                ShowTwitterProgress = false;
            }
        }
    }
}
