using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasterDetailCRUD.Startup))]
namespace MasterDetailCRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
