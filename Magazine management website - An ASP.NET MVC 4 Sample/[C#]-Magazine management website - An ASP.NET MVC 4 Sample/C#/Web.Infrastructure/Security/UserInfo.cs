namespace CIK.News.Web.Infras.Security
{
    using System;

    using CIK.News.Entities;
    using CIK.News.Entities.UserAgg;

    public class UserInfo
    {
        /// <summary>
        /// Get or set UserID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Get or set UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set GroupID
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Get or set DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Get or set Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Get or set DateCreated
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Get or set LastLoginTime
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// Get or set Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfo"/> class.
        /// </summary>
        public UserInfo()
        {
            this.UserId = -1;
            this.UserName = string.Empty;
            this.DisplayName = string.Empty;
            this.Email = string.Empty;
            this.DateCreated = DateTime.Now;
            this.LastLoginTime = DateTime.Now;
            this.GroupId = -1;
        }

        public UserInfo(User user)
            : this()
        {
            this.UserName = user.UserName;
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;
        }
    }
}