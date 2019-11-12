namespace MyCompany.Vacation.Web.Hubs
{
    using Microsoft.AspNet.SignalR;
    using MyCompany.Common.CrossCutting;
    using MyCompany.Common.Notification;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Hub for SignalR notifications
    /// </summary>
    // [Authorize] Authorize attribute is disabled in order to support the NoAuth scenario
    public class VacationNotificationHub : BaseHub
    {
        /// <summary>
        /// Creates a new instance of VacationNotificationHub
        /// </summary>
        public VacationNotificationHub()
        {
        }

        /// <summary>
        /// Send a notification when a new TravelRequest is created
        /// </summary>
        /// <param name="vactionRequest">the TravelRequest entity</param>
        /// <param name="to">destinatary of the notification</param>
        public static void NotifyNew(VacationRequest vactionRequest, string to)
        {
            TraceManager.TraceError("NotifyNew");

            try
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<VacationNotificationHub>();

                IEnumerable<string> receivers = GetUserConnections(to);
                foreach (var cid in receivers)
                {
                    context.Clients.Client(cid).notifyNew(vactionRequest);
                }
            }
            catch (Exception ex)
            {
                TraceManager.TraceError(ex);
            }
        }
    }
}