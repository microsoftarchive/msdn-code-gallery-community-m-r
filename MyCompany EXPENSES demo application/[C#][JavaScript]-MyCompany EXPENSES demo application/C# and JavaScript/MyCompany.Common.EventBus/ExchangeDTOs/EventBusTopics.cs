using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Event Bus Topics
    /// </summary>
    public static class EventBusTopics
    {
        /// <summary>
        /// Staff
        /// </summary>
        public static readonly string Staff = "Staff";

        /// <summary>
        /// Expenses
        /// </summary>
        public static readonly string Expenses = "Expenses";

        /// <summary>
        /// Vacation
        /// </summary>
        public static readonly string Vacation = "Vacation";
    }
}
