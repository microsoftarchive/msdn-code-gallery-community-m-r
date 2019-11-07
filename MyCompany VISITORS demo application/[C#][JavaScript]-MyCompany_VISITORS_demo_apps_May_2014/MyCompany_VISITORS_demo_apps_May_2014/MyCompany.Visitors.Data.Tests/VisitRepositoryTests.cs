namespace MyCompany.Visitors.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class VisitRepositoryTests
    {
        [TestMethod]
        public async Task VisitRepository_GetVisit_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int visitId = context.Visits.FirstOrDefault().VisitId;

            var target = new VisitRepository(context);
            var result = await target.GetAsync(visitId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.VisitId == visitId);
        }

        [TestMethod]
        public async Task VisitRepository_GetAllVisits_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Visits.Count();

            var target = new VisitRepository(context);
            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task VisitRepository_GetCompleteInfo_Test()
        {
            var context = new MyCompanyContext();
            int visitId = context.Visits.FirstOrDefault().VisitId;

            var target = new VisitRepository(context);
            var result = await target.GetCompleteInfoAsync(visitId, PictureType.Small);
        }

        [TestMethod]
        public async Task VisitRepository_GetVisits_All_Test()
        {
            var context = new MyCompanyContext();
            int pageSize = 1;
            int pageCount = 0;

            var target = new VisitRepository(context);
            var results = await target.GetVisitsAsync(string.Empty, PictureType.Small, pageSize, pageCount, null, null);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            if (results.First().Employee.EmployeePictures != null
                &&
                results.First().Employee.EmployeePictures.Any())
            {
                Assert.IsTrue(results.First().Employee.EmployeePictures.All(ep=>ep.PictureType == PictureType.Small));
            }
            Assert.IsNotNull(results.First().Visitor);
            if (results.First().Visitor.VisitorPictures != null
                &&
                results.First().Visitor.VisitorPictures.Any())
            {
                Assert.IsTrue(results.First().Visitor.VisitorPictures.All(ep=>ep.PictureType == PictureType.Small));
            }
        }

        [TestMethod]
        public async Task VisitRepository_GetVisits_ByDate_Test()
        {
            var context = new MyCompanyContext();
            int pageSize = 1;
            int pageCount = 0;

            var target = new VisitRepository(context);
            var date = context.Visits.First().VisitDateTime;

            var results = await target.GetVisitsAsync(string.Empty, PictureType.Small, pageSize, pageCount, date, null);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 1);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            if (results.First().Employee.EmployeePictures != null
                 &&
                 results.First().Employee.EmployeePictures.Any())
            {
                Assert.IsTrue(results.First().Employee.EmployeePictures.All(ep => ep.PictureType == PictureType.Small));
            }
            Assert.IsNotNull(results.First().Visitor);
            if (results.First().Visitor.VisitorPictures != null
                &&
                results.First().Visitor.VisitorPictures.Any())
            {
                Assert.IsTrue(results.First().Visitor.VisitorPictures.All(ep => ep.PictureType == PictureType.Small));
            }
        }

        [TestMethod]
        public async Task VisitRepository_GetVisitsFromDate_Test()
        {
            var context = new MyCompanyContext();
            var visit = context.Visits.First();
            int pageSize = 1;
            int pageCount = 0;
            var date = visit.VisitDateTime;

            var target = new VisitRepository(context);
            var results = await target.GetVisitsFromDateAsync(string.Empty, PictureType.Small, pageSize, pageCount, date);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Visitor);
            if (results.First().Visitor.VisitorPictures != null
                &&
                results.First().Visitor.VisitorPictures.Any())
            {
                Assert.IsTrue(results.First().Visitor.VisitorPictures.All(vp=>vp.PictureType == PictureType.Small));
            }
            
        }

        [TestMethod]
        public async Task VisitRepository_GetCount_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Visits.Count();

            var target = new VisitRepository(context);
            var actual = await target.GetCountAsync(string.Empty, null, null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitRepository_GetCountFromDate_Test()
        {
            var date = DateTime.Today;

            var context = new MyCompanyContext();
            int expected = context.Visits.Count(v => v.VisitDateTime >= date);

            var target = new VisitRepository(context);
            var actual = await target.GetCountFromDateAsync(string.Empty, date);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitRepository_GetCount_Today_Test()
        {
            var context = new MyCompanyContext();
            DateTime date = DateTime.UtcNow.AddDays(2).Date;
            DateTime toDate = DateTime.UtcNow.AddDays(3).Date;

            int expected = context.Visits.ToList().Count(v => v.VisitDateTime.Date.Equals(date));
            var target = new VisitRepository(context);
            var actual = await target.GetCountAsync(string.Empty, date, toDate);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitRepository_GetUserCount_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.Include("Visits").Where(e => e.Visits.Any()).FirstOrDefault();
            var target = new VisitRepository(context);
            var actual = await target.GetUserCountAsync(employee.Email, string.Empty);

            Assert.AreEqual(employee.Visits.Count(), actual);
        }

        [TestMethod]
        public async Task VisitRepository_GetUserVisits_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.Include("Visits").Where(e => e.Visits.Any()).FirstOrDefault();
            int pageSize = employee.Visits.Count();
            int pageCount = 0;

            var target = new VisitRepository(context);
            var results = await target.GetUserVisitsAsync(employee.Email, string.Empty, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == pageSize);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Visitor);
        }

        [TestMethod]
        public async Task VisitRepository_AddVisit_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Visits.Count() + 1;

            var target = new VisitRepository(context);
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var visitorId = context.Visitors.FirstOrDefault().VisitorId;
            var Visit = new Visit()
            {
                CreatedDateTime = DateTime.UtcNow,
                VisitDateTime = DateTime.UtcNow.AddDays(2),
                Comments = "Comments",
                EmployeeId = employeeId,
                HasCar = true,
                Plate = "AAA-BBBB",
                VisitorId = visitorId,
            };

            await target.AddAsync(Visit);

            int actual = context.Visits.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitRepository_UpdateVisit_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var visit = context.Visits.FirstOrDefault();
            var target = new VisitRepository(context);

            visit.Comments = Guid.NewGuid().ToString();
            await target.UpdateAsync(visit);

            var actual = await target.GetAsync(visit.VisitId);

            Assert.AreEqual(visit.Comments, actual.Comments);
        }

        [TestMethod]
        public async Task VisitRepository_DeleteVisit_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            IVisitRepository target = new VisitRepository(context);

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var visitorId = context.Visitors.FirstOrDefault().VisitorId;
            var newVisit = new Visit()
            {
                CreatedDateTime = DateTime.UtcNow,
                VisitDateTime = DateTime.UtcNow.AddDays(2),
                Comments = "Comments",
                EmployeeId = employeeId,
                HasCar = true,
                Plate = "AAA-BBBB",
                VisitorId = visitorId,
            };

            int visitId = await target.AddAsync(newVisit);

            int expected = context.Visits.Count() - 1;


            await target.DeleteAsync(visitId);

            int actual = context.Visits.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitRepository_DeleteVisit_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Visits.Count();

            IVisitRepository target = new VisitRepository(context);
            await target.DeleteAsync(-1);

            int actual = context.Visits.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
