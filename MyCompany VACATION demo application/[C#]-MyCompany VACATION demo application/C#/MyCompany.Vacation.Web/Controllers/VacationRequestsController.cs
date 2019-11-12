

namespace MyCompany.Vacation.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Notifications;
    using MyCompany.Vacation.Web.Hubs;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Common.CrossCutting;
    using System.Threading.Tasks;

    /// <summary>
    /// Vacation Requests Controller
    /// </summary>
    [RoutePrefix("api/vacationrequests")]
    public class VacationRequestsController : ApiController
    {
        IVacationRequestRepository _vacationRequestRepository = null;
        private readonly IEmployeeRepository _employeeRepository;
        ISecurityHelper _securityHelper = null;
        private readonly IVacationNotificationService _vacationNotificationService;

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
        /// Get vacation request
        /// </summary>
        /// <param name="year">Year.</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("{id:int}")]
        public VacationRequest Get(int id)
        {
            var identity = _securityHelper.GetUser();
            return _vacationRequestRepository.Get(id);
        }

        /// <summary>
        /// Add new vacation Request.
        /// </summary>
        /// <param name="vacationRequest">vacation Request information</param>
        /// <returns>vacationRequestId</returns>
        [WebApiOutputCacheAttribute(false, true)]
        public int Add(VacationRequest vacationRequest)
        {
            TraceManager.TraceError("Add vacation request");


            if (vacationRequest == null)
                throw new ArgumentNullException("vacationRequest");

            var employee =_employeeRepository.GetByEmail(_securityHelper.GetUser(), PictureType.Small);

            vacationRequest.EmployeeId = employee.EmployeeId;
            var newVacationRequestId = _vacationRequestRepository.Add(vacationRequest);
            var newVacationRequest = _vacationRequestRepository.Get(newVacationRequestId);

            TraceManager.TraceError("before if and Calling notify new in VacationHub");
            if (employee.Team != null && employee.Team.Manager != null)
            {
                TraceManager.TraceError("Calling notify new in VacationHub");
                VacationNotificationHub.NotifyNew(newVacationRequest, employee.Team.Manager.Email);
            }
            _vacationNotificationService.NotifyNewVacationRequest(vacationRequest);

            return newVacationRequest.VacationRequestId;
        }

        /// <summary>
        /// Delete vacation Request
        /// </summary>
        /// <param name="vacationRequestId">vacationRequestId</param>
        [WebApiOutputCacheAttribute(false, true)]
        [Route("{vacationRequestId:int:min(1)}")]
        [HttpDelete]
        public void Delete(int vacationRequestId)
        {
            var vacationRequest = _vacationRequestRepository.Get(vacationRequestId);
            _vacationRequestRepository.Delete(vacationRequestId);
            _vacationNotificationService.NotifyVacationRequestDeleted(vacationRequest);
        }

        /// <summary>
        /// Update Status
        /// </summary>
        [Route("{vacationRequestId:int:min(1)}/status/{status}")]
        [HttpPut]
        [WebApiOutputCacheAttribute(false, true)]
        public void UpdateStatus(int vacationRequestId, VacationRequestStatus status, [FromUri] string reason)
        {
            var vacationRequest = _vacationRequestRepository.Get(vacationRequestId);
            if (vacationRequest != null && vacationRequest.Status != status)
            {
                vacationRequest.Status = status;
                _vacationRequestRepository.Update(vacationRequest);
                _vacationNotificationService.NotifyStatusChange(vacationRequest, reason);
            }
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
        /// Get Vacation Requests Count for the user that makes the request
        /// </summary>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("user/count")]
        public int GetUserCount(int? month, int year, int status)
        {
            var identity = _securityHelper.GetUser();
            return _vacationRequestRepository.GetUserCount(identity, month, year, status);
        }

        /// <summary>
        /// Get Vacation Requests Count for the user that makes the request
        /// </summary>
        /// <param name="year">Year.</param>
        /// <returns>Number of pending requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("{year:int}/user/pending")]
        public int GetUserPendingVacation(int year)
        {
            var identity = _securityHelper.GetUser();
            return _vacationRequestRepository.GetUserPendingVacation(identity, year);
        }

        /// <summary>
        /// Get All requests for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter used for searching</param>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team")]
        public IEnumerable<VacationRequest> GetTeamVacationRequests(string filter, int? month, int year, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            var managerIdentity = _securityHelper.GetUser();
            return _vacationRequestRepository.GetTeamVacationRequests(managerIdentity, filter, month, year, status, pictureType, pageSize, pageCount);
        }

        /// <summary>
        /// Get Vacation Requests Count for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter used for searching</param>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team/count")]
        public int GetTeamCount(string filter, int? month, int year, int status)
        {
            var managerIdentity = _securityHelper.GetUser();
            return _vacationRequestRepository.GetTeamCount(managerIdentity, filter, month, year, status);
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
