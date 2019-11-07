
namespace MyCompany.Expenses.Web.CORS
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Cors;
    using System.Web.Http.Cors;

    /// <summary>
    /// The [EnableCors] attribute implements the ICorsPolicyProvider interface. 
    /// You can provide your own implementation by creating a class that derives from Attribute and implements ICorsProlicyProvider
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class MyCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyCorsPolicyAttribute()
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                AllowAnyOrigin = false
            };

            // loads the origins from AppSettings
            string originsString = WebConfigurationManager.AppSettings["CORS:origins"];
            if (!String.IsNullOrEmpty(originsString))
            {
                if (originsString == "*")
                {
                    _policy.AllowAnyOrigin = true;
                }
                else
                {
                    foreach (var origin in originsString.Split(','))
                    {
                        _policy.Origins.Add(origin);
                    }
                }
            }

        }

        /// <summary>
        /// Get Cors Policy
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}