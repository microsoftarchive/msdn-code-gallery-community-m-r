using System;
using System.Collections.Generic;

namespace MyEvents.Api.Models
{
    /// <summary>
    /// Class to return information about the event schedule.
    /// It is used to show the session in the schedule
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// The shedule event.
        /// </summary>
        public ScheduleEvent EventDefinition { get; set; }

        /// <summary>
        /// The list of times to show in the schedule.
        /// </summary>
        public List<DateTime> Times { get; set; }
    }
}