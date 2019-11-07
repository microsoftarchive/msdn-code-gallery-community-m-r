using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pagination.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, StringLength(25), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(25), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, StringLength(20), Display(Name = "Title")]
        public string Title { get; set; }

        [Required, StringLength(100), Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required, StringLength(50), Display(Name = "Department")]
        public string DepartmentName { get; set; }
    }
}