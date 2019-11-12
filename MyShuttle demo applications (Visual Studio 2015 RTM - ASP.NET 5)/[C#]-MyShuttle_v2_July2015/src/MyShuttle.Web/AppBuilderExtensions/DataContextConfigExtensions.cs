
namespace MyShuttle.Web.DataContextConfigExtensions
{
    using Microsoft.Data.Entity;
    using Microsoft.Framework.DependencyInjection;
    using Data;
    using Microsoft.Data.Entity.Infrastructure;

    public static class DataContextConfigExtensions
    {
        
        public static EntityFrameworkServicesBuilder AddStore(this EntityFrameworkServicesBuilder services, bool useInMemoryStore)
        {
            // Add EF services to the services container 
            if (useInMemoryStore)
            {
                services.AddInMemoryStore();
            }
            else
            {
                services.AddSqlServer();
            }

            return services;
        }
    }
}