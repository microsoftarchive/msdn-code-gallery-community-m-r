
namespace MyCompany.Vacation.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    [TestClass]
    public class VacationRequestRepositoryTests
    {
        [TestMethod]
        public void VacationRequestRepository_GetVacationRequest_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            int vacationRequestId = context.VacationRequests.FirstOrDefault().VacationRequestId;

            var target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var result = target.Get(vacationRequestId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.VacationRequestId == vacationRequestId);
        }

        [TestMethod]
        public void VacationRequestRepository_GetAllVacationRequests_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            int expectedCount = context.VacationRequests.Count();

            var target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var results = target.GetAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void VacationRequestRepository_AddVacationRequest_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.VacationRequests.Count() + 1;
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();

            workingDaysCalculator.GetWorkingDaysInt32DateTimeDateTime = (i, time, arg3) => 5;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var target = new VacationRequestRepository(context, null, workingDaysCalculator, false);

            var vacationRequest = new VacationRequest()
            {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow,
                NumDays = 0,
                Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreationDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedDate = DateTime.UtcNow,
                Status = VacationRequestStatus.Approved,
                EmployeeId = employeeId,
            };

            target.Add(vacationRequest);

            int actual = context.VacationRequests.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VacationRequestRepository_AddVacationRequest_ExceedingMaxVacationNumber_ShouldFail_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();

            workingDaysCalculator.GetWorkingDaysInt32DateTimeDateTime = (i, time, arg3) => 100;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var target = new VacationRequestRepository(context, null, workingDaysCalculator, false);

            var vacationRequest = new VacationRequest()
            {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow,
                NumDays = 0,
                Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreationDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedDate = DateTime.UtcNow,
                Status = VacationRequestStatus.Approved,
                EmployeeId = employeeId,
            };

            target.Add(vacationRequest);
        }

        [TestMethod]
        public void VacationRequestRepository_UpdateVacationRequest_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var vacationRequest = context.VacationRequests.FirstOrDefault(v=> v.Status != VacationRequestStatus.Denied);
            var target = new VacationRequestRepository(context, null, workingDaysCalculator, false);

            vacationRequest.Status = VacationRequestStatus.Denied;
            target.Update(vacationRequest);

            var actual = target.Get(vacationRequest.VacationRequestId);

            Assert.AreEqual(VacationRequestStatus.Denied, actual.Status);
        }

        [TestMethod]
        public void VacationRequestRepository_DeleteVacationRequest_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var target = new VacationRequestRepository(context, null, workingDaysCalculator, false);

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
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

            int id = target.Add(vacationRequest);

            int expected = context.VacationRequests.Count() - 1;


            target.Delete(id);

            int actual = context.VacationRequests.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VacationRequestRepository_DeleteVacationRequest_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            int expected = context.VacationRequests.Count();

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            target.Delete(-1);

            int actual = context.VacationRequests.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async void VacationRequestRepository_GetUserVacationRequests_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var employee = context.Employees
                                .Include("VacationRequests")
                                .Where(e => e.VacationRequests.Any() && e.EmployeePictures.Any())
                                .FirstOrDefault();
            string identity = employee.Email;
            int year = employee.VacationRequests.First().From.Year;

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var results = await target.GetUserVacationRequests(identity, year);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        public void VacationRequestRepository_GetUserVacationRequestsById_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var employee = context.Employees
                                .Include("VacationRequests")
                                .Where(e => e.VacationRequests.Any() && e.EmployeePictures.Any())
                                .FirstOrDefault();
            int employeeId = employee.EmployeeId;
            int year = employee.VacationRequests.First().From.Year;

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var results = target.GetUserVacationRequests(employeeId, year);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        public void VacationRequestRepository_GetUserCount_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var employee = context.Employees.Include("VacationRequests").FirstOrDefault(e => e.VacationRequests.Any());
            string identity = employee.Email;
            int? month = employee.VacationRequests.First().From.Month;
            int year = employee.VacationRequests.First().From.Year;
            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);
            int expected = employee.VacationRequests.Count(v => v.From.Month == month && v.From.Year == year);

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var actual = target.GetUserCount(identity, month, year, status);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VacationRequestRepository_GetUserPendingVacation_Test()
        {
            var context = new Data.MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var employee = context.Employees.Include("VacationRequests").FirstOrDefault(e => e.VacationRequests.Any());
            string identity = employee.Email;
            int year = employee.VacationRequests.First().From.Year;

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);

            var actual = target.GetUserPendingVacation(employee.Email, year);

            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void VacationRequestRepository_GetTeamVacationRequests_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var manager = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any());
            var vacationRequest = context.VacationRequests.FirstOrDefault();

            string identity = manager.Email;
            int? month = vacationRequest.From.Month;
            int year = vacationRequest.From.Year;
            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);
            int pageSize = 1;
            int pageCount = 0;

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var results = target.GetTeamVacationRequests(identity, string.Empty, month, year, status, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        public void VacationRequestRepository_GetTeamCount_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var team = context.Teams.Include("Manager").Where(t => t.Employees.Any()).FirstOrDefault();
            var vacationRequest = context.VacationRequests.FirstOrDefault();

            string identity = team.Manager.Email;
            int? month = vacationRequest.From.Month;
            int year = vacationRequest.From.Year;
            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var actual = target.GetTeamCount(identity, string.Empty, month, year, status);

            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public async void VacationRequestRepository_GetTeamVacationRequestsByEmployee_Test()
        {
            var context = new MyCompanyContext();
            var workingDaysCalculator = new Data.Services.Fakes.StubIWorkingDaysCalculator();
            var team = context.Teams.Include("Manager").Where(t => t.Employees.Any()).FirstOrDefault();
            var vacationRequest = context.VacationRequests.FirstOrDefault();

            string identity = team.Manager.Email;
            int? month = vacationRequest.From.Month;
            int year = vacationRequest.From.Year;
            int status = (int)(VacationRequestStatus.Approved | VacationRequestStatus.Denied | VacationRequestStatus.Pending);

            IVacationRequestRepository target = new VacationRequestRepository(context, null, workingDaysCalculator, false);
            var results = await target.GetTeamVacationRequestsByEmployee(identity, month, year, status, PictureType.Small);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            //Assert.IsTrue(results.Count() == 6); //TODO: comentar con vgaltes
            Assert.IsNotNull(results.First().VacationRequests);
            Assert.IsTrue(results.First().VacationRequests.Any());
            Assert.IsNotNull(results.First().EmployeePictures);
        }
    }
}
