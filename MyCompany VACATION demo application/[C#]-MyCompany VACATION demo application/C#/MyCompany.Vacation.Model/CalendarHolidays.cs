namespace MyCompany.Vacation.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Calendar Holidays entities
    /// </summary>
    public class CalendarHolidays
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int CalendarHolidaysId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// CalendarId
        /// </summary>
        [Required]
        public int CalendarId { get; set; }

        /// <summary>
        /// Calendar
        /// </summary>
        public Calendar Calendar { get; set; }
    }
}
