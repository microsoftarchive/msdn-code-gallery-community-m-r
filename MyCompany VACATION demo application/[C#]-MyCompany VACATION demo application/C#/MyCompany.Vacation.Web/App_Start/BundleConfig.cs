
namespace MyCompany.Vacation.Web
{
    using System.Web.Optimization;

    /// <summary>
    /// Bundle Config
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register bundles
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/vendor")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.dotdotdot-{version}.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/knockout-{version}.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/Scripts/knockout.validation.js")
                .Include("~/Scripts/knockout.custom-bindings.js")
                .Include("~/Scripts/sammy-{version}.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/moment.en-gb.js")
                .Include("~/Scripts/jquery.signalR-{version}.js")
                .Include("~/Scripts/toastr.js")
                );


            bundles.Add(new ScriptBundle("~/scripts/modernizr")
                .Include("~/Scripts/modernizr-*")
                );

            bundles.Add(new StyleBundle("~/Content/styles")
               .Include("~/Content/normalize.css")
               .Include("~/Content/ie10mobile.css")
               .Include("~/Content/durandal.css")
               .Include("~/Content/toastr.css")
               .Include("~/Content/site.css")
               .Include("~/Content/list.css")
               .Include("~/Content/teamVacations.css")
               .Include("~/Content/myCalendar.css")
               .Include("~/Content/overlaps.css")
               .Include("~/Content/messageBox.css")
               );

            bundles.Add(new StyleBundle("~/Content/bootstrap/css")
                .Include("~/Content/bootstrap/bootstrap.css")
                );
        }
    }
}