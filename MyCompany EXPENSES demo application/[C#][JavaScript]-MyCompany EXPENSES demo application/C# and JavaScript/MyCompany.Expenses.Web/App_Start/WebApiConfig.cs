namespace MyCompany.Expenses.Web
{
    using MyCompany.Expenses.Web.CORS;
    using System.Web.Http;
    using System.Web.Http.Cors;


    /// <summary>
    /// WebApiConfig
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // To enable CORS for all Web API controllers in your application, pass an EnableCorsAttribute instance to the EnableCors method:
            // Allow CORS for all origins. (Caution!)
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            //config.SetCorsPolicyProviderFactory(new CorsPolicyFactory());
            //config.EnableCors();

            config.MapHttpAttributeRoutes();

            // Default
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}",
                defaults: null);

            // NoAuth routes are only for DEMOS without internet access
            // this routes are not securized and all the requests are done with a dummy user
            config.Routes.MapHttpRoute(
                name: "DefaultApiNoAuth",
                routeTemplate: "noauth/api/{controller}",
                defaults: null
            );
        }
    }
}
