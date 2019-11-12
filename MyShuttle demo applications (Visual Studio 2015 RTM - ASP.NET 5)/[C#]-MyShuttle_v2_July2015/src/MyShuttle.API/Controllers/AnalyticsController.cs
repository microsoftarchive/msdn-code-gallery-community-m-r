
namespace MyShuttle.API
{
    using Microsoft.AspNet.Mvc;
    using Data;
    using Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    [NoCacheFilter]
    public class AnalyticsController : Controller
    {
        IDriverRepository _driverRepository;
        MyShuttleSecurityContext _securityContext;
        IVehicleRepository _vehicleRepository;
        ICarrierRepository _carrierRepository;
        IRidesRepository _ridesRepository;

        public AnalyticsController(MyShuttleSecurityContext securityContext, IDriverRepository driverRepository
            , IVehicleRepository vehicleRepository, ICarrierRepository carrierRepository, IRidesRepository ridesRepository)
        {
            _driverRepository = driverRepository;
            _securityContext = securityContext;
            _vehicleRepository = vehicleRepository;
            _carrierRepository = carrierRepository;
            _ridesRepository = ridesRepository;
        }

        [ActionName("topdrivers")]
        public async Task<ICollection<Driver>> GetTopDrivers()
        {
            return await _driverRepository.GetTopDriversAsync(await _securityContext.GetCarrierId(User.Identity), GlobalConfig.TOP_NUMBER);
        }

        [ActionName("topvehicles")]
        public async Task<ICollection<Vehicle>> GetTopVehicles()
        {
            return await _vehicleRepository.GetTopVehiclesAsync(await _securityContext.GetCarrierId(User.Identity), GlobalConfig.TOP_NUMBER);
        }

        [ActionName("summary")]
        public async Task<SummaryAnalyticInfo> GetSummaryInfo()
        {
            return await _carrierRepository.GetAnalyticSummaryInfoAsync(await _securityContext.GetCarrierId(User.Identity));
        }

        [ActionName("rides")]
        public async Task<RidesAnalyticInfo> GetRidesEvolution()
        {
            DateTime fromFilter = DateTime.UtcNow.AddDays(-30);

            var labels = new List<int>();
            var to = DateTime.UtcNow;

            var date = fromFilter;
            while (DateTime.Compare(date, to) < 0)
            {
                labels.Add(date.Day);
                date = date.AddDays(1);
            }

            int carrierId = await _securityContext.GetCarrierId(User.Identity);

            IEnumerable<RideResult> evolution = await _ridesRepository.GetRidesEvolutionAsync(carrierId, fromFilter);

            var values = new int[labels.Count];
            foreach (var item in evolution)
            {
                int index = (int)(item.Date - fromFilter).TotalDays;
                values[index] = item.Value;
            }

            RideGroupInfo rides = new RideGroupInfo()
            {
                Days = labels,
                Values = values
            };

            int ridesCount = await _ridesRepository.LastDaysRidesAsync(carrierId, fromFilter);
            int passengersCount = await _ridesRepository.LastDaysPassengersAsync(carrierId, fromFilter);

            return new RidesAnalyticInfo()
            {
                LastDaysRides = ridesCount,
                LastDaysPassengers = passengersCount,
                RidesEvolution = rides
            };
        }
    }
}