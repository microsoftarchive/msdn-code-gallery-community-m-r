
namespace MyShuttle.Client.Core.ServiceAgents
{
    using DocumentResponse;
    using MyShuttle.Client.Core.ServiceAgents.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRidesService : IUpdatableUrl
    {
        Task<Ride> GetAsync(int rideId);

        Task<IEnumerable<Ride>> GetAsync(int? driverId, int? vehicleId, int pageSize, int pageCount);

        Task<int> GetCountAsync(int? driverId, int? vehicleId);

        Task<IEnumerable<Ride>> GetMyRidesAsync(int count);

        Task<IEnumerable<Ride>> GetCompanyRidesAsync(int comanyId);

        Task PutAsync(Ride ride);
    }
}
