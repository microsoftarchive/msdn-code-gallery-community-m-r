namespace MyShuttle.Dashboard.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Models;
    using Repositories.Abstract;
    using Services.Abstract;
    using System.Globalization;
    using System.Linq;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;

    public class CarHubPageViewModel : BaseViewModel
    {
        private VehicleDetailResult _vehicleDetail;
        private string _vehicleModel;
        private VehicleData _vehicleData;
        private readonly IVehiclesRepository _vehiclesRepository;
        private IReadOnlyCollection<VehicleAlarmResult> _alarms;
        public event EventHandler VehicleDataLoaded;
        private bool _alarmsIsLoading;
        private ObservableCollection<MilesChartData> _traveledMiles;
        private ObservableCollection<MilesChartData> _invoicedMiles;
        private readonly IResourceLoader _resourceLoader;
        private ObservableCollection<ComboBoxItemValue> _months;
        private ObservableCollection<int> _years;
        private int _selectedMonth;
        private int _selectedYear;
        private bool _isYearSelectionChartActive;
        private string _chartDescription;
        private int startDay;

        public CarHubPageViewModel(IVehiclesRepository vehiclesRepository, IAlertMessageService alertMessageService, IResourceLoader resourceLoader)
            : base(alertMessageService)
        {
            _vehiclesRepository = vehiclesRepository;
            _resourceLoader = resourceLoader;

            SelectMonthCommand = new DelegateCommand<object>(SelectMonth);
            SelectYearCommand = new DelegateCommand<object>(SelectYear);
            NextMonthCommand = new DelegateCommand<object>(NextMonth, CanNextMonth);
            PreviousMonthCommand = new DelegateCommand<object>(PreviousMonth, CanPreviousMonth);
        }

        private bool CanPreviousMonth(object arg)
        {
            return startDay > 0;
        }

        private bool CanNextMonth(object arg)
        {
            if (SelectedYear == 0) return false;

            var days = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

            return startDay + 10 < days;
        }

        // HACK C# 6 Feature - Expression bodies functions
        private string GetMonthName(int month, bool abbreviated = false) =>
            abbreviated
                ? CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month)
                : CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

        private async void SelectYear(object obj)
        {
            var indexYear = (int)obj;
            SelectedYear = Years[indexYear];
            ChartDescription = SelectedYear.ToString();
            IsYearSelectionChartActive = true;
            await LoadCharDataPerYear();
        }

        private async void SelectMonth(object obj)
        {
            var indexMonth = (int)obj;
            SelectedMonth = Months[indexMonth].Id;
            ChartDescription = GetMonthName(SelectedMonth);
            IsYearSelectionChartActive = false;
            startDay = 0;
            await LoadCharDataPerMonth();
        }

        private void NextMonth(object obj)
        {
            var days = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
            if (startDay + 20 < days) startDay += 10;
            else startDay = days - 10;
            GetValue();
        }

        private void PreviousMonth(object obj)
        {
            if (startDay - 10 > 0) startDay -= 10;
            else startDay = 0;
            GetValue();
        }

        private IEnumerable<VehicleMilesMonthResult> dataCache;

        // TODO: Refactor
        private async Task LoadCharDataPerMonth()
        {
            if (SelectedYear == 0) return;

            dataCache = await _vehiclesRepository.GetMilesPerMonth(_vehicleData.Device, SelectedYear, SelectedMonth);

            GetValue();
        }

        private void GetValue()
        {
            var totalMiles = new List<MilesChartData>();
            var invoicedMiles = new List<MilesChartData>();

            for (var i = startDay; i < startDay + 10; i++)
            {
                var dataMiles = dataCache.FirstOrDefault(x => x.Day == i + 1);
                var dt = new DateTime(SelectedYear, SelectedMonth, i + 1);

                totalMiles.Add(new MilesChartData { Month = dt.ToString("dd ddd"), Quantity = dataMiles?.TotalMiles ?? 0 });
                invoicedMiles.Add(new MilesChartData { Month = dt.ToString("dd ddd"), Quantity = dataMiles?.InvoicedMiles ?? 0 });
            }

            TraveledMiles = new ObservableCollection<MilesChartData>(totalMiles);
            InvoicedMiles = new ObservableCollection<MilesChartData>(invoicedMiles);

            NextMonthCommand.RaiseCanExecuteChanged();
            PreviousMonthCommand.RaiseCanExecuteChanged();
        }

        // TODO: Refactor
        private async Task LoadCharDataPerYear()
        {
            var data = await _vehiclesRepository.GetMilesPerYear(_vehicleData.Device, SelectedYear);

            var totalMiles = new List<MilesChartData>();
            var invoicedMiles = new List<MilesChartData>();

            for (var i = 0; i < 12; i++)
            {
                var dataMiles = data.FirstOrDefault(x => x.Month == i + 1);

                totalMiles.Add(new MilesChartData { Month = GetMonthName(i + 1, true), Quantity = dataMiles?.TotalMiles ?? 0 });
                invoicedMiles.Add(new MilesChartData { Month = GetMonthName(i + 1, true), Quantity = dataMiles?.InvoicedMiles ?? 0 });
            }

            TraveledMiles = new ObservableCollection<MilesChartData>(totalMiles);
            InvoicedMiles = new ObservableCollection<MilesChartData>(invoicedMiles);
        }

        public VehicleDetailResult VehicleDetail
        {
            get { return _vehicleDetail; }
            private set { SetProperty(ref _vehicleDetail, value); }
        }

        public string VehicleModel
        {
            get { return _vehicleModel; }
            private set { SetProperty(ref _vehicleModel, value); }
        }

        public bool AlarmsIsLoading
        {
            get { return _alarmsIsLoading; }
            private set
            {
                SetProperty(ref _alarmsIsLoading, value);
            }
        }

        public IReadOnlyCollection<VehicleAlarmResult> Alarms
        {
            get { return _alarms; }
            private set { SetProperty(ref _alarms, value); }
        }

        public ObservableCollection<MilesChartData> TraveledMiles
        {
            get { return _traveledMiles; }
            private set { SetProperty(ref _traveledMiles, value); }
        }

        public ObservableCollection<MilesChartData> InvoicedMiles
        {
            get { return _invoicedMiles; }
            private set { SetProperty(ref _invoicedMiles, value); }
        }

        public ObservableCollection<ComboBoxItemValue> Months
        {
            get { return _months; }
            private set { SetProperty(ref _months, value); }
        }

        public ObservableCollection<int> Years
        {
            get { return _years; }
            private set { SetProperty(ref _years, value); }
        }

        public int SelectedMonth
        {
            get { return _selectedMonth; }
            private set { SetProperty(ref _selectedMonth, value); }
        }

        public int SelectedYear
        {
            get { return _selectedYear; }
            private set { SetProperty(ref _selectedYear, value); }
        }

        public bool IsYearSelectionChartActive
        {
            get { return _isYearSelectionChartActive; }
            private set { SetProperty(ref _isYearSelectionChartActive, value); }
        }

        public string ChartDescription
        {
            get { return _chartDescription; }
            private set { SetProperty(ref _chartDescription, value); }
        }

        public DelegateCommand<object> SelectMonthCommand { get; private set; }
        public DelegateCommand<object> SelectYearCommand { get; private set; }
        public DelegateCommand<object> NextMonthCommand { get; }
        public DelegateCommand<object> PreviousMonthCommand { get; }

        protected async override Task LoadData(object navigationParameter)
        {
            _vehicleData = navigationParameter as VehicleData;

            var task1 = LoadVehicleDetail();
            var task2 = LoadVehicleAlarms();
            var task3 = LoadTraveledInvoicedMiles();

            await Task.WhenAll(task1, task2, task3);
        }

        private async Task LoadVehicleDetail()
        {
            VehicleModel = "Loading...";

            var vehicleDetail = await _vehiclesRepository.GetVehicleDetailAsync(_vehicleData.Device);

            await Task.Delay(1000);

            VehicleDetail = vehicleDetail;

            VehicleModel = _vehicleData.Model;

            // HACK C# 6 Feature - Null-conditional operator
            VehicleDataLoaded?.Invoke(this, EventArgs.Empty);
        }

        private async Task LoadVehicleAlarms()
        {
            AlarmsIsLoading = true;

            var vehicleAlarms = await _vehiclesRepository.GetVehicleAlarmsAsync(_vehicleData.Device);

            var vehicleAlarmResults = vehicleAlarms as VehicleAlarmResult[] ?? vehicleAlarms.ToArray();

            foreach (var vehicleAlarm in vehicleAlarmResults)
            {
                vehicleAlarm.Description = _resourceLoader.GetString(vehicleAlarm.Code);
            }

            Alarms = new ReadOnlyCollection<VehicleAlarmResult>(vehicleAlarmResults.Take(8).ToList());

            AlarmsIsLoading = false;
        }

        private async Task LoadTraveledInvoicedMiles()
        {
            await Task.Delay(1000);

            var months = new List<ComboBoxItemValue>();

            for (var i = 1; i < 13; i++)
            {
                months.Add(new ComboBoxItemValue { Id = i, Value = GetMonthName(i, true).ToUpper() });
            }

            Months = new ObservableCollection<ComboBoxItemValue>(months);
            SelectedMonth = DateTime.UtcNow.Month;

            var years = new List<int>();

            for (var i = 0; i < 2; i++)
            {
                years.Add(DateTime.UtcNow.Year - i);
            }

            Years = new ObservableCollection<int>(years);
            SelectedYear = DateTime.UtcNow.Year;

            IsYearSelectionChartActive = true;
        }
    }
}
