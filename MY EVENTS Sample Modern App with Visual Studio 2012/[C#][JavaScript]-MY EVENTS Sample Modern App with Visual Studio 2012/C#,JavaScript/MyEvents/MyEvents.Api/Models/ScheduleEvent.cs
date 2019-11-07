using System.Collections.Generic;

namespace MyEvents.Api.Models
{
    /// <summary>
    /// Class to return information about the event schedule.
    /// It is used to show the session in the schedule
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