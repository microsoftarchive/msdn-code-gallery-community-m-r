namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.Resources;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.Services.Tile;
    using MyCompany.Expenses.Client.WP.Settings;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Expense detail viewmodel
    /// </summary>
    public class VMExpenseDetail : VMBase
    {
        private readonly IMyCompanyClient myCompanyClient;
        private readonly INavigationService navigationService;
        private readonly ITileService tileService;
        private bool isNavigatingFromTile;
        private Expense expense;

        private RelayCommand refuseExpenseCommand;
        private RelayCommand approveExpenseCommand;
        private RelayCommand<CancelEventArgs> backKeyCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="myCompanyClient"></param>
        /// <param name="tileService"></param>
        public VMExpenseDetail(INavigationService navigationService, IMyCompanyClient myCompanyClient, ITileService tileService)
        {
            this.myCompanyClient = myCompanyClient;
            this.navigationService = navigationService;
            this.tileService = tileService;

            InitializeCommand();
        }

        /// <summary>
        /// Expense
        /// </summary>
        public Expense Expense
        {
            get { return this.expense; }
            set
            {
                this.expense = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => this.ViewActions);
            }
        }

        /// <summary>
        /// View actions
        /// </summary>
        public bool ViewActions
        {
            get 
            {
                if (AppSettings.EmployeeInformation == null)
                    return false;

                return AppSettings.EmployeeInformation.IsManager && Expense.Status == ExpenseStatus.Pending; 
            }
        }

        /// <summary>
        /// Is this detail pinned to startup screen?
        /// </summary>
        public bool IsPinned
        {
            get 
            {
                if (Expense == null)
                    return false;

                return this.tileService.IsPinned(string.Format("/Views/AuthenticationPage.xaml?expenseId={0}", Expense.ExpenseId)); 
            }
        }

        /// <summary>
        /// Refuse Expense Command
        /// </summary>
        public ICommand RefuseExpenseCommand
        {
            get { return this.refuseExpenseCommand; }
        }

        /// <summary>
        /// Approve Expense Command
        /// </summary>
        public ICommand ApproveExpenseCommand
        {
            get { return this.approveExpenseCommand; }
        }

        /// <summary>
        /// THis command is invoked when the user press the back button.
        /// </summary>
        public ICommand BackKeyCommand
        {
            get { return this.backKeyCommand; }
        }

        /// <summary>
        /// Pin current expense
        /// </summary>
        public void PinExpense()
        {
            tileService.PinSecondaryTile(string.Format("/Views/AuthenticationPage.xaml?expenseId={0}", Expense.ExpenseId), 
                "/Assets/Tiles/FlipCycleTileMedium.png", 
                Expense.Name,
                string.Format("{0}{1}", Expense.Amount, AppResources.CurrencySymbol),
                Expense.Description);
        }

        /// <summary>
        /// Unpin current expense
        /// </summary>
        public void UnpinExpense()
        {
            tileService.UnpinSecondaryTile(string.Format("/Views/AuthenticationPage.xaml?expenseId={0}", Expense.ExpenseId));
        }

        /// <summary>
        /// Load Expense
        /// </summary>
        /// <param name="expenseId"></param>
        public async void LoadExpense(int expenseId)
        {
            IsBusy = true;
            isNavigatingFromTile = true;
            var expense = await this.myCompanyClient.ExpenseService.Get(expenseId, PictureType.Small);
            Expense = expense;

            if (AppSettings.EmployeeInformation == null)
            {
                await AppSettings.GetLoggedUserInformation(this.myCompanyClient);
                RaisePropertyChanged(() => ViewActions);
            }

            IsBusy = false;
        }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitializeCommand()
        {
            this.refuseExpenseCommand = new RelayCommand(this.RefuseExpense);
            this.approveExpenseCommand = new RelayCommand(this.ApproveExpense);
            this.backKeyCommand = new RelayCommand<CancelEventArgs>(this.BackKeyExecute);
        }

        private void RefuseExpense()
        {
            this.UpdateStatus(ExpenseStatus.Denied);
        }

        private void ApproveExpense()
        {
            this.UpdateStatus(ExpenseStatus.Approved);
        }

        private void UpdateStatus(ExpenseStatus expenseStatus)
        {
            myCompanyClient.ExpenseService.UpdateStatus(Expense.ExpenseId, expenseStatus);
            BackKeyExecute(new CancelEventArgs());
        }

        private void BackKeyExecute(CancelEventArgs args)
        {
            args.Cancel = true;
            if (this.isNavigatingFromTile)
            {
                this.isNavigatingFromTile = false;
                this.navigationService.NavigateToAuthPage();
            }
            else
            {
                navigationService.NavigateBack();
            }
            MessengerInstance.Send<ReloadHistoryMessage>(new ReloadHistoryMessage());
        }
    }
}
