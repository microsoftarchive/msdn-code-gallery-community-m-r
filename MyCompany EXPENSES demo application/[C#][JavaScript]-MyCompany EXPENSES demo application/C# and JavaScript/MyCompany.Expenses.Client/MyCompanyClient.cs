namespace MyCompany.Expenses.Client
{
    using System;

    /// <summary>
    /// Client entry point class
    /// </summary>
    public class MyCompanyClient : IMyCompanyClient
    {
        IEmployeeService _employeeService = null;
        IExpenseService _expenseService = null;
        IExpenseTravelService _expenseTravelService = null;
        ISecurityService _securityService = null;
        INotificationsService _notificationsService = null;

        private string _urlPrefix = string.Empty;
        private string _securityToken = string.Empty;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public MyCompanyClient(string urlPrefix, string securityToken)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            if (String.IsNullOrEmpty(securityToken))
                throw new ArgumentNullException("securityToken");

            _urlPrefix = urlPrefix;
            _securityToken = securityToken;
        }

        /// <summary>
        /// Property to access to the Employee Service client
        /// </summary>
        public IEmployeeService EmployeeService
        {
            get
            {
                if (_employeeService == null)
                    _employeeService = new EmployeeService(_urlPrefix, _securityToken);

                return _employeeService;
            }
        }

        /// <summary>
        /// Property to access to the Expense Service client
        /// </summary>
        public IExpenseService ExpenseService
        {
            get
            {
                if (_expenseService == null)
                    _expenseService = new ExpenseService(_urlPrefix, _securityToken);

                return _expenseService;
            }
        }

        /// <summary>
        /// Property to access to the Expense Travel Service client
        /// </summary>
        public IExpenseTravelService ExpenseTravelService
        {
            get
            {
                if (_expenseTravelService == null)
                    _expenseTravelService = new ExpenseTravelService(_urlPrefix, _securityToken);

                return _expenseTravelService;
            }
        }

        /// <summary>
        /// Property to access to the security service client.
        /// </summary>
        public ISecurityService SecurityService
        {
            get
            {
                if (_securityService == null)
                    _securityService = new SecurityService(_urlPrefix, _securityToken);

                return _securityService;
            }
        }

        /// <summary>
        /// Property to access to the security service client.
        /// </summary>
        public INotificationsService NotificationsService
        {
            get
            {
                if (_notificationsService == null)
                    _notificationsService = new NotificationsService(_urlPrefix, _securityToken);

                return _notificationsService;
            }
        }
    }
}
