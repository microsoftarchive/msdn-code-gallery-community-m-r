
namespace MyShuttle.Client.Core.ServiceAgents
{
    using MyShuttle.Client.Core.DocumentResponse;
    using MyShuttle.Client.Core.ServiceAgents.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IAnalyticsService : IUpdatableUrl
    {
        Task<List<Driver>> GetTopDriversAsync();

        Task<List<Vehicle>> GetTopVehiclesAsync();

        Task<SummaryAnalyticInfo> GetSummaryInfoAsync();

        Task<RidesAnalyticInfo> GetRidesInfoAsync();
    }
}
