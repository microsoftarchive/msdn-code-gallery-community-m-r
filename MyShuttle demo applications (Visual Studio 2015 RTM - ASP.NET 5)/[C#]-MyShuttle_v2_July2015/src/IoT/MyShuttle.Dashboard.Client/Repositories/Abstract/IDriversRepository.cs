namespace MyShuttle.Dashboard.Client.Repositories.Abstract
{
    using System.Threading.Tasks;
    using Models;

    public interface IDriversRepository
    {
        Task<TopDriversResult> GetTopRatedDriversAsync(bool withPhoto, int count);

        Task<DriverResult> GetDriverAsync(int driverId);

        Task<DrivingStyleResult> GetDrivingStyleAsync(int driverId);

        Task<StatisticsResult> GetStatisticsAsync(int driverId);
    }
}