namespace MyCompany.Vacation.Web.Controllers
{
    using System;
    using System.Web.Http;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    /// <summary>
    /// Employee Controller
    /// </summary>
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        IEmployeeRepository _employeeRepository = null;
        ISecurityHelper _securityHelper = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository">IEmployeeRepository dependency</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        public EmployeesController(IEmployeeRepository employeeRepository, ISecurityHelper securityHelper)
        {
            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            _employeeRepository = employeeRepository;
            _securityHelper = securityHelper;
        }

        /// <summary>
        /// Get logged employee info
        /// </summary>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("current/{pictureType}")]
        public Employee GetLoggedEmployeeInfo(PictureType pictureType)
        {
            return _employeeRepository.GetByEmail(_securityHelper.GetUser(), pictureType);
        }
    }
}
