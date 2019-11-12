

namespace MyShuttle.MobileServices
{
    using MyShuttle.MobileServices.Services.Invoices;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }

    }
}