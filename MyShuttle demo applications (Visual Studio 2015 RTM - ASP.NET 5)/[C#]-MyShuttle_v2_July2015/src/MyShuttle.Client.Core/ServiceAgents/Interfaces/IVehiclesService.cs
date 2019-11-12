
namespace MyShuttle.Client.Core.ServiceAgents
{
    using DocumentResponse;
    using MyShuttle.Client.Core.ServiceAgents.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVehiclesService : IUpdatableUrl
    {
        Task<Vehicle> GetAsync(int vehicleId);

        Task<IEnumerable<Vehicle>> GetAsync(string filter, int pageSize, int pageCount);

        Task<int> GetCountAsync(string filter);

        Task<int> PostAsync(Vehicle vehicle);

        Task PutAsync(Vehicle vehicle);

        Task DeleteAsync(int vehicleId);

        Task<IEnumerable<Vehicle>> GetByPriceAsync(double latitude, double longitude, int count);

        Task<IEnumerable<Vehicle>> GetByDistanceAsync(double latitude, double longitude, int count);
    }
}
