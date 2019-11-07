
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyShuttle.JobsAPI.Startup))]

namespace MyShuttle.JobsAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
