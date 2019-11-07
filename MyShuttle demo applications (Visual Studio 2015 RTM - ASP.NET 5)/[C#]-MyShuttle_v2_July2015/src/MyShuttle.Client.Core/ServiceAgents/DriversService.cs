namespace MyShuttle.Client.Core.ServiceAgents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyShuttle.Client.Core.DocumentResponse;
    using Web;
    using System.Globalization;

    internal class DriversService : BaseRequest, IDriversService
    {
        public DriversService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        public async Task DeleteAsync(int driverId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}drivers/Delete/{1}", _urlPrefix, driverId);

            await base.DeleteAsync(url);
        }

        public async Task<Driver> GetAsync(int driverId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}drivers/get/{1}", _urlPrefix, driverId);

            return await base.GetAsync<Driver>(url);
        }

        public async Task<IEnumerable<Driver>> GetAsync(string filter, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}drivers/search?filter={1}&pageSize={2}&pageCount={3}", _urlPrefix, filter, pageSize, pageCount);

            return await base.GetIEnumerableAsync<Driver>(url);
        }

        public async Task<int> GetCountAsync(string filter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}drivers/count?filter={1}", _urlPrefix, filter);

            return await base.GetAsync<int>(url);
        }

        public async Task<int> PostAsync(Driver driver)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}drivers/Post", _urlPrefix);

            return await base.PostAsync<int, Driver>(url, driver);
        }

        public async Task PutAsync(Driver driver)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}drivers/Put", _urlPrefix);

            await base.PutAsync<Driver>(url, driver);
        }
    }
}
