namespace MyShuttle.Dashboard.Client.Services.Abstract
{
    using System.Threading.Tasks;
    using Models;

    public interface IServicesService
    {
        Task<ServicesSatisfactionResult> GetServicesSatisfactionAsync(int days);
    }
}
