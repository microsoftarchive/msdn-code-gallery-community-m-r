namespace MyCompany.Visitors.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class VisitorPictureRepositoryTests
    {
       

        [TestMethod]
        public async Task VisitorPictureRepository_GetVisitorPictureByVisitorId_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            var visitorPicture = context.VisitorPictures.FirstOrDefault();

            var target = new VisitorPictureRepository(new MyCompanyContext());
            var result = await target.GetAsync(visitorPicture.VisitorId, visitorPicture.PictureType);

            Assert.IsNotNull(result);
            Assert.AreEqual(visitorPicture.VisitorPictureId, result.VisitorPictureId);
        }

        [TestMethod]
        public async Task VisitorPictureRepository_AddVisitorPicture_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.VisitorPictures.Count() + 1;

            var target = new VisitorPictureRepository(new MyCompanyContext());
            var visitorId = context.Visitors.FirstOrDefault().VisitorId;
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            var VisitorPicture = new VisitorPicture()
            {
                VisitorId = visitorId,
                PictureType = PictureType.Big,
                Content = encoder.GetBytes("sample"),
            };

            await target.AddAsync(VisitorPicture);

            int actual = context.VisitorPictures.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitorPictureRepository_UpdateVisitorPicture_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var visitorPicture = context.VisitorPictures.FirstOrDefault();
            var target = new VisitorPictureRepository(new MyCompanyContext());

            var context2 = new MyCompanyContext();
            visitorPicture.PictureType = PictureType.Unknown;
            await target.UpdateAsync(visitorPicture);

            var actual = context.VisitorPictures.Find(visitorPicture.VisitorPictureId);

            Assert.AreEqual(visitorPicture.PictureType, actual.PictureType);
        }

        [TestMethod]
        public async Task VisitorPictureRepository_AddOrUpdateExistingEntity_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var visitorPicture = (new MyCompanyContext()).VisitorPictures.FirstOrDefault();
            var target = new VisitorPictureRepository(new MyCompanyContext());

            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            var content = encoder.GetBytes(Guid.NewGuid().ToString());

            await target.AddOrUpdateAsync(visitorPicture.VisitorId, visitorPicture.PictureType, content);

            var actual = (new MyCompanyContext()).VisitorPictures.FirstOrDefault();

            Assert.AreEqual(content.Length, actual.Content.Length);
        }

        [TestMethod]
        public async Task VisitorPictureRepository_AddOrUpdateNewEntity_NotFail_Test()
        {
            var context = new MyCompanyContext();

            var target = new VisitorPictureRepository(new MyCompanyContext());
            var visitorId = await AddVisitor();

            int expected = context.VisitorPictures.Count();

            var pictureToRemove = context.VisitorPictures
                .First(p => p.VisitorId == visitorId && p.PictureType == PictureType.Small);

            await target.DeleteAsync(pictureToRemove.VisitorPictureId);

            var encoder = new System.Text.ASCIIEncoding();
            await target.AddOrUpdateAsync(visitorId, PictureType.Small, encoder.GetBytes("sample"));

            int actual = context.VisitorPictures.Count();
            Assert.AreEqual(expected, actual);
        }

        public async Task<int> AddVisitor()
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

            var visitorId = await target.AddAsync(Visitor);

            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            var visitorPicture = new VisitorPicture()
            {
                VisitorId = visitorId,
                PictureType = PictureType.Small,
                Content = encoder.GetBytes("sample"),
            };

            var visitorPictureRepository = new VisitorPictureRepository(new MyCompanyContext());
            await visitorPictureRepository.AddAsync(visitorPicture);

            return visitorId;
        }

        [TestMethod]
        public async Task VisitorPictureRepository_DeleteVisitorPicture_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            IVisitorPictureRepository target = new VisitorPictureRepository(new MyCompanyContext());

            var visitorId = context.Visitors.FirstOrDefault().VisitorId;
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            var newVisitorPicture = new VisitorPicture()
            {
                VisitorId = visitorId,
                PictureType = PictureType.Big,
                Content = encoder.GetBytes("sample"),
            };

            int visitorPictureId = await target.AddAsync(newVisitorPicture);

            int expected = context.VisitorPictures.Count() - 1;

            await target.DeleteAsync(visitorPictureId);

            int actual = context.VisitorPictures.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task VisitorPictureRepository_DeleteVisitorPicture_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.VisitorPictures.Count();

            IVisitorPictureRepository target = new VisitorPictureRepository(new MyCompanyContext());
            await target.DeleteAsync(-1);

            int actual = context.VisitorPictures.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
