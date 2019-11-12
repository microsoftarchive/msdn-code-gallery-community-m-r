namespace MyCompany.Travel.Web.Notifications
{
    using MyCompany.Common.CrossCutting;
    using MyCompany.Common.Notification;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Travel notifications service
    /// </summary>
    public class TravelNotificationService : BaseNotificationService, ITravelNotificationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Creates a new instance of TravelNotificationService
        /// </summary>
        /// <param name="emailer">service that sends the emails</param>
        /// <param name="emailTemplatesRepository">templates repository</param>
        /// <param name="employeeRepository">employees repository</param>
        public TravelNotificationService(IEmailer emailer, IEmailTemplatesRepository emailTemplatesRepository, IEmployeeRepository employeeRepository) : base (emailer, emailTemplatesRepository)
        {
            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Sends an email with the new status of the travel request
        /// </summary>
        /// <param name="travelRequest">the travel request</param>
        /// <param name="reason">reason for status change</param>
        public async Task EmailNotifyStatusChange(TravelRequest travelRequest, string reason)
        {
            try
            {
                string baseUrl = GetBaseUrl();

                var substitutions = new Dictionary<string, string>();
                substitutions.Add("APPLICATIONURL", baseUrl);
                substitutions.Add("TRAVELDETAILURL", String.Format("{0}?travelId={1}", baseUrl, travelRequest.TravelRequestId));

                var employee = await _employeeRepository.GetAsync(travelRequest.EmployeeId);
                if (employee != null)
                {
                    if (travelRequest.Status == TravelRequestStatus.Approved)
                    {
                        SendTemplate(
                            String.Format("{0} {1}", employee.FirstName, employee.LastName),
                            employee.Email,
                            "TravelApproved",
                            "Your travel request has been approved",
                            substitutions,
                            new string[] { "logo.png" }
                            );
                    }

                    if (travelRequest.Status == TravelRequestStatus.Denied)
                    {
                        substitutions.Add("REASON", reason);
                        SendTemplate(
                            String.Format("{0} {1}", employee.FirstName, employee.LastName),
                            employee.Email,
                            "TravelDenied",
                            "Your travel request has been denied",
                            substitutions,
                            new string[] { "logo.png" }
                            );
                    }

                    if (travelRequest.Status == TravelRequestStatus.Completed)
                    {
                        SendTemplate(
                            String.Format("{0} {1}", employee.FirstName, employee.LastName),
                            employee.Email,
                            "TravelCompleted",
                            "Your travel request has been completed",
                            substitutions,
                            new string[] { "logo.png" }
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                TraceManager.TraceError(ex);
            }
        }

        /// <summary>
        /// Sends an email with the new travel request
        /// </summary>
        /// <param name="travelRequest">the travel request</param>
        public async Task EmailNotifyNewRequest(MyCompany.Travel.Model.TravelRequest travelRequest)
        {
            string baseUrl = GetBaseUrl();
            var employee = await _employeeRepository.GetAsync(travelRequest.EmployeeId);
            
            var substitutions = new Dictionary<string, string>();
            substitutions.Add("APPLICATIONURL", baseUrl);
            substitutions.Add("TRAVELDETAILURL", String.Format("{0}?travelRequest={1}", baseUrl, travelRequest.TravelRequestId));
            substitutions.Add("EMPLOYEENAME", String.Format("{0} {1}", employee.FirstName, employee.LastName));

            SendTemplate(
                String.Format("{0} {1}", employee.Team.Manager.FirstName, employee.Team.Manager.LastName),
                employee.Team.Manager.Email,
                "NewTravel",
                "You have received a new travel request",
                substitutions,
                new string[] { "logo.png" }
                );

        }

        private string GetBaseUrl()
        {
            string baseUrl = string.Empty;
                    if (HttpContext.Current != null)
                    {
                        UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                        baseUrl = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, urlHelper.Content("~"));
                    }
            return baseUrl;
        }
    }
}
