using Auth.DataAccess;
using DataAccessEfCore.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessEfCore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApiEfCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var authConnectionString = Configuration.GetConnectionString("AuthConnection");
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(authConnectionString, optionBuilder => optionBuilder.MigrationsAssembly("WebApiEfCore")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Lockout.MaxFailedAccessAttempts = 10;

                })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddResponseCompression();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var dbConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SkiShopDbContext>(options => 
                options.UseSqlServer(dbConnectionString)
            );

            services.AddDataAccessEfCore();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder
                    .WithOrigins("https://localhost:44315", "http://localhost:58722")
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            app.UseResponseCompression();

            app.UseAuthentication();

            app.UseMvc();

            UserSeed.Seed(app.ApplicationServices).Wait();
        }
    }
}
