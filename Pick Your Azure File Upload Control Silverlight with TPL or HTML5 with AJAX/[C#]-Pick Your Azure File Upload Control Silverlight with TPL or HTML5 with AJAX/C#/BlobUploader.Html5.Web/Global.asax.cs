//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
[assembly: System.CLSCompliant(true), System.Resources.NeutralResourcesLanguage("en")]
namespace BlobUploader.Html5.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Handles application level events.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if (filters != null)
            {
                filters.Add(new HandleErrorAttribute());
            }
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// Application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}