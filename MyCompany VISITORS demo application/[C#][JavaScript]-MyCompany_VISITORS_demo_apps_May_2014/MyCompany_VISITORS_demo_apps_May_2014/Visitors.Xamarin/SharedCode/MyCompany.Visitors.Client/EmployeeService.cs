

namespace MyCompany.Visitors.Client
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyCompany.Visitors.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Visitors.Client.IEmployeeService"/>
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
        /// <see cref="MyCompany.Visitors.Client.IEmployeeService"/>
        /// </summary>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <returns></returns>
        public async Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/current/{1}", _urlPrefix, (int)pictureType);

            return await base.GetAsync<Employee>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IEmployeeService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <returns></returns>
        public async Task<IList<Employee>> GetEmployees(string filter, PictureType pictureType, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/GetEmployees?filter={1}&pictureType={2}&pageSize={3}&pageCount={4}", _urlPrefix, filter, (int)pictureType, pageSize, pageCount);

            return await base.GetAsync<IList<Employee>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IEmployeeService"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IEmployeeService"/></param>
        /// <returns></returns>
        public async Task<Employee> Get(int employeeId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/{1}/{2}", _urlPrefix, employeeId, (int)pictureType);

            return await base.GetAsync<Employee>(url);
        }
    }
}
