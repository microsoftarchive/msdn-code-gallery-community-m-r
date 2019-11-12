namespace MyShuttle.Web.Controllers
{
    using API;
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Mvc;
    using Model;
    using Models;
    using System.Threading.Tasks;

    public class CarrierListController
    {
        private ICarrierRepository _carrierRepository;
        private MyShuttleSecurityContext _securityContext;

        [ViewDataDictionary]
        public ViewDataDictionary ViewData { get; set; }

        [FromServices]
        public UserManager<ApplicationUser> UserManager { get; set; }


        public CarrierListController(ICarrierRepository carrierRepository,
                                    MyShuttleSecurityContext securityContext)
        {
            _carrierRepository = carrierRepository;
            _securityContext = securityContext;
        }

        public async Task<IActionResult> Index(SearchViewModel searchVM)
        {
            string searchString = searchVM == null ? null : searchVM.SearchString;
            var carriers = await _carrierRepository.GetCarriersAsync(searchString);
            ViewData.Model = new CarrierListViewModel(carriers);
            return new ViewResult() { ViewData = ViewData, ViewName = "Index" };
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCarrier(CarrierViewModel carrierViewModel)
        {
            var vm = new CarrierListViewModel(await _carrierRepository.GetCarriersAsync(null));
            if (ViewData.ModelState.IsValid)
            {
                var carrier = new Carrier();
                carrierViewModel.CopyTo(carrier);
                var user = await UserManager.FindByNameAsync(carrier.Name);
                if (user == null)
                {
                    var carrierId = await _carrierRepository.AddAsync(carrier);
                    user = new ApplicationUser { UserName = carrier.Name, CarrierId = carrierId };
                    await UserManager.CreateAsync(user, _securityContext.DefaultPassword);
                    return new RedirectToActionResult("Login", "Carrier", null);
                }
                else
                {
                    ViewData.ModelState.AddModelError("UserNameNotAvailable","The User Name is not available");
                }
            }
            else
            {
                vm.LoadCarrierFrom(carrierViewModel);
            }

            ViewData.Model = vm;
            return new ViewResult() { ViewData = ViewData, ViewName = "Index" };
        }
    }
}
