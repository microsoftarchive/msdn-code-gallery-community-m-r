
namespace MyCompany.Expenses.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class EmployeeRepositoryTests
    {
        IEmployeeRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new EmployeeRepository(new MyCompanyContext());
        }

        [TestMethod]
        public async Task EmployeeRepository_Get_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var result = await target.GetAsync(employeeId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == employeeId);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetEmployeeByEmail_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();

            var email = context.Employees.FirstOrDefault(e => e.EmployeePictures.Any()).Email;

            var result = await target.GetByEmailAsync(email, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Count() == 1);
            Assert.IsTrue(result.Email == email);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetEmployee_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var employeeId = context.Employees.FirstOrDefault(e => e.EmployeePictures.Any()).EmployeeId;

            var result = await target.GetEmployeeAsync(employeeId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Count() == 1);
            Assert.IsTrue(result.EmployeeId == employeeId);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetAllEmployees_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Employees.Count();

            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task EmployeeRepository_AddEmployee_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count() + 1;

            var employeeId = context.Employees.Select(e => e.EmployeeId).Max() + 1;
            var employee = new Employee()
            {
                EmployeeId = employeeId,
                FirstName = "Andrew",
                LastName = "Davis",
                Email = "Andrew.Davis@[tenantname].onmicrosoft.com",
                JobTitle = "Developer"
            };

            await target.AddAsync(employee);

            int actual = context.Employees.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeeRepository_UpdateEmployee_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.FirstOrDefault();

            employee.FirstName = Guid.NewGuid().ToString();
            await target.UpdateAsync(employee);

            var actual = await target.GetAsync(employee.EmployeeId);

            Assert.AreEqual(employee.FirstName, actual.FirstName);
        }

        [TestMethod]
        public async Task EmployeeRepository_DeleteEmployee_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.FirstOrDefault(e => !e.ManagedTeams.Any());
            int expected = context.Employees.Count() - 1;

            await target.DeleteAsync(employee.EmployeeId);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeeRepository_DeleteEmployee_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count();

            await target.DeleteAsync(0);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
