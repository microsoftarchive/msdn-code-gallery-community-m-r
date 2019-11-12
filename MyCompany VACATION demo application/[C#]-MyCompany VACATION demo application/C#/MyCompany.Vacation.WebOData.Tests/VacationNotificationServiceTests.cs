
namespace MyCompany.Vacation.Web.Tests
{
    using Microsoft.QualityTools.Testing.Fakes.Stubs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Notifications;
    using System.Linq;

    [TestClass]
    public class VacationNotificationServiceTests
    {
        [TestMethod]
        public void VacationNotificationServiceTests_WithApprovedStatus_EmailNotifyStatusChange_Approved_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetInt32 = (id) =>
            {
                return new Employee()
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com"
                };
            };

            VacationNotificationService service = new VacationNotificationService(emailer, templatesRepository, employeesRepository);
            VacationRequest vacationRequest = new VacationRequest()
            {
                Status = VacationRequestStatus.Approved
            };
         
            service.NotifyStatusChange(vacationRequest, string.Empty);

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));           
        }

        [TestMethod]
        public void VacationNotificationServiceTests_WithDeniedStatus_EmailNotifyStatusChange_Denied_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetInt32 = (id) =>
            {
                return new Employee()
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com"
                };
            };

            VacationNotificationService service = new VacationNotificationService(emailer, templatesRepository, employeesRepository);
            VacationRequest vacationRequest = new VacationRequest()
            {
                Status = VacationRequestStatus.Denied
            };

            service.NotifyStatusChange(vacationRequest, string.Empty);

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));
        }

        [TestMethod]
        public void VacationNotificationServiceTests_NotifyNewVacationRequest_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetInt32 = (id) =>
            {
                return new Employee()
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com",
                    Team = new Team(){Manager = new Employee()
                                                    {
                                                        FirstName = "Manager",
                                                        LastName = "LastName",
                                                        Email = "manager@mail.com"
                                                    }}
                };
            };

            VacationNotificationService service = new VacationNotificationService(emailer, templatesRepository, employeesRepository);
            VacationRequest vacationRequest = new VacationRequest()
            {
                EmployeeId = 1
            };

            service.NotifyNewVacationRequest(vacationRequest);

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));
        }


        [TestMethod]
        public void VacationNotificationServiceTests_NotifyVacationRequestDeleted_Test()
        {
            var emailer = new Common.Notification.Fakes.StubIEmailer() { InstanceObserver = new StubObserver() };
            var templatesRepository = new Common.Notification.Fakes.StubIEmailTemplatesRepository();
            var employeesRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeesRepository.GetInt32 = (id) =>
            {
                return new Employee()
                {
                    EmployeeId = id,
                    FirstName = "My",
                    LastName = "Employee",
                    Email = "myemployee@mycompany.com",
                    Team = new Team()
                    {
                        Manager = new Employee()
                        {
                            FirstName = "Manager",
                            LastName = "LastName",
                            Email = "manager@mail.com"
                        }
                    }
                };
            };

            VacationNotificationService service = new VacationNotificationService(emailer, templatesRepository, employeesRepository);
            VacationRequest vacationRequest = new VacationRequest()
            {
                EmployeeId = 1
            };

            service.NotifyVacationRequestDeleted(vacationRequest);

            var observer = (StubObserver)emailer.InstanceObserver;
            Assert.AreEqual(1, observer.GetCalls().Count(c => c.StubbedMethod.Name == "Send"));
        }
    }
}
