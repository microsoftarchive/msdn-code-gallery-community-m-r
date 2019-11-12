
namespace MyCompany.Travel.Client
{
    using System.Collections.Generic;

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
        public bool IsManager { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}",FirstName, LastName);
            }
        }

        /// <summary>
        /// Check if two employees are the same
        /// </summary>
        /// <param name="obj">employee to compare</param>
        /// <returns>A boolean indicating if both employees are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Employee))
                return false;

            return this.EmployeeId == (obj as Employee).EmployeeId;
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>the hash code</returns>
        public override int GetHashCode()
        {
            return this.EmployeeId.GetHashCode();
        }
    }
}
