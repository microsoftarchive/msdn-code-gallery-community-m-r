
namespace MyCompany.Travel.Client
{
    /// <summary>
    /// Interfaz to access to the WebApi controllers 
    /// </summary>
    public interface IMyCompanyClient
    {
        /// <summary>
        /// Interfaz to access to the Employee Service 
        /// </summary>
        IEmployeeService EmployeeService { get; }

        /// <summary>
        /// Interfaz to access to the Travel Request Service 
        /// </summary>
        ITravelRequestService TravelRequestService { get; }

        /// <summary>
        /// Interfaz to access to the Travel attachment Service 
        /// </summary>
        ITravelAttachmentService TravelAttachmentService { get; }

        /// <summary>
        /// Interfaz to access to the Account Service 
        /// </summary>
        ISecurityService AccountService { get; }
    }
}
