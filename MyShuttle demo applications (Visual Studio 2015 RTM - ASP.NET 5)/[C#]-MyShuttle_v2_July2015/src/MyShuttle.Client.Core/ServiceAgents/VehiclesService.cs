namespace MyShuttle.Client.Core.ServiceAgents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DocumentResponse;
    using Web;
    using System.Globalization;

    internal class VehiclesService : BaseRequest, IVehiclesService
    {
        public VehiclesService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        public async Task DeleteAsync(int vehicleId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/Delete/{1}", _urlPrefix, vehicleId);

            await base.DeleteAsync(url);
        }

        public async Task<Vehicle> GetAsync(int vehicleId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/get/{1}", _urlPrefix, vehicleId);

            return await base.GetAsync<Vehicle>(url);
        }

        public async Task<IEnumerable<Vehicle>> GetAsync(string filter, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/search?filter={1}&pageSize={2}&pageCount={3}", _urlPrefix, filter, pageSize, pageCount);

            return await base.GetIEnumerableAsync<Vehicle>(url);
        }

        public async Task<IEnumerable<Vehicle>> GetByDistanceAsync(double latitude, double longitude, int count)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/distance?latitude={1}&longitude={2}&count={3}", _urlPrefix, latitude, longitude, count);

            return await base.GetIEnumerableAsync<Vehicle>(url);
        }

        public async Task<IEnumerable<Vehicle>> GetByPriceAsync(double latitude, double longitude, int count)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/price?latitude={1}&longitude={2}&count={3}", _urlPrefix, latitude, longitude, count);

            return await base.GetIEnumerableAsync<Vehicle>(url);
        }

        public async Task<int> GetCountAsync(string filter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/count?filter={1}", _urlPrefix, filter);

            return await base.GetAsync<int>(url);
        }

        public async Task<int> PostAsync(Vehicle vehicle)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}vehicles/Post", _urlPrefix);

            return await base.PostAsync<int, Vehicle>(url, vehicle);
        }

        public async Task PutAsync(Vehicle vehicle)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}vehicles/Put", _urlPrefix);

            await base.PutAsync<Vehicle>(url, vehicle);
        }
    }
}
