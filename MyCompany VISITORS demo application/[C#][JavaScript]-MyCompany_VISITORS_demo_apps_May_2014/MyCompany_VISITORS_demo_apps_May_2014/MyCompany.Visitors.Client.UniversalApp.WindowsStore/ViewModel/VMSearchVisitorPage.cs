namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.SampleData;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using Services.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel.Resources;
    using Windows.ApplicationModel.Search;

    /// <summary>
    /// Search Visitor Page ViewModel.
    /// </summary>
    public class VMSearchVisitorPage : VMBase
    {
        private const int VISITORS_PAGEZERO = 0;
        private const int VISITORS_TO_RETRIEVE = 30;

        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;
        private readonly ISampleDataService sampleDataService;

        private RelayCommand navigateBackCommand;
        private RelayCommand<int> navigateToNewVisitCommand;
        private RelayCommand navigateToNewVisitorCommand;

        private ObservableCollection<Visitor> visitors;
        private string textToSearch = string.Empty;

        private CancellationTokenSource searchCancellationTokenSource;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMSearchVisitorPage(INavigationService navService, IMyCompanyClient clientService,
                                   IMessageService messageService, ISampleDataService sampleDataService)
        {
            this.navService = navService;
            this.clientService = clientService;
            this.messageService = messageService;
            this.sampleDataService = sampleDataService;
            InitializeCommands();
            if (base.IsInDesignMode)
            {
                InitializeMockedData();
            }
        }

        /// <summary>
        /// Visitors List.
        /// </summary>
        public ObservableCollection<Visitor> VisitorsList
        {
            get
            {
                return this.visitors;
            }
            set
            {
                this.visitors = value;
                base.RaisePropertyChanged(() => VisitorsList);
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
        /// Navigate to New Visitor Command.
        /// </summary>
        public ICommand NavigateToNewVisitorCommand
        {
            get { return this.navigateToNewVisitorCommand; }
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
            await SearchVisitor(TextToSearch);
        }

        private void InitializeMockedData()
        {
            var visitorsMocked = this.sampleDataService.GetVisitors();
            VisitorsList = new ObservableCollection<Visitor>(visitorsMocked);
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
                    await SearchVisitor(TextToSearch);
                }
            }, ct, TaskCreationOptions.None, uiScheduler);
        }

        private async Task SearchVisitor(string filter)
        {
            try
            {
                IsBusy = true;
                VisitorsList = new ObservableCollection<Visitor>();
                IList<Visitor> results = await this.clientService.VisitorService.GetVisitors(filter, PictureType.Small, VISITORS_TO_RETRIEVE, VISITORS_PAGEZERO);
                if (results != null)
                {
                    VisitorsList = new ObservableCollection<Visitor>(results);
                }
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
            this.navigateToNewVisitorCommand = new RelayCommand(NavigateToNewVisitorExecute);
        }

        private void NavigateBackExecute()
        {
            this.navService.GoBack();
        }

        private void NavigateToNewVisitExecute(int visitorId)
        {
            this.navService.NavigateToNewVisitPageFromVisitorSearch(visitorId);
        }

        private void NavigateToNewVisitorExecute()
        {
            this.navService.NavigateToNewVisitorPage();
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            searchCancellationTokenSource.Dispose();
            this.visitors = null;
        }
    }
}
