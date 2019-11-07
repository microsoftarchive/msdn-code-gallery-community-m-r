namespace MyCompany.Travel.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Controllers;
    using MyCompany.Travel.Data;
    using MyCompany.Common.Notification;
    using MyCompany.Travel.Web.Notifications;
    using MyCompany.Travel.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class TravelRequestsControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelRequestsController_Constructor_Null_TravelRequestRepository_Test()
        {
            ITravelRequestRepository travelRequestsRepository = null;
            IEmployeeRepository employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            ITravelNotificationService notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();
            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelRequestsController_Constructor_Null_EmployeeRepository_Test()
        {
            ITravelRequestRepository travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            IEmployeeRepository employeeRepository = null;
            ITravelNotificationService notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();
            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelRequestsController_Constructor_Null_TravelNotificationService_Test()
        {
            ITravelRequestRepository travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            IEmployeeRepository employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            ITravelNotificationService notificationService = null;
            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelRequestsController_Constructor_Null_SecurityHelper_Test()
        {
            ITravelRequestRepository travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            IEmployeeRepository employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            ITravelNotificationService notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();
            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, null, notificationService);
        }

        [TestMethod]
        public async Task TravelRequestsController_Add_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            var newTravelRequests = new TravelRequest()
            {
                TravelRequestId = 1,
            };

            travelRequestsRepository.AddAsyncTravelRequest = (TravelRequest) =>
            {
                Assert.IsTrue(newTravelRequests.TravelRequestId == TravelRequest.TravelRequestId);
                called = true;

                return Task.FromResult( 10 );
            };

            travelRequestsRepository.GetAsyncInt32 = (travelRequestId) =>
                {
                    return Task.FromResult(new TravelRequest()
                    {
                        TravelRequestId = travelRequestId
                    });
                };

            travelRequestsRepository.GetWithEmployeeInfoAsyncInt32 = (travelRequestId) =>
            {
                return Task.FromResult(new TravelRequest()
                {
                    TravelRequestId = travelRequestId
                });
            };

            employeeRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult(new Employee
                {
                    EmployeeId = id,
                    Team = new Team()
                    {
                        Manager = new Employee()
                        {
                            EmployeeId = 1,
                            Email = "manager@managersdomain.com"
                        }
                    }
                });
            };

            notificationService.EmailNotifyNewRequestTravelRequest = (travelRequest) =>
            {
                return Task.FromResult(string.Empty);
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.Add(newTravelRequests);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TravelRequestsController_Add_Exception_Test()
        {
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();
            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.Add(null);
        }

        [TestMethod]
        public async Task TravelRequestsController_Update_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            var updateTravelRequests = new TravelRequest()
            {
                TravelRequestId = 1,
            };

            travelRequestsRepository.UpdateAsyncTravelRequest = (TravelRequest) =>
            {
                Assert.IsTrue(updateTravelRequests.TravelRequestId == TravelRequest.TravelRequestId);
                called = true;

                return Task.FromResult(string.Empty);
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.Update(updateTravelRequests);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TravelRequestsController_Update_Exception_Test()
        {
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();
            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.Update(null);
        }

        [TestMethod]
        public async Task TravelRequestsController_Get_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetCompleteInfoAsyncInt32PictureType = (id, picture) =>
            {
                called = true;

                return Task.FromResult( new TravelRequest());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.Get(1, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_GetUserTravelRequests_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetUserTravelRequestsAsyncStringStringInt32Int32Int32 = (id, filter, status, size, count) =>
            {
                called = true;
                return Task.FromResult( new List<TravelRequest>().AsEnumerable());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetUserTravelRequests(string.Empty, 1, 1, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_GetNotFinishedUserTravelRequests_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetNotFinishedUserTravelRequestsAsyncStringStringInt32 = (id, filter, status) =>
            {
                called = true;

                return Task.FromResult(new List<TravelRequest>().AsEnumerable());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetNotFinishedUserTravelRequests(string.Empty, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_GetAllTravelRequests_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetAllTravelRequestsAsyncStringInt32PictureTypeInt32Int32 = (filter, status, picture, size, count) =>
            {
                called = true;

                return Task.FromResult(new List<TravelRequest>().AsEnumerable());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetAllTravelRequests(string.Empty, 1, PictureType.Small, 1, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_GetTeamTravelRequests_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetTeamTravelRequestsAsyncStringStringInt32PictureTypeInt32Int32 = (managerIdentity, filter, status, picture, size, count) =>
            {
                called = true;

                return Task.FromResult(new List<TravelRequest>().AsEnumerable());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetTeamTravelRequests(string.Empty, 1, PictureType.Small, 1, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_GetNotFinishedTeamTravelRequests_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetNotFinishedTeamTravelRequestsAsyncStringStringInt32PictureType = (managerIdentity, filter, status, picture) =>
            {
                called = true;

                return Task.FromResult(new List<TravelRequest>().AsEnumerable());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetNotFinishedTeamTravelRequests(string.Empty, 1, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }
        

        [TestMethod]
        public async Task TravelRequestsController_GetTeamTravelDistribution_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetTeamTravelDistributionAsyncStringInt32 = (id, numPictures) =>
            {
                called = true;

                return Task.FromResult(new List<TravelDistribution>().AsEnumerable());
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetTeamTravelDistribution(5);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }      

        [TestMethod]
        public async Task TravelRequestsController_GetUserCount_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetUserCountAsyncStringStringInt32 = (id, filter, status) =>
            {
                called = true;

                return Task.FromResult(10);
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetUserCount(string.Empty, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void TravelRequestsController_GetTeamCount_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetTeamCountStringStringInt32 = (id, filter, status) =>
            {
                called = true;

                return 10;
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = target.GetTeamCount(string.Empty, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_GetAllCount_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetAllCountAsyncStringInt32 = (filter, status) =>
            {
                called = true;

                return Task.FromResult(10);
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            var result = await target.GetAllCount(string.Empty, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_UpdateStatus_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult( new TravelRequest());
            };

            travelRequestsRepository.UpdateAsyncTravelRequest = (travel) =>
            {
                Assert.AreEqual(TravelRequestStatus.Completed, travel.Status);
                Assert.AreEqual("comments", travel.Comments);
                called = true;

                return Task.FromResult(string.Empty);
            };

            notificationService.EmailNotifyStatusChangeTravelRequestString = (travelRequest, comments) =>
            {
                Assert.AreEqual(TravelRequestStatus.Completed, travelRequest.Status);
                return Task.FromResult(string.Empty);
            };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.UpdateStatus(1, TravelRequestStatus.Completed, "comments");

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_UpdateStatus_Approved_Test()
        {
            bool called = false;
            var travelRequestsRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            travelRequestsRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult( new TravelRequest());
            };

            travelRequestsRepository.UpdateAsyncTravelRequest = (travel) =>
            {
                Assert.AreEqual(TravelRequestStatus.Approved, travel.Status);
                Assert.AreEqual("comments", travel.Comments);
                called = true;

                return Task.FromResult(string.Empty);
            };

            notificationService.EmailNotifyStatusChangeTravelRequestString = (travelRequest, comments) =>
                {
                    Assert.AreEqual(TravelRequestStatus.Approved, travelRequest.Status);
                    return Task.FromResult(string.Empty);
                };

            var target = new TravelRequestsController(travelRequestsRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.UpdateStatus(1, TravelRequestStatus.Approved, "comments");

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task TravelRequestsController_Delete_Test()
        {
            bool called = false;
            var travelRequestRepository = new Data.Repositories.Fakes.StubITravelRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var notificationService = new Web.Notifications.Fakes.StubITravelNotificationService();

            var travelRequest = new TravelRequest()
            {
                TravelRequestId = 1,
            };

            travelRequestRepository.DeleteAsyncInt32 = (id) =>
            {
                Assert.IsTrue(id == travelRequest.TravelRequestId);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new TravelRequestsController(travelRequestRepository, employeeRepository, new SecurityHelper(), notificationService);
            await target.Delete(travelRequest.TravelRequestId);

            Assert.IsTrue(called);
        }
    }
}