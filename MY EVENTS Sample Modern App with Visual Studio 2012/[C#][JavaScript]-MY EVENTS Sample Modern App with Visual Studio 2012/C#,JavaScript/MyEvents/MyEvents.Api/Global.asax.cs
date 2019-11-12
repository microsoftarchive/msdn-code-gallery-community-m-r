using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using MyEvents.Api.App_Start;
using MyEvents.Api.Controllers;
using MyEvents.Api.IoC;
using MyEvents.Api.Formatters;

namespace MyEvents.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// Web api application class.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The application start method.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            TraceConfig.RegisterListener(GlobalConfiguration.Configuration);
            FormatConfig.ConfigureFormats(GlobalConfiguration.Configuration);
            DependencyConfig.ResolveDependencies(GlobalConfiguration.Configuration);
            DocumentationConfig.Configure(GlobalConfiguration.Configuration);

            // Ensure that your Web API is only accessed via HTTPS
            // GlobalConfiguration.Configuration.Filters.Add(new MyEvents.Api.Authentication.OnlyHttpsAttribute());
        }
    }
}