namespace MyShuttle.Dashboard.Client.ViewModels
{
    using Models;
    using Repositories;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Windows.UI.Xaml.Navigation;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Foundation;
    using Windows.UI.Xaml.Media;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.Mvvm;
    using Repositories.Abstract;
    using Services.Abstract;
    using System.Linq;

    public class DriverHubPageViewModel : BaseViewModel
    {
        private Driver _driver;
        private VehicleData _vehicleData;
        private PointCollection _drivingStylePoints;
        private readonly IDriversRepository _driversRepository;
        private IReadOnlyCollection<VehicleSummaryData> _generalStatistics;
        private readonly INavigationService _navigationService;
        private bool _driverIsLoading;
        private bool _statisticsIsLoading;
        public event EventHandler RatingsDataLoaded;

        public DriverHubPageViewModel(IDriversRepository driversRepository, IAlertMessageService alertMessageService, INavigationService navigationService)
            : base (alertMessageService)

        {
            _driversRepository = driversRepository;
            _navigationService = navigationService;

            GoCarHubPageCommand = new DelegateCommand(GoCarHubPage);
        }

        public DelegateCommand GoCarHubPageCommand { get; private set; }

        public Driver Driver
        {
            get { return _driver; }
            private set { SetProperty(ref _driver, value); }
        }

        public VehicleData VehicleData
        {
            get { return _vehicleData; }
            private set { SetProperty(ref _vehicleData, value); }
        }

        public IReadOnlyCollection<VehicleSummaryData> GeneralStatistics
        {
            get { return _generalStatistics; }
            private set { SetProperty(ref _generalStatistics, value); }
        }

        public PointCollection DrivingStylePoints
        {
            get { return _drivingStylePoints; }
            set { SetProperty(ref _drivingStylePoints, value); }
        }

        public bool DriverIsLoading
        {
            get { return _driverIsLoading; }
            private set
            {
                SetProperty(ref _driverIsLoading, value);
            }
        }

        public bool StatisticsIsLoading
        {
            get { return _statisticsIsLoading; }
            private set
            {
                SetProperty(ref _statisticsIsLoading, value);
            }
        }

        protected async override Task LoadData(object navigationParameter)
        {
            Driver = navigationParameter as Driver;

            var task1 = LoadGeneralStatistics();
            var task2 = LoadDrivingStyle();
            var task3 = LoadDriverInfo();

            await Task.WhenAll(task1, task2, task3);
        }
        
        private async Task LoadDriverInfo()
        {
            DriverIsLoading = true;

            var driverInfo = await _driversRepository.GetDriverAsync(Driver.DriverId);

            VehicleData = new VehicleData
            {
                Device = driverInfo.MostUsedVehicleDevice,
                Make = driverInfo.MostUsedVehicleMake,
                Model = driverInfo.MostUsedVehicleMake + " " + driverInfo.MostUsedVehicleModel,
                Picture = driverInfo.MostUsedVehiclePhoto,
                VehicleId = driverInfo.MostUsedVehicleId,
                Rides = driverInfo.DriverTotalRides
            };

            DriverIsLoading = false;

        }

        private async Task LoadGeneralStatistics()
        {
            StatisticsIsLoading = true;

            var statistics = await _driversRepository.GetStatisticsAsync(Driver.DriverId);

            var vehicleData = MapStatisticsToVehicleData(statistics);

            GeneralStatistics = new ReadOnlyCollection<VehicleSummaryData>(vehicleData);

            StatisticsIsLoading = false;
        }

        private static List<VehicleSummaryData> MapStatisticsToVehicleData(StatisticsResult statistics)
        {
            var vehicleData = new List<VehicleSummaryData>()
            {
                new VehicleSummaryData
                {
                    Index = 0,
                    Name = "Avg Speed",
                    Value = statistics.Driver.AvgSpeed.ToString("0.0"),
                    Unit = "mph",
                    GlobalValue = statistics.Global.AvgSpeed.ToString("0.0")
                },
                new VehicleSummaryData
                {
                    Index = 1,
                    Name = "Miles Traveled",
                    Value = statistics.Driver.Miles.ToString("0.0"),
                    Unit = "",
                    GlobalValue = statistics.Global.Miles.ToString("0.0")
                },
                new VehicleSummaryData
                {
                    Index = 2,
                    Name = "Breakdowns",
                    Value = statistics.Driver.Breakdowns.ToString("0"),
                    GlobalValue = statistics.Global.Breakdowns.ToString("0")
                }
            };
            return vehicleData;
        }

        private void GoCarHubPage()
        {
            _navigationService.Navigate("CarHub", VehicleData);
        }

        private async Task LoadDrivingStyle()
        {
            var drivingStyleInfo = await _driversRepository.GetDrivingStyleAsync(Driver.DriverId);
            DrivingStylePoints = MapDrivingStyleToPointCollection(drivingStyleInfo);

            await Task.Delay(1000);

            RatingsDataLoaded?.Invoke(this, EventArgs.Empty);

        }

        private PointCollection MapDrivingStyleToPointCollection(DrivingStyleResult drivingStyleInfo)
        {
            var drivingStyleValues = new[]
            {
                drivingStyleInfo.Aggressiveness,
                drivingStyleInfo.Speed,
                drivingStyleInfo.Brakes,
                drivingStyleInfo.Consumption,
                drivingStyleInfo.Profiability,
                drivingStyleInfo.Breakdowns
            };

            var angles = new[] { 270, 330, 30, 90, 150, 210, };
            const int maxRadius = 150;
            const int minRadius = 30;
            const int maxValue = 10;

            var drivingStylePoints = new PointCollection();

            for (var i = 0; i < 6; i++)
            {
                var angle = (Math.PI / 180) * angles[i];

                var value = drivingStyleValues[i] == -1 ? 0 : drivingStyleValues[i];
                var radius = value * maxRadius / maxValue;
                radius = radius < minRadius ? minRadius : radius;
                var xPos = radius * Math.Cos(angle) + 250;
                var yPos = radius * Math.Sin(angle) + 195;
                drivingStylePoints.Add(new Point(xPos, yPos));
            }

            return drivingStylePoints;
        }

        public void Refresh()
        {
            if (_generalStatistics != null) GeneralStatistics = new ReadOnlyCollection<VehicleSummaryData>(_generalStatistics.ToList());
        }
    }


}
