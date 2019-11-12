using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Expense Status
    /// </summary>
    [Serializable]
    public enum ExpenseStatusDTO
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
        Denied = 4
    }
}
