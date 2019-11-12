
namespace MyShuttle.MobileServices.Controllers
{
    using DataObjects;
    using Microsoft.WindowsAzure.Mobile.Service;
    using Microsoft.WindowsAzure.Mobile.Service.Security;
    using Models;
    using Services.Redis;
    using StackExchange.Redis;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.OData;

    //[AuthorizeLevel(AuthorizationLevel.User)]
    public class EmployeeController : TableController<Employee>
    {
        MobileServiceContext _context = null;
        IDatabase _cache = null;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new MobileServiceContext();         
            DomainManager = new EntityDomainManager<Employee>(_context, Request, Services);

            // Create cache connection
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["RedisCache"].ToString());
            _cache = connection.GetDatabase();
        }

        // GET tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Employee GetEmployee(string id)
        {
            string cacheKey = string.Format("EmployeeId{0}", id);

            var employee = _cache.Get<Employee>(cacheKey);
            if (employee == null)
            {
                var singleEmployee = Lookup(id);
                if (singleEmployee != null && singleEmployee.Queryable.Any())
                {
                    employee = singleEmployee.Queryable.FirstOrDefault();
                    _cache.Set(cacheKey, employee);

                }
            }

            return employee;
        }


        // GET tables/Employee
        public IQueryable<Employee> GetAllEmployees()
        {
            return Query();
        }


        // PATCH tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Employee> PatchEmployee(string id, Delta<Employee> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Employee
        public async Task<IHttpActionResult> PostEmployee(Employee item)
        {
            // EmployeeId is not defined as identity due to the current version of EF doesn´t allow data annotations
            item.EmployeeId = _context.Employees.Max(r => r.EmployeeId) + 1;
            
            Employee current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEmployee(string id)
        {
            return DeleteAsync(id);
        }

    }
}