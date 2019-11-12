namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{

    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
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
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeeDTO>();
            var employee = Mapper.Map<Employee>(dto);
            
            employeeRepository.UpdateAsync(employee).Wait();
        }
    }
}
