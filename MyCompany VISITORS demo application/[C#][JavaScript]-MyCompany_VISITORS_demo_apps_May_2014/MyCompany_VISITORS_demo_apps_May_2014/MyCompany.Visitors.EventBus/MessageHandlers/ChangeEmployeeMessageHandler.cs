namespace MyCompany.Visitors.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Visitors.Data;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System;


    class ChangeEmployeeMessageHandler
        : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateEmployee;
        }

        public override void Handle(BrokeredMessage message)
        {
            var context = new MyCompanyContext();
            var employeeRepository = new EmployeeRepository(context);
            var dto = message.GetBody<EmployeeDTO>();
            var employee = Mapper.Map<Employee>(dto);

            employeeRepository.UpdateAsync(employee).Wait();
        }
    }
}
