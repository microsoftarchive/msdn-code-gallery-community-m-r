using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Model;
using MyEvents.Web.Areas.Organizer.Models;
using MyEvents.Web.Authentication;
using System;
using MyEvents.Web.Services;

namespace MyEvents.Web.Areas.Organizer.Controllers
{
    /// <summary>
    /// Session controller.
    /// </summary>
    [MyEvents.Web.Authentication.Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IEventDefinitionRepository _eventsRepository;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Session controller constructor.
        /// </summary>
        /// <param name="sessionRepository">The session repository.</param>
        /// <param name="materialRepository">The material repository.</param>
        /// <param name="eventsRepository">The events repository.</param>
        /// <param name="authorizationService">The authorization service.</param>
        public SessionController(ISessionRepository sessionRepository, IMaterialRepository materialRepository, IEventDefinitionRepository eventsRepository, IAuthorizationService authorizationService)
        {
            _sessionRepository = sessionRepository;
            _materialRepository = materialRepository;
            _eventsRepository = eventsRepository;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Manage action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public ActionResult Manage(MyEventsIdentity identity, int eventDefinitionId)
        {
            var sessions = _sessionRepository.GetAll(eventDefinitionId);
            var viewModel = new ManageSessionViewModel()
                                {
                                    EventDefinitonId = eventDefinitionId,
                                    Sessions = sessions
                                };

            return View(viewModel);
        }

        /// <summary>
        /// Create action.
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public ActionResult Create(int eventDefinitionId)
        {
            var session = new Session { EventDefinitionId = eventDefinitionId };
            return View("Form", session);
        }

        /// <summary>
        /// Edit action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public ActionResult Edit(MyEventsIdentity identity, int sessionId)
        {
            var session = _sessionRepository.Get(sessionId);

            _authorizationService.ValidateSessionAuthorization(identity, session);

            return View("Form", session);
        }

        /// <summary>
        /// Save action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(MyEventsIdentity identity, Session session)
        {
            if (!ModelState.IsValid)
                return PartialView("Form", session);

            _authorizationService.ValidateSessionAuthorization(identity, session);
            SaveSession(session);

            return RedirectToAction("Manage", new { eventDefinitionId = session.EventDefinitionId });
        }

        /// <summary>
        /// Deletes the session.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="sessionId"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(MyEventsIdentity identity, int sessionId, int eventDefinitionId)
        {
            _authorizationService.ValidateSessionAuthorization(identity, sessionId);
            _sessionRepository.Delete(sessionId);

            return RedirectToAction("Manage", new { eventDefinitionId = eventDefinitionId });
        }

        /// <summary>
        /// Upload material action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadMaterial(MyEventsIdentity identity, int sessionId)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                var fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, (int)file.ContentLength);

                if (fileBytes.Length != 0)
                {
                    _authorizationService.ValidateSessionAuthorization(identity, sessionId);

                    var material = new Material
                    {
                        SessionId = sessionId,
                        Name = HttpUtility.HtmlDecode(file.FileName),
                        ContentType = file.ContentType,
                        Content = fileBytes
                    };
                    _materialRepository.Add(material);
                    return PartialView("MaterialRow", material);
                }
            }
            return null;
        }

        /// <summary>
        /// Delete the material action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMaterial(MyEventsIdentity identity, int materialId)
        {
            var material = _materialRepository.Get(materialId);
            int sessionId = material.SessionId;

            _authorizationService.ValidateSessionAuthorization(identity, sessionId);
            _materialRepository.Delete(materialId);

            return RedirectToAction("Materials", new { sessionId = sessionId });
        }

        /// <summary>
        /// Manage action.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public ActionResult Materials(int sessionId)
        {
            var materials = _materialRepository.GetAll(sessionId).ToList();
            var viewModel = new ManageMaterialsViewModel()
            {
                SessionId = sessionId,
                Materials = materials
            };

            return View(viewModel);
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

        private void SaveSession(Session session)
        {
            bool isUpdate = session.SessionId > 0;

            if (isUpdate)
            {
                var sessionToUpdate = _sessionRepository.Get(session.SessionId);
                MapSession(sessionToUpdate, session);
                _sessionRepository.Update(sessionToUpdate);
            }
            else
            {
                _sessionRepository.Add(session);
            }
        }

        private void MapSession(Session sessionToUpdate, Session session)
        {
            sessionToUpdate.Biography = session.Biography;
            sessionToUpdate.Description = session.Description;
            sessionToUpdate.RoomNumber = session.RoomNumber;
            sessionToUpdate.Speaker = session.Speaker;
            sessionToUpdate.Title = session.Title;
            sessionToUpdate.TwitterAccount = session.TwitterAccount;
        }
    }
}
