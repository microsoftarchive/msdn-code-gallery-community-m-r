using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Shedule event.
    /// </summary>
    public class ScheduleEvent
    {
        /// <summary>
        /// The event definition id.
        /// </summary>
        public int EventDefinitionId { get; set; }
        
        /// <summary>
        /// The number of rooms.
        /// </summary>
        public int Rooms { get; set; }

        /// <summary>
        /// The sessions.
        /// </summary>
        public List<ScheduleSession> Sessions { get; set; }
    }
}