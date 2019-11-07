using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// type for the DeletedRoomPointsCommands.
    /// </summary>
    /// <param name="room">room number</param>
    public delegate void DeletedRoomPointsEventArgs(int room);
    /// <summary>
    /// Manage map viewmodel.
    /// </summary>
    public class ManageMapViewModel : ViewModelBase
    {
        private INavigationService _navService;
        private IMyEventsClient _eventsClient;

        private EventDefinition _event;
        private Dictionary<int, PointCollection> _eventRooms;
        private byte[] _eventRoomMap;

        private Lazy<RelayCommand> _navigateBackCommand;
        private Lazy<RelayCommand> _acceptCommand;
        private Lazy<RelayCommand<int>> _deleteRoomPointsCommand;
        private Lazy<RelayCommand> _openFileSelectorCommand;

        private bool _isBusy = false;

        /// <summary>
        /// Event to notify that the geometry has changed.
        /// </summary>
        public event DeletedRoomPointsEventArgs DeletedRoomPointsEvent;

        /// <summary>
        /// Constructor
        /// </summary>
        public ManageMapViewModel(INavigationService navService, IMyEventsClient myEventsClient)
        {
            _navService = navService;
            _eventsClient = myEventsClient;
            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
            SuscribeCommands();
        }

        /// <summary>
        /// Currently selected event.
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
                GetMapForEvent();
                RaisePropertyChanged(() => Event);
            }
        }

        /// <summary>
        /// Event rooms.
        /// </summary>
        public Dictionary<int, PointCollection> EventRooms
        { 
            get
            {
                return _eventRooms;
            }
            set
            {
                _eventRooms = value;
                RaisePropertyChanged(() => EventRooms);
            }
        }

        /// <summary>
        /// Navigate back command.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get
            {
                return _navigateBackCommand.Value;
            }
        }

        /// <summary>
        /// Command to accept changes and save it to the database.
        /// </summary>
        public ICommand AcceptCommand
        {
            get
            {
                return _acceptCommand.Value;
            }
        }

        /// <summary>
        /// Command to delete points of a room.
        /// </summary>
        public ICommand DeleteRoomPointsCommand
        {
            get
            {
                return _deleteRoomPointsCommand.Value;
            }
        }

        /// <summary>
        /// Command to open the file selector command
        /// </summary>
        public ICommand OpenFileSelectorCommand
        {
            get 
            { 
                return _openFileSelectorCommand.Value; 
            }
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
        /// Room map saved in database.
        /// </summary>
        public byte[] EventRoomMap
        {
            get
            {
                return _eventRoomMap;
            }
            set
            {
                _eventRoomMap = value;
                RaisePropertyChanged(() => EventRoomMap);
            }
        }

        /// <summary>
        /// add a new point to the room indicated.
        /// </summary>
        /// <param name="room">room number</param>
        /// <param name="point">new point</param>
        public void AddPoint(int room, Point point)
        {
            EventRooms[room].Add(point);
        }

        private void SuscribeCommands()
        {
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));
            _acceptCommand = new Lazy<RelayCommand>(() => new RelayCommand(AcceptCommandExecute));
            _deleteRoomPointsCommand = new Lazy<RelayCommand<int>>(() => new RelayCommand<int>(DeleteRoomPointsCommandExecute));
            _openFileSelectorCommand = new Lazy<RelayCommand>(() => { return new RelayCommand(OpenFileSelectorExecute); });
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }

        private void AcceptCommandExecute()
        {
            foreach (var room in EventRooms)
            {
                IList<RoomPoint> _roomsToSave = new List<RoomPoint>();

                room.Value.ToList().ForEach(rp => _roomsToSave.Add(new RoomPoint()
                {
                    EventDefinitionId = Event.EventDefinitionId,
                    PointX = (int)rp.X,
                    PointY = (int)rp.Y,
                    RoomNumber = room.Key
                }));

                if (_roomsToSave.Any())
                    _eventsClient.RoomPointService.AddRoomPointsAsync(_roomsToSave, (result) => { });
                else
                    _eventsClient.RoomPointService.DeleteRoomPointsAsync(Event.EventDefinitionId, room.Key, (result) => { });
            }

            _eventsClient.RoomPointService.UpdateRoomImageAsync(Event.EventDefinitionId, EventRoomMap, (result) => { });

            _navService.NavigateBack();
        }

        private void DeleteRoomPointsCommandExecute(int room)
        {
            if (room == 0)
                return;

            EventRooms[room].Clear();
            if (DeletedRoomPointsEvent != null)
                DeletedRoomPointsEvent(room);
        }

        private void OpenFileSelectorExecute()
        {
            Stream myStream;
            OpenFileDialog openMaterialDialog = new OpenFileDialog();

            openMaterialDialog.FilterIndex = 2;
            openMaterialDialog.RestoreDirectory = true;

            if (openMaterialDialog.ShowDialog().Value == true)
            {
                if ((myStream = openMaterialDialog.OpenFile()) != null)
                {
                    var bytes = File.ReadAllBytes(openMaterialDialog.FileName);

                    App.Current.Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        EventRoomMap = bytes;
                    }));
                }
            }
        }

        private void GetMapForEvent()
        {
            IsBusy = true;

            _eventsClient.RoomPointService.GetAllRoomPointsAsync(_event.EventDefinitionId, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    _eventRooms = new Dictionary<int, PointCollection>();

                    for (int i = 1; i < _event.RoomNumber + 1; i++)
                    {
                        List<Point> points = result.Where(q => q.RoomNumber == i)
                            .Select(q => new Point((int)q.PointX, (int)q.PointY))
                               .ToList();

                        _eventRooms.Add(i, new PointCollection(points));
                    }

                    RaisePropertyChanged(() => EventRooms);
                });
            });

            _eventsClient.RoomPointService.GetRoomImageAsync(Event.EventDefinitionId, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (result == null)
                    {
                        var blankmapStream = App.GetResourceStream(new Uri("/Resources/Images/BlankMap.png", UriKind.RelativeOrAbsolute));
                        _eventRoomMap = new byte[blankmapStream.Stream.Length];
                        blankmapStream.Stream.Read(_eventRoomMap, 0, (int)blankmapStream.Stream.Length);
                        RaisePropertyChanged(() => EventRoomMap);
                    }
                    else
                        EventRoomMap = result;

                    IsBusy = false;
                });
            });
        }
    }
}
