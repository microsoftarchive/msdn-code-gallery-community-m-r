using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shanuWebAPICRUDSP.Startup))]
namespace shanuWebAPICRUDSP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
