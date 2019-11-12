using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyCompany.Expenses.Web.Mobile
{
    /// <summary>
    /// Web Api Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit: http://go.microsoft.com/fwlink/?LinkId=301869
            //config.EnableQuerySupport();
        }
    }
}