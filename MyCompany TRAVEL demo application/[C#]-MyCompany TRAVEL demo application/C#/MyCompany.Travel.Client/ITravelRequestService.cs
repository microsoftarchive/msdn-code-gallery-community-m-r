namespace MyCompany.Travel.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the TravelRequest webapi Controller 
    /// </summary>
    public interface ITravelRequestService
    {
        /// <summary>
        /// Get TravelRequest by Id
        /// </summary>
        /// <param name="travelRequestId"></param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<TravelRequest> Get(int travelRequestId, PictureType pictureType);

        /// <summary>
        /// Get All TravelRequests for the user that makes the request
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Page Number</param>
        /// <returns>List of TravelRequests</returns>
        Task<IList<TravelRequest>> GetUserTravelRequests(string filter, int status, int pageSize, int pageCount);

        /// <summary>
        /// Get all not finished TravelRequests for the user that makes the request
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>List of TravelRequests</returns>
        Task<IList<TravelRequest>> GetNotFinishedUserTravelRequests(string filter, int status);

        /// <summary>
        /// Get All TravelRequests for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Page Number</param>
        /// <returns>List of TravelRequests</returns>
        Task<IList<TravelRequest>> GetTeamTravelRequests(string filter, int status, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get all not finished TravelRequests for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of TravelRequests</returns>
        Task<IList<TravelRequest>> GetNotFinishedTeamTravelRequests(string filter, int status, PictureType pictureType);

        /// <summary>
        /// Get All TravelRequests 
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageCount">Page Number</param>
        /// <returns>List of TravelRequests</returns>
        Task<IList<TravelRequest>> GetAllTravelRequests(string filter, int status, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get Count for the user that makes the request
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>List of TravelRequests</returns>
        Task<int> GetUserCount(string filter, int status);

        /// <summary>
        /// Get Count for the manager that makes the request
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>List of TravelRequests</returns>
        Task<int> GetTeamCount(string filter, int status);

        /// <summary>
        /// Get all TravelRequests count 
        /// </summary>
        /// <param name="filter">Filter Text</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>List of TravelRequests</returns>
        Task<int> GetAllCount(string filter, int status);

        /// <summary>
        /// Get team travels distribution
        /// </summary>
        /// <returns>List of TravelDistribution</returns>
        Task<IList<TravelDistribution>> GetTeamTravelDistribution();

        /// <summary>
        /// Add new TravelRequest.
        /// </summary>
        /// <param name="travelRequest">TravelRequest information</param>
        /// <returns>TravelRequestId</returns>
        Task<int> Add(TravelRequest travelRequest);

        /// <summary>
        /// Update TravelRequest.
        /// </summary>
        /// <param name="travelRequest">TravelRequest information</param>
        Task Update(TravelRequest travelRequest);

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="travelRequestId">Id to update</param>
        /// <param name="status">TravelRequestStatus</param>
        /// <param name="comments">comments</param>
        Task UpdateStatus(int travelRequestId, TravelRequestStatus status, string comments);

        /// <summary>
        /// Delete TravelRequest
        /// </summary>
        /// <param name="travelRequestId">travelRequestId</param>
        Task Delete(int travelRequestId);
    }
}
