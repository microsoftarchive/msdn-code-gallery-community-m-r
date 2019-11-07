
namespace MyCompany.Expenses.Client
{
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the expensetravel webapi Controller 
    /// </summary>
    public interface IExpenseTravelService
    {
        /// <summary>
        /// Add new expense travel
        /// </summary>
        /// <param name="expenseTravel">expense travel information</param>
        Task Add(ExpenseTravel expenseTravel);

        /// <summary>
        /// Add expense travel
        /// </summary>
        /// <param name="expenseTravel">expense travel information</param>
        Task Update(ExpenseTravel expenseTravel);
    }
}
