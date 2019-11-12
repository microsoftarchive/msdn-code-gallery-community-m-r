
namespace MyCompany.Vacation.Data.Repositories
{
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base contract for calendar repository
    /// </summary>
    public interface ICalendarRepository
        : IDisposable
    {

        /// <summary>
        /// Gets the calendar.
        /// </summary>
        /// <param name="officeId">The office id.</param>
        /// <returns></returns>
        Calendar GetOfficeCalendar(int officeId);

        /// <summary>
        /// Add new Calendar
        /// </summary>
        /// <param name="calendar">calendar information</param>
        /// <returns>calendarId</returns>
        int Add(Calendar calendar);

        /// <summary>
        /// Update Calendar
        /// </summary>
        /// <param name="calendar">calendar information</param>
        void Update(Calendar calendar);

        /// <summary>
        /// Delete Calendar
        /// </summary>
        /// <param name="calendarId">calendar to delete</param>
        void Delete(int calendarId);

        /// <summary>
        /// Add new calendar holiday
        /// </summary>
        /// <param name="calendarHoliday">calendar holiday information</param>
        /// <returns>calendarHolidayId</returns>
        int AddHoliday(CalendarHolidays calendarHoliday);

        /// <summary>
        /// Delete calendar holiday
        /// </summary>
        /// <param name="calendarHolidayId">calendar holiday to delete</param>
        void DeleteHoliday(int calendarHolidayId);
    }
}
