using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Calendar DTO
    /// </summary>
    [Serializable]
    public class CalendarDTO
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int CalendarId { get; set; }

        /// <summary>
        /// Number of days of Vacation
        /// </summary>
        public int Vacation { get; set; }
    }
}