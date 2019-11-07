
namespace MyCompany.Vacation.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Services;
    using System.Collections.ObjectModel;

    [TestClass]
    public class WorkingDaysCalculatorTests
    {
        [TestMethod]
        public void WorkingDaysCalculator_PeriodWithoutHolidaysOrWeekends_CountsAllTheDays_Test()
        {
            const int expected = 5;
            const int officeId = 1;
            var startDate = new DateTime(2013, 6, 17); //monday
            var endDateDate = new DateTime(2013, 6, 21); //friday
            var calendarRepository = new Data.Repositories.Fakes.StubICalendarRepository();
            calendarRepository.GetOfficeCalendarInt32 = i => new Calendar
                                                                 {
                                                                     CalendarHolidays = new Collection<CalendarHolidays>()
                                                                 };
            var workingDaysCalculator = new WorkingDaysCalculator(calendarRepository);
            
            int result = workingDaysCalculator.GetWorkingDays(officeId, startDate, endDateDate);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WorkingDaysCalculator_PeriodWithoutHolidaysAndWithAWeekend_CountsAllTheDaysMinusTheWeekend_Test()
        {
            const int expected = 5;
            const int officeId = 1;
            var startDate = new DateTime(2013, 6, 17); //monday
            var endDateDate = new DateTime(2013, 6, 23); //sunday
            var calendarRepository = new Data.Repositories.Fakes.StubICalendarRepository();
            calendarRepository.GetOfficeCalendarInt32 = i => new Calendar
            {
                CalendarHolidays = new Collection<CalendarHolidays>()
            };
            var workingDaysCalculator = new WorkingDaysCalculator(calendarRepository);
            
            int result = workingDaysCalculator.GetWorkingDays(officeId, startDate, endDateDate);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WorkingDaysCalculator_PeriodWithOneHolidaysAndWithAWeekend_CountsAllTheDaysMinusTheWeekendAndTheHoliday_Test()
        {
            const int expected = 4;
            const int officeId = 1;
            var startDate = new DateTime(2013, 6, 17); //monday
            var endDateDate = new DateTime(2013, 6, 23); //sunday
            var calendarRepository = new Data.Repositories.Fakes.StubICalendarRepository();
            calendarRepository.GetOfficeCalendarInt32 = i => new Calendar
            {
                CalendarHolidays = new Collection<CalendarHolidays>
                                       {
                                           new CalendarHolidays{ Day = new DateTime(2013, 6, 17)}
                                       }
            };
            var workingDaysCalculator = new WorkingDaysCalculator(calendarRepository);

            int result = workingDaysCalculator.GetWorkingDays(officeId, startDate, endDateDate);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WorkingDaysCalculator_PeriodWithTwoHolidaysAndWithAWeekend_CountsAllTheDaysMinusTheWeekendAndTheHolidays_Test()
        {
            const int expected = 4;
            const int officeId = 1;
            var startDate = new DateTime(2013, 6, 17); //monday
            var endDateDate = new DateTime(2013, 6, 23); //sunday
            var calendarRepository = new Data.Repositories.Fakes.StubICalendarRepository();
            calendarRepository.GetOfficeCalendarInt32 = i => new Calendar
            {
                CalendarHolidays = new Collection<CalendarHolidays>
                                       {
                                           new CalendarHolidays{ Day = new DateTime(2013, 6, 17)},
                                           new CalendarHolidays{ Day = new DateTime(2013, 6, 23)}
                                       }
            };
            var workingDaysCalculator = new WorkingDaysCalculator(calendarRepository);

            int result = workingDaysCalculator.GetWorkingDays(officeId, startDate, endDateDate);

            Assert.AreEqual(expected, result);
        }
    }
}
