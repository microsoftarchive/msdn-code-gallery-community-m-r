
namespace MyCompany.Vacation.Web
{
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Models;
    using System.Web.Http;
    using System.Web.Http.OData.Builder;

    /// <summary>
    /// WebApiConfig
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            RegisterOData(config);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}",
                defaults: null
            );

        }

        private static void RegisterOData(HttpConfiguration config)
        {
            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Employee>("EmployeesOData");
            modelBuilder.EntitySet<Team>("TeamsOData");
            modelBuilder.EntitySet<Office>("OfficesOData");
            modelBuilder.EntitySet<EmployeePicture>("EmployeePicturesOData");
            modelBuilder.EntitySet<Calendar>("CalendarsOData");
            modelBuilder.EntitySet<CalendarHolidays>("CalendarHolidaysOData");
            modelBuilder.EntitySet<VacationRequest>("VacationRequestsOData");

            modelBuilder.Entity<VacationRequest>().Action("AcceptVacation")
                .Parameter<string>("Reason");
            modelBuilder.Entity<VacationRequest>().Action("RejectVacation")
                .Parameter<string>("Reason");


            Microsoft.Data.Edm.IEdmModel model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);

            config.EnableQuerySupport();

        }
    }
}
