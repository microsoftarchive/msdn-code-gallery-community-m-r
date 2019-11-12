
namespace MyCompany.Visitors.Client
{
    using System.Collections.Generic;

    /// <summary>
    /// Team entity
    /// </summary>
    public class Team
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// ManagerId
        /// </summary>
        public int ManagerId { get; set; }

        /// <summary>
        /// Manager
        /// </summary>
        public Employee Manager { get; set; }

        /// <summary>
        /// Employees
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
}
