
namespace MyCompany.Travel.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Data.Repositories;
using System.Threading.Tasks;

    [TestClass]
    public class TravelRequestRepositoryTests
    {
        [TestMethod]
        public async Task TravelRequestRepository_GetTravelRequest_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int TravelRequestId = context.TravelRequests.FirstOrDefault().TravelRequestId;

            var target = new TravelRequestRepository(new MyCompanyContext());
            var result = await target.GetAsync(TravelRequestId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TravelRequestId == TravelRequestId);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetAllTravelRequests_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.TravelRequests.Count();

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetCompleteInfo_Test()
        {
            int employeeId, travelRequestId;

            using (var context = new MyCompanyContext())
            {
                employeeId = context.Employees.FirstOrDefault(e => e.EmployeePictures.Any(ep => ep.PictureType == PictureType.Small) && e.Travels.Any()).EmployeeId;
                travelRequestId = context.TravelRequests.FirstOrDefault(tr => tr.EmployeeId == employeeId).TravelRequestId;
            }

            ITravelRequestRepository target = new TravelRequestRepository(new MyCompanyContext());
           
            var result = await target.GetCompleteInfoAsync(travelRequestId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TravelRequestId == travelRequestId);
            Assert.IsNotNull(result.Employee);
            Assert.IsNotNull(result.Employee.EmployeePictures);
            Assert.IsTrue(result.Employee.EmployeePictures.Any());
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetUserTravelRequests_Test()
        {
            var context = new MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 1;

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetUserTravelRequestsAsync(userIdentity, string.Empty, status, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetNotFinishedUserTravelRequests_Test()
        {
            var context = new MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetNotFinishedUserTravelRequestsAsync(userIdentity, string.Empty, status);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetUserCount_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.Include("Travels").FirstOrDefault(e => e.Travels.Any());
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var target = new TravelRequestRepository(new MyCompanyContext());
            var result = await target.GetUserCountAsync(employee.Email, string.Empty, status);

            Assert.AreEqual(employee.Travels.Count(), result);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetTeamTravelRequests_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 1;

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetTeamTravelRequestsAsync(managerIdentity, string.Empty, status, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetNotFinishedTeamTravelRequests_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetNotFinishedTeamTravelRequestsAsync(managerIdentity, string.Empty, status, PictureType.Small);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetAllTravelRequests_Test()
        {
            var context = new MyCompanyContext();
            int status = (int)(TravelRequestStatus.Approved);
            int pageSize = 1;
            int pageCount = 1;

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetAllTravelRequestsAsync(string.Empty, status, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(TravelRequestStatus.Approved, results.First().Status);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetTeamTravelDistribution_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any()).Email;

            var target = new TravelRequestRepository(new MyCompanyContext());
            var results = await target.GetTeamTravelDistributionAsync(managerIdentity, 5);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());

        }

        [TestMethod]
        public async Task TravelRequestRepository_GetTeamCount_Test()
        {
            var context = new MyCompanyContext();
            var manager = context.Employees.Include("Travels").FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any());
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var target = new TravelRequestRepository(new MyCompanyContext());
            var result = await target.GetUserCountAsync(manager.Email, string.Empty, status);

            Assert.AreEqual(manager.Travels.Count(), result);
        }

        [TestMethod]
        public async Task TravelRequestRepository_GetAllCount_Test()
        {
            var context = new MyCompanyContext();
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var target = new TravelRequestRepository(new MyCompanyContext());
            var result = await target.GetAllCountAsync(string.Empty, status);

            Assert.AreEqual(context.TravelRequests.Count(), result);
        }

        [TestMethod]
        public async Task TravelRequestRepository_AddTravelRequest_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.TravelRequests.Count() + 1;

            var target = new TravelRequestRepository(new MyCompanyContext());
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var TravelRequest = new TravelRequest()
            {
                Name = "Bussiness Travel",
                Description = "Lorem ipsum dolor sit amet.",
                From = "From",
                To = "To",
                Depart = DateTime.UtcNow.AddDays(5),
                Return = DateTime.UtcNow.AddDays(7),
                CreationDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedDate = DateTime.UtcNow,
                AccommodationNeed = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                TransportationNeed = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                RelatedProject = "MyCompany",
                Status = TravelRequestStatus.Approved,
                TravelType = TravelType.Roundtrip,
                EmployeeId = employeeId,
            };

            var travelRequest = await target.AddAsync(TravelRequest);

            int actual = context.TravelRequests.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TravelRequestRepository_UpdateTravelRequest_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var travelRequest = context.TravelRequests.FirstOrDefault();
            var target = new TravelRequestRepository(new MyCompanyContext());

            travelRequest.From = Guid.NewGuid().ToString();
            await target.UpdateAsync(travelRequest);

            var actual = target.GetAsync(travelRequest.TravelRequestId);

            Assert.AreEqual(travelRequest.From, travelRequest.From);
        }

        [TestMethod]
        public async Task TravelRequestRepository_DeleteTravelRequest_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;

            ITravelRequestRepository target = new TravelRequestRepository(context);

            var newTravelRequest = new TravelRequest()
            {
                Name = "Bussiness Travel",
                Description = "Lorem ipsum dolor sit amet.",
                From = "From",
                To = "To",
                Depart = DateTime.UtcNow.AddDays(5),
                Return = DateTime.UtcNow.AddDays(7),
                CreationDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedDate = DateTime.UtcNow,
                AccommodationNeed = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                TransportationNeed = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                RelatedProject = "MyCompany",
                Status = TravelRequestStatus.Approved,
                TravelType = TravelType.Roundtrip,
                EmployeeId = employeeId,
            };

            var travelRequestId = await target.AddAsync(newTravelRequest);

            int expected = context.TravelRequests.Count() - 1;

            await target.DeleteAsync(travelRequestId);

            int actual = context.TravelRequests.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TravelRequestRepository_DeleteTravelRequest_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.TravelRequests.Count();

            ITravelRequestRepository target = new TravelRequestRepository(new MyCompanyContext());
            await target.DeleteAsync(-1);

            int actual = context.TravelRequests.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
