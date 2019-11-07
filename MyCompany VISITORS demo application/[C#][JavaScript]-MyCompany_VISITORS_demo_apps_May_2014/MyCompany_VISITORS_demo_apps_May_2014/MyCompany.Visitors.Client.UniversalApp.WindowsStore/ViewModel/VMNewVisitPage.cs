namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// New Visit page ViewModel.
    /// </summary>
    public class VMNewVisitPage : VMBase
    {
        private const int YEAR_INTERVAL = 4;

        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;

        private RelayCommand navigateBackCommand;
        private RelayCommand navigateToSearchVisitorCommand;
        private RelayCommand navigateToSearchEmployeeCommand;
        private RelayCommand sendNewVisitCommand;
        private RelayCommand confirmNavigateBackCommand;
        private RelayCommand remainAtPageCommand;

        private Visitor visitor;
        private Employee employee;
        private Visit visit;

        private DateTimeOffset visitDate;
        private TimeSpan visitTime;
        private DateTimeOffset minYear;
        private DateTimeOffset maxYear;

        private bool areEnabledButtoms;
        private bool isConfirmationPopupOpened;
        private bool isAppBarOpened;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMNewVisitPage(INavigationService navService, IMyCompanyClient clientService,
                              IMessageService messageService)
        {
            this.navService = navService;
            this.clientService = clientService;
            this.messageService = messageService;
            if (base.IsInDesignMode)
            {
                Visitor = new Visitor();
                Employee = new Employee();
            }
            InitializeCommands();
        }

        /// <summary>
        /// Property that indicates if user is in the page
        /// </summary>
        public bool IsCreatingVisit = false;

        /// <summary>
        /// Visitor.
        /// </summary>
        public Visitor Visitor
        {
            get { return this.visitor; }
            set
            {
                this.visitor = value;
                base.RaisePropertyChanged(() => Visitor);
            }
        }

        /// <summary>
        /// Employee.
        /// </summary>
        public Employee Employee
        {
            get { return this.employee; }
            set
            {
                this.employee = value;
                base.RaisePropertyChanged(() => Employee);
            }
        }

        /// <summary>
        /// Visit.
        /// </summary>
        public Visit Visit
        {
            get { return this.visit; }
            set
            {
                this.visit = value;
                base.RaisePropertyChanged(() => Visit);
            }
        }

        /// <summary>
        /// Has Vehicle.
        /// </summary>
        public bool HasVehicle
        {
            get { return this.visit.HasCar; }
            set
            {
                this.visit.HasCar = value;
                RaisePropertyChanged(() => HasVehicle);
            }
        }

        /// <summary>
        /// Visit date.
        /// </summary>
        public DateTimeOffset VisitDate
        {
            get { return this.visitDate; }
            set
            {
                this.visitDate = value;
                RaisePropertyChanged(() => VisitDate);
            }
        }

        /// <summary>
        /// Visit time.
        /// </summary>
        public TimeSpan VisitTime
        {
            get { return this.visitTime; }
            set
            {
                this.visitTime = value;
                RaisePropertyChanged(() => VisitTime);
            }
        }

        /// <summary>
        /// Maximum year to select in datepicker.
        /// </summary>
        public DateTimeOffset MaxYear
        {
            get { return this.maxYear; }
            set
            {
                this.maxYear = value;
                RaisePropertyChanged(() => MaxYear);
            }
        }

        /// <summary>
        /// Maximum year to select in datepicker.
        /// </summary>
        public DateTimeOffset MinYear
        {
            get { return this.minYear; }
            set
            {
                this.minYear = value;
                RaisePropertyChanged(() => MinYear);
            }
        }

        /// <summary>
        /// Enabled buttoms,
        /// </summary>
        public bool AreEnabledButtoms
        {
            get { return this.areEnabledButtoms; }
            set
            {
                this.areEnabledButtoms = value;
                RaisePropertyChanged(() => AreEnabledButtoms);
            }
        }

        /// <summary>
        /// Open or close the popup
        /// </summary>
        public bool IsConfirmationPopupOpened
        {
            get
            {
                return this.isConfirmationPopupOpened;
            }
            set
            {
                this.isConfirmationPopupOpened = value;
                RaisePropertyChanged(() => IsConfirmationPopupOpened);
            }
        }

        /// <summary>
        /// Open or close the app bar
        /// </summary>
        public bool IsAppBarOpened
        {
            get
            {
                return this.isAppBarOpened;
            }
            set
            {
                this.isAppBarOpened = value;
                RaisePropertyChanged(() => IsAppBarOpened);
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
        /// Navigate to Search Visitor Command.
        /// </summary>
        public ICommand NavigateToSearchVisitorCommand
        {
            get { return this.navigateToSearchVisitorCommand; }
        }

        /// <summary>
        /// Navigate to Search Employee Command.
        /// </summary>
        public ICommand NavigateToSearchEmployeeCommand
        {
            get { return this.navigateToSearchEmployeeCommand; }
        }

        /// <summary>
        /// Send new Visit Command.
        /// </summary>
        public ICommand SendNewVisitCommand
        {
            get { return this.sendNewVisitCommand; }
        }

        /// <summary>
        /// Confirm navigate back comman
        /// </summary>
        public ICommand ConfirmNavigateBackCommand
        {
            get { return this.confirmNavigateBackCommand; }
        }

        /// <summary>
        /// Remain at page command
        /// </summary>
        public ICommand RemainAtPageCommand
        {
            get { return this.remainAtPageCommand; }
        }

        /// <summary>
        /// Initialize Data function.
        /// </summary>
        public void InitializeData()
        {
            try
            {
                InitializeDateTime();
                IsBusy = true;

                Visitor = new Visitor();

                Employee = new Employee();
                Visit = new Visit();

                HasVehicle = false;
                IsCreatingVisit = true;
            }
            catch (Exception)
            {
                var loader = new ResourceLoader();
                this.messageService.ShowMessage(loader.GetString("ErrorOcurred"), loader.GetString("Error"));
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Initialize Employee Function.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns>Employee</returns>
        public async Task InitializeEmployee(int employeeId = 0)
        {
            IsBusy = true;
            if (employeeId != 0)
            {
                Employee result = await this.clientService.EmployeeService.Get(employeeId, PictureType.Big);
                if (result != null)
                {
                    Employee = result;
                    IsBusy = false;
                }
                else
                {
                    var loader = new ResourceLoader();
                    messageService.ShowMessage(loader.GetString("ElementNotFound"), loader.GetString("Error"));
                }
            }
        }

        /// <summary>
        /// Initialize Visitor Function.
        /// </summary>
        /// <param name="visitorId">Visitor Id</param>
        /// <returns>Visitor</returns>
        public async Task InitializeVisitor(int visitorId = 0)
        {
            IsBusy = true;
            if (visitorId != 0)
            {
                var result = await this.clientService.VisitorService.Get(visitorId, PictureType.Big);
                if (result != null)
                {
                    Visitor = result;
                    IsBusy = false;
                }
                else
                {
                    var loader = new ResourceLoader();
                    messageService.ShowMessage(loader.GetString("ElementNotFound"), loader.GetString("Error"));
                }
            }
        }

        private void InitializeCommands()
        {
            this.navigateBackCommand = new RelayCommand(NavigateBackExecute);
            this.navigateToSearchVisitorCommand = new RelayCommand(NavigateToSearchVisitorExecute);
            this.navigateToSearchEmployeeCommand = new RelayCommand(NavigateToSearchEmployeeExecute);
            this.sendNewVisitCommand = new RelayCommand(SendNewVisitExecute);
            this.confirmNavigateBackCommand = new RelayCommand(ConfirmNavigateBackExecute);
            this.remainAtPageCommand = new RelayCommand(RemainAtPageExecute);
        }

        private void ConfirmNavigateBackExecute()
        {
            IsConfirmationPopupOpened = false;
            IsAppBarOpened = false;
            this.navService.GoBack();
        }

        private void RemainAtPageExecute()
        {
            IsAppBarOpened = true;
            IsConfirmationPopupOpened = false;            
        }

        private void NavigateBackExecute()
        {
            IsConfirmationPopupOpened = true;
        }

        private void NavigateToSearchVisitorExecute()
        {
            this.navService.NavigateToSearchVisitorPage();
        }

        private void NavigateToSearchEmployeeExecute()
        {
            this.navService.NavigateToSearchEmployeePage();
        }

        private async void SendNewVisitExecute()
        {
            var loader = new ResourceLoader();

            // Checks if Employee or Visitor are emtpty
            if ((Visitor.VisitorId == 0) && (Employee.EmployeeId == 0))
                messageService.ShowMessage(loader.GetString("RequiredVisitorEmployee"), loader.GetString("Incomplete_Form"));

            else if ((Visitor.VisitorId == 0) && (Employee.EmployeeId != 0))
                messageService.ShowMessage(loader.GetString("RequiredVisitor"), loader.GetString("Incomplete_Form"));

            else if ((Visitor.VisitorId != 0) && (Employee.EmployeeId == 0))
                messageService.ShowMessage(loader.GetString("RequiredEmployee"), loader.GetString("Incomplete_Form"));

            // Checks that visit datetime is correct.
            else if (GetDateTime() < DateTime.Now.AddMinutes(-20))
                messageService.ShowMessage(loader.GetString("TimeBad"), loader.GetString("IncorrectTime"));

            else
            {
                try
                {
                    AreEnabledButtoms = false;
                    this.visit.VisitorId = this.visitor.VisitorId;
                    this.visit.EmployeeId = this.employee.EmployeeId;
                    this.visit.VisitDateTime = GetDateTime().ToUniversalTime();
                    if (!this.visit.HasCar)
                        this.visit.Plate = null;

                    await this.clientService.VisitService.Add(visit);
                    this.IsCreatingVisit = false;
                    this.navService.NavigateToMainPage();
                }
                catch (Exception)
                {
                    this.messageService.ShowMessage(loader.GetString("ErrorOcurred"), loader.GetString("Error"));
                    AreEnabledButtoms = true;
                }
            }
        }

        private DateTime GetDateTime()
        {
            DateTimeOffset combinedValue = new DateTimeOffset((this.visitDate.Date) + this.visitTime);
            return combinedValue.DateTime;
        }

        private void InitializeDateTime()
        {
            VisitDate = DateTimeOffset.Now;
            MinYear = DateTimeOffset.Now;
            MaxYear = DateTimeOffset.Now.AddYears(YEAR_INTERVAL);
            VisitTime = DateTimeOffset.Now.TimeOfDay;
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            this.visit = null;
            this.visitor = null;
            this.employee = null;
            this.IsCreatingVisit = false;
        }
    }
}
