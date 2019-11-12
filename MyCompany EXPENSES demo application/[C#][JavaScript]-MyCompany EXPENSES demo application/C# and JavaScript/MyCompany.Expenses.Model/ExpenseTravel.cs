namespace MyCompany.Expenses.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    /// <summary>
    /// ExpenseTravel entity
    /// </summary>
    [DataContract] 
    public class ExpenseTravel 
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [DataMember(IsRequired = true)]
        public int ExpenseId { get; set; }

        /// <summary>
        /// Distance for travel
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Distance { get; set; }

        /// <summary>
        /// From
        /// </summary>
        [DataMember(IsRequired = true)]
        public string From { get; set; }

        /// <summary>
        /// To
        /// </summary>
        [DataMember(IsRequired = true)]
        public string To { get; set; }

        /// <summary>
        /// Expense
        /// </summary>
        public Expense Expense { get; set; }
    }
}
