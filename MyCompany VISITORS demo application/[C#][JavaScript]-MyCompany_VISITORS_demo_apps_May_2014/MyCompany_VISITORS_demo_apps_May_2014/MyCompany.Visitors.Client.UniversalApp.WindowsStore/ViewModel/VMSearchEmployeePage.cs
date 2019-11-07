namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.Services.SampleData;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel.Resources;
    using Windows.ApplicationModel.Search;

    /// <summary>
    /// Search Employee Page ViewModel.
    /// </summary>
    public class VMSearchEmployeePage : VMBase
    {
        private const int EMPLOYEES_PAGEZERO = 0;
        private const int EMPLOYEES_TO_RETRIEVE = 30;

        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;
        private readonly ISampleDataService sampleDataService;

        private RelayCommand navigateBackCommand;
        private RelayCommand<int> navigateToNewVisitCommand;

        private ObservableCollection<Employee> employees;
        private string textToSearch = string.Empty;

        private CancellationTokenSource searchCancellationTokenSource;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMSearchEmployeePage(INavigationService navService, IMyCompanyClient clientService,
                                    IMessageService messageService, ISampleDataService sampleDataService)
        {
            this.navService = navService;
            this.clientService = clientService;
            this.messageService = messageService;
            this.sampleDataService = sampleDataService;
            InitializeCommands();

            if (base.IsInDesignMode)
                InitializeMockedData();
        }

        /// <summary>
        /// Employees List.
        /// </summary>
        public ObservableCollection<Employee> EmployeesList
        {
            get
            {
                return this.employees;
            }
            set
            {
                this.employees = value;
                base.RaisePropertyChanged(() => EmployeesList);
            }
        }

        /// <summary>
        /// Text to search an employee.
        /// </summary>
        public string TextToSearch
        {
            get { return this.textToSearch; }
            set
            {
                this.textToSearch = value;
                SearchQueryChanged();
                RaisePropertyChanged(() => TextToSearch);
            }
        }

        /// <summary>
        /// Navigate Back Command.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return this.navigateBackCommand; }
        }

        /// <summary>
        /// Navigate to New Visit Command.
        /// </summary>
        public ICommand NavigateToNewVisitCommand
        {
            get { return this.navigateToNewVisitCommand; }
        }

        /// <summary>
        /// Initialize Data method.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeData()
        {
            await this.SearchEmployee(TextToSearch);
        }

        private void InitializeMockedData()
        {
            var employeesMocked = this.sampleDataService.GetEmployees();
            EmployeesList = new ObservableCollection<Employee>(employeesMocked);
        }

        private void SearchQueryChanged()
        {
            if (this.searchCancellationTokenSource != null)
                this.searchCancellationTokenSource.Cancel();

            this.searchCancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = this.searchCancellationTokenSource.Token;

            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(600));
                if (!ct.IsCancellationRequested)
                {
                    await SearchEmployee(TextToSearch);
                }
            }, ct, TaskCreationOptions.None, uiScheduler);
        }

        private async Task SearchEmployee(string filter)
        {
            try
            {
                IsBusy = true;
                EmployeesList = new ObservableCollection<Employee>();
                IList<Employee> results = await this.clientService.EmployeeService.GetEmployees(filter, PictureType.Small, EMPLOYEES_TO_RETRIEVE, EMPLOYEES_PAGEZERO);
                if (results != null)
                    EmployeesList = new ObservableCollection<Employee>(results);
            }
            catch (Exception)
            {
                var loader = new ResourceLoader();
                messageService.ShowMessage(loader.GetString("ErrorOcurred"), loader.GetString("Error"));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void InitializeCommands()
        {
            this.navigateBackCommand = new RelayCommand(NavigateBackExecute);
            this.navigateToNewVisitCommand = new RelayCommand<int>(NavigateToNewVisitExecute);
        }

        private void NavigateBackExecute()
        {
            this.navService.GoBack();
        }

        private void NavigateToNewVisitExecute(int employeeId)
        {
            this.navService.NavigateToNewVisitPageFromEmployeeSearch(employeeId);
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            searchCancellationTokenSource.Dispose();
            this.employees = null;
        }
    }
}
