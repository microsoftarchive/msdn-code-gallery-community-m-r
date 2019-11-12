using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Model;

namespace MyEvents.Data.Test.Initializers
{
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
            //Set default initializer
            Database.SetInitializer<MyEventsContext>(new MyEventsContextInitializer());
        }

    }
}
