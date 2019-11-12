using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Vacation Request DTO
    /// </summary>
    [Serializable]
    public class VacationRequestDTO
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int VacationRequestId { get; set; }

        /// <summary>
        /// From
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// To
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Num Days
        /// </summary>
        public int NumDays { get; set; }

        /// <summary>
        /// Vacation Request Status
        /// </summary>
        public VacationRequestStatusDTO Status { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last Modified Date
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

    }
}
