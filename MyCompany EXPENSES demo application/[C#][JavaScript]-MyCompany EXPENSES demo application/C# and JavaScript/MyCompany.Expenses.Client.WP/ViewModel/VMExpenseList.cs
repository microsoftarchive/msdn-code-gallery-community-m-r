namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Phone.Controls;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.Settings;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Expense list viewmodel
    /// </summary>
    public abstract class VMExpenseList : VMBase
    {
        /// <summary>
        /// navigation Service
        /// </summary>
        protected readonly INavigationService navigationService;
        /// <summary>
        /// api client
        /// </summary>
        protected readonly IMyCompanyClient myCompanyClient;

        private const int itemsToLoad = 10;
        private const int loadMoreItemsWhenLeft = 3;
        private int pageNumber = 0;

        private ObservableCollection<Expense> expenses;
        private RelayCommand<ItemRealizationEventArgs> loadDataCommand;
        private RelayCommand<Expense> navigateToDetailCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService">navigation service</param>
        /// <param name="myCompanyClient">myCompany client</param>
        public VMExpenseList(INavigationService navigationService, IMyCompanyClient myCompanyClient)
        {
            this.navigationService = navigationService;
            this.myCompanyClient = myCompanyClient;

            LoadData();
            InitializeCommands();
            RegisterMessages();
        }

        /// <summary>
        /// Expenses
        /// </summary>
        public ObservableCollection<Expense> Expenses
        {
            get { return this.expenses; }
            set
            {
                this.expenses = value;
                base.RaisePropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets if has no expenses
        /// </summary>
        public bool HasNoExpenses
        {
            get { return this.Expenses == null || !this.Expenses.Any(); }
        }

        /// <summary>
        /// Command to load more data.
        /// </summary>
        public ICommand LoadDataCommand
        {
            get { return this.loadDataCommand; }
        }

        /// <summary>
        /// Command to navigate to detail expense page.
        /// </summary>
        public ICommand NavigateToDetailCommand
        {
            get { return this.navigateToDetailCommand; }
        }

        /// <summary>
        /// Method to get the list expenses data
        /// </summary>
        /// <param name="itemsToLoad"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        protected abstract Task<IList<Expense>> GetData(int itemsToLoad, int pageNumber);

        /// <summary>
        /// Register for messages sent from other viewmodels.
        /// </summary>
        private void RegisterMessages()
        {
            MessengerInstance.Register<ReloadHistoryMessage>(this, (msg) =>
            {
                pageNumber = 0;
                LoadData();
            });
        }

        private void InitializeCommands()
        {
            this.loadDataCommand = new RelayCommand<ItemRealizationEventArgs>(ItemRealized);
            this.navigateToDetailCommand = new RelayCommand<Expense>(NavigateToDetail);
        }

        private void ItemRealized(ItemRealizationEventArgs e)
        {
            if (!IsBusy && Expenses != null && Expenses.Count >= loadMoreItemsWhenLeft)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    var expense = (e.Container.Content as Expense);
                    if (expense == expenses[expenses.Count - loadMoreItemsWhenLeft])
                    {
                        LoadData();
                    }
                }
            }
        }

        private void NavigateToDetail(Expense expense)
        {
            if (expense.Employee.EmployeeId == AppSettings.EmployeeInformation.EmployeeId) 
            {
                var photo = AppSettings.EmployeeInformation.EmployeePictures.First();
                if (expense.Employee.EmployeePictures == null)
                    expense.Employee.EmployeePictures = new List<EmployeePicture>();

                expense.Employee.EmployeePictures.Add(photo);
            }

            this.navigationService.NavigateToExpenseDetail(expense);
        }

        private async void LoadData()
        {
            IsBusy = true;
            IList<Expense> list = await GetData(itemsToLoad, pageNumber);
            if (list.Any())
            {
                if (Expenses == null)
                {
                    Expenses = new ObservableCollection<Expense>(list);
                }
                else
                {
                    if (pageNumber == 0) Expenses.Clear();

                    foreach (var item in list)
                    {
                        Expenses.Add(item);
                    }
                }
                pageNumber++;
            }
            RaisePropertyChanged(() => this.HasNoExpenses);
            IsBusy = false;
        }

        /// <summary>
        /// Called when the viewmodel is disposed.
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            MessengerInstance.Unregister<ReloadHistoryMessage>(this);
        }
    }
}
