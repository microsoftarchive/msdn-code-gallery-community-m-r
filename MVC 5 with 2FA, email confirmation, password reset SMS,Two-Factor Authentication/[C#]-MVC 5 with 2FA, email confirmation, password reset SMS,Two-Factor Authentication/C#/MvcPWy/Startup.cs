using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcPWy.Startup))]
namespace MvcPWy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
