using AutoMapper;
using DataAccessEfCore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace DataAccessEfCoreTesting
{
    internal static class Configurations
    {
        private static string ConnectionString { get; } = GetConfigurations().GetConnectionString("DefaultConnection");

        private static IConfiguration GetConfigurations()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static SkiShopDbContext GetDbContext()
        {
            var services = new ServiceCollection();

            services.AddDbContext<SkiShopDbContext>(options =>
                options.UseSqlServer(ConnectionString));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<SkiShopDbContext>();
        }

        public static IConfigurationProvider GetMapperProvider()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IConfigurationProvider>();
        }

        public static IMapper GetMapper()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IMapper>();
        }
    }
}
