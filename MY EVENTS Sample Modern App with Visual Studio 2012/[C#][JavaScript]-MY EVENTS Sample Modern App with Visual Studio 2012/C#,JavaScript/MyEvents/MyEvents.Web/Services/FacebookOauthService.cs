using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using MyEvents.Data;
using MyEvents.Web.Authentication;
using MyEvents.Web.Mappers;

namespace MyEvents.Web.Services
{
    /// <summary>
    /// Service to get information from facebook.
    /// </summary>
    public class FacebookOauthService : IThirdPartyOauthService
    {
        private readonly IRegisteredUserRepository _registeredUserRepository;

        /// <summary>
        /// Facebook service constructor
        /// </summary>
        public FacebookOauthService(IRegisteredUserRepository registeredUserRepository)
        {
            _registeredUserRepository = registeredUserRepository;
        }

        /// <summary>
        /// Obtains the user information for the connected user.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInformation(MyEventsIdentity identity)
        {
            var client = new WebClient();
            string meUrl = string.Format("https://graph.facebook.com/me?access_token={0}", identity.AccessToken);

            string userInformationAsJson;

            try
            {
                userInformationAsJson = client.DownloadString(meUrl);
            }
            catch (Exception)
            {
                return null;
            }

            var jsSerializer = new JavaScriptSerializer();
            var userInformation = jsSerializer.Deserialize<Dictionary<string, object>>(userInformationAsJson);

            return userInformation;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string GetAccessToken(string code, string returnUrl)
        {
            string facebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
            string clientSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];

            var client = new WebClient();

            string getTokenUrl = string.Format(
                "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                facebookAppId,
                returnUrl,
                clientSecret,
                code);

            string response = client.DownloadString(getTokenUrl);

            var parameters = response.Split('&')
                .Select(segment => segment.Split('='))
                .ToDictionary(segmentData => segmentData[0], segmentData => segmentData[1]);

            return parameters["access_token"];
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <param name="identity"></param>
        public void LogOut(MyEventsIdentity identity)
        {
            if (null == identity)
                return;

            var client = new WebClient();
            string facebookLogOutUrl = string.Format("https://www.facebook.com/logout.php?access_token={0}", identity.AccessToken);
            client.DownloadString(facebookLogOutUrl);
        }

        /// <summary>
        /// Maps the user information to the identity.
        /// </summary>
        /// <param name="userInformation"></param>
        /// <param name="identity"></param>
        public void MapUserInformationToIdentity(Dictionary<string, object> userInformation, MyEventsIdentity identity)
        {
            var mapper = new FacebookUserInformationToIdentityMapper();
            mapper.Map(userInformation, identity);
        }

        /// <summary>
        /// Gets the login url.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="returnUrl">optional, if not provided it will take the url from the current request</param>
        /// <returns></returns>
        public string GetLoginUrl(System.Web.HttpContextBase httpContext, string returnUrl = "")
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                Uri requestUrl = httpContext.Request.Url;
                returnUrl = requestUrl.AbsoluteUri;
            }

            string facebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
            string display = httpContext.Request.Browser.IsMobileDevice ? "touch" : "page";
            string scope = "email, user_location, user_about_me";

            string facebookLoginPageUrl = string.Format(
            "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}&state={3}&display={4}",
            facebookAppId,
            returnUrl,
            scope,
            string.Empty,
            display
            );

            return facebookLoginPageUrl;
        }

        /// <summary>
        /// Gets the authentication code from the request.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public string GetCodeFromRequest(System.Web.HttpContextBase httpContext)
        {
            string code = httpContext.Request.QueryString.Get("code");
            return code;
        }
    }
}