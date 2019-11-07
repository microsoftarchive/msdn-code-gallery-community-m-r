namespace MyCompany.Expenses.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Expenses.Model;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Base contract for expense repository
    /// </summary>
    public interface IExpenseRepository : IDisposable
    {
        /// <summary>
        /// Get expense by Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        Task<Expense> GetAsync(int expenseId);

        /// <summary>
        /// Get expense by Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="pictureType"></param>
        /// <returns></returns>
        Task<Expense> GetAllUserInfoAsync(int expenseId, PictureType pictureType);

        /// <summary>
        /// Get Team expenses for the given status and manager
        /// </summary>
        /// <param name="managerIdentity"></param>
        /// <param name="expenseStatus"></param>
        /// <param name="pictureType"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        Task<IEnumerable<Expense>> GetTeamExpensesAsync(string managerIdentity, int expenseStatus, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get User expenses for the given status
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="expenseStatus"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        Task<IEnumerable<Expense>> GetUserExpensesAsync(string userIdentity, int expenseStatus, int pageSize, int pageCount);

        /// <summary>
        /// Get Team expenses count for the given status and manager
        /// </summary>
        /// <param name="managerIdentity"></param>
        /// <param name="expenseStatus"></param>
        /// <returns></returns>
        Task<int> GetTeamCountAsync(string managerIdentity, int expenseStatus);

        /// <summary>
        /// Get User expenses Count for the given status
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="expenseStatus"></param>
        /// <returns></returns>
        Task<int> GetUserCountAsync(string userIdentity, int expenseStatus);

        /// <summary>
        /// Get All expenses
        /// </summary>
        /// <returns>List of expenses</returns>
        Task<IEnumerable<Expense>> GetAllAsync();

        /// <summary>
        /// Add new expense
        /// </summary>
        /// <param name="expense">expense information</param>
        /// <returns>expenseId</returns>
        Task<int> AddAsync(Expense expense);

        /// <summary>
        /// Update expense
        /// </summary>
        /// <param name="expense">expense information</param>
        Task UpdateAsync(Expense expense);

        /// <summary>
        /// Delete expense
        /// </summary>
        /// <param name="expenseId">expense to delete</param>
        Task DeleteAsync(int expenseId);

        /// <summary>
        /// Get All expenses for the manager that makes the request group by team member
        /// </summary>
        /// <param name="managerIdentity">Manager Identity</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of expenses group by team member</returns>
        Task<IEnumerable<ExpenseGrouped>> GetTeamExpensesByMemberAsync(string managerIdentity, PictureType pictureType);

        /// <summary>
        /// Get All expenses count for the manager that makes the request group by team member
        /// </summary>
        /// <param name="managerIdentity">Manager Identity</param>
        /// <returns>List of expenses group by team member</returns>
        Task<int> GetTeamExpensesByMemberCountAsync(string managerIdentity);

        /// <summary>
        /// Get team expenses for the manager that makes the request group by month
        /// </summary>
        /// <param name="managerIdentity">Manager Identity</param>
        /// <param name="expenseType">Flag to filter by expenseType</param>
        /// <returns>List of expenses group by month</returns>
        IEnumerable<ExpenseMonth> GetTeamExpensesByMonth(string managerIdentity, int expenseType);

        /// <summary>
        /// Get team member expenses for the manager that makes the request group by month
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="expenseType">Flag to filter by expenseType</param>
        /// <returns>List of expenses group by month</returns>
        IEnumerable<ExpenseMonth> GetTeamMemberExpensesByMonth(int employeeId, int expenseType);

        /// <summary>
        /// Get Team Member expense summary
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="month">Month to filter</param>
        /// <param name="year">Year to filter</param>
        /// <returns>Team Member Summary Information</returns>
        Task<IEnumerable<TeamMemberSummary>> GetTeamMemberSummaryExpensesAsync(int employeeId, int? month, int year);
    }
}
