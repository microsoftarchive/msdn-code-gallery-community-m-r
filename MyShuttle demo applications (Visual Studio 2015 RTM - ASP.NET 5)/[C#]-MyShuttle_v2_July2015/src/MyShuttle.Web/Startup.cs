
namespace MyShuttle.Web
{
    using Microsoft.Framework.Caching.Memory;
    using AppBuilderExtensions;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Diagnostics;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;
    using Model;
    using Data;
    using Microsoft.Framework.Runtime;

    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureDataContext(Configuration);

            // Register MyShuttle dependencies
            services.ConfigureDependencies();

            //Add Identity services to the services container
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyShuttleContext>()
                .AddDefaultTokenProviders();

            CookieServiceCollectionExtensions.ConfigureCookieAuthentication(services, options =>
            {
                options.LoginPath = new Microsoft.AspNet.Http.PathString("/Carrier/Login");
            });

            // Add MVC services to the services container
            services.AddMvc();

            services
                .AddSignalR(options =>
                {
                    options.Hubs.EnableDetailedErrors = true;
                });

            //Add InMemoryCache
            services.AddSingleton<IMemoryCache, MemoryCache>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Add static files to the request pipeline
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline
            app.UseIdentity();

            /* Error page middleware displays a nice formatted HTML page for any unhandled exceptions in the request pipeline.
             * Note: ErrorPageOptions.ShowAll to be used only at development time. Not recommended for production.
             */
            app.UseErrorPage(ErrorPageOptions.ShowAll);

            app.ConfigureSecurity();

            //Configure SignalR
            app.UseSignalR();

            // Add MVC to the request pipeline
            app.ConfigureRoutes();

            MyShuttleDataInitializer.InitializeDatabaseAsync(app.ApplicationServices).Wait();

        }
    }

}
