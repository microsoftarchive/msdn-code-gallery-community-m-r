using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Registered Users View Model
    /// </summary>
    public class RegisteredUsersViewModel
    {
        /// <summary>
        /// The registered users list for the current event.
        /// </summary>
        public IEnumerable<RegisteredUser> RegisteredUsers { get; set; }

        /// <summary>
        /// Number of likes
        /// </summary>
        public int Likes { get; set; }
    }
}