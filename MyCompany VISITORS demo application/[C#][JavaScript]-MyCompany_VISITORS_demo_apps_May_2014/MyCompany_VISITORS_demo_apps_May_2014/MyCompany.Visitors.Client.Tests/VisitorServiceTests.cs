namespace MyCompany.Visitors.Client.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VisitorServiceTests
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

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitorService_Add_Update_Delete_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.Visitors.Count() + 1;
            var visitor = new Visitor()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Company = "MyCompany",
                Email = "Email",
                CreatedDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
            };

            int id = await client.VisitorService.Add(visitor);

            int actual = context.Visitors.Count();
            Assert.AreEqual(expected, actual);

            visitor.VisitorId = id;
            visitor.Email = Guid.NewGuid().ToString();
            await client.VisitorService.Update(visitor);

            var actualUpdated = context.Visitors.Where(t => t.VisitorId == id).FirstOrDefault();

            Assert.AreEqual(visitor.Email, actualUpdated.Email);

            await client.VisitorService.Delete(id);

            actual = context.Visitors.Count();

            Assert.AreEqual(expected - 1, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitorService_Get_Test()
        {
            var context = new Data.MyCompanyContext();
            int visitorId = context.Visitors.FirstOrDefault().VisitorId;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var result = await client.VisitorService.Get(visitorId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.VisitorPictures);
            Assert.IsTrue(result.VisitorId == visitorId);
            Assert.IsTrue(result.VisitorPictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitorService_GetVisitors_Test()
        {
            var context = new Data.MyCompanyContext();
            int pageSize = 1;
            int pageCount = 0;

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var results = await client.VisitorService.GetVisitors(string.Empty, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 1);
            Assert.IsNotNull(results.First().VisitorPictures);
            Assert.IsTrue(results.First().VisitorPictures.Count == 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task VisitorService_GetCount_Test()
        {
            var context = new Data.MyCompanyContext();
            int expectedCount = context.Visitors.Count();

            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var actual = await client.VisitorService.GetCount(string.Empty);

            Assert.AreEqual(expectedCount, actual);
        }
    }
}
