using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Model;
using MyEvents.Web.Areas.Organizer.Models;
using MyEvents.Web.Authentication;


namespace MyEvents.Web.Areas.Organizer.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    [MyEvents.Web.Authentication.Authorize]
    public class HomeController : Controller
    {
        readonly IEventDefinitionRepository _eventDefinitionRepository;

        /// <summary>
        /// Home controller constructor.
        /// </summary>
        /// <param name="eventsRepository"></param>
        public HomeController(IEventDefinitionRepository eventsRepository)
        {
            if (eventsRepository == null)
                throw new ArgumentNullException("eventsRepository");

            _eventDefinitionRepository = eventsRepository;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public ActionResult Index(MyEventsIdentity identity)
        {
            IEnumerable<EventDefinition> events = _eventDefinitionRepository.GetByOrganizerId(identity.UserId, string.Empty, int.MaxValue, 0, true);

            var homeViewModel = new HomeViewModel
            {
                Events = events
            };

            return View(homeViewModel);
        }
    }
}
