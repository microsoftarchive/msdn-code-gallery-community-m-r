namespace MyShuttle.Dashboard.Client.Repositories.Abstract
{
    using System.Threading.Tasks;
    using Models;
    public interface IServicesRepository
    {
        Task<ServicesSatisfactionResult> GetServicesSatisfaction(int days);
    }
}