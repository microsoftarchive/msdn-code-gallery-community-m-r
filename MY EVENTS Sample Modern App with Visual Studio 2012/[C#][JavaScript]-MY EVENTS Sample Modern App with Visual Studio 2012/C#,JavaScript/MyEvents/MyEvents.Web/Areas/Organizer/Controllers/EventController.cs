using System;
using System.Web;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Model;
using MyEvents.Web.Areas.Organizer.Models;
using MyEvents.Web.Authentication;
using MyEvents.Web.Services;

namespace MyEvents.Web.Areas.Organizer.Controllers
{
    /// <summary>
    /// Event controller
    /// </summary>
    [MyEvents.Web.Authentication.Authorize]
    public class EventController : Controller
    {
        private readonly IEventDefinitionRepository _eventsRepository;
        private readonly IAuthorizationService _authorizationService;
        private const string CurrentLogoKey = "CURRENT_LOGO";
        private const string JpegContentType = "image/jpeg";

        /// <summary>
        /// Event controller contructor.
        /// </summary>
        public EventController(IEventDefinitionRepository eventsRepository, IAuthorizationService authorizationService)
        {
            _eventsRepository = eventsRepository;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Create action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new EditEventViewModel
                                {
                                    Date = DateTime.Now,
                                    RoomNumber = 1
                                };
            return View(viewModel);
        }

        /// <summary>
        /// Edit action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(MyEventsIdentity identity, int eventDefinitionId)
        {
            EventDefinition eventDefinition = _eventsRepository.GetById(eventDefinitionId);

            _authorizationService.ValidateEventAuthorization(identity, eventDefinition);

            var viewModel = new EditEventViewModel()
                                {
                                    HasLogo = true
                                };
            MapEventDefinitionToViewModel(eventDefinition, viewModel);
            ResetLogo();
            return View(viewModel);
        }

        /// <summary>
        /// Create post action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(MyEventsIdentity identity, EditEventViewModel viewModel)
        {
            SetLogo(viewModel, ModelState);

            if (!ModelState.IsValid)
                return View(viewModel);

            var eventDefinition = new EventDefinition();
            MapViewModelToEventDefinition(viewModel, eventDefinition);
            eventDefinition.OrganizerId = identity.UserId;
            _eventsRepository.Add(eventDefinition);

            ResetLogo();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Edit post action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(MyEventsIdentity identity, EditEventViewModel viewModel)
        {
            SetLogo(viewModel, ModelState);

            if (!ModelState.IsValid)
                return View(viewModel);

            var eventDefinition = _eventsRepository.GetById(viewModel.EventDefinitionId);

            _authorizationService.ValidateEventAuthorization(identity, eventDefinition);

            MapViewModelToEventDefinition(viewModel, eventDefinition);
            _eventsRepository.Update(eventDefinition);

            ResetLogo();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Delete action.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(MyEventsIdentity identity, int eventDefinitionId)
        {
            EventDefinition eventDefinition = _eventsRepository.GetById(eventDefinitionId);

            _authorizationService.ValidateEventAuthorization(identity, eventDefinition);

            _eventsRepository.Delete(eventDefinitionId);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// event logo action.
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EventLogo(int eventDefinitionId)
        {
            if (eventDefinitionId == 0)
                return new FilePathResult(Url.Content("~/Styles/Images/default-logo.png"), JpegContentType);

            var eventDefinition = _eventsRepository.GetById(eventDefinitionId);
            return File(eventDefinition.Logo, JpegContentType);
        }

        /// <summary>
        /// Current logo action.
        /// </summary>
        /// <returns></returns>
        public ActionResult CurrentLogo()
        {
            if (Session[CurrentLogoKey] != null)
            {
                var imageData = (byte[])Session[CurrentLogoKey];
                return File(imageData, JpegContentType);
            }

            return null;
        }

        /// <summary>
        /// Upload event logo action.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void UploadEventLogo()
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                byte[] imageBytes = new byte[file.ContentLength];
                file.InputStream.Read(imageBytes, 0, (int)file.ContentLength);

                if (imageBytes.Length != 0)
                {
                    Session[CurrentLogoKey] = imageBytes;
                }
            }
        }

        private void SetLogo(EditEventViewModel viewModel, ModelStateDictionary modelState)
        {
            bool hasLogo = Session[CurrentLogoKey] != null;

            if(hasLogo)
            {
                viewModel.Logo = (byte[])Session[CurrentLogoKey];
                viewModel.IsLogoSetted = true;
                viewModel.HasLogo = true;
                if(modelState.ContainsKey("HasLogo"))
                    modelState.Remove("HasLogo");
            }
        }

        private void ResetLogo()
        {
            Session.Remove(CurrentLogoKey);
        }

        private void MapViewModelToEventDefinition(EditEventViewModel viewModel, EventDefinition eventDefinition)
        {
            eventDefinition.Address = viewModel.Address;
            eventDefinition.City = viewModel.City;
            eventDefinition.Date = viewModel.Date;
            eventDefinition.Description = viewModel.Description;
            eventDefinition.EndTime = viewModel.EndTime.ToUniversalTime();
            eventDefinition.EventDefinitionId = viewModel.EventDefinitionId;
            eventDefinition.Latitude = viewModel.Latitude;
            eventDefinition.Longitude = viewModel.Longitude;
            eventDefinition.Name = viewModel.Name;
            eventDefinition.RoomNumber = viewModel.RoomNumber;
            eventDefinition.StartTime = viewModel.StartTime.ToUniversalTime();
            eventDefinition.Tags = viewModel.Tags;
            eventDefinition.TimeZoneOffset = viewModel.TimeZoneOffset;
            eventDefinition.TwitterAccount = viewModel.TwitterAccount;
            eventDefinition.ZipCode = viewModel.ZipCode;

            if (viewModel.IsLogoSetted)
                eventDefinition.Logo = viewModel.Logo;
        }

        private void MapEventDefinitionToViewModel(EventDefinition eventDefinition, EditEventViewModel viewModel)
        {
            viewModel.Address = eventDefinition.Address;
            viewModel.City = eventDefinition.City;
            viewModel.Date = eventDefinition.Date;
            viewModel.Description = eventDefinition.Description;
            viewModel.EndTime = eventDefinition.EndTime;
            viewModel.EventDefinitionId = eventDefinition.EventDefinitionId;
            viewModel.Latitude = eventDefinition.Latitude;
            viewModel.Longitude = eventDefinition.Longitude;
            viewModel.Name = eventDefinition.Name;
            viewModel.RoomNumber = eventDefinition.RoomNumber;
            viewModel.StartTime = eventDefinition.StartTime;
            viewModel.Tags = eventDefinition.Tags;
            viewModel.TimeZoneOffset = eventDefinition.TimeZoneOffset;
            viewModel.TwitterAccount = eventDefinition.TwitterAccount;
            viewModel.ZipCode = eventDefinition.ZipCode;
        }
        
    }
}
