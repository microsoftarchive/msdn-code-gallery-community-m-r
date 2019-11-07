namespace MyCompany.Expenses.Data.Tests.Initializers
{
    using System.Data.Entity;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Data.Test.Initializers;
    using MyCompany.Expenses.Model;

    /// <summary>
    /// Test class to initialize 
    /// </summary>
    [TestClass]
    public class TestDataInitialize
    {
        /// <summary>
        /// This method is called before launching the unit tests that are in this assembly
        /// </summary>
        /// <param name="context">Test context</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Mapper.CreateMap<Expense, ExpenseDTO>();
        }

    }
}
