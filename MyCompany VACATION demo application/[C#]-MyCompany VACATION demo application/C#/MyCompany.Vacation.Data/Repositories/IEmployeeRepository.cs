
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Vacation.Model;
    using System;

    /// <summary>
    /// Base contract for employee repository
    /// </summary>
    public interface IEmployeeRepository
        : IDisposable
    {
        /// <summary>
        /// Get employee by Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Employee Get(int employeeId);

        /// <summary>
        /// Get employee by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pictureType"></param>
        /// <returns></returns>
        Employee GetByEmail(string email, PictureType pictureType);

        /// <summary>
        /// Get All employees
        /// </summary>
        /// <returns>List of employees</returns>
        IEnumerable<Employee> GetAll();

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="employee">employee information</param>
        /// <returns>commentId</returns>
        int Add(Employee employee);

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="expense">employee information</param>
        void Update(Employee expense);

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="employeeId">employee to delete</param>
        void Delete(int employeeId);
    }
}
