using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pagination.Startup))]
namespace Pagination
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
