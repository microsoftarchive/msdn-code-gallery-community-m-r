namespace MyCompany.Expenses.WebApiRole.Controllers
{
    using System;
    using System.Web.Http;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Model;
    using System.Web.Http.Cors;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;

    /// <summary>
    /// Employee Controller
    /// </summary>
    [RoutePrefix("api/employees")]
    [Authorize]
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
        [Route("{pictureType:int:range(1,2)}")]
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
        [Route("{employeeId:int:min(1)}/{pictureType:int:range(1,2)}")]
        public async Task<Employee> Get(int employeeId, PictureType pictureType = PictureType.Small)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId, pictureType);
            return employee;
        }
    }
}