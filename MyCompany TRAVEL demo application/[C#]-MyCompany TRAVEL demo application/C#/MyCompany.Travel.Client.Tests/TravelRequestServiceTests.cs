
namespace MyCompany.Travel.Client.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TravelRequestServiceTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetCompleteInfo_Test()
        {
            var context = new Data.MyCompanyContext();
            int travelRequestId = context.TravelRequests.FirstOrDefault().TravelRequestId;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.TravelRequestService.Get(travelRequestId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TravelRequestId == travelRequestId);
            Assert.IsNotNull(result.Employee);
            Assert.IsNotNull(result.Employee.EmployeePictures);
            Assert.IsTrue(result.Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetUserTravelRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 1;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.TravelRequestService.GetUserTravelRequests(string.Empty, status, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetNotFinishedUserTravelRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.TravelRequestService.GetNotFinishedUserTravelRequests(string.Empty, status);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetUserCount_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Include("Travels").FirstOrDefault(e => e.Travels.Any());
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.TravelRequestService.GetUserCount(string.Empty, status);

            Assert.AreEqual(employee.Travels.Count(), result);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetTeamTravelDistribution_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Where(e => e.ManagedTeams.Any()).FirstOrDefault();

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.TravelRequestService.GetTeamTravelDistribution();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }


        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetTeamTravelRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 1;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.TravelRequestService.GetTeamTravelRequests(string.Empty, status, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetNotFinishedTeamTravelRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any()).Email;
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.TravelRequestService.GetNotFinishedTeamTravelRequests(string.Empty, status, PictureType.Small);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetTeamCount_Test()
        {
            var context = new Data.MyCompanyContext();
            var manager = context.Employees.Include("Travels").FirstOrDefault(e => e.ManagedTeams.Any() && e.Travels.Any());
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.TravelRequestService.GetUserCount(string.Empty, status);

            Assert.AreEqual(manager.Travels.Count(), result);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetAllTravelRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 1;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.TravelRequestService.GetAllTravelRequests(string.Empty, status, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_GetAllCount_Test()
        {
            var context = new Data.MyCompanyContext();
            int status = (int)(TravelRequestStatus.Approved | TravelRequestStatus.Denied | TravelRequestStatus.Completed | TravelRequestStatus.Pending);

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.TravelRequestService.GetAllCount(string.Empty, status);

            Assert.AreEqual(context.TravelRequests.Count(), result);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_AddTravelRequest_Added_NotFail_Test()
        {
            var context = new Data.MyCompanyContext();
            int expected = context.TravelRequests.Count() + 1;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
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

            int travelRequestId = await client.TravelRequestService.Add(TravelRequest);

            int actual = context.TravelRequests.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_UpdateTravelRequest_NotFail_Test()
        {
            var context = new Data.MyCompanyContext();
            var travelRequest = context.TravelRequests.FirstOrDefault(r => r.Employee.EmployeePictures.Any());
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var newTravelRequest = new TravelRequest()
            {
                TravelRequestId = travelRequest.TravelRequestId,
                Name = travelRequest.Name,
                Description = travelRequest.Description,
                From = Guid.NewGuid().ToString(),
                To = travelRequest.To,
                Depart = travelRequest.Depart,
                Return = travelRequest.Return,
                CreationDate = travelRequest.CreationDate,
                LastModifiedDate = travelRequest.LastModifiedDate,
                AccommodationNeed = travelRequest.AccommodationNeed,
                TransportationNeed = travelRequest.TransportationNeed,
                Comments = travelRequest.Comments,
                RelatedProject = travelRequest.RelatedProject,
                Status = (TravelRequestStatus)(int)travelRequest.Status,
                TravelType = (TravelType)(int)travelRequest.TravelType,
                EmployeeId = travelRequest.EmployeeId,
            };

            await client.TravelRequestService.Update(newTravelRequest);

            var newContext = new Data.MyCompanyContext();
            var actual = newContext.TravelRequests.FirstOrDefault(r => r.TravelRequestId == travelRequest.TravelRequestId);

            Assert.AreEqual(newTravelRequest.From, actual.From);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelRequestService_Add_Update_Delete_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.TravelRequests.Count() + 1;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var travelRequest = new TravelRequest()
            {
                AccommodationNeed = "Rivendel, please",
                CreationDate = DateTime.UtcNow,
                Depart = DateTime.UtcNow.AddDays(2),
                Description = "I must throw the ring into the fire",
                EmployeeId = employeeId,
                From = "The Shire",
                To = "Mount of Doom",
                LastModifiedDate = DateTime.UtcNow,
                Name = "Frodo",
                RelatedProject = "The Ring",
                Return = DateTime.UtcNow.AddDays(102),
                Status = TravelRequestStatus.Pending,
                TransportationNeed = "Some eagles will be ok.",
                TravelType = Client.TravelType.Roundtrip
            };

            int id = await client.TravelRequestService.Add(travelRequest);

            int actual = context.TravelRequests.Count();
            Assert.AreEqual(expected, actual);

            travelRequest.TravelRequestId = id;
            travelRequest.Comments = "I will travel with some fellows";
            await client.TravelRequestService.Update(travelRequest);

            var actualUpdated = context.TravelRequests.Where(t => t.TravelRequestId == id).FirstOrDefault();

            Assert.AreEqual(travelRequest.Comments, actualUpdated.Comments);

            await client.TravelRequestService.Delete(id);

            actual = context.TravelRequests.Count();

            Assert.AreEqual(expected - 1, actual);
        }

    }
}
