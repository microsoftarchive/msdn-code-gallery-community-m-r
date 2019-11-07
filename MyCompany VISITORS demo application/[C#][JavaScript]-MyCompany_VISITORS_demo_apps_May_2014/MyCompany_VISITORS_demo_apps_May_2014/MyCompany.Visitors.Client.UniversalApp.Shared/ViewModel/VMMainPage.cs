namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using MyCompany.Visitors.Client.UniversalApp.Model.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.Services.SampleData;
    using MyCompany.Visitors.Client.UniversalApp.Services.Dispatcher;
    using MyCompany.Visitors.Client.UniversalApp.Services.Tiles;
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Microsoft.AspNet.SignalR.Client;

    using Windows.ApplicationModel.Resources;
    using Windows.UI.Core;


    /// <summary>
    /// Main page ViewModel.
    /// </summary>
    public class VMMainPage : VMBase
    {
        private const int ITEMS_TO_RETRIEVE_PAGEZERO = 0;
        private const int ITEMS_TO_RETRIEVE_TODAY = 3;
        private const int ITEMS_TO_RETRIEVE_OTHER = 12;
        private const int ITEMS_TO_RETRIEVE_LIVETILE = 5;
        private const int TODAY_GROUP_ID = 1;
        private const int OTHER_GROUP_ID = 2;

        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;
        private readonly ITilesService tilesService;
        private readonly ISampleDataService sampleDataService;
        private readonly IDispatcherService dispatcherService;

        private ObservableCollection<VisitsGroup> visits;
        private VisitItem nextVisit;
        private ObservableCollection<VisitItem> todayVisits;
        private ObservableCollection<VisitItem> othersVisits;
        private int todayVisitsCount;
        private int otherVisitsCount;
        private string todayVisitsHeader;
        private string otherVisitsHeader;

        private bool showTodayVisits = true;
        private bool showOtherVisits = true;
        private bool showSplashLoading = false;
        int numberOfLoads = 0;
        private ResourceLoader loader;

        private RelayCommand<int> navigateToVisitsListingCommand;
        private RelayCommand<int> navigateToVisitDetailsCommand;

        private RelayCommand refreshCommand;
        private HubConnection hubConnection;

#if WINDOWS_APP
        private RelayCommand navigateToNewVisitCommand;
#endif

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMMainPage(
            INavigationService navService,
            IMyCompanyClient clientService,
            IMessageService messageService,
            ITilesService tilesService,
            ISampleDataService sampleDataService,
            IDispatcherService dispatcherService)
        {
            this.navService = navService;
            this.clientService = clientService;
            this.messageService = messageService;
            this.tilesService = tilesService;
            this.sampleDataService = sampleDataService;
            this.dispatcherService = dispatcherService;

            loader = ResourceLoader.GetForCurrentView();
            InitializeCommands();


            if (base.IsInDesignMode)
            {
                InitializeMockedData();
            }

            if (!base.IsInDesignMode)
            {
                InitializeNotifications();
            }
        }

        public bool ShowSplashLoading
        {
            get { return showSplashLoading; }
            set
            {
                showSplashLoading = value;
                this.RaisePropertyChanged(() => ShowSplashLoading);
            }
        }

        /// <summary>
        /// Visits List.
        /// </summary>
        public ObservableCollection<VisitsGroup> VisitsList
        {
            get { return this.visits; }
            set
            {
                this.visits = value;
                RaisePropertyChanged(() => VisitsList);
            }
        }

        /// <summary>
        /// Today next visit
        /// </summary>
        public VisitItem NextVisit
        {
            get { return this.nextVisit; }
            set
            {
                this.nextVisit = value;
                base.RaisePropertyChanged(() => NextVisit);
            }
        }

        /// <summary>
        /// Today visits
        /// </summary>
        public ObservableCollection<VisitItem> TodayVisits
        {
            get { return this.todayVisits; }
            set
            {
                this.todayVisits = value;
                base.RaisePropertyChanged(() => TodayVisits);
            }
        }

        /// <summary>
        /// Today visits
        /// </summary>
        public ObservableCollection<VisitItem> OtherVisits
        {
            get { return this.othersVisits; }
            set
            {
                this.othersVisits = value;
                base.RaisePropertyChanged(() => OtherVisits);
            }
        }

        /// <summary>
        /// This property is true if there are no visits the other days, false otherwise.
        /// </summary>
        public bool ShowOtherVisits
        {
            get { return this.showOtherVisits; }
            set
            {
                this.showOtherVisits = value;
                base.RaisePropertyChanged(() => ShowOtherVisits);
            }
        }

        /// <summary>
        /// This property is true if there are no visits today, false otherwise.
        /// </summary>
        public bool ShowTodayVisits
        {
            get { return this.showTodayVisits; }
            set
            {
                this.showTodayVisits = value;
                base.RaisePropertyChanged(() => ShowTodayVisits);
            }
        }

        /// <summary>
        /// This property is true if there isn't any visit to show.
        /// </summary>
        public bool ShowNextVisit
        {
            get { return !(!this.showTodayVisits && !this.showOtherVisits); }
        }

        /// <summary>
        /// Today visits count.
        /// </summary>
        public int TodayVisitsCount
        {
            get
            {
                return this.todayVisitsCount;
            }
            set
            {
                this.todayVisitsCount = value;
                base.RaisePropertyChanged(() => TodayVisitsCount);
                base.RaisePropertyChanged(() => TodayVisitsHeader);
            }
        }

        /// <summary>
        /// Other visits count
        /// </summary>
        public int OtherVisitsCount
        {
            get
            {
                return this.otherVisitsCount;
            }
            set
            {
                this.otherVisitsCount = value;
                base.RaisePropertyChanged(() => OtherVisitsCount);
                base.RaisePropertyChanged(() => OtherVisitsHeader);
            }
        }

        /// <summary>
        /// Hub today visits header
        /// </summary>
        public string TodayVisitsHeader
        {
            get
            {

                this.todayVisitsHeader = string.Format("Today visits ({0})", this.TodayVisitsCount);
                return this.todayVisitsHeader;
            }
        }

        /// <summary>
        /// Hub other visits header
        /// </summary>
        public string OtherVisitsHeader
        {
            get
            {

                this.otherVisitsHeader = string.Format(loader.GetString("Visits_OtherVisits"), this.OtherVisitsCount);
                return this.otherVisitsHeader;
            }
        }

        /// <summary>
        /// Hub next visit header
        /// </summary>
        public string NextVisitHeader
        {
            get
            {

                return loader.GetString("Visits_Next");
            }
        }

        /// <summary>
        /// Navigate to Visit Details Command.
        /// </summary>
        public ICommand NavigateToVisitDetailsCommand
        {
            get { return this.navigateToVisitDetailsCommand; }
        }

        /// <summary>
        /// Navigate to Visits Listing Command.
        /// </summary>
        public ICommand NavigateToVisitsListingCommand
        {
            get { return this.navigateToVisitsListingCommand; }
        }

        public ICommand RefreshCommand
        {
            get { return this.refreshCommand; }
        }

#if WINDOWS_APP
        /// <summary>
        /// Navigate to new Visit Command.
        /// </summary>
        public ICommand NavigateToNewVisitCommand
        {
            get { return this.navigateToNewVisitCommand; }
        }
#endif

        /// <summary>
        /// Initialize Data method.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeData()
        {
            try
            {
                if (numberOfLoads == 0)
                    ShowSplashLoading = true;
                else
                    IsBusy = true;

                VisitsList = new ObservableCollection<VisitsGroup>();
                TodayVisits = new ObservableCollection<VisitItem>();
                OtherVisits = new ObservableCollection<VisitItem>();

                // Today visits.
                int todayItems = await this.GetTodayVisits();
                // Other visits.
                int otherItems = await this.GetOtherVisits();

                // Update next visit visibility.
                RaisePropertyChanged(() => ShowNextVisit);

                // Update the Tiles.
                await UpdateLiveTileData();
                CreateVisitsGroup(todayItems, otherItems);
                numberOfLoads++;
            }
            catch (Exception)
            {
                this.navService.NavigateToAuthentication();
            }
            finally
            {
                IsBusy = false;
                ShowSplashLoading = false;
            }
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            this.MessengerInstance.Unregister(this);
            this.hubConnection.Dispose();
        }

        private void InitializeMockedData()
        {
            NextVisit = new VisitItem(this.sampleDataService.GetVisits(2).First(), TODAY_GROUP_ID);
            TodayVisits = new ObservableCollection<VisitItem>(this.sampleDataService.GetVisits(3).Select(v => new VisitItem(v, TODAY_GROUP_ID)));
            OtherVisits = new ObservableCollection<VisitItem>(this.sampleDataService.GetVisits(9).Select(v => new VisitItem(v, OTHER_GROUP_ID)));

            this.TodayVisitsCount = 5;
            this.OtherVisitsCount = 100;
        }

        private void VisitorPictureChanged(VisitorPicturesChanged visitorPictureChanged)
        {
            OnVisitorPicturesChanged(visitorPictureChanged.Content);
        }

        private void CreateVisitsGroup(int todayItems, int otherItems)
        {
            if (todayItems > 0)
            {
                VisitsList.Add(new VisitsGroup
                {
                    GroupId = TODAY_GROUP_ID,
                    GroupName = string.Format(loader.GetString("Visits_Today"), todayItems),
                    Items = new ObservableCollection<VisitItem>(TodayVisits)
                });
            }
            if (otherItems > 0)
            {
                VisitsList.Add(new VisitsGroup
                {
                    GroupId = OTHER_GROUP_ID,
                    GroupName = string.Format(loader.GetString("Visits_OtherVisits"), otherItems),
                    Items = new ObservableCollection<VisitItem>(OtherVisits)
                });
            }

            RaisePropertyChanged(() => VisitsList);
        }

        /// <summary>
        /// Get the three next today's visits.
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetTodayVisits()
        {
            var fromDate = DateTime.Now.AddHours(-1).ToUniversalTime();
            var toDate = DateTime.Today.AddDays(1).ToUniversalTime();

            IList<Visit> todayResults = await this.clientService.VisitService.GetVisits(string.Empty, PictureType.All, ITEMS_TO_RETRIEVE_TODAY, ITEMS_TO_RETRIEVE_PAGEZERO, fromDate, toDate);
            int todayItems = await this.clientService.VisitService.GetCount(string.Empty, fromDate, toDate);
            var nextVisit = todayResults.FirstOrDefault(v => v.VisitDateTime >= fromDate);

            NextVisit = nextVisit == null ? null : new VisitItem(nextVisit, TODAY_GROUP_ID);
            ShowTodayVisits = todayResults.Any();
            TodayVisits = new ObservableCollection<VisitItem>(todayResults.Select(v => new VisitItem(v, TODAY_GROUP_ID)));
            TodayVisitsCount = todayItems;

            return todayItems;
        }

        /// <summary>
        /// This method get the 9 first visits others than today visits.
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetOtherVisits()
        {
            IList<Visit> otherResults = await this.clientService.VisitService.GetVisitsFromDate(string.Empty, PictureType.Small, ITEMS_TO_RETRIEVE_OTHER, ITEMS_TO_RETRIEVE_PAGEZERO, DateTime.Today.AddDays(1).ToUniversalTime());
            int otherItems = await this.clientService.VisitService.GetCountFromDate(string.Empty, DateTime.Today.AddDays(1).ToUniversalTime());

            ShowOtherVisits = otherResults.Any();
            OtherVisits = new ObservableCollection<VisitItem>(otherResults.Select(v => new VisitItem(v, OTHER_GROUP_ID)));

            if (otherResults.Any() && NextVisit == null)
            {
                // Set the next visit of another day.
                var id = otherResults.First().VisitId;
                var nextVisit = await this.clientService.VisitService.Get(id, PictureType.Big);
                NextVisit = new VisitItem(nextVisit, TODAY_GROUP_ID);
            }
            OtherVisitsCount = otherItems;

            return otherItems;
        }

        /// <summary>
        /// This method get updated visit data to refresh application live tile.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateLiveTileData()
        {
            if (!this.IsInDesignMode)
            {
                IList<Visit> visitsToShowInTile = await this.clientService.VisitService.GetVisitsFromDate(string.Empty, PictureType.Small, ITEMS_TO_RETRIEVE_LIVETILE, ITEMS_TO_RETRIEVE_PAGEZERO, DateTime.Now.ToUniversalTime());
                this.tilesService.UpdateMainTile(visitsToShowInTile);
            }
        }

        private void InitializeCommands()
        {
            this.navigateToVisitsListingCommand = new RelayCommand<int>(NavigateToVisitsListingExecute);
            this.refreshCommand = new RelayCommand(RefreshCommandExecute);
            this.navigateToVisitDetailsCommand = new RelayCommand<int>(NavigateToVisitDetailsExecute);
#if WINDOWS_APP

            this.navigateToNewVisitCommand = new RelayCommand(NavigateToNewVisitExecute);
#endif
        }

        private void NavigateToVisitsListingExecute(int groupId)
        {
            this.navService.NavigateToVisitsListing
                (groupId == 1 ? true : false);
        }

        private async void RefreshCommandExecute()
        {
            await InitializeData();
        }

        private void NavigateToVisitDetailsExecute(int visitId)
        {
            this.navService.NavigateToVisitDetailPage(visitId);
        }

        private void InitializeNotifications()
        {
            //messenger notifications
            this.MessengerInstance.Register<VisitorPicturesChanged>(this, VisitorPictureChanged);

            //signalr notifications
            this.hubConnection = new HubConnection(AppSettings.ApiUri.ToString(), new Dictionary<string, string>
                                   {
                                       { "isNoAuth", AppSettings.TestMode.ToString() }
                                   });

            if (!AppSettings.TestMode)
                this.hubConnection.Headers.Add("Authorization", AppSettings.SecurityToken);

            IHubProxy hubProxy = this.hubConnection.CreateHubProxy("VisitorsNotificationHub");
            hubProxy.On<Visit>("notifyVisitAdded", OnVisitAdded);
            hubConnection.Start().ContinueWith(
                task =>
                {
                    if (task.IsCompleted)
                    {
                    }
                });
        }

        private async void OnVisitAdded(Visit visit)
        {
            await dispatcherService.InvokeUI(() =>
               {
                   bool isNext = NextVisit == null || visit.VisitDateTime < NextVisit.VisitDate;

                   bool isToday = visit.VisitDateTime.ToLocalTime().Date == DateTime.Now.Date;
                   var list = isToday ? TodayVisits : OtherVisits;
                   var maxNumberOfItems = isToday ? ITEMS_TO_RETRIEVE_TODAY : ITEMS_TO_RETRIEVE_OTHER;
                   int visitGroup = isToday ? TODAY_GROUP_ID : OTHER_GROUP_ID;
                   var visitItem = new VisitItem(visit, visitGroup);

                   var previousVisit = list.LastOrDefault(v =>
                   {
                       return v.VisitDate < visitItem.VisitDate;
                   });

                   int visitIndex = previousVisit == null ? 0 : list.IndexOf(previousVisit) + 1;

                   bool hasToBeAdded = visitIndex <= list.Count;
                   bool needToRemoveLast = list.Count() == maxNumberOfItems;

                   if (isNext)
                       NextVisit = visitItem;

                   if (hasToBeAdded)
                   {
                       list.Insert(visitIndex, visitItem);
                   }

                   if (needToRemoveLast)
                   {
                       list.Remove(list.Last());
                   }

                   if (isToday)
                   {
                       ShowTodayVisits = true;
                       TodayVisitsCount++;
                   }
                   else
                   {
                       ShowOtherVisits = true;
                       OtherVisitsCount++;
                   }

                   this.RaisePropertyChanged(() => ShowNextVisit);
               });
        }

        private async void OnVisitorPicturesChanged(ICollection<VisitorPicture> visitorPictures)
        {
            await dispatcherService.InvokeUI(() =>
               {
                   int visitorId = visitorPictures.First().VisitorId;

                   IEnumerable<VisitItem> visitsWithChangedVisitor;
                   foreach (var visitGroup in this.VisitsList)
                   {
                       visitsWithChangedVisitor = visitGroup.Items.Where(v => v.VisitorId == visitorId);
                       foreach (var visit in visitsWithChangedVisitor)
                       {
                           visit.ChangePhotos(visitorPictures);
                       }
                   }
                   base.RaisePropertyChanged(() => VisitsList);

                   visitsWithChangedVisitor = this.OtherVisits.Where(v => v.VisitorId == visitorId);
                   if (visitsWithChangedVisitor.Any())
                   {

                       foreach (var visit in visitsWithChangedVisitor)
                       {
                           visit.ChangePhotos(visitorPictures);
                       }
                       base.RaisePropertyChanged(() => OtherVisits);
                   }


                   visitsWithChangedVisitor = this.TodayVisits.Where(v => v.VisitorId == visitorId);
                   if (visitsWithChangedVisitor.Any())
                   {
                       foreach (var visit in visitsWithChangedVisitor)
                       {
                           visit.ChangePhotos(visitorPictures);
                       }
                       base.RaisePropertyChanged(() => TodayVisits);
                   }

                   if (NextVisit.VisitorId == visitorId)
                   {
                       NextVisit.ChangePhotos(visitorPictures);
                       base.RaisePropertyChanged(() => NextVisit);
                   }
               });
        }

#if WINDOWS_APP
        private void NavigateToNewVisitExecute()
        {
            this.navService.NavigateToNewVisitPage();
        }
#endif

    }
}