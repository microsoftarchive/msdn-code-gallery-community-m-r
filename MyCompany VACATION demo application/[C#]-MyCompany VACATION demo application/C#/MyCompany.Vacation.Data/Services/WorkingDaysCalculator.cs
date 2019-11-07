namespace MyCompany.Vacation.Data.Services
{
    using MyCompany.Vacation.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Working days calculator
    /// </summary>
    public class WorkingDaysCalculator : IWorkingDaysCalculator
    {
        private readonly ICalendarRepository _calendarRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingDaysCalculator" /> class.
        /// </summary>
        public WorkingDaysCalculator(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        /// <summary>
        /// Gets the working days.
        /// </summary>
        /// <param name="officeId">The office id.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public int GetWorkingDays(int officeId, DateTime start, DateTime end)
        {
            var calendar = _calendarRepository.GetOfficeCalendar(officeId);
            var holidaysInPeriod = calendar.CalendarHolidays
                .Where(h => h.Day.DayOfWeek != DayOfWeek.Saturday && h.Day.DayOfWeek != DayOfWeek.Sunday)
                .Where(h => start.DayOfYear <= h.Day.DayOfYear && h.Day.DayOfYear <= end.DayOfYear)
                .Count();

            var notWeekendDaysInPeriod = 0;
            var currentDate = start;

            while (currentDate <= end)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    notWeekendDaysInPeriod++;
                currentDate = currentDate.AddDays(1);
            }
            
            int workingDays = notWeekendDaysInPeriod - holidaysInPeriod;
            return workingDays;
        }
    }
}
