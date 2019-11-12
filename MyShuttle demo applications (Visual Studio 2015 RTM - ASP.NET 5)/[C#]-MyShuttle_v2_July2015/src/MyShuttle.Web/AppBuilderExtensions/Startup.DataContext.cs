
namespace MyShuttle.Web.AppBuilderExtensions
{
    using Data;
    using DataContextConfigExtensions;
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Data.Entity;
    using System;

    public static class DataContextExtensions
    {
        public static IServiceCollection ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var runningOnMono = Type.GetType("Mono.Runtime") != null;
            bool useInMemoryStore = runningOnMono || configuration["Data:UseInMemoryStore"].Equals("true", StringComparison.OrdinalIgnoreCase);

            services.AddEntityFramework()
                    .AddStore(useInMemoryStore)
                    .AddDbContext<MyShuttleContext>(options =>
                    {
                        if (useInMemoryStore)
                        {
                            options.UseInMemoryStore();
                        }
                        else
                        {
                            options.UseSqlServer(configuration.Get("Data:DefaultConnection:Connectionstring"));
                        }
                    });

            return services;
        }


    }
}