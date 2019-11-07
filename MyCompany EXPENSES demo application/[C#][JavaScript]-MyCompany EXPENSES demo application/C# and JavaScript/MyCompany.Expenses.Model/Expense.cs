
namespace MyCompany.Expenses.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Expense Entity
    /// </summary>
    public class Expense 
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
        public ExpenseType ExpenseType { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// ExpenseTravelId
        /// </summary>
        public int ExpenseTravelId { get; set; }

        /// <summary>
        /// Expense Travel additional information
        /// </summary>
        public ExpenseTravel ExpenseTravel { get; set; }

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
        /// Employee
        /// </summary>
        public Employee Employee { get; set; }

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
        public ExpenseStatus Status { get; set; }




    }
}
