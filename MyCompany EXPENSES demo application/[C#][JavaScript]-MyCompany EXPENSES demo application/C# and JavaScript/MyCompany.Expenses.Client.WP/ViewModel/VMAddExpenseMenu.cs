namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Add expense menu viewmodel
    /// </summary>
    public class VMAddExpenseMenu : VMBase
    {
        private readonly INavigationService navService;
        private RelayCommand<string> navigateToAddExpenseCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navService">navigation service</param>
        public VMAddExpenseMenu(INavigationService navService)
        {
            this.navService = navService;

            InitializeCommands();
        }

        /// <summary>
        /// Command to navigate to add expense page.
        /// </summary>
        public ICommand NavigateToAddExpenseCommand
        {
            get { return this.navigateToAddExpenseCommand; }
        }

        private void InitializeCommands()
        {
            this.navigateToAddExpenseCommand = new RelayCommand<string>(NavigateToAddExpense);
        }

        private void NavigateToAddExpense(string type)
        {
            int expenseType = int.Parse(type);

            this.navService.NavigateToAddExpense((ExpenseType)expenseType);
        }
    }
}
