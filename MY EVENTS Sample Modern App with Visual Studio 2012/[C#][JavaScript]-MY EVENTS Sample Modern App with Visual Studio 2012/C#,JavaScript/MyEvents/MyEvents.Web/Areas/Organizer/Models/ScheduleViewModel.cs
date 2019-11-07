using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Schedule view model.
    /// </summary>
    public class ScheduleViewModel
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