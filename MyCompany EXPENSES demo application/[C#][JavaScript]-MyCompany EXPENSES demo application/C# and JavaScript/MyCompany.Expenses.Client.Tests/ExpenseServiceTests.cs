
namespace MyCompany.Expenses.Client.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpenseServiceTests
    {

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetExpenseAllUserInfo_Call_GetResults_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            int expenseId = context.Expenses.FirstOrDefault().ExpenseId;

            var result = await client.ExpenseService.Get(expenseId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Employee);
            Assert.IsNotNull(result.Employee.EmployeePictures);
            Assert.IsTrue(result.Employee.EmployeePictures.Count() == 1);
            Assert.IsTrue(result.ExpenseId == expenseId);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_AddExpense_Added_NotFail_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
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
                ExpenseType = ExpenseType.Accommodation,
                RelatedProject = "MyCompany",
                EmployeeId = employeeId                
            };
            await client.ExpenseService.Add(expense);

            int actual = context.Expenses.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_UpdateExpense_NotFail_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var expenseId = context.Expenses.FirstOrDefault().ExpenseId;

            await client.ExpenseService.UpdateStatus(expenseId, ExpenseStatus.Denied);

            var actual = await client.ExpenseService.Get(expenseId, PictureType.Small);

            Assert.AreEqual(actual.Status, ExpenseStatus.Denied);
        }


        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetUserExpenses_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);
            int number = 1;

            var results = await client.ExpenseService.GetUserExpenses(expenseStatus, number, 0);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNull(results.First().Employee.EmployeePictures);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetUserExpenses_NoResults_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;
            int number = 1;

            var results = await client.ExpenseService.GetUserExpenses(expenseStatus, number, 0);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetUserCount_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            int expectedCount = context.Expenses.Count();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);

            var count = await client.ExpenseService.GetUserCount(expenseStatus);

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetUserCount_NoResults_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            int expectedCount = context.Expenses.Count();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;

            var count = await client.ExpenseService.GetUserCount(expenseStatus);

            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseServicee_GetTeamExpenses_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);
            PictureType pictureType = PictureType.Small;
            int number = 1;

            var results = await client.ExpenseService.GetTeamExpenses(expenseStatus, pictureType, number, 0);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
            Assert.IsNotNull(results.First().Employee);
            Assert.IsNotNull(results.First().Employee.EmployeePictures);
            Assert.IsTrue(results.First().Employee.EmployeePictures.Count() == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamExpenses_NoResults_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;
            PictureType pictureType = PictureType.Small;
            int number = 1;

            var results = await client.ExpenseService.GetTeamExpenses(expenseStatus, pictureType, number, 0);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }


        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamCount_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            int expenseStatus = (int)(ExpenseStatus.Approved | ExpenseStatus.Denied | ExpenseStatus.Pending);
            
            var count = await client.ExpenseService.GetTeamCount(expenseStatus);

            Assert.IsTrue(count > 0);
       }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamCount_NoResults_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            int expenseStatus = (int)ExpenseStatus.Unknown;

            var count = await client.ExpenseService.GetTeamCount(expenseStatus);

            Assert.IsTrue(count == 0);
        }


        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamExpensesByMember_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            PictureType pictureType = PictureType.Small;

            var results = await client.ExpenseService.GetTeamExpensesByMember(pictureType);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First().Picture);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamExpensesByMemberCount_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;

            var count = await client.ExpenseService.GetTeamExpensesByMemberCount();

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamExpensesByMonth_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var managerIdentity = context.Employees.FirstOrDefault(e => e.ManagedTeams.Any()).Email;
            int expenseType = (int)(ExpenseType.Accommodation | ExpenseType.Food | ExpenseType.Other | ExpenseType.Travel);

            var results = await client.ExpenseService.GetTeamExpensesByMonth(expenseType);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamMemberExpensesByMonth_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;
            int expenseType = (int)(ExpenseType.Accommodation | ExpenseType.Food | ExpenseType.Other | ExpenseType.Travel);

            var results = await client.ExpenseService.GetTeamMemberExpensesByMonth(employeeId, expenseType);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamMemberSummaryExpenses_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();

            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;
            var datetime = context.Expenses.Where(e => e.EmployeeId == employeeId).First().CreationDate;

            var results = await client.ExpenseService.GetTeamMemberSummaryExpenses(employeeId, datetime.Month, datetime.Year);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseService_GetTeamMemberSummaryExpenses_EmptyMonth_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();

            var employeeId = context.Employees.FirstOrDefault(e => e.Expenses.Any()).EmployeeId;
            var datetime = context.Expenses.Where(e => e.EmployeeId == employeeId).First().CreationDate;

            var results = await client.ExpenseService.GetTeamMemberSummaryExpenses(employeeId, null, datetime.Year);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }
    }
}
