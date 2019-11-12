using System.ComponentModel.DataAnnotations;

namespace MyEvents.Model
{
    /// <summary>
    /// registeredUsers that will go to the session
    /// </summary>
    public class SessionRegisteredUser
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int SessionRegisteredUserId { get; set; }

        /// <summary>
        /// SessionId
        /// </summary>
        [Required]
        public int SessionId { get; set; }

        /// <summary>
        /// RegisteredUserId
        /// </summary>
        [Required]
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// FacebookId
        /// </summary>
        [Required]
        public string FacebookId { get; set; }

        /// <summary>
        /// Session Score
        /// </summary>
        [Range(0,5)]
        public double Score { get; set; }

        /// <summary>
        /// true is the user has rate the session
        /// </summary>
        [Required]
        public bool Rated { get; set; }
    }
}
