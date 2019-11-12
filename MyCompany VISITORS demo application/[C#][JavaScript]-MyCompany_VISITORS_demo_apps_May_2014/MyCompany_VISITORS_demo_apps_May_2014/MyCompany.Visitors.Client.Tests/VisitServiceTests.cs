
namespace MyCompany.Visitors.Client.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VisitServiceTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_Add_Update_Delete_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.Visits.Count() + 1;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var visitorId = context.Visitors.FirstOrDefault().VisitorId;
            var visit = new Visit()
            {
                CreatedDateTime = DateTime.UtcNow,
                VisitDateTime = DateTime.UtcNow.AddDays(2),
                Comments = "Comments",
                EmployeeId = employeeId,
                HasCar = true,
                Plate = "AAA-BBBB",
                VisitorId = visitorId,
            };

            int id = await client.VisitService.Add(visit);

            int actual = context.Visits.Count();
            Assert.AreEqual(expected, actual);

            visit.VisitId = id;
            visit.Comments = Guid.NewGuid().ToString();
            await client.VisitService.Update(visit);

            var actualUpdated = context.Visits.Where(t => t.VisitId == id).FirstOrDefault();

            Assert.AreEqual(visit.Comments, actualUpdated.Comments);

            await client.VisitService.Delete(id);

            actual = context.Visits.Count();

            Assert.AreEqual(expected - 1, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_Add_UpdateStatus_Delete_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.Visits.Count() + 1;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var visitorId = context.Visitors.FirstOrDefault().VisitorId;
            var visit = new Visit()
            {
                CreatedDateTime = DateTime.UtcNow,
                VisitDateTime = DateTime.UtcNow.AddDays(2),
                Comments = "Comments",
                EmployeeId = employeeId,
                HasCar = true,
                Plate = "AAA-BBBB",
                VisitorId = visitorId,
                Status = DocumentResponse.VisitStatus.Pending
            };

            int id = await client.VisitService.Add(visit);

            int actual = context.Visits.Count();
            Assert.AreEqual(expected, actual);

            await client.VisitService.UpdateStatus(id, DocumentResponse.VisitStatus.Arrived);

            var actualUpdated = context.Visits.Where(t => t.VisitId == id).FirstOrDefault();

            Assert.AreEqual((int)DocumentResponse.VisitStatus.Arrived, (int)actualUpdated.Status);

            await client.VisitService.Delete(id);

            actual = context.Visits.Count();

            Assert.AreEqual(expected - 1, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_UpdateStatus_Test()
        {
            var context = new Data.MyCompanyContext();
            int visitId = context.Visits.FirstOrDefault().VisitId;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            await client.VisitService.UpdateStatus(visitId, DocumentResponse.VisitStatus.Arrived);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_Get_Test()
        {
            var context = new Data.MyCompanyContext();
            int visitId = context.Visits.FirstOrDefault().VisitId;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.VisitService.Get(visitId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Employee);
            Assert.IsNotNull(result.Visitor);
            Assert.IsTrue(result.VisitId == visitId);
            Assert.IsTrue(result.Employee.EmployeePictures.Count == 1);
            Assert.IsTrue(result.Visitor.VisitorPictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetVisits_Test()
        {
            var context = new Data.MyCompanyContext();
            int pageSize = 1;
            int pageCount = 0;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.VisitService.GetVisits(string.Empty, PictureType.Small, pageSize, pageCount, null, null);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsNotNull(results.First().Visitor);
            Assert.IsNotNull(results.First().Visitor.VisitorPictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetVisitsFromDate_Test()
        {
            var context = new Data.MyCompanyContext();
            int pageCount = 0;
            int pageSize = 1;

            var visit = context.Visits.FirstOrDefault();

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.VisitService.GetVisitsFromDate(string.Empty, PictureType.Small, pageSize, pageCount, visit.VisitDateTime);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == pageSize);
            Assert.IsNotNull(results.First().Visitor);
            Assert.IsNotNull(results.First().Visitor.VisitorPictures);
        }

        // This test only will pass if you login as the first employee
        // hosted in the database (normally Andrew.Davis@mycompanydemo.onmicrosoft.com)
        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetUserVisits_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Include("Visits").Where(e => e.Visits.Any()).FirstOrDefault();
            int pageSize = employee.Visits.Count();
            int pageCount = 0;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.VisitService.GetUserVisits(string.Empty, PictureType.Small, pageSize, pageCount, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == pageSize);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsNotNull(results.First().Visitor);
            Assert.IsNotNull(results.First().Visitor.VisitorPictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetCount_Test()
        {
            var context = new Data.MyCompanyContext();
            int expected = context.Visits.Count();
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var actual = await client.VisitService.GetCount(string.Empty, null, null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetCount_Today_Test()
        {
            DateTime date = DateTime.UtcNow.AddDays(2).Date;
            DateTime toDate = DateTime.UtcNow.AddDays(3).Date;

            var context = new Data.MyCompanyContext();
            int expected = context.Visits.ToList().Where(v => v.VisitDateTime.Date == date).Count();

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var actual = await client.VisitService.GetCount(string.Empty, date, toDate);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetCountFromDate_Test()
        {
            var context = new Data.MyCompanyContext();
            DateTime date = context.Visits.ToList().First().VisitDateTime;

            int expected = context.Visits.Count(v => v.VisitDateTime >= date);

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var actual = await client.VisitService.GetCountFromDate(string.Empty, date);

            Assert.AreEqual(expected, actual);
        }

        // This test only will passed if you login as the first employee
        // hosted in the database. (Andrew.Davis@mycompanydemo.onmicrosoft.com)
        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitService_GetUserCount_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Include("Visits").Where(e => e.Visits.Any()).FirstOrDefault();
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var actual = await client.VisitService.GetUserCount(string.Empty, DateTime.Now.AddMonths(-1));

            Assert.AreEqual(employee.Visits.Count(), actual);
        }
    }
}
