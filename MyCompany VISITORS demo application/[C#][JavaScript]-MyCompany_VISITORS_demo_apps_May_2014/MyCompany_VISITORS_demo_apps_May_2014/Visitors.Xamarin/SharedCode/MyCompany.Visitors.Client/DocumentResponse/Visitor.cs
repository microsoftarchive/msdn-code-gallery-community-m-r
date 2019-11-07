
namespace MyCompany.Visitors.Client
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Visit entity
    /// </summary>
    public class Visitor
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// PersonalId
        /// </summary>
        public string PersonalId { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Created DateTime
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Last Modified DateTime
        /// </summary>
        public DateTime LastModifiedDateTime { get; set; }

        /// <summary>
        /// VisitorPictures
        /// </summary>
        public ICollection<VisitorPicture> VisitorPictures { get; set; }

        /// <summary>
        /// Gets or sets the last visit.
        /// </summary>
        /// <value>
        /// The last visit.
        /// </value>
        public Visit LastVisit { get; set; }

        /// <summary>
        /// Visit
        /// </summary>
        public ICollection<Visit> Visits { get; set; }
    }
}
