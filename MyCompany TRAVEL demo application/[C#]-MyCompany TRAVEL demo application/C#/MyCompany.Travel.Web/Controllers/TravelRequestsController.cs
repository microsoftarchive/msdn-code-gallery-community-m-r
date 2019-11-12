namespace MyCompany.Travel.Web.Controllers
{
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Hubs;
    using MyCompany.Travel.Web.Infraestructure.Security;
    using MyCompany.Travel.Web.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    /// Travel Requests Controller
    /// </summary>
    [RoutePrefix("api/travelrequests")]
    [MyCompanyAuthorization]
    public class TravelRequestsController : ApiController
    {
        private readonly ITravelRequestRepository _travelRequestRepository = null;
        private readonly IEmployeeRepository _employeeRepository = null;
        private readonly ISecurityHelper _securityHelper = null;
        private readonly ITravelNotificationService _notificationService = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="travelRequestRepository">ITravelRequestRepository dependency</param>
        /// <param name="employeeRepository">IEmployeeRepository dependency</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        /// <param name="notificationService">ITravelNotificationService dependency</param>
        public TravelRequestsController(
            ITravelRequestRepository travelRequestRepository, 
            IEmployeeRepository employeeRepository, 
            ISecurityHelper securityHelper,
            ITravelNotificationService notificationService
            )
        {
            if (travelRequestRepository == null)
                throw new ArgumentNullException("travelRequestRepository");

            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            if (notificationService == null)
                throw new ArgumentNullException("notificationService");

            _travelRequestRepository = travelRequestRepository;
            _employeeRepository = employeeRepository;
            _securityHelper = securityHelper;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get expense by Id
        /// </summary>
        /// <param name="travelrequestId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute()]
        [Route("{travelrequestId:int:min(1)}/{pictureType:int:range(1,2)}")]
        [Route("~/noauth/api/travelrequests/{travelrequestId:int:min(1)}/{pictureType:int:range(1,2)}")]
        public async Task<TravelRequest> Get(int travelrequestId, PictureType pictureType)
        {
            return await _travelRequestRepository.GetCompleteInfoAsync(travelrequestId, pictureType);
        }

        /// <summary>
        /// Get all travels for the user that makes the request
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("user")]
        [Route("~/noauth/api/travelrequests/user")]
        public async Task<IEnumerable<TravelRequest>> GetUserTravelRequests(string filter, int status, int pageSize, int pageCount)
        {
            var identity = _securityHelper.GetUser();
            return await _travelRequestRepository.GetUserTravelRequestsAsync(identity, filter, status, pageSize, pageCount);
        }

        /// <summary>
        /// Get all not finished travels for the user that makes the request
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("unfinished/user")]
        [Route("~/noauth/api/travelrequests/unfinished/user")]
        public async Task<IEnumerable<TravelRequest>> GetNotFinishedUserTravelRequests(string filter, int status)
        {
            var identity = _securityHelper.GetUser();
            return await _travelRequestRepository.GetNotFinishedUserTravelRequestsAsync(identity, filter, status);
        }

        /// <summary>
        /// Get All requests for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team")]
        [Route("~/noauth/api/travelrequests/team")]
        public async Task<IEnumerable<TravelRequest>> GetTeamTravelRequests(string filter, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _travelRequestRepository.GetTeamTravelRequestsAsync(managerIdentity, filter, status, pictureType, pageSize, pageCount);
        }

        /// <summary>
        /// Get all not finished travels for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("unfinished/team")]
        [Route("~/noauth/api/travelrequests/unfinished/team")]
        public async Task<IEnumerable<TravelRequest>> GetNotFinishedTeamTravelRequests(string filter, int status, PictureType pictureType)
        {
            var identity = _securityHelper.GetUser();
            return await _travelRequestRepository.GetNotFinishedTeamTravelRequestsAsync(identity, filter, status, pictureType);
        }

        /// <summary>
        /// Get All requests 
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("all")]
        [Route("~/noauth/api/travelrequests/all")]
        public async Task<IEnumerable<TravelRequest>> GetAllTravelRequests(string filter, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            return await _travelRequestRepository.GetAllTravelRequestsAsync(filter, status, pictureType, pageSize, pageCount);
        }

        /// <summary>
        /// Get travels count by city in last year and month for the entire team
        /// </summary>
        /// <returns>a list with the travel distribution</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team/distribution/{maxPicturesPerCity:int:min(1)}")]
        [Route("~/noauth/api/travelrequests/team/distribution/{maxPicturesPerCity:int:min(1)}")]
        public async Task<IEnumerable<TravelDistribution>> GetTeamTravelDistribution(int maxPicturesPerCity)
        {
            var managerIdentity = _securityHelper.GetUser();
            return await _travelRequestRepository.GetTeamTravelDistributionAsync(managerIdentity, maxPicturesPerCity);
        }

        /// <summary>
        /// Get Count for the user that makes the request
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("user/count")]
        [Route("~/noauth/api/travelrequests/user/count")]
        public async Task<int> GetUserCount(string filter, int status)
        {
            var identity = _securityHelper.GetUser();
            return await _travelRequestRepository.GetUserCountAsync(identity, filter, status);
        }

        /// <summary>
        /// Get Count for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("team/count")]
        [Route("~/noauth/api/travelrequests/team/count")]
        public int GetTeamCount(string filter, int status)
        {
            var managerIdentity = _securityHelper.GetUser();
            return _travelRequestRepository.GetTeamCount(managerIdentity, filter, status);
        }

        /// <summary>
        /// Get all Travel Requests count
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("all/count")]
        [Route("~/noauth/api/travelrequests/all/count")]
        public async Task<int> GetAllCount(string filter, int status)
        {
            return await _travelRequestRepository.GetAllCountAsync(filter, status);
        }

        /// <summary>
        /// Add new request.
        /// </summary>
        /// <param name="travelRequest">request information</param>
        /// <returns>requestId</returns>
        [WebApiOutputCacheAttribute(false, true)]
        public async Task<int> Add(TravelRequest travelRequest)
        {
            if (travelRequest == null)
                throw new ArgumentNullException("travelRequest");

            var employee = await _employeeRepository.GetAsync(travelRequest.EmployeeId);
            var team = employee.Team;

            if (team == null || team.Manager == null)
                travelRequest.Status = TravelRequestStatus.Approved;

            var newTravelRequestId = await _travelRequestRepository.AddAsync(travelRequest);
            
            if (team != null)
            {
                var manager = team.Manager;
                if (manager != null)
                {
                    var newTravelRequest = await _travelRequestRepository.GetWithEmployeeInfoAsync(newTravelRequestId);
                    TravelsNotificationHub.NotifyNew(newTravelRequest, manager.Email);
                    await _notificationService.EmailNotifyNewRequest(newTravelRequest);
                }
            }

            return newTravelRequestId;
        }

        /// <summary>
        /// Add new request.
        /// </summary>
        /// <param name="travelRequest">request information</param>
        [HttpPut()]
        [WebApiOutputCacheAttribute(false, true)]
        public async Task Update(TravelRequest travelRequest)
        {
            if (travelRequest == null)
                throw new ArgumentNullException("travelRequest");

            await _travelRequestRepository.UpdateAsync(travelRequest);
        }

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="travelrequestId">Id to update</param>
        /// <param name="status">TravelRequestStatus</param>
        /// <param name="comments">Comments </param>
        [Route("update")]
        [Route("~/noauth/api/travelrequests/update")]
        [HttpGet]
        [WebApiOutputCacheAttribute(false, true)]
        public async Task UpdateStatus(int travelrequestId, TravelRequestStatus status, string comments)
        {
            var travelRequest = await _travelRequestRepository.GetAsync(travelrequestId);
            if (travelRequest != null)
            {
                travelRequest.Status = status;
                if (!string.IsNullOrWhiteSpace(comments))
                {
                    if (!string.IsNullOrEmpty(travelRequest.Comments))
                        travelRequest.Comments = String.Format("{0}{1}{2}", travelRequest.Comments,System.Environment.NewLine,comments); 
                    else
                        travelRequest.Comments = comments;
                }
                await _travelRequestRepository.UpdateAsync(travelRequest);

                if (travelRequest.Status == TravelRequestStatus.Approved)
                {
                    TravelsNotificationHub.NotifyApproved(travelRequest);
                }
                await _notificationService.EmailNotifyStatusChange(travelRequest, comments);
            }
        }

        /// <summary>
        /// Delete travel request
        /// </summary>
        /// <param name="travelrequestId">travelrequestId</param>
        [WebApiOutputCacheAttribute(false, true)]
        [Route("{travelrequestId:int:min(1)}")]
        [Route("~/noauth/api/travelrequests/{travelrequestId:int:min(1)}")]
        [HttpDelete]
        public async Task Delete(int travelrequestId)
        {
            await _travelRequestRepository.DeleteAsync(travelrequestId);
        }
    }
}