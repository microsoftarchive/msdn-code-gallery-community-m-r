using MyCompany.Vacation.Client.Tests.ODataServiceReference;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Vacation.Client.Tests.OData
{
    /// <summary>
    /// OData helper
    /// </summary>
    public class ContainerFactory
    {
        /// <summary>
        /// Creates a container with the default settings.
        /// </summary>
        /// <returns></returns>
        public Container Create() {
            var container = new Container(new Uri(SecurityHelper.ODataUrlBase));
            container.SendingRequest2 += SetAuthorizationToken;
            return container;
        }
        public void SetAuthorizationToken(object sender, SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", SecurityHelper.AccessToken);
        }
    }
}
