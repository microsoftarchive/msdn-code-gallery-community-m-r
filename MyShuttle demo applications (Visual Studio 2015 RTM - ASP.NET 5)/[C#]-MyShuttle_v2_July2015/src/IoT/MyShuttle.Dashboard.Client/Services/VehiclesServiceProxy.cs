namespace MyShuttle.Dashboard.Client.Services
{
    using Models;
    using System.Threading.Tasks;
    using Abstract;
    using System.Collections.Generic;

    public class VehiclesServiceProxy : BaseService, IVehiclesService
    {
        private readonly string _route = "/api/vehicles";

        public async Task<VehicleSummaryResult> GetVehiclesSummaryAsync()
        {
            // HACK C# 6 Feature - String interpolation
            return await GetAsync<VehicleSummaryResult>($"{_route}/summary");
        }

        public async Task<VehicleDetailResult> GetVehiclesDetailAsync(string deviceId)
        {
            return await GetAsync<VehicleDetailResult>($"{_route}/{deviceId}/details");
        }

        public async Task<IEnumerable<VehicleAlarmResult>> GetVehicleAlarmsAsync(string deviceId)
        {
            return await GetAsync<IEnumerable<VehicleAlarmResult>>($"{_route}/{deviceId}/alarms");
        }

        public async Task<IEnumerable<VehicleMilesYearResult>> GetMilesPerYear(string deviceId, int year)
        {
            return await GetAsync<IEnumerable<VehicleMilesYearResult>>($"{_route}/{deviceId}/miles/{year}");
        }

        public async Task<IEnumerable<VehicleMilesMonthResult>> GetMilesPerMonth(string deviceId, int year, int month)
        {
            return await GetAsync<IEnumerable<VehicleMilesMonthResult>>($"{_route}/{deviceId}/miles/{year}/{month}");
        }
    }
}
