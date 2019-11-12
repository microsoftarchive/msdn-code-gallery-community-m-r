using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Facebook;

namespace MyEvents.Client.Organizer.Desktop.Services.Facebook
{
    /// <summary>
    /// Facebook api implementation
    /// </summary>
    public class FacebookApi : IFacebookApi
    {
        string _appId = string.Empty;
        string _appPermissions = string.Empty;
        FacebookClient _fb = new FacebookClient();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appId">application id</param>
        /// <param name="appPermissions">extended permissions to submit</param>
        public FacebookApi(string appId, string appPermissions)
        {
            _appId = appId;
            _appPermissions = appPermissions;
        }

        /// <summary>
        /// Get the facebook login url.
        /// </summary>
        /// <returns>return the facebook login url</returns>
        public Uri GetFacebookLoginUrl()
        {
            var parameters = new Dictionary<string, object>();
            parameters["client_id"] = _appId;
            parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
            parameters["response_type"] = "token";
            parameters["display"] = "popup";


            // add the 'scope' only if we have extendedPermissions.
            if (!string.IsNullOrEmpty(_appPermissions))
            {
                parameters["scope"] = _appPermissions;
            }

            return _fb.GetLoginUrl(parameters);
        }

        /// <summary>
        /// Get the response access token.
        /// </summary>
        /// <param name="responseUri">response uri sent by the browser</param>
        /// <returns>access token if sucess, string empty if error</returns>
        public string GetFacebookLoginResponse(Uri responseUri)
        {
            FacebookOAuthResult oauthResult;
            if (_fb.TryParseOAuthCallbackUrl(responseUri, out oauthResult))
            {
                if (oauthResult.IsSuccess)
                {
                    return oauthResult.AccessToken;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the loged in user info based on the access token
        /// </summary>
        /// <param name="accessToken">Login returned access token</param>
        /// <returns>a dictionary with the user info values.</returns>
        public async Task<IDictionary<string, object>> GetLogedUserInfoAsync(string accessToken)
        {
            _fb = new FacebookClient(accessToken);

            return (IDictionary<string, object>)await _fb.GetTaskAsync("me");

        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public string LogOut()
        {
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = ConfigurationManager.AppSettings.Get("FacebookAccessToken");
            ConfigurationManager.AppSettings["FacebookAccessToken"] = string.Empty;
            var url = _fb.GetLogoutUrl(parameters);

            return url.AbsoluteUri;
        }
    }
}
