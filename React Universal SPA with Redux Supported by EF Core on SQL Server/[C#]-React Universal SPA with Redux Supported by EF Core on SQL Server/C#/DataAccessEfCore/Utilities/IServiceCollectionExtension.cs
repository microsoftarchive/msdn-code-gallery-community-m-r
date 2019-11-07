using AutoMapper;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.Repositories;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessEfCore.Utilities
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccessEfCore(this IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddScoped(typeof(IGeneralRepo<>), typeof(GeneralRepo<>));

            services.AddScoped<IGeneralRepo<Category>, GeneralRepo<Category>>();

            services.AddScoped<IStyleRepo, StyleRepo>();

            services.AddScoped<ISkisRepo, SkisRepo>();

            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IReviewRepo, ReviewRepo>();

            services.AddScoped<IOrderRepo, OrderRepo>();

            return services;
        }
    }
}
