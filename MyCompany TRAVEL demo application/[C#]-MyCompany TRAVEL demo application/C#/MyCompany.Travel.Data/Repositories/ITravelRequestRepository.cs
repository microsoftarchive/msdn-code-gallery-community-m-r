namespace MyCompany.Travel.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Travel.Model;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Base contract for TravelRequest repository
    /// </summary>
    public interface ITravelRequestRepository : IDisposable
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="travelRequestId">Id</param>
        /// <returns></returns>
        Task<TravelRequest> GetAsync(int travelRequestId);

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="travelRequestId">Id</param>
        /// <returns></returns>
        Task<TravelRequest> GetWithEmployeeInfoAsync(int travelRequestId);

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="travelRequestId">Id</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<TravelRequest> GetCompleteInfoAsync(int travelRequestId, PictureType pictureType);

        /// <summary>
        /// Get All requests
        /// </summary>
        /// <returns>List of travelRequests</returns>
        Task<IEnumerable<TravelRequest>> GetAllAsync();

        /// <summary>
        /// Get All status for the specified user
        /// </summary>
        /// <param name="employeeIdentity">employee Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<TravelRequest>> GetUserTravelRequestsAsync(string employeeIdentity, string filter, int status, int pageSize, int pageCount);

        /// <summary>
        /// Get all not finished travels for the specified user
        /// </summary>
        /// <param name="employeeIdentity">employee Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<TravelRequest>> GetNotFinishedUserTravelRequestsAsync(string employeeIdentity, string filter, int status);

        /// <summary>
        /// Get all requests for the specified manager
        /// </summary>
        /// <param name="managerIdentity">manager Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<TravelRequest>> GetTeamTravelRequestsAsync(string managerIdentity, string filter, int status, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get all not finished requests for the specified manager
        /// </summary>
        /// <param name="managerIdentity">manager Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<TravelRequest>> GetNotFinishedTeamTravelRequestsAsync(string managerIdentity, string filter, int status, PictureType pictureType);

        /// <summary>
        /// Get All requests
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync(string filter, int status, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get Count for the specified user 
        /// </summary>
        /// <param name="employeeIdentity">employee Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        Task<int> GetUserCountAsync(string employeeIdentity, string filter, int status);

        /// <summary>
        /// Get travels count by city in last year and month
        /// </summary>
        /// <param name="managerIdentity">identity of the team manager</param>
        /// <param name="maxPicturesPerCity">max pictures retrieved per city</param>
        /// <returns>a list with the travel distribution</returns>
        Task<IEnumerable<TravelDistribution>> GetTeamTravelDistributionAsync(string managerIdentity, int maxPicturesPerCity);

        /// <summary>
        /// Get Count for the manager that makes the request
        /// </summary>
        /// <param name="managerIdentity">manager Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        int GetTeamCount(string managerIdentity, string filter, int status);

        /// <summary>
        /// Get Count for the manager that makes the request
        /// </summary>
        /// <param name="managerIdentity">manager Identity</param>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        Task<int> GetTeamCountAsync(string managerIdentity, string filter, int status);

        /// <summary>
        /// Get all travel requests count
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        Task<int> GetAllCountAsync(string filter, int status);

        /// <summary>
        /// Add new request
        /// </summary>
        /// <param name="travelRequest">request information</param>
        /// <returns>requestId</returns>
        Task<int> AddAsync(TravelRequest travelRequest);

        /// <summary>
        /// Update request
        /// </summary>
        /// <param name="travelRequest">request information</param>
        Task UpdateAsync(TravelRequest travelRequest);

        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="travelRequestId">request to delete</param>
        Task DeleteAsync(int travelRequestId);
    }
}
