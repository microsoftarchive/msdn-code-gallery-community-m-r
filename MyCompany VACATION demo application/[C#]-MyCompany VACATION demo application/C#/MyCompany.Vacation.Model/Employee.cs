
namespace MyCompany.Vacation.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// the unique identifier for employee entities
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// JobTitle
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// TeamId
        /// </summary>
        public int? TeamId { get; set; }

        /// <summary>
        /// Team
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// OfficeId
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// Office
        /// </summary>
        public Office Office { get; set; }

        /// <summary>
        /// ManagedT eams
        /// </summary>
        public ICollection<Team> ManagedTeams { get; set; }

        /// <summary>
        /// Vacation Requests
        /// </summary>
        public ICollection<VacationRequest> VacationRequests { get; set; }

        /// <summary>
        /// Employee Pictures
        /// </summary>
        public ICollection<EmployeePicture> EmployeePictures { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is manager.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is manager; otherwise, <c>false</c>.
        /// </value>
        public bool IsManager { get { return ManagedTeams != null && ManagedTeams.Any(); } }
    }
}
