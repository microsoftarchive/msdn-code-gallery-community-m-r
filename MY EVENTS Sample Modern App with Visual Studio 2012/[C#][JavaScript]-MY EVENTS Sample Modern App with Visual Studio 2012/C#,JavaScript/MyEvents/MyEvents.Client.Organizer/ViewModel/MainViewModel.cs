using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Model;
using MyEvents.Client.Organizer.Services.Navigation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.Storage;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Main page view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        Lazy<RelayCommand<EventDefinition>> _navigateToDetailsCommand;
        

        INavigationService _navService;
        IMyEventsClient _myEventsService;
        bool _showProgress = true;
        bool _noEventsAvailable;
        public bool isLoading = true;
        EventDefinition selectedEvent = null;
        ObservableCollection<EventDefinition> _ungroupedEvents;
        ObservableCollection<EventGroup> _events;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// <param name="myEventsSrv">myEvents Service</param>
        public MainViewModel(INavigationService navSrv, IMyEventsClient myEventsSrv)
        {
            _navService = navSrv;
            _myEventsService = myEventsSrv;

            _navigateToDetailsCommand = new Lazy<RelayCommand<EventDefinition>>(() => new RelayCommand<EventDefinition>(NavigateToDetailsCommandExecute));

            InitializeData();           
        }

        /// <summary>
        /// Controls the visibility of the progressbar.
        /// </summary>
        public bool ShowProgress
        {
            get 
            { 
                return _showProgress; 
            }
            set 
            {
                if (value != _showProgress)
                {
                    _showProgress = value;
                    RaisePropertyChanged(() => ShowProgress);
                }
            }
        }

        /// <summary>
        /// Controls the visibility of the error message
        /// </summary>
        public bool NoEventsAvailable 
        {
            get 
            { 
                return _noEventsAvailable; 
            }
            set 
            {
                _noEventsAvailable = value;
                RaisePropertyChanged(() => NoEventsAvailable);
            }
        }

        /// <summary>
        /// Currently selected event
        /// </summary>
        public EventDefinition SelectedEvent
        {
            get
            {
                return selectedEvent;
            }
            set
            {
                selectedEvent = value;
                RaisePropertyChanged(() => SelectedEvent);
            }
        }

        /// <summary>
        /// Grouped event list by date.
        /// </summary>
        public ObservableCollection<EventGroup> Events 
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                RaisePropertyChanged(() => Events);
            }
        }

        /// <summary>
        /// Plain, ungrouped list of events.
        /// </summary>
        public ObservableCollection<EventDefinition> UngroupedEvents
        {
            get
            {
                return _ungroupedEvents;
            }
            set
            {
                _ungroupedEvents = value;
                RaisePropertyChanged(() => UngroupedEvents);
            }
        }

        /// <summary>
        /// Expose command logic to navigate to event details.
        /// </summary>
        public ICommand NavigateToDetailsCommand
        {
            get
            {
                return _navigateToDetailsCommand.Value;
            }
        }

        /// <summary>
        /// When selecting an event, navigate to event details.
        /// </summary>
        public void NavigateToDetailsCommandExecute(EventDefinition eventDefinition)
        {
            _navService.NavigateToEventDetails(eventDefinition);
        }

        private void InitializeData()
        {
            ResourceLoader loader = new ResourceLoader();
            _myEventsService.SetAccessToken(UserCredentials.Current.CurrentUser.MyEventsToken);
            _myEventsService.EventDefinitionService.GetEventDefinitionByOrganizerIdAsync(UserCredentials.Current.CurrentUser.UserId, string.Empty, 50, 0, (result) =>
            {
                _ungroupedEvents = new ObservableCollection<EventDefinition>(result);
                ObservableCollection<EventDefinition> events = new ObservableCollection<EventDefinition>(result);

                _events = new ObservableCollection<EventGroup>();
                _events.Add(new EventGroup()
                {
                    GroupIndex = 1,
                    GroupTitle = loader.GetString("ComingSoonTitle"),
                    Items = new ObservableCollection<EventDefinition>(events.Where(e => e.Date >= DateTime.UtcNow).OrderBy(ev => ev.Date).Take(4)
                                                                        .Select(ev => new EventDefinition() 
                                                                        {
                                                                            Address = ev.Address,
                                                                            AttendeesCount = ev.AttendeesCount,
                                                                            City = ev.City,
                                                                            Date = ev.Date,
                                                                            Description = ev.Description,
                                                                            EndTime = ev.EndTime,
                                                                            EventDefinitionId = ev.EventDefinitionId,
                                                                            GroupNumber = 1,
                                                                            Latitude = ev.Latitude,
                                                                            Likes = ev.Likes,
                                                                            Logo = ev.Logo,
                                                                            Longitude = ev.Longitude,
                                                                            MapImage = ev.MapImage,
                                                                            Name = ev.Name,
                                                                            Organizer = ev.Organizer,
                                                                            OrganizerId = ev.OrganizerId,
                                                                            Registered = ev.Registered,
                                                                            RegisteredUsers = ev.RegisteredUsers,
                                                                            RoomNumber = ev.RoomNumber,
                                                                            RoomPoints = ev.RoomPoints,
                                                                            Sessions = ev.Sessions,
                                                                            StartTime = ev.StartTime,
                                                                            Tags = ev.Tags,
                                                                            TimeZoneOffset = ev.TimeZoneOffset,
                                                                            TwitterAccount = ev.TwitterAccount,
                                                                            ZipCode = ev.ZipCode
                                                                        }).ToList())});
                _events.Add(new EventGroup()
                {
                    GroupIndex = 2,
                    GroupTitle = loader.GetString("MoreEventsTitle"),
                    Items = new ObservableCollection<EventDefinition>(events
                                                                        .Select(ev => new EventDefinition()
                                                                        {
                                                                            Address = ev.Address,
                                                                            AttendeesCount = ev.AttendeesCount,
                                                                            City = ev.City,
                                                                            Date = ev.Date,
                                                                            Description = ev.Description,
                                                                            EndTime = ev.EndTime,
                                                                            EventDefinitionId = ev.EventDefinitionId,
                                                                            GroupNumber = 2,
                                                                            Latitude = ev.Latitude,
                                                                            Likes = ev.Likes,
                                                                            Logo = ev.Logo,
                                                                            Longitude = ev.Longitude,
                                                                            MapImage = ev.MapImage,
                                                                            Name = ev.Name,
                                                                            Organizer = ev.Organizer,
                                                                            OrganizerId = ev.OrganizerId,
                                                                            Registered = ev.Registered,
                                                                            RegisteredUsers = ev.RegisteredUsers,
                                                                            RoomNumber = ev.RoomNumber,
                                                                            RoomPoints = ev.RoomPoints,
                                                                            Sessions = ev.Sessions,
                                                                            StartTime = ev.StartTime,
                                                                            Tags = ev.Tags,
                                                                            TimeZoneOffset = ev.TimeZoneOffset,
                                                                            TwitterAccount = ev.TwitterAccount,
                                                                            ZipCode = ev.ZipCode
                                                                        }).ToList())});

                App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                {
                    RaisePropertyChanged(() => Events);
                    RaisePropertyChanged(() => UngroupedEvents);
                    ShowProgress = false;
                    this.isLoading = false;
                    this.SelectedEvent = null;
                    NoEventsAvailable = events == null ? true : !events.Any();
                    RaisePropertyChanged(() => NoEventsAvailable);
                })).AsTask().Wait();
            });
        }
    }
}