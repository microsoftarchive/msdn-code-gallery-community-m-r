
namespace MyCompany.Expenses.WebApiRole
{
    using Microsoft.Owin.Security.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Owin;
    using System.Web.Http;

    // THIS SAMPLE hosts the same WebAPI that MyCompany.Expenses.Web. 
        //  The source code is duplicate to review it easier

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWindowsAzureBearerToken(new WindowsAzureJwtBearerAuthenticationOptions()
            {
                Audience = RoleEnvironment.GetConfigurationSettingValue("Audience"),
                Tenant = RoleEnvironment.GetConfigurationSettingValue("Tenant"),
            });

            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.DependencyResolver = new MyCompany.Expenses.WebApiRole.UnityDependencyResolver();
            config.Filters.Add(new ExceptionHandlingAttribute());

            // Default
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}",
                defaults: null);

            app.UseWebApi(config);
        }

    }
}
