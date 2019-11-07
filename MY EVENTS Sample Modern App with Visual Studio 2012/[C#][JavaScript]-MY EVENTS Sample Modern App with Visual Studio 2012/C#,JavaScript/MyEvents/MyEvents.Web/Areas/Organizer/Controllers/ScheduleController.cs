using System;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Web.Mappers;
using MyEvents.Web.Authentication;
using System.Web;
using MyEvents.Model;
using MyEvents.Web.Services;

namespace MyEvents.Web.Areas.Organizer.Controllers
{
    /// <summary>
    /// The schedule controller.
    /// </summary>
    [MyEvents.Web.Authentication.Authorize]
    public class ScheduleController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IEventDefinitionRepository _eventDefinitionRepository;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Schedule controller constructor.
        /// </summary>
        public ScheduleController(ISessionRepository sessionRepository, IEventDefinitionRepository eventDefinitionRepository, IAuthorizationService authorizationService)
        {
            _sessionRepository = sessionRepository;
            _eventDefinitionRepository = eventDefinitionRepository;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public ActionResult Index(int eventDefinitionId)
        {
            return View(eventDefinitionId);
        }

        /// <summary>
        /// Gets the shedule information as json.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetSheduleInfo(MyEventsIdentity identity, int eventDefinitionId)
        {
            var eventDefinition = _eventDefinitionRepository.GetById(eventDefinitionId);
            
            _authorizationService.ValidateEventAuthorization(identity, eventDefinition);

            var mapper = new EventDefinitionToScheduleViewModelMapper();
            var viewModel = mapper.Map(eventDefinition);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the session.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="sessionId"></param>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <param name="roomNumber"></param>
        [HttpPost]
        public void UpdateSession(MyEventsIdentity identity, int sessionId, DateTime startTime, int duration, int roomNumber)
        {
            var session = _sessionRepository.Get(sessionId);

            _authorizationService.ValidateSessionAuthorization(identity, session);

            session.StartTime = startTime;
            session.Duration = duration;
            session.RoomNumber = roomNumber;

            _sessionRepository.Update(session);
        }
    }
}
