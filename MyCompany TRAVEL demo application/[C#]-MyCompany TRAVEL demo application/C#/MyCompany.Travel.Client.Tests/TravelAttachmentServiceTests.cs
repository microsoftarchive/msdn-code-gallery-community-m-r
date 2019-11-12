
namespace MyCompany.Travel.Client.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TravelAttachmentServiceTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelAttachmentService_AddTravelAttachment_Added_NotFail_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            int expected = context.TravelAttachments.Count() + 1;

            var travelRequestId = context.TravelRequests.FirstOrDefault().TravelRequestId;
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            var travelAttachment = new TravelAttachment()
            {
                Name = "Boarding Pass",
                FileName = "BoardingPass.pdf",
                Content = encoder.GetBytes("sample"),
                AttachmentType = AttachmentType.BoardingPass,
                TravelRequestId = travelRequestId,
            };

            int travelAttachmentId = await client.TravelAttachmentService.Add(travelAttachment);

            int actual = context.TravelAttachments.Count();
            Assert.AreEqual(expected, actual);

            travelAttachment.TravelAttachmentId = travelAttachmentId;
            travelAttachment.AttachmentType = AttachmentType.Other;
            await client.TravelAttachmentService.Update(travelAttachment);

            var actualUpdated = context.TravelAttachments.Where(t => t.TravelAttachmentId == travelAttachmentId).FirstOrDefault();

            await client.TravelAttachmentService.Delete(travelAttachmentId);

            actual = context.TravelAttachments.Count();

            Assert.AreEqual(expected - 1, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task TravelAttachmentService_Get_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);

            var context = new Data.MyCompanyContext();
            var travelAttachmentId = context.TravelAttachments.FirstOrDefault().TravelAttachmentId;

            TravelAttachment travelAttachment = await client.TravelAttachmentService.Get(travelAttachmentId);

            Assert.IsNotNull(travelAttachment);
            Assert.AreEqual(travelAttachmentId, travelAttachment.TravelAttachmentId);
        }
    }
}
