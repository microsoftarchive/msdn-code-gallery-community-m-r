
namespace MyCompany.Travel.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class EmployeeRepositoryTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Constructor_ShouldThrowAnExceptionIfContextIsntSupplied()
        {
            var target = new EmployeeRepository(null);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetEmployee_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var target = new EmployeeRepository(new MyCompanyContext());
            var result = await target.GetAsync(employeeId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == employeeId);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetEmployeeByEmail_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var email = context.Employees.FirstOrDefault(e => e.EmployeePictures.Any()).Email;

            var target = new EmployeeRepository(new MyCompanyContext());
            var result = await target.GetByEmailAsync(email, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Count() == 1);
            Assert.IsTrue(result.Email == email);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetAllEmployees_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Employees.Count();

            var target = new EmployeeRepository(new MyCompanyContext());
            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task EmployeeRepository_AddEmployee_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count() + 1;

            var target = new EmployeeRepository(new MyCompanyContext());
            var employeeId = context.Employees.Select(e => e.EmployeeId).Max() + 1;
            var employee = new Employee()
            {
                EmployeeId = employeeId,
                FirstName = "Andrew",
                LastName = "Davis",
                Email = "Andrew.Davis@[tenantname].onmicrosoft.com",
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
            var target = new EmployeeRepository(new MyCompanyContext());

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

            IEmployeeRepository target = new EmployeeRepository(new MyCompanyContext());
            await target.DeleteAsync(employee.EmployeeId);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeeRepository_DeleteEmployee_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count();

            IEmployeeRepository target = new EmployeeRepository(new MyCompanyContext());
            await target.DeleteAsync(0);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
