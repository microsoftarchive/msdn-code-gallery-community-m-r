
namespace MyCompany.Travel.Web.Controllers
{
    using Microsoft.WindowsAzure.ActiveDirectory;
    using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Infraestructure.Security;
    using MyCompany.Travel.Web.Security;
    using System;
    using System.Collections.Generic;
    using System.Data.Services.Client;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Http;

    /// <summary>
    /// Employee Controller
    /// </summary>
    [RoutePrefix("api/employees")]
    [MyCompanyAuthorization]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository = null;
        private readonly ISecurityHelper _securityHelper = null;
        private readonly IADGraphApi _ADGrapAPI = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository">IEmployeeRepository dependency</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        /// <param name="ADGrapAPI">IADGraphApi dependency</param>
        public EmployeesController(IEmployeeRepository employeeRepository, ISecurityHelper securityHelper, IADGraphApi ADGrapAPI)
        {
            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            if (ADGrapAPI == null)
                throw new ArgumentNullException("ADGrapAPI");

            _employeeRepository = employeeRepository;
            _securityHelper = securityHelper;
            _ADGrapAPI = ADGrapAPI;
        }

        /// <summary>
        /// Get logged employee info
        /// </summary>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("{pictureType:int:range(1,2)}")]
        [Route("~/noauth/api/employees/{pictureType:int:range(1,2)}")]
        public async Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType)
        {
            string userPrincipalName = _securityHelper.GetUser();
            var employee = await _employeeRepository.GetByEmailAsync(userPrincipalName, pictureType);
            if (employee != null)
            {
                employee.IsRRHH = IsHHRREmployee(userPrincipalName);
            }

            return employee;
        }

        /// <summary>
        /// Get employees info
        /// </summary>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true)]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees;
        }

        private bool IsHHRREmployee(string userPrincipalName)
        {
            if (SecurityHelper.RequestIsNoAuthRoute() || IsDemoUser(userPrincipalName))
                return true;

            string groupName = WebConfigurationManager.AppSettings["HHRRGroupName"];
            return _ADGrapAPI.IsInGroup(userPrincipalName, groupName);
        }

        bool IsDemoUser(string userPrincipalName)
        {
            // Only needed for demos
            return userPrincipalName.Equals(WebConfigurationManager.AppSettings["rrhhIdentity"], StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
