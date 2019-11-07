using System.Web.Http;
using System.Web.Mvc;

namespace MyCompany.Expenses.Web.Areas.HelpPage
{
    /// <summary>
    /// Help page area registration
    /// </summary>
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Area name
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        /// <summary>
        /// Register area
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}