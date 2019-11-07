namespace MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using System;


    /// <summary>
    /// Represent the add new employee message handler
    /// </summary>
    public sealed class AddNewEmployeeMessageHandler
        : MessageHandler
    {
        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeeDTO>();
            var employee = Mapper.Map<Employee>(dto);

            employeeRepository.Add(employee);
        }

        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddEmployee;
        }
    }
}
