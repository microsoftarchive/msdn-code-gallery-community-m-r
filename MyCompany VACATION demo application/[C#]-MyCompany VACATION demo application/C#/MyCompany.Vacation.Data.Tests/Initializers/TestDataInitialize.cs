
namespace MyCompany.Data.Test.Initializers
{
    using System.Data.Entity;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Model;

    /// <summary>
    /// Tes class to initialize 
    /// </summary>
    [TestClass]
    public static class TestDataInitialize
    {
        /// <summary>
        /// This method is called before launching the unit tests that are in this assembly
        /// </summary>
        /// <param name="context">Test context</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Mapper.CreateMap<VacationRequest, VacationRequestDTO>();
        }

    }
}
