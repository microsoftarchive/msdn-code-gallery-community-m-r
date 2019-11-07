using System.Web.Mvc;

namespace MyEvents.Web.Areas.Organizer
{
    /// <summary>
    /// Organizer area registration class.
    /// </summary>
    public class OrganizerAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// The area name.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "Organizer";
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Organizer_default",
                "Organizer/{controller}/{action}/{eventDefinitionId}",
                new { action = "Index", eventDefinitionId = UrlParameter.Optional },
                namespaces: new[] { "MyEvents.Web.Areas.Organizer.Controllers" }
            );
        }
    }
}
