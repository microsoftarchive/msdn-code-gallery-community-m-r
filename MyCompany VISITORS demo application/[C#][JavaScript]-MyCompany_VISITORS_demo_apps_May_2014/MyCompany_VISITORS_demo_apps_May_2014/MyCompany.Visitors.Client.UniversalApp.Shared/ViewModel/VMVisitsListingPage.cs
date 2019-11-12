namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.Services.SampleData;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// Visits list viewmodel.
    /// </summary>
    public class VMVisitsListingPage : VMBase
    {
        private const int VISITS_PAGEZERO = 0;
        private const int VISITS_TO_RETRIEVE = 30;
        private const int VISITS_GROUP_ID = 1;

        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;
        private readonly ISampleDataService sampleDataService;

        private RelayCommand navigateBackCommand;
        private RelayCommand<int> navigateToVisitDetailsCommand;

        private ObservableCollection<VisitItem> visits;

#if WINDOWS_APP
        private RelayCommand navigateToNewVisitCommand;
#endif

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="navService">Navigation Service</param>
        /// <param name="clientService">Client Service</param>
        /// <param name="messageService">Message Service</param>
        /// <param name="sampleDataService">Sample data Service</param>
        public VMVisitsListingPage(INavigationService navService, IMyCompanyClient clientService,
                                   IMessageService messageService, ISampleDataService sampleDataService)
        {
            this.messageService = messageService;
            this.navService = navService;
            this.clientService = clientService;
            this.sampleDataService = sampleDataService;

            InitializeCommands();
            if (base.IsInDesignMode)
            {
                InitializeMockedData();
            }
        }

        /// <summary>
        /// Visits List.
        /// </summary>
        public ObservableCollection<VisitItem> VisitsList
        {
            get { return this.visits; }
            set
            {
                this.visits = value;
                RaisePropertyChanged(() => VisitsList);
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
        /// Navigate to Visit Details Command.
        /// </summary>
        public ICommand NavigateToVisitDetailsCommand
        {
            get { return this.navigateToVisitDetailsCommand; }
        }

#if WINDOWS_APP
        /// <summary>
        /// Navigate to New Visit Command.
        /// </summary>
        public ICommand NavigateToNewVisitCommand
        {
            get { return this.navigateToNewVisitCommand; }
        }
#endif

        /// <summary>
        /// Initialize Data Method.
        /// </summary>
        /// <param name="todayVisits"></param>
        /// <returns></returns>
        public async Task InitializeData(bool todayVisits = false)
        {
            try
            {
                IsBusy = true;

                VisitsList = new ObservableCollection<VisitItem>();
                IList<Visit> results;
                if (todayVisits)
                {
                    var fromDate = DateTime.Now.AddHours(-1).ToUniversalTime();
                    var toDate = DateTime.Today.AddDays(1).ToUniversalTime();
                    results = await this.clientService.VisitService.GetVisits(string.Empty, PictureType.Small, VISITS_TO_RETRIEVE, VISITS_PAGEZERO, fromDate, toDate);
                }
                else
                    results = await this.clientService.VisitService.GetVisitsFromDate(string.Empty, PictureType.Small, VISITS_TO_RETRIEVE, VISITS_PAGEZERO, DateTime.Today.AddDays(1).ToUniversalTime());

                if (results != null)
                {
                    VisitsList = new ObservableCollection<VisitItem>(results.Select(v => new VisitItem(v, VISITS_GROUP_ID)));
                    IsBusy = false;
                }
            }
            catch (Exception)
            {
                var loader = new ResourceLoader();
                this.messageService.ShowMessage(loader.GetString("ErrorOcurred"), loader.GetString("Error"));
            }
        }

        private void InitializeMockedData()
        {
            var visitsMocked = this.sampleDataService.GetVisits(10);
            VisitsList = new ObservableCollection<VisitItem>(visitsMocked.Select(v => new VisitItem(v, VISITS_GROUP_ID)));
        }

        private void InitializeCommands()
        {
            this.navigateBackCommand = new RelayCommand(NavigateBackExecute);
            this.navigateToVisitDetailsCommand = new RelayCommand<int>(NavigateToVisitDetailsExecute);
#if WINDOWS_APP
            this.navigateToNewVisitCommand = new RelayCommand(NavigateToNewVisitExecute);
#endif
        }

        private void NavigateBackExecute()
        {
            this.navService.GoBack();
        }

        private void NavigateToVisitDetailsExecute(int visitId)
        {
            this.navService.NavigateToVisitDetailPage(visitId);
        }

#if WINDOWS_APP
        private void NavigateToNewVisitExecute()
        {
            this.navService.NavigateToNewVisitPage();
        }
#endif

        /// <summary>
        /// Dispose method.
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            this.visits = null;
        }
    }
}
