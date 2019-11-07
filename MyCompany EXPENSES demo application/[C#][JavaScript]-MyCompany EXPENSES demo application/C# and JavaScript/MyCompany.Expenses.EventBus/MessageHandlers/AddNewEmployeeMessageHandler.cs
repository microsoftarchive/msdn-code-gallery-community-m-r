namespace MyCompany.Expenses.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.Model;
    using System;

    class AddNewEmployeeMessageHandler
        : MessageHandler
    {

        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddEmployee;
        }

        public override void Handle(BrokeredMessage message)
        {
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeeDTO>();
            var employee = Mapper.Map<Employee>(dto);

            var current = employeeRepository.GetAsync(employee.EmployeeId).Result;

            if (current == null)
            {
                employeeRepository.AddAsync(employee).Wait();
            }
        }
    }
}
