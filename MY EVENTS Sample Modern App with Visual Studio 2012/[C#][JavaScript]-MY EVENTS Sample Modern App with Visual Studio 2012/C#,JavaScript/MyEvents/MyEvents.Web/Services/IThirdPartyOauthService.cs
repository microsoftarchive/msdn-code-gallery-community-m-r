using System.Collections.Generic;
using System.Web;
using MyEvents.Web.Authentication;

namespace MyEvents.Web.Services
{
    /// <summary>
    /// Service to get information from facebook.
    /// </summary>
    public interface IThirdPartyOauthService
    {
        /// <summary>
        /// Obtains the user information for the connected user.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Dictionary<string, object> GetUserInformation(MyEventsIdentity identity);

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        string GetAccessToken(string code, string returnUrl);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <param name="identity"></param>
        void LogOut(MyEventsIdentity identity);

        /// <summary>
        /// Maps the user information to the identity.
        /// </summary>
        /// <param name="userInformation"></param>
        /// <param name="identity"></param>
        void MapUserInformationToIdentity(Dictionary<string, object> userInformation, MyEventsIdentity identity);

        /// <summary>
        /// Gets the login url.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="returnUrl">optional, if not provided it will take the url from the current request</param>
        /// <returns></returns>
        string GetLoginUrl(HttpContextBase httpContext, string returnUrl = "");

        /// <summary>
        /// Gets the authentication code from the request.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        string GetCodeFromRequest(HttpContextBase httpContext);
    }
}