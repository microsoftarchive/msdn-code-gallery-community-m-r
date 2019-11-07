
namespace MyCompany.Travel.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the employee webapi Controller 
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Get logged employee info
        /// </summary>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType);

        /// <summary>
        /// Get employees 
        /// </summary>
        /// <returns></returns>
        Task<IList<Employee>> GetEmployees();
    }
}
