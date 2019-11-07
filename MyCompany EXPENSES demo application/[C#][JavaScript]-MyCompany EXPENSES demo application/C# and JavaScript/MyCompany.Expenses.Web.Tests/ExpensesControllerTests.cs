namespace MyCompany.Expenses.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Web.Controllers;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Web.Services;
    using System.Threading.Tasks;

    [TestClass]
    public class ExpensesControllerTests
    {
        [TestMethod]
        public async Task ExpensesController_Get_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetAllUserInfoAsyncInt32PictureType = (id, picture) =>
            {
                called = true;
                return Task.FromResult(new Expense());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.Get(0, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_GetUserExpenses_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetUserExpensesAsyncStringInt32Int32Int32 = (id, expenseStatus, pageSize, pageCount) =>
            {
                called = true;
                return Task.FromResult(new List<Expense>().AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.GetUserExpenses(0, 5, 0);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_GetTeamExpenses_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamExpensesAsyncStringInt32PictureTypeInt32Int32 = (id, status, picture, pageSize, pageCount) =>
            {
                called = true;
                return Task.FromResult(new List<Expense>().AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.GetTeamExpenses(0, PictureType.Small, 5, 0);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_GetTeamCount_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamCountAsyncStringInt32 = (id, status) =>
            {
                called = true;
                return Task.FromResult(10);
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.GetTeamCount(0);

            Assert.IsTrue(result == 10);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_Add_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            var newExpense = new Expense()
            {
                ExpenseId = 1,
            };

            expenseRepository.AddAsyncExpense = (expense) =>
            {
                Assert.IsTrue(expense.ExpenseId == newExpense.ExpenseId);
                called = true;
                return Task.FromResult(10);
            };

            expenseRepository.GetAsyncInt32 = (expenseId) =>
            {
                return Task.FromResult(new Expense
                    {
                        Employee = new Employee
                        {
                            FirstName = "John",
                            LastName = "Smith"
                        }
                    });
            };

            notificationChannelRepository.GetManagersChannelsAsync = () =>
            {
                return Task.FromResult(new List<NotificationChannel>().AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            await target.Add(newExpense);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_Add_ShouldNotifyManagers()
        {
            bool notificationServiceCalled = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            var newExpense = new Expense()
            {
                ExpenseId = 1,
            };

            notificationService.NewExpenseAddedNotificationChannelExpense = (notificationChannel, expense) =>
            {
                notificationServiceCalled = true;
            };

            expenseRepository.AddAsyncExpense = (expense) =>
            {
                return Task.FromResult(10);
            };

            expenseRepository.GetAsyncInt32 = (expenseId) =>
            {
                return Task.FromResult(new Expense
                    {
                        Employee = new Employee
                        {
                            FirstName = "John",
                            LastName = "Smith"
                        }
                    });
            };

            notificationChannelRepository.GetManagersChannelsAsync = () =>
                {
                    return Task.FromResult(new List<NotificationChannel>() { new NotificationChannel() }.AsEnumerable());
                };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            await target.Add(newExpense);

            Assert.IsTrue(notificationServiceCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExpensesController_Add_Exception_Test()
        {
            var expenseTravelRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            var target = new ExpensesController(expenseTravelRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            await target.Add(null);
        }

        [TestMethod]
        public async Task ExpensesController_UpdateStatus_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetAsyncInt32 = (id) =>
                {
                    return Task.FromResult(new Expense());
                };

            expenseRepository.UpdateAsyncExpense = (expense) =>
            {
                Assert.AreEqual(expense.Status, ExpenseStatus.Pending);
                called = true;

                return Task.FromResult(string.Empty);
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            await target.UpdateStatus(1, ExpenseStatus.Pending);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_UpdateStatus_ShouldNotifyEmployeesInWindowsPhone()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult(new Expense
                {
                    Employee = new Employee
                    {
                        FirstName = "John",
                        LastName = "Smith"
                    }
                });
            };

            expenseRepository.UpdateAsyncExpense = (expense) =>
            {
                Assert.AreEqual(expense.Status, ExpenseStatus.Approved);
                return Task.FromResult(string.Empty);
            };

            notificationService.ExpenseStatusChangedNotificationChannelExpense = (channel, expense) =>
                {
                    called = true;
                };

            notificationChannelRepository.GetUserChannelsAsyncString = (email) =>
            {
                return Task.FromResult( new List<NotificationChannel>() 
                    { 
                        new NotificationChannel()
                        {
                            NotificationType = NotificationType.WindowsPhoneNotification
                        } 
                    }.AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            await target.UpdateStatus(1, ExpenseStatus.Approved);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_UpdateStatus_ShouldNotNotifyEmployeesInWindowsStore()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult(new Expense
                {
                    Employee = new Employee
                    {
                        FirstName = "John",
                        LastName = "Smith"
                    }
                });
            };

            expenseRepository.UpdateAsyncExpense = (expense) =>
            {
                Assert.AreEqual(expense.Status, ExpenseStatus.Approved);
                return Task.FromResult(string.Empty);
            };

            notificationService.ExpenseStatusChangedNotificationChannelExpense = (channel, expense) =>
            {
                called = true;
            };

            notificationChannelRepository.GetUserChannelsAsyncString = (email) =>
            {
                return Task.FromResult(new List<NotificationChannel>() 
                        { 
                            new NotificationChannel()
                            {
                                NotificationType = NotificationType.WindowsStoreNotification
                            }
                        }.AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            await target.UpdateStatus(1, ExpenseStatus.Approved);

            Assert.IsFalse(called);
        }

        [TestMethod]
        public async Task ExpensesController_GetTeamExpensesByMember_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamExpensesByMemberAsyncStringPictureType = (id, picture) =>
            {
                called = true;
                return Task.FromResult(new List<ExpenseGrouped>().AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.GetTeamExpensesByMember(PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_GetTeamExpensesByMemberCount_Test()
        {
            bool called = false;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamExpensesByMemberCountAsyncString = (id) =>
            {
                called = true;
                return Task.FromResult(10);
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.GetTeamExpensesByMemberCount();

            Assert.IsTrue(result == 10);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void ExpensesController_GetTeamExpensesByMonth_Test()
        {
            bool called = false;
            int expenseType = 1;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamExpensesByMonthStringInt32 = (id, type) =>
            {
                Assert.AreEqual(expenseType, type);
                called = true;
                return new List<ExpenseMonth>();
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = target.GetTeamExpensesByMonth(expenseType);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void ExpensesController_GetTeamMemberExpensesByMonth_Test()
        {
            bool called = false;
            int expenseType = 1;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamMemberExpensesByMonthInt32Int32 = (id, type) =>
            {
                Assert.AreEqual(expenseType, type);
                called = true;
                return new List<ExpenseMonth>();
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = target.GetTeamMemberExpensesByMonth(1, expenseType);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task ExpensesController_GetTeamMemberSummaryExpenses_Test()
        {
            bool called = false;
            int month = 1;
            int year = 2;
            var expenseRepository = new Data.Repositories.Fakes.StubIExpenseRepository();
            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            var notificationService = new Services.Fakes.StubINotificationService();

            expenseRepository.GetTeamMemberSummaryExpensesAsyncInt32NullableOfInt32Int32 = (id, m, y) =>
            {
                Assert.AreEqual(month, m);
                Assert.AreEqual(year, y);
                called = true;

                return Task.FromResult(new List<TeamMemberSummary>().AsEnumerable());
            };

            var target = new ExpensesController(expenseRepository, new SecurityHelper(), notificationChannelRepository, notificationService);
            var result = await target.GetTeamMemberSummaryExpenses(1, year, month);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }
    }
}