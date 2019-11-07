using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyEvents.Web
{
    /// <summary>
    /// Class to configure the Asp.net Mvc routes.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{eventDefinitionId}",
                defaults: new { controller = "Home", action = "Index", eventDefinitionId = UrlParameter.Optional },
                namespaces: new[] { "MyEvents.Web.Controllers" }
            );
        }
    }
}