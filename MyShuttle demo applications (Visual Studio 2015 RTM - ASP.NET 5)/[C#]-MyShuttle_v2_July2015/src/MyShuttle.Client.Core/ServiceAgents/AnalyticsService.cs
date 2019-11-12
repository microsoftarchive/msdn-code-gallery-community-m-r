namespace MyShuttle.Client.Core.ServiceAgents
{
    using MyShuttle.Client.Core.DocumentResponse;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Web;


    internal class AnalyticsService : BaseRequest, IAnalyticsService
    {
        public AnalyticsService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        public async Task<RidesAnalyticInfo> GetRidesInfoAsync()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}analytics/rides", _urlPrefix);

            return await base.GetAsync<RidesAnalyticInfo>(url);
        }

        public async Task<SummaryAnalyticInfo> GetSummaryInfoAsync()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}analytics/summary", _urlPrefix);

            return await base.GetAsync<SummaryAnalyticInfo>(url);
        }

        public async Task<List<Driver>> GetTopDriversAsync()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}analytics/topdrivers", _urlPrefix);

            return await base.GetAsync<List<Driver>>(url);
        }

        public async Task<List<Vehicle>> GetTopVehiclesAsync()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}analytics/topvehicles", _urlPrefix);

            return await base.GetAsync<List<Vehicle>>(url);
        }
    }
}
