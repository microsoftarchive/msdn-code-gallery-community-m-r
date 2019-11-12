namespace MyCompany.Travel.Client.Desktop.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MyCompany.Travel.Client.Desktop.Model;
    using MyCompany.Travel.Client.Desktop.Services.Navigation;
    using MyCompany.Travel.Client.Desktop.Services.SampleData;
    using MyCompany.Travel.Client.Desktop.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Linq;
    using MyCompany.Travel.Client.Desktop.Resources.Strings;

    /// <summary>
    /// Travel list ViewModel.
    /// </summary>
    public class TravelListViewModel : VMBase
    {
        private const int PAGESIZE = 10;
        private RelayCommand<TravelRequest> editTravelRequestCommand;
        private RelayCommand addTravelRequestCommand;
        private RelayCommand updateTravelRequestCommand;
        private RelayCommand<int> removeTravelRequestCommand;
        private RelayCommand<int> confirmTravelRequestCommand;
        private RelayCommand nextPageCommand;
        private RelayCommand previousPageCommand;
        private ObservableCollection<TravelRequest> travelsList;
        private ObservableCollection<PageItem> pagesList;
        private string filter;
        private CancellationTokenSource searchCancellationTokenSource;

        private readonly INavigationService navService;
        private readonly ISampleDataService sampleDataService;
        private readonly IMyCompanyClient myCompanyClient;

        private PaginationConfig pagConfig;
        private int travelsCount;

        /// <summary>
        /// Initializes a new instance of the TravelListViewModel class.
        /// </summary>
        public TravelListViewModel(INavigationService navService, ISampleDataService sampleDataService, IMyCompanyClient myCompanyClient)
        {
            this.navService = navService;
            this.sampleDataService = sampleDataService;
            this.myCompanyClient = myCompanyClient;
            SubscribeCommands();

            if (base.IsInDesignMode)
            {
                InitializeMockedData();
            }
        }

        /// <summary>
        /// Travels list.
        /// </summary>
        public ObservableCollection<TravelRequest> TravelsList
        {
            get
            {
                return this.travelsList;
            }
            set
            {
                this.travelsList = value;
                base.RaisePropertyChanged(() => TravelsList);
            }
        }

        /// <summary>
        /// Travels counted.
        /// </summary>
        public int TravelsCount
        {
            get { return this.travelsCount; }
            set
            { 
                this.travelsCount = value;
                RaisePropertyChanged(() => TravelsCount);
            }
        }

        /// <summary>
        /// PagesList
        /// </summary>
        public ObservableCollection<PageItem> PagesList
        {
            get { return this.pagesList; }
            set
            { 
                this.pagesList = value;
                RaisePropertyChanged(() => PagesList);
            }
        }

        /// <summary>
        /// Command to open travel request form
        /// </summary>
        public ICommand EditTravelRequestCommand
        {
            get { return editTravelRequestCommand; }
        }

        /// <summary>
        /// Command to confirm a travel request
        /// </summary>
        public ICommand ConfirmTravelRequestCommand
        {
            get { return this.confirmTravelRequestCommand; }
        }

        /// <summary>
        /// Command to open travel request form
        /// </summary>
        public ICommand AddTravelRequestCommand
        {
            get { return addTravelRequestCommand; }
        }

        /// <summary>
        /// Command to go to the next page.
        /// </summary>
        public ICommand NextPageCommand
        {
            get { return this.nextPageCommand; }
        }

        /// <summary>
        /// Command to go to the previous page.
        /// </summary>
        public ICommand PreviousPageCommand
        {
            get { return this.previousPageCommand; }
        }

        /// <summary>
        /// Update travel requests.
        /// </summary>
        public ICommand UpdateTravelRequests
        {
            get { return this.updateTravelRequestCommand; }
        }

        /// <summary>
        /// Remove travel requests.
        /// </summary>
        public ICommand RemoveTravelRequestCommand
        {
            get { return this.removeTravelRequestCommand; }
        }

        /// <summary>
        /// Search filter.
        /// </summary>
        public string Filter
        {
            get { return this.filter; }
            set
            { 
                this.filter = value;
                SearchTravels();
                RaisePropertyChanged(() => Filter);
            }
        }

        /// <summary>
        /// Initialize Data.
        /// </summary>
        public async Task InitializeData()
        {
            try
            {
                Messenger.Default.Send(new LoadingMessage(true));
                TravelsCount = await myCompanyClient.TravelRequestService.GetAllCount(this.filter, (int)TravelRequestStatus.Approved);
                this.pagConfig = new PaginationConfig(this.travelsCount, PAGESIZE);
                NavigateToPage(this.pagConfig.NumberOfPageSelected);  
            }
            catch (Exception)
            {
                CustomDialogMessage message = new CustomDialogMessage(() => { }, StringResources.UnexpectedError, Visibility.Collapsed);
                Messenger.Default.Send<CustomDialogMessage>(message);
            }
        }

        /// <summary>
        /// Initialize Mocked Data.
        /// </summary>
        private void InitializeMockedData()
        {
            var travels = this.sampleDataService.GetTravelRequests();
            TravelsList = new ObservableCollection<TravelRequest>(travels);

            var pages = this.sampleDataService.GetPagesList();
            PagesList = new ObservableCollection<PageItem>(pages);
        }
        private List<PageItem> CalculatePaginator()
        {
            var pages = new List<PageItem>();
            int firstPage = (int)Math.Floor((double) this.pagConfig.NumberOfPageSelected / 12) * 12;

            if (firstPage > 0)
                firstPage = firstPage - 2;

            for (int i = firstPage; i < (firstPage + Math.Min(12, this.pagConfig.NumberOfPages)) && (i < this.pagConfig.NumberOfPages); i++)
            {
                var isSelected = false;

                if(this.pagConfig.NumberOfPageSelected == i + 1)
                    isSelected = true;
                
                PageItem page = new PageItem((i + 1).ToString(), isSelected, true);
                page.PageSelected += PageSelectedChanged;
                pages.Add(page);
            }

            if (this.pagConfig.NumberOfPages > this.pagConfig.PageSize && int.Parse(pages[pages.Count - 1].Page) != this.pagConfig.NumberOfPages)
            {
                pages.Add(new PageItem("...", false, false));
                var lastPage = new PageItem(this.pagConfig.NumberOfPages.ToString(), false, true);
                pages.Add(lastPage);
                lastPage.PageSelected += PageSelectedChanged;
            }
            return pages;
        }
        private void PageSelectedChanged(object sender, EventArgs args)
        {
            this.pagConfig.PageSelected = (PageItem) sender;
            NavigateToPage(this.pagConfig.NumberOfPageSelected);
        }
        private void ResetPaginator()
        {
            if (this.pagesList != null)
            {
                foreach (var page in this.pagesList)
                {
                    page.PageSelected -= PageSelectedChanged;
                }
            }
        }
        private void SubscribeCommands()
        {
            this.editTravelRequestCommand = new RelayCommand<TravelRequest>(EditTravelRequestCommandExecute);
            this.addTravelRequestCommand = new RelayCommand(AddTravelRequestCommandExecute);
            this.confirmTravelRequestCommand = new RelayCommand<int>(ConfirmTravelRequestExecute);
            this.nextPageCommand = new RelayCommand(NextPageCommandExecute);
            this.previousPageCommand = new RelayCommand(PreviousPageCommandExecute);
            this.updateTravelRequestCommand = new RelayCommand(UpdateTravelRequestsExecute);
            this.removeTravelRequestCommand = new RelayCommand<int>(RemoveTravelRequestExecute);
        }
        private void EditTravelRequestCommandExecute(TravelRequest travelRequest)
        {
            navService.NavigateToTravelRequest(travelRequest);
        }
        private void AddTravelRequestCommandExecute()
        {
            navService.NavigateToTravelRequest(new TravelRequest() { 
                Depart = DateTime.Now.Date, 
                Return = DateTime.Now.AddDays(1).Date,
                TravelType = Client.TravelType.Roundtrip,
                Status= TravelRequestStatus.Approved 
            });
        }
        private void NextPageCommandExecute()
        {
            if (this.pagConfig.CanGoNext)
            {
                this.pagConfig.NumberOfPageSelected = this.pagConfig.NumberOfPageSelected + 1;
                NavigateToPage(this.pagConfig.NumberOfPageSelected);
            }
        }
        private void PreviousPageCommandExecute()
        {
            if (this.pagConfig.CanGoPrevious)
            {
                this.pagConfig.NumberOfPageSelected = this.pagConfig.NumberOfPageSelected - 1;
                NavigateToPage(this.pagConfig.NumberOfPageSelected);
            }
        }
        private void RemoveTravelRequestExecute(int id)
        {
            // Get the travel request to remove.
            TravelRequest travelToRemove = this.travelsList.FirstOrDefault(t => t.TravelRequestId == id);

            CustomDialogMessage message = new CustomDialogMessage(async () =>
            {
                // Remove travel request in the UI list
                RemoveFromList(travelToRemove);

                // Remove travel request in BBDD
                await this.myCompanyClient.TravelRequestService.Delete(id);
            },String.Format(Resources.Strings.StringResources.ConfirmDeleteMessage, travelToRemove.Name), Visibility.Visible);

            Messenger.Default.Send<CustomDialogMessage>(message);
        }
        private async void ConfirmTravelRequestExecute(int id)
        {
            // Get the travel request to confirm
            TravelRequest travelToConfirm = this.travelsList.FirstOrDefault(t => t.TravelRequestId == id);

            // Remove travel request in the UI list
            RemoveFromList(travelToConfirm);

            // Confirm travel request in BBDD
            await this.myCompanyClient.TravelRequestService.UpdateStatus(id, TravelRequestStatus.Completed, null);
        }
        private async void NavigateToPage(int pageToNavigate)
        {
            try
            {
                Messenger.Default.Send(new LoadingMessage(true));

                ResetPaginator();
                var travels = await myCompanyClient.TravelRequestService.GetAllTravelRequests(this.filter, (int)TravelRequestStatus.Approved, PictureType.Small, this.pagConfig.PageSize, pageToNavigate - 1);
                TravelsList = new ObservableCollection<TravelRequest>(travels);
                PagesList = new ObservableCollection<PageItem>(CalculatePaginator());
            }
            catch(Exception)
            {
                CustomDialogMessage message = new CustomDialogMessage(() => { }, StringResources.UnexpectedError, Visibility.Collapsed);
                Messenger.Default.Send<CustomDialogMessage>(message);
            }
            finally
            {
                Messenger.Default.Send(new LoadingMessage(false));
            }
        }
        private async void UpdateTravelRequestsExecute()
        {
            await InitializeData();
        }
        private void SearchTravels()
        {
            if (searchCancellationTokenSource != null)
            {
                searchCancellationTokenSource.Cancel();
            }

            if (String.IsNullOrWhiteSpace(filter) || filter.Trim().Length > 2)
            {
                searchCancellationTokenSource = new CancellationTokenSource();
                CancellationToken ct = searchCancellationTokenSource.Token;

                TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(600));
                    if (!ct.IsCancellationRequested)
                    {
                        await InitializeData();
                    }
                }, ct, TaskCreationOptions.None, uiScheduler);
            }
        }
        private void RemoveFromList(TravelRequest travelToRemove)
        {
            this.pagConfig.ItemsCounted--;
            TravelsCount--;

            this.travelsList.Remove(travelToRemove);

            if (this.travelsList.Count != 0)
            {
                RaisePropertyChanged(() => TravelsList);
            }
            else
            {
                if (this.pagConfig.CanGoNext)
                {
                    NavigateToPage(this.pagConfig.NumberOfPageSelected);
                }
                else if (this.pagConfig.CanGoPrevious)
                {
                    this.pagConfig.NumberOfPageSelected--;
                    NavigateToPage(this.pagConfig.NumberOfPageSelected);
                }
            }
        }
    }
}
