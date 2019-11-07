using StructureMap;

namespace OwinSignalR.Pulse.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void Configure()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<Data.Models.IDataContext>().HybridHttpOrThreadLocalScoped().Use<Data.Models.DataContext>();
                
                x.For<Data.Services.IApplicationService>().Use<Data.Services.ApplicationService>();
                x.For<Data.DataAccessors.IApplicationDataAccessor>().Use<Data.DataAccessors.ApplicationDataAccessor>();

                x.SetAllProperties(y =>
                {   
                    y.OfType<Data.Services.IApplicationService>();
                    y.OfType<Data.DataAccessors.IApplicationDataAccessor>();                                        
                });
            });
        }
    }
}
