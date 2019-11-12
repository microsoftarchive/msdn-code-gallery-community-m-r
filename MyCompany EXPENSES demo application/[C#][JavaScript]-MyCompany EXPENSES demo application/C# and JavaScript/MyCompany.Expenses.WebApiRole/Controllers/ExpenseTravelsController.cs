namespace MyCompany.Expenses.WebApiRole.Controllers
{
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.Model;
    using System;
    using System.Web.Http;
    using System.Threading.Tasks;

     /// <summary>
    /// Expense Travel Controller
    /// </summary>
    [Authorize]
    public class ExpenseTravelsController : ApiController
    {
        private readonly IExpenseTravelRepository _expenseTravelRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expenseTravelRepository">IExpenseRepository dependency</param>
        public ExpenseTravelsController(IExpenseTravelRepository expenseTravelRepository)
        {
            if (expenseTravelRepository == null)
                throw new ArgumentNullException("expenseTravelRepository");

            _expenseTravelRepository = expenseTravelRepository;
        }

        /// <summary>
        /// Add new expense travel
        /// </summary>
        /// <param name="expenseTravel">expense travel information</param>
        [WebApiOutputCacheAttribute(false, true)]
        public async Task<int> Add(ExpenseTravel expenseTravel)
        {
            if (expenseTravel == null)
                throw new ArgumentNullException("expenseTravel");

            return await _expenseTravelRepository.AddAsync(expenseTravel);
        }

        /// <summary>
        /// Add expense travel
        /// </summary>
        /// <param name="expenseTravel">expense travel information</param>
        [HttpPut]
        [WebApiOutputCacheAttribute(false, true)]
        public async Task Update(ExpenseTravel expenseTravel)
        {
            if (expenseTravel == null)
                throw new ArgumentNullException("expenseTravel");

            await _expenseTravelRepository.UpdateAsync((ExpenseTravel)expenseTravel);
        }
    }
}