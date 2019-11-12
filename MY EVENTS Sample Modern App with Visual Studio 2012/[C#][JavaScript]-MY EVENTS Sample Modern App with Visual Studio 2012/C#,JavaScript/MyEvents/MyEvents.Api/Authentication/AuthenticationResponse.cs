using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvents.Api.Authentication
{
    /// <summary>
    /// Authorization Response
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// Id of the user registered in my events
        /// </summary>
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// Facebook id of the registered user.
        /// </summary>
        public string FacebookUserId { get; set; }

        /// <summary>
        /// User name of the registered user in my events.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Auth token that clients should pass in the request headers
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// ExpirationTime (Mileseconds)
        /// </summary>
        public double ExpirationTime { get; set; }
    }
}