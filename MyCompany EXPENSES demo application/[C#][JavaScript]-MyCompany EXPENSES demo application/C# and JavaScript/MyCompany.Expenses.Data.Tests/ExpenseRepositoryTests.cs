
namespace MyCompany.Expenses.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class ExpenseRepositoryTests
    {
        ExpenseRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new ExpenseRepository(null, false, new MyCompanyContext());
        }

        [TestMethod]
        public async Task ExpenseRepository_GetExpense_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expenseId = context.Expenses.FirstOrDefault().ExpenseId;

            var result = await target.GetAsync(expenseId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ExpenseId == expenseId);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetExpenseAllUserInfo_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expenseId = context.Expenses.FirstOrDefault(e => e.Employee.EmployeePictures.Any()).ExpenseId;

            var result = await target.GetAllUserInfoAsync(expenseId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Employee);
            Assert.IsNotNull(result.Employee.EmployeePictures);
            Assert.IsTrue(result.Employee.EmployeePictures.Count() == 1);
            Assert.IsTrue(result.ExpenseId == expenseId);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetAllExpenses_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Expenses.Count();

            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task ExpenseRepository_AddExpense_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Expenses.Count() + 1;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var expense = new Expense()
            {
                Name = "Bussiness",
                Description = "Lorem ipsum dolor sit amet.",
                CreationDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedDate = DateTime.UtcNow,
                Status = ExpenseStatus.Approved,
                Amount = 270,
                Contact = "Jeff Phillips",
                Picture = null,
                ExpenseType = Model.ExpenseType.Accommodation,
                RelatedProject = "MyCompany",
                EmployeeId = employeeId,
            };
            await target.AddAsync(expense);

            int actual = context.Expenses.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ExpenseRepository_UpdateExpense_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var expense = context.Expenses.FirstOrDefault();

            expense.Name = Guid.NewGuid().ToString();
            await target.UpdateAsync(expense);

            var actual = await target.GetAsync(expense.ExpenseId);

            Assert.AreEqual(expense.Name, actual.Name);
        }

        [TestMethod]
        public async Task ExpenseRepository_DeleteExpense_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var expense = context.Expenses.FirstOrDefault();
            int expected = context.Expenses.Count() - 1;

            await target.DeleteAsync(expense.ExpenseId);

            int actual = context.Expenses.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ExpenseRepository_DeleteExpense_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Expenses.Count();

            await target.DeleteAsync(0);

            int actual = context.Expenses.Count();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task ExpenseRepository_GetUserExpenses_Test()
        {
            var context = new MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);
            int number = 1;

            var results = await target.GetUserExpensesAsync(userIdentity, expenseStatus, number, 0);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetUserExpenses_NoResults_Test()
        {
            var context = new MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;
            int number = 1;

            var results = await target.GetUserExpensesAsync(userIdentity, expenseStatus, number, 0);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public async Task ExpenseRepository_GetUserCount_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Expenses.Count();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);

            var count = await target.GetUserCountAsync(userIdentity, expenseStatus);

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetUserCount_NoResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Expenses.Count();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;

            var count = await target.GetUserCountAsync(userIdentity, expenseStatus);

            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetTeamExpenses_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Expenses.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);
            PictureType pictureType = PictureType.Small;
            int number = 1;

            var results = await target.GetTeamExpensesAsync(managerIdentity, expenseStatus, pictureType, number, 0);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count() == 1);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetTeamExpenses_NoResults_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Expenses.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;
            PictureType pictureType = PictureType.Small;
            int number = 1;

            var results = await target.GetTeamExpensesAsync(managerIdentity, expenseStatus, pictureType, number, 0);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }


        [TestMethod]
        public async Task ExpenseRepository_GetTeamCount_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any() && e.Expenses.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);

            var count = await target.GetTeamCountAsync(managerIdentity, expenseStatus);

            Assert.IsTrue(count > 0);
       }

        [TestMethod]
        public async Task ExpenseRepository_GetTeamCount_NoResults_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;

            var count = await target.GetTeamCountAsync(managerIdentity, expenseStatus);

            Assert.IsTrue(count == 0);
        }


        [TestMethod]
        public async Task ExpenseRepository_GetTeamExpensesByMember_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            PictureType pictureType = PictureType.Small;

            var results = await target.GetTeamExpensesByMemberAsync(managerIdentity, pictureType);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Picture);
        }

        [TestMethod]
        public async Task ExpenseRepository_GetTeamExpensesByMemberCount_Test()
        {
            var context = new MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;

            var count = await target.GetTeamExpensesByMemberCountAsync(managerIdentity);

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void ExpenseRepository_GetTeamExpensesByMonth_Test()
        {
            var context = new MyCompanyContext();
            int expenseType = (int)(ExpenseType.Accommodation | ExpenseType.Food | ExpenseType.Other | ExpenseType.Travel);
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;

            var results = target.GetTeamExpensesByMonth(managerIdentity, expenseType);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }


        [TestMethod]
        public void ExpenseRepository_GetTeamExpensesByMonth_EmptyResults_Test()
        {
            var context = new MyCompanyContext();
            int expenseType = (int)(ExpenseType.Unknown);
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;

            var results = target.GetTeamExpensesByMonth(managerIdentity, expenseType);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }


        [TestMethod]
        public void ExpenseRepository_GetTeamMemberExpensesByMonth_Test()
        {
            var context = new MyCompanyContext();
            int expenseType = (int)(ExpenseType.Accommodation | ExpenseType.Food | ExpenseType.Other | ExpenseType.Travel);
            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;

            var results = target.GetTeamMemberExpensesByMonth(employeeId, expenseType);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void ExpenseRepository_GetTeamMemberExpensesByMonth_EmptyResults_Test()
        {
            var context = new MyCompanyContext();
            int expenseType = (int)(ExpenseType.Unknown);
            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;

            var results = target.GetTeamMemberExpensesByMonth(employeeId, expenseType);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public async Task ExpenseRepository_GetTeamMemberSummaryExpenses_Test()
        {
            var context = new MyCompanyContext();

            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;
            var datetime = context.Expenses.Where(e => e.EmployeeId == employeeId).First().CreationDate;

            var results = await target.GetTeamMemberSummaryExpensesAsync(employeeId, datetime.Month, datetime.Year);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task ExpenseRepository_GetTeamMemberSummaryExpenses_MonthNull_Test()
        {
            var context = new MyCompanyContext();

            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;
            var datetime = context.Expenses.Where(e => e.EmployeeId == employeeId).First().CreationDate;

            var results = await target.GetTeamMemberSummaryExpensesAsync(employeeId, null, datetime.Year);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }


        [TestMethod]
        public async Task ExpenseRepository_GetTeamMemberSummaryExpenses_EmptyResults_Test()
        {
            var context = new MyCompanyContext();
            int month = 0;
            int year = 0;

            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;

            var results = await target.GetTeamMemberSummaryExpensesAsync(employeeId, month, year);

            Assert.IsNotNull(results);
        }
    }
}
