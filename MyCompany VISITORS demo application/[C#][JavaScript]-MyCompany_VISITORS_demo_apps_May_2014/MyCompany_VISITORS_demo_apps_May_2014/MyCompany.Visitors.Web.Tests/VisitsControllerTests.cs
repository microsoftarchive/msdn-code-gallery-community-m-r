namespace MyCompany.Visitors.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Model;
    using MyCompany.Visitors.Web.Controllers;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class VisitsControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Visit repository is null.")]
        public void VisitsController_Constructor_Failed_FirstArgument_test()
        {
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var target = new VisitsController(null,new SecurityHelper(), employeeRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Secutiry helper is null.")]
        public void VisitsController_Constructor_Failed_SecondArgument_test()
        {
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var target = new VisitsController(visitRepository, null, employeeRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Employee repository is null.")]
        public void VisitsController_Constructor_Failed_ThirdArgument_test()
        {
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var target = new VisitsController(visitRepository, new SecurityHelper(), null);
        }

        [TestMethod]
        public async Task VisitsController_Get_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            visitRepository.GetCompleteInfoAsyncInt32PictureType = (id, picture) =>
            {
                called = true;
                return Task.FromResult(new Visit());
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.Get(0, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_GetVisits_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var timeOffset = DateTimeOffset.Now.Offset;

            visitRepository.GetVisitsAsyncStringPictureTypeInt32Int32NullableOfDateTimeNullableOfDateTime = (filter, picture, pageSize, pageCount, dateFilter, toDate) =>
            {
                called = true;
                return Task.FromResult(new List<Visit>().AsEnumerable());
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.GetVisits(string.Empty, PictureType.Small, 1, 1, null, null);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_GetVisitsFromDate_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var timeOffset = DateTimeOffset.Now.Offset;

            visitRepository.GetVisitsFromDateAsyncStringPictureTypeInt32Int32DateTime = (filter, picture, pageSize, pageCount, dateFilter) =>
            {
                called = true;
                return Task.FromResult(new List<Visit>().AsEnumerable());
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.GetVisitsFromDate(string.Empty, PictureType.Small, 1, 1, DateTime.Now);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_GetUserVisits_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            visitRepository.GetUserVisitsAsyncStringStringPictureTypeInt32Int32 = (identity, filter, picture, pageSize, pageCount) =>
            {
                called = true;
                return Task.FromResult(new List<Visit>().AsEnumerable());
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.GetUserVisits(string.Empty, PictureType.Small, 1, 1, DateTime.UtcNow);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_GetCount_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            visitRepository.GetCountAsyncStringNullableOfDateTimeNullableOfDateTime = (filter, datetime, toDateTime) =>
            {
                Assert.IsNull(datetime);
                called = true;
                return Task.FromResult(10);
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.GetCount(string.Empty, null, null);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_GetContFromDate_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            visitRepository.GetCountFromDateAsyncStringDateTime = (filter, datetime) =>
            {
                Assert.IsNotNull(datetime);
                called = true;
                return Task.FromResult(20);
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.GetCountFromDate(string.Empty, DateTime.Today);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_GetUserCount_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            visitRepository.GetUserCountAsyncStringString = (user, filter) =>
            {
                called = true;
                return Task.FromResult(10);
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            var result = await target.GetUserCount(string.Empty, DateTime.UtcNow);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_Add_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            var visit = new Visit()
            {
                VisitId = 1,
            };

            visitRepository.AddAsyncVisit = (visitParam) =>
            {
                Assert.IsTrue(visitParam.VisitId == visit.VisitId);
                called = true;
                return Task.FromResult(10);
            };

            visitRepository.GetCompleteInfoAsyncInt32PictureType = (visitId, pictureType) =>
            {
                return Task.FromResult(new Visit());
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            await target.Add(visit);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Visit is null.")]
        public async Task VisitsController_Add_Failed_Test()
        {
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            await target.Add(null);
        }

        [TestMethod]
        public async Task VisitsController_Update_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            var visit = new Visit()
            {
                VisitId = 1,
            };

            visitRepository.UpdateAsyncVisit = (visitParam) =>
            {
                Assert.IsTrue(visitParam.VisitId == visit.VisitId);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            await target.Update(visit);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Visit is null.")]
        public async Task VisitsController_Update_Failed_Test()
        {
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            await target.Update(null);
        }

        [TestMethod]
        public async Task VisitsController_UpdateStatus_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            var visit = new Visit()
            {
                VisitId = 1,
                Status = VisitStatus.Pending,
                EmployeeId = 1,
            };

            visitRepository.GetCompleteInfoAsyncInt32PictureType = (id, visitPicture) =>
            {
                return Task.FromResult(visit);
            };

            employeeRepository.GetCompleteInfoAsyncInt32PictureType = (id, pictureType) =>
            {
                Employee employee = null;
                if (id == visit.EmployeeId)
                {
                    employee = new Employee()
                    {
                        EmployeeId = visit.EmployeeId,
                        Email = "fake@mail.com"
                    };
                }

                return Task.FromResult(employee);
            };

            visitRepository.UpdateAsyncVisit = (visitParam) =>
            {
                Assert.IsTrue(visitParam.VisitId == visit.VisitId);
                Assert.IsTrue(visitParam.Status == VisitStatus.Arrived);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            await target.UpdateStatus(visit.VisitId, VisitStatus.Arrived);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitsController_Delete_Test()
        {
            bool called = false;
            var visitRepository = new Data.Repositories.Fakes.StubIVisitRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            var visit = new Visit()
            {
                VisitId = 1,
            };

            visitRepository.DeleteAsyncInt32 = (id) =>
            {
                Assert.IsTrue(id == visit.VisitId);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new VisitsController(visitRepository, new SecurityHelper(), employeeRepository);
            await target.Delete(visit.VisitId);

            Assert.IsTrue(called);
        }
    }
}
