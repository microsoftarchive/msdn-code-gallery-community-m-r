
namespace MyShuttle.API.Controllers
{
    using Data;
    using Microsoft.AspNet.Mvc;
    using MyShuttle.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [NoCacheFilter]
    public class RidesController : Controller
    {
        IRidesRepository _ridesRepository;
        MyShuttleSecurityContext _securityContext;

        public RidesController(MyShuttleSecurityContext securityContext, IRidesRepository ridesRepository)
        {
            _securityContext = securityContext;
            _ridesRepository = ridesRepository;
        }

        public async Task<Ride> Get(int id)
        {
            return await _ridesRepository.GetAsync(id);
        }

        [ActionName("search")]
        public async Task<IEnumerable<Ride>> Get(int driverId, int vehicleId, int pageSize, int pageCount)
        {
            return await _ridesRepository.GetRidesAsync(await _securityContext.GetCarrierId(User.Identity), driverId, vehicleId, pageSize, pageCount);
        }

        [ActionName("count")]
        public async Task<int> GetCount(int? driverId, int? vehicleId)
        {
            return await _ridesRepository.GetCountAsync(await _securityContext.GetCarrierId(User.Identity), driverId, vehicleId);
        }

        [ActionName("myrides")]
        public async Task<IEnumerable<Ride>> GetMyRides(int count)
        {
            return await _ridesRepository.GetEmployeeAsync(_securityContext.EmployeeId, count);
        }

        [ActionName("mycompanyrides")]
        public async Task<IEnumerable<Ride>> GetMyCompanyRides(int count)
        {
            return await _ridesRepository.GetCompanyAsync(_securityContext.CustomerId, count);
        }



        [HttpPut]
        public async Task Put([FromBody]Ride ride)
        {
            await _ridesRepository.UpdateAsync(ride);
        }
    }
}
