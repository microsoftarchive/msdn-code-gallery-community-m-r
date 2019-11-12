using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Import_data_from_Excel.Startup))]
namespace Import_data_from_Excel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
