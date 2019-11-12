namespace MyShuttle.Dashboard.Client.Repositories
{
    using System.Threading.Tasks;
    using Models;
    using Abstract;
    using Services.Abstract;
    using System.Collections.Generic;

    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly IVehiclesService _vehiclesService;

        public VehiclesRepository(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        public async Task<VehicleSummaryResult> GetVehiclesDataAsync()
        {
            return await _vehiclesService.GetVehiclesSummaryAsync();
        }

        public async Task<VehicleDetailResult> GetVehicleDetailAsync(string deviceId)
        {
           return await _vehiclesService.GetVehiclesDetailAsync(deviceId);
        }

        public async Task<IEnumerable<VehicleAlarmResult>> GetVehicleAlarmsAsync(string deviceId)
        {
            return await _vehiclesService.GetVehicleAlarmsAsync(deviceId);
        }

        public async Task<IEnumerable<VehicleMilesYearResult>> GetMilesPerYear(string deviceId, int year)
        {
            return await _vehiclesService.GetMilesPerYear(deviceId, year);
        }

        public async Task<IEnumerable<VehicleMilesMonthResult>> GetMilesPerMonth(string deviceId, int year, int month)
        {
            return await _vehiclesService.GetMilesPerMonth(deviceId, year, month);
        }

    }
}
