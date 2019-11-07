
namespace MyCompany.Vacation.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using MyCompany.Vacation.Model.Validators;

    /// <summary>
    /// Vacation Request Entity
    /// </summary>
    [DataContract]
    public class VacationRequest
    {
        /// <summary>
        /// the unique identifier for vacation request entities
        /// </summary>
        [DataMember]
        public int VacationRequestId { get; set; }

        /// <summary>
        /// From
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime From { get; set; }

        /// <summary>
        /// To
        /// </summary>
        [DataMember(IsRequired = true)]
        [DateGreaterorEqualThan("From")]
        public DateTime To { get; set; }

        /// <summary>
        /// Num Days
        /// </summary>
        [DataMember(IsRequired = true)]
        public int NumDays { get; set; }

        /// <summary>
        /// Vacation Request Status
        /// </summary>
        [DataMember(IsRequired = true)]
        public VacationRequestStatus Status { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [DataMember]
        public string Comments { get; set; }

        /// <summary>
        /// Creation Date
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last Modified Date
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        [DataMember(IsRequired = true)]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee
        /// </summary>
        [DataMember]
        public Employee Employee { get; set; }
    }
}
