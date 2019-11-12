
namespace MyCompany.Travel.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Travel.Model;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Base contract for Travel Request Attachments repository
    /// </summary>
    public interface ITravelAttachmentRepository : IDisposable
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="travelAttachmentId">attachment id</param>
        /// <returns></returns>
        Task<TravelAttachment> GetAsync(int travelAttachmentId);

        /// <summary>
        /// Get All attachements
        /// </summary>
        /// <returns>List of attachements</returns>
        Task<IEnumerable<TravelAttachment>> GetAllAsync();

        /// <summary>
        /// Add new attachement
        /// </summary>
        /// <param name="TravelAttachment">attachement information</param>
        /// <returns>attachementId</returns>
        Task<int> AddAsync(TravelAttachment TravelAttachment);

        /// <summary>
        /// Update attachement
        /// </summary>
        /// <param name="TravelAttachment">attachement information</param>
        Task UpdateAsync(TravelAttachment TravelAttachment);

        /// <summary>
        /// Delete attachement
        /// </summary>
        /// <param name="travelAttachmentId">attachement to delete</param>
        Task DeleteAsync(int travelAttachmentId);
    }
}
