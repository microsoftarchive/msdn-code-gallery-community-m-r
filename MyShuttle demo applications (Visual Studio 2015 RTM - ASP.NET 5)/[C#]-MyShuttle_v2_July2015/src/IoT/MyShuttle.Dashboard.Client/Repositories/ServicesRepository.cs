namespace MyShuttle.Dashboard.Client.Repositories
{
    using System.Threading.Tasks;
    using Models;
    using Abstract;
    using Services.Abstract;

    public class ServicesRepository : IServicesRepository
    {
        private readonly IServicesService _servicesService;

        public ServicesRepository(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public async Task<ServicesSatisfactionResult> GetServicesSatisfaction(int days)
        {
            var servicesSatisfactionResult = await _servicesService.GetServicesSatisfactionAsync(days);

            return servicesSatisfactionResult;
        }
    }
}
