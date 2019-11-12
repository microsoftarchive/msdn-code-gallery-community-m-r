
namespace MyCompany.Vacation.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    [TestClass]
    public class EmployeeRepositoryTests
    {
        [TestMethod]
        public void EmployeeRepository_GetEmployee_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var target = new EmployeeRepository(context);
            var result = target.Get(employeeId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId == employeeId);
        }

        [TestMethod]
        public void EmployeeRepository_GetEmployeeByEmail_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var email = context.Employees.FirstOrDefault(e => e.EmployeePictures.Any()).Email;

            var target = new EmployeeRepository(context);
            var result = target.GetByEmail(email, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Any());
            Assert.IsTrue(result.Email == email);
        }

        [TestMethod]
        public void EmployeeRepository_GetAllEmployees_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Employees.Count();

            var target = new EmployeeRepository(context);
            var results = target.GetAll().ToList();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void EmployeeRepository_AddEmployee_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count() + 1;

            var target = new EmployeeRepository(context);

            var employeeId = context.Employees.Select(e => e.EmployeeId).Max() + 1;
            var officeId = context.Offices.FirstOrDefault().OfficeId;
            var employee = new Employee()
            {
                OfficeId = officeId,
                EmployeeId = employeeId,
                FirstName = "Andrew",
                LastName = "Davis",
                Email = "Andrew.Davis@[tenantname].onmicrosoft.com",
            };

            target.Add(employee);

            int actual = context.Employees.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmployeeRepository_UpdateEmployee_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.FirstOrDefault();
            var target = new EmployeeRepository(context);

            employee.FirstName = Guid.NewGuid().ToString();
            target.Update(employee);

            var actual = target.Get(employee.EmployeeId);

            Assert.AreEqual(employee.FirstName, actual.FirstName);
        }

        [TestMethod]
        public void EmployeeRepository_DeleteEmployee_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employee = context.Employees.FirstOrDefault(e => !e.ManagedTeams.Any());
            int expected = context.Employees.Count() - 1;

            IEmployeeRepository target = new EmployeeRepository(context);
            target.Delete(employee.EmployeeId);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmployeeRepository_DeleteEmployee_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Employees.Count();

            IEmployeeRepository target = new EmployeeRepository(context);
            target.Delete(0);

            int actual = context.Employees.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
