
namespace MyCompany.Travel.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Travel Entity
    /// </summary>
    [DataContract]
    public class TravelRequest
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [DataMember()]
        public int TravelRequestId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        /// <summary>
        /// Travel Type
        /// </summary>
        [DataMember(IsRequired = true)]
        public TravelType TravelType { get; set; }

        /// <summary>
        /// From city
        /// </summary>
        [DataMember(IsRequired = true)]
        public string From { get; set; }

        /// <summary>
        /// To city
        /// </summary>
        [DataMember(IsRequired = true)]
        public string To { get; set; }

        /// <summary>
        /// Depart date
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime Depart { get; set; }

        /// <summary>
        /// Return date
        /// </summary>
        [DataMember()]
        public DateTime Return { get; set; }

        /// <summary>
        /// Related Project
        /// </summary>
        [DataMember()]
        public string RelatedProject { get; set; }

        /// <summary>
        /// Transportation
        /// </summary>
        [DataMember()]
        public string TransportationNeed { get; set; }

        /// <summary>
        /// Accommodation
        /// </summary>
        [DataMember()]
        public string AccommodationNeed { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [DataMember()]
        public string Comments { get; set; }

        /// <summary>
        /// CreationDate
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last Modified Date
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        ///Travel Request Status
        /// </summary>
        [DataMember(IsRequired = true)]
        public TravelRequestStatus Status { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        [DataMember(IsRequired = true)]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee
        /// </summary>
        [DataMember()]
        public Employee Employee { get; set; }

        /// <summary>
        /// Travel Attachments
        /// </summary>
        [DataMember()]
        public ICollection<TravelAttachment> TravelAttachments { get; set; }
    }
}
