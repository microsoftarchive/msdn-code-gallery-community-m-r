using System;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Interfaz to access to the Authentication Controller exposed by MyEvents.API
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Get a valid token to call the server
        /// </summary>
        /// <param name="token"></param>
        /// <param name="callback">CallBack func to get authentication token</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult LogOnAsync(string token, Action<AuthenticationResponse> callback);

        /// <summary>
        /// Get Fake Authorization to used it when the clients are in offline mode.
        /// In only to use in demos without internet
        /// </summary>
        /// <param name="callback">CallBack func to get authentication token</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetFakeAuthorizationAsync(Action<AuthenticationResponse> callback);
    }
}
