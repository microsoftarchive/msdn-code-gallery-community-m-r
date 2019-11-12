
namespace MyCompany.Expenses.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Expenses.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
    /// </summary>
    internal class ExpenseService : BaseRequest, IExpenseService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken">Security Token</param>
        public ExpenseService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<Expense> Get(int expenseId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/{1}/{2}", _urlPrefix, expenseId, (int)pictureType);

            return await base.GetAsync<Expense>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<IList<Expense>> GetUserExpenses(int expenseStatus, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/user?expenseStatus={1}&pageSize={2}&pageCount={3}", _urlPrefix, expenseStatus, pageSize, pageCount);

            return await base.GetAsync<IList<Expense>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<IList<Expense>> GetTeamExpenses(int expenseStatus, PictureType pictureType, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/team?expenseStatus={1}&pictureType={2}&pageSize={3}&pageCount={4}", _urlPrefix, expenseStatus, (int)pictureType, pageSize, pageCount);

            return await base.GetAsync<IList<Expense>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<int> GetUserCount(int expenseStatus)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/user/{1}/count", _urlPrefix, expenseStatus);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<int> GetTeamCount(int expenseStatus)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/team/{1}/count", _urlPrefix, expenseStatus);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expense"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<int> Add(Expense expense)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses", _urlPrefix);

            return await base.PostAsync<int, Expense>(url, expense);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="status"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        public async Task UpdateStatus(int expenseId, ExpenseStatus status)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses?expenseId={1}&status={2}", _urlPrefix, expenseId, (int)status);

            await base.PutAsync(url, string.Empty);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<IList<ExpenseGrouped>> GetTeamExpensesByMember(PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/teammembers/{1}", _urlPrefix, (int)pictureType);

            return await base.GetAsync<IList<ExpenseGrouped>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<int> GetTeamExpensesByMemberCount()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/teammembers/count", _urlPrefix);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="expenseType"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<IList<ExpenseMonth>> GetTeamExpensesByMonth(int expenseType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/team/month/{1}", _urlPrefix, expenseType);

            return await base.GetAsync<IList<ExpenseMonth>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="expenseType"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<IList<ExpenseMonth>> GetTeamMemberExpensesByMonth(int employeeId, int expenseType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/teammember/{1}/month/{2}", _urlPrefix, employeeId, expenseType);

            return await base.GetAsync<IList<ExpenseMonth>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IExpenseService"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="month"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <param name="year"><see cref="MyCompany.Expenses.Client.IExpenseService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.IExpenseService"/></returns>
        public async Task<IList<TeamMemberSummary>> GetTeamMemberSummaryExpenses(int employeeId, int? month, int year)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/expenses/teammember/{1}/summary/{2}/{3}", _urlPrefix, employeeId, year, month);

            return await base.GetAsync<IList<TeamMemberSummary>>(url);
        }
    }
}
