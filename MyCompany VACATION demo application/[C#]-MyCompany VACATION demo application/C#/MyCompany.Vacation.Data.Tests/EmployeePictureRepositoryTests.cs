namespace MyCompany.Vacation.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    [TestClass]
    public class EmployeePicturePictureRepositoryTests
    {

        [TestMethod]
        public void EmployeePictureRepository_AddEmployeePicture_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.EmployeePictures.Count() + 1;

            var target = new EmployeePictureRepository(context);

            var employeePictureId = context.EmployeePictures.Select(e => e.EmployeePictureId).Max() + 1;
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;

            var employeePicture = new EmployeePicture()
            {
                EmployeePictureId = employeePictureId,
                EmployeeId = employeeId,
                PictureType = PictureType.Small,
                Content = System.Text.Encoding.UTF8.GetBytes("sample"),
            };

            target.Add(employeePicture);

            int actual = context.EmployeePictures.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmployeePictureRepository_UpdateEmployeePicture_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employeePicture = context.EmployeePictures.FirstOrDefault();
            var target = new EmployeePictureRepository(context);

            employeePicture.PictureType = PictureType.Big;
            target.Update(employeePicture);

            var actual = context.EmployeePictures.FirstOrDefault(p => p.EmployeePictureId == employeePicture.EmployeePictureId);

            Assert.AreEqual((int)employeePicture.PictureType, (int)actual.PictureType);
        }

        [TestMethod]
        public void EmployeePictureRepository_DeleteEmployeePicture_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employeePicture = context.EmployeePictures.First();
            int expected = context.EmployeePictures.Count() - 1;

            IEmployeePictureRepository target = new EmployeePictureRepository(context);
            target.Delete(employeePicture.EmployeePictureId);

            int actual = context.EmployeePictures.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmployeePictureRepository_DeleteEmployeePicture_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.EmployeePictures.Count();

            IEmployeePictureRepository target = new EmployeePictureRepository(context);
            target.Delete(0);

            int actual = context.EmployeePictures.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
