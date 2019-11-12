namespace MyShuttle.Client.Core.ServiceAgents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DocumentResponse;
    using Web;
    using System.Globalization;

    internal class RidesService : BaseRequest, IRidesService
    {
        public RidesService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        public async Task<Ride> GetAsync(int rideId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}rides/get/{1}", _urlPrefix, rideId);

            return await base.GetAsync<Ride>(url);
        }

        public async Task<IEnumerable<Ride>> GetAsync(int? driverId, int? vehicleId, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}rides/search?driverId={1}&vehicleId={2}&pageSize={3}&pageCount={4}", _urlPrefix, driverId, vehicleId, pageSize, pageCount);

            return await base.GetIEnumerableAsync<Ride>(url);
        }

        public async Task<int> GetCountAsync(int? driverId, int? vehicleId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}rides/count?driverId={1}&vehicleId={2}", _urlPrefix, driverId, vehicleId);

            return await base.GetAsync<int>(url);
        }

        public async Task<IEnumerable<Ride>> GetMyRidesAsync(int count)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}rides/myrides?count={1}", _urlPrefix, count);

            return await base.GetIEnumerableAsync<Ride>(url);
        }
        public async Task<IEnumerable<Ride>> GetCompanyRidesAsync(int comanyId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}rides/mycompanyrides?count={1}", _urlPrefix, comanyId);

            return await base.GetIEnumerableAsync<Ride>(url);
        }

        public async Task PutAsync(Ride ride)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}rides/Put", _urlPrefix);

            await base.PutAsync<Ride>(url, ride);
        }
    }
}
