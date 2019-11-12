namespace MyShuttle.Web.Controllers
{
    using API;
    using Data;
    using Microsoft.AspNet.Mvc;
    using Model;
    using Models;
    using System.Threading.Tasks;

    public class HomeController 
    {
        private ICarrierRepository _carrierRepository;

        [ViewDataDictionary]
        public ViewDataDictionary ViewData { get; set; }


        public HomeController(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public IActionResult Index()
        {
            ViewData.Model = new MyShuttleViewModel()
            {
                MainMessage = "The Ultimate B2B Shuttle Service Solution"
            };

            return new ViewResult() { ViewData = ViewData };

        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            return new RedirectToActionResult("Index", "Home", null);
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Carrier carrier)
        {
            return await _carrierRepository.AddAsync(carrier);
        }

    }
}