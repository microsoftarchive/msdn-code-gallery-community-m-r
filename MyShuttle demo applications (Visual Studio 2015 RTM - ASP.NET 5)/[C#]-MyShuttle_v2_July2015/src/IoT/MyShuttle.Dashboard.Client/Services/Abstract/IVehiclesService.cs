namespace MyShuttle.Dashboard.Client.Services.Abstract
{
    using System.Threading.Tasks;
    using Models;
    using System.Collections.Generic;

    public interface IVehiclesService
    {
        Task<VehicleSummaryResult> GetVehiclesSummaryAsync();
        Task<VehicleDetailResult> GetVehiclesDetailAsync(string deviceId);
        Task<IEnumerable<VehicleAlarmResult>> GetVehicleAlarmsAsync(string deviceId);
        Task<IEnumerable<VehicleMilesYearResult>> GetMilesPerYear(string deviceId, int year);
        Task<IEnumerable<VehicleMilesMonthResult>> GetMilesPerMonth(string deviceId, int year, int month);
    }
}
