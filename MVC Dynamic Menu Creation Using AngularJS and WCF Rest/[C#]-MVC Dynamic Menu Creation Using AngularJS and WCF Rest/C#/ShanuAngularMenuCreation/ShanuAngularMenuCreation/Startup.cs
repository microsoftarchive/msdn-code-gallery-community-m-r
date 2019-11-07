using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShanuAngularMenuCreation.Startup))]
namespace ShanuAngularMenuCreation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
