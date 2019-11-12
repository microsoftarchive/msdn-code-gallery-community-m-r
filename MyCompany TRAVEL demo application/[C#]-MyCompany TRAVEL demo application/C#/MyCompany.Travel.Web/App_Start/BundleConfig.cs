
namespace MyCompany.Travel.Web
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
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/bootstrap-datepicker.js")
                .Include("~/Scripts/knockout-{version}.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/Scripts/knockout.validation.js")
                .Include("~/Scripts/sammy-{version}.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/jquery.dotdotdot-{version}.js")
                .Include("~/Scripts/knockout-custom-bindings.js")
                .Include("~/Scripts/jquery.signalR-{version}.js")
                .Include("~/Scripts/toastr.js")
                );

            bundles.Add(new ScriptBundle("~/scripts/modernizr")
                .Include("~/Scripts/modernizr-*")
                );

            bundles.Add(new StyleBundle("~/Content/styles")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/site.css")
                .Include("~/Content/list.css")
                .Include("~/Content/messageBox.css")
                .Include("~/Content/calendar.css")
                .Include("~/Content/travels.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-datepicker.css")
                .Include("~/Content/map.css")
                );

            bundles.Add(new StyleBundle("~/Content/css-mobile")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/messageBox.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-datepicker.css")
                .Include("~/Content/site-mobile.css")
                .Include("~/Content/list-mobile.css")
            );

            bundles.Add(new ScriptBundle("~/scripts/faqvendor")
                 .Include("~/Scripts/jquery-{version}.js")
            );

            bundles.Add(new ScriptBundle("~/scripts/mainLayout")
                 .Include("~/Scripts/mainLayout.js")
            );

            bundles.Add(new ScriptBundle("~/scripts/bootstrap")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/faqcss")
                .Include("~/Content/toastr.css")
                .Include("~/Content/bootstrap/bootstrap.css")
                .Include( "~/Content/site.css")
            );
        }
    }
}