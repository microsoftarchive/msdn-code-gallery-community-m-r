using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Web.Areas.Organizer.Models;
using MyEvents.Web.Authentication;
using MyEvents.Model;
using System;

namespace MyEvents.Web.Areas.Organizer.Controllers
{
    /// <summary>
    /// Registered users controller
    /// </summary>
    [MyEvents.Web.Authentication.Authorize]
    public class RegisteredUsersController : Controller
    {
        private readonly IEventDefinitionRepository _eventDefinitionRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;

        /// <summary>
        /// Registered Users Controller constructor
        /// </summary>
        /// <param name="eventDefinitionRepository"> </param>
        /// <param name="registeredUserRepository"></param>
        public RegisteredUsersController(IEventDefinitionRepository eventDefinitionRepository, IRegisteredUserRepository registeredUserRepository)
        {
            _eventDefinitionRepository = eventDefinitionRepository;
            _registeredUserRepository = registeredUserRepository;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public ActionResult Index(MyEventsIdentity identity, int eventDefinitionId)
        {
            var eventDefinition = this._eventDefinitionRepository.GetById(eventDefinitionId);
            ValidateEventAuthorization(identity, eventDefinition);

            RegisteredUsersViewModel vm = new RegisteredUsersViewModel()
            {
                RegisteredUsers = this._registeredUserRepository.GetAllByEventId(eventDefinitionId),
                Likes = eventDefinition.Likes
            };

            return View(vm);
        }

        private void ValidateEventAuthorization(MyEventsIdentity identity, EventDefinition eventDefinition)
        {
            if (identity.UserId != eventDefinition.OrganizerId)
                throw new UnauthorizedAccessException();
        }  
    }
}
