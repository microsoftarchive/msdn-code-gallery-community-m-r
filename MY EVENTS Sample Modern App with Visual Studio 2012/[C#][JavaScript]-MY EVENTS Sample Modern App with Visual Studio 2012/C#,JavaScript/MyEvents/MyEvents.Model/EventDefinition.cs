using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyEvents.Model.Validators;

namespace MyEvents.Model
{
    /// <summary>
    /// Event definition information
    /// </summary>
    public class EventDefinition
    {
        /// <summary>
        /// Event definition constructor.
        /// </summary>
        public EventDefinition()
        {
            this.Sessions = new List<Session>();
            this.RegisteredUsers = new List<RegisteredUser>();
            this.Registered = false;
        }

        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int EventDefinitionId { get; set; }

        /// <summary>
        /// Event Name
        /// </summary>
        [Required]
        public String Name { get; set; }

        /// <summary>
        /// Event Description
        /// </summary>
        [Required]
        public String Description { get; set; }

        /// <summary>
        /// Tags; Windows Phone;Windows Azure...
        /// </summary>
        [Required]
        public String Tags { get; set; }

        /// <summary>
        /// Number of rooms
        /// </summary>
        [Required]
        public int RoomNumber { get; set; }

        /// <summary>
        /// Twitter Account
        /// </summary>
        [Required]
        public String TwitterAccount { get; set; }

        /// <summary>
        /// Event Date
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// TimeZoneOffset
        /// </summary>
        [Required]
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Start time.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time.
        /// </summary>
        [Required]
        [DateGreaterThan("StartTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// City 
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// ZipCode
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// GPS Latitute for GPS localization
        /// </summary>
        [Required]
        public float Latitude { get; set; }

        /// <summary>
        /// GPS Longitude for GPS localization
        /// </summary>
        [Required]
        public float Longitude { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [Required]
        public byte[] Logo { get; set; }

        /// <summary>
        /// Number of likes (facebook) of the event
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// MapImage
        /// </summary>
        public byte[] MapImage { get; set; }

        /// <summary>
        /// the number of attendees registered in this event definition
        /// </summary>
        [NotMapped]
        public int AttendeesCount { get; set; }

        /// <summary>
        /// OrganizerId of the event
        /// </summary>
        public int OrganizerId { get; set; }

        /// <summary>
        /// Organizer of the event
        /// </summary>
        public RegisteredUser Organizer { get; set; }

        /// <summary>
        /// Sessions
        /// </summary>
        public ICollection<Session> Sessions { get; set; }

        /// <summary>
        /// List of RegisteredUsers
        /// </summary>
        public ICollection<RegisteredUser> RegisteredUsers { get; set; }

        /// <summary>
        /// true the user will be attend to see the event
        /// </summary>
        [NotMapped]
        public bool Registered { get; set; }

        /// <summary>
        /// Room Points
        /// </summary>
        public ICollection<RoomPoint> RoomPoints { get; set; }

    }
}
