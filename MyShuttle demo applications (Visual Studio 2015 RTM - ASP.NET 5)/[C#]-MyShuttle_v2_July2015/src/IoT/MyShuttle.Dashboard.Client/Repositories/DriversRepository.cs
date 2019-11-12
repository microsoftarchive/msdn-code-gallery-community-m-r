namespace MyShuttle.Dashboard.Client.Repositories
{
    using System.Threading.Tasks;
    using Models;
    using Abstract;
    using Services.Abstract;

    public class DriversRepository : IDriversRepository
    {
        private readonly IDriversService _driversService;

        public DriversRepository(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<TopDriversResult> GetTopRatedDriversAsync(bool withPhoto, int count)
        {
            var drivers = await _driversService.GetTopRatedDriversAsync(withPhoto, count);

            var index = 0;

            foreach (var item in drivers.Items)
            {
                item.Index = index++;
            }

            return drivers;
        }

        public async Task<DriverResult> GetDriverAsync(int driverId)
        {
            var driver = await _driversService.GetDriverAsync(driverId);

            return driver;
        }

        public async Task<DrivingStyleResult> GetDrivingStyleAsync(int driverId)
        {
            var drivingStyle = await _driversService.GetDrivingStyleAsync(driverId);

            return drivingStyle;
        }

        public async Task<StatisticsResult> GetStatisticsAsync(int driverId)
        {
            var statistics = await _driversService.GetStatisticsAsync(driverId);

            return statistics;
        }
    }
}
