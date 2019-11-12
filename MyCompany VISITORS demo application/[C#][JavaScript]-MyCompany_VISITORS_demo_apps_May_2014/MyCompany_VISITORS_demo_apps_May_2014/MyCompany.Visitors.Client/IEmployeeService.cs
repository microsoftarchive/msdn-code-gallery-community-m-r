namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client.Web;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the employee webapi Controller 
    /// </summary>
    public interface IEmployeeService : IBaseRequest
    {
        /// <summary>
        /// GetByVisitor logged employee info
        /// </summary>
        /// <param name="pictureType">PictureType</param>
        /// <returns></returns>
        Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType);

        /// <summary>
        /// GetByVisitor Employees
        /// </summary>
        /// <param name="filter">Text to filter</param>
        /// <param name="pictureType">Type Picture</param>
        /// <param name="pageSize">Size of the Page</param>
        /// <param name="pageCount">Page count</param>
        /// <returns></returns>
        Task<IList<Employee>> GetEmployees(string filter, PictureType pictureType, int pageSize, int pageCount);

        /// <summary>
        /// GetByVisitor Employee
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="pictureType">Type Picture</param>
        /// <returns></returns>
        Task<Employee> Get(int employeeId, PictureType pictureType);
    }
}
