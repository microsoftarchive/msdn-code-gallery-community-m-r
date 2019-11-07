using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// RegisteredUser Controller
    /// </summary>
    public class RegisteredUsersController : ApiController
    {
        private readonly IRegisteredUserRepository _registeredUserRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registeredUserRepository">IRegisteredUserRepository dependency</param>
        public RegisteredUsersController(IRegisteredUserRepository registeredUserRepository)
        {
            if (registeredUserRepository == null)
                throw new ArgumentNullException("registeredUserRepository");

            _registeredUserRepository = registeredUserRepository;
        }

        /// <summary>
        /// Get RegisteredUser Information
        /// </summary>
        /// <param name="facebookId">facebookId</param>
        /// <returns>RegisteredUser</returns>
        public RegisteredUser GetRegisteredUser(string facebookId)
        {
            return _registeredUserRepository.Get(facebookId);
        }

        /// <summary>
        /// Get All RegisteredUsers By EventId
        /// </summary>
        /// <param name="eventDefinitionId">Event Definition Id</param>
        /// <returns>List of RegisteredUsers</returns>
        public IList<RegisteredUser> GetAllRegisteredUsersByEventId(int eventDefinitionId)
        {
            return _registeredUserRepository.GetAllByEventId(eventDefinitionId);
        }

        /// <summary>
        /// Get All RegisteredUsers By SessionId
        /// </summary>
        /// <param name="sessionId">Session Id</param>
        /// <returns>List of RegisteredUsers</returns>
        public IList<RegisteredUser> GetAllRegisteredUsersBySessionId(int sessionId)
        {
            return _registeredUserRepository.GetAllBySessionId(sessionId);
        }

        /// <summary>
        /// Get All events for the currennt RegisteredUserId
        /// </summary>
        /// <param name="registeredUserId"></param>
        /// <returns></returns>
        public IList<EventDefinition> GetRegisteredUserEventDefinitions(int registeredUserId)
        {
            return _registeredUserRepository.GetEventDefinitions(registeredUserId);
        }

        /// <summary>
        /// Add RegisteredUser
        /// </summary>
        /// <param name="registeredUser">registeredUser information</param>
        /// <returns>RegisteredUser id</returns>
        public int PostRegisteredUserCreateIfNotExists(RegisteredUser registeredUser)
        {
            if (registeredUser == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            return _registeredUserRepository.Add(registeredUser);
        }

        /// <summary>
        /// Asociate RegisteredUser with event
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="eventDefinitionId">EventDefinition Id</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void PostRegisteredUserToEvent(int registeredUserId, int eventDefinitionId)
        {
            ValidateRegisteredUserId(registeredUserId);

            _registeredUserRepository.AddRegisteredUserToEvent(registeredUserId, eventDefinitionId);
        }

        /// <summary>
        /// Delete RegisteredUser from event
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="eventDefinitionId">EventDefinition Id</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void DeleteRegisteredUserFromEvent(int registeredUserId, int eventDefinitionId)
        {
            ValidateRegisteredUserId(registeredUserId);

            _registeredUserRepository.DeleteRegisteredUserFromEvent(registeredUserId, eventDefinitionId);
        }

        /// <summary>
        /// Asociate RegisteredUser with session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void PostRegisteredUserToSession(int registeredUserId, int sessionId)
        {
            ValidateRegisteredUserId(registeredUserId);

            _registeredUserRepository.AddRegisteredUserToSession(registeredUserId, sessionId);
        }

        /// <summary>
        /// Asociate RegisteredUser with session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        /// <param name="score">Score</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void PostRegisteredUserScore(int registeredUserId, int sessionId, double score)
        {
            ValidateRegisteredUserId(registeredUserId);

            _registeredUserRepository.AddRegisteredUserScore(registeredUserId, sessionId, score);
        }


        /// <summary>
        /// Delete RegisteredUser from session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void DeleteRegisteredUserFromSession(int registeredUserId, int sessionId)
        {
            ValidateRegisteredUserId(registeredUserId);

            _registeredUserRepository.DeleteRegisteredUserFromSession(registeredUserId, sessionId);
        }

        /// <summary>
        /// Get Fake User to used it when the clients are in offline mode.
        /// In only to use in the demos without internet
        /// </summary>
        /// <returns>RegisteredUser</returns>
        public RegisteredUser GetFakeRegisteredUser()
        {
            int fakeUserId = Int32.Parse(WebConfigurationManager.AppSettings["fakeUserId"]);
            return _registeredUserRepository.GetById(fakeUserId);
        }

        private static void ValidateRegisteredUserId(int registeredUserId)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            if (token.RegisteredUserId != registeredUserId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }
    }
}
