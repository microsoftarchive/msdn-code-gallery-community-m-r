using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyEvents.Api.App_Start
{
    /// <summary>
    /// Class to configure MVC Routing
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register the routes for the application
        /// </summary>
        /// <param name="routes">RouteCollection</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapHttpRoute(
                name: "ActionRule",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { action = @"([a-z]+-?)+" }
            );

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "ApiExplorer", action = "Index", eventDefinitionId = UrlParameter.Optional },
                namespaces: new[] { "MyEvents.Web.Controllers" }
            );
        }
    }
}