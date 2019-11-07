using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Expense Type
    /// </summary>
    [Serializable]
    public enum ExpenseTypeDTO
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Travel
        /// </summary>
        Travel = 1,
        /// <summary>
        /// Food
        /// </summary>
        Food = 2,
        /// <summary>
        /// Accommodation
        /// </summary>
        Accommodation = 3,
        /// <summary>
        /// Other
        /// </summary>
        Other = 4
    }
}
