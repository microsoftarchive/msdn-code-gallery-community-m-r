using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEvents.Model
{
    /// <summary>
    /// Event RegisteredUser
    /// </summary>
    public class RegisteredUser
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RegisteredUser()
        {
            AttendeeEventDefinitions = new HashSet<EventDefinition>();
        }

        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// FacebookId
        /// </summary>
        [Required]
        public string FacebookId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Biography
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// City location
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Events that are organized by this users
        /// </summary>
        public ICollection<EventDefinition> OrganizerEventDefinitions { get; set; }

        /// <summary>
        /// Events associated with the RegisteredUser 
        /// </summary>
        public ICollection<EventDefinition> AttendeeEventDefinitions { get; set; }

        /// <summary>
        /// Session RegisteredUsers
        /// </summary>
        public ICollection<SessionRegisteredUser> SessionRegisteredUsers { get; set; }
    }
}
