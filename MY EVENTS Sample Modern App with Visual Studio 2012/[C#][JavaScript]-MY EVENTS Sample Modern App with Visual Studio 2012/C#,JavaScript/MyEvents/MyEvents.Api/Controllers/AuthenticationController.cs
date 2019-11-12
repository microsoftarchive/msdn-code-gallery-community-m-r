using System;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    public class AuthenticationController : ApiController
    {
        private readonly IRegisteredUserRepository _registeredUserRepository = null;
        private readonly IFacebookService _facebookService = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registeredUserRepository">IRegisteredUserRepository dependency</param>
        /// <param name="facebookService">IFacebookService dependency</param>
        public AuthenticationController(IRegisteredUserRepository registeredUserRepository, IFacebookService facebookService)
        {
            if (registeredUserRepository == null)
                throw new ArgumentNullException("registeredUserRepository");

            if (facebookService == null)
                throw new ArgumentNullException("facebookService");

            _registeredUserRepository = registeredUserRepository;
            _facebookService = facebookService;
        }

        /// <summary>
        /// Validate the facebook token to validate that it´s a valid one for a registered user.
        /// </summary>
        /// <param name="token">facebook access token</param>
        /// <returns>AuthenticationResponse</returns>
        [HttpGet]
        public AuthenticationResponse LogOn(string token)
        {
            AuthenticationResponse response = null;

            // ONLY TO USE IN DEMOS WITHOUT INTERNET!
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["OfflineMode"]))
                return GetFakeAuthorization();

            if (String.IsNullOrEmpty(token))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            RegisteredUser registeredUser = _facebookService.GetUserInformation(token);
            if (registeredUser != null && !String.IsNullOrEmpty(registeredUser.FacebookId))
            {
                int registeredUserId = _registeredUserRepository.Add(registeredUser);
                if (registeredUserId > 0)
                {
                    response = new AuthenticationResponse();
                    response.RegisteredUserId = registeredUserId;
                    response.UserName = registeredUser.Name;
                    response.Token = MyEventsToken.CreateToken(registeredUserId);
                    response.ExpirationTime = TimeSpan.FromHours(1).TotalMilliseconds;
                    response.FacebookUserId = registeredUser.FacebookId;
                }
            }

            return response;
        }

        /// <summary>
        /// Get Fake Authorization to used it when the clients are in no internet mode.
        /// ONLY TO USE IN DEMOS WITHOUT INTERNET!
        /// </summary>
        /// <returns>AuthenticationResponse</returns>
        public AuthenticationResponse GetFakeAuthorization()
        {
            var response = new AuthenticationResponse();
            response.RegisteredUserId = Int32.Parse(WebConfigurationManager.AppSettings["fakeUserId"]);
            response.UserName = WebConfigurationManager.AppSettings["fakeUserName"]; 
            response.Token = MyEventsToken.CreateToken(response.RegisteredUserId);
            response.ExpirationTime = TimeSpan.FromHours(1).TotalMilliseconds;
            return response;
        }

    }
}