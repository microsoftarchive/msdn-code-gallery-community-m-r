using System.Web;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Web.Authentication;
using MyEvents.Web.Mappers;
using MyEvents.Web.Models;

namespace MyEvents.Web.Controllers
{
    /// <summary>
    /// Event controller
    /// </summary>
    public class EventController : Controller
    {
        private readonly IEventDefinitionRepository _eventsRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMaterialRepository _materialRepository;
        private const string JpegContentType = "image/jpeg";

        /// <summary>
        /// Event controller contructor.
        /// </summary>
        /// <param name="eventsRepository"></param>
        /// <param name="registeredUserRepository"></param>
        /// <param name="sessionRepository"></param>
        /// <param name="materialRepository"></param>
        public EventController(
            IEventDefinitionRepository eventsRepository,
            IRegisteredUserRepository registeredUserRepository,
            ISessionRepository sessionRepository,
            IMaterialRepository materialRepository)
        {
            _eventsRepository = eventsRepository;
            _registeredUserRepository = registeredUserRepository;
            _sessionRepository = sessionRepository;
            _materialRepository = materialRepository;
        }

        /// <summary>
        /// event logo action.
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EventLogo(int eventDefinitionId)
        {
            return File(_eventsRepository.GetEventLogo(eventDefinitionId), JpegContentType);
        }

        /// <summary>
        /// Detail view action
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public ActionResult Detail(MyEventsIdentity identity, int eventDefinitionId)
        {

            var eventDefinition = _eventsRepository.GetById(eventDefinitionId);

            if (null == eventDefinition)
                throw new HttpException("404");

            return View(eventDefinition);
        }

        /// <summary>
        /// Action to register to an event
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [MyEvents.Web.Authentication.Authorize]
        public ActionResult Register(MyEventsIdentity identity, int eventDefinitionId)
        {
            _registeredUserRepository.AddRegisteredUserToEvent(identity.UserId, eventDefinitionId);
            return RedirectToAction("Detail", new { eventDefinitionId = eventDefinitionId });
        }

        /// <summary>
        /// Gets the schedule view for the event
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public ActionResult Schedule(int eventDefinitionId)
        {
            var eventDefinition = _eventsRepository.GetById(eventDefinitionId);
            ViewBag.EventTitle = eventDefinition.Name;
            ViewBag.TimeZoneOffset = eventDefinition.TimeZoneOffset;
            return View(eventDefinitionId);
        }

        /// <summary>
        /// Gets the schedule mobile view for the event
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public ActionResult ScheduleMobile(int eventDefinitionId)
        {
            var eventDefinition = _eventsRepository.GetById(eventDefinitionId);
            return View(eventDefinition);
        }

        /// <summary>
        /// Session detail action
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public ActionResult SessionDetail(int sessionId)
        {
            var session = _sessionRepository.Get(sessionId);
            return View(session);
        }

        /// <summary>
        /// Action that gets the information for the event schedule as json.
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetSheduleInfo(int eventDefinitionId)
        {
            var eventDefinition = _eventsRepository.GetById(eventDefinitionId);

            var mapper = new EventDefinitionToScheduleViewModelMapper();
            var viewModel = mapper.Map(eventDefinition);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action to get the material file.
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMaterial(int materialId)
        {
            var material = _materialRepository.Get(materialId);
            return File(material.Content, material.ContentType, material.Name);
        }
    }
}
