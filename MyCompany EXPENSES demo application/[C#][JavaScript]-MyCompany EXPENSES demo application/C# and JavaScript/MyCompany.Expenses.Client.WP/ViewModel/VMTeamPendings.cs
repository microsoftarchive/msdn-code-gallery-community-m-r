namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Phone.Controls;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Team pendings viewmodel
    /// </summary>
    public class VMTeamPendings : VMExpenseList
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService">navigation service</param>
        /// <param name="myCompanyClient">myCompany client</param>
        public VMTeamPendings(INavigationService navigationService, IMyCompanyClient myCompanyClient)
            : base(navigationService, myCompanyClient)
        {
        }

        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="itemsToLoad"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        protected override Task<IList<Expense>> GetData(int itemsToLoad, int pageNumber)
        {
            return this.myCompanyClient.ExpenseService.GetTeamExpenses((int)ExpenseStatus.Pending, PictureType.Small, itemsToLoad, pageNumber);
        }
    }
}
