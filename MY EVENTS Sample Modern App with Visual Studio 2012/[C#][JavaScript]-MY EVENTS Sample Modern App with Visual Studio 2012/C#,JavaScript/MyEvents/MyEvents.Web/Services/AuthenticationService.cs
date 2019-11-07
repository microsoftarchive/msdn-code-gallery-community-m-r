using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using MyEvents.Data;
using MyEvents.Model;
using MyEvents.Web.Authentication;
using System.Security.Principal;

namespace MyEvents.Web.Services
{
    /// <summary>
    /// Manages the user authentication.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IThirdPartyOauthService _thirdPartyLoginService;
        private readonly IRegisteredUserRepository _registeredUserRepository;

        /// <summary>
        /// Authentication service contructor.
        /// </summary>
        /// <param name="thirdPartyLoginService"> </param>
        /// <param name="registeredUserRepository"></param>
        public AuthenticationService(IThirdPartyOauthService thirdPartyLoginService, IRegisteredUserRepository registeredUserRepository)
        {
            _thirdPartyLoginService = thirdPartyLoginService;
            _registeredUserRepository = registeredUserRepository;
        }

        /// <summary>
        /// Creates the user authentication ticket.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="httpContext">The HTTP context.</param>
        public void CreateAuthenticationTicket(string code, HttpContextBase httpContext)
        {
            string returnUrl = httpContext.Request.Url.AbsoluteUri;
            string accessToken = _thirdPartyLoginService.GetAccessToken(code, returnUrl);
            CreateAuthenticationTicket(new MyEventsIdentity() { AccessToken = accessToken }, httpContext);
            SetPrincipal(httpContext);
        }

        /// <summary>
        /// Creates the user authentication ticket.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="httpContext"></param>
        public void CreateAuthenticationTicket(MyEventsIdentity identity, HttpContextBase httpContext)
        {
            Dictionary<string, object> facebookUserInformation = _thirdPartyLoginService.GetUserInformation(identity);
            _thirdPartyLoginService.MapUserInformationToIdentity(facebookUserInformation, identity);

            identity.UserId = AddUser(identity);

            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(identity);

            var authTicket =
                new FormsAuthenticationTicket(
                    3,
                    identity.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    false,
                    userData
                );

            var authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(authTicket)
            )
            {
                HttpOnly = true
            };
            httpContext.Response.AppendCookie(authCookie);
        }

        /// <summary>
        /// Destroys the authentication ticket.
        /// </summary>
        public void DestroyAuthenticationTicket(MyEventsIdentity identity)
        {
            _thirdPartyLoginService.LogOut(identity);
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Gets the identity from the authentication ticket.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public MyEventsIdentity GetIdentityFromTicket(HttpContextBase httpContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            if (httpContext.Request.Cookies == null ||
                httpContext.Request.Cookies[cookieName] == null)
            {
                return null;
            }

            var authCookie = httpContext.Request.Cookies[cookieName];
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var serializer = new JavaScriptSerializer();
            var identity = serializer.Deserialize<MyEventsIdentity>(authTicket.UserData);

            return identity;
        }       

        /// <summary>
        /// Gets the login url.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string GetLoginUrl(HttpContextBase httpContext, string returnUrl = "")
        {
            return _thirdPartyLoginService.GetLoginUrl(httpContext, returnUrl);
        }

        /// <summary>
        /// Says if the user has an authentication ticket.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public bool HasAuthenticationTicket(HttpContextBase httpContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            if (httpContext.Request.Cookies == null ||
                httpContext.Request.Cookies[cookieName] == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the authentication code from the request.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public string GetOauthCode(HttpContextBase httpContext)
        {
            return _thirdPartyLoginService.GetCodeFromRequest(httpContext);
        }

        /// <summary>
        /// Says if the user is authenticated.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public bool IsAuthenticated(HttpContextBase httpContext)
        {
            SetPrincipal(httpContext);
            return HasAuthenticationTicket(httpContext);
        }

        private int AddUser(MyEventsIdentity identity)
        {
            var user = new RegisteredUser()
            {
                Email = identity.Email,
                FacebookId = identity.ThirdPartyUserId,
                Name = identity.UserName,
                Bio = identity.Bio,
                City = identity.City
            };

            int registeredUserId = _registeredUserRepository.Add(user);
            return registeredUserId;
        }

        private void SetPrincipal(HttpContextBase httpContext)
        {
            var userInfo = this.GetIdentityFromTicket(httpContext);
            if (null == userInfo)
                return;
            var principal = new GenericPrincipal(userInfo, new string[0]);
            httpContext.User = principal;
        }
    }
}