using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEvents.Model
{
    /// <summary>
    /// Session information
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Session constructor.
        /// </summary>
        public Session()
        {
            StartTime = DateTime.Now;
            Materials = new List<Material>();
        }

        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int SessionId { get; set; }

        /// <summary>
        /// Session Title
        /// </summary>
        [Required]
        public String Title { get; set; }

        /// <summary>
        /// Session Description
        /// </summary>
        [Required]
        public String Description { get; set; }

        /// <summary>
        /// Session Speaker
        /// </summary>
        [Required]
        public String Speaker { get; set; }

        /// <summary>
        /// Twitter Account
        /// </summary>
        [Required]
        public String TwitterAccount { get; set; }

        /// <summary>
        /// Biography
        /// </summary>
        [Required]
        public String Biography { get; set; }

        /// <summary>
        /// Session Startime. 
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// TimeZoneOffset
        /// </summary>
        [Required]
        public int TimeZoneOffset { get; set; }

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
        [NotMapped]
        public int AttendeesCount { get; set; }

        /// <summary>
        /// The score of the session. This value is a calculated property
        /// </summary>
        [NotMapped]
        public double Score { get; set; }

        /// <summary>
        /// EventDefinitionId associated with the session
        /// </summary>
        [Required]
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
        [NotMapped]
        public bool IsFavorite { get; set; }

        /// <summary>
        /// the score of the user that call this method
        /// </summary>
        [NotMapped]
        public double UserScore { get; set; }

    }
}
