
namespace MyCompany.Expenses.Model
{
    using System;

    /// <summary>
    /// DTO to return information about expenses grouped by month
    /// </summary>
    public class ExpenseMonth
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }
    }
}
