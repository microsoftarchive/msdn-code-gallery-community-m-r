namespace MyCompany.Expenses.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Model;
    using System;
    using System.Web;

    /// <summary>
    /// MvcApplication
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_Start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            IdentityConfig.ConfigureIdentity();
            DependencyConfig.ResolveDependencies(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FormatConfig.ConfigureFormats(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            InitializeMappings();

        }

        /// <summary>
        /// Application_BeginRequest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");

                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization, Accept");

                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }

        void InitializeMappings()
        {
            Mapper.CreateMap<Expense, ExpenseDTO>();
        }
    }
}