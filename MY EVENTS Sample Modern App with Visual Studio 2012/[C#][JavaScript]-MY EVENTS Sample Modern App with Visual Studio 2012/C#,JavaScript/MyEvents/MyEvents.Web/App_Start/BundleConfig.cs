using System.Web;
using System.Web.Optimization;

namespace MyEvents.Web
{
    /// <summary>
    /// Bundle Config
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the styles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterStyles(BundleCollection bundles)
        {
            var attendeeLessBundle = new Bundle("~/AttendeeStyles/");
            attendeeLessBundle.Include("~/Styles/Attendee/Variables.less");
            attendeeLessBundle.IncludeDirectory("~/Styles/", "*.less", false);
            attendeeLessBundle.Include("~/Styles/Attendee/Global.less");
            attendeeLessBundle.IncludeDirectory("~/Styles/Attendee/Pages", "*.less", false);
            attendeeLessBundle.Transforms.Add(new LessTransform());
            attendeeLessBundle.Transforms.Add(new CssMinify());
            bundles.Add(attendeeLessBundle);

            var organizerLessBundle = new Bundle("~/OrganizerStyles/");
            organizerLessBundle.Include("~/Styles/Organizer/Variables.less");
            organizerLessBundle.IncludeDirectory("~/Styles/", "*.less", false);
            organizerLessBundle.Include("~/Styles/Organizer/Global.less");
            organizerLessBundle.IncludeDirectory("~/Styles/Organizer/Pages", "*.less", false);
            organizerLessBundle.Transforms.Add(new LessTransform());
            organizerLessBundle.Transforms.Add(new CssMinify());
            bundles.Add(organizerLessBundle);


            var mobileCssBundle = new Bundle("~/MobileStyles/");
            mobileCssBundle.Include("~/Styles/Mobile/myevent.mobile.css");
            mobileCssBundle.IncludeDirectory("~/Styles/Mobile/", "*.css", false);
            mobileCssBundle.Transforms.Add(new CssMinify());
            bundles.Add(mobileCssBundle);

        }

        /// <summary>
        /// Registers the scripts.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/ThirdPartyScripts/").Include(
            "~/Scripts/jquery.upload-1.0.2.js",
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/jquery.validate.customValidators.js",
            "~/Scripts/jquery.dotdotdot-1.5.1.js",
            "~/Scripts/moment.js"));

            var appJsBundle = new ScriptBundle("~/AppScripts/");
            appJsBundle.Include("~/Scripts/MyEvents/myEvents.app.js");
            appJsBundle.IncludeDirectory("~/Scripts/MyEvents/Controls", "*.js");
            bundles.Add(appJsBundle);

            bundles.Add(new ScriptBundle("~/Organizer/Event/CreateScripts/").Include(
                    "~/Scripts/MyEvents/Views/Organizer/myEvents.views.organizer.event.create.js"));

            bundles.Add(new ScriptBundle("~/Organizer/Event/EditScripts/").Include(
                "~/Scripts/MyEvents/Views/Organizer/myEvents.views.organizer.event.edit.js"));

            bundles.Add(new ScriptBundle("~/Organizer/AttendeesScripts/").Include(
                "~/Scripts/MyEvents/Views/Organizer/myEvents.views.organizer.attendees.js"));

            bundles.Add(new ScriptBundle("~/Organizer/Session/ManageMaterialsScripts/").Include(
                "~/Scripts/MyEvents/Views/Organizer/myEvents.views.organizer.session.manageMaterials.js"));

            bundles.Add(new ScriptBundle("~/Organizer/Schedule/ManegeSchedule/").Include(
                "~/Scripts/MyEvents/Controls/myEvents.controls.schedule.scheduleControl.js",
                "~/Scripts/MyEvents/Controls/myEvents.controls.schedule.painters.editableSessionPainter.js",
                "~/Scripts/MyEvents/Controls/myEvents.controls.schedule.painters.availableEditableSessionPainter.js"));

            bundles.Add(new ScriptBundle("~/Attendee/Event/DetailScripts/").Include(
                "~/Scripts/MyEvents/Views/Attendee/myEvents.views.attendee.event.detail.js"));

            bundles.Add(new ScriptBundle("~/Attendee/Session/DetailScripts/").Include(
                "~/Scripts/MyEvents/Views/Attendee/myEvents.views.attendee.session.detail.js"));

            bundles.Add(new ScriptBundle("~/Attendee/HomeScripts/").Include(
                "~/Scripts/MyEvents/Views/Attendee/myEvents.views.attendee.home.js"));

            bundles.Add(new ScriptBundle("~/Attendee/Event/Schedule/").Include(
                "~/Scripts/MyEvents/Controls/myEvents.controls.schedule.scheduleControl.js",
                "~/Scripts/MyEvents/Controls/myEvents.controls.schedule.painters.sessionPainter.js",
                "~/Scripts/MyEvents/Views/Attendee/myEvents.views.attendee.event.schedule.js"));
        }
    }
}