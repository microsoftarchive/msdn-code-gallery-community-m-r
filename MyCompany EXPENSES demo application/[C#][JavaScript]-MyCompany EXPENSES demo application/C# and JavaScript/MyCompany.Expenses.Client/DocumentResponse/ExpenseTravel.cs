using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Expenses.Client
{
    /// <summary>
    /// ExpenseTravel entity
    /// </summary>
    public class ExpenseTravel 
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int ExpenseId { get; set; }

        /// <summary>
        /// Distance for travel
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// From
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Expense
        /// </summary>
        public Expense Expense { get; set; }
    }
}
