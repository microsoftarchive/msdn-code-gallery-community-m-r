
namespace MyCompany.Vacation.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    [TestClass]
    public class CalendarRepositoryTests
    {
        [TestMethod]
        public void CalendarRepository_GetOfficeCalendar_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var calendarWithOffices = 
                context.Calendars
                .Include("Offices")
                .First(c => c.Offices.Any());
            
            var officeId = calendarWithOffices.Offices.First().OfficeId;

            var target = new CalendarRepository(context);
            var result = target.GetOfficeCalendar(officeId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CalendarRepository_AddCalendar_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Calendars.Count() + 1;

            var target = new CalendarRepository(context);

            var calendarId = context.Calendars.Select(e => e.CalendarId).Max() + 1;
            var calendar = new Calendar()
            {
                CalendarId = calendarId,
                Vacation = 12,
            };

            target.Add(calendar);

            int actual = context.Calendars.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalendarRepository_UpdateCalendar_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var calendar = context.Calendars.FirstOrDefault();
            var target = new CalendarRepository(context);

            calendar.Vacation = calendar.Vacation + 20;
            target.Update(calendar);

            var newContext = new MyCompanyContext();
            var actual = newContext.Calendars.FirstOrDefault(c => c.CalendarId == calendar.CalendarId);

            Assert.AreEqual(calendar.Vacation, actual.Vacation);
        }

        [TestMethod]
        public void CalendarRepository_DeleteCalendar_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();

            var calendarId = context.Calendars.Select(e => e.CalendarId).Max() + 1;
            var calendar = new Calendar()
            {
                CalendarId = calendarId,
                Vacation = 12,
            };

            ICalendarRepository target = new CalendarRepository(context);
            target.Add(calendar);
            target.Delete(calendarId);
        }

        [TestMethod]
        public void CalendarRepository_DeleteCalendar_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Calendars.Count();

            ICalendarRepository target = new CalendarRepository(context);
            target.Delete(0);

            int actual = context.Calendars.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalendarRepository_AddCalendarHoliday_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.CalendarHolidays.Count() + 1;

            var target = new CalendarRepository(context);

            var calendarHolidaysId = context.CalendarHolidays.Select(e => e.CalendarHolidaysId).Max() + 1;
            var calendarId = context.CalendarHolidays.First().CalendarId;
            var calendar = new CalendarHolidays()
            {
                CalendarId = calendarId,
                Day = DateTime.UtcNow,
                Name = "name",
                CalendarHolidaysId = calendarHolidaysId
            };

            target.AddHoliday(calendar);

            int actual = context.CalendarHolidays.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalendarRepository_DeleteCalendarHoliday_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var calendar = context.CalendarHolidays.FirstOrDefault();
            int expected = context.CalendarHolidays.Count() - 1;

            ICalendarRepository target = new CalendarRepository(context);
            target.DeleteHoliday(calendar.CalendarHolidaysId);

            int actual = context.CalendarHolidays.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
