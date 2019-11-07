
namespace MyShuttle.Client.Core.ServiceAgents
{
    using DocumentResponse;
    using MyShuttle.Client.Core.ServiceAgents.Interfaces;
    using System.Threading.Tasks;

    public interface ICarriersService : IUpdatableUrl
    {
        Task<Carrier> GetAsync();

        Task<int> PostAsync(Carrier carrier);

        Task PutAsync(Carrier carrier);
    }
}
