using System;
using System.Collections.Generic;

namespace MyEvents.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleSession
    {
        /// <summary>
        /// Session id.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// EventDefinition Id
        /// </summary>
        public int EventDefinitionId { get; set; }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The duration in minutes
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// TimeZoneOffset
        /// </summary>
        public int TimeZoneOffset { get; set; }

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

        /// <summary>
        /// true the user will be attend to see the session
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// the score of the user that call this method
        /// </summary>
        public double UserScore { get; set; }

        /// <summary>
        /// Session RegisteredUsers
        /// </summary>
        public IEnumerable<SessionRegisteredUser> SessionRegisteredUsers { get; set; }
    }
}