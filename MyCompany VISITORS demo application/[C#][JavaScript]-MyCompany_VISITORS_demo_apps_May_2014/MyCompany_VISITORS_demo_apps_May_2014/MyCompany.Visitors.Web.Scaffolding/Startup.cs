using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCompany.Visitors.Web.Scaffolding.Startup))]
namespace MyCompany.Visitors.Web.Scaffolding
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
