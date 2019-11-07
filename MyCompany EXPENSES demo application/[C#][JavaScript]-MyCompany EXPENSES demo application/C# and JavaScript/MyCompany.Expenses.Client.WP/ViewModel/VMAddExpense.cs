namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
using MyCompany.Expenses.Client.WP.Messages;
using MyCompany.Expenses.Client.WP.Model;
using MyCompany.Expenses.Client.WP.Services.Navigation;
using MyCompany.Expenses.Client.WP.Services.Photo;
using MyCompany.Expenses.Client.WP.Settings;
using MyCompany.Expenses.Client.WP.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

    /// <summary>
    /// Add expense viewmodel
    /// </summary>
    public class VMAddExpense : VMBase
    {
        private readonly IMyCompanyClient myCompanyClient;
        private readonly INavigationService navService;
        private readonly IPhotoService photoService;

        private RelayCommand takePhotoCommand;
        private RelayCommand saveExpenseCommand;
        private RelayCommand trackRouteCommand;

        private ExpenseType type;
        private Expense expense = new Expense();
        private byte[] expensePhoto;
        private string expenseAmount;
        private AddExpenseValidation validations = new AddExpenseValidation();

        /// <summary>
        /// Constructor
        /// </summary>
        public VMAddExpense(IMyCompanyClient myCompanyClient, INavigationService navService, IPhotoService photoService)
        {
            this.myCompanyClient = myCompanyClient;
            this.navService = navService;
            this.photoService = photoService;

            InitializeCommand();
        }

        /// <summary>
        /// Type of the current expense
        /// </summary>
        public ExpenseType Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                this.expense.ExpenseType = this.type;
                this.RaisePropertyChanged();
            }
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
            }
        }

        /// <summary>
        /// selected expense photo.
        /// </summary>
        public byte[] ExpensePhoto
        {
            get { return this.expensePhoto; }
            set
            {
                this.expensePhoto = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Expense Amount
        /// </summary>
        public string ExpenseAmount
        {
            get { return this.expenseAmount; }
            set
            {
                this.expenseAmount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Contains required fields validations.
        /// </summary>
        public AddExpenseValidation Validations
        {
            get { return this.validations; }
            set
            {
                this.validations = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Save expense
        /// </summary>
        public ICommand SaveExpenseCommand
        {
            get { return this.saveExpenseCommand; }
        }

        /// <summary>
        /// take a photo.
        /// </summary>
        public ICommand TakePhotoCommand
        {
            get { return this.takePhotoCommand; }
        }

        /// <summary>
        /// Track route if expense type is travel.
        /// </summary>
        public ICommand TrackRouteCommand
        {
            get { return this.trackRouteCommand; }
        }

        /// <summary>
        /// Initialize data when open
        /// </summary>
        public void InitializeData()
        {
            this.expense = new Expense();
            this.expenseAmount = string.Empty;
            this.expensePhoto = null;

            this.validations.DescriptionFailed = false;
            this.validations.AmountFailed = false;
            this.validations.TitleFailed = false;
        }

        /// <summary>
        /// When navigating back from adding a track, refresh the bindings so the amount is updated.
        /// </summary>
        public void RefreshData()
        {
            RaisePropertyChanged(() => Expense);
            ExpenseAmount = Convert.ToString(Expense.Amount);
        }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitializeCommand()
        {
            this.saveExpenseCommand = new RelayCommand(this.SaveExpenseExecute, this.CanSaveExpenses);
            this.takePhotoCommand = new RelayCommand(this.TakePhotoExecute);
            this.trackRouteCommand = new RelayCommand(this.TrackRouteExecute);
        }

        /// <summary>
        /// Save new expense and return to mainpage.
        /// </summary>
        private async void SaveExpenseExecute()
        {
            if (string.IsNullOrWhiteSpace(this.expense.Name))
                this.validations.TitleFailed = true;

            if (string.IsNullOrWhiteSpace(this.expense.Description))
                this.validations.DescriptionFailed = true;

            double amount;
            this.validations.AmountFailed = !double.TryParse(this.expenseAmount, out amount);

            if (this.CanSaveExpenses())
            {
                this.IsBusy = true;
                saveExpenseCommand.RaiseCanExecuteChanged();

                this.expense.ExpenseType = this.type;
                this.expense.EmployeeId = AppSettings.EmployeeInformation.EmployeeId;
                this.expense.CreationDate = DateTime.UtcNow;
                this.expense.LastModifiedDate = DateTime.UtcNow;
                this.expense.Amount = amount;

                if (this.ExpensePhoto != null)
                    this.expense.Picture = this.ExpensePhoto;
                this.expense.Status = ExpenseStatus.Pending;
                await this.myCompanyClient.ExpenseService.Add(Expense);

                MessengerInstance.Send<ReloadHistoryMessage>(new ReloadHistoryMessage());
                this.IsBusy = false;
                this.navService.NavigateBack();
            }
        }

        /// <summary>
        /// check if we can execute the command
        /// </summary>
        /// <returns></returns>
        private bool CanSaveExpenses()
        {
            return !IsBusy && (!this.validations.TitleFailed && !this.validations.AmountFailed && !this.validations.DescriptionFailed);
        }

        /// <summary>
        /// take a photo from the gallery of the camera.
        /// </summary>
        private async void TakePhotoExecute()
        {
            var photo = await this.photoService.GetPhotoAsync();
            if (photo != null)
                ExpensePhoto = photo;
        }

        /// <summary>
        /// navigate to track route page.
        /// </summary>
        private void TrackRouteExecute()
        {
            this.navService.NavigateToTrackRoute();
        }
    }
}
