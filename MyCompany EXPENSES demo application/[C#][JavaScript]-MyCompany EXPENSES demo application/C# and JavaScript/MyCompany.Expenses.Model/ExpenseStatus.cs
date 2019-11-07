
namespace MyCompany.Expenses.Model
{
    using System;

    /// <summary>
    /// Expense Status
    /// </summary>
    [Flags]
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
        Denied = 4
    }
}
