using System;
using System.Globalization;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// <see cref="MyEvents.Api.Client.IAuthenticationService"/>
    /// </summary>
    public class AuthenticationService : BaseRequest, IAuthenticationService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        public AuthenticationService(string urlPrefix)
            : base(urlPrefix, string.Empty)
        {
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IAuthenticationService"/>
        /// </summary>
        /// <param name="token"><see cref="MyEvents.Api.Client.IAuthenticationService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IAuthenticationService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IAuthenticationService"/></returns>
        public IAsyncResult LogOnAsync(string token, Action<AuthenticationResponse> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/authentication?token={1}", _urlPrefix, token);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IAuthenticationService"/>
        /// </summary>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IAuthenticationService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IAuthenticationService"/></returns>
        public IAsyncResult GetFakeAuthorizationAsync(Action<AuthenticationResponse> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/authentication", _urlPrefix);

            return base.DoGet(url, callback);
        }

    }
}
