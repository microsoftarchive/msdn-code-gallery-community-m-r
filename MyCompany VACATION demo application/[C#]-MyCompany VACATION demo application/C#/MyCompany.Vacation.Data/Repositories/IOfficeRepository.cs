
namespace MyCompany.Vacation.Data.Repositories
{
    using MyCompany.Vacation.Model;
    using System;

    /// <summary>
    /// Base contract for office repository
    /// </summary>
    public interface IOfficeRepository
        : IDisposable
    {
        /// <summary>
        /// Add new office
        /// </summary>
        /// <param name="office">office information</param>
        /// <returns>officeId</returns>
        int Add(Office office);

        /// <summary>
        /// Update office
        /// </summary>
        /// <param name="office">office information</param>
        void Update(Office office);

        /// <summary>
        /// Delete office
        /// </summary>
        /// <param name="officeId">office to delete</param>
        void Delete(int officeId);
    }
}
