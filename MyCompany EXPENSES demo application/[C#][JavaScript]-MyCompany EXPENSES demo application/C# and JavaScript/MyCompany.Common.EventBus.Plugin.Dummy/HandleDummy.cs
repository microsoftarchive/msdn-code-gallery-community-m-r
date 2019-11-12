
namespace MyCompany.Common.EventBus.Plugin.Dummy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IHandler"/>
    /// </summary>
    public class HandleDummy :  IHandler
    {
        public void Handle(BrokeredMessageEventArgs e)
        {

        }
    }
}
