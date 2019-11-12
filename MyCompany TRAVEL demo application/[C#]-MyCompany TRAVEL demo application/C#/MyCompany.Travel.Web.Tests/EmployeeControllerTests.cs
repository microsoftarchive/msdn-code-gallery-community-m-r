namespace MyCompany.Travel.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Controllers;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class EmployeeControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void EmployeeController_Constructor_No_Repository_Helper_Fails_Test()
        {
            var ADGraphAPI = new Web.Security.Fakes.StubIADGraphApi();
            var target = new EmployeesController(null, new SecurityHelper(), ADGraphAPI);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmployeeController_Constructor_No_Security_Helper_Fails_Test()
        {
            var ADGraphAPI = new Web.Security.Fakes.StubIADGraphApi();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var target = new EmployeesController(employeeRepository, null, ADGraphAPI);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmployeeController_Constructor_No_ADGraphAPI_Fails_Test()
        {
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var target = new EmployeesController(employeeRepository, new SecurityHelper(), null);
        }

        [TestMethod]
        public async Task EmployeeController_GetLoggedEmployeeInfo_Test()
        {
            var ADGraphAPI = new Web.Security.Fakes.StubIADGraphApi();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            employeeRepository.GetByEmailAsyncStringPictureType = (email, pictureType) =>
            {
                return Task.FromResult(new Employee() { EmployeeId = 10 });
            };

            ADGraphAPI.IsInGroupStringString = (userName, groupName) =>
            {
                return true;
            };

            var target = new EmployeesController(employeeRepository, new SecurityHelper(), ADGraphAPI);
            var result = await target.GetLoggedEmployeeInfo(Model.PictureType.Small);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.EmployeeId);
            Assert.IsTrue(result.IsRRHH);
        }

        [TestMethod]
        public async Task EmployeeController_GetLoggedEmployeeInfo_User_Not_Found_Test()
        {
            var ADGraphAPI = new Web.Security.Fakes.StubIADGraphApi();
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();

            Employee employee = null;

            employeeRepository.GetByEmailAsyncStringPictureType = (email, pictureType) =>
            {
                return Task.FromResult(employee);
            };

            var target = new EmployeesController(employeeRepository, new SecurityHelper(), ADGraphAPI);
            var result = await target.GetLoggedEmployeeInfo(Model.PictureType.Small);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task EmployeeController_GetEmployees_Test()
        {
            var employeeRepository = new Data.Repositories.Fakes.StubIEmployeeRepository();
            var ADGraphAPI = new Web.Security.Fakes.StubIADGraphApi();

            employeeRepository.GetAllAsync = () =>
            {
                return Task.FromResult(new List<Employee>() { new Employee() { EmployeeId = 1 }, new Employee() { EmployeeId = 2 } }.AsEnumerable());
            };

            var target = new EmployeesController(employeeRepository, new SecurityHelper(), ADGraphAPI);
            var result = await target.GetEmployees();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}