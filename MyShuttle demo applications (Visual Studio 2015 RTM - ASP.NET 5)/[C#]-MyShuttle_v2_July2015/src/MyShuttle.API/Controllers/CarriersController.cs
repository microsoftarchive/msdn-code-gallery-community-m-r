
namespace MyShuttle.API.Controllers
{
    using Data;
    using Microsoft.AspNet.Mvc;
    using MyShuttle.Model;
    using System;
    using System.Threading.Tasks;

    [NoCacheFilter]
    public class CarriersController : Controller
    {
        ICarrierRepository _carrierRepository = null;
        MyShuttleSecurityContext _securityContext;

        public CarriersController(MyShuttleSecurityContext securityContext, ICarrierRepository carrierRepository)
        {
            _securityContext = securityContext;
            _carrierRepository = carrierRepository;
        }

        public async Task<Carrier> Get()
        {
            return await _carrierRepository.GetAsync(await _securityContext.GetCarrierId(User.Identity));
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Carrier carrier)
        {
            return await _carrierRepository.AddAsync(carrier);
        }

        [HttpPut]
        public async Task Put([FromBody]Carrier carrier)
        {
            await _carrierRepository.UpdateAsync(carrier);
        }

    }
}
