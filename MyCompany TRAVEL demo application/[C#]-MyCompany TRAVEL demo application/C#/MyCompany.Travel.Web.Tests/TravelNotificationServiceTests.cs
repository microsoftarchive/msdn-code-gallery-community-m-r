namespace MyCompany.Travel.Web.Tests
{
    using Microsoft.QualityTools.Testing.Fakes.Stubs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Common.Notification;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Notifications;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class TravelNotificationServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelNotificationService_Constructor_NullEmployeeRepository_Test()
        {
            IEmailer emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            IEmailTemplatesRepository templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            IEmployeeRepository employeesRepository = null;

            TravelNotificationService service = new TravelNotificationService(emailer, templatesRepository, employeesRepository);
        }

        [TestMethod]
        public async Task TravelNotificationServiceTests_EmailNotifyStatusChange_Approved_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult(new Employee{
                        EmployeeId = id,
                        FirstName = "My",
                        LastName = "Employee",
                        Email = "myemployee@mycompany.com"
                    });
            };

            TravelNotificationService service = new TravelNotificationService(emailer, templatesRepository, employeesRepository);
            TravelRequest travelRequest = new TravelRequest()
            {
                Status = TravelRequestStatus.Approved
            };
         
            await service.EmailNotifyStatusChange(travelRequest, "my reason");

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));           
        }

        [TestMethod]
        public async Task TravelNotificationServiceTests_EmailNotifyStatusChange_Denied_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult(new Employee
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com"
                });
            };

            TravelNotificationService service = new TravelNotificationService(emailer, templatesRepository, employeesRepository);
            TravelRequest travelRequest = new TravelRequest()
            {
                Status = TravelRequestStatus.Denied
            };

            await service.EmailNotifyStatusChange(travelRequest, "my reason");

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));
        }

        [TestMethod]
        public async Task TravelNotificationServiceTests_EmailNotifyStatusChange_Completed_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult( new Employee()
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com"
                });
            };

            TravelNotificationService service = new TravelNotificationService(emailer, templatesRepository, employeesRepository);
            TravelRequest travelRequest = new TravelRequest()
            {
                Status = TravelRequestStatus.Completed
            };

            await service.EmailNotifyStatusChange(travelRequest, "my reason");

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));
        }

        [TestMethod]
        public async Task TravelNotificationServiceTests_EmailNotifyNewRequest_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult( new Employee()
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com",
                    Team = new Team()
                    {
                        TeamId = 1,
                        Manager = new Employee()
                        {
                            EmployeeId = 2,
                            FirstName = "My",
                            LastName = "Manager",
                            Email = "mymanager@mycompany.com",
                        }
                    }
                });
            };

            TravelNotificationService service = new TravelNotificationService(emailer, templatesRepository, employeesRepository);
            TravelRequest travelRequest = new TravelRequest()
            {
                Status = TravelRequestStatus.Completed
            };

            await service.EmailNotifyNewRequest(travelRequest);

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));
        }
    }
}