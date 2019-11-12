using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Expenses.Client
{
    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee constructor.
        /// </summary>
        public Employee()
        {
            this.ManagedTeams = new List<Team>();
        }

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
        /// Full name
        /// </summary>
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }

        /// <summary>
        /// Job Title
        /// </summary>
        public string JobTitle { get; set; }

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
        public Team Team { get; set; }

        /// <summary>
        /// ManagedT eams
        /// </summary>
        public ICollection<Team> ManagedTeams { get; set; }

        /// <summary>
        /// Expenses
        /// </summary>
        public ICollection<Expense> Expenses { get; set; }

        /// <summary>
        /// Employee Pictures
        /// </summary>
        public ICollection<EmployeePicture> EmployeePictures { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is manager.
        /// </summary>
        public bool IsManager { get { return ManagedTeams.Any(); } }

    }
}
