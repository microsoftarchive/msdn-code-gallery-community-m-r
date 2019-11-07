namespace MyShuttle.Client.Core.ServiceAgents
{
    using System;
    using System.Threading.Tasks;
    using DocumentResponse;
    using Web;
    using System.Globalization;

    internal class CarriersService : BaseRequest, ICarriersService
    {
        public CarriersService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }
        
        public async Task<Carrier> GetAsync()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}carriers/get", _urlPrefix);

            return await base.GetAsync<Carrier>(url);
        }

        public async Task<int> PostAsync(Carrier carrier)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}carriers/Post", _urlPrefix);

            return await base.PostAsync<int, Carrier>(url, carrier);
        }

        public async Task PutAsync(Carrier carrier)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}carriers/Put", _urlPrefix);

            await base.PutAsync<Carrier>(url, carrier);
        }

    }
}
