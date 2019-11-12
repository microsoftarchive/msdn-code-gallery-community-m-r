
namespace MyCompany.Vacation.Client.OData.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Client.Tests.ODataServiceReference;
    using System.Configuration;
    using MyCompany.Vacation.Client.Tests;
    using System.Data.Services.Client;
    using MyCompany.Vacation.Client.Tests.OData;
    using System.Collections.Generic;

    [TestClass]
    public class EmployeesODataController_Integration_Tests
    {
        private ContainerFactory _containerFactory = new ContainerFactory();

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_OData")]
        public void Integration_EmployeesODataController_Get_Test()
        {
            var container = _containerFactory.Create();

            IEnumerable<Employee> result = container.EmployeesOData.Take(10).ToList();
            
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_OData")]
        public void Integration_EmployeesODataController_GetEntityByKey_Test()
        {
            int employeeId = 1;
            var container = _containerFactory.Create();

            IEnumerable<Employee> result = container.EmployeesOData.Where(e => e.EmployeeId == employeeId);
            
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_OData")]
        public void Integration_EmployeesODataController_GetVacationRequests_Test()
        {
            int employeeId = 1;
            var container = _containerFactory.Create();

            IEnumerable<VacationRequest> result = container.VacationRequestsOData.Where(vr => vr.EmployeeId == employeeId);
            
            Assert.IsTrue(result.Any());
        }
    }
}
