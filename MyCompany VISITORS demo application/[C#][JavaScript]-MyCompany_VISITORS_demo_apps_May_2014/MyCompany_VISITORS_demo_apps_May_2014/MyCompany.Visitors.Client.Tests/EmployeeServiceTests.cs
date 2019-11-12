namespace MyCompany.Visitors.Client.Tests
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
        public async Task EmployeeService_Get_Test()
        {
            var context = new Data.MyCompanyContext();
            int employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.EmployeeService.Get(employeeId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeePictures);
            Assert.IsTrue(result.EmployeeId == employeeId);
            Assert.IsTrue(result.EmployeePictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task EmployeeService_GetEmployees_Test()
        {
            var context = new Data.MyCompanyContext();
            int pagesize = 1;
            int count = 0;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.EmployeeService.GetEmployees(string.Empty, PictureType.Small, pagesize, count);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.First().EmployeePictures);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 1);
        }
    }
}
