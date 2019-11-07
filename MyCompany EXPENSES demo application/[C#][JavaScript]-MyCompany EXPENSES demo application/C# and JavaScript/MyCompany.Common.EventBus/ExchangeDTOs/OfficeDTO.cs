using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Office DTO
    /// </summary>
    [Serializable]
    public class OfficeDTO
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// CalendarId
        /// </summary>
        public int CalendarId { get; set; }
    }
}