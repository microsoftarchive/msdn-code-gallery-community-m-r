using System.Web.Http;
using MyEvents.Api.IoC;

namespace MyEvents.Api.App_Start
{
    /// <summary>
    /// Resolve dependencies
    /// </summary>
    public class DependencyConfig
    {
        /// <summary>
        /// Register trace listener
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void ResolveDependencies(HttpConfiguration config)
        {
            config.DependencyResolver = new UnityDependencyResolver();
        }
    }
}