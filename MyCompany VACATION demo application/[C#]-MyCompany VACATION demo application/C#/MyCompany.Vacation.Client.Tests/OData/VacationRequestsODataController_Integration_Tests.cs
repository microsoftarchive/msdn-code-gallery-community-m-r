
namespace MyCompany.Vacation.Client.OData.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Client.Tests.ODataServiceReference;
    using System.Configuration;
    using MyCompany.Vacation.Client.Tests;
    using System.Data.Services.Client;
    using MyCompany.Vacation.Client.Tests.OData;

    [TestClass]
    public class VacationRequestsODataController_Integration_Tests
    {
        private ContainerFactory _containerFactory = new ContainerFactory();

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_OData")]
        public void Integration_VacationRequestsV2Controller_Get_Test()
        {
            string securityToken = string.Empty;
            var container = _containerFactory.Create();
            
            var result = container.VacationRequestsOData.Where(vr => vr.From.Year == DateTime.Now.Year).ToList();

            Assert.IsTrue(result.Any());
        }
    }
}
