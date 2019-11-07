
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Vacation.Model;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Base contract for vacation request repository
    /// </summary>
    public interface IVacationRequestRepository
        : IDisposable
    {
        /// <summary>
        /// Get vacation request by Id
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <returns></returns>
        VacationRequest Get(int vacationRequestId);

        /// <summary>
        /// Get All vacation requests
        /// </summary>
        /// <returns>List of vacation requests</returns>
        IEnumerable<VacationRequest> GetAll();

        /// <summary>
        /// Add new vacation request
        /// </summary>
        /// <param name="vacationRequest">vacation request information</param>
        /// <returns>vacationRequestId</returns>
        int Add(VacationRequest vacationRequest);

        /// <summary>
        /// Update vacation request
        /// </summary>
        /// <param name="vacationRequest">vacation request information</param>
        void Update(VacationRequest vacationRequest);

        /// <summary>
        /// Delete vacation request
        /// </summary>
        /// <param name="vacationRequestId">vacation request to delete</param>
        void Delete(int vacationRequestId);

        /// <summary>
        /// Get vacationrequests for the user that makes the request. 
        /// </summary>
        /// <param name="employeeIdentity">Employee Identity</param>
        /// <param name="year">Year.</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<VacationRequest>> GetUserVacationRequests(string employeeIdentity, int year);

        /// <summary>
        /// Get vacationrequests for the user that makes the request. 
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="year">Year.</param>
        /// <returns>List of requests</returns>
        IEnumerable<VacationRequest> GetUserVacationRequests(int employeeId, int year);

        /// <summary>
        /// Get Vacation Requests Count for the user that makes the request
        /// </summary>
        /// <param name="employeeIdentity">Employee Identity</param>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        int GetUserCount(string employeeIdentity, int? month, int year, int status);

        /// <summary>
        /// Get Vacation Requests Count for the user that makes the request
        /// </summary>
        /// <param name="employeeIdentity">Employee Identity</param>
        /// <param name="year">Year.</param>
        /// <returns>Number of oending requests</returns>
        int GetUserPendingVacation(string employeeIdentity, int year);

        /// <summary>
        /// Get All requests for the manager that makes the request
        /// </summary>
        /// <param name="managerIdentity">Manager Identity</param>
        /// <param name="filter">Filter used for searching</param>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of requests</returns>
        IEnumerable<VacationRequest> GetTeamVacationRequests(string managerIdentity, string filter, int? month, int year, int status, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// Get Vacation Requests Count for the manager that makes the request
        /// </summary>
        /// <param name="managerIdentity">Manager Identity</param>
        /// <param name="filter">Filter used for searching</param>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <returns>Number of requests</returns>
        int GetTeamCount(string managerIdentity, string filter, int? month, int year, int status);

        /// <summary>
        /// Get All requests for the manager that makes the request group by employee
        /// </summary>
        /// <param name="managerIdentity">Manager Identity</param>
        /// <param name="month">Month. Null returns all request for the current year.</param>
        /// <param name="year">Year.</param>
        /// <param name="status">Flags to filter by status</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns>List of requests</returns>
        Task<IEnumerable<Employee>> GetTeamVacationRequestsByEmployee(string managerIdentity, int? month, int year, int status, PictureType pictureType);
    }
}
