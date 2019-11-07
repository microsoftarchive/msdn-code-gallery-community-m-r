using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Expenses.Client
{
    /// <summary>
    /// DTO to return information about expenses grouped by team member
    /// </summary>
    public class ExpenseGrouped
    {
        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// JobTitle
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Picture
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }
    }
}
