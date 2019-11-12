using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shanuMVCProfileImage.Startup))]
namespace shanuMVCProfileImage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
