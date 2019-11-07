

namespace Visitors
{
    using System.Collections.Generic;

    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// The unique identifier of this employee
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
        /// JobTitle
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Employee Pictures
        /// </summary>
        public ICollection<EmployeePicture> EmployeePictures { get; set; }

        public Employee()
        {
            //PORTNOTE: EF will not initialize the collections for you yet.
            EmployeePictures = new HashSet<EmployeePicture>();
        }
    }
}
