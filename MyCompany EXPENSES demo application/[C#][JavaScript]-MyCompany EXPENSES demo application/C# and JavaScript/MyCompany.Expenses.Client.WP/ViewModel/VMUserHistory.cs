namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Phone.Controls;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.Services.Tile;
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
    /// History page viewmodel
    /// </summary>
    public class VMUserHistory : VMExpenseList
    {
        private readonly ITileService tileService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService">navigation service</param>
        /// <param name="myCompanyClient">myCompany client</param>
        /// <param name="tileService">tile service</param>
        public VMUserHistory(INavigationService navigationService, IMyCompanyClient myCompanyClient, ITileService tileService)
            : base(navigationService, myCompanyClient)
        {
            this.tileService = tileService;
            this.PropertyChanged += VMUserHistory_PropertyChanged;
        }

        void VMUserHistory_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Expenses")
            {
                this.PropertyChanged -= VMUserHistory_PropertyChanged;
                var firstExpense = this.Expenses.FirstOrDefault();

                if (firstExpense != null)
                    this.tileService.UpdateMainTile(string.Empty, string.Format("{0} {1}$", firstExpense.Name, firstExpense.Amount), firstExpense.ExpenseType.ToString(), 0);
            }
        }

        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="itemsToLoad"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        protected override Task<IList<Expense>> GetData(int itemsToLoad, int pageNumber)
        {
            return this.myCompanyClient.ExpenseService.GetUserExpenses((int)ExpenseStatus.All, itemsToLoad, pageNumber);
        }
    }
}
