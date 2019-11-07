using System;
using System.Windows.Input;
using System.Windows.Threading;
using MyShuttle.Device.Command;
using MyShuttle.Device.Enums;
using MyShuttle.Device.EventHub;
using MyShuttle.Device.Services;
using MyShuttle.Model;
using System.Collections.ObjectModel;
using MyShuttle.Device.Model;

namespace MyShuttle.Device.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private int _compassTimerCount;
        private int _accelTimerCount;
        private int _carCrashTimerCount;

        private bool _parkingBrakeChecked;
        private bool _seatBeltChecked;
        private bool _fuelChecked;
        private bool _drivingSessionStarted;
        private bool _driverSelected;
        private bool _isCarCrashed;
        private bool _showDriverPanel;
        private bool _showDriverSelectionPanel;
        private string _licensePlate;
        private ObservableCollection<Driver> _availableDrivers;
        private SessionStatus _sessionStatus;
        private Driver _currentDriver;

        private CompassEvent _compassEvent;
        private AccelerometerEvent _accelEvent;
        private OBDEvent _obdEvent;

        private DispatcherTimer _compassEventTimer;
        private DispatcherTimer _accelEventTimer;
        private DispatcherTimer _carCrashTimer;

        private ICommand _selectGoodDriverCommand;
        private ICommand _selectBadDriverCommand;
        private ICommand _selectCarCrashCommand;
        private ICommand _selectDriverCommand;
        private ICommand _toggleDrivingSessionCommand;
        private ICommand _sendOBDParkingCommand;
        private ICommand _sendOBDSecurityBeltCommand;
        private ICommand _sendOBDFuelCommand;
        private ICommand _toggleDriversPanelCommand;
        private ICommand _toggleDriverSelectionPanelCommand;


        public MainViewModel()
        {
            Initialize();
        }

        public ObservableCollection<Driver> AvailableDrivers
        {
            get { return _availableDrivers; }
            set
            {
                _availableDrivers = value;
                NotifyOfPropertyChange(() => AvailableDrivers);
            }
        }

        public bool DrivingSessionStarted
        {
            get { return _drivingSessionStarted; }
            set
            {
                _drivingSessionStarted = value;
                NotifyOfPropertyChange(() => DrivingSessionStarted);
            }
        }

        public bool DriverSelected
        {
            get { return _driverSelected; }
            set
            {
                _driverSelected = value;
                NotifyOfPropertyChange(() => DriverSelected);
            }
        }

        public bool IsCarCrashed
        {
            get { return _isCarCrashed; }
            set
            {
                _isCarCrashed = value;
                NotifyOfPropertyChange(() => IsCarCrashed);
            }
        }

        public bool ShowDriverPanel
        {
            get { return _showDriverPanel; }
            set
            {
                _showDriverPanel = value;
                NotifyOfPropertyChange(() => ShowDriverPanel);
            }
        }

        public bool ShowDriverSelectionPanel
        {
            get { return _showDriverSelectionPanel; }
            set
            {
                _showDriverSelectionPanel = value;
                NotifyOfPropertyChange(() => ShowDriverSelectionPanel);
            }
        }

        public string LicensePlate
        {
            get { return _licensePlate; }
            set
            {
                _licensePlate = value;
                NotifyOfPropertyChange(() => LicensePlate);
            }
        }

        public bool ParkingBrakeChecked
        {
            get { return _parkingBrakeChecked; }
            set
            {
                _parkingBrakeChecked = value;
                NotifyOfPropertyChange(() => ParkingBrakeChecked);
            }
        }

        public bool SeatBeltChecked
        {
            get { return _seatBeltChecked; }
            set
            {
                _seatBeltChecked = value;
                NotifyOfPropertyChange(() => SeatBeltChecked);
            }
        }

        public bool FuelChecked
        {
            get { return _fuelChecked; }
            set
            {
                _fuelChecked = value;
                NotifyOfPropertyChange(() => FuelChecked);
            }
        }

        public CompassEvent CompassEvent
        {
            get { return _compassEvent; }
            set
            {
                _compassEvent = value;
                NotifyOfPropertyChange(() => CompassEvent);
            }
        }

        public AccelerometerEvent AccelerometerEvent
        {
            get { return _accelEvent; }
            set
            {
                _accelEvent = value;
                NotifyOfPropertyChange(() => AccelerometerEvent);
            }
        }

        public OBDEvent OBDEvent
        {
            get { return _obdEvent; }
            set
            {
                _obdEvent = value;
                NotifyOfPropertyChange(() => OBDEvent);
            }
        }

        public SessionStatus SessionStatus
        {
            get { return _sessionStatus; }
            set
            {
                _sessionStatus = value;
                NotifyOfPropertyChange(() => SessionStatus);
            }
        }

        public Driver CurrentDriver
        {
            get { return _currentDriver; }
            set
            {
                _currentDriver = value;
                NotifyOfPropertyChange(() => CurrentDriver);
            }
        }


        public ICommand SelectGoodDriverCommand
        {
            get
            {
                return _selectGoodDriverCommand ??
                       (_selectGoodDriverCommand = new RelayCommand(SelectGoodDriverCommandExecute));
            }
        }

        public ICommand SelectBadDriverCommand
        {
            get
            {
                return _selectBadDriverCommand ??
                       (_selectBadDriverCommand = new RelayCommand(SelectBadDriverCommandExecute));
            }
        }

        public ICommand SelectDriverCommand
        {
            get
            {
                return _selectDriverCommand ??
                       (_selectDriverCommand = new RelayCommand(SelectDriverCommandExecute));
            }
        }


        public ICommand SelectCarCrashCommand
        {
            get
            {
                return _selectCarCrashCommand ??
                       (_selectCarCrashCommand = new RelayCommand(SelectCarCrashCommandExecute));
            }
        }

        public ICommand ToggleDrivingSessionCommand
        {
            get
            {
                return _toggleDrivingSessionCommand ??
                    (_toggleDrivingSessionCommand = new RelayCommand(ToggleDrivingSessionCommandExecute));
            }
        }

        public ICommand ToggleDriversPanelCommand
        {
            get
            {
                return _toggleDriversPanelCommand ??
                    (_toggleDriversPanelCommand = new RelayCommand(ToggleDriversPanelCommandExecute));
            }
        }

        public ICommand ToggleDriverSelectionPanelCommand
        {
            get
            {
                return _toggleDriverSelectionPanelCommand ??
                    (_toggleDriverSelectionPanelCommand = new RelayCommand(ToggleDriverSelectionPanelCommandExecute));
            }
        }

        public ICommand SendOBDParkingCommand
        {
            get
            {
                return _sendOBDParkingCommand ??
                    (_sendOBDParkingCommand = new RelayCommand(SendOBDParkingCommandExecute));
            }
        }

        public ICommand SendOBDSecurityBeltCommand
        {
            get
            {
                return _sendOBDSecurityBeltCommand ??
                    (_sendOBDSecurityBeltCommand = new RelayCommand(SendOBDSecurityBeltCommandExecute));
            }
        }

        public ICommand SendOBDFuelCommand
        {
            get
            {
                return _sendOBDFuelCommand ??
                    (_sendOBDFuelCommand = new RelayCommand(SendOBDFuelCommandExecute));
            }
        }


        private void Initialize()
        {
            _compassTimerCount = 0;
            _accelTimerCount = 0;
            _carCrashTimerCount = 0;

            SessionStatus = SessionStatus.Stopped;

            if (CurrentDriver == null)
            {
                CurrentDriver = new Driver { Image = "/Assets/user_unknow.png" };
            }

            CompassEvent = new CompassEvent
            {
                HeadingDegrees = 0
            };

            AccelerometerEvent = new AccelerometerEvent
            {
                X = 0000.00,
                Z = 0000.00,
                Y = 0000.00,
            };

            AvailableDrivers = new ObservableCollection<Driver>
            {
                new Driver { Id =  1, LicensePlate = "FGH-9876", Image="/Assets/Drivers/1.jpg" },
                new Driver { Id =  2, LicensePlate = "SLV-4335", Image="/Assets/Drivers/2.jpg"},
                new Driver { Id =  3, LicensePlate = "MAX-9876", Image="/Assets/Drivers/3.jpg"},
                new Driver { Id =  4, LicensePlate = "CCC-1432", Image="/Assets/Drivers/4.jpg"},
                new Driver { Id =  5, LicensePlate = "JNV-9876", Image="/Assets/Drivers/5.jpg"},
                new Driver { Id =  6, LicensePlate = "CUA-1456", Image="/Assets/Drivers/6.jpg"},
                new Driver { Id =  7, LicensePlate = "JOP-9876", Image="/Assets/Drivers/7.jpg"},
                new Driver { Id =  8, LicensePlate = "HTY-1243", Image="/Assets/Drivers/8.jpg"},
                new Driver { Id =  9, LicensePlate = "VVV-4444", Image="/Assets/Drivers/9.jpg"},
                new Driver { Id = 10, LicensePlate = "ERT-1256", Image="/Assets/Drivers/10.jpg" }
            };
        }

        private void DriverLogin()
        {
            LicensePlate = CurrentDriver.LicensePlate;

            Sender.SendEvent(new RfidEvent
            {
                DeviceId = CurrentDriver.LicensePlate,
                DriverId = CurrentDriver.Id
            });

        }

        private void StartTimers()
        {
            if (_compassEventTimer == null)
            {
                _compassEventTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                _compassEventTimer.Tick += CompassEventTimerTickHandler;
            }

            if (_accelEventTimer == null)
            {
                _accelEventTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
                _accelEventTimer.Tick += AccelEventTimerTickHandler;
            }

            if (_carCrashTimer == null)
            {
                _carCrashTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                _carCrashTimer.Tick += CarCrashTickHandler;
            }

            _accelEventTimer.Start();
            _compassEventTimer.Start();
        }

        private void StopTimers()
        {
            _compassEventTimer.Stop();
            _accelEventTimer.Stop();
            _carCrashTimer.Stop();
        }

        private void ToggleDriversPanel()
        {
            ShowDriverPanel = !ShowDriverPanel;
        }

        private void ToggleDriverSelectionPanel()
        {
            ShowDriverSelectionPanel = !ShowDriverSelectionPanel;
        }


        private void ToggleDrivingSessionCommandExecute(object obj)
        {
            if (DrivingSessionStarted)
            {
                DriverLogin();
                Initialize();
                StartTimers();
                SessionStatus = SessionStatus.GoodDriver;
            }
            else
            {
                Initialize();
                StopTimers();
                if (ShowDriverPanel)
                    ToggleDriversPanel();
            }
        }

        private void SelectGoodDriverCommandExecute(object obj)
        {
            SessionStatus = SessionStatus.GoodDriver;
            ToggleDriversPanel();
        }

        private void SelectBadDriverCommandExecute(object obj)
        {
            SessionStatus = SessionStatus.BadDriver;
            ToggleDriversPanel();
        }

        private void SelectDriverCommandExecute(object obj)
        {
            CurrentDriver = obj as Driver;
            DriverLogin();
            ToggleDriverSelectionPanel();
            DataService.DriverId = CurrentDriver.Id;
            DataService.DeviceId = CurrentDriver.LicensePlate;
            DriverSelected = true;
        }

        private void SelectCarCrashCommandExecute(object obj)
        {
            SessionStatus = SessionStatus.CarCrash;
            ToggleDriversPanel();
            _accelEventTimer.Stop();
            _carCrashTimer.Start();
        }

        private void SendOBDParkingCommandExecute(object obj)
        {
            OBDEvent = DataService.GetOBDData(ParkingBrakeChecked ? Properties.Settings.Default.OBDParkingOn : Properties.Settings.Default.OBDParkingOff);
            Sender.SendEvent(OBDEvent);
        }

        private void SendOBDSecurityBeltCommandExecute(object obj)
        {
            OBDEvent = DataService.GetOBDData(SeatBeltChecked ? Properties.Settings.Default.OBDSecurityBeltOn : Properties.Settings.Default.OBDSecurityBeltOff);
            Sender.SendEvent(OBDEvent);
        }

        private void SendOBDFuelCommandExecute(object obj)
        {
            OBDEvent = DataService.GetOBDData(FuelChecked ? Properties.Settings.Default.OBDFuelOn : Properties.Settings.Default.OBDFuelOff);
            Sender.SendEvent(OBDEvent);
        }

        private void ToggleDriversPanelCommandExecute(object obj)
        {
            ToggleDriversPanel();
        }

        private void ToggleDriverSelectionPanelCommandExecute(object obj)
        {
            ToggleDriverSelectionPanel();
        }

        private void CompassEventTimerTickHandler(object sender, EventArgs e)
        {
            _compassTimerCount = _compassTimerCount % Properties.Settings.Default.NumDataRegisters;

            CompassEvent = DataService.GetCompassData();
            Sender.SendEvent(CompassEvent);

            _compassTimerCount++;
        }

        private void AccelEventTimerTickHandler(object sender, EventArgs e)
        {
            _accelTimerCount = _accelTimerCount % Properties.Settings.Default.NumDataRegisters;

            AccelerometerEvent = DataService.GetDrivingData(_accelTimerCount, SessionStatus);
            Sender.SendEvent(AccelerometerEvent);

            _accelTimerCount++;

        }

        private void CarCrashTickHandler(object sender, EventArgs e)
        {
            if (_carCrashTimerCount < Properties.Settings.Default.CrashDataRegisters)
            {
                AccelerometerEvent = DataService.GetCrashData(_carCrashTimerCount);
                Sender.SendEvent(AccelerometerEvent);
                _carCrashTimerCount++;
            }
            else
            {
                StopTimers();
                IsCarCrashed = true;
                DrivingSessionStarted = false;
            }
        }
    }
}
