
namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client.Web;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the visitor picture service webapi Controller 
    /// </summary>
    public interface IVisitorPictureService : IBaseRequest
    {
        /// <summary>
        /// Add new visitor picture.
        /// </summary>
        /// <param name="visitorPicture">Visitor picture</param>
        /// <returns>Visitor picture id</returns>
        Task<int> Add(VisitorPicture visitorPicture);

        /// <summary>
        /// Update visitor picture.
        /// </summary>
        /// <param name="visitorPicture">Visitor picture</param>
        Task Update(VisitorPicture visitorPicture);

        /// <summary>
        /// Updates or adds a visitor picture.
        /// </summary>
        /// <param name="visitorPicture">Visitor picture</param>

        Task AddOrUpdatePictures(ICollection<VisitorPicture> visitorPicture);

        /// <summary>
        /// Delete visitor picture.
        /// </summary>
        /// <param name="visitorPictureId">Visitor picture id</param>
        Task Delete(int visitorPictureId);

        /// <summary>
        /// Get visitor picture by visitor.
        /// </summary>
        /// <param name="visitorId">Visitor id</param>
        /// <param name="pictureType">Picture type</param>
        /// <returns>Visitor picture</returns>
        Task<byte[]> GetByVisitor(int visitorId, PictureType pictureType);        
    }
}
