
namespace MyCompany.Expenses.Client
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Expenses.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Expenses.Client.IExpenseTravelService"/>
    /// </summary>
    internal class ExpenseTravelService : BaseRequest, IExpenseTravelService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken">Security Token</param>
        public ExpenseTravelService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseTravelService"/>
        /// </summary>
        /// <param name="expenseTravel"><see cref="MyCompany.Expenses.Client.IExpenseTravelService"/></param>
        public async Task Add(ExpenseTravel expenseTravel)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/ExpenseTravels", _urlPrefix);

            await base.PostAsync<int, ExpenseTravel>(url, expenseTravel);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseTravelService"/>
        /// </summary>
        /// <param name="expenseTravel"><see cref="MyCompany.Expenses.Client.IExpenseTravelService"/></param>
        public async Task Update(ExpenseTravel expenseTravel)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/ExpenseTravels", _urlPrefix);

            await base.PutAsync<ExpenseTravel>(url, expenseTravel);
        }
    }
}
