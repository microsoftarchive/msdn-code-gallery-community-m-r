namespace MyCompany.Travel.Web.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Controllers;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;

    [TestClass]
    public class TravelAttachmentsControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelAttachmentsController_Constructor_No_Repository_Test()
        {
            var target = new TravelAttachmentsController(null);
        }

        [TestMethod]
        public async Task TravelAttachmentsController_Add_Test()
        {
            bool called = false;
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();

            var newTravelAttachments = new TravelAttachment()
            {
                TravelRequestId = 1,
            };

            travelAttachmentsRepository.AddAsyncTravelAttachment = (travelAttachment) =>
            {
                Assert.IsTrue(newTravelAttachments.TravelRequestId == travelAttachment.TravelRequestId);
                called = true;

                return Task.FromResult(10);
            };

            var target = new TravelAttachmentsController(travelAttachmentsRepository);
            var idAdded = await  target.Add(newTravelAttachments);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TravelAttachmentsController_Add_Exception_Test()
        {
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
            var target = new TravelAttachmentsController(travelAttachmentsRepository);
            await target.Add(null);
        }

        [TestMethod]
        public async Task TravelAttachmentsController_Update_Test()
        {
            bool called = false;
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();

            var updateTravelAttachments = new TravelAttachment()
            {
                TravelRequestId = 1,
            };

            travelAttachmentsRepository.UpdateAsyncTravelAttachment = (travelAttachment) =>
            {
                Assert.IsTrue(updateTravelAttachments.TravelRequestId == travelAttachment.TravelRequestId);
                called = true;

                return Task.FromResult(string.Empty);
            };

            var target = new TravelAttachmentsController(travelAttachmentsRepository);
            await target.Update(updateTravelAttachments);

            Assert.IsTrue(called);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TravelAttachmentsController_Update_Exception_Test()
        {
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
            var target = new TravelAttachmentsController(travelAttachmentsRepository);
            await target.Update(null);
        }

        [TestMethod]
        public async Task TravelAttachmentsController_Delete_Test()
        {
            bool called = false;
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
                        
            travelAttachmentsRepository.DeleteAsyncInt32 = (id) =>
            {
                Assert.IsTrue(id == 1);
                called = true;

                return Task.FromResult(string.Empty);
            };

            var target = new TravelAttachmentsController(travelAttachmentsRepository);
            await target.Delete(1);

            Assert.IsTrue(called);            
        }

        [TestMethod]
        public async Task TravelAttachmentsController_Get_Test()
        {
            bool called = false;
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();

            travelAttachmentsRepository.GetAsyncInt32 = (id) =>
            {
                called = true;

                return Task.FromResult(new TravelAttachment());
            };

            var target = new TravelAttachmentsController(travelAttachmentsRepository);
            await target.Get(1);

            Assert.IsTrue(called);            
        }

        [TestMethod]
        public void TravelAttachmentsController_UnquoteFileName_Test()
        {
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
            var target = new TravelAttachmentsController(travelAttachmentsRepository);

            var result = target.UnquoteFileName("\"potatoe\"");

            Assert.AreEqual("potatoe", result);
        }

        [TestMethod]
        public void TravelAttachmentsController_UnquoteFileName_EmptyTest_Test()
        {
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
            var target = new TravelAttachmentsController(travelAttachmentsRepository);

            var result = target.UnquoteFileName(String.Empty);

            Assert.AreEqual(String.Empty, result);
        }

        [TestMethod]
        public void TravelAttachmentsController_UnquoteFileName_NoQuotes_Test()
        {
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
            var target = new TravelAttachmentsController(travelAttachmentsRepository);

            var result = target.UnquoteFileName("potatoe");

            Assert.AreEqual("potatoe", result);
        }

        [TestMethod]
        public async Task TravelAttachmentsController_DownloadFile_Test()
        {
            var travelAttachmentsRepository = new Data.Repositories.Fakes.StubITravelAttachmentRepository();
            travelAttachmentsRepository.GetAsyncInt32 = (id) =>
            {
                return Task.FromResult(new TravelAttachment()
                    {
                        FileName = "MyFile.bin",
                        Content = new byte[256]
                    });
            };

            var target = new TravelAttachmentsController(travelAttachmentsRepository);

            var result = await target.DownloadFile(1);
            

            Assert.AreEqual("MyFile.bin",result.Content.Headers.ContentDisposition.FileName);
            Assert.AreEqual("application/octet-stream",result.Content.Headers.ContentType.MediaType);
            byte[] content = await result.Content.ReadAsByteArrayAsync();

            Assert.AreEqual(256, content.Length);
        }
    }
}