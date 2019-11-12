namespace MyCompany.Visitors.Web.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Model;
    using MyCompany.Visitors.Web.Controllers;
    using System.Threading.Tasks;

    [TestClass]
    public class VisitorsControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Visitor repository is null.")]
        public void VisitorsController_Constructor_Failed_FirstArgument_test()
        {
            var target = new VisitorsController(null);
        }

        [TestMethod]
        public async Task VisitorsController_Get_Test()
        {
            bool called = false;
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();
            visitorRepository.GetCompleteInfoAsyncInt32PictureType = (id, picture) =>
            {
                called = true;
                return Task.FromResult(new Visitor());
            };

            var target = new VisitorsController(visitorRepository);
            var result = await target.Get(0, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitorsController_GetVisitors_Test()
        {
            bool called = false;
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();
            visitorRepository.GetVisitorsAsyncStringPictureTypeInt32Int32 = (id, picture, pageSize, pageCount) =>
            {
                called = true;
                return Task.FromResult(new List<Visitor>().AsEnumerable());
            };

            var target = new VisitorsController(visitorRepository);
            var result = await target.GetVisitors(string.Empty, PictureType.Small, 1, 1);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitorsController_GetCount_Test()
        {
            bool called = false;
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();
            visitorRepository.GetCountAsyncString = (filter) =>
            {
                called = true;
                return Task.FromResult(10);
            };

            var target = new VisitorsController(visitorRepository);
            var result = await target.GetCount(string.Empty);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task VisitorsController_Add_Test()
        {
            bool called = false;
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();

            var newVisitor = new Visitor()
            {
                VisitorId = 1,
            };

            visitorRepository.AddAsyncVisitor = (visitor) =>
            {
                Assert.IsTrue(visitor.VisitorId == newVisitor.VisitorId);
                called = true;
                return Task.FromResult(10);
            };

            var target = new VisitorsController(visitorRepository);
            await target.Add(newVisitor);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),"Visitor is null.")]
        public async Task VisitorsController_Add_Failed_Test()
        {
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();
            var target = new VisitorsController(visitorRepository);
            await target.Add(null);
        }

        [TestMethod]
        public async Task VisitorsController_Update_Test()
        {
            bool called = false;
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();

            var visitor = new Visitor()
            {
                VisitorId = 1,
            };

            visitorRepository.UpdateAsyncVisitor = (visitorUpdate) =>
            {
                Assert.IsTrue(visitorUpdate.VisitorId == visitor.VisitorId);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new VisitorsController(visitorRepository);
            await target.Update(visitor);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Visitor is null.")]
        public async Task VisitorsController_Update_Failed_Test()
        {
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();
            var target = new VisitorsController(visitorRepository);
            await target.Update(null);
        }

        [TestMethod]
        public async Task VisitorsController_Delete_Test()
        {
            bool called = false;
            var visitorRepository = new Data.Repositories.Fakes.StubIVisitorRepository();

            var visitor = new Visitor()
            {
                VisitorId = 1,
            };

            visitorRepository.DeleteAsyncInt32 = (id) =>
            {
                Assert.IsTrue(id == visitor.VisitorId);
                called = true;
                return Task.FromResult(string.Empty);
            };

            var target = new VisitorsController(visitorRepository);
            await target.Delete(visitor.VisitorId);

            Assert.IsTrue(called);
        }
    }
}
