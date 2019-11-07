namespace MyCompany.Expenses.Web.Controllers
{
    using System;
    using System.Web.Http;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Model;
    using System.Web.Http.Cors;
    using MyCompany.Expenses.Web.CORS;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;
    using MyCompany.Expenses.Web.Infraestructure.Security;

    /// <summary>
    /// Employee Controller
    /// </summary>
    [RoutePrefix("api/employees")]
    [MyCompanyAuthorization]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository = null;
        private readonly ISecurityHelper _securityHelper = null;

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
        [Route("{pictureType:int:range(1,2)}")]
        [Route("~/noauth/api/employees/{pictureType:int:range(1,2)}")]
        public async Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType)
        {
            var employee = await _employeeRepository.GetByEmailAsync(_securityHelper.GetUser(), pictureType);
            return employee;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true)]
        [Route("{employeeId:int:min(1)}/{pictureType:int:range(1,2)}")]
        [Route("~/noauth/api/employees/{employeeId:int:min(1)}/{pictureType:int:range(1,2)}")]
        public async Task<Employee> Get(int employeeId, PictureType pictureType = PictureType.Small)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId, pictureType);
            return employee;
        }
    }
}