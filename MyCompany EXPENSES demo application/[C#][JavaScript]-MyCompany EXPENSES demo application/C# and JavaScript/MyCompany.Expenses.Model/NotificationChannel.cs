namespace MyCompany.Expenses.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// NotificationChannel Entity
    /// </summary>
    public class NotificationChannel 
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int NotificationChannelId { get; set; }

        /// <summary>
        /// Channel Uri
        /// </summary>
        public string ChannelUri { get; set; }

        /// <summary>
        /// Notification Type
        /// </summary>
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee
        /// </summary>
        public Employee Employee { get; set; }
    }
}
