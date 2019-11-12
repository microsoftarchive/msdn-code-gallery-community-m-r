using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyEvents.Api.Client
{
    /// <summary>
    /// Comments that users add to events
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Comment`s content 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// RegisteredUserId that add the comment
        /// </summary>
        public int RegisteredUserId { get; set; }

        /// <summary>
        /// RegisteredUser that add the comment
        /// </summary>
        public RegisteredUser RegisteredUser { get; set; }

        /// <summary>
        /// Set the  datetime when the user has done the comment
        /// </summary>
        public DateTime AddedDateTime { get; set; }

        /// <summary>
        /// SessionId that has the comments
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Session that has the comments
        /// </summary>
        public Session Session { get; set; }
    }
}
