
namespace MyCompany.Vacation.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    [TestClass]
    public class OfficeRepositoryTests
    {

        [TestMethod]
        public void OfficeRepository_AddOffice_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Offices.Count() + 1;

            var target = new OfficeRepository(context);

            var officeId = context.Offices.Select(e => e.OfficeId).Max() + 1;
            var calendarId = context.Calendars.FirstOrDefault().CalendarId;
            var office = new Office()
            {
                OfficeId = officeId,
                CalendarId = calendarId
            };

            target.Add(office);

            int actual = context.Offices.Count();
            Assert.AreEqual(expected, actual);

            target.Delete(officeId);

            actual = context.Offices.Count();
            Assert.AreEqual(expected - 1, actual);

        }

        [TestMethod]
        public void OfficeRepository_UpdateOffice_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var office = context.Offices.FirstOrDefault();
            var target = new OfficeRepository(context);

            office.CalendarId = context.Calendars.FirstOrDefault().CalendarId;
            target.Update(office);
        }

    }
}
