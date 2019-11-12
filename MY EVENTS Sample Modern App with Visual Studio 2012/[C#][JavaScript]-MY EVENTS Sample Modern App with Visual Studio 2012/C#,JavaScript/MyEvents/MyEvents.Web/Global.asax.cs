using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.WebPages;
using MyEvents.Web.Authentication;
using MyEvents.Web.Binders;
using MyEvents.Web.Controllers;
using MyEvents.Web.DisplayModes;
using MyEvents.Web.Models;
using Microsoft.Practices.Unity;

namespace MyEvents.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// The mvc application class.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The application start method.
        /// </summary>
        protected void Application_Start()
        {
            DisplayModeProvider.Instance.Modes.Insert(0, new MobileDisplayMode());

            AreaRegistration.RegisterAllAreas();
            
            var unityContainer = new UnityContainer();
            var resolver = new UnityDependencyResolver(unityContainer);
            DependencyResolver.SetResolver(resolver);

            ModelBinderConfig.RegisterModelBinders(unityContainer);
            FilterConfig.RegisterFilterProviders(unityContainer);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, unityContainer);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterStyles(BundleTable.Bundles);
            BundleConfig.RegisterScripts(BundleTable.Bundles);
        }
    }
}