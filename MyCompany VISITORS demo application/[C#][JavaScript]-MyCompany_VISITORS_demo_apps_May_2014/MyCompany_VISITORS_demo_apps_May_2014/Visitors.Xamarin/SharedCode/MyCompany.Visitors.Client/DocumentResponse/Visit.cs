namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client.DocumentResponse;
    using System;

    /// <summary>
    /// Visit entity
    /// </summary>
    public class Visit
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int VisitId { get; set; }

        /// <summary>
        /// VisitorId
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// created DateTime
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Visit DateTime
        /// </summary>
        public DateTime VisitDateTime { get; set; }

        /// <summary>
        /// Has car? 
        /// </summary>
        public bool HasCar { get; set; }

        /// <summary>
        /// Plate
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Visitor
        /// </summary>
        public Visitor Visitor { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public VisitStatus Status { get; set; }
    }
}
