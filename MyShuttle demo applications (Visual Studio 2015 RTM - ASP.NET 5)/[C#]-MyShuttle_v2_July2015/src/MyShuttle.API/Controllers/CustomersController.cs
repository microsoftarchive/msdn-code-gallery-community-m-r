
namespace MyShuttle.API.Controllers
{
    using Microsoft.AspNet.Mvc;
    using Data;

    [NoCacheFilter]
    public class CustomersController : Controller
    {
        ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}
