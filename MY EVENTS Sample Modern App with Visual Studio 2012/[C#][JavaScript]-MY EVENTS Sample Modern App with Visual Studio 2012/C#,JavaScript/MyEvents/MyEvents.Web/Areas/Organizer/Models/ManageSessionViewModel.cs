using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Manage session view model.
    /// </summary>
    public class ManageSessionViewModel
    {
        /// <summary>
        /// The event definition id.
        /// </summary>
        public int EventDefinitonId { get; set; }

        /// <summary>
        /// The event sessions.
        /// </summary>
        public IEnumerable<Session> Sessions { get; set; }
    }
}