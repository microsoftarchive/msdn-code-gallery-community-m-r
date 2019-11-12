namespace MyCompany.Vacation.Web.Controllers.OData
{
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Data.Services;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Hubs;
    using MyCompany.Vacation.Web.Infraestructure.Security;
    using MyCompany.Vacation.Web.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Http.OData;
    using System.Web.Http.OData.Query;

    /// <summary>
    /// Vacation request odata controller.
    /// </summary>
    [MyCompanyAuthorization]
    public class VacationRequestsODataController : EntitySetController<VacationRequest, int>
    {
        private readonly MyCompanyContext _context;
        private readonly ISecurityHelper _securityHelper;
        private readonly IVacationRequestRepository _vacationRequestRepository;
        private readonly IVacationNotificationService _vacationNotificationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="securityHelper">context</param>
        /// <param name="vacationRequestRepository">context</param>
        /// <param name="vacationNotificationService">context</param>
        public VacationRequestsODataController(
            MyCompanyContext context,
            ISecurityHelper securityHelper,
            IVacationRequestRepository vacationRequestRepository,
            IVacationNotificationService vacationNotificationService)
        {
            _context = context;
            _securityHelper = securityHelper;
            _vacationRequestRepository = vacationRequestRepository;
            _vacationNotificationService = vacationNotificationService;
        }

        /// <summary>
        /// Gets the vacation requests.
        /// </summary>
        /// <returns></returns>
        [Queryable(MaxExpansionDepth = 2)]
        public override IQueryable<VacationRequest> Get()
        {
            var queryOptions = this.QueryOptions;
            var identity = _securityHelper.GetUser();

            var vacations = _context.
                VacationRequests
                .Include("Employee")
                .Where(vr => vr.Employee.Team.Manager.Email == identity)
                .AsQueryable();

            queryOptions.ApplyTo(vacations);

            return vacations;
        }

        /// <summary>
        /// Gets the employee by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Employee GetEmployee([FromODataUri] int key)
        {
            VacationRequest vacationRequest =
                _context
                .VacationRequests
                .Include("Employee")
                .FirstOrDefault(vr => vr.VacationRequestId == key);

            if (vacationRequest == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return vacationRequest.Employee;
        }

        /// <summary>
        /// Accepts the vacation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true, true)]
        [HttpPost]

        public void AcceptVacation([FromODataUri] int key, ODataActionParameters parameters)
        {
            var vacationRequest = _vacationRequestRepository.Get(key);
            vacationRequest.Status = VacationRequestStatus.Approved;

            _vacationRequestRepository.Update(vacationRequest);

            string reason = parameters.ContainsKey("Reason") ? parameters["Reason"].ToString() : string.Empty;
            _vacationNotificationService.NotifyStatusChange(vacationRequest, reason);            
        }

        /// <summary>
        /// Rejects the vacation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true, true)]
        [HttpPost]
        public void RejectVacation([FromODataUri] int key, ODataActionParameters parameters)
        {
            var vacationRequest = _vacationRequestRepository.Get(key);
            vacationRequest.Status = VacationRequestStatus.Denied;

            _vacationRequestRepository.Update(vacationRequest);
            
            string reason = parameters.ContainsKey("Reason") ? parameters["Reason"].ToString() : string.Empty;
            _vacationNotificationService.NotifyStatusChange(vacationRequest, reason);
        }

        /// <summary>
        /// Gets the vacation by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override VacationRequest GetEntityByKey(int key)
        {
            var vacation = _context
                .VacationRequests
                .FirstOrDefault(vr => vr.VacationRequestId == key);
            return vacation;
        }

        /// <summary>
        /// Gets the entity key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override int GetKey(VacationRequest entity)
        {
            return entity.VacationRequestId;
        }


        /// <summary>
        /// Post action.
        /// NOTE: Overrided only to put the cache attribute.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true, true)]
        
        public override HttpResponseMessage Post(VacationRequest entity)
        {
            return base.Post(entity);
        }

        /// <summary>
        /// Creates a new vacation request
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true, true)]

        protected override VacationRequest CreateEntity(VacationRequest entity)
        {
            var identity = _securityHelper.GetUser();

            var employee = _context.Employees
                .Include("Team.Manager")
                .Single(e => e.Email == identity);

            entity.EmployeeId = employee.EmployeeId;

            _vacationRequestRepository.Add(entity);


            //To avoid cirucular references in serialization
            entity.Employee.VacationRequests = null;

            if (employee.Team != null && employee.Team.Manager != null)
            {
                VacationNotificationHub.NotifyNew(entity, employee.Team.Manager.Email);
            }
            _vacationNotificationService.NotifyNewVacationRequest(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the vacation request.
        /// </summary>
        /// <param name="key"></param>
        [WebApiOutputCacheAttribute(true, true)]
        public override void Delete(int key)
        {
            var vacationRequest = _context.VacationRequests.Find(key);
            _vacationRequestRepository.Delete(key);
            _vacationNotificationService.NotifyVacationRequestDeleted(vacationRequest);
        }
    }
}
