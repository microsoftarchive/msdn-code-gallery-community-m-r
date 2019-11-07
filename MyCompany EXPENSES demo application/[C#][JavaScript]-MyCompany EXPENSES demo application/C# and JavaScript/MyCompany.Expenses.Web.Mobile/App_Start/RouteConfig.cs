using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyCompany.Expenses.Web.Mobile
{
    /// <summary>
    /// Route Config
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // NoAuth route is only for demos without internet access
            // this route is not securized and all the requests are done with a dummy user
            routes.MapRoute(
                name: "NoAuth",
                url: "NoAuth/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
