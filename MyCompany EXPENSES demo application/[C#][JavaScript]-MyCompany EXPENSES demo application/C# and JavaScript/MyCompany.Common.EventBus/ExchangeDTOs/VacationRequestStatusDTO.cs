using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Vacation Request Status
    /// </summary>
   [Serializable]
    public enum VacationRequestStatusDTO
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Vacation is requested by employee
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Employee manager has validated the vacation
        /// </summary>
        Validated = 2,
        /// <summary>
        /// RRHH has validated the vacation, the workflow is completed!
        /// </summary>
        Approved = 3,
        /// <summary>
        /// Denied
        /// </summary>
        Denied = 4
    }
}
