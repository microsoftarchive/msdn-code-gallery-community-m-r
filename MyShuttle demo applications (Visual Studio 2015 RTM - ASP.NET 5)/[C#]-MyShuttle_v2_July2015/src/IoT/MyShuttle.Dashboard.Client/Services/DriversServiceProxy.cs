namespace MyShuttle.Dashboard.Client.Services
{
    using Models;
    using System.Threading.Tasks;
    using Abstract;
    
    public class DriversServiceProxy : BaseService, IDriversService
    {
        private readonly string _route = "/api/drivers";

        public async Task<TopDriversResult> GetTopRatedDriversAsync(bool withPhoto, int count)
        {
            return await GetAsync<TopDriversResult>($"{_route}/top?withPhoto={withPhoto}&count={count}");
        }

        public async Task<DriverResult> GetDriverAsync(int driverId)
        {
            return await GetAsync<DriverResult>($"{_route}/{driverId}/card");
        }

        public async Task<StatisticsResult> GetStatisticsAsync(int driverId)
        {
            return await GetAsync<StatisticsResult>($"{_route}/{driverId}/statistics");
        }

        public async Task<DrivingStyleResult> GetDrivingStyleAsync(int driverId)
        {
            return await GetAsync<DrivingStyleResult>($"{_route}/{driverId}/style");
        }
    }
}
