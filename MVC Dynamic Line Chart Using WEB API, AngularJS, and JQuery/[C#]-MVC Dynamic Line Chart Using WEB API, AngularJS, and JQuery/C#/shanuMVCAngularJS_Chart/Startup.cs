using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shanuMVCAngularJS_Chart.Startup))]
namespace shanuMVCAngularJS_Chart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
