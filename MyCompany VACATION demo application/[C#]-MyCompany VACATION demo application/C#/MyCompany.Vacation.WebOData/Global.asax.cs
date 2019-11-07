
namespace MyCompany.Vacation.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Model;

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
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyConfig.ResolveDependencies(GlobalConfiguration.Configuration);
            FormatConfig.ConfigureFormats(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            InitializeMappings();
        }

        void InitializeMappings()
        {
            Mapper.CreateMap<VacationRequest, VacationRequestDTO>();
        }
    }
}