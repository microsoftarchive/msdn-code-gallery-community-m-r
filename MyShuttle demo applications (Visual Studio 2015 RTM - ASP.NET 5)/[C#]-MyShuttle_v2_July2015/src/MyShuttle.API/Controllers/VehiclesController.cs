
namespace MyShuttle.API.Controllers
{
    using Data;
    using Microsoft.AspNet.Mvc;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [NoCacheFilter]
    public class VehiclesController : Controller
    {
        IVehicleRepository _vehicleRepository;
        MyShuttleSecurityContext _securityContext;
        IServiceProvider _serviceProvider;

        public VehiclesController(MyShuttleSecurityContext securityContext, IVehicleRepository vehicleRepository, IServiceProvider serviceProvider)
        {
            _securityContext = securityContext;
            _vehicleRepository = vehicleRepository;
            _serviceProvider = serviceProvider;
        }
        
        public async Task<Vehicle> Get(int id)
        {
            return await _vehicleRepository.GetAsync(id);
        }

        [ActionName("device")]
        public async Task<Vehicle> GetByDeviceId(string deviceId)
        {
            return await _vehicleRepository.GetByDeviceIdAsync(deviceId);
        }

        [ActionName("search")]
        public async Task<IEnumerable<Vehicle>> Get(string filter, int pageSize, int pageCount)
        {
            if (String.IsNullOrEmpty(filter))
                filter = string.Empty;

            return await _vehicleRepository.GetVehiclesAsync(await _securityContext.GetCarrierId(User.Identity), filter, pageSize, pageCount);
        }

        [ActionName("filter")]
        public async Task<IEnumerable<Vehicle>> GetVehiclesFilter()
        {
            return await _vehicleRepository.GetVehiclesFilterAsync(await _securityContext.GetCarrierId(User.Identity));
        }

        [ActionName("count")]
        public async Task<int> GetCount(string filter)
        {
            if (String.IsNullOrEmpty(filter))
                filter = string.Empty;

            return await _vehicleRepository.GetCountAsync(await _securityContext.GetCarrierId(User.Identity), filter);
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Vehicle vehicle)
        {
            vehicle.CarrierId = await _securityContext.GetCarrierId(User.Identity);
            return await _vehicleRepository.AddAsync(vehicle);
        }

        [HttpPut]
        public async Task Put([FromBody]Vehicle vehicle)
        {
            vehicle.CarrierId = await _securityContext.GetCarrierId(User.Identity);
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _vehicleRepository.DeleteAsync(id);
        }

        [ActionName("price")]
        public async Task<IEnumerable<Vehicle>> GetByPrice(double latitude, double longitude, int count)
        {
            return await _vehicleRepository.GetByPriceAsync(latitude, longitude, count);
        }

        [ActionName("distance")]
        public async Task<IEnumerable<Vehicle>> GetByDistance(double latitude, double longitude, int count)
        {
            return await _vehicleRepository.GetByDistanceAsync(latitude, longitude, count);
        }

    }
}
