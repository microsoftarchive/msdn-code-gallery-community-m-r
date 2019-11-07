namespace MyCompany.Travel.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// UniqueId
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
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// TeamId
        /// </summary>
        public int? TeamId { get; set; }

        /// <summary>
        /// Team
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Team
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// ManagedT eams
        /// </summary>
        public ICollection<Team> ManagedTeams { get; set; }

        /// <summary>
        /// Travel
        /// </summary>
        public ICollection<TravelRequest> Travels { get; set; }

        /// <summary>
        /// Employee Pictures
        /// </summary>
        public ICollection<EmployeePicture> EmployeePictures { get; set; }

        /// <summary>
        /// Employee is part of RRHH
        /// </summary> 
        public bool IsRRHH { get; set; }

        /// <summary>
        /// Employee is Manager
        /// </summary> 
        [NotMapped]
        public bool IsManager
        {
            get
            {
                return (ManagedTeams != null && ManagedTeams.Any());
            }
        }

    }
}
