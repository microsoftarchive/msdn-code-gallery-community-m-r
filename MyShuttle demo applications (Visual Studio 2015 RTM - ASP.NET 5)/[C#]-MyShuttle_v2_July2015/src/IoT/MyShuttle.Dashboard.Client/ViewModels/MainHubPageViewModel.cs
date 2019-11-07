namespace MyShuttle.Dashboard.Client.ViewModels
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using Windows.UI.Xaml.Navigation;
    using System.Threading.Tasks;
    using Microsoft.Practices.Prism.Mvvm;
    using Repositories.Abstract;
    using Services.Abstract;
    using System.Linq;

    public class MainHubPageViewModel : BaseViewModel
    {
        private const int MaxTopDrivers = 5;
        private const int DaysServiceSatisfaction = 20;
        private IReadOnlyCollection<Driver> _topDrivers;
        private IReadOnlyCollection<VehicleSummaryData> _vehicleData;
        private ServicesSatisfactionData _satisfactionData;
        private readonly IDriversRepository _driversRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IServicesRepository _servicesRepository;
        private int _totalVehicles;
        private int _totalDrivers;
        private bool _topDriversIsLoading;
        private bool _vehiclesIsLoading;
        private bool _servicesSatisfactionIsLoaded;
        public event EventHandler ChartLoaded;

        public MainHubPageViewModel(IDriversRepository driversRepository,
            IVehiclesRepository vehiclesRepository,
            IServicesRepository servicesRepository,
            IAlertMessageService alertMessageService) : base(alertMessageService)
        {
            _driversRepository = driversRepository;
            _vehiclesRepository = vehiclesRepository;
            _servicesRepository = servicesRepository;
        }

        public IReadOnlyCollection<Driver> TopDrivers
        {
            get { return _topDrivers; }
            private set
            {
                SetProperty(ref _topDrivers, value);
                OnPropertyChanged(() => TopDriversHeader);
            }
        }

        public IReadOnlyCollection<VehicleSummaryData> VehicleData
        {
            get { return _vehicleData; }
            private set
            {
                SetProperty(ref _vehicleData, value);
                OnPropertyChanged(() => VehiclesHeader);
            }
        }

        public ServicesSatisfactionData SatisfactionData
        {
            get { return _satisfactionData; }
            private set
            {
                SetProperty(ref _satisfactionData, value);
            }
        }

        public bool ServicesSatisfactionIsLoaded
        {
            get { return _servicesSatisfactionIsLoaded; }
            protected set
            {
                SetProperty(ref _servicesSatisfactionIsLoaded, value);
            }
        }

        public bool TopDriversIsLoading
        {
            get { return _topDriversIsLoading; }
            private set
            {
                SetProperty(ref _topDriversIsLoading, value);
            }
        }

        public bool VehiclesIsLoading
        {
            get { return _vehiclesIsLoading; }
            private set
            {
                SetProperty(ref _vehiclesIsLoading, value);
            }
        }

        // HACK C# 6 Feature - Expression bodies properties & string interpolation
        public string TopDriversHeader => TopDrivers == null ? "top drivers" : $"top drivers ({_totalDrivers})";

        public string VehiclesHeader => VehicleData == null ? "vehicles" : $"vehicles ({_totalVehicles})";


        protected override async Task LoadData(object navigationParameter)
        {
            var task1 = LoadTopDrivers();
            var task2 = LoadVehicles();
            var task3 = LoadServices();

            await Task.WhenAll(task1, task2, task3);
        }

        public async Task LoadTopDrivers()
        {
            TopDriversIsLoading = true;

            var topDriversResult = await _driversRepository.GetTopRatedDriversAsync(true, MaxTopDrivers);
            MapTopDriversResult(topDriversResult);

            TopDriversIsLoading = false;
        }

        public async Task LoadVehicles()
        {
            VehiclesIsLoading = true;

            var vehicleSummaryResult = await _vehiclesRepository.GetVehiclesDataAsync();
            MapSummaryVehicle(vehicleSummaryResult);

            VehiclesIsLoading = false;
        }

        public async Task LoadServices()
        {
            var servicesSatisfactionResult = await _servicesRepository.GetServicesSatisfaction(DaysServiceSatisfaction);
            MapServicesSatisfaction(servicesSatisfactionResult);
        }

        private void MapSummaryVehicle(VehicleSummaryResult dataResult)
        {
            var vehicleData = new List<VehicleSummaryData>()
            {
                new VehicleSummaryData
                {
                    Index = 0,
                    Name = "Avg Speed",
                    Value = dataResult.AverageSpeed.ToString("0.0"),
                    Unit = "mph"
                },
                new VehicleSummaryData
                {
                    Index = 1,
                    Name = "Miles Traveled",
                    Value = dataResult.TotalMiles.ToString("0.0"),
                    Unit = "miles"
                },
                new VehicleSummaryData
                {
                    Index = 2,
                    Name = "Breakdowns",
                    Value = dataResult.Breakdowns
                },
                new VehicleSummaryData
                {
                    Index = 3,
                    Name = "Accidents",
                    Value = dataResult.Accidents
                }
            };

            _totalVehicles = dataResult.VehiclesCount;
            VehicleData = new ReadOnlyCollection<VehicleSummaryData>(vehicleData);
        }

        private void MapTopDriversResult(TopDriversResult dataResult)
        {
            _totalDrivers = dataResult.TotalItems;

            TopDrivers = new ObservableCollection<Driver>(dataResult.Items); ;
        }

        private async void MapServicesSatisfaction(ServicesSatisfactionResult dataResult)
        {
            await Task.Delay(1000);

            SatisfactionData = new ServicesSatisfactionData
            {
                AcceptedPercent = dataResult.Satisfactions[0].AcceptedPercent,
                AcceptedDifference = dataResult.AcceptedDifference,
                PositivePercent = dataResult.Satisfactions[0].SatisfactionPercent,
                PositiveDifference = dataResult.PositivesDifference
            };

            ServicesSatisfactionIsLoaded = true;
            
            // HACK C# 6 Feature - Null-conditional operator
            ChartLoaded?.Invoke(this, EventArgs.Empty);
        }

        public void Refresh()
        {
            if (_topDrivers != null) TopDrivers = new ReadOnlyCollection<Driver>(_topDrivers.ToList());
            if (_vehicleData != null) VehicleData = new ReadOnlyCollection<VehicleSummaryData>(_vehicleData.ToList());
        }
    }
}
