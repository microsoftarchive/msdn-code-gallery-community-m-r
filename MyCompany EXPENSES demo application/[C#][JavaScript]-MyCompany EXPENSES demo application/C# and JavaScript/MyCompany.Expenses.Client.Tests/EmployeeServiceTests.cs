
namespace MyCompany.Expenses.Client.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmployeeServiceTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task EmployeeService_GetLoggedEmployeeInfo_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.EmployeeService.GetLoggedEmployeeInfo(PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Count() == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task EmployeeService_GetEmployee_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var context = new Data.MyCompanyContext();
            int employeeId = context.Employees.First().EmployeeId;

            var result = await client.EmployeeService.GetEmployee(employeeId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeePictures.Count() == 1);
        }


    }
}
