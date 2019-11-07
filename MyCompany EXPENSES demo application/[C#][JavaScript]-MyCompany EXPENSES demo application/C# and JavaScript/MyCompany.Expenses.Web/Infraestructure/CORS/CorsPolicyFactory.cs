
namespace MyCompany.Expenses.Web.CORS
{
    using System.Net.Http;
    using System.Web.Http.Cors;

    /// <summary>
    /// For example, a custom CORS policy provider could read the settings from a configuration file.
    /// As an alternative to using attributes, you can register an ICorsPolicyProviderFactory object that creates ICorsPolicyProvider objects
    /// </summary>
    public class CorsPolicyFactory : ICorsPolicyProviderFactory
    {
        ICorsPolicyProvider _provider = new MyCorsPolicyProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request)
        {
            return _provider;
        }
    } 
}