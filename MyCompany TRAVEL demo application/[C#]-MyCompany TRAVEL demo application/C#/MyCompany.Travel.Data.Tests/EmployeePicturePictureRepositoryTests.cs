namespace MyCompany.Travel.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Data.Repositories;

    [TestClass]
    public class EmployeePicturePictureRepositoryTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Constructor_ShouldThrowAnExceptionIfContextIsntSupplied()
        {
            var target = new EmployeePictureRepository(null);
        }

        [TestMethod]
        public async Task EmployeePictureRepository_AddEmployeePicture_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.EmployeePictures.Count() + 1;

            var target = new EmployeePictureRepository(new MyCompanyContext());

            var employeePictureId = context.EmployeePictures.Select(e => e.EmployeePictureId).Max() + 1;

            var employeePicture = new EmployeePicture()
            {
                EmployeePictureId = employeePictureId,
                EmployeeId = 1,
                PictureType = PictureType.Small,
                Content = System.Text.Encoding.UTF8.GetBytes("sample"),
            };

            await target.AddAsync(employeePicture);

            int actual = context.EmployeePictures.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeePictureRepository_UpdateEmployeePicture_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employeePicture = context.EmployeePictures.FirstOrDefault();
            var target = new EmployeePictureRepository(new MyCompanyContext());

            employeePicture.PictureType = PictureType.Big;
            await target.UpdateAsync(employeePicture);

            var actual = context.EmployeePictures.FirstOrDefault(p => p.EmployeePictureId == employeePicture.EmployeePictureId);

            Assert.AreEqual((int)employeePicture.PictureType, (int)actual.PictureType);
        }

        [TestMethod]
        public async Task EmployeePictureRepository_DeleteEmployeePicture_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employeePicture = context.EmployeePictures.First();
            int expected = context.EmployeePictures.Count() - 1;

            IEmployeePictureRepository target = new EmployeePictureRepository(new MyCompanyContext());
            await target.DeleteAsync(employeePicture.EmployeePictureId);

            int actual = context.EmployeePictures.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeePictureRepository_DeleteEmployeePicture_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.EmployeePictures.Count();

            IEmployeePictureRepository target = new EmployeePictureRepository(new MyCompanyContext());
            await target.DeleteAsync(0);

            int actual = context.EmployeePictures.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}