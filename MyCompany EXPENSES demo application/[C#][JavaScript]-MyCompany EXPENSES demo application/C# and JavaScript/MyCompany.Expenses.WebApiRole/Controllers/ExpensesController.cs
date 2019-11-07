namespace MyCompany.Expenses.WebApiRole.Controllers
{
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.WebApiRole;
    using MyCompany.Expenses.WebApiRole.Services;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Expenses Controller
    /// </summary>   
    [RoutePrefix("api/expenses")]
    [Authorize]
    public class ExpensesController : ApiController
    {
        private readonly IExpenseRepository _expenseRepository = null;
        private readonly ISecurityHelper _securityHelper = null;
        private readonly INotificationChannelRepository _notificationChannelRepository = null;
        private readonly INotificationService _notificationService = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expenseRepository">IExpenseRepository dependency</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        /// <param name="notificationChannelRepository">INotificationChannelRepository dependency.</param>
        /// <param name="notificationService">notificationService dependency</param>
        public ExpensesController(IExpenseRepository expenseRepository,
                                  ISecurityHelper securityHelper,
                                  INotificationChannelRepository notificationChannelRepository,
                                  INotificationService notificationService)
        {
            if (expenseRepository == null)
                throw new ArgumentNullException("expenseRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            if (notificationService == null)
                throw new ArgumentNullException("notificationService");

            if (notificationChannelRepository == null)
                throw new ArgumentNullException("notificationChannelRepository");

            _expenseRepository = expenseRepository;
            _securityHelper = securityHelper;
            _notificationChannelRepository = notificationChannelRepository;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get expense by Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute]
        [Route("{expenseId:int:min(1)}/{pictureType:int:range(1,2)}")]
        public async Task<Expense> Get(int expenseId, PictureType pictureType = PictureType.Small)
        {
            return await _expenseRepository.GetAllUserInfoAsync(expenseId, pictureType);
        }

        /// <summary>
        /// Get All expenses for the user that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Number of page to get</param>
        /// <returns>List of expenses</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("user")]
        public async Task<IEnumerable<Expense>> GetUserExpenses(int expenseStatus, int pageSize, int pageCount)
        {
            var identity = _securityHelper.GetUser();
            return await _expenseRepository.GetUserExpensesAsync(identity, expenseStatus, pageSize, pageCount);
        }

        /// <summary>
        /// Get All expenses for the manager that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Number of page to get</param>
        /// <returns>List of expenses</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team")]
        public async Task<IEnumerable<Expense>> GetTeamExpenses(int expenseStatus, PictureType pictureType, int pageSize, int pageCount)
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _expenseRepository.GetTeamExpensesAsync(managerIdentity, expenseStatus, pictureType, pageSize, pageCount);
        }

        /// <summary>
        /// Get Count for the user that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <returns>List of expenses</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("user/{expenseStatus:int:min(0)}/count")]
        public async Task<int> GetUserCount(int expenseStatus)
        {
            var identity = _securityHelper.GetUser();
            return await _expenseRepository.GetUserCountAsync(identity, expenseStatus);
        }

        /// <summary>
        /// Get Count for the manager that makes the request
        /// </summary>
        /// <param name="expenseStatus">Flags to filter by status</param>
        /// <returns>List of expenses</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team/{expenseStatus:int:min(0)}/count")]
        public async Task<int> GetTeamCount(int expenseStatus)
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _expenseRepository.GetTeamCountAsync(managerIdentity, expenseStatus);
        }

        /// <summary>
        /// Add new expense.
        /// </summary>
        /// <param name="expense">expense information</param>
        /// <returns>expenseId</returns>
        [WebApiOutputCacheAttribute(false, true)]
        public async Task<int> Add(Expense expense)
        {
            if (expense == null)
                throw new ArgumentNullException("expense");

            var expenseId = await _expenseRepository.AddAsync(expense);
            var createdExpense = await _expenseRepository.GetAsync(expenseId);

            //notify to managers
            var managersChannels = await _notificationChannelRepository.GetManagersChannelsAsync();

            foreach (var notificationChannel in managersChannels)
            {
                _notificationService.NewExpenseAdded(notificationChannel, createdExpense);
            }

            return expenseId;
        }

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="expenseId">Id to update</param>
        /// <param name="status">ExpenseStatus</param>
        [HttpPut]
        [WebApiOutputCacheAttribute(false, true)]
        public async Task UpdateStatus(int expenseId, ExpenseStatus status)
        {
            var expense = await _expenseRepository.GetAsync(expenseId);
            if (expense != null)
            {
                expense.Status = status;
                await _expenseRepository.UpdateAsync(expense);
            }
        }

        /// <summary>
        /// Get All expenses for the manager that makes the request group by team member
        /// </summary>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of expenses group by team member</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("teammembers/{pictureType:int:range(1,2)}")]
        public async Task<IEnumerable<ExpenseGrouped>> GetTeamExpensesByMember(PictureType pictureType = PictureType.Small)
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _expenseRepository.GetTeamExpensesByMemberAsync(managerIdentity, pictureType);
        }

        /// <summary>
        /// Get All expenses count for the manager that makes the request group by team member
        /// </summary>
        /// <returns>Count</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("teammembers/count")]
        public async Task<int> GetTeamExpensesByMemberCount()
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _expenseRepository.GetTeamExpensesByMemberCountAsync(managerIdentity);
        }

        /// <summary>
        /// Get team expenses for the manager that makes the request group by month
        /// </summary>
        /// <param name="expenseType">Flag to filter by expenseType</param>
        /// <returns>List of expenses group by month</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team/month/{expenseType:int:min(0)}")]
        public IEnumerable<ExpenseMonth> GetTeamExpensesByMonth(int expenseType)
        {
            var managerIdentity = _securityHelper.GetUser();
            return _expenseRepository.GetTeamExpensesByMonth(managerIdentity, expenseType);
        }

        /// <summary>
        /// Get team member expenses for the manager that makes the request group by month
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="expenseType">Flag to filter by expenseType</param>
        /// <returns>List of expenses group by month</returns>
        [WebApiOutputCacheAttribute]
        [Route("teammember/{employeeId:int:min(1)}/month/{expenseType:int:min(0)}")]
        public IEnumerable<ExpenseMonth> GetTeamMemberExpensesByMonth(int employeeId, int expenseType)
        {
            return _expenseRepository.GetTeamMemberExpensesByMonth(employeeId, expenseType);
        }

        /// <summary>
        /// Get Team Member expense summary
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="month">Month to filter</param>
        /// <param name="year">Year to filter</param>
        /// <returns>Team Member Summary Information</returns>
        [WebApiOutputCacheAttribute]
        [Route("teammember/{employeeId:int:min(1)}/summary/{year:int}/{month?}")]
        public async Task<IEnumerable<TeamMemberSummary>> GetTeamMemberSummaryExpenses(int employeeId, int year, int? month = null)
        {
            return await _expenseRepository.GetTeamMemberSummaryExpensesAsync(employeeId, month, year);
        }

    }
}
