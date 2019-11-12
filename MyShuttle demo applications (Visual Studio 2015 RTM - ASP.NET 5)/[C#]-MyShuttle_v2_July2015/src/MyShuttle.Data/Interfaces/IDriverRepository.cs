
namespace MyShuttle.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IDriverRepository
    {
        Task<int> AddAsync(Driver driver);
        Task DeleteAsync(int driverId);
        Task<Driver> GetAsync(int carrierId, int driverId);
        Task<int> GetCountAsync(int carrierId, string filter);
        Task<IEnumerable<Driver>> GetDriversAsync(int carrierId, string filter, int pageSize, int pageCount);
        Task<IEnumerable<Driver>> GetDriversFilterAsync(int carrierId);
        Task<ICollection<Driver>> GetTopDriversAsync(int carrierId, int numOfDrivers);
        Task UpdateAsync(Driver driver);
    }
}