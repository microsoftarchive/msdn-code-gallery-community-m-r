namespace MyCompany.Expenses.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Team Member Summary information
    /// </summary>
    public class TeamMemberSummary
    {
        /// <summary>
        /// Expense Type
        /// </summary>
        public ExpenseType ExpenseType { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }
    }
}
