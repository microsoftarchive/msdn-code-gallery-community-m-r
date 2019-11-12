using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Calendar Holidays DTO
    /// </summary>
    [Serializable]
    public class CalendarHolidayDTO
    {
        /// <summary>
        /// UniqueId
        /// </summary>
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
        public int CalendarId { get; set; }

    }
}