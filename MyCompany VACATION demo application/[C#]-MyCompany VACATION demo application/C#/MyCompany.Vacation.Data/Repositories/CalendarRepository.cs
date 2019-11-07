
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Linq;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// The calendar repository implementation
    /// </summary>
    public class CalendarRepository : ICalendarRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="context">the context dependency</param>
        public CalendarRepository(MyCompanyContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/>
        /// </summary>
        /// <param name="officeId"><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></param>
        public Calendar GetOfficeCalendar(int officeId)
        {
            return _context
                .Calendars
                .Include(c => c.CalendarHolidays)
                .SingleOrDefault(q => q.Offices.Any(o => o.OfficeId == officeId));
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/>
        /// </summary>
        /// <param name="calendar"><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></returns>
        public int Add(Calendar calendar)
        {
            _context.Calendars.Add(calendar);
            _context.SaveChanges();
            return calendar.CalendarId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/>
        /// </summary>
        /// <param name="calendar"><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></param>
        public void Update(Calendar calendar)
        {
            _context.Entry<Calendar>(calendar)
                .State = EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/>
        /// </summary>
        /// <param name="calendarId"><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></param>
        public void Delete(int calendarId)
        {
            var calendar = _context.Calendars
                .Find(calendarId);
            if (calendar != null)
            {
                _context.Calendars.Remove(calendar);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/>
        /// </summary>
        /// <param name="calendarHoliday"><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></returns>
        public int AddHoliday(CalendarHolidays calendarHoliday)
        {
            _context.CalendarHolidays.Add(calendarHoliday);
            _context.SaveChanges();
            return calendarHoliday.CalendarHolidaysId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/>
        /// </summary>
        /// <param name="calendarHolidayId"><see cref="MyCompany.Vacation.Data.Repositories.ICalendarRepository"/></param>
        public void DeleteHoliday(int calendarHolidayId)
        {
            var holiday = _context.CalendarHolidays
                .Find(calendarHolidayId);

            if (holiday != null)
            {
                _context.CalendarHolidays.Remove(holiday);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
