
namespace MyCompany.Expenses.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class ExpenseTravelRepositoryTests
    {
        ExpenseTravelRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new ExpenseTravelRepository(new MyCompanyContext());
        }

        [TestMethod]
        public async Task ExpenseRepository_GetAllExpenseTravels_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.ExpenseTravels.Count();

            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task ExpenseRepository_AddExpenseTravel_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.ExpenseTravels.Count() + 1;

            var expenseId = context.Expenses.First().ExpenseId;
            var expenseTravel = new ExpenseTravel()
            {
                ExpenseId = expenseId, 
                Distance = 1,
                From = "From",
                To = "To"
            };

            await target.AddAsync(expenseTravel);

            int actual = context.ExpenseTravels.Count();
            Assert.AreEqual(expected, actual);

            expenseTravel.From = Guid.NewGuid().ToString();
            await target.UpdateAsync(expenseTravel);

            var actualUpdated = await target.GetAsync(expenseTravel.ExpenseId);

            Assert.AreEqual(expenseTravel.From, actualUpdated.From);
        }


        [TestMethod]
        public async Task ExpenseRepository_DeleteExpenseTravel_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();

            var expenseId = context.Expenses.Where(e => e.ExpenseTravel == null).First().ExpenseId;
            var newExpenseTravel = new ExpenseTravel()
            {
                ExpenseId = expenseId,
                Distance = 1,
                From = "From",
                To = "To"
            };

            await target.AddAsync(newExpenseTravel);

            var expenseTravel = context.ExpenseTravels.FirstOrDefault();
            int expected = context.ExpenseTravels.Count() - 1;

            await target.DeleteAsync(expenseTravel.ExpenseId);

            int actual = context.ExpenseTravels.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ExpenseRepository_DeleteExpenseTravel_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.ExpenseTravels.Count();

            await target.DeleteAsync(0);

            int actual = context.ExpenseTravels.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
