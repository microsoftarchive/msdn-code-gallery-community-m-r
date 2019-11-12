using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Event RegisteredUser
    /// </summary>
    public class RegisteredUser
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// FacebookId
        /// </summary>
        public string FacebookId { get; set; }

        /// <summary>
        /// Email
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
        /// Events that are organized by this users
        /// </summary>
        public ICollection<EventDefinition> OrganizerEventDefinitions { get; set; }

        /// <summary>
        /// Events associated with the RegisteredUser 
        /// </summary>
        public ICollection<EventDefinition> AttendeeEventDefinitions { get; set; }

        /// <summary>
        /// Session RegisteredUsers
        /// </summary>
        public ICollection<SessionRegisteredUser> SessionRegisteredUsers { get; set; }
    }
}
