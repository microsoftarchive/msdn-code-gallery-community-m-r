using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelloWorldDotnetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//Mvc related files
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();
            //app.UseDefaultFiles();
            //app.UseStaticFiles(); // For the wwwroot folder

            //Customised startup page
            DefaultFilesOptions DefaultFile = new DefaultFilesOptions();
            DefaultFile.DefaultFileNames.Clear();
            DefaultFile.DefaultFileNames.Add("Welcome.html");
            app.UseDefaultFiles(DefaultFile);
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync(" Hello World !!");
                await next.Invoke();
            });
            
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(" Welcome to Dotnet Core !!");
                
            });
            

        }
    }
}
