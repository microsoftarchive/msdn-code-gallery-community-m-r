namespace MyCompany.Vacation.Web.Notifications
{
    using MyCompany.Common.CrossCutting;
    using MyCompany.Common.Notification;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Vacation notifications service
    /// </summary>
    public class VacationNotificationService : BaseNotificationService, IVacationNotificationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Creates a new instance of VacationNotificationService
        /// </summary>
        /// <param name="emailer">service that sends the emails</param>
        /// <param name="emailTemplatesRepository">templates repository</param>
        /// <param name="employeeRepository">employees repository</param>
        public VacationNotificationService(IEmailer emailer, IEmailTemplatesRepository emailTemplatesRepository, IEmployeeRepository employeeRepository)
            : base(emailer, emailTemplatesRepository)
        {
            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Sends an email with the new status of the vacation request
        /// </summary>
        /// <param name="vacationRequest">the vacation request</param>
        /// <param name="reason">The reason.</param>
        public void NotifyStatusChange(VacationRequest vacationRequest, string reason)
        {
            try
            {
                if (null == reason)
                    reason = string.Empty;

                string baseUrl = GetBaseUrl();

                var substitutions = new Dictionary<string, string>();
                substitutions.Add("APPLICATIONURL", baseUrl);
                substitutions.Add("VACATIONMYCALENDARURL", String.Format("{0}?myCalendar", baseUrl));
                substitutions.Add("REASON", reason);

                var employee = _employeeRepository.Get(vacationRequest.EmployeeId);
                if (employee != null)
                {
                    if (vacationRequest.Status == VacationRequestStatus.Approved)
                    {
                        SendTemplate(
                            String.Format("{0} {1}", employee.FirstName, employee.LastName),
                            employee.Email,
                            "VacationApproved",
                            "Your vacation request has been approved",
                            substitutions,
                            new string[] { "logo.png" }
                            );
                    }
                    
                    if (vacationRequest.Status == VacationRequestStatus.Denied)
                    {
                        SendTemplate(
                            String.Format("{0} {1}", employee.FirstName, employee.LastName),
                            employee.Email,
                            "VacationDenied",
                            "Your vacation request has been denied",
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
        /// Sends an email notifiying a new vacation request
        /// </summary>
        /// <param name="vacationRequest">The vacation request.</param>
        public void NotifyNewVacationRequest(VacationRequest vacationRequest)
        {
            try
            {
                var employee = _employeeRepository.Get(vacationRequest.EmployeeId);
                string employeeFullName = String.Format("{0} {1}", employee.FirstName, employee.LastName);

                string baseUrl = GetBaseUrl();

                var substitutions = new Dictionary<string, string>();
                substitutions.Add("APPLICATIONURL", baseUrl);
                substitutions.Add("TEAMVACATIONURL", String.Format("{0}?teamVacation", baseUrl));
                substitutions.Add("EMPLOYEENAME", employeeFullName);

                if (employee.Team != null && employee.Team.Manager != null)
                {
                    SendTemplate(
                        String.Format("{0} {1}", employee.Team.Manager.FirstName, employee.Team.Manager.LastName),
                        employee.Team.Manager.Email,
                        "NewVacationRequest",
                        string.Format("[{0}] New vacation requested by {1}", vacationRequest.VacationRequestId, employeeFullName),
                        substitutions,
                        new string[] { "logo.png" }
                        );
                }
            } 
            catch (Exception ex)
            {
                TraceManager.TraceError(ex);
            }
        }

        /// <summary>
        /// Sends an email notifiying that a vacation request has been deleted
        /// </summary>
        /// <param name="vacationRequest">The vacation request.</param>
        public void NotifyVacationRequestDeleted(VacationRequest vacationRequest)
        {
            try
            {
                var employee = _employeeRepository.Get(vacationRequest.EmployeeId);
                string employeeFullName = String.Format("{0} {1}", employee.FirstName, employee.LastName);

                string baseUrl = GetBaseUrl();

                var substitutions = new Dictionary<string, string>();
                substitutions.Add("APPLICATIONURL", baseUrl);
                substitutions.Add("TEAMVACATIONURL", String.Format("{0}?teamVacation", baseUrl));
                substitutions.Add("EMPLOYEENAME", employeeFullName);

                if (employee.Team != null && employee.Team.Manager != null)
                {
                    SendTemplate(
                        String.Format("{0} {1}", employee.Team.Manager.FirstName, employee.Team.Manager.LastName),
                        employee.Team.Manager.Email,
                        "VacationRequestDeleted",
                        string.Format("Vacation request deleted by {0}", employeeFullName),
                        substitutions,
                        new string[] { "logo.png" }
                        );
                }
            }
            catch (Exception ex)
            {
                TraceManager.TraceError(ex);
            }
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
