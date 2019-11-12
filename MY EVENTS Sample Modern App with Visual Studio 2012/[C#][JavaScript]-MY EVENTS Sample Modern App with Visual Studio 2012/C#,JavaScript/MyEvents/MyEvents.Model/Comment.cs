using System;
using System.ComponentModel.DataAnnotations;

namespace MyEvents.Model
{
    /// <summary>
    /// Comments that users add to events
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int CommentId { get; set; }

        /// <summary>
        /// Comment`s content 
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// RegisteredUserId that add the comment
        /// </summary>
        [Required]
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// Set the  datetime when the user has done the comment
        /// </summary>
        [Required]
        public DateTime AddedDateTime { get; set; }

        /// <summary>
        /// RegisteredUser that add the comment
        /// </summary>
        public RegisteredUser RegisteredUser { get; set; }

        /// <summary>
        /// SessionId that has the comments
        /// </summary>
        [Required]
        public int SessionId { get; set; }

        /// <summary>
        /// Session that has the comments
        /// </summary>
        public Session Session { get; set; }
    }
}
