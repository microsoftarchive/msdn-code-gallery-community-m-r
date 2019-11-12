using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Expenses.Client
{
    /// <summary>
    /// Expense Status
    /// </summary>
    public enum ExpenseStatus
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Expense is requested by employee
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Employee manager has approved the expense
        /// </summary>
        Approved = 2,
        /// <summary>
        /// Denied
        /// </summary>
        Denied = 4,
        /// <summary>
        /// All
        /// </summary>
        All = 7
    }
}
