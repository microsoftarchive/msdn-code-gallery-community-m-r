using System;
using System.IO;
using System.Linq;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Builder;

namespace Visitors
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration()
                .AddIniFile("config.ini")
                .AddEnvironmentVariables();

            app.UseServices(services =>
            {
                services.AddMvc();
                services.AddTransient<ISecurityHelper, DemoSecurityHelper>();
                services.AddInstance<IConfiguration>(configuration);
                services.AddEntityFramework()
                        .AddSqlServer();

                services.AddTransient<VisitorContext>();
            });

            app.UseErrorPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(null, "noauth/api/{Controller}/current/{pictureType}");
                routes.MapRoute(null, "noauth/api/{Controller}");
                routes.MapRoute(null, "{Controller}/{action}", new { controller = "Home", action = "Index" });
            });

            var ctx = app.ApplicationServices.GetService<VisitorContext>();
            //ctx.Database.Create();

        }
    }
}
