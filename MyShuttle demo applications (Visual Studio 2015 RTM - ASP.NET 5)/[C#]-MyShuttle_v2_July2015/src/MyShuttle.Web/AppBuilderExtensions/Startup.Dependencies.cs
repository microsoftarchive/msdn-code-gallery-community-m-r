
namespace MyShuttle.Web.AppBuilderExtensions
{
    using API;
    using Data;
    using Microsoft.Framework.DependencyInjection;

    public static class DependenciesExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICarrierRepository, CarrierRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRidesRepository, RidesRepository>();
            services.AddScoped<MyShuttleSecurityContext>();

            return services;
        }
    }
}