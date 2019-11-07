using System.Web.Mvc;

namespace MyEvents.Api.App_Start
{
    /// <summary>
    /// Add filters
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register MVC filters.
        /// </summary>
        /// <param name="filters">GlobalFilterCollection</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}