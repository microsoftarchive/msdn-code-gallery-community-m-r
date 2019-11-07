
namespace MyCompany.Expenses.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class EmployeePicturePictureRepositoryTests
    {
        IEmployeePictureRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new EmployeePictureRepository(new MyCompanyContext());
        }

        [TestMethod]
        public async Task EmployeePictureRepository_AddEmployeePicture_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.EmployeePictures.Count() + 1;
            
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
            var employeePicture = context.EmployeePictures.OrderBy(ep => ep.EmployeePictureId).FirstOrDefault();
            var oldPictureType = employeePicture.PictureType;

            employeePicture.PictureType = PictureType.Big;
            await target.UpdateAsync(employeePicture);

            var actual = context.EmployeePictures.OrderBy(ep => ep.EmployeePictureId).FirstOrDefault(p => p.EmployeePictureId == employeePicture.EmployeePictureId);

            Assert.AreEqual((int)employeePicture.PictureType, (int)actual.PictureType);

            // restore old data
            employeePicture.PictureType = oldPictureType;
            await target.UpdateAsync(employeePicture);
        }

        [TestMethod]
        public async Task EmployeePictureRepository_DeleteEmployeePicture_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employeePicture = context.EmployeePictures.First();
            int expected = context.EmployeePictures.Count() - 1;

            await target.DeleteAsync(employeePicture.EmployeePictureId);

            int actual = context.EmployeePictures.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EmployeePictureRepository_DeleteEmployeePicture_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.EmployeePictures.Count();

            await target.DeleteAsync(0);

            int actual = context.EmployeePictures.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}