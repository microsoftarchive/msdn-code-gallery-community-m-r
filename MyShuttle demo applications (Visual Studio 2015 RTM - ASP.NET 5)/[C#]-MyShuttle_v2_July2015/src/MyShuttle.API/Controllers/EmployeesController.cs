
namespace MyShuttle.API.Controllers
{
    using Microsoft.AspNet.Mvc;
    using Data;
    using System.Threading.Tasks;
    using Model;

    [NoCacheFilter]
    public class EmployeesController : Controller
    {
        IEmployeeRepository _employeeRepository;
        MyShuttleSecurityContext _securityContext;

        public EmployeesController(MyShuttleSecurityContext securityContext, IEmployeeRepository employeeRepository)
        {
            _securityContext = securityContext;
            _employeeRepository = employeeRepository;
        }

        [ActionName("myprofile")]
        public async Task<Employee> GetMyProfile()
        {
            return await _employeeRepository.GetAsync(_securityContext.EmployeeId);
        }
    }
}
