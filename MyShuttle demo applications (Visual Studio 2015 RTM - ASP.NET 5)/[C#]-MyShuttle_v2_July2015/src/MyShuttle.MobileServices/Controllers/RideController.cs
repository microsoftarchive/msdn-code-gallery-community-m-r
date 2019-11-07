
namespace MyShuttle.MobileServices.Controllers
{
    using DataObjects;
    using Microsoft.WindowsAzure.Mobile.Service;
    using Microsoft.WindowsAzure.Mobile.Service.Security;
    using Models;
    using Services.ConnectedServices;
    using Services.Invoices;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.OData;

    //[AuthorizeLevel(AuthorizationLevel.User)]
    public class RideController : TableController<Ride>
    {
        MobileServiceContext _context = null;
        EntityDomainManager<Employee> _domainEmployeeManager = null;
        SalesforceService _salesforceService = null;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new MobileServiceContext();
            _salesforceService = new SalesforceService();
            _domainEmployeeManager = new EntityDomainManager<Employee>(_context, Request, Services);
            DomainManager = new EntityDomainManager<Ride>(_context, Request, Services);
        }

        // GET tables/Ride
        public IQueryable<Ride> GetAllRides()
        {
            return Query();
        }

        // GET tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Ride> GetRide(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Ride> PatchRide(string id, Delta<Ride> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Ride
        public async Task<IHttpActionResult> PostRide(Ride item)
        {
            var employee = GetEmployee(item);
            if (employee == null) 
                return NotFound();

            item.RideId = _context.Rides.Max(r => r.RideId) + 1;
            item.EmployeeId = employee.EmployeeId; 
            item.Employee = null;

            Ride current = await InsertAsync(item);

            if (current != null)
            {
                // After creating the ride...                                    
                // Create invoice, Queue Message and Submit to Azure Queue
                await InvoiceService.CreateInvoice(GetInvoice(current, employee));

                // Add Customer Contact´s data to SaleForce CRM
                await _salesforceService.AddContact(item, employee);
            }
            
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public System.Threading.Tasks.Task DeleteRide(string id)
        {
            return DeleteAsync(id);
        }


        private Invoice GetInvoice(Ride ride, Employee employee)
        {
            return new Invoice()
            {
                RideId = ride.RideId,
                Cost = ride.Cost,
                Distance = ride.Distance,
                Duration = ride.Duration,
                EmployeeName = employee.Name,
                StartDateTime = ride.StartDateTime,
                EndDateTime = ride.EndDateTime,
                StartAddress = ride.StartAddress,
                EndAddress = ride.EndAddress,
                Signature = ride.Signature,
            };
        }

        private Employee GetEmployee(Ride item)
        {
            var employee = _domainEmployeeManager
                    .Query()
                    .FirstOrDefault(m => m.Email == item.Employee.Email);
            return employee;
        }

    }
}