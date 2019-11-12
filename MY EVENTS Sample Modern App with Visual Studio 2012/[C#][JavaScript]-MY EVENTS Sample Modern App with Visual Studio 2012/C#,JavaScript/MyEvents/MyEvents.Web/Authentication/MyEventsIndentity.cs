using System.Security.Principal;

namespace MyEvents.Web.Authentication
{
    /// <summary>
    /// Information about the logged in user.
    /// </summary>
    public class MyEventsIdentity : IIdentity
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Third party user id.
        /// </summary>
        public string ThirdPartyUserId { get; set; }

        /// <summary>
        /// Facebook user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Facebook user acces token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The user email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Biography
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// City location
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        public string AuthenticationType
        {
            get { return "MyEvents.Authentication"; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(this.ThirdPartyUserId)
                       && !string.IsNullOrEmpty(AccessToken);
            }
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name
        {
            get { return UserName; }
        }
    }
}