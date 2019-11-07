
namespace MyCompany.Vacation.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using AutoMapper;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Model;
    using MyCompany.Common.CrossCutting;
    using MyCompany.Vacation.Data.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// The vacationRequest repository implementation
    /// </summary>
    public class VacationRequestRepository : IVacationRequestRepository
    {
        private readonly MyCompany.Common.EventBus.IEventBus _eventBus = null;
        private readonly IWorkingDaysCalculator _workingDaysCalculator;
        private readonly bool _eventBusEnabled = false;

        private readonly MyCompanyContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">the context dependency</param>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="workingDaysCalculator">The working days calculator.</param>
        /// <param name="eventBusEnabled">if set to <c>true</c> [event bus enabled].</param>
        public VacationRequestRepository(MyCompanyContext context, IEventBus eventBus, IWorkingDaysCalculator workingDaysCalculator, bool eventBusEnabled)
        {
            _eventBusEnabled = eventBusEnabled;
            _eventBus = eventBus;
            _workingDaysCalculator = workingDaysCalculator;
            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="vacationRequestId"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public VacationRequest Get(int vacationRequestId)
        {
            return _context.VacationRequests
                .Include(v => v.Employee)
                .Select(BuildVacationRequest)
                .Single(q => q.VacationRequestId == vacationRequestId);
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public IEnumerable<VacationRequest> GetAll()
        {
            return _context.VacationRequests.ToList();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="vacationRequest"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public int Add(VacationRequest vacationRequest)
        {
            Employee employee;
            employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == vacationRequest.EmployeeId);

            if (null == employee)
                throw new Exception("Employee does not exist");

            vacationRequest.CreationDate = DateTime.Now.ToUniversalTime();
            vacationRequest.LastModifiedDate = vacationRequest.CreationDate;
            vacationRequest.Status = VacationRequestStatus.Pending;

            int employeePendingVacations = GetUserPendingVacation(employee.Email, vacationRequest.From.Year);

            vacationRequest.NumDays = GetVacationNumDays(vacationRequest);

            if ((employeePendingVacations - vacationRequest.NumDays) < 0)
                throw new Exception("Maximum number days exceeded");

            _context.VacationRequests.Add(vacationRequest);
            _context.SaveChanges();

            if (_eventBusEnabled)
            {
                var dto = Mapper.Map<VacationRequestDTO>(vacationRequest);
                _eventBus.Publish<VacationRequestDTO>(dto, VacationActions.AddVacation);
            }

            return vacationRequest.VacationRequestId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="vacationRequest"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        public void Update(VacationRequest vacationRequest)
        {
            //Only status can be changed.
            var vacationRequesttoUpdate = _context.VacationRequests
                .Single(q => q.VacationRequestId == vacationRequest.VacationRequestId);

            bool hasChanged = vacationRequesttoUpdate.Status != vacationRequest.Status;

            if (!hasChanged)
                return;

            vacationRequesttoUpdate.Status = vacationRequest.Status;

            _context.SaveChanges();

            if (_eventBusEnabled)
            {
                var dto = Mapper.Map<VacationRequestDTO>(vacationRequest);
                _eventBus.Publish<VacationRequestDTO>(dto, VacationActions.UpdateVacation);
            }
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="vacationRequestId"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        public void Delete(int vacationRequestId)
        {
            var vacationRequest = _context.VacationRequests.Find(vacationRequestId);

            if (vacationRequest != null)
            {
                _context.VacationRequests.Remove(vacationRequest);
                _context.SaveChanges();

                if (_eventBusEnabled)
                    _eventBus.Publish<int>(vacationRequestId, VacationActions.DeleteVacation);
            }
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="employeeIdentity"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public async Task<IEnumerable<VacationRequest>> GetUserVacationRequests(string employeeIdentity, int year)
        {
            var results = await _context.VacationRequests
                .Include(v => v.Employee)
                .Where(q => q.Employee.Email == employeeIdentity
                            &&
                            (q.From.Year == year))
                .OrderByDescending(q => q.From)
                .ToListAsync();

            return results.Select(BuildVacationRequest);
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public IEnumerable<VacationRequest> GetUserVacationRequests(int employeeId, int year)
        {
            var results = _context.VacationRequests
                .Include(v => v.Employee)
                .Where(q => q.Employee.EmployeeId == employeeId
                            &&
                            (q.From.Year == year))
                .OrderByDescending(q => q.From)
                .ToList()
                .Select(BuildVacationRequest)
                .ToList();

            return results;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="employeeIdentity"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="month"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public int GetUserCount(string employeeIdentity, int? month, int year, int status)
        {
            return _context.VacationRequests
                .Where(q => q.Employee.Email == employeeIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (!month.HasValue || q.From.Month == month)
                            &&
                            (q.From.Year == year))
                .Count();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="employeeIdentity"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public int GetUserPendingVacation(string employeeIdentity, int year)
        {
            int maxVacationNumber = _context.Employees.Where(q => q.Email == employeeIdentity)
                    .Select(e => e.Office.Calendar.Vacation).FirstOrDefault();

            int employeeVacationRequests = _context.VacationRequests
                    .Where(q =>
                        q.Status != VacationRequestStatus.Denied
                        && q.Employee.Email == employeeIdentity
                        && q.From.Year == year)
                                .Sum(q => (int?)q.NumDays) ?? 0;

            return maxVacationNumber - employeeVacationRequests;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="month"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public IEnumerable<VacationRequest> GetTeamVacationRequests(string managerIdentity, string filter, int? month, int year, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            var results = _context.VacationRequests
                .Where(q =>
                            (String.IsNullOrEmpty(filter) ||
                            q.Employee.FirstName.Contains(filter) ||
                            q.Employee.LastName.Contains(filter) ||
                            (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            &&
                            q.Employee.Team.Manager.Email == managerIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (!month.HasValue || q.From.Month == month)
                            &&
                            (q.From.Year == year))
                .OrderByDescending(q => q.From)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .Select(v => new
                {
                    VacationRequest = v,
                    Employee = v.Employee,
                    EmployeePictures = v.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)
                })
                .ToList()
                .Select(v => BuildVacationRequest(v.VacationRequest));

            return results;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="month"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public int GetTeamCount(string managerIdentity, string filter, int? month, int year, int status)
        {
            return _context.VacationRequests.Count(q => (String.IsNullOrEmpty(filter) ||
                                                q.Employee.FirstName.Contains(filter) ||
                                                q.Employee.LastName.Contains(filter) ||
                                                (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                                                &&
                                                q.Employee.Team.Manager.Email == managerIdentity
                                                &&
                                                ((int)q.Status & status) == (int)q.Status
                                                &&
                                                (!month.HasValue || q.From.Month == month)
                                                &&
                                                (q.From.Year == year));
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="month"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IVacationRequestRepository"/></returns>
        public async Task<IEnumerable<Employee>> GetTeamVacationRequestsByEmployee(string managerIdentity, int? month, int year, int status, PictureType pictureType)
        {
            List<Employee> employees = await _context.Employees
                                    .Include(e => e.VacationRequests)
                                    .Where(e => e.Team.Manager.Email == managerIdentity)
                                    .ToListAsync();

            IEnumerable<int> employeeIds = employees.Select(e => e.EmployeeId).Distinct();

            var employeePictures = _context.EmployeePictures
                .Where(e => employeeIds.Contains(e.EmployeeId) && e.PictureType == pictureType)
                .ToList()
                .Select(e => new EmployeePicture()
                            {
                                Employee = null,
                                PictureType = e.PictureType,
                                EmployeePictureId = e.EmployeePictureId,
                                EmployeeId = e.EmployeeId,
                                Content = e.Content
                            });

            var employeeVacations = _context.VacationRequests
                .Where(vr => employeeIds.Contains(vr.EmployeeId) && vr.From.Month == month);

            employees.ForEach(e =>
                                    {
                                        e.EmployeePictures = employeePictures.Where(ep => ep.EmployeeId == e.EmployeeId).ToList();
                                        e.VacationRequests = employeeVacations.Where(vr => vr.EmployeeId == e.EmployeeId).ToList();
                                    });
            return employees;
        }

        VacationRequest BuildVacationRequest(VacationRequest vacationRequest)
        {
            return new VacationRequest()
            {
                VacationRequestId = vacationRequest.VacationRequestId,
                From = vacationRequest.From,
                To = vacationRequest.To,
                NumDays = vacationRequest.NumDays,
                Comments = vacationRequest.Comments,
                CreationDate = vacationRequest.CreationDate,
                LastModifiedDate = vacationRequest.LastModifiedDate,
                Status = vacationRequest.Status,
                EmployeeId = vacationRequest.EmployeeId,
                Employee = new Employee()
                {
                    EmployeeId = vacationRequest.Employee.EmployeeId,
                    FirstName = vacationRequest.Employee.FirstName,
                    LastName = vacationRequest.Employee.LastName,
                    JobTitle = vacationRequest.Employee.JobTitle,
                    Email = vacationRequest.Employee.Email,
                    TeamId = vacationRequest.Employee.TeamId,
                    EmployeePictures = vacationRequest.Employee.EmployeePictures != null ? vacationRequest.Employee.EmployeePictures
                                .Select(ep => new EmployeePicture()
                                {
                                    Employee = null,
                                    PictureType = ep.PictureType,
                                    EmployeePictureId = ep.EmployeePictureId,
                                    EmployeeId = ep.EmployeeId,
                                    Content = ep.Content
                                }).ToList()
                                : null,
                },
            };
        }

        EmployeeVacationRequests BuildEmployeeVacationRequests(EmployeeVacationRequests employeeVacationRequests)
        {
            return new EmployeeVacationRequests()
            {
                Employee = new Employee()
                {
                    EmployeeId = employeeVacationRequests.Employee.EmployeeId,
                    FirstName = employeeVacationRequests.Employee.FirstName,
                    LastName = employeeVacationRequests.Employee.LastName,
                    JobTitle = employeeVacationRequests.Employee.JobTitle,
                    Email = employeeVacationRequests.Employee.Email,
                    TeamId = employeeVacationRequests.Employee.TeamId,
                    EmployeePictures = employeeVacationRequests.Employee.EmployeePictures != null ?
                                employeeVacationRequests.Employee.EmployeePictures
                                .Select(ep => new EmployeePicture()
                                {
                                    Employee = null,
                                    PictureType = ep.PictureType,
                                    EmployeePictureId = ep.EmployeePictureId,
                                    EmployeeId = ep.EmployeeId,
                                    Content = ep.Content
                                }).ToList()
                                : null,
                },
            };
        }

        private int GetVacationNumDays(VacationRequest vacationRequest)
        {
            Employee employee;
            employee = _context.Employees.Single(e => e.EmployeeId == vacationRequest.EmployeeId);

            int numDays = _workingDaysCalculator.GetWorkingDays(employee.OfficeId, vacationRequest.From,
                                                                            vacationRequest.To);
            return numDays;
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }

    }
}
