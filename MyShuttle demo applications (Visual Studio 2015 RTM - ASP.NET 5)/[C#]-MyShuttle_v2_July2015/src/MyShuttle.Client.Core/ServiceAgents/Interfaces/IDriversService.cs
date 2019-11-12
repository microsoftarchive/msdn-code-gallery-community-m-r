
namespace MyShuttle.Client.Core.ServiceAgents
{
    using DocumentResponse;
    using MyShuttle.Client.Core.ServiceAgents.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDriversService : IUpdatableUrl
    {
        Task<Driver> GetAsync(int driverId);

        Task<IEnumerable<Driver>> GetAsync(string filter, int pageSize, int pageCount);

        Task<int> GetCountAsync(string filter);

        Task<int> PostAsync(Driver driver);

        Task PutAsync(Driver driver);

        Task DeleteAsync(int driverId);
    }
}
