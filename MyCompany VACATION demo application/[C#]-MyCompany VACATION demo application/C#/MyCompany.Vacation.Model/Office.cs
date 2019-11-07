namespace MyCompany.Vacation.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Office entity
    /// </summary>
    public class Office
    {
        /// <summary>
        /// the unique identifier for office entities
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// CalendarId
        /// </summary>
        public int CalendarId { get; set; }

        /// <summary>
        /// Calendar
        /// </summary>
        public Calendar Calendar { get; set; }

        /// <summary>
        /// Employees
        /// </summary>
        public ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// Teams
        /// </summary>
        public ICollection<Team> Teams { get; set; }
    }
}
