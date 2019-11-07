using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyEvents.Api.Client
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
        }

        /// <summary>
        /// UniqueId
        /// </summary>
        public int EventDefinitionId { get; set; }

        /// <summary>
        /// Event Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Event Description
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Tags; Windows Phone;Windows Azure...
        /// </summary>
        public String Tags { get; set; }

        /// <summary>
        /// Number of rooms
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Twitter Account
        /// </summary>
        public String TwitterAccount { get; set; }

        /// <summary>
        /// Event Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// TimeZoneOffset
        /// </summary>
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// City 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// ZipCode 
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// GPS Latitute for GPS localization
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// GPS Longitude for GPS localization
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
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
        public bool Registered { get; set; }

        /// <summary>
        /// Room Points
        /// </summary>
        public ICollection<RoomPoint> RoomPoints { get; set; }

        /// <summary>
        /// Indicates the group where the item is displayed for template selection
        /// </summary>
        public int GroupNumber { get; set; }

    }
}
