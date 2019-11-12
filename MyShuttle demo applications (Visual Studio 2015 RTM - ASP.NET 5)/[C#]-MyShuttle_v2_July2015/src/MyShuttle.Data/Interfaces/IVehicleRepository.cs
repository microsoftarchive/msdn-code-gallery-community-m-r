
namespace MyShuttle.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IVehicleRepository
    {
        Task<int> AddAsync(Vehicle vehicle);
        Task DeleteAsync(int vehicleId);
        Task<Vehicle> GetAsync(int vehicleId);
        Task<Vehicle> GetByDeviceIdAsync(string deviceId);
        Task<IEnumerable<Vehicle>> GetByDistanceAsync(double latitude, double longitude, int count);
        Task<IEnumerable<Vehicle>> GetByPriceAsync(double latitude, double longitude, int count);
        Task<int> GetCountAsync(int carrierId, string filter);
        Task<ICollection<Vehicle>> GetTopVehiclesAsync(int carrierId, int numOfDrivers);
        Task<IEnumerable<Vehicle>> GetVehiclesAsync(int carrierId, string filter, int pageSize, int pageCount);
        Task<IEnumerable<Vehicle>> GetVehiclesFilterAsync(int carrierId);
        Task UpdateAsync(Vehicle vehicle);
    }
}