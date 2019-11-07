
namespace MyCompany.Visitors.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class EmployeeRepositoryTests
    {
        [TestMethod]
        public async Task EmployeeRepository_Get_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var target = new EmployeeRepository(context);
            var result = await target.GetAsync(employeeId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == employeeId);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetEmployeeByEmail_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var email = context.Employees.FirstOrDefault().Email;

            var target = new EmployeeRepository(context);
            var result = await target.GetByEmailAsync(email, PictureType.Small);

            Assert.IsNotNull(result);
            if (result.EmployeePictures != null
                &&
                result.EmployeePictures.Any())
            {
                Assert.IsTrue(result.EmployeePictures.All(ep => ep.PictureType == PictureType.Small));
            }
        }

        [TestMethod]
        public async Task EmployeeRepository_GetAllEmployees_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Employees.Count();

            var target = new EmployeeRepository(context);
            var results = (await target.GetAllAsync()).ToList();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task EmployeeRepository_AddEmployee_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count() + 1;

            var target = new EmployeeRepository(context);
            var employeeId = context.Employees.Select(e => e.EmployeeId).Max() + 1;
            var employee = new Employee()
            {
                EmployeeId = employeeId,
                FirstName = "Andrew",
                LastName = "Davis",
                Email = "Andrew.Davis@mycompanydemos.com",
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
            var target = new EmployeeRepository(context);

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

            IEmployeeRepository target = new EmployeeRepository(context);
            await target.DeleteAsync(employee.EmployeeId);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeeRepository_DeleteEmployee_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count();

            IEmployeeRepository target = new EmployeeRepository(context);
            await target.DeleteAsync(0);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeeRepository_GetCompleteInfo_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var target = new EmployeeRepository(context);
            var result = await target.GetCompleteInfoAsync(employeeId, PictureType.All);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == employeeId);
        }

       

        [TestMethod]
        public async Task EmployeeRepository_GetEmployees_GetResults_Test()
        {
            var target = new EmployeeRepository(new MyCompanyContext());
            var result = await target.GetEmployeesAsync(string.Empty, PictureType.All, 1, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}
