namespace MyShuttle.Dashboard.Client.Services
{
    using Models;
    using System.Threading.Tasks;
    using Abstract;

    public class ServicesServiceProxy : BaseService, IServicesService
    {
        private readonly string _route = "/api/services";

        public async Task<ServicesSatisfactionResult> GetServicesSatisfactionAsync(int days)
        {
            return await GetAsync<ServicesSatisfactionResult>($"{_route}/satisfaction?Days={days}");
        }
    }
}
