namespace MyShuttle.Client.Core.ServiceAgents
{
    using Web;


    internal class CustomersService : BaseRequest, ICustomersService
    {
        public CustomersService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

    }
}
