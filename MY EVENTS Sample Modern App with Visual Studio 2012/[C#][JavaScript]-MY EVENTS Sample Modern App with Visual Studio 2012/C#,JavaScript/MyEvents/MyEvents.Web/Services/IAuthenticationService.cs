using System.Web;
using MyEvents.Web.Authentication;

namespace MyEvents.Web.Services
{
    /// <summary>
    /// Manages the user authentication.
    /// </summary>
    public interface IAuthenticationService
    {

        /// <summary>
        /// Says if the user is authenticated.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        bool IsAuthenticated(HttpContextBase httpContext);

        /// <summary>
        /// Creates the user authentication ticket.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="httpContext">The HTTP context.</param>
        void CreateAuthenticationTicket(string code, HttpContextBase httpContext);

        /// <summary>
        /// Creates the user authentication ticket.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="httpContext"></param>
        void CreateAuthenticationTicket(MyEventsIdentity identity, HttpContextBase httpContext);

        /// <summary>
        /// Destroys the authentication ticket.
        /// </summary>
        void DestroyAuthenticationTicket(MyEventsIdentity identity);

        /// <summary>
        /// Gets the identity from the authentication ticket.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        MyEventsIdentity GetIdentityFromTicket(HttpContextBase httpContext);

        /// <summary>
        /// Gets the login url.
        /// </summary>
        /// <returns></returns>
        string GetLoginUrl(HttpContextBase httpContext, string returnUrl = "");

        /// <summary>
        /// Says if has authentication ticket.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        bool HasAuthenticationTicket(HttpContextBase httpContext);

        /// <summary>
        /// Gets the Oauth from the request code.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        string GetOauthCode(HttpContextBase httpContext);
    }
}