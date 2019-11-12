
namespace MyCompany.Vacation.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    /// <summary>
    /// Calendar holidays Controller
    /// </summary>
    public class CalendarController : ApiController
    {
        ICalendarRepository _calendarRepository = null;
        IEmployeeRepository _employeeRepository;
        ISecurityHelper _securityHelper = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="calendarRepository">ICalendarRepository dependency</param>
        /// <param name="employeeRepository">IEmployeeRepository dependency</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public CalendarController(ICalendarRepository calendarRepository, IEmployeeRepository employeeRepository, ISecurityHelper securityHelper)
        {
            if (calendarRepository == null)
                throw new ArgumentNullException("calendarRepository");

            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            _calendarRepository = calendarRepository;
            _employeeRepository = employeeRepository;
            _securityHelper = securityHelper;
        }

        /// <summary>
        /// Gets the calendar holidays
        /// </summary>
        /// <returns></returns>
        [WebApiOutputCacheAttribute(true)]
        public Calendar GetCalendar()
        {
            string email = _securityHelper.GetUser();
            Employee employee = _employeeRepository.GetByEmail(email, PictureType.Small);
            return _calendarRepository.GetOfficeCalendar(employee.OfficeId);
        }
    }
}
