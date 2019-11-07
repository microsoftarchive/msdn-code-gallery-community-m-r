using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Home view model.
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// The event list for the current user.
        /// </summary>
        public IEnumerable<EventDefinition> Events { get; set; }
    }
}