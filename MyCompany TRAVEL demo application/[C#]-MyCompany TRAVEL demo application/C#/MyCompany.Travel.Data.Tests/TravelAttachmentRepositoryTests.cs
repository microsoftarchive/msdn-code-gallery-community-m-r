namespace MyCompany.Travel.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class TravelAttachmentRepositoryTests
    {
        [TestMethod]
        public async Task TravelAttachmentRepository_GetTravelAttachment_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int TravelAttachmentId = context.TravelAttachments.FirstOrDefault().TravelAttachmentId;

            var target = new TravelAttachmentRepository(new MyCompanyContext());
            var result = await target.GetAsync(TravelAttachmentId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TravelAttachmentId == TravelAttachmentId);
        }

        [TestMethod]
        public async Task TravelAttachmentRepository_GetAllTravelAttachments_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.TravelAttachments.Count();

            var target = new TravelAttachmentRepository(new MyCompanyContext());
            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task TravelAttachmentRepository_AddTravelAttachment_Added_NotFail_Test()
        {
            using (var context = new MyCompanyContext())
            {
                int expected = new MyCompanyContext().TravelAttachments.Count() + 1;
                var travelRequestId = new MyCompanyContext().TravelRequests.FirstOrDefault().TravelRequestId;

                var target = new TravelAttachmentRepository(new MyCompanyContext());                
                System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
                var travelAttachment = new TravelAttachment()
                {
                    Name = "Boarding Pass",
                    FileName = "BoardingPass.pdf",
                    Content = encoder.GetBytes("sample"),
                    TravelRequestId = travelRequestId,
                };

                await target.AddAsync(travelAttachment);

                int actual = context.TravelAttachments.Count();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task TravelAttachmentRepository_UpdateTravelAttachment_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var TravelAttachment = context.TravelAttachments.FirstOrDefault();
            var target = new TravelAttachmentRepository(new MyCompanyContext());

            TravelAttachment.Name = "NewName";
            await target.UpdateAsync(TravelAttachment);

            var actual = await target.GetAsync(TravelAttachment.TravelAttachmentId);

            Assert.AreEqual("NewName", actual.Name  );
        }

        [TestMethod]
        public async Task TravelAttachmentRepository_DeleteTravelAttachment_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            ITravelAttachmentRepository target = new TravelAttachmentRepository(new MyCompanyContext());

            var travelRequestId = context.TravelRequests.FirstOrDefault().TravelRequestId;
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            var newTravelAttachment = new TravelAttachment()
            {
                Name = "Boarding Pass",
                FileName = "BoardingPass.pdf",
                Content = encoder.GetBytes("sample"),
                TravelRequestId = travelRequestId,
            };

            int travelAttachmentId = await target.AddAsync(newTravelAttachment);

            int expected = context.TravelAttachments.Count() - 1;

            await target.DeleteAsync(travelAttachmentId);

            int actual = context.TravelAttachments.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TravelAttachmentRepository_DeleteTravelAttachment_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.TravelAttachments.Count();

            ITravelAttachmentRepository target = new TravelAttachmentRepository(new MyCompanyContext());
            await target.DeleteAsync(-1);

            int actual = context.TravelAttachments.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
