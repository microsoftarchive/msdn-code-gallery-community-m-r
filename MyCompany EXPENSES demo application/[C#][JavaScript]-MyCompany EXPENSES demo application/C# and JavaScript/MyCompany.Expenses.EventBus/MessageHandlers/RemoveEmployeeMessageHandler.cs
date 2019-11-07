
namespace MyCompany.Expenses.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.Model;
    using System;

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
