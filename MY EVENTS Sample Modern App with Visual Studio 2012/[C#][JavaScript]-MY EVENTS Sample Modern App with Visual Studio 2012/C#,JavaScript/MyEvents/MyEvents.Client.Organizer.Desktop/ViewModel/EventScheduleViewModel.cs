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
    /// Event schedule viewmodel.
    /// </summary>
    public class EventScheduleViewModel : ViewModelBase
    {
        IMyEventsClient _eventsClient;
        INavigationService _navService;
        EventDefinition _event = new EventDefinition();

        Lazy<RelayCommand> _navigateBackCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        public EventScheduleViewModel(IMyEventsClient eventsClient, INavigationService navService)
        {
            _eventsClient = eventsClient;
            _navService = navService;
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));

            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
        }

        /// <summary>
        /// Event
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

                if (!_event.Sessions.Any())
                {
                    LoadSessions();
                }
                else
                {
                    RefreshProperties();
                }
            }
        }



        /// <summary>
        /// event timezone offset
        /// </summary>
        public string EventTimeZone
        {
            get
            {
                if (Event.TimeZoneOffset > -1)
                    return string.Format("(GMT+{0})", Event.TimeZoneOffset);
                else
                    return string.Format("(GMT {0})", Event.TimeZoneOffset);
            }
        }

        /// <summary>
        /// hours availables in the events
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
                        hours.Add(string.Format("{0:00}:{1:00}", i.Hour, i.Minute));
                    }

                    return hours;
                }


                return null;
            }
        }

        /// <summary>
        /// number of rooms availables in the event
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
        /// Command to navigate to last page.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return _navigateBackCommand.Value; }
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }

        private void LoadSessions()
        {
            _eventsClient.SessionService.GetAllSessionsAsync(Event.EventDefinitionId, (sessions) =>
            {
                Event.Sessions = sessions;

                App.Current.Dispatcher.Invoke(() =>
                {
                    RefreshProperties();
                });

            });
        }

        private void RefreshProperties()
        {
            RaisePropertyChanged(() => Event);
            RaisePropertyChanged(() => EventHours);
            RaisePropertyChanged(() => EventRooms);
            RaisePropertyChanged(() => EventTimeZone);
        }

    }
}
