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
    public class OfflineOauthService : IOfflineOauthService
    {
        /// <summary>
        /// Obtains the user information for the connected user.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInformation(MyEventsIdentity identity)
        {
            var userInformation = new Dictionary<string, object>();
            userInformation.Add("name", ConfigurationManager.AppSettings["fakeUserName"]);
            userInformation.Add("id", ConfigurationManager.AppSettings["fakeFacebookUserId"]);

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
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <param name="identity"></param>
        public void LogOut(MyEventsIdentity identity)
        {
        }

        /// <summary>
        /// Maps the user information to the identity.
        /// </summary>
        /// <param name="userInformation"></param>
        /// <param name="identity"></param>
        public void MapUserInformationToIdentity(Dictionary<string, object> userInformation, MyEventsIdentity identity)
        {
            identity.UserName = ConfigurationManager.AppSettings["fakeUserName"];
            identity.UserId = int.Parse(ConfigurationManager.AppSettings["fakeUserId"]);
            identity.ThirdPartyUserId = ConfigurationManager.AppSettings["fakeFacebookUserId"];
            identity.Email = "fakemail@outlook.com";
        }

        /// <summary>
        /// Gets the login url.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="returnUrl">optional, if not provided it will take the url from the current request</param>
        /// <returns></returns>
        public string GetLoginUrl(System.Web.HttpContextBase httpContext, string returnUrl = "")
        {
            return "/Organizer/Home/Index/";
        }

        /// <summary>
        /// Gets the authentication code from the request.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public string GetCodeFromRequest(System.Web.HttpContextBase httpContext)
        {
            return Guid.NewGuid().ToString();
        }
    }
}