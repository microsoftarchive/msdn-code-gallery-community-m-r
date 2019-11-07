using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Schedule session.
    /// </summary>
    public class ScheduleSession
    {
        /// <summary>
        /// Session id.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The duration in minutes
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// The speaker name
        /// </summary>
        public string Speaker { get; set; }

        /// <summary>
        /// The end time.
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return StartTime.AddMinutes(Duration);
            }
        }

        /// <summary>
        /// The room number.
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// True if the session is assigned to the schedule.
        /// </summary>
        public bool IsAssigned { get { return RoomNumber > 0; } }
    }
}