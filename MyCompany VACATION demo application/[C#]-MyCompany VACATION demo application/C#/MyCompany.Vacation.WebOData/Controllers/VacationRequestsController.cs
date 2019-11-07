
namespace MyCompany.Vacation.Web.Controllers
{
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Infraestructure.Security;
    using MyCompany.Vacation.Web.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    /// Vacation Requests Controller
    /// </summary>
    [RoutePrefix("api/vacationrequests")]
    [MyCompanyAuthorization]
    public class VacationRequestsController : ApiController
    {
        private readonly IVacationRequestRepository _vacationRequestRepository = null;
        private readonly IEmployeeRepository _employeeRepository = null;
        private readonly ISecurityHelper _securityHelper = null;
        private readonly IVacationNotificationService _vacationNotificationService = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vacationRequestRepository">IVacationRequestRepository dependency</param>
        /// <param name="employeeRepository">IEmployeeRepository dependency.</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        /// <param name="vacationNotificationService">The vacation notification service.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public VacationRequestsController(
            IVacationRequestRepository vacationRequestRepository,
            IEmployeeRepository employeeRepository,
            ISecurityHelper securityHelper,
            IVacationNotificationService vacationNotificationService)
        {
            if (vacationRequestRepository == null)
                throw new ArgumentNullException("vacationRequestRepository");

            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            _vacationRequestRepository = vacationRequestRepository;
            _employeeRepository = employeeRepository;
            _securityHelper = securityHelper;
            _vacationNotificationService = vacationNotificationService;
        }

        
        /// <summary>
        /// Get vacationrequests for the user that makes the request. 
        /// </summary>
        /// <param name="year">Year.</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("{year:int}/user")]
        public async Task<IEnumerable<VacationRequest>> GetUserVacationRequests(int year)
        {
            var identity = _securityHelper.GetUser();
            return await _vacationRequestRepository.GetUserVacationRequests(identity, year);
        }

        /// <summary>
        /// Get All requests for the manager that makes the request
        /// </summary>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team/employee")]
        public async Task<IEnumerable<Employee>> GetTeamVacationRequestsByEmployee(int? month, int year, int status, PictureType pictureType)
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _vacationRequestRepository.GetTeamVacationRequestsByEmployee(managerIdentity, month, year, status, pictureType);
        }
    }
}
