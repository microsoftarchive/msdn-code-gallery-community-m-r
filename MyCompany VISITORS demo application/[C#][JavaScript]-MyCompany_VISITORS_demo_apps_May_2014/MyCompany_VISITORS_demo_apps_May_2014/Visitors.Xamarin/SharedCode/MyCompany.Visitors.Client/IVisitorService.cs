
namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client.Web;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the visitor service webapi Controller 
    /// </summary>
    public interface IVisitorService : IBaseRequest
    {
        /// <summary>
        /// GetByVisitor visitor by Id
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>Visitor</returns>
        Task<Visitor> Get(int visitorId, PictureType pictureType);

        /// <summary>
        /// GetByVisitor Visitors
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of visitors</returns>
        Task<IList<Visitor>> GetVisitors(string filter, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// GetByVisitor Visitor Count
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>Number of visitors</returns>
        Task<int> GetCount(string filter);

        /// <summary>
        /// Add new visitor.
        /// </summary>
        /// <param name="visitor">visitor information</param>
        /// <returns>visitorId</returns>
        Task<int> Add(Visitor visitor);

        /// <summary>
        /// Update visitor
        /// </summary>
        /// <param name="visitor">visitor information</param>
        Task Update(Visitor visitor);

        /// <summary>
        /// Delete visitor
        /// </summary>
        /// <param name="visitorId">visitorId</param>
        Task Delete(int visitorId);
    }
}
