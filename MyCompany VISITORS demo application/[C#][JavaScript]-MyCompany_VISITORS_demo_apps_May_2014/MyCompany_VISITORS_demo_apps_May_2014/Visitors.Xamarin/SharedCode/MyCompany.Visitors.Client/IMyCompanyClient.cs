
namespace MyCompany.Visitors.Client
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
        /// Interfaz to access to the Visitor Service
        /// </summary>
        IVisitorService VisitorService { get; }

        /// <summary>
        /// Interfaz to access to the Visit Service 
        /// </summary>
        IVisitService VisitService { get; }
        
        /// <summary>
        /// Interfaz to access to the Visitor picture Service 
        /// </summary>
        IVisitorPictureService VisitorPictureService { get; }        

        /// <summary>
        /// Interface to access to the security services
        /// </summary>
        ISecurityService SecurityService { get; }
        
        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="token"></param>
        void RefreshToken(string token);
    }
}
