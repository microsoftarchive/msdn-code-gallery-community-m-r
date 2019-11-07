namespace MyCompany.Visitors.Web.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Model;
    using MyCompany.Visitors.Web.Controllers;
    using System.Threading.Tasks;
    using System.Linq;

    [TestClass]
    public class VisitorPicturesControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Visitor picture repository is null.")]
        public void VisitorPictures_Constructor_Failed_test()
        {
            var target = new VisitorPicturesController(null);
        }

        [TestMethod]
        public void VisitorPictures_Add_Test()
        {
            bool called = false;
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();

            var newVisitor = new VisitorPicture()
            {
                VisitorPictureId = 1,
            };

            visitorPicturesRepository.AddAsyncVisitorPicture = (visitor) =>
            {
                Assert.IsTrue(visitor.VisitorPictureId == newVisitor.VisitorPictureId);
                called = true;
                return Task.FromResult(10);
            };

            var target = new VisitorPicturesController(visitorPicturesRepository);
            var task = target.Add(newVisitor);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void VisitorPictures_Add_Failed_Test()
        {
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();

            var target = new VisitorPicturesController(visitorPicturesRepository);
            var task = target.Add(null);

            Assert.IsTrue(task.Exception != null);
            Assert.IsTrue(task.Exception.InnerExceptions.Where(e => e.GetType() == typeof(ArgumentNullException)).Any());
        }

        [TestMethod]
        public void VisitorPictures_Update_Test()
        {
            bool called = false;
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();

            var updateVisitor = new VisitorPicture()
            {
                VisitorPictureId = 1,
            };

            visitorPicturesRepository.UpdateAsyncVisitorPicture = (visitor) =>
            {
                Assert.IsTrue(visitor.VisitorPictureId == updateVisitor.VisitorPictureId);
                called = true;

                return Task.Delay(0);
            };

            var target = new VisitorPicturesController(visitorPicturesRepository);
            var task = target.Update(updateVisitor);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void VisitorPictures_Update_Failed_Test()
        {
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();
            var target = new VisitorPicturesController(visitorPicturesRepository);
            var task = target.Update(null);

            Assert.IsTrue(task.Exception != null);
            Assert.IsTrue(task.Exception.InnerExceptions.Where(e => e.GetType() == typeof(ArgumentNullException)).Any());

        }

        [TestMethod]
        public void VisitorPictures_GetSpecificType_Test()
        {
            bool called = false;
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();

            var picture = new VisitorPicture()
            {
                VisitorPictureId = 1,
                Content = new byte[1]
            };

            visitorPicturesRepository.GetAsyncInt32PictureType = (visitor, pictureType) =>
            {
                called = true;
                return Task.FromResult(picture);
            };

            var target = new VisitorPicturesController(visitorPicturesRepository);
            var result = target.Get(1, PictureType.Small);

            Assert.IsNotNull(result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void VisitorPictures_GetSpecificType_NotFound_Test()
        {
            bool called = false;
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();

            visitorPicturesRepository.GetAsyncInt32PictureType = (visitor, pictureType) =>
            {
                called = true;
                return Task.FromResult<VisitorPicture>(null);
            };

            var target = new VisitorPicturesController(visitorPicturesRepository);
            var task = target.Get(1, PictureType.Small);

            Assert.IsNull(task.Result);
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void VisitorPictures_Delete_Test()
        {
            bool called = false;
            var visitorPicturesRepository = new Data.Repositories.Fakes.StubIVisitorPictureRepository();

            var visitor = new VisitorPicture()
            {
                VisitorPictureId = 1,
            };

            visitorPicturesRepository.DeleteAsyncInt32 = (id) =>
            {
                Assert.IsTrue(id == visitor.VisitorPictureId);
                called = true;

                return Task.Delay(0);
            };

            var target = new VisitorPicturesController(visitorPicturesRepository);
            target.Delete(visitor.VisitorPictureId);

            Assert.IsTrue(called);
        }
    }
}
