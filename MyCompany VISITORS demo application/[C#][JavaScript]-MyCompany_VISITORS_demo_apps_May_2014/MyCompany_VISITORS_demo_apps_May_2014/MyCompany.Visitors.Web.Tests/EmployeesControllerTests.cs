namespace MyCompany.Visitors.Web.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Model;
    using MyCompany.Visitors.Web.Controllers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class EmployeeControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Employee repository is null.")]
        public void EmployeesController_Constructor_Failed_FirstArgument_test()
        {
            var target = new EmployeesController(null, new SecurityHelper());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Security helper is null.")]
        public void EmployeesController_Constructor_Failed_SecondArgument_test()
        {
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var target = new EmployeesController(employeeRepository, null);
        }

        [TestMethod]
        public async Task EmployeesController_GetLoggedEmployeeInfo_Test()
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
        public async Task EmployeesController_Get_Test()
        {
            bool called = false;
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            employeeRepository.GetCompleteInfoAsyncInt32PictureType = (id, picture) =>
            {
                called = true;
                return Task.FromResult(new Employee());
            };

            var target = new EmployeesController(employeeRepository, new SecurityHelper());
            var result = await target.Get(0, PictureType.Small);

            Assert.AreEqual(0,result.EmployeeId);
            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task EmployeesController_GetEmployees_Test()
        {
            bool called = false;
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            employeeRepository.GetEmployeesAsyncStringPictureTypeInt32Int32 = (id, picture, pageSize, pageCount) =>
            {
                called = true;
                return Task.FromResult((new List<Employee>()).AsEnumerable());
            };

            var target = new EmployeesController(employeeRepository, new SecurityHelper());
            var result = await target.GetEmployees(string.Empty, PictureType.Small, 1, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }
    }
}
