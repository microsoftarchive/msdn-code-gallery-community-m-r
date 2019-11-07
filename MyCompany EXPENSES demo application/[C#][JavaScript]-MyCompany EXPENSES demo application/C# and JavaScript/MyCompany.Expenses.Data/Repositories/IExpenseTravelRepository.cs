namespace MyCompany.Expenses.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Expenses.Model;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Base contract for expense travel repository
    /// </summary>
    public interface IExpenseTravelRepository : IDisposable
    {
        /// <summary>
        /// Get expenseTravel by Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        Task<ExpenseTravel> GetAsync(int expenseId);

        /// <summary>
        /// Get All expenseTravels
        /// </summary>
        /// <returns>List of expenseTravels</returns>
        Task<IEnumerable<ExpenseTravel>> GetAllAsync();

        /// <summary>
        /// Add new expenseTravel
        /// </summary>
        /// <param name="expenseTravel">expenseTravel information</param>
        /// <returns>expenseTravelId</returns>
        Task<int> AddAsync(ExpenseTravel expenseTravel);

        /// <summary>
        /// Update expenseTravel
        /// </summary>
        /// <param name="expenseTravel">expenseTravel information</param>
        Task UpdateAsync(ExpenseTravel expenseTravel);

        /// <summary>
        /// Delete expenseTravel
        /// </summary>
        /// <param name="expenseId">expenseTravel to delete</param>
        Task DeleteAsync(int expenseId);
    }
}
