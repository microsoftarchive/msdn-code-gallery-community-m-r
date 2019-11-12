using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Data;
using MyEvents.Data.Test.Initializers;
using MyEvents.Model;

namespace MyEvents.Api.Tests
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
