using System.Web.Mvc;
using System.Linq;
using MyEvents.Web.Authentication;
using Microsoft.Practices.Unity;

namespace MyEvents.Web
{
    /// <summary>
    /// Class to configure the Asp.net mvc fiters.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the filter providers.
        /// </summary>
        /// <param name="unityContainer"></param>
        public static void RegisterFilterProviders(IUnityContainer unityContainer) {
            var defaultProvider = FilterProviders.Providers.Single(fp => fp is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(defaultProvider);
            
            var provider = new FilterAttributeProvider(unityContainer);
            FilterProviders.Providers.Add(provider);
        }

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="unityContainer"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IUnityContainer unityContainer)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(unityContainer.Resolve<OauthCodeProcessAttribute>(), (int)FilterScope.First);
        }
    }
}