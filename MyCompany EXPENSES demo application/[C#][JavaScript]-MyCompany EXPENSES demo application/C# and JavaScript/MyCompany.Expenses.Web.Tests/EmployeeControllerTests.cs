namespace MyCompany.Expenses.Web.Tests
{
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Web.Controllers;

    [TestClass]
    public class EmployeeControllerTests
    {
        [TestMethod]
        public async Task EmployeeController_GetLoggedEmployeeInfo_Test()
        {
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeeRepository.GetByEmailAsyncStringPictureType = (email, pictureType) =>
                {
                    return Task.FromResult(new Employee() { EmployeeId = 1 });
                };

            var target = new EmployeesController(employeeRepository, new SecurityHelper());
            var result = await target.GetLoggedEmployeeInfo(Model.PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == 1);
        }

        [TestMethod]
        public async Task EmployeeController_GetEmployeeInfo_Test()
        {
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeeRepository.GetEmployeeAsyncInt32PictureType = (id, pictureType) =>
            {
                return Task.FromResult(new Employee() { EmployeeId = id });
            };

            var target = new EmployeesController(employeeRepository, new SecurityHelper());
            var result = await target.Get(1, Model.PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == 1);
        }
    }
}