using OwinSignalR.Common.Dto;
using OwinSignalR.Data.DataAccessors;

using AutoMapper;

namespace OwinSignalR.Data.Services
{
    public interface IApplicationService 
    {
        ApplicationDto FetchApplication(string apiToken);
    }

    public class ApplicationService
        : IApplicationService
    {
        public IApplicationDataAccessor ApplicationDataAccessor { get; set; }        

        public ApplicationDto FetchApplication(
            string apiToken)
        {
            return Mapper.Map<ApplicationDto>(ApplicationDataAccessor.FetchApplication(apiToken));
        }
    }
}
