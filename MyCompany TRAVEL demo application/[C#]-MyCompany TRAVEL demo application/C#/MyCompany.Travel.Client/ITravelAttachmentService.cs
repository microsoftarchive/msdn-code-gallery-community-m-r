namespace MyCompany.Travel.Client
{
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the expensetravel webapi Controller 
    /// </summary>
    public interface ITravelAttachmentService
    {
        /// <summary>
        /// Add new travel attachment 
        /// </summary>
        /// <param name="travelAttachment">travel Attachment information</param>
        Task<int> Add(TravelAttachment travelAttachment);

        /// <summary>
        /// Update travel attachment 
        /// </summary>
        /// <param name="travelAttachment">travel Attachment information</param>
        Task Update(TravelAttachment travelAttachment);

        /// <summary>
        /// Delete travel attachment 
        /// </summary>
        /// <param name="travelAttachmentId">travel Attachment Id</param>
        Task Delete(int travelAttachmentId);

        /// <summary>
        /// Get travel attachment 
        /// </summary>
        /// <param name="travelAttachmentId">travel Attachment Id</param>
        Task<TravelAttachment> Get(int travelAttachmentId);
    }
}
