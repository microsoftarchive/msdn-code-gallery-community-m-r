namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
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
            var employeeRepository = new EmployeeRepository(new Data.MyCompanyContext());
            int employeeId = message.GetBody<int>();

            employeeRepository.DeleteAsync(employeeId).Wait();
        }
    }
}
