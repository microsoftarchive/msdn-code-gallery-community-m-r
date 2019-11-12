namespace MyShuttle.Dashboard.Client.Services.Abstract
{
    using System.Threading.Tasks;
    using Models;

    public interface IDriversService
    {
        Task<TopDriversResult> GetTopRatedDriversAsync(bool withPhoto, int count);

        Task<DriverResult> GetDriverAsync(int driverId);

        Task<DrivingStyleResult> GetDrivingStyleAsync(int driverId);

        Task<StatisticsResult> GetStatisticsAsync(int driverId);
    }
}
