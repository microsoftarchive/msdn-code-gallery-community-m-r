namespace MyCompany.Expenses.Web.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Web.Controllers;

    [TestClass]
    public class ExpenseTravelControllerTests
    {
        [TestMethod]
        public async Task ExpenseTravelController_Add_Test()
        {
            bool called = false;
            var expenseTravelRepository = new Data.Repositories.Fakes.StubIExpenseTravelRepository();

            var newExpenseTravel = new ExpenseTravel()
            {
                ExpenseId = 1,
                Distance = 10
            };

            expenseTravelRepository.AddAsyncExpenseTravel = (expenseTravel) =>
            {
                Assert.IsTrue(newExpenseTravel.ExpenseId == expenseTravel.ExpenseId);
                called = true;

                return Task.FromResult(10);
            };

            var target = new ExpenseTravelsController(expenseTravelRepository);
            await target.Add(newExpenseTravel);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExpenseTravelController_Add_Exception_Test()
        {
            var expenseTravelRepository = new Data.Repositories.Fakes.StubIExpenseTravelRepository();
            var target = new ExpenseTravelsController(expenseTravelRepository);
            await target.Add(null);
        }

        [TestMethod]
        public async Task ExpenseTravelController_Update_Test()
        {
            bool called = false;
            var expenseTravelRepository = new Data.Repositories.Fakes.StubIExpenseTravelRepository();

            var updateExpenseTravel = new ExpenseTravel()
            {
                ExpenseId = 1,
                Distance = 10
            };

            expenseTravelRepository.UpdateAsyncExpenseTravel = (expenseTravel) =>
            {
                Assert.IsTrue(updateExpenseTravel.ExpenseId == expenseTravel.ExpenseId);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new ExpenseTravelsController(expenseTravelRepository);
            await target.Update(updateExpenseTravel);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExpenseTravelController_Update_Exception_Test()
        {
            var expenseTravelRepository = new Data.Repositories.Fakes.StubIExpenseTravelRepository();
            var target = new ExpenseTravelsController(expenseTravelRepository);
            await target.Update(null);
        }
    }
}