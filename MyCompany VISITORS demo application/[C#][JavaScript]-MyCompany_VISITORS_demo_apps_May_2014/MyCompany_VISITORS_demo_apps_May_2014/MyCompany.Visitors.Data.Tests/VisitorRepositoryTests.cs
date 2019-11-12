namespace MyCompany.Visitors.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class VisitorRepositoryTests
    {
        [TestMethod]
        public async Task VisitorRepository_GetVisitor_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int visitorId = context.Visitors.FirstOrDefault().VisitorId;

            var target = new VisitorRepository(context);
            var result = await target.GetAsync(visitorId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.VisitorId == visitorId);
        }

        [TestMethod]
        public async Task VisitorRepository_GetAllVisitors_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Visitors.Count();

            var target = new VisitorRepository(context);
            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task VisitorRepository_GetCompleteInfo_Test()
        {
            var context = new MyCompanyContext();
            int visitorId = context.Visitors.FirstOrDefault(v => v.VisitorPictures.Any()).VisitorId;

            var target = new VisitorRepository(context);
            var result = await target.GetCompleteInfoAsync(visitorId, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.VisitorId == visitorId);

            if (result.VisitorPictures != null)
            {
                Assert.IsTrue(result.VisitorPictures.Any(v => v.PictureType == PictureType.Small));
            }
            
        }

        [TestMethod]
        public async Task VisitorRepository_GetVisitors_Test()
        {
            var context = new MyCompanyContext();
            int pageSize = 1;
            int pageCount = 1;

            var target = new VisitorRepository(context);
            var results = await target.GetVisitorsAsync(string.Empty, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.All(v=>
            {
                if ( v.VisitorPictures != null)
                {
                   return  v.VisitorPictures.All(vp=>vp.PictureType==PictureType.Small);
                }

                return true;
            }));
        }

        [TestMethod]
        public async Task VisitorRepository_GetVisitors_ZeroResults_Test()
        {
            var context = new MyCompanyContext();
            int pageSize = 1;
            int pageCount = 1;

            var target = new VisitorRepository(context);
            var results = await target.GetVisitorsAsync(Guid.NewGuid().ToString(), PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public async Task VisitorRepository_GetVisitors_Filtered_Test()
        {
            var context = new MyCompanyContext();
            var filter = context.Visitors.First().Company;
            int pageSize = 50;
            int pageCount = 0;

            var target = new VisitorRepository(context);
            var results = await target.GetVisitorsAsync(filter, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.All(v =>
            {
                if (v.VisitorPictures != null)
                {
                    return v.VisitorPictures.All(vp => vp.PictureType == PictureType.Small);
                }

                return true;
            }));
        }

        [TestMethod]
        public async Task VisitorRepository_GetVisitors_FilterByEmail_Test()
        {
            var context = new MyCompanyContext();
            var filter = context.Visitors.First().Email;
            int pageSize = 1;
            int pageCount = 0;

            var target = new VisitorRepository(context);
            var results = await target.GetVisitorsAsync(filter, PictureType.Small, pageSize, pageCount);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 1);
            Assert.IsTrue(filter.Contains(results.First().Email));
        }

        [TestMethod]
        public async Task VisitorRepository_GetCount_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Visitors.Count();

            var target = new VisitorRepository(context);
            var actual = await target.GetCountAsync(string.Empty);

            Assert.AreEqual(expectedCount, actual);
        }

        [TestMethod]
        public async Task VisitorRepository_GetCount_Filtered_Test()
        {
            var context = new MyCompanyContext();
            var visitors = context.Visitors;

            var target = new VisitorRepository(context);
            var actual = await target.GetCountAsync(visitors.First().Company);

            Assert.IsTrue(actual == 1);
        }

        [TestMethod]
        public async Task VisitorRepository_GetCount__ZeroResults_Test()
        {
            var context = new MyCompanyContext();

            var target = new VisitorRepository(context);
            var actual = await target.GetCountAsync(Guid.NewGuid().ToString());

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task VisitorRepository_AddVisitor_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Visitors.Count() + 1;

            var target = new VisitorRepository(context);
            var Visitor = new Visitor()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Company = "MyCompany",
                Email = "Email",
                CreatedDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow, 
            };

            await target.AddAsync(Visitor);

            int actual = context.Visitors.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitorRepository_UpdateVisitor_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var visitor = context.Visitors.FirstOrDefault();
            var target = new VisitorRepository(context);

            visitor.Company = Guid.NewGuid().ToString();

            await target.UpdateAsync(visitor);

            var actual = await target.GetAsync(visitor.VisitorId);

            Assert.AreEqual(visitor.Company, actual.Company);
        }

        [TestMethod]
        public async Task VisitorRepository_DeleteVisitor_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            IVisitorRepository target = new VisitorRepository(context);

            var newVisitor = new Visitor()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Company = "MyCompany",
                Email = "Email",
                CreatedDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow, 
            };

            int visitorId = await target.AddAsync(newVisitor);

            int expected = context.Visitors.Count() - 1;

            await target.DeleteAsync(visitorId);

            int actual = context.Visitors.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitorRepository_DeleteVisitor_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Visitors.Count();

            IVisitorRepository target = new VisitorRepository(context);
            await target.DeleteAsync(-1);

            int actual = context.Visitors.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
