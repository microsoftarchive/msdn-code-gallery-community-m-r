
namespace MyCompany.Vacation.Client.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;

    [TestClass]
    public class EmployeeController_Integration_Tests
    {
        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Integration_WebAPI")]
        public async Task Integration_EmployeeController_GetLoggedEmployeeInfo_Test()
        {
            BaseRequest request = new BaseRequest(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/current/{1}", SecurityHelper.UrlBase, (int)PictureType.Small);

            var result = await request.GetAsync<Employee>(url);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Count() == 1);
        }
    }
}
