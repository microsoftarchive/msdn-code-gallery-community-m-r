using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Team DTO
    /// </summary>
    [Serializable]
    public class TeamDTO
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// ManagerId
        /// </summary>
        public int ManagerId{ get; set; }

        /// <summary>
        /// OfficeId
        /// </summary>
        public int OfficeId { get; set; }

    }
}