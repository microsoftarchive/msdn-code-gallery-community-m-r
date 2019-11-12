using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEvents.Client.Organizer.Desktop.Services.Facebook
{
    /// <summary>
    /// Facebook api service contract
    /// </summary>
    public interface IFacebookApi
    {
        /// <summary>
        /// Get the facebook login url.
        /// </summary>
        /// <returns>return the facebook login url</returns>
        Uri GetFacebookLoginUrl();

        /// <summary>
        /// Get the response access token.
        /// </summary>
        /// <param name="responseUri">response uri sent by the browser</param>
        /// <returns>access token if sucess, string empty if error</returns>
        string GetFacebookLoginResponse(Uri responseUri);

        /// <summary>
        /// Get the loged in user info based on the access token
        /// </summary>
        /// <param name="accessToken">Login returned access token</param>
        /// <returns>a dictionary with the user info values.</returns>
        Task<IDictionary<string, object>> GetLogedUserInfoAsync(string accessToken);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        string LogOut();
    }
}
