namespace MyCompany.Expenses.Service.Host
{
    using Microsoft.Owin.Security.WindowsAzure;
using Owin;
using System.Web.Http;

    /// <summary>
    /// Startup class to initialize WebAPI routes
    /// </summary>
    class Startup
    {
        /// <summary>
        /// Config WebAPI
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.UseWindowsAzureBearerToken(new WindowsAzureJwtBearerAuthenticationOptions()
            {
                Audience = System.Configuration.ConfigurationManager.AppSettings["Audience"],
                Tenant = System.Configuration.ConfigurationManager.AppSettings["Tenant"],
            });

            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            // Default
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}",
                defaults: null);

            // NoAuth routes are only for DEMOS without internet access
            // this routes are not securized and all the requests are done with a dummy user
            config.Routes.MapHttpRoute(
                name: "DefaultApiNoAuth",
                routeTemplate: "noauth/api/{controller}",
                defaults: null
            );

            config.DependencyResolver = new MyCompany.Expenses.Web.UnityDependencyResolver();

            app.UseWebApi(config);
        }

    }
}
