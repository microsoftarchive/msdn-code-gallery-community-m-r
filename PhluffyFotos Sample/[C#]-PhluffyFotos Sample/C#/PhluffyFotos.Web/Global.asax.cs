namespace PhluffyFotos.Web
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using Microsoft.WindowsAzure;
    using PhluffyFotos.Data.WindowsAzure;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");

            routes.MapRoute(
                 "Home",
                 string.Empty,
                 new { controller = "Home", action = "Index", });

            routes.MapRoute(
                 "Albums",
                 "Albums",
                 new { controller = "Album", action = "Index", });

            routes.MapRoute(
                "Search",
                "Search/{action}",
                new { controller = "Search", action = "Search" });

            routes.MapRoute(
                "Account",
                "Account/{action}",
                new { controller = "Account", action = "LogOn" });

            routes.MapRoute(
                "Upload",
                "Upload",
                new { controller = "Album", action = "Upload" });

            routes.MapRoute(
                "Delete",
                "Delete/{owner}/{album}/{photoId}",
                new { controller = "Photo", action = "Delete" });

            routes.MapRoute(
                "DeleteAlbum",
                "DeleteAlbum/{owner}/{album}",
                new { controller = "Album", action = "Delete" });

            routes.MapRoute(
                "CreateAlbum",
                "Album/Create",
                new { controller = "Album", action = "Create" });

            routes.MapRoute(
                "MyAlbums",
                "{owner}",
                new { controller = "Album", action = "MyAlbums" });

            routes.MapRoute(
                "UserAlbums",
                "{owner}/{album}",
                new { controller = "Album", action = "Get", album = (string)null });

            routes.MapRoute(
                "Picture",
                "{owner}/{album}/{photoId}",
                new { controller = "Photo", action = "Index" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Album", action = "Index", id = (string)null });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.EnableDefaultBundles();

            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                configSetter(ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            });
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            FirstRequestInitialization.Initialize();
        }

        internal class FirstRequestInitialization
        {
            private static bool initializedAlready = false;
            private static object initializationLock = new object();

            public static void Initialize()
            {
                if (initializedAlready)
                {
                    return;
                }

                lock (initializationLock)
                {
                    if (initializedAlready)
                    {
                        return;
                    }

                    ApplicationStartUponFirstRequest();
                    initializedAlready = true;
                }
            }

            private static void ApplicationStartUponFirstRequest()
            {
                var userName = ConfigurationManager.AppSettings["DefaultAdminRoleUser"];
                var userPass = ConfigurationManager.AppSettings["DefaultAdminRolePass"];

                // We need to check if this is the first launch of the app and pre-create
                // the admin role and the first user to be admin (still needs to register).
                if (!Roles.GetAllRoles().Contains("Administrator"))
                {
                    Roles.CreateRole("Administrator");
                }

                // make sure the admin user exists
                if (Membership.GetUser(userName) == null)
                {
                    Membership.CreateUser(userName, userPass);
                }

                // add the user to the admin role
                if (!Roles.GetUsersInRole("Administrator").Any() && Membership.GetUser(userName) != null)
                {
                    Roles.AddUserToRole(userName, "Administrator");
                }
            }
        }
    }   
}