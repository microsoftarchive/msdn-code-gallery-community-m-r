
namespace MyShuttle.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IRidesRepository
    {
        Task<Ride> GetAsync(int rideId);
        Task<IEnumerable<Ride>> GetCompanyAsync(int customerId, int count);
        Task<int> GetCountAsync(int carrierId, int? driverId, int? vehicleId);
        Task<IEnumerable<Ride>> GetEmployeeAsync(int employeeId, int count);
        Task<IEnumerable<Ride>> GetRidesAsync(int carrierId, int? driverId, int? vehicleId, int pageSize, int pageCount);
        Task<IEnumerable<RideResult>> GetRidesEvolutionAsync(int carrierId, DateTime from);
        Task<int> LastDaysPassengersAsync(int carrierId, DateTime from);
        Task<int> LastDaysRidesAsync(int carrierId, DateTime from);
        Task UpdateAsync(Ride ride);
    }
}