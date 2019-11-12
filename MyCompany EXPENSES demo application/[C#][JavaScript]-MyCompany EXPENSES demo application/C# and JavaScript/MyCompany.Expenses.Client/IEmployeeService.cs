namespace MyCompany.Expenses.Client
{
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
        /// Get employeeInfo
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<Employee> GetEmployee(int employeeId, PictureType pictureType);
    }
}
