namespace MyShuttle.Client.Core.ServiceAgents
{
    using System;
    using System.Threading.Tasks;
    using DocumentResponse;
    using Web;
    using System.Globalization;

    internal class EmployeesService : BaseRequest, IEmployeesService
    {
        public EmployeesService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        public async Task<Employee> GetMyProfileAsync()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}employees/myprofile", _urlPrefix);

            return await base.GetAsync<Employee>(url);
        }
    }
}
