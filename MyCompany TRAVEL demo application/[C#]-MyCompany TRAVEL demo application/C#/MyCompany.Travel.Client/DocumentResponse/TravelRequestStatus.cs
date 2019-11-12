
namespace MyCompany.Travel.Client
{
    /// <summary>
    /// Travel Request Status
    /// </summary>
    public enum TravelRequestStatus
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Travel is requested by employee
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Manager has approved the travel
        /// </summary>
        Approved = 2,
        /// <summary>
        /// The request is completed with all the information for the travel.
        /// </summary>
        Completed = 4,
        /// <summary>
        /// Denied
        /// </summary>
        Denied = 8
    }
}
