
namespace MyCompany.Visitors.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Visitors.Data;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    class RemoveEmployeeMessageHandler
       : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteEmployee;
        }

        public override void Handle(BrokeredMessage message)
        {
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            int employeeId = message.GetBody<int>();
            
            employeeRepository.DeleteAsync(employeeId).Wait();
        }
    }
}
