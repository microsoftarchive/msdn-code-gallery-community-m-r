
namespace MyCompany.Vacation.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;

    [TestClass]
    public class VacationRequestsController_Integration_Tests
    {
        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_Add_Update_Delete_Test()
        {
            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.VacationRequests.Count() + 1;

            var employeeId = context.Employees.First().EmployeeId; 

            var vacationRequest = new VacationRequest()
            {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow,
                NumDays = 5,
                Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreationDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedDate = DateTime.UtcNow,
                Status = VacationRequestStatus.Approved,
                EmployeeId = employeeId,
            };

            string url = String.Format(CultureInfo.InvariantCulture
             , "{0}api/vacationRequests", SecurityHelper.UrlBase);

            var vacationRequestId = await request.PostAsync<int, VacationRequest>(url, vacationRequest);

            int actual = context.VacationRequests.Count();
            Assert.AreEqual(expected, actual);

            url = String.Format(CultureInfo.InvariantCulture
             , "{0}api/vacationRequests/{1}", SecurityHelper.UrlBase, vacationRequestId);

            await request.DeleteAsync(url);

            actual = context.VacationRequests.Count();

            Assert.AreEqual(expected - 1, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_UpdateStatus_Test()
        {
            var context = new Data.MyCompanyContext();
            var vacationRequest = context.VacationRequests.FirstOrDefault();
            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/{1}/status/{2}?reason=''", SecurityHelper.UrlBase, vacationRequest.VacationRequestId, (int)VacationRequestStatus.Pending);

            await request.PutAsync(url, string.Empty);

            var context2 = new Data.MyCompanyContext();
            var actual = context2.VacationRequests.Where(v => v.VacationRequestId == vacationRequest.VacationRequestId).First().Status;

            Assert.AreEqual((int)VacationRequestStatus.Pending, (int)actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_GetUserVacationRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Include("VacationRequests").FirstOrDefault(e => e.VacationRequests.Any());
            string identity = employee.Email;
            int year = employee.VacationRequests.First().From.Year;

            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/{1}/user", SecurityHelper.UrlBase, year);

            var results = await request.GetAsync<List<VacationRequest>>(url);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() > 0);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_GetUserCount_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Include("VacationRequests").FirstOrDefault(e => e.VacationRequests.Any());
            string identity = employee.Email;
            int? month = employee.VacationRequests.First().From.Month;
            int year = employee.VacationRequests.First().From.Year;
            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);
            int expected = employee.VacationRequests.Count(v => v.From.Month == month && v.From.Year == year);

            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/user/count?month={1}&year={2}&status={3}", SecurityHelper.UrlBase,
                month, year, status);
            var actual = await request.GetAsync<int>(url);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_GetUserPendingVacation_Test()
        {
            var context = new Data.MyCompanyContext();
            var employee = context.Employees.Include("VacationRequests").FirstOrDefault(e => e.VacationRequests.Any());
            string identity = employee.Email;
            int year = employee.VacationRequests.First().From.Year;
       
            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/{1}/user/pending", SecurityHelper.UrlBase, year);

            var actual = await request.GetAsync<int>(url);

            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_GetTeamVacationRequests_Test()
        {
            var context = new Data.MyCompanyContext();
            var manager = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any());
            string identity = manager.Email;

            var vacation = context.VacationRequests.FirstOrDefault();
            int? month = vacation.From.Month;
            int year = vacation.From.Year;

            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 0;

            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/team?month={1}&year={2}&status={3}&pageSize={4}&pageCount={5}&PictureType={6}&filter=", SecurityHelper.UrlBase,
                month, year, status, pageSize, pageCount, (int)PictureType.Small);

            var results = await request.GetAsync<List<VacationRequest>>(url);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_GetTeamCount_Test()
        {
            var context = new Data.MyCompanyContext();
            var team = context.Teams.Include("Manager").Where(t => t.Employees.Any()).FirstOrDefault();

            string identity = team.Manager.Email;

            var vacation = context.VacationRequests.FirstOrDefault(v => v.Employee.TeamId == team.TeamId);
            int? month = vacation.From.Month;
            int year = vacation.From.Year;

            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);

            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/team/count?month={1}&year={2}&status={3}&filter=", SecurityHelper.UrlBase, month, year, status);

            var actual = await request.GetAsync<int>(url);

            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_VacationRequestsController_GetTeamVacationRequestsByEmployee_Test()
        {
            var context = new Data.MyCompanyContext();
            var team = context.Teams.Include("Manager").Where(t => t.Employees.Any()).FirstOrDefault();

            string identity = team.Manager.Email;
            var vacation = context.VacationRequests.FirstOrDefault();
            int? month = vacation.From.Month;
            int year = vacation.From.Year;
            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);

            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/team/employee?month={1}&year={2}&status={3}&PictureType={4}", SecurityHelper.UrlBase,
                month, year, status, PictureType.Small);

            var results = await request.GetAsync<List<Employee>>(url);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() > 0);
            Assert.IsNotNull(results.First().VacationRequests);
            Assert.IsNotNull(results.First().EmployeePictures);
        }
    }
}
