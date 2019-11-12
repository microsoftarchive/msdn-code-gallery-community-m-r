using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyEvents.Api.Client
{
    /// <summary>
    /// Session information
    /// </summary>
    public class Session
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Session Title
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Session Description
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Session Speaker
        /// </summary>
        public String Speaker { get; set; }

        /// <summary>
        /// Twitter Account
        /// </summary>
        public String TwitterAccount { get; set; }

        /// <summary>
        /// Biography
        /// </summary>
        public String Biography { get; set; }

        /// <summary>
        /// Session Startime. 
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// TimeZoneOffset
        /// </summary>
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Session EndTime. 
        /// </summary>
        public DateTime EndTime 
        {
            get
            {
                return StartTime.AddMinutes(Duration);
            }
        }

        /// <summary>
        /// Duration in minutes
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Room where event happens
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// the number of attendees registered in this session
        /// </summary>
        public int AttendeesCount { get; set; }

        /// <summary>
        /// The score of the session. This value is a calculated property
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// EventDefinitionId associated with the session
        /// </summary>
        public int EventDefinitionId { get; set; }

        /// <summary>
        /// Event associated with the session
        /// </summary>
        public EventDefinition EventDefinition { get; set; }

        /// <summary>
        /// Session RegisteredUsers
        /// </summary>
        public List<SessionRegisteredUser> SessionRegisteredUsers { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Materials
        /// </summary>
        public ICollection<Material> Materials { get; set; }

        /// <summary>
        /// true the user will be attend to see the session
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// the score of the user that call this method
        /// </summary>
        public double UserScore { get; set; }
    }
}
