using System.Web.Http;

using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

using Owin;

using Newtonsoft.Json.Serialization;

namespace OwinSignalR.Pulse
{
    public class Startup
    {
        #region Private Members
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Public Properties
        public static log4net.ILog Logger
        {
            get
            {
                return _log;
            }
        }
        #endregion

        public void Configuration(
            IAppBuilder app)
        {
            Pulse.Configuration.DependencyInjectionConfiguration.Configure();
            OwinSignalR.Data.Configuration.AutomapperConfiguration.Configure();

            log4net.Config.XmlConfigurator.Configure();

            RegsiterHubs();

            app.UseCors(CorsOptions.AllowAll);

            app.Use<StructureMapOWINMiddleware>();

            app.MapSignalR();

            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            app.UseWebApi(config);
        }

        #region Private Methods
        private void RegsiterHubs()
        {
            GlobalHost.DependencyResolver.Register(typeof(PulseHub), () => new PulseHub());
        }

        private void SetJsonFormatting(
            HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
        #endregion
    }
}
