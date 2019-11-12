
namespace MyCompany.Vacation.Web.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Controllers;
    using System.Threading.Tasks;
    using System.Linq;

    [TestClass]
    public class VacationRequestsControllerTests
    {
        [TestMethod]
        public async void VacationRequestsController_GetTeamVacationRequestsByEmployee_Test()
        {
            bool called = false;
            var vacationRepository = new Data.Repositories.Fakes.StubIVacationRequestRepository();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var vacationNotificationService = new Notifications.Fakes.StubIVacationNotificationService();

            vacationRepository.GetTeamVacationRequestsByEmployeeStringNullableOfInt32Int32Int32PictureType = (identity, month, year, status, picture) =>
            {
                called = true;
                return Task.FromResult(Enumerable.Empty<Employee>());
            };

            var target = new VacationRequestsController(vacationRepository, employeeRepository, new SecurityHelper(), vacationNotificationService);
            var result = await target.GetTeamVacationRequestsByEmployee(0, 0, 0, PictureType.Small);

            Assert.IsTrue(called);
        }
    }
}
