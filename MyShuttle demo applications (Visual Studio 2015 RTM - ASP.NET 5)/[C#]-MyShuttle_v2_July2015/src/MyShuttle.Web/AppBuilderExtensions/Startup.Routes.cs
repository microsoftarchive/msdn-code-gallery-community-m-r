
namespace MyShuttle.Web.AppBuilderExtensions
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Routing;

    public static class RouteExtensions
    {

        public static IApplicationBuilder ConfigureRoutes(this IApplicationBuilder app)
        {
            return app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}");
            });
        }
    }
}