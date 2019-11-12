namespace MyCompany.Travel.Web.Notifications
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz for travel notifications
    /// </summary>
    public interface ITravelNotificationService
    {
        /// <summary>
        /// Sends an email with the new status of the travel request
        /// </summary>
        /// <param name="travelRequest">the travel request</param>
        /// <param name="reason">reason for status change</param>
        Task EmailNotifyStatusChange(MyCompany.Travel.Model.TravelRequest travelRequest, string reason);

        /// <summary>
        /// Sends an email with the new travel request
        /// </summary>
        /// <param name="travelRequest">the travel request</param>
        Task EmailNotifyNewRequest(MyCompany.Travel.Model.TravelRequest travelRequest);
        
    }
}
