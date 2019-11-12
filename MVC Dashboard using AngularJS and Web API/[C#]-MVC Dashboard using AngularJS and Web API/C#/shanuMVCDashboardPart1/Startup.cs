using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shanuMVCDashboardPart1.Startup))]
namespace shanuMVCDashboardPart1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
