using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoLoginApp.Startup))]
namespace DemoLoginApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
