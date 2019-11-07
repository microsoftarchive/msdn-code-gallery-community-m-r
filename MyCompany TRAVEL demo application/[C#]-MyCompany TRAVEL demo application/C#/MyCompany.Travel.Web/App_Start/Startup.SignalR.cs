
namespace MyCompany.Travel.Web
{
    using Microsoft.Owin.Security.WindowsAzure;
    using Owin;
    using System.Configuration;

    /// <summary>
    /// Configure SignalR
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initialize SignalR
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}