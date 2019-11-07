
namespace MyShuttle.Client.Core.ServiceAgents
{
    using DocumentResponse;
    using MyShuttle.Client.Core.ServiceAgents.Interfaces;
    using System.Threading.Tasks;

    public interface IEmployeesService : IUpdatableUrl
    {
        Task<Employee> GetMyProfileAsync();
    }
}
