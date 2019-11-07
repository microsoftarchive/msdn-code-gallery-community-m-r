
namespace MyCompany.Vacation.Data.Repositories
{
    using MyCompany.Vacation.Model;
    using System;

    /// <summary>
    /// Base contract for employee picture repository
    /// </summary>
    public interface IEmployeePictureRepository
        : IDisposable
    {
        /// <summary>
        /// Add new employee picture
        /// </summary>
        /// <param name="employeePicture">employee picture information</param>
        /// <returns>employeePictureId</returns>
        int Add(EmployeePicture employeePicture);

        /// <summary>
        /// Update employee picture
        /// </summary>
        /// <param name="employeePicture">employee picture information</param>
        void Update(EmployeePicture employeePicture);

        /// <summary>
        /// Delete employee picture
        /// </summary>
        /// <param name="employeePictureId">employee picture to delete</param>
        void Delete(int employeePictureId);
    }
}
