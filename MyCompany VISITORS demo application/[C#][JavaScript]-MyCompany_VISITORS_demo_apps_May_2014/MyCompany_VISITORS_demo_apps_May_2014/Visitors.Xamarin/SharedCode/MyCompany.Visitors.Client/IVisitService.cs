
namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client.DocumentResponse;
    using MyCompany.Visitors.Client.Web;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the visit service webapi Controller 
    /// </summary>
    public interface IVisitService : IBaseRequest
    {
        /// <summary>
        /// Get visitor expense by Id
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<Visit> Get(int visitId, PictureType pictureType);

        /// <summary>
        /// Get Visits
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <param name="dateFilter">Date filter</param>
        /// <param name="toDate">To date filter</param>
        /// <returns>List of visits</returns>
        Task<IList<Visit>> GetVisits(string filter, PictureType pictureType, int pageSize, int pageCount, DateTime? dateFilter, DateTime? toDate);

        /// <summary>
        /// Get Visits from Date
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <param name="dateFilter">Date filter</param>
        /// <returns>List of visits</returns>
        Task<IList<Visit>> GetVisitsFromDate(string filter, PictureType pictureType, int pageSize, int pageCount, DateTime dateFilter);

        /// <summary>
        /// Get user Visits
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <param name="dateFilter">Date ti filter</param>
        /// <returns>List of visits</returns>
        Task<IList<Visit>> GetUserVisits(string filter, PictureType pictureType, int pageSize, int pageCount, DateTime dateFilter);

        /// <summary>
        /// Visit Count
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="dateFilter">DateFilter</param>
        /// <param name="toDate">To date Filter</param>
        /// <returns>Number of visits</returns>
        Task<int> GetCount(string filter, DateTime? dateFilter, DateTime? toDate);

        /// <summary>
        /// Visit Count from Date.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="dateFilter">DateFilter</param>
        /// <returns>Number of visits from Date</returns>
        Task<int> GetCountFromDate(string filter, DateTime dateFilter);

        /// <summary>
        /// Count user visits.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="dateFilter">Date ti filter</param>
        /// <returns>Number of visits</returns>
        Task<int> GetUserCount(string filter, DateTime dateFilter);

        /// <summary>
        /// Add new visit
        /// </summary>
        /// <param name="visit">visit information</param>
        /// <returns>visitId</returns>
        Task<int> Add(Visit visit);


        /// <summary>
        /// Update visit
        /// </summary>
        /// <param name="visit">visit information</param>
        Task Update(Visit visit);

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="visitId">Id to update</param>
        /// <param name="status">VisitStatus</param>
        Task UpdateStatus(int visitId, VisitStatus status);

        /// <summary>
        /// Delete visit
        /// </summary>
        /// <param name="visitId">visitId</param>
        Task Delete(int visitId);
    }
}
