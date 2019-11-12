using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(MyShuttle.API.Host.Startup))]

namespace MyShuttle.API.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            IoCConfig.Register(config);
            app.Map("/api", apiApp =>
            {
                apiApp.UseWebApi(config);
            });
        }
    }
}
