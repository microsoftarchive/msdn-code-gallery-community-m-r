
namespace MyCompany.Vacation.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Calendar entity
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CalendarId { get; set; }

        /// <summary>
        /// Number of days of Vacation
        /// </summary>
        [Required]
        public int Vacation { get; set; }

        /// <summary>
        /// Calendar Holidays
        /// </summary>
        public ICollection<CalendarHolidays> CalendarHolidays { get; set; }

        /// <summary>
        /// Offices
        /// </summary>
        public ICollection<Office> Offices { get; set; }
    }
}
