
namespace MyCompany.Expenses.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the expense webapi Controller 
    /// </summary>
    public interface IExpenseService
    {
        /// <summary>
        /// Get expense by Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<Expense> Get(int expenseId, PictureType pictureType);

        /// <summary>
        /// Get All expenses for the user that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Number of page to get</param>
        /// <returns>List of expenses</returns>
        Task<IList<Expense>> GetUserExpenses(int expenseStatus, int pageSize, int pageCount);

        /// <summary>
        /// Get All expenses for the manager that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Number of page to get</param>
        /// <returns>List of expenses</returns>
        Task<IList<Expense>> GetTeamExpenses(int expenseStatus, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get Count for the user that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <returns>List of expenses</returns>
        Task<int> GetUserCount(int expenseStatus);

        /// <summary>
        /// Get Count for the manager that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <returns>List of expenses</returns>
        Task<int> GetTeamCount(int expenseStatus);

        /// <summary>
        /// Add new expense.
        /// </summary>
        /// <param name="expense">expense information</param>
        /// <returns>expenseId</returns>
        Task<int> Add(Expense expense);

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="expenseId">Id to update</param>
        /// <param name="status">ExpenseStatus</param>
        Task UpdateStatus(int expenseId, ExpenseStatus status);

        /// <summary>
        /// Get All expenses for the manager that makes the request group by team member
        /// </summary>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of expenses group by team member</returns>
        Task<IList<ExpenseGrouped>> GetTeamExpensesByMember(PictureType pictureType);

        /// <summary>
        /// Get All expenses count for the manager that makes the request group by team member
        /// </summary>
        /// <returns>Count</returns>
        Task<int> GetTeamExpensesByMemberCount();

        /// <summary>
        /// Get team expenses for the manager that makes the request group by month
        /// </summary>
        /// <param name="expenseType">Flag to filter by expenseType</param>
        /// <returns>List of expenses group by month</returns>
        Task<IList<ExpenseMonth>> GetTeamExpensesByMonth(int expenseType);

        /// <summary>
        /// Get team member expenses for the manager that makes the request group by month
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="expenseType">Flag to filter by expenseType</param>
        /// <returns>List of expenses group by month</returns>
        Task<IList<ExpenseMonth>> GetTeamMemberExpensesByMonth(int employeeId, int expenseType);

        /// <summary>
        /// Get Team Member expense summary
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="month">Month to filter</param>
        /// <param name="year">Year to filter</param>
        /// <returns>Team Member Summary Information</returns>
        Task<IList<TeamMemberSummary>> GetTeamMemberSummaryExpenses(int employeeId, int? month, int year);
    }
}
