using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Expense DTO
    /// </summary>
    [Serializable]
    public class ExpenseDTO
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int ExpenseId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Expense Type
        /// </summary>
        public ExpenseTypeDTO ExpenseType { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Related Project
        /// </summary>
        public string RelatedProject { get; set; }

        /// <summary>
        /// Contact
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Picture
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last Modified Date
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        ///Expense Status
        /// </summary>
        public ExpenseStatusDTO Status { get; set; }




    }
}
