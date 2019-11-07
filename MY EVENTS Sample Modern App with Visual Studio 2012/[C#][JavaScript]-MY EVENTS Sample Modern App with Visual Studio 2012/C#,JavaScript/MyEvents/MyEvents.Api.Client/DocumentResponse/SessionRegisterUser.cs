using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// registeredUsers that will go to the session
    /// </summary>
    public class SessionRegisteredUser
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int SessionRegisteredUserId { get; set; }

        /// <summary>
        /// SessionId
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// RegisteredUserId
        /// </summary>
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// FacebookId
        /// </summary>
        public string FacebookId { get; set; }

        /// <summary>
        /// Session Score
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Rated
        /// </summary>
        public bool Rated { get; set; }
    }
}
