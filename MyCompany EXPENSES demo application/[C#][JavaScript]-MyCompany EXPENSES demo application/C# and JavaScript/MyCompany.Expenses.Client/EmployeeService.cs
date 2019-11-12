
namespace MyCompany.Expenses.Client
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Expenses.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Expenses.Client.IEmployeeService"/>
    /// </summary>
    internal class EmployeeService : BaseRequest, IEmployeeService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public EmployeeService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IEmployeeService"/>
        /// </summary>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Client.IEmployeeService"/></param>
        /// <returns></returns>
        public async Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/{1}", _urlPrefix, (int)pictureType);

            return await base.GetAsync<Employee>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.IEmployeeService"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Client.IEmployeeService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Client.IEmployeeService"/></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployee(int employeeId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/{1}/{2}", _urlPrefix, employeeId, (int)pictureType);

            return await base.GetAsync<Employee>(url);
        }

    }
}
