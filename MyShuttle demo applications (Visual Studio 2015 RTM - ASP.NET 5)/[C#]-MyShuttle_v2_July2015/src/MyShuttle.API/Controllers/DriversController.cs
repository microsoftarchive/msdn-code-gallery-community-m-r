
namespace MyShuttle.API.Controllers
{
    using Microsoft.AspNet.Mvc;
    using Data;
    using System.Collections.Generic;
    using Model;
    using System.Threading.Tasks;
    using System;

    [NoCacheFilter]
    public class DriversController : Controller
    {
        IDriverRepository _driverRepository;
        MyShuttleSecurityContext _securityContext;

        public DriversController(MyShuttleSecurityContext securityContext, IDriverRepository driverRepository)
        {
            _securityContext = securityContext;
            _driverRepository = driverRepository;
        }


        public async Task<Driver> Get(int id)
        {
            return await _driverRepository.GetAsync(await _securityContext.GetCarrierId(User.Identity), id);
        }

        [ActionName("search")]
        public async Task<IEnumerable<Driver>> Get(string filter, int pageSize, int pageCount)
        {
            if (String.IsNullOrEmpty(filter))
                filter = string.Empty;

            return await _driverRepository.GetDriversAsync(await _securityContext.GetCarrierId(User.Identity), filter, pageSize, pageCount);
        }

        [ActionName("filter")]
        public async Task<IEnumerable<Driver>> GetDriversFilter()
        {
            return await _driverRepository.GetDriversFilterAsync(await _securityContext.GetCarrierId(User.Identity));
        }


        [ActionName("count")]
        public async Task<int> GetCount(string filter)
        {
            if (String.IsNullOrEmpty(filter))
                filter = string.Empty;

            return await _driverRepository.GetCountAsync(await _securityContext.GetCarrierId(User.Identity), filter);
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Driver driver)
        {
            driver.CarrierId = await _securityContext.GetCarrierId(User.Identity);
            return await _driverRepository.AddAsync(driver);
        }

        [HttpPut]
        public async Task Put([FromBody]Driver driver)
        {
            driver.CarrierId = await _securityContext.GetCarrierId(User.Identity);
            await _driverRepository.UpdateAsync(driver);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _driverRepository.DeleteAsync(id);
        }
    }
}