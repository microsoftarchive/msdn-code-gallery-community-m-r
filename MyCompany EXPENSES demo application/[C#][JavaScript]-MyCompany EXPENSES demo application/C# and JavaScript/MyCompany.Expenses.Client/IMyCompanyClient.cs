
namespace MyCompany.Expenses.Client
{
    /// <summary>
    /// Interfaz to access to the WebApi controllers 
    /// </summary>
    public interface IMyCompanyClient
    {
        /// <summary>
        /// Interface to access to the Employee Service 
        /// </summary>
        IEmployeeService EmployeeService { get; }

        /// <summary>
        /// Interface to access to the Expense Service 
        /// </summary>
        IExpenseService ExpenseService { get; }

        /// <summary>
        /// Interface to access to the ExpenseTravel Service 
        /// </summary>
        IExpenseTravelService ExpenseTravelService { get; }

        /// <summary>
        /// Interface to access to the security services
        /// </summary>
        ISecurityService SecurityService { get; }

        /// <summary>
        /// Interface to access the Notification Service
        /// </summary>
        INotificationsService NotificationsService { get; }
    }
}
