
namespace MyCompany.Vacation.Model
{
    /// <summary>
    /// Vacation Request Status
    /// </summary>
    public enum VacationRequestStatus
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Vacation is requested by employee
        /// </summary>
        Pending = 1,
        /// <summary>
        /// RRHH has validated the vacation, the workflow is completed!
        /// </summary>
        Approved = 2,
        /// <summary>
        /// Denied
        /// </summary>
        Denied = 3
    }
}
