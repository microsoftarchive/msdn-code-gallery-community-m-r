
namespace MyCompany.Travel.Client
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Travel.Client.Web;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="MyCompany.Travel.Client.IEmployeeService"/>
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
        /// <see cref="MyCompany.Travel.Client.IEmployeeService"/>
        /// </summary>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Client.IEmployeeService"/></param>
        /// <returns></returns>
        public async Task<Employee> GetLoggedEmployeeInfo(PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees/{1}", _urlPrefix, (int)pictureType);

            return await base.GetAsync<Employee>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.IEmployeeService"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Employee>> GetEmployees()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/employees", _urlPrefix);

            return await base.GetAsync<IList<Employee>>(url);
        }
    }
}
