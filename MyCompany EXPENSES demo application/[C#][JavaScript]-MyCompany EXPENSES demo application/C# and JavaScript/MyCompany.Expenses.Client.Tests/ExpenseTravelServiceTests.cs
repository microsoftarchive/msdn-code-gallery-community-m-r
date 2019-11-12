namespace MyCompany.Expenses.Client.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpenseTravelServiceTests
    {
       
        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseTravelService_AddExpenseTravel_Added_NotFail_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.ExpenseTravels.Count() + 1;

            var expenseId = context.Expenses.Include("ExpenseTravel")
                    .Where(e => e.ExpenseTravel == null).First().ExpenseId;

            var expenseTravel = new ExpenseTravel()
            {
                ExpenseId = expenseId,
                Distance = 1,
                From = "From",
                To = "To",
                Expense = null,
            };

            await client.ExpenseTravelService.Add(expenseTravel);

            int actual = context.ExpenseTravels.Count();
            Assert.AreEqual(expected, actual);

            expenseTravel.From = Guid.NewGuid().ToString();
            await client.ExpenseTravelService.Update(expenseTravel);

            var actualUpdated = context.ExpenseTravels.Where(t => t.ExpenseId == expenseTravel.ExpenseId).FirstOrDefault();

            Assert.AreEqual(expenseTravel.From, actualUpdated.From);
        }

        
    }
}
