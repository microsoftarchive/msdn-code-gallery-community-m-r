
namespace MyShuttle.Web.AppBuilderExtensions
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Authentication.Cookies;

    public static class SecurityExtensions
    {
        public static IApplicationBuilder ConfigureSecurity(this IApplicationBuilder app)
        {
            return app.UseIdentity();
        }

    }
}