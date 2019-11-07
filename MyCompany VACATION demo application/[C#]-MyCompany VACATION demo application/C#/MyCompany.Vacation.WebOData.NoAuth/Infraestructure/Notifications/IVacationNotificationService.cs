namespace MyCompany.Vacation.Web.Notifications
{
    using System;
    using MyCompany.Vacation.Model;

    /// <summary>
    /// Interfaz for vacation notifications
    /// </summary>
    public interface IVacationNotificationService
    {
        /// <summary>
        /// Sends an email with the new status of the vacation request
        /// </summary>
        /// <param name="vacationRequest">the vacation request</param>
        /// <param name="reason">The reason.</param>
        void NotifyStatusChange(VacationRequest vacationRequest, string reason);


        /// <summary>
        /// Sends an email notifiying a new vacation request
        /// </summary>
        /// <param name="vacationRequest">The vacation request.</param>
        void NotifyNewVacationRequest(VacationRequest vacationRequest);

        /// <summary>
        /// Sends an email notifiying that a vacation request has been deleted
        /// </summary>
        /// <param name="vacationRequest">The vacation request.</param>
        void NotifyVacationRequestDeleted(VacationRequest vacationRequest);
    }
}
