using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableToViewMigrationDemo
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public int? ReportsToEmployeeID { get; set; }
        public virtual Employee ReportsTo { get; set; }
        public virtual ICollection<Employee> Manages { get; set; }
    }
}
