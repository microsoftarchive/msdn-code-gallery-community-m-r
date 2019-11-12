using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEvents.Model;
using MyEvents.Web.Validators;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Edit event view model.
    /// </summary>
    public class EditEventViewModel
    {
        private const int NumberOfRooms = 3;

        /// <summary>
        /// Edit event view model constructor.
        /// </summary>
        public EditEventViewModel()
        {
            Rooms = new SelectList(GetRoomList(), "Key", "Value");
        }

        /// <summary>
        /// Event Definition Id
        /// </summary>
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
        /// OrganizerId of the event
        /// </summary>
        public int OrganizerId { get; set; }

        /// <summary>
        /// Start time. UTC.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public byte[] Logo { get; set; }

        /// <summary>
        /// Determines if the logo has been edited.
        /// </summary>
        public bool IsLogoSetted { get; set; }

        /// <summary>
        /// The event has logo.
        /// </summary>
        [IsTrue]
        public bool HasLogo { get; set; }

        /// <summary>
        /// End time. UTC.
        /// </summary>
        [Required]
        [MyEvents.Web.Validators.DateGreaterThan("StartTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The list of possible available number of rooms.
        /// </summary>
        public SelectList Rooms { get; set; }

        private IEnumerable GetRoomList()
        {
            var rooms = new List<KeyValuePair<int, string>>();
            for (int roomNumber = 1; roomNumber <= NumberOfRooms; roomNumber++)
            {
                rooms.Add(new KeyValuePair<int, string>(roomNumber, roomNumber.ToString(CultureInfo.InvariantCulture)));
            }
            return rooms;
        }
    }
}